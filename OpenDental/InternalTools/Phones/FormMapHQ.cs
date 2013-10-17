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
		}

		///<summary>Setup the map panel with the cubicles and labels before filling with real-time data. Call this on load or anytime the cubicle layout has changed.</summary>
		private void FillMapAreaPanel() {
			timerRefresh.Stop();
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
			//call the timer for the first time
			timerRefresh_Tick(this,new EventArgs());
		}

		#endregion

		#region Set label text and colors

		///<summary>Refresh the phone panel every X seconds after it has already been setup. Make sure to call FillMapAreaPanel before calling this the first time.</summary>
		public void SetPhoneList(List<PhoneEmpDefault> peds,List<Phone> phones) {
			try {
				try { //get the triage coord label but don't fail just because we can't find it
					labelTriageCoordinator.Text=PrefC.GetString(PrefName.HQTriageCoordinator);
				}
				catch { labelTriageCoordinator.Text="Not Set"; }
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
					if(phone.DateTimeStart==DateTime.Today) {
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
					GetPhoneColor(phone,phoneEmpDefault,out outerColor,out innerColor,out fontColor,out isTriageOperatorOnTheClock);
					room.OuterColor=outerColor;
					room.InnerColor=innerColor;
					room.ForeColor=fontColor;					
					if(isTriageOperatorOnTheClock) {
						triageStaffCount++;
					}
					room.Invalidate(true);
					//todo: deal with flashing on NeedsHelp (as was previously done below)

					//if(phone.ClockStatus==ClockStatusEnum.NeedsHelp) {
					//	//todo: add falsh to cubicle
					//	//if(!timerFlash.Enabled) { //Only start the flash timer and color the control once. This prevents over-flashing effect.
					//		room.OuterColor=Phones.ColorOrchid;
					//	//	room.Tag=new object[2] { false,room.BackColor };
					//	//	timerFlash.Start();
					////	}
					//}
					//else if(phone.ClockStatus==ClockStatusEnum.Home
					//|| phone.ClockStatus==ClockStatusEnum.None
					//|| phone.ClockStatus==ClockStatusEnum.Break) {
					//	room.OuterColor=Color.Gray;//No color if employee is not currently working.
					//}
					//else if(PhoneEmpDefaults.IsTriageOperatorForExtension(phone.Extension,peds)) {//Color triage operator specially, don't pay attention to what the phone server is telling us.
					//	room.OuterColor=Phones.ColorSkyBlue;
					//}
					//else {//Phone Server is actively setting this value for us when phone status changes.
					//	room.OuterColor=phone.ColorBar;
					//}
					//if(phone.ClockStatus!=ClockStatusEnum.NeedsHelp) { //Always assume the flash timer was previously turned on and turn it off here.
					//	//timerFlash.Stop();
					//}
					//room.InnerColor=Color.FromArgb(40,room.OuterColor);
					//room.Invalidate(true);
				}
				this.labelTriageOpsStaff.Text=triageStaffCount.ToString();
			}
			catch { 
				//something failed unexpectedly
			}
		}
		
		public void SetTriageRed(int calls,TimeSpan timeBehind) {
			groupPhoneMetrics.Visible=true;
			this.labelTriageRedCalls.Text=calls.ToString();
			this.labelTriageRedTimeSpan.Text=timeBehind.ToStringHmmss();
			if(calls>1 || timeBehind>TimeSpan.FromMinutes(1)) { //we are behind
				labelTriageRedCalls.BackColor=Color.Red;
				labelTriageRedTimeSpan.BackColor=Color.Red;
				labelTriageRedCalls.ForeColor=Color.White;
				labelTriageRedTimeSpan.ForeColor=Color.White;
			}
			else { //we are ok
				labelTriageRedCalls.BackColor=Color.White;
				labelTriageRedTimeSpan.BackColor=Color.White;
				labelTriageRedCalls.ForeColor=Color.Black;
				labelTriageRedTimeSpan.ForeColor=Color.Black;
			}
		}

		public void SetVoicemailRed(int calls,TimeSpan timeBehind) {
			groupPhoneMetrics.Visible=true;
			this.labelVoicemailCalls.Text=calls.ToString();
			this.labelVoicemailTimeSpan.Text=timeBehind.ToStringHmmss();
			if(calls>5 || timeBehind>TimeSpan.FromMinutes(5)) { //we are behind
				labelVoicemailCalls.BackColor=Color.Red;
				labelVoicemailTimeSpan.BackColor=Color.Red;
				labelVoicemailCalls.ForeColor=Color.White;
				labelVoicemailTimeSpan.ForeColor=Color.White;
			}
			else { //we are ok
				labelVoicemailCalls.BackColor=Color.White;
				labelVoicemailTimeSpan.BackColor=Color.White;
				labelVoicemailCalls.ForeColor=Color.Black;
				labelVoicemailTimeSpan.ForeColor=Color.Black;
			}
		}

		public void SetTriageBlue(int calls,TimeSpan timeBehind) {
			groupPhoneMetrics.Visible=true;
			this.labelTriageCalls.Text=calls.ToString();
			this.labelTriageTimeSpan.Text=timeBehind.ToStringHmmss();
			if(calls>10 || timeBehind>TimeSpan.FromMinutes(19)) { //we are behind
				labelTriageCalls.BackColor=Color.Red;
				labelTriageTimeSpan.BackColor=Color.Red;
				labelTriageCalls.ForeColor=Color.White;
				labelTriageTimeSpan.ForeColor=Color.White;
			}
			else { //we are ok
				labelTriageCalls.BackColor=Color.White;
				labelTriageTimeSpan.BackColor=Color.White;
				labelTriageCalls.ForeColor=Color.Black;
				labelTriageTimeSpan.ForeColor=Color.Black;
			}
		}

		#endregion Set label text and colors

		///<summary>Consider all scenarios for a employee/phone/cubicle and return color and triage information</summary>
		public static void GetPhoneColor(Phone phone,PhoneEmpDefault phoneEmpDefault,out Color outerColor,out Color innerColor,out Color fontColor,out bool isTriageOperatorOnTheClock) {
			isTriageOperatorOnTheClock=false;
			//first set the font color
			if(phone.ClockStatus==ClockStatusEnum.Home
						|| phone.ClockStatus==ClockStatusEnum.None
						|| phone.ClockStatus==ClockStatusEnum.Off) {
				fontColor=Color.FromArgb(128,Color.Gray);
			}
			else {
				fontColor=Color.Black;
			}
			//now cover all scenarios and set the inner and out color
			if(phone.ClockStatus==ClockStatusEnum.Home
				|| phone.ClockStatus==ClockStatusEnum.None
				|| phone.ClockStatus==ClockStatusEnum.Off) {
				//No color if employee is not currently working. Trumps all.
				outerColor=Color.FromArgb(128,Color.Gray);
				innerColor=Color.FromArgb(20,Color.Gray);
				return;
			}
			if(phone.ClockStatus==ClockStatusEnum.NeedsHelp) { //get this person help now!
				outerColor=Phones.ColorOrchid;
				innerColor=Color.FromArgb(40,Phones.ColorOrchid);
				return;
			}
			if(phone.ClockStatus==ClockStatusEnum.Unavailable //Unavailable is very rare and must be approved by management. Make them look like admin/engineer.
				|| !phoneEmpDefault.HasColor) //not colored (generally an engineer or admin)
			{
				outerColor=Color.FromArgb(128,Color.Gray);
				innerColor=Color.FromArgb(20,Color.Gray);
				return;
			}
			//If we get this far then the person is a tech who is working today.
			if(phoneEmpDefault.IsTriageOperator) {
				outerColor=Phones.ColorSkyBlue;
				if(phone.ClockStatus==ClockStatusEnum.Break ||
					phone.ClockStatus==ClockStatusEnum.Lunch) {
					//triage op is working today but currently on break/lunch
					innerColor=Color.White;
				}
				else {
					//this is a triage operator who is currently here and on the clock
					isTriageOperatorOnTheClock=true;
					innerColor=Color.FromArgb(40,Phones.ColorSkyBlue);
				}
				return;
			}
			if(phone.Description!="") { //Description field only has 'in use' when person is on the phone. That is the only time the field is not empty.
				outerColor=Phones.ColorRed;
				innerColor=Color.FromArgb(40,Phones.ColorRed);
				return;
			}
			//We get this far so we are dealing with a tech who is not on a phone call. Handle each state.
			switch(phone.ClockStatus) {
				case ClockStatusEnum.Lunch:
				case ClockStatusEnum.Break:
					outerColor=Phones.ColorGreen;
					innerColor=Color.White;
					return;
				case ClockStatusEnum.Available:
					outerColor=Phones.ColorGreen;
					innerColor=Phones.ColorPaleGreen;
					return;
				case ClockStatusEnum.WrapUp:
					outerColor=Phones.ColorGreen;
					innerColor=Phones.ColorYellow;
					return;
				case ClockStatusEnum.Training:
				case ClockStatusEnum.TeamAssist:
					outerColor=Phones.ColorGreen;
					innerColor=Phones.ColorYellow;
					return;
				case ClockStatusEnum.OfflineAssist:
					outerColor=Phones.ColorGreen;
					innerColor=Phones.ColorYellow;
					return;
				case ClockStatusEnum.Backup:
					outerColor=Color.FromArgb(128,Color.Gray);
					innerColor=Color.FromArgb(128,Phones.ColorPaleGreen);
					return;
				default:
					break;
			}
			throw new Exception("FormMapHQ.GetPhoneColor has a state that is currently unsupported!");
		}

		private void butFullScreen_Click(object sender,EventArgs e) {
			_isFullScreen=!_isFullScreen;
			if(_isFullScreen) { //switch to full screen
				this.butFullScreen.Text="Restore";
				this.menuStrip.Visible=false;
				this.WindowState = FormWindowState.Normal;
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
				this.Bounds = System.Windows.Forms.Screen.FromControl(this).Bounds;
			}
			else { //set back to defaults
				this.butFullScreen.Text="Full Screen";
				FormMapHQ FormCMS=new FormMapHQ();
				this.FormBorderStyle = FormCMS.FormBorderStyle;
				this.Size=FormCMS.Size;
				this.CenterToScreen();
				this.menuStrip.Visible=true;				
			}
		}

		private Random _random=new Random();
		private void timerRefresh_Tick(object sender,EventArgs e) {
			try {
				timerRefresh.Stop();
#if DEBUG
				/*SetPhoneList(PhoneEmpDefaults.Refresh(),Phones.GetPhoneList());
				SetTriageRed(_random.Next(0,5),TimeSpan.FromSeconds(_random.Next(0,90)));
				SetVoicemailRed(_random.Next(0,10),TimeSpan.FromSeconds(_random.Next(0,60*10))); 
				SetTriageBlue(_random.Next(0,15),TimeSpan.FromSeconds(_random.Next(0,60*30)));*/
#endif
			}
			catch { }
			finally {
				timerRefresh.Start();
			}
		}
		
		private void setupToolStripMenuItem_Click(object sender,EventArgs e) {
			new FormMapSetup().ShowDialog();
			FillMapAreaPanel();
		}
	}
}
