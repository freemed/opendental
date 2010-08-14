using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class UserControlPhonePanel:UserControl {
		DataTable tablePhone;
		///<summary>When the GoToChanged event fires, this tells us which patnum.</summary>
		public long GotoPatNum;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;
		private int rowI;
		private int colI;
		///<summary>This is the difference between server time and local computer time.  Used to ensure that times displayed are accurate to the second.  This value is usally just a few seconds, but possibly a few minutes.</summary>
		private TimeSpan timeDelta;
		private int msgCount;
		string pathPhoneMsg=@"\\192.168.0.197\Voicemail\default\998\INBOX";

		public UserControlPhonePanel() {
			InitializeComponent();
		}

		private void UserControlPhonePanel_Load(object sender,EventArgs e) {
			timer1.Enabled=true;
			timerMsgs.Enabled=true;
			SetLabelMsg();
			timeDelta=MiscData.GetNowDateTime()-DateTime.Now;
			FillEmps();
			//look for phone messages
			/*
			string pathPhoneMsg=@"\\asterisk\Voicemail\default\998\INBOX";
			if(!Directory.Exists(pathPhoneMsg)) {
				MessageBox.Show("Could not find voicemail path: "+pathPhoneMsg);
				return;
			}
			FileSystemWatcher watcher=new FileSystemWatcher(pathPhoneMsg,"*.txt");
			watcher.Created += new FileSystemEventHandler(OnCreated);
			watcher.Deleted +=new FileSystemEventHandler(OnDeleted);
			watcher.EnableRaisingEvents=true;
			//set initial value
			msgCount=Directory.GetFiles(pathPhoneMsg,"*.txt").Length;
			SetLabelMsg();*/
		}

		/*
		private void OnCreated(object source,FileSystemEventArgs e) {
			msgCount++;
			SetLabelMsg();
		}

		private void OnDeleted(object source,FileSystemEventArgs e) {
			msgCount--;
			SetLabelMsg();
		}*/

		private void SetLabelMsg() {
			#if DEBUG
				return;
			#endif
			if(!Directory.Exists(pathPhoneMsg)) {
				labelMsg.Text="msg path not found";
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Regular);
				labelMsg.ForeColor=Color.Black;
				return;
			}
			msgCount=Directory.GetFiles(pathPhoneMsg,"*.txt").Length;
			if(msgCount==0) {
				labelMsg.Text="Phone Messages: 0";
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Regular);
				labelMsg.ForeColor=Color.Black;
			}
			else {
				labelMsg.Text="Phone Messages: "+msgCount.ToString();
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,10f,FontStyle.Bold);
				labelMsg.ForeColor=Color.Firebrick;
			}
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}

		private void FillEmps(){
			gridEmp.BeginUpdate();
			gridEmp.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TableEmpClock","Ext"),25);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Employee"),60);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Status"),80);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Phone"),50);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","InOut"),35);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Customer"),90);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Time"),70);
			gridEmp.Columns.Add(col);
			gridEmp.Rows.Clear();
			UI.ODGridRow row;
			tablePhone=Employees.GetPhoneTable();
			DateTime dateTimeStart;
			TimeSpan span;
			DateTime timeOfDay;//because TimeSpan does not have good formatting.
			for(int i=0;i<tablePhone.Rows.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(tablePhone.Rows[i]["Extension"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["EmployeeName"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["ClockStatus"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["Description"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["InOrOut"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["CustomerNumber"].ToString());
				dateTimeStart=PIn.DateT(tablePhone.Rows[i]["DateTimeStart"].ToString());
				if(dateTimeStart.Date==DateTime.Today){
					span=DateTime.Now-dateTimeStart+timeDelta;
					timeOfDay=DateTime.Today+span;
					/*if(tablePhone.Rows[i]["Description"].ToString()==""){//Idle
						if(span<TimeSpan.FromSeconds(30)){//if the phone has been idle for less than 30 seconds
							row.Cells.Add(timeOfDay.ToString("H:mm:ss"));
						}
						else{
							row.Cells.Add("");
						}
					}
					else{*/
						row.Cells.Add(timeOfDay.ToString("H:mm:ss"));
					//}
				}
				else{
					row.Cells.Add("");
				}
				row.ColorBackG=Color.FromArgb(PIn.Int(tablePhone.Rows[i]["ColorBar"].ToString()));
				row.ColorText=Color.FromArgb(PIn.Int(tablePhone.Rows[i]["ColorText"].ToString()));
				gridEmp.Rows.Add(row);
			}
			gridEmp.EndUpdate();
			gridEmp.SetSelected(false);
		}

		/*private void FillMetrics(){
			gridMetrics.BeginUpdate();
			gridMetrics.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TablePhoneMetrics","Description"),40);
			gridMetrics.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePhoneMetrics","#"),60);
			gridMetrics.Columns.Add(col);
			gridMetrics.Rows.Clear();
			UI.ODGridRow row;
			tablePhone=Employees.GetPhoneMetricTable();
			for(int i=0;i<tablePhone.Rows.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(tablePhone.Rows[i]["Description"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["MetricVal"].ToString());
				row.ColorText=Color.FromArgb(PIn.PInt(tablePhone.Rows[i]["ColorText"].ToString()));
				gridMetrics.Rows.Add(row);
			}
			gridMetrics.EndUpdate();
			gridMetrics.SetSelected(false);
		}*/

		private void timer1_Tick(object sender,EventArgs e) {
			//For now, happens once per 1.6 seconds regardless of phone activity.
			//This might need improvement.
			FillEmps();
		}

		private void timerMsgs_Tick(object sender,EventArgs e) {
			//every 3 sec.
			SetLabelMsg();
		}

		private void butOverride_Click(object sender,EventArgs e) {
			FormPhoneOverrides FormO=new FormPhoneOverrides();
			FormO.ShowDialog();
		}

		private void gridEmp_CellClick(object sender,ODGridClickEventArgs e) {
			if((e.Button & MouseButtons.Right)==MouseButtons.Right){
				return;
			}
			long patNum=PIn.Long(tablePhone.Rows[e.Row]["PatNum"].ToString());
			GotoPatNum=patNum;
			OnGoToChanged();
		}

		private void menuItemManage_Click(object sender,EventArgs e) {
			long patNum=PIn.Long(tablePhone.Rows[rowI]["PatNum"].ToString());
			if(patNum==0){
				MsgBox.Show(this,"Please attach this number to a patient first.");
				return;
			}
			FormPhoneNumbersManage FormM=new FormPhoneNumbersManage();
			FormM.PatNum=patNum;
			FormM.ShowDialog();
		}

		private void menuItemAdd_Click(object sender,EventArgs e) {
			if(FormOpenDental.CurPatNum==0){
				MsgBox.Show(this,"Please select a patient in the main window first.");
				return;
			}
			if(tablePhone.Rows[rowI]["PatNum"].ToString()!="0") {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"The current number is already attached to a patient. Attach it to this patient instead?")) {
					return;
				}
				PhoneNumber ph=PhoneNumbers.GetByVal(tablePhone.Rows[rowI]["CustomerNumber"].ToString());
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
				ph.PhoneNumberVal=tablePhone.Rows[rowI]["CustomerNumber"].ToString();
				PhoneNumbers.Insert(ph);
			}
			//tell the phone server to refresh this row with the patient name and patnum
			DataValid.SetInvalid(InvalidType.PhoneNumbers);
		}

		private void gridEmp_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button!=MouseButtons.Right) {
				return;
			}
			rowI=gridEmp.PointToRow(e.Y);
			colI=gridEmp.PointToCol(e.X);
			if(rowI==-1){
				return;
			}
			if(colI==5){
				menuNumbers.Show(gridEmp,e.Location);
			}
			if(colI==2){
				menuStatus.Show(gridEmp,e.Location);
			}		
		}

		private void menuItemAvailable_Click(object sender,EventArgs e) {
			if(!ClockIn()){
				return;
			}
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetToDefaultRingGroups(extension,employeeNum);
			Employees.SetPhoneStatus("Available",extension);//green
			FillEmps();
		}

		private void menuItemTraining_Click(object sender,EventArgs e) {
			if(!ClockIn()){
				return;
			}
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Employees.SetPhoneStatus("Training",extension);
			FillEmps();
		}

		private void menuItemTeamAssist_Click(object sender,EventArgs e) {
			if(!ClockIn()){
				return;
			}
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Employees.SetPhoneStatus("TeamAssist",extension);
			FillEmps();
		}

		private void menuItemWrapUp_Click(object sender,EventArgs e) {
			if(!ClockIn()){
				return;
			}
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Employees.SetPhoneStatus("WrapUp",extension);
			//this is usually an automatic status
			FillEmps();
		}

		private void menuItemOfflineAssist_Click(object sender,EventArgs e) {
			if(!ClockIn()){
				return;
			}
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Employees.SetPhoneStatus("OfflineAssist",extension);
			FillEmps();
		}

		private void menuItemUnavailable_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
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
			FillEmps();
		}

		//RingGroups---------------------------------------------------

		private void menuItemRinggroupAll_Click(object sender,EventArgs e) {
			//This even works if the person is still clocked out.
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.All);
		}

		private void menuItemRinggroupNone_Click(object sender,EventArgs e) {
			//This even works if the person is still clocked in.
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
		}

		private void menuItemRinggroupsDefault_Click(object sender,EventArgs e) {
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneAsterisks.SetToDefaultRingGroups(extension,employeeNum);
		}

		private void menuItemBackup_Click(object sender,EventArgs e) {
			if(!ClockIn()) {
				return;
			}
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			PhoneOverrides.SetAvailable(extension,employeeNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.Backup);
			Employees.SetPhoneStatus("Backup",extension);
			FillEmps();
		}

		//Timecard---------------------------------------------------

		private void menuItemLunch_Click(object sender,EventArgs e) {
			//verify that employee is logged in as user
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)){
				if(Security.CurUser.EmployeeNum!=employeeNum){
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)){
						MsgBox.Show(this,"Not authorized.");
						return;
					}
				}
			}
			try{
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Lunch);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			PhoneOverrides.SetAvailable(extension,employeeNum);
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Lunch.ToString());
			Employees.Update(EmpCur);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Employees.SetPhoneStatus("Lunch",extension);
			FillEmps();
		}

		private void menuItemHome_Click(object sender,EventArgs e) {
			//verify that employee is logged in as user
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)){
				if(Security.CurUser.EmployeeNum!=employeeNum){
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)){
						MsgBox.Show(this,"Not authorized.");
						return;
					}
				}
			}
			try{
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Home);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			PhoneOverrides.SetAvailable(extension,employeeNum);
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Home.ToString());
			Employees.Update(EmpCur);
			//ModuleSelected(PatCurNum);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Employees.SetPhoneStatus("Home",extension);
			FillEmps();
		}

		private void menuItemBreak_Click(object sender,EventArgs e) {
			//verify that employee is logged in as user
			int extension=PIn.Int(tablePhone.Rows[rowI]["Extension"].ToString());
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)){
				if(Security.CurUser.EmployeeNum!=employeeNum){
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)){
						MsgBox.Show(this,"Not authorized.");
						return;
					}
				}
			}
			try{
				ClockEvents.ClockOut(employeeNum,TimeClockStatus.Break);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);//This message will tell user that they are already clocked out.
				return;
			}
			PhoneOverrides.SetAvailable(extension,employeeNum);
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g("enumTimeClockStatus",TimeClockStatus.Break.ToString());
			Employees.Update(EmpCur);
			PhoneAsterisks.SetRingGroups(extension,AsteriskRingGroups.None);
			Employees.SetPhoneStatus("Break",extension);
			FillEmps();
		}

		///<summary>If already clocked in, this does nothing.  Returns false if not able to clock in due to security, or true if successful.</summary>
		private bool ClockIn(){
			long employeeNum=PIn.Long(tablePhone.Rows[rowI]["EmployeeNum"].ToString());
			if(employeeNum==0){
				MsgBox.Show(this,"No employee at that extension.");
				return false;
			}
			if(PrefC.GetBool(PrefName.TimecardSecurityEnabled)){
				if(Security.CurUser.EmployeeNum!=employeeNum){
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)){
						MsgBox.Show(this,"Not authorized.");
						return false;
					}
				}
			}
			try{
				ClockEvents.ClockIn(employeeNum);
			}
			catch{
				//the only reason this will throw an exception is if already clocked in.  Fail silently.
				return true;
			}
			Employee EmpCur=Employees.GetEmp(employeeNum);
			EmpCur.ClockStatus=Lan.g(this,"Working");;
			Employees.Update(EmpCur);
			return true;
		}

	

	

		

		

		

	}
}
