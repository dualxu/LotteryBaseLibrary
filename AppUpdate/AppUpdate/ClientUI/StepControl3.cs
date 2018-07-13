using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Tools.WizardLibrary;
using Tools.DownloadLibrary;
using Tools.ZipLibrary;
using updater.DataModels;
using updater.VersionCheck;
using System.Configuration;

namespace updater.ClientUI
{
	/// <summary>
	/// StepControl3 的摘要说明。
	/// </summary>
	public class StepControl3 : Tools.WizardLibrary.StepControlBase
	{
		private System.Windows.Forms.ProgressBar progressBar1Current;
		private System.Windows.Forms.ProgressBar progressBar2Total;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblcurpgsvalue;
		private System.Windows.Forms.Label lbltotalpgsvalue;
		private System.Windows.Forms.Label lblcurfilename;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button btnRedo;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnResume;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
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

			//add delegate 
			this.singlePercentChanger += new StringIntDelegate(this.ChangeSinglePercent);
			this.totalPercentChanger += new IntDelegate(this.ChangeTotalPercent);
			this.FileCompletedCharger += new StringDelegate(this.CompletedFile);

			label3.Text = "文件下载完成，请点击“下一步”进行软件升级（建议先关闭主程序）。";
			label3.Visible = false;

			label4.Text = "";
			label4.Visible = false;

