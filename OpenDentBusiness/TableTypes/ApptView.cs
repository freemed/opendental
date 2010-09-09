using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Enables viewing a variety of operatories or providers.  This table holds the views that the user picks between.  The apptviewitem table holds the items attached to each view.</summary>
	[Serializable()]
	public class ApptView:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ApptViewNum;
		///<summary>Description of this view.  Gets displayed in Appt module.</summary>
		public string Description;
		///<summary>0-based order to display in lists. Every view must have a unique itemorder, but it is acceptable to have some missing itemorders in the sequence.</summary>
		public int ItemOrder;
		///<summary>Number of rows per time increment.  Usually 1 or 2.  Programming note: Value updated to ContrApptSheet.RowsPerIncr to track current state.</summary>
		public byte RowsPerIncr;
		///<summary>If set to true, then the only operatories that will show will be for providers that have schedules for the day, ops with no provs assigned.</summary>
		public bool OnlyScheduledProvs;
		///<summary>If OnlyScheduledProvs is set to true, and this time is not 0:00, then only provider schedules with start or stop time before this time will be included.</summary>
		public TimeSpan OnlySchedBeforeTime;
		///<summary>If OnlyScheduledProvs is set to true, and this time is not 0:00, then only provider schedules with start or stop time after this time will be included.</summary>
		public TimeSpan OnlySchedAfterTime;
		///<summary></summary>
		public ApptViewStackBehavior StackBehavUR;
		///<summary></summary>
		public ApptViewStackBehavior StackBehavLR;



		public ApptView(){

		}



	}

	public enum ApptViewStackBehavior {
		Vertical,
		Horizontal
	}


}









