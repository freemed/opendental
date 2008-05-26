using System;
using System.Windows.Forms;

namespace OpenDental
{
	///<summary>This is a more efficient version of the MS MessageBox.</summary>
	public class MsgBox
	{
		

		///<summary>This is a more efficient version of the MS MessageBox. It also automates the language translation. Do NOT use if the text is variable in any way, because it will mess up the translation feature.</summary>
		public static void Show(System.Object sender,string text){
			MessageBox.Show(Lan.g(sender.GetType().Name,text));
		}

		///<summary>This is a more efficient version of the MS MessageBox. It also automates the language translation.  Will show okCancel regardless if you set it to true or false, so always use true.</summary>
		public static bool Show(System.Object sender,bool okCancel,string question){
			if(MessageBox.Show(Lan.g(sender.GetType().Name,question),"",MessageBoxButtons.OKCancel)
				==DialogResult.OK)
			{
				return true;
			}
			else{
				return false;
			}
		}

	}
}
