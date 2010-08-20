using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public class PhoneUI {
		private static string langThis="Phone";

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

		public static void Available(PhoneTile tile) {
			if(!ClockIn(tile)) {
				return;
			}
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetToDefaultRingGroups(extension,employeeNum);
			Phones.SetPhoneStatus(ClockStatusEnum.Available,extension);//green
		}

		public static void Training(PhoneTile tile) {
			if(!ClockIn(tile)) {
				return;
			}
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.Training,extension);
		}

		public static void TeamAssist(PhoneTile tile) {
			if(!ClockIn(tile)) {
				return;
			}
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.TeamAssist,extension);
		}

		public static void WrapUp(PhoneTile tile) {
			if(!ClockIn(tile)) {
				return;
			}
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.WrapUp,extension);
			//this is usually an automatic status
		}

		public static void OfflineAssist(PhoneTile tile) {
			if(!ClockIn(tile)) {
				return;
			}
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.OfflineAssist,extension);
		}

		public static void Unavailable(PhoneTile tile) {
			if(!ClockIn(tile)) {
				return;
			}
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			//Employees.SetUnavailable(extension,employeeNum);
			//Get an override if it exists
			PhoneOverride phoneOR=PhoneOverrides.GetByExtAndEmp(extension,employeeNum);
			if(phoneOR==null) {//there is no override for that extension/emp combo.
				phoneOR=new PhoneOverride();
				phoneOR.EmpCurrent=employeeNum;
				phoneOR.Extension=extension;
				phoneOR.IsAvailable=false;
				FormPhoneOverrideEdit FormO=new FormPhoneOverrideEdit();
				FormO.phoneCur=phoneOR;
				FormO.IsNew=true;
				FormO.ForceUnAndExplanation=true;
				FormO.ShowDialog();
				if(FormO.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else {
				phoneOR.IsAvailable=false;
				FormPhoneOverrideEdit FormO=new FormPhoneOverrideEdit();
				FormO.phoneCur=phoneOR;
				FormO.ForceUnAndExplanation=true;
				FormO.ShowDialog();
				if(FormO.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			//this is now handled within PhoneOverrides.Insert or PhoneOverrides.Update
			//Employees.SetPhoneStatus("Unavailable",extension);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
		}

		//RingGroups---------------------------------------------------

		public static void RinggroupAll(PhoneTile tile) {
			//This even works if the person is still clocked out.
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.All);
		}

		public static void RinggroupNone(PhoneTile tile) {
			//This even works if the person is still clocked in.
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
		}

		public static void RinggroupsDefault(PhoneTile tile) {
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneAsterisks.SetToDefaultRingGroups(extension,employeeNum);
		}

		public static void Backup(PhoneTile tile) {
			if(!ClockIn(tile)) {
				return;
			}
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.Backup);
			Phones.SetPhoneStatus(ClockStatusEnum.Backup,extension);
		}

		//Timecard---------------------------------------------------

		public static void Lunch(PhoneTile tile) {
			//verify that employee is logged in as user
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)) {
				if(Security.CurUser.EmployeeNum!=employeeNum) {
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
						MsgBox.Show(langThis,"Not authorized.");
						return;
					}
				}
			}
			try {
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Lunch);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			PhoneOverrides.SetAvailable(extension,employeeNum);
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Lunch.ToString());
			Employees.Update(EmpCur);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.Lunch,extension);
		}

		public static void Home(PhoneTile tile) {
			//verify that employee is logged in as user
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)) {
				if(Security.CurUser.EmployeeNum!=employeeNum) {
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
						MsgBox.Show(langThis,"Not authorized.");
						return;
					}
				}
			}
			try {
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Home);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			PhoneOverrides.SetAvailable(extension,employeeNum);
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Home.ToString());
			Employees.Update(EmpCur);
			//ModuleSelected(PatCurNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.Home,extension);
		}

		public static void Break(PhoneTile tile) {
			//verify that employee is logged in as user
			int extension=tile.PhoneCur.Extension;
			long employeeNum=tile.PhoneCur.EmployeeNum;
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)) {
				if(Security.CurUser.EmployeeNum!=employeeNum) {
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
						MsgBox.Show(langThis,"Not authorized.");
						return;
					}
				}
			}
			try {
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Break);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			PhoneOverrides.SetAvailable(extension,employeeNum);
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Break.ToString());
			Employees.Update(EmpCur);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.Break,extension);
		}

		///<summary>If already clocked in, this does nothing.  Returns false if not able to clock in due to security, or true if successful.</summary>
		private static bool ClockIn(PhoneTile tile) {
			long employeeNum=tile.PhoneCur.EmployeeNum;
			if(employeeNum==0) {
				MsgBox.Show(langThis,"No employee at that extension.");
				return false;
			}
			if(ClockEvents.IsClockedIn(employeeNum)) {
				return true;
			}
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)) {
				if(Security.CurUser.EmployeeNum!=employeeNum) {
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
						MsgBox.Show(langThis,"Not authorized.");
						return false;
					}
				}
			}
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







	}
}