			this.HeaderText = "第三步：下载新版本文件";
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
			this.progressBar1Current = new System.Windows.Forms.ProgressBar();
			this.progressBar2Total = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblcurpgsvalue = new System.Windows.Forms.Label();
			this.lbltotalpgsvalue = new System.Windows.Forms.Label();
			this.lblcurfilename = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.btnRedo = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnResume = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar1Current
			// 
			this.progressBar1Current.Location = new System.Drawing.Point(24, 200);
			this.progressBar1Current.Name = "progressBar1Current";
			this.progressBar1Current.Size = new System.Drawing.Size(552, 16);
			this.progressBar1Current.TabIndex = 0;
			// 
			// progressBar2Total
			// 
			this.progressBar2Total.Location = new System.Drawing.Point(24, 248);
			this.progressBar2Total.Name = "progressBar2Total";
			this.progressBar2Total.Size = new System.Drawing.Size(552, 16);
			this.progressBar2Total.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 168);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "正在下载：";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 224);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(200, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "全部进度：";
			// 
			// lblcurpgsvalue
			// 
			this.lblcurpgsvalue.Location = new System.Drawing.Point(536, 176);
			this.lblcurpgsvalue.Name = "lblcurpgsvalue";
			this.lblcurpgsvalue.Size = new System.Drawing.Size(40, 16);
			this.lblcurpgsvalue.TabIndex = 4;
			this.lblcurpgsvalue.Text = "100%";
			// 
			// lbltotalpgsvalue
			// 
			this.lbltotalpgsvalue.Location = new System.Drawing.Point(536, 224);
			this.lbltotalpgsvalue.Name = "lbltotalpgsvalue";
			this.lbltotalpgsvalue.Size = new System.Drawing.Size(40, 16);
			this.lbltotalpgsvalue.TabIndex = 5;
			this.lbltotalpgsvalue.Text = "100%";
			// 
			// lblcurfilename
			// 
			this.lblcurfilename.Location = new System.Drawing.Point(104, 168);
			this.lblcurfilename.Name = "lblcurfilename";
			this.lblcurfilename.Size = new System.Drawing.Size(104, 24);
			this.lblcurfilename.TabIndex = 6;
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3});
			this.listView1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.listView1.Location = new System.Drawing.Point(24, 16);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(448, 144);
			this.listView1.TabIndex = 7;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "文件";
			this.columnHeader1.Width = 150;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "大小(B)";
			this.columnHeader2.Width = 125;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "状态";
			this.columnHeader3.Width = 140;
			// 
			// btnRedo
			// 
			this.btnRedo.Location = new System.Drawing.Point(496, 112);
			this.btnRedo.Name = "btnRedo";
			this.btnRedo.Size = new System.Drawing.Size(104, 32);
			this.btnRedo.TabIndex = 9;
			this.btnRedo.Text = "重新下载(&R)";
			this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(496, 64);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(104, 32);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "暂停下载(P)";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnResume
			// 
			this.btnResume.Location = new System.Drawing.Point(496, 16);
			this.btnResume.Name = "btnResume";
			this.btnResume.Size = new System.Drawing.Size(104, 32);
			this.btnResume.TabIndex = 11;
			this.btnResume.Text = "继续下载(&D)";
			this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(24, 320);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(568, 48);
			this.label4.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(24, 280);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(568, 32);
			this.label3.TabIndex = 13;
			// 
			// StepControl3
			// 
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnResume);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnRedo);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.lblcurfilename);
			this.Controls.Add(this.lbltotalpgsvalue);
			this.Controls.Add(this.lblcurpgsvalue);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBar2Total);
			this.Controls.Add(this.progressBar1Current);
			this.Name = "StepControl3";
			this.Size = new System.Drawing.Size(656, 406);
			this.Load += new System.EventHandler(this.StepControl3_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private bool bnodown = false;
		private string strnodownlist = "";

		private void StepControl3_Load(object sender, System.EventArgs e)
		{
			this.ctlnext = GlobalObjects.ctl4;
		 

			this.Cursor = Cursors.WaitCursor;

			///////////////////////////////////
			filenames = new string[GlobalObjects.ServerFileData.Tables[FileListData.mTableName].Rows.Count];
			filelengths = new int[GlobalObjects.ServerFileData.Tables[FileListData.mTableName].Rows.Count];
			foreach(DataRow row in GlobalObjects.ServerFileData.Tables[FileListData.mTableName].Rows)
			{
				ListViewItem lvi = new ListViewItem(
					new string[] { row[FileListData.mFileName].ToString(), 
					row[FileListData.mFileLength].ToString(), "" });

				listView1.Items.Add(lvi);

				int i=0;
				filenames[i] = row[FileListData.mFileName].ToString();
				filelengths[i] = int.Parse(row[FileListData.mFileLength].ToString());
				i++;
			}
			Application.DoEvents();

			if(filenames.Length > 1)
			{
				label2.Visible = true;
				lbltotalpgsvalue.Visible = true;
				progressBar2Total.Visible = true;
			}
			else
			{
				label2.Visible = false;
				lbltotalpgsvalue.Visible = false;
				progressBar2Total.Visible = false;
			}


			dstpath = Path.Combine(Application.StartupPath,GlobalObjects.C_Download);
			if( !Directory.Exists(dstpath) )
			{
				Directory.CreateDirectory(dstpath);
			}

			this.curState = EnumDownState.Downloading;
			this.SetDownState();
			
			btnResume_Click(sender,e);	

			
			this.Cursor = Cursors.Arrow;
		}
		
		/*
		private delegate void NoDownDelegate(bool bnodown,int inodown);
			
		private void NoDownMsg(bool bnodown,int inodown)
		{
			if(bnodown)
			{		
				string tempstr = "程序发现文件" + filenames[inodown]
					+ "上次已经下载完成，本次没有进行下载。\n如果确定需要更新，请点击“重新下载”刷新下载。";
				label4.Text = tempstr;
				//MessageBox.Show(this, tempstr,"提醒",MessageBoxButtons.OK,MessageBoxIcon.Information);
				label4.ForeColor = Color.Red;
				label4.Visible = true;
			}
		}
		*/

		private string[] filenames = null;
		private int[] filelengths = null;
		private void btnRedo_Click(object sender, System.EventArgs e)
		{
			for(int i=0;i<filenames.Length;i++)
			{
				if( File.Exists( Path.Combine(dstpath,filenames[i])) )
				{
					File.Delete(Path.Combine(dstpath,filenames[i])) ;
				}
			}

			btnResume_Click(sender,e);
		}

		string dstpath = string.Empty;
		private void BatchDownload()
		{
			cancelEvent = new System.Threading.ManualResetEvent(false);

			string[] urls = new string[filenames.Length];
			for(int i=0;i<filenames.Length;i++)
			{
				urls[i] = ConfigurationValues.UpdateServerPath + filenames[i];
			}

			using(HttpBatchDownloader dl = new HttpBatchDownloader())
			{
				dl.UrlsInfo = new BatchUrlInfo[urls.Length];
				for(int i=0;i<urls.Length;i++)
				{
					dl.UrlsInfo[i] = new BatchUrlInfo();
					dl.UrlsInfo[i].url = urls[i];
					dl.UrlsInfo[i].length = filelengths[i];
				}

				dl.CurrentProgressChanged += new DownloadProgressHandler(CurrentProgressProcess);
				dl.TotalProgressChanged += new DownloadProgressHandler(TotalProgressProcess);
				dl.FileCompleted += new DownloadProgressHandler(FileCompletedProcess);
				dl.Download( dstpath, cancelEvent );
			}

		}

		private System.Threading.ManualResetEvent cancelEvent = null;
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.curState = EnumDownState.Paused;
			this.SetDownState();

			this.cancelEvent.Set();

			foreach(ListViewItem lvi in listView1.Items)
			{
				if(lvi.SubItems[0].Text.Trim() == curDownFile )
				{
					lvi.SubItems[2].Text = "暂停";
					break;
				}
			}
		}

		private delegate void IntDelegate(int i);
		private delegate void StringIntDelegate(string text,int i);
		private delegate void StringDelegate(string text);
		private StringIntDelegate singlePercentChanger;
		private IntDelegate totalPercentChanger;
		private StringDelegate FileCompletedCharger;
		
		private void ChangeSinglePercent(string text,int percent)
		{
			progressBar1Current.Value = percent;
			lblcurpgsvalue.Text = percent.ToString() + "%";

			foreach(ListViewItem lvi in listView1.Items)
			{
				if(lvi.SubItems[0].Text.Trim() == Path.GetFileName(text) )
				{
					lvi.SubItems[2].Text = "正在下载...";
					break;
				}
			}

			curDownFile = Path.GetFileName(text);

			lblcurfilename.Text = curDownFile;
		}

		private void ChangeTotalPercent(int percent)
		{
			progressBar2Total.Value = percent;
			lbltotalpgsvalue.Text = percent.ToString() + "%";

			if(percent >=100)
			{
				this.curState = EnumDownState.Finished;
				this.SetDownState();
			}
		}

		private void CompletedFile(string filefullname)
		{			
			foreach(ListViewItem lvi in listView1.Items)
			{
				if(lvi.SubItems[0].Text.Trim() == Path.GetFileName(filefullname) )
				{
					lvi.SubItems[2].Text = "完成";
					break;
				}
			}

			curDownFile = "";
		}

		private void CurrentProgressProcess(object sender, DownloadEventArgs e)
		{
			this.Invoke(this.singlePercentChanger,new object[] { e.DownloadState, e.PercentDone } );
		}

		private void TotalProgressProcess(object sender, DownloadEventArgs e)
		{
			this.Invoke(this.totalPercentChanger,new object[] { e.PercentDone } );
		}

		private void FileCompletedProcess(object sender, DownloadEventArgs e)
		{
			this.Invoke(this.FileCompletedCharger, new object[] { e.DownloadState});
		}

		private void CheckNoDown()
		{
			bnodown = false;
			strnodownlist = "";
			for(int i=0;i<filenames.Length;i++)
			{
				string filepath = Path.Combine(dstpath,filenames[i]);
				if( File.Exists(filepath) )
				{
					FileInfo fi = new FileInfo(filepath);
					if(fi.Length >= filelengths[i])
					{
						bnodown = true;
						if(strnodownlist.Length >0)
						{
							strnodownlist += ",";
						}
						strnodownlist += filenames[i];
					}
				}
			}

			label4.Text = "程序发现文件" + strnodownlist
				+ "上次已经下载完成，本次没有进行下载。\n如果确定需要更新，请点击“重新下载”刷新下载。";
			label4.ForeColor = Color.Red;
		}

		private void btnResume_Click(object sender, System.EventArgs e)
		{
			this.curState = EnumDownState.Downloading;
			this.SetDownState();
			
			CheckNoDown();

			System.Threading.Thread t = new System.Threading.Thread(
				new System.Threading.ThreadStart(BatchDownload) );
			t.IsBackground = true;
			t.Start();

			//this.Invoke(new NoDownDelegate(NoDownMsg), new object[] {bnodown, inodown} );
		}

		public enum EnumDownState
		{
			None,
			Downloading,
			Paused,
			Finished
		};

		private EnumDownState curState = EnumDownState.None;

		private void SetDownState()
		{
			switch(this.curState)
			{
				case EnumDownState.Downloading:
					btnCancel.Enabled = true;
					btnResume.Enabled = false;
					btnRedo.Enabled = false;

					this.IsNextEnable = this.IsPrevEnable = false;
					WizardObjects.MainCtler.SetButtonStates();
					label3.Visible = false;
					break;
				case EnumDownState.Paused:
					btnCancel.Enabled = false;
					btnResume.Enabled = true;
					btnRedo.Enabled = true;

					this.IsNextEnable = this.IsPrevEnable = true;
					WizardObjects.MainCtler.SetButtonStates();
					label3.Visible = false;
					break;
				case EnumDownState.Finished:
					btnCancel.Enabled = false;
					btnResume.Enabled = false;
					btnRedo.Enabled = true;

					this.IsNextEnable = this.IsPrevEnable = true;
					WizardObjects.MainCtler.SetButtonStates();
					label3.Visible = true;
					break;
				default:
					btnCancel.Enabled = false;
					btnResume.Enabled = false;
					btnRedo.Enabled = true;

					this.IsNextEnable = this.IsPrevEnable = true;
					WizardObjects.MainCtler.SetButtonStates();
					label3.Visible = false;
					break;
			}
			label4.Visible = label3.Visible && bnodown;
			Application.DoEvents();
		}


		private string curDownFile = string.Empty;


	}
}
