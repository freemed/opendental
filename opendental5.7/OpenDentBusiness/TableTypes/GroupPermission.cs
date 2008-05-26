using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Every user group has certain permissions.  This defines a permission for a group.  The absense of permission would cause that row to be deleted from this table.</summary>
	public class GroupPermission{
		///<summary>Primary key.</summary>
		public int GroupPermNum;
		///<summary>Only granted permission if newer than this date.  Can be Minimum (01-01-0001) to always grant permission.</summary>
		public DateTime NewerDate;
		///<summary>Can be 0 to always grant permission.  Otherwise, only granted permission if item is newer than the given number of days.  1 would mean only if entered today.</summary>
		public int NewerDays;
		///<summary>FK to usergroup.UserGroupNum.  The user group for which this permission is granted.  If not authorized, then this groupPermission will have been deleted.</summary>
		public int UserGroupNum;
		///<summary>Enum:Permissions</summary>
		public Permissions PermType;

		///<summary></summary>
		public GroupPermission Copy(){
			GroupPermission g=new GroupPermission();
			g.GroupPermNum=GroupPermNum;
			g.NewerDate=NewerDate;
			g.NewerDays=NewerDays;
			g.UserGroupNum=UserGroupNum;
			g.PermType=PermType;
			return g;
		}

	}
 
	

	
}













