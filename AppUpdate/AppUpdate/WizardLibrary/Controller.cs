using System;

namespace Tools.WizardLibrary
{
	/// <summary>
	/// Controller 的摘要说明。
	/// </summary>
	public class Controller
	{
		private StepControlBase ctl = null;
		private WizardFormBase frm = null;

		public Controller()
		{;
		}

		public Controller(WizardFormBase form, StepControlBase control)
		{
			frm = form;
			ctl = control;
		}

		public bool SetButtonStates()
		{
			if( (ctl==null) || (frm == null) )
			{
				return false;
			}
			return SetButtonStates(ctl.IsPrevEnable,ctl.IsNextEnable,
				ctl.IsCancelEnable,ctl.IsFinishEnable);
		}

		private bool SetButtonStates(bool prev,bool next,bool cancel,bool finish)
		{
			if( (ctl==null) || (frm == null) )
			{
				return false;
			}
			frm.ButtonPrev.Enabled = prev;
			frm.ButtonNext.Enabled = next;
			frm.ButtonCancel.Enabled = cancel;
			frm.ButtonFinish.Enabled = finish;
			return true;
		}
		private bool SetButtonStates(bool prev,bool next)
		{
			if( (ctl==null) || (frm == null) )
			{
				return false;
			}
			return this.SetButtonStates(prev,next,ctl.IsCancelEnable,ctl.IsFinishEnable);
		}


		public WizardFormBase Form
		{
			set
			{
				frm = value;
			}
			get
			{
				return frm;
			}
		}
		public StepControlBase Control
		{
			set
			{
				ctl = value;
			}
			get
			{
				return ctl;
			}
		}

		public void SetHearderText(string text)
		{
			
		}

		public void PanelAddControl(StepControlBase scb)
		{
	
		}

	}
}
