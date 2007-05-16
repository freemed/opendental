using System;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Data;

namespace OpenDental{
	///<summary></summary>
	public class Security{
		///<summary>The current user.  Might be null when first starting the program.  Otherwise, must contain valid user.</summary>
		public static Userod CurUser;

		///<summary></summary>
		public Security(){
			
		}

		///<summary>Checks to see if current user is authorized.  It also checks any date restrictions.  If not authorized, it gives a Message box saying so and returns false.</summary>
		public static bool IsAuthorized(Permissions perm){
			return IsAuthorized(perm,DateTime.MinValue,false);
		}

		///<summary>Checks to see if current user is authorized.  It also checks any date restrictions.  If not authorized, it gives a Message box saying so and returns false.</summary>
		public static bool IsAuthorized(Permissions perm,DateTime date){
			return IsAuthorized(perm,date,false);
		}

		///<summary>Checks to see if current user is authorized.  It also checks any date restrictions.  If not authorized, it gives a Message box saying so and returns false.</summary>
		public static bool IsAuthorized(Permissions perm,DateTime date,bool suppressMessage){
			if(Security.CurUser==null || !GroupPermissions.HasPermission(Security.CurUser.UserGroupNum,perm)){
				if(!suppressMessage){
					MessageBox.Show(Lan.g("Security","Not authorized for")+"\r\n"+GroupPermissions.GetDesc(perm));
				}
				return false;
			}
			if(perm==Permissions.AccountingCreate || perm==Permissions.AccountingEdit){
				if(date <= PrefB.GetDate("AccountingLockDate")){
					if(!suppressMessage) {
						MessageBox.Show(Lan.g("Security","Locked by Administrator."));
					}
					return false;	
				}
			}
			if(!GroupPermissions.PermTakesDates(perm)){
				return true;
			}
			DateTime dateLimit=GetDateLimit(perm,Security.CurUser.UserGroupNum);
			if(date>dateLimit){//authorized
				return true;
			}
			if(!suppressMessage){
				MessageBox.Show(Lan.g("Security","Not authorized for")+"\r\n"
					+GroupPermissions.GetDesc(perm)+"\r\n"+Lan.g("Security","Date limitation"));
			}
			return false;		
		}

		private static DateTime GetDateLimit(Permissions permType,int userGroupNum){
			DateTime nowDate=MiscData.GetNowDateTime().Date;
			DateTime retVal=DateTime.MinValue;
			for(int i=0;i<GroupPermissions.List.Length;i++){
				if(GroupPermissions.List[i].UserGroupNum!=userGroupNum || GroupPermissions.List[i].PermType!=permType){
					continue;
				}
				//this should only happen once.  One match.
				if(GroupPermissions.List[i].NewerDate.Year>1880){
					retVal=GroupPermissions.List[i].NewerDate;
				}
				if(GroupPermissions.List[i].NewerDays==0){//do not restrict by days
					//do not change retVal
				}
				else if(nowDate.AddDays(-GroupPermissions.List[i].NewerDays)>retVal){
					retVal=nowDate.AddDays(-GroupPermissions.List[i].NewerDays);
				}
			}
			return retVal;
		}

		///<summary>Gets a module that the user has permission to use.  Tries the suggestedI first.  If a -1 is supplied, it tries to find any authorized module.  If no authorization for any module, it returns a -1, causing no module to be selected.</summary>
		public static int GetModule(int suggestI){
			if(suggestI!=-1 && IsAuthorized(PermofModule(suggestI),DateTime.MinValue,true)){
				return suggestI;
			}
			for(int i=0;i<7;i++){
				if(IsAuthorized(PermofModule(i),DateTime.MinValue,true)){
					return i;
				}
			}
			return -1;
		}

		private static Permissions PermofModule(int i){
			switch(i){
				case 0:
					return Permissions.AppointmentsModule;
				case 1:
					return Permissions.FamilyModule;
				case 2:
					return Permissions.AccountModule;
				case 3:
					return Permissions.TPModule;
				case 4:
					return Permissions.ChartModule;
				case 5:
					return Permissions.ImagesModule;
				case 6:
					return Permissions.ManageModule;
			}
			return Permissions.None;
		}

		///<summary></summary>
		public static void ResetPassword(){
			//FIXME:UPDATE-MULTIPLE-TABLES
			/*string command="UPDATE userod,grouppermissions SET userod.Password='' "
				+"WHERE grouppermissions.UserGroupNum=userod.UserGroupNum "
				+"AND grouppermissions.PermType=24";
 			General.NonQ(command);
			 */

			//Code updated to be compatible with Oracle as well as MySQL.

			string command="SELECT userod.UserNum FROM userod,grouppermissions "
				+"WHERE grouppermissions.UserGroupNum=userod.UserGroupNum "
				+"AND grouppermissions.PermType=24";
			DataTable table=General.GetTable(command); 
			if(table.Rows.Count==0){
				throw new ApplicationException("No admin exists.");
			}
			command="UPDATE userod SET Password='' WHERE UserNum="+POut.PString(table.Rows[0][0].ToString());
			General.NonQ(command);
		}

	}
}

























