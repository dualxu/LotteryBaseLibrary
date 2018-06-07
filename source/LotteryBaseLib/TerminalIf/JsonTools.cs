using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.Management;
using System.Net;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

using LotteryBaseLib.Public;

namespace LotteryBaseLib.TerminalIf
{
    #region JsonTools
    /// <summary>
    /// JsonTools 对象和Json字符串互转,MD5加密等工具类
    /// </summary>
    public class JsonTools
    {
        /// <summary>
        /// 从一个对象信息生成Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                serializer.WriteObject(stream, obj);
                byte[] dataBytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(dataBytes, 0, (int)stream.Length);
                return Encoding.UTF8.GetString(dataBytes);
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("JsonTools.ObjectToJson(): " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 从一个Json串生成对象信息
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object JsonToObject(string jsonString, object obj)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                return serializer.ReadObject(mStream);
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("JsonTools.JSONToObject():" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 读取json数组中特定的参数值
        /// </summary>
        /// <param name="JsonStr">JSON字符串</param>
        /// <param name="KeyStr">键值</param>
        /// <returns></returns>
        public static string GetDedicatedKeyFromJson(string JsonStr,string KeyStr)
        {
            string RetStr = "";

            try
            {
                JObject jobj = JObject.Parse(JsonStr);
                if (jobj.Count > 0)
                {
                    RetStr = jobj[KeyStr].ToString();
                    //
                    RetStr = RetStr.Replace("\n", "");
                    RetStr = RetStr.Replace("\r", "");
                    RetStr = RetStr.Replace(" ", "");
                }
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("JSonTools.GetDedicatedKeyFromJson:" + JsonStr+","+KeyStr+","+ ex.Message);
                RetStr = "";
            }
            return RetStr;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="StrTicket"></param>
        /// <returns></returns>
        public static string GetMD5(string StrTicket)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(StrTicket));

                string byte2String = null;

                for (int i = 0; i < result.Length; i++)
                {
                    byte2String += result[i].ToString("x2");
                }

                return byte2String;
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("JsonTools.GetMD5():" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 两次MD5加密
        /// </summary>
        /// <param name="StrTicket"></param>
        /// <returns></returns>
        public static string GetMD5Twice(string StrTicket)
        {
            try
            {
                string StrMD5 = "";

                StrMD5 = GetMD5(StrTicket);

                if (StrMD5 == null)
                {
                    PublicLib.logger.Error("JsonTools.GetMD5Twice():Error in first hash.");
                    return null;
                }

                StrMD5 = GetMD5(StrMD5);

                if (StrMD5 == null)
                {
                    PublicLib.logger.Error("JsonTools.GetMD5Twice():Error in second hash.");
                    return null;
                }

                return StrMD5;
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("JsonTools.GetMD5Twice():" + ex.Message);
                return null;
            }
        }
    }
    #endregion
}
