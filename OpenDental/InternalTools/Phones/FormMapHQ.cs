using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Drawing;

namespace OpenDental {
	public partial class FormMapHQ:Form {
		#region Private Members
		///<summary>Keep track of full screen state</summary>
		private bool _isFullScreen;
		///<summary>This is the difference between server time and local computer time.  Used to ensure that times displayed are accurate to the second.  This value is usally just a few seconds, but possibly a few minutes.</summary>
		private TimeSpan _timeDelta;
		#endregion Private Members

		#region Initialize

		public FormMapHQ() {
			InitializeComponent();
			//Do not do anything to do with database or control init here. We will be using this ctor later in order to create a temporary object so we can figure out what size the form should be when the user comes back from full screen mode. Wait until FormMapHQ_Load to do anything meaningful.
			_isFullScreen=false;
			_timeDelta=MiscData.GetNowDateTime()-DateTime.Now;
		}

		private void FormMapHQ_Load(object sender,EventArgs e) {
			FillMapAreaPanel();
			labelTriageOpsStaff.InnerColor=Phones.PhoneColorScheme.COLOR_DUAL_InnerTriageHere;
			labelTriageOpsStaff.OuterColor=Phones.PhoneColorScheme.COLOR_DUAL_OuterTriage;
		}

		///<summary>Setup the map panel with the cubicles and labels before filling with real-time data. Call this on load or anytime the cubicle layout has changed.</summary>
		private void FillMapAreaPanel() {
			mapAreaPanelHQ.Controls.Clear();
			//fill the panel
			List<MapArea> clinicMapItems=MapAreas.Refresh();
			for(int i=0;i<clinicMapItems.Count;i++) {
				if(clinicMapItems[i].ItemType==MapItemType.Room) {
					mapAreaPanelHQ.AddCubicle(clinicMapItems[i]);
				}
				else if(clinicMapItems[i].ItemType==MapItemType.DisplayLabel) {
					mapAreaPanelHQ.AddDisplayLabel(clinicMapItems[i]);
				}
			}
		}

		#endregion

		#region Set label text and colors

		///<summary>Refresh the phone panel every X seconds after it has already been setup.  Make sure to call FillMapAreaPanel before calling this the first time.</summary>
		public void SetPhoneList(List<PhoneEmpDefault> peds,List<Phone> phones) {
			try {
				string title="Call Center Status Map - Triage Coordinator - ";
				try { //get the triage coord label but don't fail just because we can't find it
					title+=Employees.GetNameFL(PrefC.GetLong(PrefName.HQTriageCoordinator));
				}
				catch {
					title+="Not Set";
				}
				labelTriageCoordinator.Text=title;
				labelCurrentTime.Text=DateTime.Now.ToShortTimeString();
				int triageStaffCount=0;
				for(int i=0;i<this.mapAreaPanelHQ.Controls.Count;i++) { //loop through all of our cubicles and labels and find the matches
					if(!(this.mapAreaPanelHQ.Controls[i] is MapAreaRoomControl)) {
						continue;
					}
					MapAreaRoomControl room=(MapAreaRoomControl)this.mapAreaPanelHQ.Controls[i];
					if(room.MapAreaItem.Extension==0) { //This cubicle has not been given an extension yet.
						room.Empty=true;
						continue;
					}
					Phone phone=Phones.GetPhoneForExtension(phones,room.MapAreaItem.Extension);
					if(phone==null) {//We have a cubicle with no corresponding phone entry.
						room.Empty=true;
						continue;
					}
					PhoneEmpDefault phoneEmpDefault=PhoneEmpDefaults.GetEmpDefaultFromList(phone.EmployeeNum,peds);
					if(phoneEmpDefault==null) {//We have a cubicle with no corresponding phone emp default entry.
						room.Empty=true;
						continue;
					}
					//we got this far so we found a corresponding cubicle for this phone entry
					room.EmployeeNum=phone.EmployeeNum;
					room.EmployeeName=phone.EmployeeName;					
					if(phone.DateTimeStart.Date==DateTime.Today) {
						TimeSpan span=DateTime.Now-phone.DateTimeStart+_timeDelta;
						DateTime timeOfDay=DateTime.Today+span;
						room.Elapsed=timeOfDay.ToString("H:mm:ss");
					}
					else {
						room.Elapsed="";
					}
					string status=phone.ClockStatus.ToString();
					//Check if the user is logged in.
					if(phone.ClockStatus==ClockStatusEnum.None
						|| phone.ClockStatus==ClockStatusEnum.Home) {
						status="Home";
					}
					room.Status=status;
					if(phone.Description=="") {
						room.PhoneImage=null;
					}
					else {
						room.PhoneImage=Properties.Resources.phoneInUse;
					}
					Color outerColor;
					Color innerColor;
					Color fontColor;
					bool isTriageOperatorOnTheClock=false;
					//get the cubicle color and triage status
					Phones.GetPhoneColor(phone,phoneEmpDefault,true,out outerColor,out innerColor,out fontColor,out isTriageOperatorOnTheClock);
					if(!room.IsFlashing) { //if the control is already flashing then don't overwrite the colors. this would cause a "spastic" flash effect.
						room.OuterColor=outerColor;
						room.InnerColor=innerColor;					
					}
					room.ForeColor=fontColor;
					if(isTriageOperatorOnTheClock) {
						triageStaffCount++;
					}
					if(phone.ClockStatus==ClockStatusEnum.NeedsHelp) { //turn on flashing
						room.StartFlashing();
					}
					else { //turn off flashing
						room.StopFlashing();
					}
					room.Invalidate(true);					
				}
				this.labelTriageOpsStaff.Text=triageStaffCount.ToString();
			}
			catch {
				//something failed unexpectedly
			}
		}
		
