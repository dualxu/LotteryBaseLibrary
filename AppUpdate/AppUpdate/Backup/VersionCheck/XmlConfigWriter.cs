using System;
using System.Xml;
using System.IO;

namespace updater.VersionCheck
{
	/// <summary>
	/// XmlConfigWriter 的摘要说明。
	/// </summary>
	public class XmlConfigWriter
	{
		static XmlConfigWriter()
		{
		}
		public static bool WriteUpdateConfig(string xmlFilePath,string strKey,string strValue)
		{
			bool bresult = false;
			XmlDocument _doc = new XmlDocument();
			if( File.Exists(xmlFilePath) )
			{
				_doc.Load(xmlFilePath);

				foreach(XmlElement el in _doc.DocumentElement["appSettings"].ChildNodes)
				{
					if(el.Attributes.GetNamedItem("key").InnerText.Trim()==strKey.Trim() )
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

	}
}
