using System;
using System.Collections;

namespace OpenDentBusiness{
	///<summary>a procedure will just be linked to one recall type for now.  The link will remain as a field in the procedurecode table.  The interface will move to the recall setup window.  Later, we could allow attaching one procedure to multiple recall types, but it will require a linking table.</summary>
	public class RecallType{
		///<summary>Primary key.</summary>
		public int RecallTypeNum;
		///<summary></summary>
		public string Description;
		///<summary>The interval between recalls.  The Interval struct combines years, months, weeks, and days into a single integer value.</summary>
		public Interval DefaultInterval;
		///<summary>For scheduling the appointment.</summary>
		public string TimePattern;
		///<summary>What procedures to put on the recall appointment.  Comma delimited.</summary>
		public string Procedures;

		///<summary>Returns a copy of this Recall.</summary>
		public Recall Copy(){
			return (Recall)this.MemberwiseClone();
		}

	}

}