		public void SetTriageUrgent(int calls,TimeSpan timeBehind) {
			this.labelTriageRedCalls.Text=calls.ToString();
			if(timeBehind==TimeSpan.Zero) { //format the string special for this case
				this.labelTriageRedTimeSpan.Text="00:00";
			}
			else {
				this.labelTriageRedTimeSpan.Text=timeBehind.ToStringmmss();
			}
			if(calls>1) { //we are behind
				labelTriageRedCalls.InnerColor=Color.Red;
				labelTriageRedCalls.ForeColor=Color.White;
			}
			else { //we are ok
				labelTriageRedCalls.InnerColor=Color.White;
				labelTriageRedCalls.ForeColor=Color.Black;
			}
			if(timeBehind>TimeSpan.FromMinutes(1)) { //we are behind
				labelTriageRedTimeSpan.InnerColor=Color.Red;
				labelTriageRedTimeSpan.ForeColor=Color.White;
			}
			else { //we are ok
				labelTriageRedTimeSpan.InnerColor=Color.White;
				labelTriageRedTimeSpan.ForeColor=Color.Black;
			}
		}

		public void SetVoicemailRed(int calls,TimeSpan timeBehind) {
			this.labelVoicemailCalls.Text=calls.ToString();
			if(timeBehind==TimeSpan.Zero) { //format the string special for this case
				this.labelVoicemailTimeSpan.Text="00:00";
			}
			else {
				this.labelVoicemailTimeSpan.Text=timeBehind.ToStringmmss();
			}
			if(calls>5) { //we are behind
				labelVoicemailCalls.InnerColor=Color.Red;
				labelVoicemailCalls.ForeColor=Color.White;
			}
			else { //we are ok
				labelVoicemailCalls.InnerColor=Color.White;
				labelVoicemailCalls.ForeColor=Color.Black;
			}
			if(timeBehind>TimeSpan.FromMinutes(5)) { //we are behind
				labelVoicemailTimeSpan.InnerColor=Color.Red;
				labelVoicemailTimeSpan.ForeColor=Color.White;
			}
			else { //we are ok
				labelVoicemailTimeSpan.InnerColor=Color.White;
				labelVoicemailTimeSpan.ForeColor=Color.Black;
			}
		}
		
		public void SetTriageNormal(int callsWithNotes,int callsWithNoNotes,TimeSpan timeBehind) {
			if(timeBehind==TimeSpan.Zero) { //format the string special for this case
				this.labelTriageTimeSpan.Text="0";
			}
			else {
				this.labelTriageTimeSpan.Text=((int)timeBehind.TotalMinutes).ToString();			
			}
			if(callsWithNoNotes>0) { //we have calls which don't have notes so display that number
				this.labelTriageCalls.Text=callsWithNoNotes.ToString();
			}
			else { //we don't have any calls with no notes so display count of total tasks
				this.labelTriageCalls.Text="("+callsWithNotes.ToString()+")";
			}
			if(callsWithNoNotes>10) { //we are behind
				labelTriageCalls.InnerColor=Color.Red;
				labelTriageCalls.ForeColor=Color.White;
			}
			else { //we are ok
				labelTriageCalls.InnerColor=Color.White;
				labelTriageCalls.ForeColor=Color.Black;
			}
			if(timeBehind>TimeSpan.FromMinutes(19)) { //we are behind
				labelTriageTimeSpan.ForeColor=Color.Red;
				labelTriageTimeSpan.OuterColor=Color.Red;
			}
			else { //we are ok
				labelTriageTimeSpan.ForeColor=Color.Black;
				labelTriageTimeSpan.OuterColor=Color.Black;
				labelTriageTimeSpan.InnerColor=Color.White;
			}
		}

		#endregion Set label text and colors

		private void fullScreenToolStripMenuItem_Click(object sender,EventArgs e) {
			_isFullScreen=!_isFullScreen;
			if(_isFullScreen) { //switch to full screen
				this.fullScreenToolStripMenuItem.Text="Restore";
				this.setupToolStripMenuItem.Visible=false;
				this.WindowState=FormWindowState.Normal;
				this.FormBorderStyle=System.Windows.Forms.FormBorderStyle.None;
				this.Bounds=System.Windows.Forms.Screen.FromControl(this).Bounds;
				this.mapAreaPanelHQ.PixelsPerFoot=18;
			}
			else { //set back to defaults
				this.fullScreenToolStripMenuItem.Text="Full Screen";
				FormMapHQ FormCMS=new FormMapHQ();
				this.FormBorderStyle=FormCMS.FormBorderStyle;
				this.Size=FormCMS.Size;
				this.CenterToScreen();
				this.setupToolStripMenuItem.Visible=true;
				this.mapAreaPanelHQ.PixelsPerFoot=17;
			}
		}

		private void setupToolStripMenuItem_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			
			FormMapSetup FormMS=new FormMapSetup();
			FormMS.ShowDialog();
			FillMapAreaPanel();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"MapHQ layout changed");
		}		
	}
}
