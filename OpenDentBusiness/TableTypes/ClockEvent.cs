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
		///<summary>The actual time that this entry was entered.  Cannot be 01-01-0001.</summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateTEntry)]
		public DateTime TimeEntered1;
		///<summary>The time to display and to use in all calculations.  Cannot be 01-01-0001.</summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateTEntryEditable)]
		public DateTime TimeDisplayed1;
		///<summary>True for ClockIn, and false for ClockOut.</summary>
		public bool ClockIn;
		///<summary>Enum:TimeClockStatus  Home, Lunch, or Break.  The status only applies to the clock out.  Except the Break status applies to both out and in.</summary>
		public TimeClockStatus ClockStatus;
		///<summary>.</summary>
		public string Note;
		///<summary>The user can never edit this, but the program has to be able to edit this when user clocks out.  Can be 01-01-0001 if not clocked out yet.</summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateT)]
		public DateTime TimeEntered2;
		///<summary>User can edit. Can be 01-01-0001 if not clocked out yet.</summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateT)]
		public DateTime TimeDisplayed2;

		///<summary></summary>
		public ClockEvent Copy() {
			return (ClockEvent)MemberwiseClone();
		}



	}

	
}




