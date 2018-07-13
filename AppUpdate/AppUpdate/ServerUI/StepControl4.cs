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
	/// StepControl4 的摘要说明。
	/// </summary>
	public class StepControl4 : StepControlBase
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.GroupBox groupBox1;
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
			this.label2 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(80, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(456, 32);
			this.label2.TabIndex = 6;
			this.label2.Text = "如果本次升级包含SQL，选中复选框继续，否则完成设置。";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(80, 72);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(216, 24);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "本次升级包含SQL";
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(56, 112);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(528, 304);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// StepControl4
			// 
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkBox1);
			this.Name = "StepControl4";
			this.Size = new System.Drawing.Size(640, 448);
			this.Load += new System.EventHandler(this.StepControl4_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void StepControl4_Load(object sender, System.EventArgs e)
		{
			this.ctlnext = null;
			this.ctlprev = GlobalObjects.ctl3;
		}

		public override bool GoFinish()
		{
			WizardObjects.MainCtler.Form.Close();

			return base.GoFinish ();
		}


	}
}
