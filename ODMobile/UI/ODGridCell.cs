using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentMobile.UI {
	///<summary></summary>
	public class ODGridCell{		
		private string text;
		
		///<summary>Creates a new ODGridCell.</summary>
		public ODGridCell(){
			text="";
		}

		///<summary>Creates a new ODGridCell.</summary>
		public ODGridCell(string myText){
			text=myText;
		}

		///<summary></summary>
		public string Text{
			get{
				return text;
			}
			set{
				text=value;
			}
		}

	        

	}
}
