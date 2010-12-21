using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class GroupPermissions {
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM grouppermission";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="GroupPermission";
			FillCache(table);
			return table;
		}

		private static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			GroupPermissionC.List=new GroupPermission[table.Rows.Count];
			for(int i=0;i<GroupPermissionC.List.Length;i++) {
				GroupPermissionC.List[i]=new GroupPermission();
				GroupPermissionC.List[i].GroupPermNum  = PIn.Long(table.Rows[i][0].ToString());
				GroupPermissionC.List[i].NewerDate     = PIn.Date(table.Rows[i][1].ToString());
				GroupPermissionC.List[i].NewerDays     = PIn.Int(table.Rows[i][2].ToString());
				GroupPermissionC.List[i].UserGroupNum  = PIn.Long(table.Rows[i][3].ToString());
				GroupPermissionC.List[i].PermType      =(Permissions)PIn.Long(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		private static void Update(GroupPermission gp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),gp);
				return;
			}
			Crud.GroupPermissionCrud.Update(gp);
		}

		///<summary></summary>
		private static long Insert(GroupPermission gp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				gp.GroupPermNum=Meth.GetLong(MethodBase.GetCurrentMethod(),gp);
				return gp.GroupPermNum;
			}
			return Crud.GroupPermissionCrud.Insert(gp);
		}

		///<summary></summary>
		public static void InsertOrUpdate(GroupPermission gp, bool isNew){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),gp,isNew);
				return;
			}
			if(gp.NewerDate.Year>1880 && gp.NewerDays>0){
				throw new Exception(Lans.g("GroupPermissions","Date or days can be set, but not both."));
			}
			if(!GroupPermissions.PermTakesDates(gp.PermType)){
				if(gp.NewerDate.Year>1880 || gp.NewerDays>0){
					throw new Exception(Lans.g("GroupPermissions","This type of permission may not have a date or days set."));
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
		public static void RemovePermission(long groupNum,Permissions permType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),groupNum,permType);
				return;
			}
			string command;
			if(permType==Permissions.SecurityAdmin){
				//need to make sure that at least one other user has this permission
				command="SELECT COUNT(*) FROM grouppermission WHERE PermType='"+POut.Long((int)permType)+"'";
				DataTable table=Db.GetTable(command);
				if(table.Rows[0][0].ToString()=="1"){//only one, so this would delete the last one.
					throw new Exception(Lans.g("FormSecurity","At least one group must have Security Admin permission."));
				}
			}
			command="DELETE from grouppermission WHERE UserGroupNum='"+POut.Long(groupNum)+"' "
				+"AND PermType='"+POut.Long((int)permType)+"'";
 			Db.NonQ(command);
		}

		///<summary>Gets a GroupPermission based on the supplied userGroupNum and permType.  If not found, then it returns null.  Used in FormSecurity when double clicking on a dated permission or when clicking the all button.</summary>
		public static GroupPermission GetPerm(long userGroupNum,Permissions permType) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<GroupPermissionC.List.Length;i++){
				if(GroupPermissionC.List[i].UserGroupNum==userGroupNum && GroupPermissionC.List[i].PermType==permType){
					return GroupPermissionC.List[i].Copy();
				}
			}
			return null;
		}

		///<summary>Used in Security.IsAuthorized</summary>
		public static bool HasPermission(long userGroupNum,Permissions permType){
			//No need to check RemotingRole; no call to db.
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
			//No need to check RemotingRole; no call to db.
			switch(perm){
				case Permissions.Accounting:
					return Lans.g("enumPermissions","Accounting");
				case Permissions.AccountingCreate:
					return Lans.g("enumPermissions","Accounting Create Entry");
				case Permissions.AccountingEdit:
					return Lans.g("enumPermissions","Accounting Edit Entry");
				case Permissions.AccountModule:
					return Lans.g("enumPermissions","Account Module");
				case Permissions.AdjustmentCreate:
					return Lans.g("enumPermissions","Adjustment Create");
				case Permissions.AdjustmentEdit:
					return Lans.g("enumPermissions","Adjustment Edit");
				case Permissions.AnesthesiaIntakeMeds:
					return Lans.g("enumPermissions","Intake Anesthetic Medications into Inventory");
				case Permissions.AnesthesiaControlMeds:
					return Lans.g("enumPermissions","Edit Anesthetic Records; Edit/Adjust Inventory Counts");
				case Permissions.AppointmentCreate:
					return Lans.g("enumPermissions","Appointment Create");
				case Permissions.AppointmentEdit:
					return Lans.g("enumPermissions","Appointment Edit");
				case Permissions.AppointmentMove:
					return Lans.g("enumPermissions","Appointment Move");
				case Permissions.AppointmentsModule:
					return Lans.g("enumPermissions","Appointments Module");
				case Permissions.Backup:
					return Lans.g("enumPermissions","Backup");
				case Permissions.Blockouts:
					return Lans.g("enumPermissions","Blockouts");
				case Permissions.ChartModule:
					return Lans.g("enumPermissions","Chart Module");
				case Permissions.ChooseDatabase:
					return Lans.g("enumPermissions","Choose Database");
				case Permissions.ClaimSentEdit:
					return Lans.g("enumPermissions","Claim Sent Edit");
				case Permissions.CommlogEdit:
					return Lans.g("enumPermissions","Commlog Edit");
				case Permissions.DepositSlips:
					return Lans.g("enumPermissions","Deposit Slips");
				case Permissions.EquipmentDelete:
					return Lans.g("enumPermissions","Equipment Delete");
				case Permissions.FamilyModule:
					return Lans.g("enumPermissions","Family Module");
				case Permissions.ImageDelete:
					return Lans.g("enumPermissions","Image Delete");
				case Permissions.ImagesModule:
					return Lans.g("enumPermissions","Images Module");
				case Permissions.InsPayCreate:
					return Lans.g("enumPermissions","Insurance Payment Create");
				case Permissions.InsPayEdit:
					return Lans.g("enumPermissions","Insurance Payment Edit");
				case Permissions.ManageModule:
					return Lans.g("enumPermissions","Manage Module");
				case Permissions.None:
					return "";
				case Permissions.PaymentCreate:
					return Lans.g("enumPermissions","Payment Create");
				case Permissions.PaymentEdit:
					return Lans.g("enumPermissions","Payment Edit");
				case Permissions.ProcComplCreate:
					return Lans.g("enumPermissions","Create Completed Procedure (or set complete)");
				case Permissions.ProcComplEdit:
					return Lans.g("enumPermissions","Edit Completed Procedure");
				case Permissions.Reports:
					return Lans.g("enumPermissions","Reports");
				case Permissions.RxCreate:
					return Lans.g("enumPermissions","Rx Create");
				case Permissions.Schedules:
					return Lans.g("enumPermissions","Schedules - Practice and Provider");
				case Permissions.SecurityAdmin:
					return Lans.g("enumPermissions","Security Admin");
				case Permissions.Setup:
					return Lans.g("enumPermissions","Setup - Covers a wide variety of setup functions");
				case Permissions.SheetEdit:
					return Lans.g("enumPermissions","Sheet Edit");
				case Permissions.TimecardDeleteEntry:
					return Lans.g("enumPermissions","Timecard Delete Entry");
				case Permissions.TimecardsEditAll:
					return Lans.g("enumPermissions","Edit All Timecards");
				case Permissions.TPModule:
					return Lans.g("enumPermissions","TreatmentPlan Module");
				case Permissions.TreatPlanEdit:
					return Lans.g("enumPermissions","Edit Treatment Plan");
				case Permissions.UserQuery:
					return Lans.g("enumPermissions","User Query");
				case Permissions.ReportProdInc:
					return Lans.g("enumPermissions","Reports - Production and Income, Aging");
			}
			return "";//should never happen
		}

		///<summary></summary>
		public static bool PermTakesDates(Permissions permType){
			//No need to check RemotingRole; no call to db.
			if(  permType==Permissions.AdjustmentEdit
				|| permType==Permissions.PaymentEdit
				|| permType==Permissions.ProcComplEdit
				|| permType==Permissions.InsPayEdit
				|| permType==Permissions.ClaimSentEdit
				|| permType==Permissions.AccountingEdit
				|| permType==Permissions.AccountingCreate//prevents backdating
				|| permType==Permissions.DepositSlips//prevents backdating
				|| permType==Permissions.TreatPlanEdit
				|| permType==Permissions.TimecardDeleteEntry
				|| permType==Permissions.EquipmentDelete
				|| permType==Permissions.CommlogEdit
				)
			{
				return true;
			}
			return false;
		}
		

	}
 
	

	
}













