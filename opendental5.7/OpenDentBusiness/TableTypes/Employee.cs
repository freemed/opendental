using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>An employee at the dental office.</summary>
	public class Employee{
		///<summary>Primary key.</summary>
		public int EmployeeNum;
		///<summary>Employee's last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle initial or name.</summary>
		public string MiddleI; 
		///<summary>If hidden, the employee will not show on the list.</summary>
		public bool IsHidden;
		///<summary>This is just text used to quickly display the clockstatus.  eg Working,Break,Lunch,Home, etc.</summary>
		public string ClockStatus;
		//public string Abbrev;//Not in use
		//public bool IsAdmin;//Not in use
		//public string TimePeriodType;//Not in use
	}
	

	

	
	

}













