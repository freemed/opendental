using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
		///<summary>(User OD since user is a reserved word) Users are a completely separate entity from Providers and Employees.  A usernumber can never be changed, ensuring a permanent way to record database entries and leave an audit trail.  A provider or employee can have multiple user entries for different situations.  You can also have users who are neither providers nor employees.</summary>
	public class Userod{
		///<summary>Primary key.</summary>
		public int UserNum;
		///<summary>.</summary>
		public string UserName;
		///<summary>The password hash, not the actual password.  If no password has been entered, then this will be blank.</summary>
		public string Password;
		///<summary>FK to usergroup.UserGroupNum.  Every user belongs to exactly one user group.  Th usergroup determines the permissions.</summary>
		public int UserGroupNum;
		///<summary>FK to employee.EmployeeNum.  Used for timecards to block access by other users.</summary>
		public int EmployeeNum;
		///<summary>FK to clinic.ClinicNum.  If 0, then user has access to all clinics.</summary>
		public int ClinicNum;

		///<summary></summary>
		public Userod Copy(){
			Userod u=new Userod();
			u.UserNum=UserNum;
			u.UserName=UserName;
			u.Password=Password;
			u.UserGroupNum=UserGroupNum;
			u.EmployeeNum=EmployeeNum;
			u.ClinicNum=ClinicNum;
			return u;
		}
	}

	public class DtoUserodRefresh:DtoQueryBase {
	}

}
