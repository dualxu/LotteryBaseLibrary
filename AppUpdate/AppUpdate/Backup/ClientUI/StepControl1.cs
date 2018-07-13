using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using updater.VersionCheck;

namespace updater.ClientUI
{
	/// <summary>
	/// StepControl1 的摘要说明。
	/// </summary>
	public class StepControl1 : Tools.WizardLibrary.StepControlBase
	{
        private System.Windows.Forms.Panel panel1;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StepControl1()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			this.ResetButtonEnable();
			this.IsPrevEnable = false;
			this.IsFinishEnable = false;

			this.HeaderText = "HHJT：启动充值管理程序";
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 427);
            this.panel1.TabIndex = 1;
            // 
            // StepControl1
            // 
            this.BackgroundImage = global::updater.ClientUI.Properties.Resources.背景杂色;
            this.Controls.Add(this.panel1);
            this.Name = "StepControl1";
            this.Size = new System.Drawing.Size(588, 427);
            this.Load += new System.EventHandler(this.StepControl1_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void StepControl1_Load(object sender, System.EventArgs e)
		{
		 
			this.ctlprev = null;

			// ///////////////////////////////

			Tools.WizardLibrary.WizardObjects.MainCtler.SetHearderText(this.HeaderText);

		}

		public override bool GoNext()
		{
	 

			return base.GoNext ();
		}

       


	}
}
