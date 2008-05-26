using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
		///<summary>(User OD since "user" is a reserved word) Users are a completely separate entity from Providers and Employees even though they can be linked.  A usernumber can never be changed, ensuring a permanent way to record database entries and leave an audit trail.  A user can be a provider, employee, or neither.</summary>
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
		///<summary>Set true to hide user from login list.</summary>
		public bool IsHidden;
		///<summary>FK to tasklist.TaskListNum.  0 if no inbox setup yet.  It is assumed that the TaskList is in the main trunk, but this is not strictly enforced.  User can't delete an attached TaskList, but they could move it.</summary>
		public int TaskListInBox;

		public Userod(){

		}

		/*public Userod(int userNum,string userName,string password,int userGroupNum,int employeeNum,int clinicNum,int provNum,bool isHidden){
			UserNum=userNum;
			UserName=userName;
			Password=password;
			UserGroupNum=userGroupNum;
			EmployeeNum=employeeNum;
			ClinicNum=clinicNum;
			ProvNum=provNum;
			IsHidden=isHidden;
		}*/
		
		///<summary></summary>
		public Userod Copy(){
			return (Userod)this.MemberwiseClone();
		}

		public override string ToString(){
			return UserName;
		}
	}

	//public class DtoUserodRefresh:DtoQueryBase {
	//}

}
