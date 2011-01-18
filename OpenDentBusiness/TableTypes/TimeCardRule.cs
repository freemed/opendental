using System;
using System.Collections;
using System.Drawing;
using System.Xml.Serialization;


namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable]
	public class TimeCardRule:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long TimeCardRuleNum;
		///<summary>FK to employee.EmployeeNum. If zero, then this rule applies to all employees.</summary>
		public long EmployeeNum;
		///<summary>Typical example is 8:00.  In California, any work after the first 8 hours is overtime.</summary>
		[XmlIgnore]
		public TimeSpan OverHoursPerDay;
		///<summary>Typical example is 16:00 to indicate that all time worked after 4pm for specific employees is overtime.</summary>
		[XmlIgnore]
		public TimeSpan AfterTimeOfDay;

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("OverHoursPerDay",typeof(long))]
		public long OverHoursPerDayXml {
			get {
				return OverHoursPerDay.Ticks;
			}
			set {
				OverHoursPerDay = TimeSpan.FromTicks(value);
			}
		}

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("AfterTimeOfDay",typeof(long))]
		public long AfterTimeOfDayXml {
			get {
				return AfterTimeOfDay.Ticks;
			}
			set {
				AfterTimeOfDay = TimeSpan.FromTicks(value);
			}
		}

		///<summary></summary>
		public TimeCardRule Clone() {
			return (TimeCardRule)this.MemberwiseClone();
		}

	}
}