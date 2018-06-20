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
    /// 广告下载类
    /// </summary>
    public class AdsDownloader
    {
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
                outStream = System.IO.File.Create(savePath);
                inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0) outStream.Write(buffer, 0, l);
                } while (l > 0);
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
        /// <param name="SavePath">默认当前目录adsDownloadDir</param>
        /// <returns></returns>
        public static bool AdsDownload(List<QueryAdsLotteryDtosItem> adsList, string SavePath)
        {
            try
            {
                if (adsList != null)
                {
                    if (adsList.Count > 0)
                    {
                        foreach (QueryAdsLotteryDtosItem ads in adsList)
                        {
                            AdsDownloader.DownloadPicture(ads.filePath, SavePath, 60000);
                        }
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

                XmlNode t_node = t_xml.SelectSingleNode("root/adsconfig");
                root.RemoveChild(t_node);
                t_xml.Save(t_s_xmlpath);
                //
                XmlElement adsconfig = t_xml.CreateElement("adsconfig");
                root.AppendChild(adsconfig);
                //
                t_node = t_xml.SelectSingleNode("root/adsconfig");
                XmlElement count = t_xml.CreateElement("ads");
                count.SetAttribute("count", adslist.Count.ToString());
                t_node.AppendChild(count);
                //
                int number = 1;
                foreach (QueryAdsLotteryDtosItem ads in adslist)
                {
                    XmlElement member = t_xml.CreateElement("ads" + number.ToString("D3"));
                    member.SetAttribute("adsId", ads.adsId);
                    member.SetAttribute("adsKind", ads.adsKind);
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
    }
}
