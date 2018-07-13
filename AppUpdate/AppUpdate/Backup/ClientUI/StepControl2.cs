using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Tools.WizardLibrary;
using Tools.DownloadLibrary;
using updater.DataModels;
using updater.VersionCheck;
using System.Diagnostics;

namespace updater.ClientUI
{
	/// <summary>
	/// StepControl2 的摘要说明。
	/// </summary>
	public class StepControl2 :Form
    {
		private System.Windows.Forms.RichTextBox richTextBox1;
        private Button button1;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StepControl2()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();
 
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(56, 44);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(515, 224);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(553, 330);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "开始升级";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StepControl2
            // 
            this.ClientSize = new System.Drawing.Size(535, 229);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "StepControl2";
            this.Load += new System.EventHandler(this.StepControl2_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void StepControl2_Load(object sender, System.EventArgs e)
		{
		 
		
			///////////////////////////////////			

		//	WizardObjects.MainCtler.SetHearderText("第二步：比较版本信息");

			/*
			this.btnRedo.Left = WizardObjects.MainCtler.Form.ButtonCancel.Left - btnRedo.Width - 16;
			this.btnRedo.Top = WizardObjects.MainCtler.Form.ButtonCancel.Top;
			this.btnRedo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

			this.btnRedo.Height = WizardObjects.MainCtler.Form.ButtonCancel.Height;

			this.Controls.Remove(btnRedo);
			WizardObjects.MainCtler.Form.FootPanel.Controls.Add(btnRedo);
			*/

		 
			btnRedo_Click(sender,e);
		 

		}

		private bool hasnewver = true;
		private void btnRedo_Click(object sender, System.EventArgs e)
		{ 
			WizardObjects.MainCtler.SetButtonStates();

			 
			richTextBox1.Text = "";
			richTextBox1.Clear();

			richTextBox1.Text = "";
			
			richTextBox1.Text = "正在下载配置文件...";
			Application.DoEvents();

			string dstdir = Application.StartupPath;
			string url1 = Path.Combine(ConfigurationValues.UpdateServerPath, ConfigurationValues.DataFileName);
			string url2 = Path.Combine(ConfigurationValues.UpdateServerPath, ConfigurationValues.ListFileName);
            
			string l1 = Path.Combine(dstdir, ConfigurationValues.DataFileName);
			string l2 = Path.Combine(dstdir, ConfigurationValues.ListFileName);

			if(File.Exists(l1)) File.Delete(l1);
			if(File.Exists(l2)) File.Delete(l2);

			bool bret = BatchDownload(new string[] {url1,url2}, dstdir);

			if(!bret)
			{
				richTextBox1.Text += "\n下载配置文件失败，请检查网络是否正常、服务器路径设置是否正确。";
                richTextBox1.Text += "\n当前服务器路径：" + ConfigurationValues.UpdateServerPath;
		 
				WizardObjects.MainCtler.SetButtonStates();
				return;
			}
			else
			{
				richTextBox1.Text += "\n下载配置文件成功.正在比较版本...";
			}
			Application.DoEvents();

			if(File.Exists(Path.Combine(dstdir,ConfigurationValues.DataFileName) ) )
			{
				GlobalObjects.ServerVerData.Clear();
				GlobalObjects.ServerVerData.ReadXml( Path.Combine(dstdir,ConfigurationValues.DataFileName), XmlReadMode.ReadSchema);
			}			
			VersionData serVersion = GlobalObjects.ServerVerData;

			if(File.Exists(Path.Combine(dstdir,ConfigurationValues.ListFileName)) )
			{
				GlobalObjects.ServerFileData.Clear();
				GlobalObjects.ServerFileData.ReadXml( Path.Combine(dstdir,ConfigurationValues.ListFileName), XmlReadMode.ReadSchema);
			}
			FileListData filelist = GlobalObjects.ServerFileData;			

			if( (serVersion == null) || (filelist == null) )
			{
				richTextBox1.Text += "\n读配置文件出错，请重试。";
				Application.DoEvents();
			 
				WizardObjects.MainCtler.SetButtonStates();
				return;
			}

			string outputversion = "";
			foreach(DataRow row in serVersion.Tables[VersionData.mTableName].Rows)
			{
				string exename = row[VersionData.mExeName].ToString();

				DirectoryInfo curdi = new DirectoryInfo(Application.StartupPath);
				DirectoryInfo parentdi = curdi.Parent;

				string exefullname = Path.Combine(parentdi.FullName, exename);
				string dircopyto = Path.Combine(curdi.FullName,GlobalObjects.C_Temp);
				string tmpexefullname = Path.Combine(dircopyto, exename + Guid.NewGuid().ToString());
				
				if(!Directory.Exists(dircopyto))
				{
					Directory.CreateDirectory(dircopyto);
				}
				
				outputversion += "文件名：" + exename + "\n";
				outputversion += "服务器版本：" + row[VersionData.mFullVersion].ToString() + "\n";

                Application.DoEvents();

                try
                {
                    if (File.Exists(exefullname))
                    {
                        if (File.Exists(tmpexefullname))
                        {
                            File.Delete(tmpexefullname);
                        }

                        File.Copy(exefullname, tmpexefullname, true);

                        //Assembly assm = Assembly.LoadFrom(tmpexefullname);
                        //if (assm == null) continue;

                        //string cliver = assm.GetName().Version.ToString();
                        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(tmpexefullname);
                        string cliver = myFileVersionInfo.FileVersion;
 
                        //assm = null;

                        outputversion += "客户端版本：" + cliver + "\n";
                        outputversion += "\n";

                        this.hasnewver = false;
                        if (Helper.IsNewVersion(new Version(cliver),
                            new Version(row[VersionData.mFullVersion].ToString())))
                        {
                            hasnewver = true;
                        }
                    }
                    else
                    {
                        outputversion += "客户端版本：无\n";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("升级出错：" + ex.Message, "提示");
                }
			}

			if(hasnewver)
			{
				richTextBox1.Text += "\n\n程序发现新版本.";
			}
			else
			{
				richTextBox1.Text += "\n\n程序没有发现新版本.";
			}

			richTextBox1.Text +=  "\n\n" + outputversion;
			
			Application.DoEvents();
	

			if(hasnewver)
			{
			 
				WizardObjects.MainCtler.SetButtonStates();
			}
			else
			{
			 
			 
				WizardObjects.MainCtler.SetButtonStates();
			}

		}

		private bool SingleDownload(string url, string dst)
		{
			try
			{
				using(HttpFileDownloader dl = new HttpFileDownloader())
				{
					dl.Download(url, dst, null);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool BatchDownload(string[] urls, string dstdir)
		{
			try
			{
				using(HttpBatchDownloader dl = new HttpBatchDownloader())
				{
					dl.Download(urls, dstdir, null);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}
        private void button1_Click(object sender, EventArgs e)
        {
            btnRedo_Click(null, null);
        }


	}
}
