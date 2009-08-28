using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Enables viewing a variety of operatories or providers.  This table holds the views that the user picks between.  The apptviewitem table holds the items attached to each view.</summary>
	public class ApptView{
		///<summary>Primary key.</summary>
		public long ApptViewNum;
		///<summary>Description of this view.  Gets displayed in Appt module.</summary>
		public string Description;
		///<summary>Order to display in lists. Every view must have a unique itemorder, but it is acceptable to have some missing itemorders in the sequence.</summary>
		public int ItemOrder;
		///<summary>Number of rows per time increment.  Usually 1 or 2.  Programming note: Value updated to ContrApptSheet.RowsPerIncr to track current state.</summary>
		public int RowsPerIncr;
		///<summary>If set to true, then the only operatories that will show will be for providers that have schedules for the day, ops with no provs assigned.</summary>
		public bool OnlyScheduledProvs;

		public ApptView(){

		}

		/*
		public ApptView(long apptViewNum,string description,long itemOrder,long rowsPerIncr){
			ApptViewNum=apptViewNum;
			Description=description;
			ItemOrder=itemOrder;
			RowsPerIncr=rowsPerIncr;
			//OnlyScheduledProvs=
		}*/


	}


}









