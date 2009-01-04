using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenDentMobile {
	public class MsgBox {
		///<summary></summary>
		public static void Show(string text){
			MessageBox.Show(text);
		}

		public static bool Show(string text,bool isQuest){
			DialogResult result=MessageBox.Show(text,"",MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
			if(result==DialogResult.OK){
				return true;
			}
			return false;
		}
	}
}
