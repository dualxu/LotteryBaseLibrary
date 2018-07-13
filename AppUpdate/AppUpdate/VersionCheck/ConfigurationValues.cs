using System;
using System.Configuration;

namespace updater.VersionCheck
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class ConfigurationValues
	{
		static ConfigurationValues()
		{
			LoadConfigValue();
		}
		private static string _datafilename = string.Empty;
		private static string _listfilename = string.Empty;
		private static string _UpdateServerPath = string.Empty;
	
		public static void LoadConfigValue()
		{
			_datafilename = ConfigurationSettings.AppSettings[ConfigurationValues.C_DataFileName].ToString();
			_listfilename= ConfigurationSettings.AppSettings[ConfigurationValues.C_ListFileName].ToString();
			_UpdateServerPath = ConfigurationSettings.AppSettings[ConfigurationValues.C_UpdateServerPath].ToString();
		}

		public static bool SetServerPath(string xmlFilePath,string urlPath)
		{
			if(XmlConfigWriter.WriteUpdateConfig(
				xmlFilePath,ConfigurationValues.C_UpdateServerPath,urlPath) )
			{
				_UpdateServerPath = urlPath;
				return true;
			}
			return false;
		}
		
		#region public property
		
		public static string DataFileName
		{
			get { return _datafilename;}
		}
		public static string ListFileName
		{
			get { return _listfilename;}
		}
		public static string UpdateServerPath
		{
			get{ return _UpdateServerPath;}
		}
		#endregion

		#region constants
		public const string C_DataFileName = "DataFileName";
		public const string C_ListFileName = "ListFileName";
		public const string C_UpdateServerPath = "UpdateServerPath";
		#endregion

		//public const string LocalConfigDirectory = "Config";

	}
}
