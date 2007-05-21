using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
		///<summary>(User OD since user is a reserved word) Users are a completely separate entity from Providers and Employees even though they can be linked.  A usernumber can never be changed, ensuring a permanent way to record database entries and leave an audit trail.  A user can be a provider, employee, or neither.</summary>
	public class Userod{
		///<summary>Primary key.</summary>
		public int UserNum;
		///<summary>.</summary>
		public string UserName;
		///<summary>The password hash, not the actual password.  If no password has been entered, then this will be blank.</summary>
		public string Password;
		///<summary>FK to usergroup.UserGroupNum.  Every user belongs to exactly one user group.  The usergroup determines the permissions.</summary>
		public int UserGroupNum;
		///<summary>FK to employee.EmployeeNum. Cannot be used if provnum is used. Used for timecards to block access by other users.</summary>
		public int EmployeeNum;
		///<summary>FK to clinic.ClinicNum.  If 0, then user has access to all clinics.</summary>
		public int ClinicNum;
		///<summary>FK to provider.ProvNum.  Cannot be used if EmployeeNum is used.</summary>
		public int ProvNum;

		///<summary></summary>
		public Userod Copy(){
			Userod u=new Userod();
			u.UserNum=UserNum;
			u.UserName=UserName;
			u.Password=Password;
			u.UserGroupNum=UserGroupNum;
			u.EmployeeNum=EmployeeNum;
			u.ClinicNum=ClinicNum;
			u.ProvNum=ProvNum;
			return u;
		}
	}

	public class DtoUserodRefresh:DtoQueryBase {
	}

}
