 using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>This table is not part of the general release.  User would have to add it manually.  All schema changes are done directly on our live database as needed.</summary>
	[Serializable]
	[CrudTable(IsMissingInGeneral=true)]
	public class Phone:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PhoneNum;
		///<summary></summary>
		public int Extension;
		///<summary></summary>
		public string EmployeeName;
		///<summary>This enum is stored in the db as a string, so it needs special handling.  In phoneTrackingServer initialize, this value is pulled from employee.ClockStatus as Home, Lunch, Break, or Working(which gets converted to Available).  After that, the phone server uses those 4 in addition to WrapUp, Off, Training, TeamAssist, OfflineAssist, Backup, and None(which is displayed as an empty string).  The main program sets Unavailable sometimes, and pulls from employee.ClockStatus sometimes.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public ClockStatusEnum ClockStatus;
		///<summary>Either blank or 'In use'</summary>
		public string Description;
		///<summary></summary>
		public Color ColorBar;
		///<summary></summary>
		public Color ColorText;
		///<summary>FK to employee.EmployeeNum.</summary>
		public long EmployeeNum;
		///<summary>The phone number or name of customer.</summary>
		public string CustomerNumber;
		///<summary>Blank or 'in' or 'out'.</summary>
		public string InOrOut;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>The date/time that the phonecall started.  Used to calculate how long user has been on phone.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeStart;
		///<summary></summary>
		public Bitmap WebCamImage;
		

	}

	public enum ClockStatusEnum {
		///<summary>This shows in the UI as blank.</summary>
		None,
		Home,
		Lunch,
		Break,
		Available,
		WrapUp,
		Off,
		Training,
		TeamAssist,
		OfflineAssist,
		Backup,
		Unavailable
	}

	
}




