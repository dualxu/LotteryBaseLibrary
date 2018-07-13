using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Tools.WizardLibrary;
using updater.DataModels;

namespace updater.ServerUI
{
	/// <summary>
	/// StepControl3 的摘要说明。
	/// </summary>
	public class StepControl3 : StepControlBase
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblmsg;
		private System.Windows.Forms.Button btnRedo;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StepControl3()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			this.ResetButtonEnable();
			this.IsFinishEnable = false;
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
			this.label1 = new System.Windows.Forms.Label();
			this.lblmsg = new System.Windows.Forms.Label();
			this.btnRedo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(96, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(424, 32);
			this.label1.TabIndex = 1;
			this.label1.Text = "刷新版本信息";
			// 
			// lblmsg
			// 
			this.lblmsg.Location = new System.Drawing.Point(96, 104);
			this.lblmsg.Name = "lblmsg";
			this.lblmsg.Size = new System.Drawing.Size(440, 192);
			this.lblmsg.TabIndex = 2;
			// 
			// btnRedo
			// 
			this.btnRedo.Location = new System.Drawing.Point(96, 312);
			this.btnRedo.Name = "btnRedo";
			this.btnRedo.Size = new System.Drawing.Size(112, 32);
			this.btnRedo.TabIndex = 3;
			this.btnRedo.Text = "重新压缩";
			this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
			// 
			// StepControl3
			// 
			this.Controls.Add(this.btnRedo);
			this.Controls.Add(this.lblmsg);
			this.Controls.Add(this.label1);
			this.Name = "StepControl3";
			this.Size = new System.Drawing.Size(624, 480);
			this.Load += new System.EventHandler(this.StepControl3_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public VersionData verdata = null;
		public string FolderPath = string.Empty;

		private void StepControl3_Load(object sender, System.EventArgs e)
		{
			this.ctlnext = GlobalObjects.ctl4;
			this.ctlprev = GlobalObjects.ctl2;

			//////////////////////////////		

			btnRedo_Click(sender,e);

		}

		private void btnRedo_Click(object sender, System.EventArgs e)
		{
			if(verdata == null)
			{
				lblmsg.Text = "刷新版本信息出错！请重试！";
				return;
			}
            
            DirectoryInfo di = new DirectoryInfo(FolderPath);

			string tofile = Path.Combine(
                    Path.Combine(di.Parent.FullName , ConfigurationValues.DownDir),
					ConfigurationValues.DataFileName);
			verdata.WriteXml(tofile, XmlWriteMode.WriteSchema);

			lblmsg.Text = "写版本信息成功！\n";
			lblmsg.Text += "正在压缩...\n";
			Application.DoEvents();

			
			if(!di.Exists)
			{
				lblmsg.Text += "压缩失败！";
				return;
			}

            string zipedfile = Path.Combine(di.Parent.FullName + "\\" + ConfigurationValues.DownDir, di.Name + ".zip");
			bool bret = Tools.ZipLibrary.ZipHelper.ZipDirectory(FolderPath,zipedfile);

			if(!bret)
			{
				lblmsg.Text += "压缩失败.请重试.";
				return;
			}
			FileInfo fi = new FileInfo(zipedfile);
			if(fi.Exists)
			{
				lblmsg.Text += "压缩成功.压缩文件为：\n" + zipedfile;

				FileListData fld = new FileListData();
				DataRow  row = fld.Tables[FileListData.mTableName].NewRow();
				row[FileListData.mFileName] = fi.Name;
				row[FileListData.mFileLength] = fi.Length;
				fld.Tables[FileListData.mTableName].Rows.Add(row);

				tofile = Path.Combine(
                    Path.Combine(di.Parent.FullName, ConfigurationValues.DownDir),					
					ConfigurationValues.ListFileName);

				fld.WriteXml(tofile, XmlWriteMode.WriteSchema);

				lblmsg.Text += "生成下载列表文件成功！\n";
			}	
		}


	}
}
