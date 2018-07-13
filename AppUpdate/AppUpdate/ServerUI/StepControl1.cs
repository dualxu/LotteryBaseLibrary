using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Tools.WizardLibrary;

namespace updater.ServerUI
{
	/// <summary>
	/// StepForm1 的摘要说明。
	/// </summary>
	public class StepControl1 : StepControlBase
	{
		private System.Windows.Forms.Label label1;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StepControl1()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			//add in contructor
			

			this.ResetButtonEnable();
			this.IsPrevEnable = false;
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
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(256, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(352, 192);
			this.label1.TabIndex = 0;
			this.label1.Text = "说明：POS升级启动";
			// 
			// StepControl1
			// 
			this.Controls.Add(this.label1);
			this.Name = "StepControl1";
			this.Size = new System.Drawing.Size(648, 448);
			this.Load += new System.EventHandler(this.StepControl1_Load);
			this.ResumeLayout(false);

		}
		#endregion

			

		private void StepControl1_Load(object sender, System.EventArgs e)
		{
			this.ctlnext = GlobalObjects.ctl2;
			this.ctlprev = null;
		}


	}
}
