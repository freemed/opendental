using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Enables viewing a variety of operatories or providers.  This table holds the views that the user picks between.  The apptviewitem table holds the items attached to each view.</summary>
	public class ApptView{
		///<summary>Primary key.</summary>
		public int ApptViewNum;
		///<summary>Description of this view.  Gets displayed in Appt module.</summary>
		public string Description;
		///<summary>Order to display in lists. Every view must have a unique itemorder, but it is acceptable to have some missing itemorders in the sequence.</summary>
		public int ItemOrder;
		///<summary>Number of rows per time increment.  Usually 1 or 2.  Programming note: Value updated to ContrApptSheet.RowsPerIncr to track current state.</summary>
		public int RowsPerIncr;
	}
	
	
	

	


}









