using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>Each condition evaluates to true or false.  A series of conditions for a single automation is ANDed together.</summary>
	[Serializable]
	public class AutomationCondition:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AutomationConditionNum;
		///<summary>FK to automation.AutomationNum. </summary>
		public long AutomationNum;
		///<summary>Enum:AutoCondField </summary>
		public AutoCondField CompareField;
		///<summary>Enum:AutoCondComparison Not all comparisons are allowed with all data types.</summary>
		public AutoCondComparison Comparison;
		///<summary></summary>
		public string CompareString;

		///<summary></summary>
		public AutomationCondition Clone() {
			return (AutomationCondition)this.MemberwiseClone();
		}

	}

	public enum AutoCondField {
		///<summary>Typically specify Equals the exact name/description of the sheet.</summary>
		SheetNotCompletedTodayWithName,
		///<summary>disease</summary>
		Problem,
		Medication,
		Allergy,
		///<summary>Example, 23</summary>
		Age,
		///<summary>Allowed values are M or F, not case sensitive.  Enforce at entry time.</summary>
		Gender,
		Labresult
	}

	public enum AutoCondComparison{
		///<summary>Not sensitive to capitalization.</summary>
		Equals,
		GreaterThan,
		LessThan,
		///<summary>aka Like</summary>
		Contains
		//Exists,
		//NotEquals,
		//
	}
}