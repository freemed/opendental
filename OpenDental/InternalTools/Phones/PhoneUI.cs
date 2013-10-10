using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public class PhoneUI {
		private static string langThis="Phone";
		public static string PathPhoneMsg=@"\\10.10.1.197\Voicemail\default\998\INBOX";

		public static void Manage(PhoneTile tile){
			//if(selectedTile.PhoneCur==null) {//already validated
			long patNum=tile.PhoneCur.PatNum;
			if(patNum==0) {
				MsgBox.Show(langThis,"Please attach this number to a patient first.");
				return;
			}
			FormPhoneNumbersManage FormM=new FormPhoneNumbersManage();
			FormM.PatNum=patNum;
			FormM.ShowDialog();
		}

		public static void Add(PhoneTile tile){
			//if(selectedTile.PhoneCur==null) {//already validated
			if(tile.PhoneCur.CustomerNumber=="") {
				MsgBox.Show(langThis,"No phone number present.");
				return;
			}
			long patNum=tile.PhoneCur.PatNum;
			if(FormOpenDental.CurPatNum==0) {
				MsgBox.Show(langThis,"Please select a patient in the main window first.");
				return;
			}
			if(patNum!=0) {
				MsgBox.Show(langThis,"The current number is already attached to a different customer.");
				return;
				//if(!MsgBox.Show(langThis,MsgBoxButtons.OKCancel,"The current number is already attached to a patient. Attach it to this patient instead?")) {
				//	return;
				//}
				//This crashes because we don't actually know what the number is.  Enhance later by storing actual number in phone grid.
				//PhoneNumber ph=PhoneNumbers.GetByVal(tile.PhoneCur.CustomerNumber);
				//ph.PatNum=FormOpenDental.CurPatNum;
				//PhoneNumbers.Update(ph);
			}
			else {
				string patName=Patients.GetLim(FormOpenDental.CurPatNum).GetNameLF();
				if(MessageBox.Show("Attach this phone number to "+patName+"?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
					return;
				}
				PhoneNumber ph=new PhoneNumber();
				ph.PatNum=FormOpenDental.CurPatNum;
				ph.PhoneNumberVal=tile.PhoneCur.CustomerNumber;
				PhoneNumbers.Insert(ph);
			}
			//tell the phone server to refresh this row with the patient name and patnum
			DataValid.SetInvalid(InvalidType.PhoneNumbers);
		}

		///<summary>If this is Security.CurUser's tile then ClockIn. If it is someone else's tile then allow the single case of switching from NeedsHelp to Available.</summary>
		public static void Available(PhoneTile tile) {
			long employeeNum=Security.CurUser.EmployeeNum;
			if(Security.CurUser.EmployeeNum!=tile.PhoneCur.EmployeeNum) { //We are on someone else's tile. So Let's do some checks before we assume we can take over this extension.				
				if(tile.PhoneCur.ClockStatus==ClockStatusEnum.NeedsHelp) { 
					//Allow the specific state where we are changing their status back from NeedsHelp to Available.
					//This does not require any security permissions as any tech in can perform this action on behalf of any other tech.
					Phones.SetPhoneStatus(ClockStatusEnum.Available,tile.PhoneCur.Extension,tile.PhoneCur.EmployeeNum);//green
					return;
				}
				//We are on a tile that is not our own
				//If another employee is occupying this extension then assume we are trying to change that employee's status back to available.
				if(ClockEvents.IsClockedIn(tile.PhoneCur.EmployeeNum)) { //This tile is taken by an employee who is clocked in.					
					//Transition the employee back to available.
					ChangeTileStatus(tile,ClockStatusEnum.Available);
					return;
				}
				if(tile.PhoneCur.ClockStatus!=ClockStatusEnum.None	//The other person is still actively using this extension.
					&& tile.PhoneCur.ClockStatus!=ClockStatusEnum.Home) {
					MsgBox.Show(langThis,"Cannot take over this extension as it is currently occuppied by someone who is likely on Break or Lunch.");			
					return;
				}			
				//If another employee is NOT occupying this extension then assume we are trying clock in at this extension.
				if(ClockEvents.IsClockedIn(employeeNum)) { //We are already clocked in at a different extension.
					MsgBox.Show(langThis,"You are already clocked in at a different extension.  You must clock out of the current extension you are logged into before moving to another extension.");
					return;
				}
				//We got this far so fall through and allow user to clock in.
			}
			//We go here so all of our checks passed and we may login at this extension
			if(!ClockIn(tile)) { //Clock in on behalf of yourself
				return;
			}
			//Update the Phone tables accordingly.
			PhoneEmpDefaults.SetAvailable(tile.PhoneCur.Extension,employeeNum);
			PhoneAsterisks.SetToDefaultRingGroups(tile.PhoneCur.Extension,employeeNum);
			Phones.SetPhoneStatus(ClockStatusEnum.Available,tile.PhoneCur.Extension,employeeNum);//green
		}

		public static void Training(PhoneTile tile) {
			ChangeTileStatus(tile,ClockStatusEnum.Training);
		}		

		public static void TeamAssist(PhoneTile tile) {
			ChangeTileStatus(tile,ClockStatusEnum.TeamAssist);
		}

		public static void NeedsHelp(PhoneTile tile) {
			ChangeTileStatus(tile,ClockStatusEnum.NeedsHelp);
		}

		public static void WrapUp(PhoneTile tile) { //this is usually an automatic status
			ChangeTileStatus(tile,ClockStatusEnum.WrapUp);			
		}

		public static void OfflineAssist(PhoneTile tile) {
			ChangeTileStatus(tile,ClockStatusEnum.OfflineAssist);
		}

		public static void Unavailable(PhoneTile tile) {
			if(!ClockEvents.IsClockedIn(Security.CurUser.EmployeeNum)) { //Employee performing the action must be clocked in.
				MsgBox.Show("PhoneUI","You must clock in before completing this action.");
				return;
			}
			if(!ClockEvents.IsClockedIn(tile.PhoneCur.EmployeeNum)) { //Employee having action performed must be clocked in.
				MessageBox.Show(Lan.g("PhoneUI","Target employee must be clocked in before setting this status: ")+tile.PhoneCur.EmployeeName);
				return;
			}
			if(!CheckUserCanChangeStatus(tile)) {
				return;
			}
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;			
			PhoneEmpDefault ped=PhoneEmpDefaults.GetByExtAndEmp(extension,employeeNum);
			if(ped==null) {
				MessageBox.Show("PhoneEmpDefault (employee setting row) not found for Extension "+extension.ToString()+" and EmployeeNum "+employeeNum.ToString());
				return;
			}
			FormPhoneEmpDefaultEdit formPED=new FormPhoneEmpDefaultEdit();
			formPED.PedCur=ped;
			formPED.PedCur.StatusOverride=PhoneEmpStatusOverride.Unavailable;
			if(formPED.ShowDialog()==DialogResult.OK && formPED.PedCur.StatusOverride==PhoneEmpStatusOverride.Unavailable) {
				//This phone status update can get skipped from within the editor if the employee is not clocked in.
				//This would be the case when you are setting an employee other than yourself to Unavailable.
				//So we will set it here. This keeps the phone table and phone panel in sync.
				Phones.SetPhoneStatus(ClockStatusEnum.Unavailable,formPED.PedCur.PhoneExt,formPED.PedCur.EmployeeNum);			
			}
		}

		/// <summary>As per Nathan, changing status should set your ring group to None (not Backup as you might think). We are abandoning the Backup ring group for now.</summary>
		public static void Backup(PhoneTile tile) {
			ChangeTileStatus(tile,ClockStatusEnum.Backup);
			PhoneAsterisks.SetRingGroups(tile.PhoneCur.Extension,AsteriskRingGroups.None);
		}

		//RingGroups---------------------------------------------------

		public static void RinggroupAll(PhoneTile tile) {
			if(!CheckUserCanChangeStatus(tile)) {
				return;
			}
			PhoneAsterisks.SetRingGroups(tile.PhoneCur.Extension,AsteriskRingGroups.All);
		}

		public static void RinggroupNone(PhoneTile tile) {
			if(!CheckUserCanChangeStatus(tile)) {
				return;
			}
			PhoneAsterisks.SetRingGroups(tile.PhoneCur.Extension,AsteriskRingGroups.None);
		}

		public static void RinggroupsDefault(PhoneTile tile) {
			if(!CheckUserCanChangeStatus(tile)) {
				return;
			}
			PhoneAsterisks.SetToDefaultRingGroups(tile.PhoneCur.Extension,tile.PhoneCur.EmployeeNum);
		}

		///<summary>As of 10/9/13 Nathan wants backup ringgroup to go to 'None'. We may go back to using 'Backup' at some point, but for now it is not necessary so just set them to None.</summary>
		public static void RinggroupsBackup(PhoneTile tile) {
			if(!CheckUserCanChangeStatus(tile)) {
				return;
			}
			PhoneAsterisks.SetRingGroups(tile.PhoneCur.Extension,AsteriskRingGroups.Backup);
		}

		//Timecard---------------------------------------------------

		public static void Lunch(PhoneTile tile) {
			//verify that employee is logged in as user
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			if(!CheckUserCanChangeStatus(tile)) {
				return;
			}
			try {
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Lunch);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			PhoneEmpDefaults.SetAvailable(extension,employeeNum);
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Lunch.ToString());
			Employees.Update(EmpCur);
			Phones.SetPhoneStatus(ClockStatusEnum.Lunch,extension);
		}

		public static void Home(PhoneTile tile) {
			//verify that employee is logged in as user
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			if(!CheckUserCanChangeStatus(tile)) {
				return;
			}
			try { //Update the clock event, phone (HQ only), and phone emp default (HQ only).
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Home);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Home.ToString());
			Employees.Update(EmpCur);
		}

		public static void Break(PhoneTile tile) {
			//verify that employee is logged in as user
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			if(!CheckUserCanChangeStatus(tile)) {
				return;
			}
			try {
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Break);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			PhoneEmpDefaults.SetAvailable(extension,employeeNum);
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Break.ToString());
			Employees.Update(EmpCur);
			Phones.SetPhoneStatus(ClockStatusEnum.Break,extension);
		}

		///<summary>If already clocked in, this does nothing.  Returns false if not able to clock in due to security, or true if successful.</summary>
		private static bool ClockIn(PhoneTile tile) {
			long employeeNum=Security.CurUser.EmployeeNum;//tile.PhoneCur.EmployeeNum;
			if(employeeNum==0) {//Can happen if logged in as 'admin' user (employeeNum==0). Otherwise should not happen, means the employee trying to clock doesn't exist in the employee table.
				MsgBox.Show(langThis,"Inavlid OD User: "+Security.CurUser.UserName);
				return false;
			}
			if(ClockEvents.IsClockedIn(employeeNum)) {
			  return true;
			}
			//We no longer need to check passwords here because the user HAS to be logged in and physically sitting at the computer.
			/*if(Security.CurUser.EmployeeNum!=employeeNum) {
				if(!Security.IsAuthorized(Permissions.TimecardsEditAll,true)) {
					if(!CheckSelectedUserPassword(employeeNum)) {
						return false;
					}
				}
			}*/
			try {
				ClockEvents.ClockIn(employeeNum);
			}
			catch {
				//This should never happen.  Fail silently.
				return true;
			}
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus="Working";
			Employees.Update(EmpCur);
			return true;
		}

		///<summary>Will ask for password if the current user logged in isn't the user status being manipulated.</summary>
		private static bool CheckSelectedUserPassword(long employeeNum) {
			if(Security.CurUser.EmployeeNum==employeeNum) {
				return true;
			}
			Userod selectedUser=Userods.GetUserByEmployeeNum(employeeNum);
			InputBox inputPass=new InputBox("Please enter password:");
			inputPass.textResult.PasswordChar='*';
			inputPass.ShowDialog();
			if(inputPass.DialogResult!=DialogResult.OK) {
				return false;
			}
			if(!Userods.CheckTypedPassword(inputPass.textResult.Text,selectedUser.Password)) {
				MsgBox.Show("PhoneUI","Wrong password.");
				return false;
			}
			return true;
		}

		///<summary>Verify... 1) Security.CurUser is clocked in. 2) Target status change employee is clocked in. 3) Secruity.CurUser has TimecardsEditAll permission.</summary>
		private static bool ChangeTileStatus(PhoneTile tile,ClockStatusEnum newClockStatus) {
			if(!ClockEvents.IsClockedIn(Security.CurUser.EmployeeNum)) { //Employee performing the action must be clocked in.
				MsgBox.Show(langThis,"You must clock in before completing this action.");
				return false;
			}
			if(!ClockEvents.IsClockedIn(tile.PhoneCur.EmployeeNum)) { //Employee having action performed must be clocked in.
				MessageBox.Show(Lan.g(langThis,"Target employee must be clocked in before setting this status: ")+tile.PhoneCur.EmployeeName);
				return false;
			}
			if(!CheckUserCanChangeStatus(tile)) {
				return false;
			}
			PhoneEmpDefaults.SetAvailable(tile.PhoneCur.Extension,tile.PhoneCur.EmployeeNum);
			PhoneAsterisks.SetToDefaultRingGroups(tile.PhoneCur.Extension,tile.PhoneCur.EmployeeNum);
			Phones.SetPhoneStatus(newClockStatus,tile.PhoneCur.Extension);
			return true;
		}

		///<summary>Verify Security.CurUser is allowed to change this tile's status.</summary>
		private static bool CheckUserCanChangeStatus(PhoneTile tile) {
			if(Security.CurUser.EmployeeNum==tile.PhoneCur.EmployeeNum) { //User is changing their own tile. This is always allowed.
				return true;
			}
			if(Security.IsAuthorized(Permissions.TimecardsEditAll,true)) { //User has time card edit permission so allow it.
				return true;
			}
			//User must enter target tile's password correctly.
			return CheckSelectedUserPassword(tile.PhoneCur.EmployeeNum);
		}
		




	}
}
