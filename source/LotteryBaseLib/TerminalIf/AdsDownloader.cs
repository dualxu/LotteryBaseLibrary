using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;

using LotteryBaseLib.Public;

namespace LotteryBaseLib.TerminalIf
{
    /// <summary>
    /// 本地广告数据
    /// </summary>
    public class QueryAdsLotteryDtosItemLocal
    {
        /// <summary>
        /// 本地广告保存的序列号
        /// </summary>
        public string adsSerial { get; set; }
        /// <summary>
        /// 广告ID
        /// </summary>
        public string adsId { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string adsName { get; set; }
        /// <summary>
        /// 广告类别，1上屏；2中屏；3下屏；4屏保
        /// </summary>
        public string adsKind { get; set; }
        /// <summary>
        /// 广告类型，1图片，2视频
        /// </summary>
        public string adsType { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string filePath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string fileSize { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string beginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 播放时间，单位：秒
        /// </summary>
        public string playTime { get; set; }
        /// <summary>
        /// 播放顺序
        /// </summary>
        public string playSeq { get; set; }
        /// <summary>
        /// 下载模式
        /// 1 远程下载
        /// 2 U盘更新
        /// </summary>
        public string downloadMode { get; set; }
    }
    
    
    
    /// <summary>
    /// 广告下载类
    /// </summary>
    public class AdsDownloader
    {
        private static int localCount = 0;
        
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="picUrl">图片Http地址</param>
        /// <param name="savePath">图片保存路径</param>
        /// <param name="timeOut">Request最大请求时间，如果为-1则无限制</param>
        /// <returns></returns>
        private static bool DownloadPicture(string picUrl, string savePath, int timeOut)
        {
            bool value = false;
            WebResponse response = null;
            Stream stream = null;
            string imgName = savePath + "\\" + picUrl.ToString().Substring(picUrl.ToString().LastIndexOf("/") + 1);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(picUrl);
                if (timeOut != -1) request.Timeout = timeOut;
                request.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1; Natas.Robot)";                
                response = request.GetResponse();
                stream = response.GetResponseStream();
                if (!response.ContentType.ToLower().StartsWith("text/"))
                    value = SaveBinaryFile(response, imgName);
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("AdsDownloader->DownloadPicture:" + ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }
            return value;
        }

        private static bool SaveBinaryFile(WebResponse response, string savePath)
        {
            bool value = false;
            byte[] buffer = new byte[1024];
            Stream outStream = null;
            Stream inStream = null;
            try
            {
                if (File.Exists(savePath)) File.Delete(savePath);
                DateTime dt1 = DateTime.Now;
                outStream = System.IO.File.Create(savePath);
                inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0) outStream.Write(buffer, 0, l);
                } while (l > 0);
                TimeSpan ts = DateTime.Now - dt1;
                PublicLib.logger.Info("AdsDownloader->SaveBinaryFile:" + savePath + ",download elasped(ms):" + ts.TotalMilliseconds);
                value = true;
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("AdsDownloader->SaveBinaryFile:" + ex.Message);
            }
            finally
            {
                if (outStream != null) outStream.Close();
                if (inStream != null) inStream.Close();
            }
            return value;
        }

