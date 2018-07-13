using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tools.WizardLibrary
{
	/// <summary>
	/// WizardFormBase 的摘要说明。
	/// </summary>
	public class WizardFormBase : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panelFoot;
		private System.Windows.Forms.Button btnFinish;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WizardFormBase()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.panelFoot = new System.Windows.Forms.Panel();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelFoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFoot
            // 
            this.panelFoot.Controls.Add(this.btnFinish);
            this.panelFoot.Controls.Add(this.btnNext);
            this.panelFoot.Controls.Add(this.btnPrev);
            this.panelFoot.Controls.Add(this.btnCancel);
            this.panelFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFoot.Location = new System.Drawing.Point(0, 277);
            this.panelFoot.Name = "panelFoot";
            this.panelFoot.Size = new System.Drawing.Size(485, 48);
            this.panelFoot.TabIndex = 6;
            // 
            // btnFinish
            // 
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.Location = new System.Drawing.Point(381, 8);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(88, 32);
            this.btnFinish.TabIndex = 3;
            this.btnFinish.Text = "完成(&O)";
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(277, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(88, 32);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步(&N)";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrev.Location = new System.Drawing.Point(173, 8);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(88, 32);
            this.btnPrev.TabIndex = 1;
            this.btnPrev.Text = "上一步(&P)";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(69, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WizardFormBase
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackgroundImage = global::Tools.WizardLibrary.Properties.Resources.背景;
            this.ClientSize = new System.Drawing.Size(485, 325);
            this.Controls.Add(this.panelFoot);
            this.Name = "WizardFormBase";
            this.Text = "自动更新程序";
            this.Load += new System.EventHandler(this.WizardFormBase_Load);
            this.Activated += new System.EventHandler(this.WizardFormBase_Activated);
            this.panelFoot.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		protected Controller corectl = null;

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			MethodCancel(sender,e);
		}

		private void btnPrev_Click(object sender, System.EventArgs e)
		{
			MethodPrev(sender,e);
		}

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			MethodNext(sender,e);			
		}

		private void btnFinish_Click(object sender, System.EventArgs e)
		{
			MethodFinish(sender,e);		
		}

		private void WizardFormBase_Load(object sender, System.EventArgs e)
		{
			MethodFormLoad(sender,e);
		}

		#region protected methods
		protected virtual void MethodPrev(object sender, System.EventArgs e)
		{
			if(null != corectl.Control)
			{
				corectl.Control.GoPrevious();

				corectl.SetButtonStates();
			}
		}
		protected virtual void MethodNext(object sender, System.EventArgs e)
		{
			if(null != corectl.Control)
			{
				corectl.Control.GoNext();

				corectl.SetButtonStates();
			}		
		}
		protected virtual void MethodCancel(object sender, System.EventArgs e)
		{
			if(null != corectl.Control)
			{
				corectl.Control.GoCancel();
			}		
		}
		protected virtual void MethodFinish(object sender, System.EventArgs e)
		{
			if(null != corectl.Control)
			{
				corectl.Control.GoFinish();
			}
		}

		protected virtual void MethodFormLoad(object sender,System.EventArgs e)
		{

		}

		#endregion

		#region public property
		public Button ButtonPrev
		{
			get 
			{
				return btnPrev;
			}
		}
		public Button ButtonNext
		{
			get 
			{
				return btnNext;
			}
		}
		public Button ButtonCancel
		{
			get 
			{
				return btnCancel;
			}
		}
		public Button ButtonFinish
		{
			get 
			{
				return btnFinish;
			}
		}
		#endregion

		private void WizardFormBase_Activated(object sender, System.EventArgs e)
		{
			if(corectl!=null)
			{
				//corectl.Control.OnCustomControlActivated(sender,e);
			}
		}

	}
}
