using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Each item is attached to a row in the apptview table.  Each item specifies ONE of: OpNum, ProvNum, or Element.  The other two will be 0 or "".</summary>
	public class ApptViewItem {
		///<summary>Primary key.</summary>
		public long ApptViewItemNum;//
		///<summary>FK to apptview.</summary>
		public long ApptViewNum;
		///<summary>FK to operatory.OperatoryNum.</summary>
		public long OpNum;
		///<summary>FK to provider.ProvNum.</summary>
		public long ProvNum;
		///<summary>Must be one of the hard coded strings picked from the available list.</summary>
		public string ElementDesc;
		///<summary>If this is a row Element, then this is the 0-based order.</summary>
		public long ElementOrder;
		///<summary>If this is an element, then this is the color.</summary>
		public Color ElementColor;

		public ApptViewItem(){

		}

		///<summary>this constructor is just used in GetForCurView when no view selected.</summary>
		public ApptViewItem(string elementDesc,long elementOrder,Color elementColor) {
			ApptViewItemNum=0;
			ApptViewNum=0;
			OpNum=0;
			ProvNum=0;
			ElementDesc=elementDesc;
			ElementOrder=elementOrder;
			ElementColor=elementColor;
		}

		


	}	
	
}
