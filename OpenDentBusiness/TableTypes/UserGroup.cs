using System;

namespace OpenDentBusiness{

	///<summary>A group of users.  Security permissions are determined by the usergroup of a user.</summary>
	public class UserGroup{
		///<summary>Primary key.</summary>
		public int UserGroupNum;
		///<summary>.</summary>
		public string Description;

		///<summary></summary>
		public UserGroup Copy(){
			UserGroup u=new UserGroup();
			u.UserGroupNum=UserGroupNum;
			u.Description=Description;
			return u;
		}

		

	}
 
	

	
}













