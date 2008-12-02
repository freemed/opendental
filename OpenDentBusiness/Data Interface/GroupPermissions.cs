using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDentBusiness{
	///<summary></summary>
	public class GroupPermissions {
		///<summary></summary>
		public static DataTable RefreshCache() {
			string command="SELECT * FROM grouppermission";
			DataTable table=General.GetTable(command);
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			GroupPermissionC.List=new GroupPermission[table.Rows.Count];
			for(int i=0;i<GroupPermissionC.List.Length;i++) {
				GroupPermissionC.List[i]=new GroupPermission();
				GroupPermissionC.List[i].GroupPermNum  = PIn.PInt(table.Rows[i][0].ToString());
				GroupPermissionC.List[i].NewerDate     = PIn.PDate(table.Rows[i][1].ToString());
				GroupPermissionC.List[i].NewerDays     = PIn.PInt(table.Rows[i][2].ToString());
				GroupPermissionC.List[i].UserGroupNum  = PIn.PInt(table.Rows[i][3].ToString());
				GroupPermissionC.List[i].PermType      =(Permissions)PIn.PInt(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		private static void Update(GroupPermission gp){
			string command= "UPDATE grouppermission SET " 
				+"NewerDate = "   +POut.PDate  (gp.NewerDate)
				+",NewerDays = '"   +POut.PInt   (gp.NewerDays)+"'"
				+",UserGroupNum = '"+POut.PInt   (gp.UserGroupNum)+"'"
				+",PermType = '"    +POut.PInt   ((int)gp.PermType)+"'"
				+" WHERE GroupPermNum = '"+POut.PInt(gp.GroupPermNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(GroupPermission gp){
			string command= "INSERT INTO grouppermission (NewerDate,NewerDays,UserGroupNum,PermType) "
				+"VALUES("
				+POut.PDate  (gp.NewerDate)+", "
				+"'"+POut.PInt   (gp.NewerDays)+"', "
				+"'"+POut.PInt   (gp.UserGroupNum)+"', "
				+"'"+POut.PInt   ((int)gp.PermType)+"')";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void InsertOrUpdate(GroupPermission gp, bool isNew){
			if(gp.NewerDate.Year>1880 && gp.NewerDays>0){
				throw new Exception(Lan.g("GroupPermissions","Date or days can be set, but not both."));
			}
			if(!GroupPermissions.PermTakesDates(gp.PermType)){
				if(gp.NewerDate.Year>1880 || gp.NewerDays>0){
					throw new Exception(Lan.g("GroupPermissions","This type of permission may not have a date or days set."));
				}
			}
			if(isNew){
				Insert(gp);
			}
			else{
				Update(gp);
			}
		}

		///<summary></summary>
		public static void RemovePermission(int groupNum,Permissions permType){
			string command;
			if(permType==Permissions.SecurityAdmin){
				//need to make sure that at least one other user has this permission
				command="SELECT COUNT(*) FROM grouppermission WHERE PermType='"+POut.PInt((int)permType)+"'";
				DataTable table=General.GetTable(command);
				if(table.Rows[0][0].ToString()=="1"){//only one, so this would delete the last one.
					throw new Exception(Lan.g("FormSecurity","At least one group must have Security Admin permission."));
				}
			}
			command="DELETE from grouppermission WHERE UserGroupNum='"+POut.PInt(groupNum)+"' "
				+"AND PermType='"+POut.PInt((int)permType)+"'";
 			General.NonQ(command);
		}

		///<summary>Gets a GroupPermission based on the supplied userGroupNum and permType.  If not found, then it returns null.  Used in FormSecurity when double clicking on a dated permission or when clicking the all button.</summary>
		public static GroupPermission GetPerm(int userGroupNum,Permissions permType){
			for(int i=0;i<GroupPermissionC.List.Length;i++){
				if(GroupPermissionC.List[i].UserGroupNum==userGroupNum && GroupPermissionC.List[i].PermType==permType){
					return GroupPermissionC.List[i].Copy();
				}
			}
			return null;
		}

		///<summary>Used in Security.IsAuthorized</summary>
		public static bool HasPermission(int userGroupNum,Permissions permType){
			for(int i=0;i<GroupPermissionC.List.Length;i++){
				if(GroupPermissionC.List[i].UserGroupNum!=userGroupNum || GroupPermissionC.List[i].PermType!=permType){
					continue;
				}
				return true;
			}
			return false;
		}

		///<summary>Gets the description for the specified permisssion.  Already translated.</summary>
		public static string GetDesc(Permissions perm){
			switch(perm){
				case Permissions.Accounting:
					return Lan.g("enumPermissions","Accounting");
				case Permissions.AccountingCreate:
					return Lan.g("enumPermissions","Accounting Create Entry");
				case Permissions.AccountingEdit:
					return Lan.g("enumPermissions","Accounting Edit Entry");
				case Permissions.AccountModule:
					return Lan.g("enumPermissions","Account Module");
				case Permissions.AdjustmentCreate:
					return Lan.g("enumPermissions","Adjustment Create");
				case Permissions.AdjustmentEdit:
					return Lan.g("enumPermissions","Adjustment Edit");
				case Permissions.AppointmentCreate:
					return Lan.g("enumPermissions","Appointment Create");
				case Permissions.AppointmentEdit:
					return Lan.g("enumPermissions","Appointment Edit");
				case Permissions.AppointmentMove:
					return Lan.g("enumPermissions","Appointment Move");
				case Permissions.AppointmentsModule:
					return Lan.g("enumPermissions","Appointments Module");
				case Permissions.Backup:
					return Lan.g("enumPermissions","Backup");
				case Permissions.Blockouts:
					return Lan.g("enumPermissions","Blockouts");
				case Permissions.ChartModule:
					return Lan.g("enumPermissions","Chart Module");
				case Permissions.ChooseDatabase:
					return Lan.g("enumPermissions","Choose Database");
				case Permissions.ClaimSentEdit:
					return Lan.g("enumPermissions","Claim Sent Edit");
				case Permissions.DepositSlips:
					return Lan.g("enumPermissions","Deposit Slips");
				case Permissions.FamilyModule:
					return Lan.g("enumPermissions","Family Module");
				case Permissions.ImagesModule:
					return Lan.g("enumPermissions","Images Module");
				case Permissions.InsPayCreate:
					return Lan.g("enumPermissions","Insurance Payment Create");
				case Permissions.InsPayEdit:
					return Lan.g("enumPermissions","Insurance Payment Edit");
				case Permissions.ManageModule:
					return Lan.g("enumPermissions","Manage Module");
				case Permissions.None:
					return "";
				case Permissions.PaymentCreate:
					return Lan.g("enumPermissions","Payment Create");
				case Permissions.PaymentEdit:
					return Lan.g("enumPermissions","Payment Edit");
				case Permissions.ProcComplCreate:
					return Lan.g("enumPermissions","Create Completed Procedure (or set complete)");
				case Permissions.ProcComplEdit:
					return Lan.g("enumPermissions","Edit Completed Procedure");
				case Permissions.Reports:
					return Lan.g("enumPermissions","Reports");
				case Permissions.RxCreate:
					return Lan.g("enumPermissions","Rx Create");
				case Permissions.AnesthesiaIntakeMeds:
					return Lan.g("enumPermissions", "Intake Anesthetic Medications into Inventory");
				case Permissions.AnesthesiaControlMeds:
					return Lan.g("enumPermissions", "Edit Anesthetic Records; Edit/Adjust Inventory Counts");
				case Permissions.Schedules:
					return Lan.g("enumPermissions","Schedules - Practice and Provider");
				case Permissions.SecurityAdmin:
					return Lan.g("enumPermissions","Security Admin");
				case Permissions.Setup:
					return Lan.g("enumPermissions","Setup - Covers a wide variety of setup functions");
				case Permissions.TimecardsEditAll:
					return Lan.g("enumPermissions","Edit All Timecards");
				case Permissions.TPModule:
					return Lan.g("enumPermissions","TreatmentPlan Module");
				case Permissions.UserQuery:
					return Lan.g("enumPermissions","User Query");
			}
			return "";//should never happen
		}

		///<summary></summary>
		public static bool PermTakesDates(Permissions permType){
			if(  permType==Permissions.AdjustmentEdit
				|| permType==Permissions.PaymentEdit
				|| permType==Permissions.ProcComplEdit
				|| permType==Permissions.InsPayEdit
				|| permType==Permissions.ClaimSentEdit
				|| permType==Permissions.AccountingEdit
				|| permType==Permissions.AccountingCreate//prevents backdating
				|| permType==Permissions.DepositSlips//prevents backdating
				)
			{
				return true;
			}
			return false;
		}
		

	}
 
	

	
}













