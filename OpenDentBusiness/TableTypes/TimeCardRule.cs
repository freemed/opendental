using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable]
	public class TimeCardRule:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long TimeCardRuleNum;
		///<summary>FK to employee.EmployeeNum. If zero, then this rule applies to all employees.</summary>
		public long EmployeeNum;
		///<summary>Typical example is 8.  In California, any work after the first 8 hours is overtime.</summary>
		public TimeSpan OverHoursPerDay;
		///<summary>Typical example is 16:00 to indicate that all time worked after 4pm for specific employees is overtime.</summary>
		public TimeSpan AfterTimeOfDay;

		///<summary></summary>
		public TimeCardRule Clone() {
			return (TimeCardRule)this.MemberwiseClone();
		}

	}
}