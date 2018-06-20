/*
 * TerminalIf.cs
 * xux
 * 2018-05-29
 * 体彩终端接口
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RestSharp;
using NLog;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.Management;
using System.Net;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

using LotteryBaseLib.Public;

namespace LotteryBaseLib.TerminalIf
{


    /// <summary>
    /// PostCreated类
    /// </summary>
    public class PostCreated
    {
        /// <summary>
        /// 连接已创建
        /// </summary>
        public string created { get; set; }
    }

    /// <summary>
    /// TerminalIf接口实现类
    /// </summary>
    public class TerminalIfHandler : ITerminalIfHandler
    {
        readonly string _accoountId;
        readonly string _secretKey;
        readonly string _baseUrl;
        private string _deviceAlias { get; set; }
        private string _bearerHeader { get; set; }
        private int _maxRetry { get; set; }

        /// <summary>
        /// TerminalIf
        /// </summary>
        public TerminalIfHandler()
        {
            _accoountId = "";
            _secretKey = "";
            _baseUrl = "https://new-orator.com/gateway/front";
            _deviceAlias = "dummy";
            _maxRetry = 3;
            _bearerHeader = "";            
        }

        /// <summary>
        /// TerminalIf
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="accountId"></param>
        /// <param name="screctKey"></param>
        /// <param name="deviceAlias"></param>
        /// <param name="maxRetry"></param>
        public TerminalIfHandler(string baseUrl, string accountId, string screctKey, string deviceAlias, int maxRetry)
        {
            _baseUrl = "https://" + baseUrl;
            _accoountId = accountId;
            _secretKey = screctKey;
            _deviceAlias = deviceAlias;
            _maxRetry = maxRetry;
        }

        private IRestResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            IRestResponse<T> response;
            var client = new RestClient();
            client.BaseUrl = new Uri(_baseUrl);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());
            client.Timeout = 15000;
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            response = client.Execute<T>(request);

            return response;
        }

        #region 1.	初始化
        private TerminalInitRsp _TerminalInit(TerminalInitReq terminalinitreq, int numRetry)
        {
            try
            {
                var request = new RestRequest("/gateway/front/", Method.POST);

                string message = "";
                TerminalInitRsp terminalinitrsp = new TerminalInitRsp();

                if (numRetry <= 0)
                {
                    return null;
                }

                message = JsonTools.ObjectToJson(terminalinitreq);

                Console.WriteLine("请求:"+message);
            
                request.AddParameter("application/json", message, ParameterType.RequestBody);
                var response = Execute<PostCreated>(request);
                PublicLib.logger.Error("StatusCode: " + response.StatusCode.ToString());
                if ((int)response.StatusCode == 401 || _bearerHeader == "")
                {
                    //GetToken();
                    return this._TerminalInit(terminalinitreq, numRetry - 1);
                }
                else if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
                {
                    //验签
                    PublicLib.logger.Info("\n应答:" + response.Content);
                    string toSignData = JsonTools.GetDedicatedKeyFromJson(response.Content, "responseData");                    
                    PublicLib.logger.Info("\n待验签数据:" + toSignData);
                    string signStr = JsonTools.GetDedicatedKeyFromJson(response.Content, "sign");
                    PublicLib.logger.Info("签名:" + signStr);
                    PublicLib.logger.Info("公钥:" + PublicLib.publicky);
                    bool ret = SHA1WithRSA.verify(toSignData, signStr, PublicLib.publicky, "UTF-8");                    
                    PublicLib.logger.Info("签名验证:" + ret);
                    if (ret == false)
                    {
                        terminalinitrsp.respCode = "9999";
                        terminalinitrsp.respDesc = "初始化数据签名验证失败";
                        return terminalinitrsp;
                    }
                    //解析Response
                    terminalinitrsp = (TerminalInitRsp)JsonTools.JsonToObject(response.Content, terminalinitrsp);

                    if ((terminalinitrsp.respCode == "") || (terminalinitrsp.respCode == null))
                    {
                        terminalinitrsp.respCode = response.StatusCode.ToString();
                    }
                    return terminalinitrsp;
                }
                else
                {
                    return this._TerminalInit(terminalinitreq, numRetry - 1);
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("_TerminalInit: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 2.	预下单
        private PrepOrderRsp _PrepOrder(PrepOrderReq preporderreq, int numRetry)
        {
            try
            {
                var request = new RestRequest("/gateway/front/", Method.POST);

                string message = "";
                PrepOrderRsp preporderrsp = new PrepOrderRsp();

                if (numRetry <= 0)
                {
                    return null;
                }

                message = JsonTools.ObjectToJson(preporderreq);

                Console.WriteLine("请求:" + message);

                request.AddParameter("application/json", message, ParameterType.RequestBody);
                var response = Execute<PostCreated>(request);
                PublicLib.logger.Error("StatusCode: " + response.StatusCode.ToString());
                if ((int)response.StatusCode == 401 || _bearerHeader == "")
                {
                    //GetToken();
                    return this._PrepOrder(preporderreq, numRetry - 1);
                }
                else if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
                {
                    //验签
                    PublicLib.logger.Info("\n应答:" + response.Content);
                    string toSignData = JsonTools.GetDedicatedKeyFromJson(response.Content, "responseData");
                    PublicLib.logger.Info("\n待验签数据:" + toSignData);
                    string signStr = JsonTools.GetDedicatedKeyFromJson(response.Content, "sign");
                    PublicLib.logger.Info("签名:" + signStr);
                    PublicLib.logger.Info("公钥:" + PublicLib.publicky);
                    bool ret = SHA1WithRSA.verify(toSignData, signStr, PublicLib.publicky, "UTF-8");
                    PublicLib.logger.Info("签名验证:" + ret);
                    if (ret == false)
                    {
                        preporderrsp.respCode = "9999";
                        preporderrsp.respDesc = "预下单数据签名验证失败";
                        return preporderrsp;
                    }
                    //解析Response
                    preporderrsp = (PrepOrderRsp)JsonTools.JsonToObject(response.Content, preporderrsp);

                    if ((preporderrsp.respCode == "") || (preporderrsp.respCode == null))
                    {
                        preporderrsp.respCode = response.StatusCode.ToString();
                    }
                    return preporderrsp;
                }
                else
                {
                    return this._PrepOrder(preporderreq, numRetry - 1);
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("_PrepOrder: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 3.	交易查询
        private QueryOrderRsp _QueryOrder(QueryOrderReq queryorderreq, int numRetry)
        {
            try
            {
                var request = new RestRequest("/gateway/front/", Method.POST);

                string message = "";
                QueryOrderRsp queryorderrsp = new QueryOrderRsp();

                if (numRetry <= 0)
                {
                    return null;
                }

                message = JsonTools.ObjectToJson(queryorderreq);

                Console.WriteLine("请求:" + message);

                request.AddParameter("application/json", message, ParameterType.RequestBody);
                var response = Execute<PostCreated>(request);
                PublicLib.logger.Error("StatusCode: " + response.StatusCode.ToString());
                if ((int)response.StatusCode == 401 || _bearerHeader == "")
                {
                    //GetToken();
                    return this._QueryOrder(queryorderreq, numRetry - 1);
                }
                else if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
                {
                    //验签
                    PublicLib.logger.Info("\n应答:" + response.Content);
                    string toSignData = JsonTools.GetDedicatedKeyFromJson(response.Content, "responseData");
                    PublicLib.logger.Info("\n待验签数据:" + toSignData);
                    string signStr = JsonTools.GetDedicatedKeyFromJson(response.Content, "sign");
                    PublicLib.logger.Info("签名:" + signStr);
                    PublicLib.logger.Info("公钥:" + PublicLib.publicky);
                    bool ret = SHA1WithRSA.verify(toSignData, signStr, PublicLib.publicky, "UTF-8");
                    PublicLib.logger.Info("签名验证:" + ret);
                    if (ret == false)
                    {
                        queryorderrsp.respCode = "9999";
                        queryorderrsp.respDesc = "交易查询数据签名验证失败";
                        return queryorderrsp;
                    }
                    //解析Response
                    queryorderrsp = (QueryOrderRsp)JsonTools.JsonToObject(response.Content, queryorderrsp);

                    if ((queryorderrsp.respCode == "") || (queryorderrsp.respCode == null))
                    {
                        queryorderrsp.respCode = response.StatusCode.ToString();
                    }
                    return queryorderrsp;
                }
                else
                {
                    return this._QueryOrder(queryorderreq, numRetry - 1);
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("_QueryOrder: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 4.	出票状态更新
        private OutTicketRsp _OutTicket(OutTicketReq outticketreq, int numRetry)
        {
            try
            {
                var request = new RestRequest("/gateway/front/", Method.POST);

                string message = "";
                OutTicketRsp outticketrsp = new OutTicketRsp();

                if (numRetry <= 0)
                {
                    return null;
                }

                message = JsonTools.ObjectToJson(outticketreq);

                Console.WriteLine("请求:" + message);

                request.AddParameter("application/json", message, ParameterType.RequestBody);
                var response = Execute<PostCreated>(request);
                PublicLib.logger.Error("StatusCode: " + response.StatusCode.ToString());
                if ((int)response.StatusCode == 401 || _bearerHeader == "")
                {
                    //GetToken();
                    return this._OutTicket(outticketreq, numRetry - 1);
                }
                else if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
                {
                    //验签
                    PublicLib.logger.Info("\n应答:" + response.Content);
                    string toSignData = JsonTools.GetDedicatedKeyFromJson(response.Content, "responseData");
                    PublicLib.logger.Info("\n待验签数据:" + toSignData);
                    string signStr = JsonTools.GetDedicatedKeyFromJson(response.Content, "sign");
                    PublicLib.logger.Info("签名:" + signStr);
                    PublicLib.logger.Info("公钥:" + PublicLib.publicky);
                    bool ret = SHA1WithRSA.verify(toSignData, signStr, PublicLib.publicky, "UTF-8");
                    PublicLib.logger.Info("签名验证:" + ret);
                    if (ret == false)
                    {
                        outticketrsp.respCode = "9999";
                        outticketrsp.respDesc = "出票状态更新数据签名验证失败";
                        return outticketrsp;
                    }
                    //解析Response
                    outticketrsp = (OutTicketRsp)JsonTools.JsonToObject(response.Content, outticketrsp);

                    if ((outticketrsp.respCode == "") || (outticketrsp.respCode == null))
                    {
                        outticketrsp.respCode = response.StatusCode.ToString();
                    }
                    return outticketrsp;
                }
                else
                {
                    return this._OutTicket(outticketreq, numRetry - 1);
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("_OutTicket: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 5.	终端兑奖
        private CashPrizeRsp _CashPrize(CashPrizeReq cashprizereq, int numRetry)
        {
            try
            {
                var request = new RestRequest("/gateway/front/", Method.POST);

                string message = "";
                CashPrizeRsp cashprizersp = new CashPrizeRsp();

                if (numRetry <= 0)
                {
                    return null;
                }

                message = JsonTools.ObjectToJson(cashprizereq);

                Console.WriteLine("请求:" + message);

                request.AddParameter("application/json", message, ParameterType.RequestBody);
                var response = Execute<PostCreated>(request);
                PublicLib.logger.Error("StatusCode: " + response.StatusCode.ToString());
                if ((int)response.StatusCode == 401 || _bearerHeader == "")
                {
                    //GetToken();
                    return this._CashPrize(cashprizereq, numRetry - 1);
                }
                else if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
                {
                    //验签
                    PublicLib.logger.Info("\n应答:" + response.Content);
                    string toSignData = JsonTools.GetDedicatedKeyFromJson(response.Content, "responseData");
                    PublicLib.logger.Info("\n待验签数据:" + toSignData);
                    string signStr = JsonTools.GetDedicatedKeyFromJson(response.Content, "sign");
                    PublicLib.logger.Info("签名:" + signStr);
                    PublicLib.logger.Info("公钥:" + PublicLib.publicky);
                    bool ret = SHA1WithRSA.verify(toSignData, signStr, PublicLib.publicky, "UTF-8");
                    PublicLib.logger.Info("签名验证:" + ret);
                    if (ret == false)
                    {
                        cashprizersp.respCode = "9999";
                        cashprizersp.respDesc = "终端兑奖数据签名验证失败";
                        return cashprizersp;
                    }
                    //解析Response
                    cashprizersp = (CashPrizeRsp)JsonTools.JsonToObject(response.Content, cashprizersp);

                    if ((cashprizersp.respCode == "") || (cashprizersp.respCode == null))
                    {
                        cashprizersp.respCode = response.StatusCode.ToString();
                    }
                    return cashprizersp;
                }
                else
                {
                    return this._CashPrize(cashprizereq, numRetry - 1);
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("_CashPrize: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 6.	派奖
        private AwardOrderRsp _AwardOrder(AwardOrderReq awardorderreq, int numRetry)
        {
            try
            {
                var request = new RestRequest("/gateway/front/", Method.POST);

                string message = "";
                AwardOrderRsp awardadsrsp = new AwardOrderRsp();

                if (numRetry <= 0)
                {
                    return null;
                }

                message = JsonTools.ObjectToJson(awardorderreq);

                Console.WriteLine("请求:" + message);

                request.AddParameter("application/json", message, ParameterType.RequestBody);
                var response = Execute<PostCreated>(request);
                PublicLib.logger.Error("StatusCode: " + response.StatusCode.ToString());
                if ((int)response.StatusCode == 401 || _bearerHeader == "")
                {
                    //GetToken();
                    return this._AwardOrder(awardorderreq, numRetry - 1);
                }
                else if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
                {
                    //验签
                    PublicLib.logger.Info("\n应答:" + response.Content);
                    string toSignData = JsonTools.GetDedicatedKeyFromJson(response.Content, "responseData");
                    PublicLib.logger.Info("\n待验签数据:" + toSignData);
                    string signStr = JsonTools.GetDedicatedKeyFromJson(response.Content, "sign");
                    PublicLib.logger.Info("签名:" + signStr);
                    PublicLib.logger.Info("公钥:" + PublicLib.publicky);
                    bool ret = SHA1WithRSA.verify(toSignData, signStr, PublicLib.publicky, "UTF-8");
                    PublicLib.logger.Info("签名验证:" + ret);
                    if (ret == false)
                    {
                        awardadsrsp.respCode = "9999";
                        awardadsrsp.respDesc = "派奖数据签名验证失败";
                        return awardadsrsp;
                    }
                    //解析Response
                    awardadsrsp = (AwardOrderRsp)JsonTools.JsonToObject(response.Content, awardadsrsp);

                    if ((awardadsrsp.respCode == "") || (awardadsrsp.respCode == null))
                    {
                        awardadsrsp.respCode = response.StatusCode.ToString();
                    }
                    return awardadsrsp;
                }
                else
                {
                    return this._AwardOrder(awardorderreq, numRetry - 1);
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("_AwardOrder: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 7.	终端状态同步
        private TerminalUpdateRsp _TerminalUpdate(TerminalUpdateReq terminalupdatereq, int numRetry)
        {
            try
            {
                var request = new RestRequest("/gateway/front/", Method.POST);

                string message = "";
                TerminalUpdateRsp terminalupdatersp = new TerminalUpdateRsp();

                if (numRetry <= 0)
                {
                    return null;
                }

                message = JsonTools.ObjectToJson(terminalupdatereq);

                Console.WriteLine("请求:" + message);

                request.AddParameter("application/json", message, ParameterType.RequestBody);
                var response = Execute<PostCreated>(request);
                PublicLib.logger.Error("StatusCode: " + response.StatusCode.ToString());
                if ((int)response.StatusCode == 401 || _bearerHeader == "")
                {
                    //GetToken();
                    return this._TerminalUpdate(terminalupdatereq, numRetry - 1);
                }
                else if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
                {
                    //验签
                    PublicLib.logger.Info("\n应答:" + response.Content);
                    string toSignData = JsonTools.GetDedicatedKeyFromJson(response.Content, "responseData");
                    PublicLib.logger.Info("\n待验签数据:" + toSignData);
                    string signStr = JsonTools.GetDedicatedKeyFromJson(response.Content, "sign");
                    PublicLib.logger.Info("签名:" + signStr);
                    PublicLib.logger.Info("公钥:" + PublicLib.publicky);
                    bool ret = SHA1WithRSA.verify(toSignData, signStr, PublicLib.publicky, "UTF-8");
                    PublicLib.logger.Info("签名验证:" + ret);
                    if (ret == false)
                    {
                        terminalupdatersp.respCode = "9999";
                        terminalupdatersp.respDesc = "终端状态同步数据签名验证失败";
                        return terminalupdatersp;
                    }
                    //解析Response
                    terminalupdatersp = (TerminalUpdateRsp)JsonTools.JsonToObject(response.Content, terminalupdatersp);

                    if ((terminalupdatersp.respCode == "") || (terminalupdatersp.respCode == null))
                    {
                        terminalupdatersp.respCode = response.StatusCode.ToString();
                    }
                    return terminalupdatersp;
                }
                else
                {
                    return this._TerminalUpdate(terminalupdatereq, numRetry - 1);
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("_TerminalUpdate: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 8.	广告查询
        private QueryAdsRsp _QueryAds(QueryAdsReq queryadsreq, int numRetry)
        {
            try
            {
                var request = new RestRequest("/gateway/front/", Method.POST);

                string message = "";
                QueryAdsRsp queryadsrsp = new QueryAdsRsp();

                if (numRetry <= 0)
                {
                    return null;
                }

                message = JsonTools.ObjectToJson(queryadsreq);

                Console.WriteLine("请求:" + message);

                request.AddParameter("application/json", message, ParameterType.RequestBody);
                var response = Execute<PostCreated>(request);
                PublicLib.logger.Error("StatusCode: " + response.StatusCode.ToString());
                if ((int)response.StatusCode == 401 || _bearerHeader == "")
                {
                    //GetToken();
                    return this._QueryAds(queryadsreq, numRetry - 1);
                }
                else if ((int)response.StatusCode == 201 || (int)response.StatusCode == 200)
                {
                    //验签
                    PublicLib.logger.Info("\n应答:" + response.Content);
                    string toSignData = JsonTools.GetDedicatedKeyFromJson(response.Content, "responseData");
                    PublicLib.logger.Info("\n待验签数据:" + toSignData);
                    string signStr = JsonTools.GetDedicatedKeyFromJson(response.Content, "sign");
                    PublicLib.logger.Info("签名:" + signStr);
                    PublicLib.logger.Info("公钥:" + PublicLib.publicky);
                    bool ret = SHA1WithRSA.verify(toSignData, signStr, PublicLib.publicky, "UTF-8");
                    PublicLib.logger.Info("签名验证:" + ret);
                    if (ret == false)
                    {
                        queryadsrsp.respCode = "9999";
                        queryadsrsp.respDesc = "广告查询数据签名验证失败";
                        return queryadsrsp;
                    }
                    //解析Response
                    queryadsrsp = (QueryAdsRsp)JsonTools.JsonToObject(response.Content, queryadsrsp);

                    if ((queryadsrsp.respCode == "") || (queryadsrsp.respCode == null))
                    {
                        queryadsrsp.respCode = response.StatusCode.ToString();
                    }
                    
                    return queryadsrsp;
                }
                else
                {
                    return this._QueryAds(queryadsreq, numRetry - 1);
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("_QueryAds: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Interface
        //1.	初始化
        TerminalInitRsp ITerminalIfHandler.TerminalInit(TerminalInitReq terminalinitreq)
        {

            return this._TerminalInit(terminalinitreq, this._maxRetry);

        }

        //2.	预下单
        PrepOrderRsp ITerminalIfHandler.PrepOrder(PrepOrderReq preporderreq)
        {

            return this._PrepOrder(preporderreq, this._maxRetry);

        }

        //3.	交易查询
        QueryOrderRsp ITerminalIfHandler.QueryOrder(QueryOrderReq queryorderreq)
        {

            return this._QueryOrder(queryorderreq, this._maxRetry);

        }

        //4.	出票状态更新
        OutTicketRsp ITerminalIfHandler.OutTicket(OutTicketReq outticketreq)
        {

            return this._OutTicket(outticketreq, this._maxRetry);

        }

        //5.	终端兑奖
        CashPrizeRsp ITerminalIfHandler.CashPrize(CashPrizeReq cashprizereq)
        {

            return this._CashPrize(cashprizereq, this._maxRetry);

        }

        //6.	派奖
        AwardOrderRsp ITerminalIfHandler.AwardOrder(AwardOrderReq awardorderreq)
        {

            return this._AwardOrder(awardorderreq, this._maxRetry);

        }

        //7.	终端状态更新
        TerminalUpdateRsp ITerminalIfHandler.TerminalUpdate(TerminalUpdateReq terminalupdatereq)
        {

            return this._TerminalUpdate(terminalupdatereq, this._maxRetry);

        }

        //8.	广告查询
        QueryAdsRsp ITerminalIfHandler.QueryAds(QueryAdsReq queryadsreq)
        {

            return this._QueryAds(queryadsreq, this._maxRetry);

        }
        #endregion
    }

    /// <summary>
    /// TerminalIf实现类
    /// </summary>
    public class TerminalIf
    {
        private static XmlDocument xml = new XmlDocument();

        /// <summary>
        ///  CM_BaseURL
        /// </summary>
        public static string CM_BaseURL = "new-orator.com";
        
        /// <summary>
        ///  ControlURL
        /// </summary>
        public static string CM_ControlURL = "new-orator.com";
        
        /// <summary>
        ///  AccountId
        /// </summary>
        public static string CM_AccountId = "";
        
        /// <summary>
        ///  SecretKey
        /// </summary>
        public static string CM_SecretKey = "";
        
        /// <summary>
        /// 调试输出开关
        /// </summary>
        private static bool DebugFlag = false;

        private static bool InitFlag = false;
        
        static ITerminalIfHandler iTerminalIfHandler;

        private static bool Init()
        {
            try
            {
                if (InitFlag == false)
                {
                    Load_Configure();
                    iTerminalIfHandler = new TerminalIfHandler(CM_BaseURL, CM_AccountId, CM_SecretKey, "dummy", 3);
                    InitFlag = true;
                    return true;
                }
                else
                {
                    return true;
                } 
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("Init:"+ex.Message);
                return false;
            }                       
        }

        #region 载入平台配置
        /// <summary>
        /// 载入平台配置
        /// </summary>
        /// <returns></returns>
        public static bool Load_Configure()
        {
            #region 初始化变量
            try
            {
                xml.Load(".\\lotterybaselib_config.xml");
                XmlNode CM_BaseURL_node = xml.SelectSingleNode("//CM_BaseURL");
                XmlNode CM_AccountId_node = xml.SelectSingleNode("//CM_AccountId");
                XmlNode CM_SecretKey_node = xml.SelectSingleNode("//CM_SecretKey");

                XmlNode CM_Category_id_node = xml.SelectSingleNode("//CM_Category_id");
                XmlNode CM_CM_Hardware_version_node = xml.SelectSingleNode("//CM_Hardware_version");
                XmlNode CM_CM_Software_version_node = xml.SelectSingleNode("//CM_Software_version");
                XmlNode CM_DeviceAlias_node = xml.SelectSingleNode("//CM_DeviceAlias");
                XmlNode CM_RetryNum_node = xml.SelectSingleNode("//CM_RetryNum");
                XmlNode CM_IsRegistered_node = xml.SelectSingleNode("//CM_IsRegisted");

                CM_BaseURL = CM_BaseURL_node.InnerText;
                CM_AccountId = CM_AccountId_node.InnerText;
                CM_SecretKey = CM_SecretKey_node.InnerText;

                return true;
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.Load_Configure,: " + ex.Message);
                return false;
            }
            #endregion
        }
        #endregion

        #region 调试开关
        /// <summary>
        /// 调试输出打开
        /// </summary>
        public static void Set_Debug_Flag()
        {
            DebugFlag = true;
            return;
        }

        /// <summary>
        /// 调试输出关闭
        /// </summary>
        public static void Clr_Debug_Flag()
        {
            DebugFlag = false;
            return;
        }
        #endregion

        #region 1.	初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="terminalinitreq">初始化请求</param>
        /// <returns></returns>
        public static TerminalInitRsp TerminalInit(TerminalInitReq terminalinitreq)
        {
            try
            {
                Init();
                //
                terminalinitreq.sign = SHA1WithRSA.sign(JsonTools.ObjectToJson(terminalinitreq.requestData), PublicLib.privatekey, "UTF-8");
                #region
                if (DebugFlag)
                {
                    string str = JsonTools.ObjectToJson(terminalinitreq);
                    PublicLib.logger.Info("调用:初始化:");
                    PublicLib.logger.Info(str);
                }

                TerminalInitRsp rsp = iTerminalIfHandler.TerminalInit(terminalinitreq);
                //
                if (rsp == null)
                {
                    PublicLib.logger.Info("Err:TerminalIf 初始化 失败 ");
                    return null;
                }
                else
                {
                    if (DebugFlag)
                    {
                        PublicLib.logger.Info("OK:TerminalIf 初始化 成功");
                        string str = JsonTools.ObjectToJson(rsp);
                        PublicLib.logger.Info("返回:初始化:");
                        PublicLib.logger.Info(str);
                    }
                    return rsp;
                }

                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.TerminalInit: ex: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 2.	预下单
        /// <summary>
        /// 预下单
        /// </summary>
        /// <param name="preporderreq">预下单请求</param>
        /// <returns></returns>
        public static PrepOrderRsp PrepOrder(PrepOrderReq preporderreq)
        {
            try
            {
                Init();
                //
                preporderreq.sign = SHA1WithRSA.sign(JsonTools.ObjectToJson(preporderreq.requestData), PublicLib.privatekey, "UTF-8");
                #region
                if (DebugFlag)
                {
                    string str = JsonTools.ObjectToJson(preporderreq);
                    PublicLib.logger.Info("调用:预下单:");
                    PublicLib.logger.Info(str);
                }

                PrepOrderRsp rsp = iTerminalIfHandler.PrepOrder(preporderreq);
                //
                if (rsp == null)
                {
                    PublicLib.logger.Info("Err:TerminalIf 预下单 失败 ");
                    return null;
                }
                else
                {
                    if (DebugFlag)
                    {
                        PublicLib.logger.Info("OK:TerminalIf 预下单 成功");
                        string str = JsonTools.ObjectToJson(rsp);
                        PublicLib.logger.Info("返回:预下单:");
                        PublicLib.logger.Info(str);
                    }
                    return rsp;
                }

                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.PrepOrder: ex: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 3.	交易查询
        /// <summary>
        /// 交易查询
        /// </summary>
        /// <param name="queryorderreq">交易查询请求</param>
        /// <returns></returns>
        public static QueryOrderRsp QueryOrder(QueryOrderReq queryorderreq)
        {
            try
            {
                Init();
                //
                queryorderreq.sign = SHA1WithRSA.sign(JsonTools.ObjectToJson(queryorderreq.requestData), PublicLib.privatekey, "UTF-8");
                #region
                if (DebugFlag)
                {
                    string str = JsonTools.ObjectToJson(queryorderreq);
                    PublicLib.logger.Info("调用:交易查询:");
                    PublicLib.logger.Info(str);
                }

                QueryOrderRsp rsp = iTerminalIfHandler.QueryOrder(queryorderreq);
                //
                if (rsp == null)
                {
                    PublicLib.logger.Info("Err:TerminalIf 交易查询 失败 ");
                    return null;
                }
                else
                {
                    if (DebugFlag)
                    {
                        PublicLib.logger.Info("OK:TerminalIf 交易查询 成功");
                        string str = JsonTools.ObjectToJson(rsp);
                        PublicLib.logger.Info("返回:交易查询:");
                        PublicLib.logger.Info(str);
                    }
                    return rsp;
                }

                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.QueryOrder: ex: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 4.	出票状态更新
        /// <summary>
        /// 出票状态更新
        /// </summary>
        /// <param name="outticketreq">出票状态更新请求</param>
        /// <returns></returns>
        public static OutTicketRsp OutTicket(OutTicketReq outticketreq)
        {
            try
            {
                Init();
                //
                outticketreq.sign = SHA1WithRSA.sign(JsonTools.ObjectToJson(outticketreq.requestData), PublicLib.privatekey, "UTF-8");
                #region
                if (DebugFlag)
                {
                    string str = JsonTools.ObjectToJson(outticketreq);
                    PublicLib.logger.Info("调用:出票状态更新:");
                    PublicLib.logger.Info(str);
                }

                OutTicketRsp rsp = iTerminalIfHandler.OutTicket(outticketreq);
                //
                if (rsp == null)
                {
                    PublicLib.logger.Info("Err:TerminalIf 出票状态更新 失败 ");
                    return null;
                }
                else
                {
                    if (DebugFlag)
                    {
                        PublicLib.logger.Info("OK:TerminalIf 出票状态更新 成功");
                        string str = JsonTools.ObjectToJson(rsp);
                        PublicLib.logger.Info("返回:出票状态更新:");
                        PublicLib.logger.Info(str);
                    }
                    return rsp;
                }

                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.OutTicket: ex: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 5.	终端兑奖
        /// <summary>
        /// 终端兑奖
        /// </summary>
        /// <param name="cashprizereq">终端兑奖请求</param>
        /// <returns></returns>
        public static CashPrizeRsp CashPrize(CashPrizeReq cashprizereq)
        {
            try
            {
                Init();
                //
                cashprizereq.sign = SHA1WithRSA.sign(JsonTools.ObjectToJson(cashprizereq.requestData), PublicLib.privatekey, "UTF-8");
                #region
                if (DebugFlag)
                {
                    string str = JsonTools.ObjectToJson(cashprizereq);
                    PublicLib.logger.Info("调用:终端兑奖:");
                    PublicLib.logger.Info(str);
                }

                CashPrizeRsp rsp = iTerminalIfHandler.CashPrize(cashprizereq);
                //
                if (rsp == null)
                {
                    PublicLib.logger.Info("Err:TerminalIf 终端兑奖 失败 ");
                    return null;
                }
                else
                {
                    if (DebugFlag)
                    {
                        PublicLib.logger.Info("OK:TerminalIf 终端兑奖 成功");
                        string str = JsonTools.ObjectToJson(rsp);
                        PublicLib.logger.Info("返回:终端兑奖:");
                        PublicLib.logger.Info(str);
                    }
                    return rsp;
                }

                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.CashPrize: ex: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 6.	派奖
        /// <summary>
        /// 派奖
        /// </summary>
        /// <param name="awardorderreq">派奖请求</param>
        /// <returns></returns>
        public static AwardOrderRsp AwardOrder(AwardOrderReq awardorderreq)
        {
            try
            {
                Init();
                //
                awardorderreq.sign = SHA1WithRSA.sign(JsonTools.ObjectToJson(awardorderreq.requestData), PublicLib.privatekey, "UTF-8");
                #region
                if (DebugFlag)
                {
                    string str = JsonTools.ObjectToJson(awardorderreq);
                    PublicLib.logger.Info("调用:派奖:");
                    PublicLib.logger.Info(str);
                }

                AwardOrderRsp rsp = iTerminalIfHandler.AwardOrder(awardorderreq);
                //
                if (rsp == null)
                {
                    PublicLib.logger.Info("Err:TerminalIf 派奖 失败 ");
                    return null;
                }
                else
                {
                    if (DebugFlag)
                    {
                        PublicLib.logger.Info("OK:TerminalIf 派奖 成功");
                        string str = JsonTools.ObjectToJson(rsp);
                        PublicLib.logger.Info("返回:派奖:");
                        PublicLib.logger.Info(str);
                    }
                    return rsp;
                }

                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.AwardOrder: ex: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 7.	终端状态同步
        /// <summary>
        /// 终端状态同步
        /// </summary>
        /// <param name="terminalupdatereq">终端状态同步请求</param>
        /// <returns></returns>
        public static TerminalUpdateRsp TerminalUpdate(TerminalUpdateReq terminalupdatereq)
        {
            try
            {
                Init();
                //
                terminalupdatereq.sign = SHA1WithRSA.sign(JsonTools.ObjectToJson(terminalupdatereq.requestData), PublicLib.privatekey, "UTF-8");
                #region
                if (DebugFlag)
                {
                    string str = JsonTools.ObjectToJson(terminalupdatereq);
                    PublicLib.logger.Info("调用:终端状态同步:");
                    PublicLib.logger.Info(str);
                }

                TerminalUpdateRsp rsp = iTerminalIfHandler.TerminalUpdate(terminalupdatereq);
                //
                if (rsp == null)
                {
                    PublicLib.logger.Info("Err:TerminalIf 终端状态同步 失败 ");
                    return null;
                }
                else
                {
                    if (DebugFlag)
                    {
                        PublicLib.logger.Info("OK:TerminalIf 终端状态同步 成功");
                        string str = JsonTools.ObjectToJson(rsp);
                        PublicLib.logger.Info("返回:终端状态同步:");
                        PublicLib.logger.Info(str);
                    }
                    return rsp;
                }

                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.TerminalUpdate: ex: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 8.	广告查询
        /// <summary>
        /// 广告查询
        /// </summary>
        /// <param name="queryadsreq">广告查询请求</param>
        /// <returns></returns>
        public static QueryAdsRsp QueryAds(QueryAdsReq queryadsreq)
        {
            try
            {
                Init();
                //
                queryadsreq.sign = SHA1WithRSA.sign(JsonTools.ObjectToJson(queryadsreq.requestData), PublicLib.privatekey, "UTF-8");
                #region
                if (DebugFlag)
                {
                    string str = JsonTools.ObjectToJson(queryadsreq);
                    PublicLib.logger.Info("调用:广告查询:");
                    PublicLib.logger.Info(str);
                }

                QueryAdsRsp rsp = iTerminalIfHandler.QueryAds(queryadsreq);
                //
                if (rsp == null)
                {
                    PublicLib.logger.Info("Err:TerminalIf 广告查询 失败 ");
                    return null;
                }
                else
                {
                    if (DebugFlag)
                    {
                        PublicLib.logger.Info("OK:TerminalIf 广告查询 成功");
                        string str = JsonTools.ObjectToJson(rsp);
                        PublicLib.logger.Info("返回:广告查询:");
                        PublicLib.logger.Info(str);
                    }
                    return rsp;
                }

                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("TerminalIf.QueryAds: ex: " + ex.Message);
                return null;
            }
        }
        #endregion
    }
}

