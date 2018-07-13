using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Tools.WizardLibrary;

namespace updater.ServerUI
{
	/// <summary>
	/// MainForm 的摘要说明。
	/// </summary>
	public class MainForm : Tools.WizardLibrary.WizardFormBase
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
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
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(688, 470);
			this.Name = "MainForm";
			this.Text = "MainForm";

		}
		#endregion

		protected override void MethodFormLoad(object sender, EventArgs e)
		{
			corectl = new Tools.WizardLibrary.Controller();
			corectl.Form = this;
			corectl.Control = GlobalObjects.ctl1;

			WizardObjects.MainCtler = corectl;

        	base.MethodFormLoad (sender, e);

			corectl.SetButtonStates();
		
			this.Controls.Clear();
			this.Controls.Add(corectl.Control);

		}


	}
}
