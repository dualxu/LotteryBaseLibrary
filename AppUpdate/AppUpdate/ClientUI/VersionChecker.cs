using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace updater.ClientUI
{
    public class VersionChecker
    {
        public static bool WriteServerPath(string fileFullName, string urlPath)
        {
            return WriteUpdateConfig(fileFullName, "UpdateServerPath", urlPath);
        }
        public static string GetServerPath(string fileFullName)
        {
            return GetUpdateConfig(fileFullName, "UpdateServerPath");
        }



        private static bool WriteUpdateConfig(string xmlFilePath, string strKey, string strValue)
        {
            bool bresult = false;
            XmlDocument _doc = new XmlDocument();
            if (File.Exists(xmlFilePath))
            {
                _doc.Load(xmlFilePath);

                foreach (XmlElement el in _doc.DocumentElement["appSettings"].ChildNodes)
                {
                    if (el.Attributes.GetNamedItem("key").InnerText.Trim() == strKey.Trim())
                    {
                        el.Attributes.GetNamedItem("value").InnerText = strValue;
                        bresult = true;
                        break;
                    }
                }

                FileInfo fi = new FileInfo(xmlFilePath);
                fi.Attributes = FileAttributes.Normal;
                _doc.Save(xmlFilePath);
            }
            return bresult;
        }

        private static string GetUpdateConfig(string xmlFilePath, string strKey)
        {
            string rst = string.Empty;
            XmlDocument _doc = new XmlDocument();
            if (File.Exists(xmlFilePath))
            {
                _doc.Load(xmlFilePath);

                foreach (XmlElement el in _doc.DocumentElement["appSettings"].ChildNodes)
                {
                    if (el.Attributes.GetNamedItem("key").InnerText.Trim() == strKey.Trim())
                    {
                        rst = el.Attributes.GetNamedItem("value").InnerText.ToString();
                        break;
                    }
                }
            }
            return rst;
        }

    }
}
