using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Tools.WizardLibrary
{
	public delegate void ControlActivated(object sender,EventArgs e);

	/// <summary>
	/// StepControlBase 的摘要说明。
	/// </summary>
	public class StepControlBase : System.Windows.Forms.UserControl, IUIWizard
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StepControlBase()
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
			// 
			// StepControlBase
			// 
			this.Name = "StepControlBase";
			this.Size = new System.Drawing.Size(632, 472);

		}
		#endregion

		#region Interface Method

		

		public virtual bool GoPrevious()
		{
			if( (WizardObjects.MainCtler != null) && (this.ctlprev!=null) )
			{	
				WizardObjects.MainCtler.PanelAddControl(this.ctlprev);
			}
			else
			{
				this.IsPrevEnable = false;
				return false;
			}
			return true;
		}
		public virtual bool GoNext()
		{
			if( (WizardObjects.MainCtler != null) && (this.ctlnext!=null) )
			{	
				WizardObjects.MainCtler.PanelAddControl(this.ctlnext);
			}
			else
			{
				this.IsNextEnable = false;
				return false;
			}
			return true;
		}
		public virtual bool GoFinish()
		{
			return true;
		}
		public virtual bool GoCancel()
		{
			if(null == WizardObjects.MainCtler) return false;

			if(MessageBox.Show(this,"确定退出？","提示",
				MessageBoxButtons.YesNo,MessageBoxIcon.Information)
				==DialogResult.Yes)
			{
				WizardObjects.MainCtler.Form.Close();
				return true;
			}
			return false;
		}
		#endregion


		public virtual void ResetButtonEnable()
		{
			IsPrevEnable = true;
			IsNextEnable = true;
			IsCancelEnable = true;
			IsFinishEnable = true;
		}

		protected StepControlBase ctlnext = null;
		protected StepControlBase ctlprev = null;


		public bool IsCancelEnable = true;
		public bool IsPrevEnable = true;
		public bool IsNextEnable = true;
		public bool IsFinishEnable = true;

		/*
		[Category("CustomEvent"),Description("控件的Activated事件,在Form Activated激活的调用")]
		public event EventHandler CustomActivatedEvent;
		public virtual void OnCustomControlActivated(object sender,EventArgs e)
		{
			if(CustomActivatedEvent != null)
			{
				CustomActivatedEvent(sender,e);
			}
		}
		*/

		//窗口在重新显示的时候，是否需要重新调用Form_Load; true需要调用。
		//public bool IsFormReload = false;

		public string HeaderText = string.Empty;

	}
}