        /// <summary>
        /// 广告图片下载并更新配置文件
        /// </summary>
        /// <param name="adsList">广告列表</param>
        /// <param name="SavePath">默认当前目录下adsDownloadDir目录</param>
        /// <returns></returns>
        public static bool AdsDownload(List<QueryAdsLotteryDtosItem> adsList, string SavePath)
        {
            try
            {
                if (adsList != null)
                {
                    if (adsList.Count > 0)
                    {
                        //获取本地配置
                        List<QueryAdsLotteryDtosItemLocal> adsListLocal = new List<QueryAdsLotteryDtosItemLocal>();
                        LoadAdsDownloadConfig(out adsListLocal);

                        //更新下载列表
                        if ((adsListLocal != null) && (adsListLocal.Count > 0))
                        {
                            //foreach (QueryAdsLotteryDtosItemLocal adslocal in adsListLocal)
                            for(int i=0;i<adsListLocal.Count;i++)
                            {
                                //foreach (QueryAdsLotteryDtosItem ads in adsList)
                                for(int j=0;j<adsList.Count;j++)
                                {
                                    string imgName = adsList[j].filePath.ToString().Substring(adsList[j].filePath.ToString().LastIndexOf("/") + 1);
                                    if (adsListLocal[i].filePath == imgName)
                                    {
                                        if ((adsListLocal[i].beginDate == adsList[j].beginDate) && (adsListLocal[i].endDate == adsList[j].endDate))
                                        {
                                            //adsList.Remove(ads);
                                            adsList.RemoveAt(j);
                                        }
                                        else
                                        {
                                            UpdateAdsDownloadConfigSingleNode(adsListLocal[i].adsSerial, adsList[j].beginDate, adsList[j].endDate);
                                            //adsList.Remove(ads);
                                            adsList.RemoveAt(j);
                                        }
                                    }
                                }
                            }
                        }
                        
                        //下载
                        foreach (QueryAdsLotteryDtosItem ads in adsList)
                        {
                            AdsDownloader.DownloadPicture(ads.filePath, SavePath, 60000);
                        }
                        //更新本地配置
                        AdsDownloader.UpdateAdsDownloadConfig(adsList);
                    }
                }

            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("AdsDownloader->AdsDownload:" + ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新广告查询结果XML配置文件
        /// </summary>
        /// <param name="adslist"></param>
        /// <returns></returns>
        private static bool UpdateAdsDownloadConfig(List<QueryAdsLotteryDtosItem> adslist)
        {
            try
            {
                string t_s_xmlpath = ".\\lotterybaselib_config.xml";
                if (File.Exists(t_s_xmlpath) == false)
                {
                    PublicLib.logger.Error("AdsDownloader->UpdateAdsDownloadConfig, Err:配置文件找不到," + t_s_xmlpath);
                    return false;
                }
                XmlDocument t_xml = new XmlDocument();
                t_xml.Load(t_s_xmlpath);

                var root = t_xml.DocumentElement;
                //
                XmlNode t_node = t_xml.SelectSingleNode("root/adsconfig/ads");
                XmlElement xe = (XmlElement)t_node;
                xe.SetAttribute("count", (localCount + adslist.Count).ToString());
                t_xml.Save(t_s_xmlpath);
                //
                t_node = t_xml.SelectSingleNode("root/adsconfig");
                int number = 1 + localCount;
                foreach (QueryAdsLotteryDtosItem ads in adslist)
                {
                    XmlElement member = t_xml.CreateElement("ads" + number.ToString("D3"));
                    member.SetAttribute("adsId", ads.adsId);
                    member.SetAttribute("adsKind", ads.adsKind);
                    member.SetAttribute("adsType", ads.adsType);
                    member.SetAttribute("adsName", ads.adsName);
                    member.SetAttribute("beginDate", ads.beginDate);
                    member.SetAttribute("downloadMode", ads.downloadMode);
                    member.SetAttribute("endDate", ads.endDate);
                    string imgName = ads.filePath.ToString().Substring(ads.filePath.ToString().LastIndexOf("/") + 1);
                    member.SetAttribute("filePath", imgName);
                    member.SetAttribute("fileSize", ads.fileSize);
                    member.SetAttribute("playSeq", ads.playSeq);
                    member.SetAttribute("playTime", ads.playTime);
                    t_node.AppendChild(member);
                    number++;
                }

                t_xml.Save(t_s_xmlpath);
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("AdsDownloader->UpdateAdsDownloadConfig:" + ex.Message);
                return false;
            }
            finally
            {

            }
            return true;
        }

        /// <summary>
        /// 更新广告查询结果XML配置文件单一节点
        /// </summary>
        /// <param name="strNode">节点名称</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        private static bool UpdateAdsDownloadConfigSingleNode(string strNode, string beginDate, string endDate)
        {
            try
            {
                string t_s_xmlpath = ".\\lotterybaselib_config.xml";
                if (File.Exists(t_s_xmlpath) == false)
                {
                    PublicLib.logger.Error("AdsDownloader->UpdateAdsDownloadConfigSingleNode, Err:配置文件找不到," + t_s_xmlpath);
                    return false;
                }
                XmlDocument t_xml = new XmlDocument();
                t_xml.Load(t_s_xmlpath);

                var root = t_xml.DocumentElement;

                XmlNode t_node = t_xml.SelectSingleNode("root/adsconfig/" + strNode);
                XmlElement xe = (XmlElement)t_node;
                xe.SetAttribute("beginDate", beginDate);
                xe.SetAttribute("endDate", endDate);
                //
                t_xml.Save(t_s_xmlpath);
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("AdsDownloader->UpdateAdsDownloadConfigSingleNode:" + ex.Message);
                return false;
            }
            finally
            {

            }
            return true;
        }

        /// <summary>
        /// 载入广告查询结果XML配置文件
        /// </summary>
        /// <param name="adslist"></param>
        /// <returns></returns>
        private static bool LoadAdsDownloadConfig(out List<QueryAdsLotteryDtosItemLocal> adslist)
        {
            try
            {
                List<QueryAdsLotteryDtosItemLocal> items = new List<QueryAdsLotteryDtosItemLocal>();
                QueryAdsLotteryDtosItemLocal item = new QueryAdsLotteryDtosItemLocal();
                
                string t_s_xmlpath = ".\\lotterybaselib_config.xml";
                if (File.Exists(t_s_xmlpath) == false)
                {
                    PublicLib.logger.Error("AdsDownloader->LoadAdsDownloadConfig, Err:配置文件找不到," + t_s_xmlpath);
                    adslist = items;
                    return false;
                }

                string adsSerial = "";
                string adsId = "";
                string adsKind = "";
                string adsType = "";
                string adsName = "";
                string beginDate = "";
                string downloadMode = "";
                string endDate = "";
                string filePath = "";
                string fileSize = "";
                string playSeq = "";
                string playTime = "";

                XmlDocument t_xml = new XmlDocument();
                t_xml.Load(t_s_xmlpath);

                var root = t_xml.DocumentElement;
                //
                XmlNode t_node = t_xml.SelectSingleNode("root/adsconfig/ads");
                int count = 0;
                count = int.Parse(t_node.Attributes["count"].Value.ToString());
                localCount = count;

                for (int i = 1; i <= count; i++)
                {
                    item = new QueryAdsLotteryDtosItemLocal();
                    
                    t_node = t_xml.SelectSingleNode("root/adsconfig/ads" + i.ToString("D3"));

                    adsSerial = "ads" + i.ToString("D3");
                    adsId = t_node.Attributes["adsId"].Value.ToString();
                    adsKind = t_node.Attributes["adsKind"].Value.ToString();
                    adsType = t_node.Attributes["adsType"].Value.ToString();
                    adsName = t_node.Attributes["adsName"].Value.ToString();
                    beginDate = t_node.Attributes["beginDate"].Value.ToString();
                    downloadMode = t_node.Attributes["downloadMode"].Value.ToString();
                    endDate = t_node.Attributes["endDate"].Value.ToString();
                    filePath = t_node.Attributes["filePath"].Value.ToString();
                    fileSize = t_node.Attributes["fileSize"].Value.ToString();
                    playSeq = t_node.Attributes["playSeq"].Value.ToString();
                    playTime = t_node.Attributes["playTime"].Value.ToString();

                    item.adsSerial = adsSerial;
                    item.adsId = adsId;
                    item.adsKind = adsKind;
                    item.adsName = adsName;
                    item.adsType = adsType;
                    item.beginDate = beginDate;
                    item.downloadMode = downloadMode;
                    item.endDate = endDate;
                    item.filePath = filePath;
                    item.fileSize = fileSize;
                    item.playSeq = playSeq;
                    item.playTime = playTime;

                    items.Add(item);
                }
                adslist = items;
                t_xml.Save(t_s_xmlpath);
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("AdsDownloader->LoadAdsDownloadConfig:" + ex.Message);
                List<QueryAdsLotteryDtosItemLocal> items = new List<QueryAdsLotteryDtosItemLocal>();
                adslist = items;
                return false;
            }
            finally
            {

            }
            return true;
        }
    }
}
