using System;

namespace updater.ClientUI
{
	/// <summary>
	/// Main 的摘要说明。
	/// </summary>
	public class MainEntry
	{
		private MainEntry()
		{
		}

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{


            System.Windows.Forms.Application.Run(new StepControl2());
		}


	}
}
