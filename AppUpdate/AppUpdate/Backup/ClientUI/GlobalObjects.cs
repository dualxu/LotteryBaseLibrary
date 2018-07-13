using System;

namespace updater.ClientUI
{
	/// <summary>
	/// GlobalObjects 的摘要说明。
	/// </summary>
	public class GlobalObjects
	{
		static GlobalObjects()
		{
		}

		public static StepControl1 ctl1 = new StepControl1();
		public static StepControl2 ctl2 = new StepControl2();
		public static StepControl3 ctl3 = new StepControl3();
		public static StepControl4 ctl4 = new StepControl4();


		//data
		public static DataModels.VersionData ServerVerData = new updater.DataModels.VersionData();
		public static DataModels.FileListData ServerFileData = new updater.DataModels.FileListData();

		//constants
		public const string C_Download = "download";
		public const string C_Temp = "temp";

		//variants

		public static string MainExeName = ""; //like "d:\POS\POS.exe"

        public static bool IsSilent = false;

	}
}
