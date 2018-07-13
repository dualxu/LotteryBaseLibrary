using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Tools.WizardLibrary;
using updater.DataModels;
using System.Diagnostics;
using System.IO;
using updater.VersionCheck;

namespace updater.ClientUI
{
	/// <summary>
	/// StepControl4 的摘要说明。
	/// </summary>
	public class StepControl4 : Tools.WizardLibrary.StepControlBase
	{
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.RichTextBox lblmsg;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StepControl4()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			this.ResetButtonEnable();
			this.IsNextEnable = false;

			this.HeaderText = "第四步：复制安装新版本";
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.lblmsg = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(40, 304);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(208, 24);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Tag = "";
			this.checkBox1.Text = "清除已下载文件";
			this.checkBox1.Visible = false;
			// 
			// checkBox2
			// 
			this.checkBox2.Checked = true;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox2.Location = new System.Drawing.Point(40, 344);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(208, 24);
			this.checkBox2.TabIndex = 2;
			this.checkBox2.Text = "立即启动程序";
			this.checkBox2.Visible = false;
			// 
			// lblmsg
			// 
			this.lblmsg.BackColor = System.Drawing.SystemColors.Control;
			this.lblmsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblmsg.Location = new System.Drawing.Point(40, 32);
			this.lblmsg.Name = "lblmsg";
			this.lblmsg.ReadOnly = true;
			this.lblmsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.lblmsg.Size = new System.Drawing.Size(552, 256);
			this.lblmsg.TabIndex = 4;
			this.lblmsg.Text = "";
			// 
			// StepControl4
			// 
			this.Controls.Add(this.lblmsg);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Name = "StepControl4";
			this.Size = new System.Drawing.Size(656, 406);
			this.Load += new System.EventHandler(this.StepControl4_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void StepControl4_Load(object sender, System.EventArgs e)
		{
			this.ctlnext = null;
			this.ctlprev = GlobalObjects.ctl3;
			
			// ////////////////////////////////////////////

			checkBox1.Visible =false;
			checkBox2.Visible = false;

			lblmsg.Text = "";
			AddMsg( "正在检查程序是否关闭...");

			string[] zipedfiles = new string[GlobalObjects.ServerFileData.Tables[FileListData.mTableName].Rows.Count];
			for(int i=0;i<GlobalObjects.ServerFileData.Tables[FileListData.mTableName].Rows.Count;i++)
			{
				DataRow row = GlobalObjects.ServerFileData.Tables[FileListData.mTableName].Rows[i];
				zipedfiles[i] = row[FileListData.mFileName].ToString();
			}
			string[] zipeddirs = new string[GlobalObjects.ServerFileData.Tables[FileListData.mTableName].Rows.Count];

			string[] filenames = new string[GlobalObjects.ServerVerData.Tables[VersionData.mTableName].Rows.Count];
			int index=0;
			foreach(DataRow row in GlobalObjects.ServerVerData.Tables[VersionData.mTableName].Rows)
			{
				string exename = row[VersionData.mExeName].ToString();				
				filenames[index] = exename;
				index++;

				exename = Path.GetFileNameWithoutExtension(exename);
				CloseProcessByName(exename);
			}

			AddMsg("\n开始解压文件，请稍等...");
			
			string zipedpath = Path.Combine(Application.StartupPath,GlobalObjects.C_Download);

			for(int i=0;i<zipedfiles.Length;i++)
			{
				string zipedfile = Path.Combine(zipedpath,zipedfiles[i]);

				if(File.Exists(zipedfile))
				{
					zipeddirs[i] = Path.Combine(zipedpath, Path.GetFileNameWithoutExtension(zipedfiles[i]) );
					bool bret = Tools.ZipLibrary.ZipHelper.UnZipFile(zipedfile,zipeddirs[i]);
					if(bret)
					{
						AddMsg("\n解压文件" + filenames[i] + "完成.");
					}
				}
			}
			AddMsg("\n开始复制安装文件...");

			DirectoryInfo di = new DirectoryInfo(Application.StartupPath);

			for(int i=0;i<zipeddirs.Length;i++)
			{
				//copy file and directory from unziped directory to program directory
				Tools.Utilities.FileOperation.CopyDirectory(zipeddirs[i],di.Parent.FullName,true);
			}

			//check main exe valid
			isMainValid = false;
			if( (GlobalObjects.MainExeName.Length >0) 
				&& File.Exists(GlobalObjects.MainExeName) )
			{
				isMainValid = true;
			}

			AddMsg("\n升级完成。");

			Helper.ClearDirectory( Path.Combine(Application.StartupPath,GlobalObjects.C_Temp) );
			
			checkBox1.Visible = true;
			checkBox2.Visible = isMainValid;
			checkBox2.Text = "立即启动程序" + Path.GetFileName(GlobalObjects.MainExeName);

			this.IsPrevEnable = false;
			this.IsCancelEnable = false;
			this.IsNextEnable = false;
			WizardObjects.MainCtler.SetButtonStates();
		}

		bool bshowmsg = true;
		private void CloseProcessByName(string appname)
		{				
			Process[] pros = Process.GetProcessesByName(appname);
			if(pros==null) return;
			int msgcnt=0;
			for(int i=0;i<pros.Length;i++)
			{				
				while( !pros[i].HasExited )
				{
					if(bshowmsg)
					{
						bshowmsg = false;
						MessageBox.Show(this,"升级程序发现程序" + appname + "等还没有关闭。\n请关闭，并按“确定”继续。" 
							+ "\n\n否则，升级程序将强制关闭所有" + appname + "程序，您未保存的数据可能丢失。",
							"提醒",MessageBoxButtons.OK,MessageBoxIcon.Warning);						
					}

					if(msgcnt==0)
						AddMsg("\n    检测到程序" + appname + "还在运行");
					pros[i].CloseMainWindow();
					System.Threading.Thread.Sleep(200);

					if( (pros[i]!=null) && (!pros[i].HasExited) )
					{
						pros[i].Kill();
						System.Threading.Thread.Sleep(200);
						if(msgcnt==0)
							AddMsg("\n    程序" + appname + "已被强制关闭.");
					}
					else
					{
						if(msgcnt==0)
							AddMsg("\n    程序" + appname + "已被关闭.");
					}
					msgcnt++;
				}
			}
		}

		private void AddMsg(string text)
		{
			lblmsg.Text += text;
			Application.DoEvents();
		}


		public override bool GoFinish()
		{
			if(checkBox1.Checked)
			{
				Helper.ClearDirectory( Path.Combine(Application.StartupPath,GlobalObjects.C_Download) );

				Tools.Utilities.FileOperationAdvance.DestroyFile( Path.Combine(Application.StartupPath,ConfigurationValues.DataFileName) );
				Tools.Utilities.FileOperationAdvance.DestroyFile( Path.Combine(Application.StartupPath,ConfigurationValues.ListFileName) );
			}

			if(isMainValid)
			{
				Process.Start(GlobalObjects.MainExeName);
			}
			
			WizardObjects.MainCtler.Form.Close();

			return base.GoFinish ();
		}

		private bool isMainValid = false;

	}
}
