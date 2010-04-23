using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>All recalls are based on these recall types.  Recall triggers are in their own table.</summary>
	[Serializable()]
	public class RecallType:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long RecallTypeNum;
		///<summary></summary>
		public string Description;
		///<summary>The interval between recalls.  The Interval struct combines years, months, weeks, and days into a single integer value.</summary>
		public Interval DefaultInterval;
		///<summary>For scheduling the appointment.</summary>
		public string TimePattern;
		///<summary>What procedures to put on the recall appointment.  Comma delimited set of ProcCodes.  (We may change this to CodeNums).</summary>
		public string Procedures;
		
		public RecallType Copy(){
			return (RecallType)this.MemberwiseClone();
		}	
	}
}

