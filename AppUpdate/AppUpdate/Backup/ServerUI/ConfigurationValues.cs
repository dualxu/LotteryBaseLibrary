using System;
using System.Configuration;
using System.Collections;

namespace updater.ServerUI
{
	/// <summary>
	/// ConfigValue 的摘要说明。
	/// </summary>
	public sealed class ConfigurationValues
	{
		private static string _downdir = string.Empty;
		private static string _datafilename = string.Empty;
		private static string _listfilename = string.Empty;
		private static string _sqlfilename = string.Empty;
		private static string _exefiles = string.Empty;
		private static string[] _arrexefiles = null;

		private static string _mainpath = string.Empty;

		static ConfigurationValues()
		{
			LoadConfigValue();
		}
		public static void LoadConfigValue()
		{
			_downdir = ConfigurationSettings.AppSettings[ConfigurationValues.C_DownDir].ToString();
			_datafilename = ConfigurationSettings.AppSettings[ConfigurationValues.C_DataFileName].ToString();
			_listfilename= ConfigurationSettings.AppSettings[ConfigurationValues.C_ListFileName].ToString();
			_sqlfilename = ConfigurationSettings.AppSettings[ConfigurationValues.C_SQLFileName].ToString();

			_exefiles = ConfigurationSettings.AppSettings[ConfigurationValues.C_VerFiles].ToString();
			if(_exefiles.Length>0)
			{
				_arrexefiles = _exefiles.Split(';');
			}
		}

		#region public property
		public static string DownDir
		{
			get { return _downdir;}
		}
		public static string DataFileName
		{
			get { return _datafilename;}
		}
		public static string ListFileName
		{
			get { return _listfilename;}
		}
		public static string SQLFileName
		{
			get { return _sqlfilename;}
		}
		public static string ExeFiles
		{
			get { return _exefiles;}
		}
		public static string[] ArrayExeFiles
		{
			get { return _arrexefiles;}
		}
		#endregion

		#region constants
		public const string C_DownDir = "DownDir";
		public const string C_DataFileName = "DataFileName";
		public const string C_ListFileName = "ListFileName";
		public const string C_SQLFileName = "SQLFileName";
		public const string C_VerFiles = "VerFiles";
		#endregion

	}


}
