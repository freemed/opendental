using System;
using System.Collections;


namespace OpenDentBusiness{

	///<summary>One clock-in / clock-out pair.</summary>
	[Serializable()]
	public class ClockEvent:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ClockEventNum;
		///<summary>FK to employee.EmployeeNum</summary>
		public long EmployeeNum;
		///<summary>The actual time that this entry was entered.</summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateTEntry)]
		public DateTime TimeEnteredIn;
		///<summary>The time to display and to use in all calculations.</summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateTEntryEditable)]
		public DateTime TimeDisplayedIn;
		///<summary>True for ClockIn, and false for ClockOut.</summary>
		public bool ClockIn;
		///<summary>Enum:TimeClockStatus  Home, Lunch, or Break.  The status only applies to the clock out.</summary>
		public TimeClockStatus ClockStatus;
		///<summary>.</summary>
		public string Note;
		///<summary></summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateTEntry)]
		public DateTime TimeEnteredOut;
		///<summary></summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateTEntryEditable)]
		public DateTime TimeDisplayedOut;

		///<summary></summary>
		public ClockEvent Copy() {
			return (ClockEvent)MemberwiseClone();
		}



	}

	
}




