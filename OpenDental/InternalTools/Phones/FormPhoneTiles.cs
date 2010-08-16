using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPhoneTiles:Form {
		private List<Phone> PhoneList;
		///<summary>When the GoToChanged event fires, this tells us which patnum.</summary>
		public long GotoPatNum;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;
		///<summary>This is the difference between server time and local computer time.  Used to ensure that times displayed are accurate to the second.  This value is usally just a few seconds, but possibly a few minutes.</summary>
		private TimeSpan timeDelta;
		private int msgCount;
		string pathPhoneMsg=@"\\192.168.0.197\Voicemail\default\998\INBOX";
		private PhoneTile selectedTile;
		private Thread workerThread;
		
		public FormPhoneTiles() {
			InitializeComponent();
		}

		private void FormPhoneTiles_Load(object sender,EventArgs e) {
			timerMain.Enabled=true;
			timeDelta=MiscData.GetNowDateTime()-DateTime.Now;
			PhoneTile tile;
			for(int i=0;i<21;i++) {
				tile=((PhoneTile)Controls.Find("phoneTile"+(i+1).ToString(),false)[0]);
				tile.GoToChanged += new System.EventHandler(this.phoneTile_GoToChanged);
				tile.SelectedTileChanged += new System.EventHandler(this.phoneTile_SelectedTileChanged);
				tile.MenuNumbers=menuNumbers;
				tile.MenuStatus=menuStatus;
			}
			FillTiles();
		}

		private void FormPhoneTiles_Shown(object sender,EventArgs e) {
			DateTime now=DateTime.Now;
			while(now.AddSeconds(1)>DateTime.Now) {
				Application.DoEvents();
			}
			timerMsgs.Enabled=true;
			//SetLabelMsg();
		}

		private void FillTiles() {
			PhoneList=Phones.GetPhoneList();
			PhoneTile tile;
			for(int i=0;i<21;i++) {
				tile=((PhoneTile)Controls.Find("phoneTile"+(i+1).ToString(),false)[0]);
				tile.TimeDelta=timeDelta;
				if(PhoneList.Count>i){
					tile.PhoneCur=PhoneList[i];
				}
				else{
					tile.PhoneCur=null;
				}
			}			
		}

		private void phoneTile_GoToChanged(object sender,EventArgs e) {
			PhoneTile tile=(PhoneTile)sender;
			if(tile.PhoneCur==null) {
				return;
			}
			if(tile.PhoneCur.PatNum==0) {
				return;
			}
			GotoPatNum=tile.PhoneCur.PatNum;
			OnGoToChanged();
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}


		private void phoneTile_SelectedTileChanged(object sender,EventArgs e) {
			selectedTile=(PhoneTile)sender;
		}

		private void timerMain_Tick(object sender,EventArgs e) {
			//every 1.6 seconds
			FillTiles();
		}

		//Phones.SetWebCamImage(intTest+101,(Bitmap)pictureWebCam.Image,PhoneList);

		private void butOverride_Click(object sender,EventArgs e) {
			FormPhoneOverrides FormO=new FormPhoneOverrides();
			FormO.ShowDialog();
		}

		private void timerMsgs_Tick(object sender,EventArgs e) {
			//every 3 sec.
			workerThread=new Thread(new ThreadStart(this.WorkerThread_SetLabelMsg));
			workerThread.Start();//It's done this way because the file activity tends to lock the UI on slow connections.
		}

		private delegate void DelegateSetString(String str,bool isBold,Color color);//typically at namespace level rather than class level

		///<summary>Always called using worker thread.</summary>
		private void WorkerThread_SetLabelMsg() {
			#if DEBUG
				//return;
			#endif
			string s;
			bool isBold;
			Color color;
			try {
				if(!Directory.Exists(pathPhoneMsg)) {
					s="msg path not found";
					isBold=false;
					color=Color.Black;
					this.Invoke(new DelegateSetString(SetString),new Object[] { s,isBold,color });
					return;
				}
				msgCount=Directory.GetFiles(pathPhoneMsg,"*.txt").Length;
				if(msgCount==0) {
					s="Phone Messages: 0";
					isBold=false;
					color=Color.Black;
					this.Invoke(new DelegateSetString(SetString),new Object[] { s,isBold,color });
				}
				else {
					s="Phone Messages: "+msgCount.ToString();
					isBold=true;
					color=Color.Firebrick;
					this.Invoke(new DelegateSetString(SetString),new Object[] { s,isBold,color });
				}
			}
			catch {
				//because this.Invoke will fail sometimes if the form is quickly closed and reopened because form handle has not yet been created.
			}
		}

		///<summary>Called from worker thread using delegate and Control.Invoke</summary>
		private void SetString(String str,bool isBold,Color color) {
			labelMsg.Text=str;
			if(isBold) {
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,10f,FontStyle.Bold);
			}
			else {
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Regular);
			}
			labelMsg.ForeColor=color;
		}

		private void menuItemManage_Click(object sender,EventArgs e) {
			//if(selectedTile.PhoneCur==null) {//already validated
			long patNum=selectedTile.PhoneCur.PatNum;
			if(patNum==0) {
				MsgBox.Show(this,"Please attach this number to a patient first.");
				return;
			}
			FormPhoneNumbersManage FormM=new FormPhoneNumbersManage();
			FormM.PatNum=patNum;
			FormM.ShowDialog();
		}

		private void menuItemAdd_Click(object sender,EventArgs e) {
			//if(selectedTile.PhoneCur==null) {//already validated
			if(selectedTile.PhoneCur.CustomerNumber=="") {
				MsgBox.Show(this,"No phone number present.");
				return;
			}
			long patNum=selectedTile.PhoneCur.PatNum;
			if(FormOpenDental.CurPatNum==0) {
				MsgBox.Show(this,"Please select a patient in the main window first.");
				return;
			}
			if(patNum!=0) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"The current number is already attached to a patient. Attach it to this patient instead?")) {
					return;
				}
				PhoneNumber ph=PhoneNumbers.GetByVal(selectedTile.PhoneCur.CustomerNumber);
				ph.PatNum=FormOpenDental.CurPatNum;
				PhoneNumbers.Update(ph);
			}
			else {
				string patName=Patients.GetLim(FormOpenDental.CurPatNum).GetNameLF();
				if(MessageBox.Show("Attach this phone number to "+patName+"?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
					return;
				}
				PhoneNumber ph=new PhoneNumber();
				ph.PatNum=FormOpenDental.CurPatNum;
				ph.PhoneNumberVal=selectedTile.PhoneCur.CustomerNumber;
				PhoneNumbers.Insert(ph);
			}
			//tell the phone server to refresh this row with the patient name and patnum
			DataValid.SetInvalid(InvalidType.PhoneNumbers);
		}

		//Timecards-------------------------------------------------------------------------------------

		private void menuItemAvailable_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetToDefaultRingGroups(extension,employeeNum);
			Phones.SetPhoneStatus(ClockStatusEnum.Available,extension);//green
			FillTiles();
		}

		private void menuItemTraining_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.Training,extension);
			FillTiles();
		}

		private void menuItemTeamAssist_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.TeamAssist,extension);
			FillTiles();
		}

		private void menuItemWrapUp_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.WrapUp,extension);
			//this is usually an automatic status
			FillTiles();
		}

		private void menuItemOfflineAssist_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Phones.SetPhoneStatus(ClockStatusEnum.OfflineAssist,extension);
			FillTiles();
		}

		private void menuItemUnavailable_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
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
			FillTiles();
		}

		//RingGroups---------------------------------------------------

		private void menuItemRinggroupAll_Click(object sender,EventArgs e) {
			//This even works if the person is still clocked out.
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.All);
		}

		private void menuItemRinggroupNone_Click(object sender,EventArgs e) {
			//This even works if the person is still clocked in.
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
		}

		private void menuItemRinggroupsDefault_Click(object sender,EventArgs e) {
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneAsterisks.SetToDefaultRingGroups(extension,employeeNum);
		}

		private void menuItemBackup_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.Backup);
			Phones.SetPhoneStatus(ClockStatusEnum.Backup,extension);
			FillTiles();
		}

		//Timecard---------------------------------------------------

		private void menuItemLunch_Click(object sender,EventArgs e) {
			//verify that employee is logged in as user
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)) {
				if(Security.CurUser.EmployeeNum!=employeeNum) {
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
						MsgBox.Show(this,"Not authorized.");
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
			FillTiles();
		}

		private void menuItemHome_Click(object sender,EventArgs e) {
			//verify that employee is logged in as user
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)) {
				if(Security.CurUser.EmployeeNum!=employeeNum) {
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
						MsgBox.Show(this,"Not authorized.");
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
			FillTiles();
		}

		private void menuItemBreak_Click(object sender,EventArgs e) {
			//verify that employee is logged in as user
			int extension=selectedTile.PhoneCur.Extension;
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)) {
				if(Security.CurUser.EmployeeNum!=employeeNum) {
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
						MsgBox.Show(this,"Not authorized.");
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
			FillTiles();
		}

		///<summary>If already clocked in, this does nothing.  Returns false if not able to clock in due to security, or true if successful.</summary>
		private bool ClockIn() {
			long employeeNum=selectedTile.PhoneCur.EmployeeNum;
			if(employeeNum==0) {
				MsgBox.Show(this,"No employee at that extension.");
				return false;
			}
			if(ClockEvents.IsClockedIn(employeeNum)) {
				return true;
			}
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)) {
				if(Security.CurUser.EmployeeNum!=employeeNum) {
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
						MsgBox.Show(this,"Not authorized.");
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
			EmpCur.ClockStatus=Lan.g(this,"Working"); ;
			Employees.Update(EmpCur);
			return true;
		}

		private void FormPhoneTiles_FormClosing(object sender,FormClosingEventArgs e) {
			if(workerThread!=null){
				workerThread.Abort();
			}
		}
		
		


	}

	
}
