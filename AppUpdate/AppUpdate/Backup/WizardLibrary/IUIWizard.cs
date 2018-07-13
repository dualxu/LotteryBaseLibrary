using System;

namespace Tools.WizardLibrary
{
	/// <summary>
	/// IWizard 的摘要说明。
	/// </summary>
	public interface IUIWizard
	{
		bool GoPrevious();
		bool GoNext();
		bool GoFinish();
		bool GoCancel();		
	}
}
