using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class PhoneTile:UserControl {
		private Phone phoneCur;
		public TimeSpan TimeDelta;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when certain controls are selected on this tile related to menu events.")]
		public event EventHandler SelectedTileChanged=null;
		///<summary></summary>
		[Category("Action"),Description("Event raised when user clicks on screenshot.")]
		public event EventHandler ScreenshotClick=null;
		///<summary>Object passed in from parent form.  Event will be fired from that form.</summary>
		public ContextMenuStrip MenuNumbers;
		///<summary>Object passed in from parent form.  Event will be fired from that form.</summary>
		public ContextMenuStrip MenuStatus;
		private bool layoutHorizontal;
		public bool ShowImageForced;

		public PhoneTile() {
			InitializeComponent();
		}
		
		///<summary>Set phone and triage flag to display. Get/Set accessor won't work here because we require 2 seperate fields in order to update the control properly.</summary>
		public void SetPhone(Phone phone,PhoneEmpDefault phoneEmpDefault,bool isTriageOperator) {
			phoneCur=phone;
			if(phoneCur==null) { //empty out everything and return
				this.Visible=false;
				pictureWebCam.Image=null;//or just make it not visible?
				pictureInUse.Visible=false;
				labelExtensionName.Text="";
				labelStatusAndNote.Text="";
				labelTime.Text="";
				labelTime.BackColor=this.BackColor;
				labelCustomer.Text="";
				return;
			}
			this.Visible=true;
			if(ShowImageForced) {
				pictureWebCam.Image=PIn.Bitmap(phoneCur.WebCamImage);
				pictureWebCam.Visible=true;
			}
			else if(phoneCur.ClockStatus==ClockStatusEnum.Home
				|| phoneCur.ClockStatus==ClockStatusEnum.None
				|| phoneCur.ClockStatus==ClockStatusEnum.Off) 
			{
				pictureWebCam.Image=null;
				pictureWebCam.Visible=false;
			}
			else if(phoneCur.ClockStatus==ClockStatusEnum.Break
						|| phoneCur.ClockStatus==ClockStatusEnum.Lunch) {
				pictureWebCam.Visible=true;
				Bitmap bmp=new Bitmap(pictureWebCam.Width,pictureWebCam.Height);
				Graphics g=Graphics.FromImage(bmp);
				try {
					g.FillRectangle(SystemBrushes.Control,0,0,bmp.Width,bmp.Height);
					string strStat=phoneCur.ClockStatus.ToString();
					SizeF sizef=g.MeasureString(strStat,labelStatusAndNote.Font);
					g.DrawString(strStat,labelStatusAndNote.Font,SystemBrushes.GrayText,(bmp.Width-sizef.Width)/2,(bmp.Height-sizef.Height)/2);
					pictureWebCam.Image=(Image)bmp.Clone();
				}
				finally {
					g.Dispose();
					g=null;
					bmp.Dispose();
					bmp=null;
				}
			}
			else {
				pictureWebCam.Visible=true;
				pictureWebCam.Image=PIn.Bitmap(phoneCur.WebCamImage);
			}
			if(phoneCur.Description=="") {
				pictureInUse.Visible=false;
			}
			else {
				pictureInUse.Visible=true;
			}
			labelExtensionName.Text="";
			string str=phoneCur.ClockStatus.ToString();
			//Check if the user is logged in.
			if(phoneCur.ClockStatus==ClockStatusEnum.None
				|| phoneCur.ClockStatus==ClockStatusEnum.Home) 
			{
				str="Clock In";
			}
			//Always show ext and name, no matter if user is clocked in or not. This keeps phone tiles from appearing blank with no extension and name.
			string nameStr="Vacant";
			if(phoneCur.EmployeeName!="") {
				nameStr=phoneCur.EmployeeName;
			}
			labelExtensionName.Text=phoneCur.Extension.ToString()+" - "+nameStr;
			labelStatusAndNote.Text=str;
			DateTime dateTimeStart=phoneCur.DateTimeStart;
			if(dateTimeStart.Date==DateTime.Today) {
				TimeSpan span=DateTime.Now-dateTimeStart+TimeDelta;
				DateTime timeOfDay=DateTime.Today+span;
				labelTime.Text=timeOfDay.ToString("H:mm:ss");
			}
			else {
				labelTime.Text="";
			}						
			if(phoneCur.ClockStatus==ClockStatusEnum.Home
				|| phoneCur.ClockStatus==ClockStatusEnum.None
				|| phoneCur.ClockStatus==ClockStatusEnum.Break) {
				labelTime.BackColor=this.BackColor;//No color if employee is not currently working.
			}
			else {
				Color outerColor;
				Color innerColor;
				Color fontColor;
				bool isTriageOperatorOnTheClock=false;
				//get the cubicle color and triage status
				Phones.GetPhoneColor(phone,phoneEmpDefault,false,out outerColor,out innerColor,out fontColor,out isTriageOperatorOnTheClock);
				if(!timerFlash.Enabled) { //if the control is already flashing then don't overwrite the colors. this would cause a "spastic" flash effect.
					labelTime.BackColor=outerColor;
				}				
				if(phoneCur.ClockStatus==ClockStatusEnum.NeedsHelp) {
					if(!timerFlash.Enabled) { //Only start the flash timer and color the control once. This prevents over-flashing effect.
						labelTime.Tag=new object[2] { false,labelTime.BackColor };
						timerFlash.Start();
					}
				}
			}
			if(phoneCur.ClockStatus==ClockStatusEnum.Home
				|| phoneCur.ClockStatus==ClockStatusEnum.None) 
			{
				labelTime.BorderStyle=System.Windows.Forms.BorderStyle.None;//Remove color box if employee is not currently working.
			}
			else {
				labelTime.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
			}
			if(phoneCur.ClockStatus!=ClockStatusEnum.NeedsHelp) { //Always assume the flash timer was previously turned on and turn it off here. No harm if it' already off.
				timerFlash.Stop();
			}
			labelCustomer.Text=phoneCur.CustomerNumber;
		}

		///<summary>use SetPhone function to set phone and triage flag</summary>
		public Phone PhoneCur {
			get {
				return phoneCur;
			}
		}
		
		[Category("Layout"),Description("Set true for horizontal layout and false for vertical.")]
		public bool LayoutHorizontal{
			get{
				return layoutHorizontal;
			}
			set{
				layoutHorizontal=value;
				if(layoutHorizontal){
					//173,7
					pictureWebCam.Location=new Point(173,7);
					pictureInUse.Location=new Point(224,25);//51,18);
					labelExtensionName.Location=new Point(221,9);//48,2);
					labelStatusAndNote.Location=new Point(249,25);//76,18);
					labelStatusAndNote.TextAlign=ContentAlignment.MiddleLeft;
					labelStatusAndNote.Size=new Size(77,16);
					labelTime.Location=new Point(329,11);//156,4);
					labelTime.Size=new Size(56,16);
					labelCustomer.Location=new Point(332,27);//159,20);
					labelCustomer.Size=new Size(147,16);
					labelCustomer.TextAlign=ContentAlignment.MiddleLeft;
				}
				else{//vertical
					pictureWebCam.Location=new Point(51,3);
					pictureInUse.Location=new Point(14,43);
					labelExtensionName.Location=new Point(37,43);
					labelStatusAndNote.Location=new Point(0,61);
					labelStatusAndNote.TextAlign=ContentAlignment.MiddleCenter;
					labelStatusAndNote.Size=new Size(150,16);
					labelTime.Location=new Point(0,81);
					labelTime.Size=new Size(150,16);
					labelCustomer.Location=new Point(0,99);
					labelCustomer.Size=new Size(150,16);
					labelCustomer.TextAlign=ContentAlignment.MiddleCenter;
				}
			}
		}

		protected override Size DefaultSize {
			get {
				if(layoutHorizontal){
					return new Size(595,37);
				}
				else{//vertical
					return new Size(150,122);
				}
			}
		}
		
		private void labelCustomer_MouseClick(object sender,MouseEventArgs e) {
			if((e.Button & MouseButtons.Right)==MouseButtons.Right) {
				return;
			}
			OnGoToChanged();
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}

		private void labelCustomer_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button!=MouseButtons.Right) {
				return;
			}
			if(phoneCur==null) {
				return;
			}
			OnSelectedTileChanged();
			MenuNumbers.Show(labelCustomer,e.Location);	
		}

		private void labelStatusAndNote_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button!=MouseButtons.Right) {
				return;
			}
			if(phoneCur==null) {
				return;
			}
			//Jason - Allowed to be 0 here.  The Security.UserCur.EmpNum will be used when they go to clock in and that is where the 0 check needs to be.
			//if(phoneCur.EmployeeNum==0) {
			//  return;
			//}
			OnSelectedTileChanged();
			bool allowStatusEdit=ClockEvents.IsClockedIn(PhoneCur.EmployeeNum);
			if(PhoneCur.EmployeeNum==Security.CurUser.EmployeeNum) { //Always allow status edit for yourself
				allowStatusEdit=true;
			}
			if(PhoneCur.ClockStatus==ClockStatusEnum.NeedsHelp) { //Always allow any employee to change any other employee from NeedsAssistance to Available
				allowStatusEdit=true;
			}
			string statusOnBehalfOf=PhoneCur.EmployeeName;
			bool allowSetSelfAvailable=false;
			if(!ClockEvents.IsClockedIn(PhoneCur.EmployeeNum) //No one is clocked in at this extension.
				&& !ClockEvents.IsClockedIn(Security.CurUser.EmployeeNum)) //This user is not clocked in either.
			{ 
				//Vacant extension and this user is not clocked in so allow this user to clock in at this extension.
				statusOnBehalfOf=Security.CurUser.UserName;
				allowSetSelfAvailable=true;
			}
			AddToolstripGroup("menuItemStatusOnBehalf","Status for: "+statusOnBehalfOf);
			AddToolstripGroup("menuItemRingGroupOnBehalf","Ringgroup for ext: "+PhoneCur.Extension.ToString());
			AddToolstripGroup("menuItemClockOnBehalf","Clock event for: "+PhoneCur.EmployeeName);
			SetToolstripItemText("menuItemAvailable",allowStatusEdit || allowSetSelfAvailable);
			SetToolstripItemText("menuItemTraining",allowStatusEdit);
			SetToolstripItemText("menuItemTeamAssist",allowStatusEdit);
			SetToolstripItemText("menuItemNeedsHelp",allowStatusEdit);
			SetToolstripItemText("menuItemWrapUp",allowStatusEdit);
			SetToolstripItemText("menuItemOfflineAssist",allowStatusEdit);
			SetToolstripItemText("menuItemUnavailable",allowStatusEdit);
			SetToolstripItemText("menuItemBackup",allowStatusEdit);
			SetToolstripItemText("menuItemLunch",allowStatusEdit);
			SetToolstripItemText("menuItemHome",allowStatusEdit);
			SetToolstripItemText("menuItemBreak",allowStatusEdit);
			MenuStatus.Show(labelStatusAndNote,e.Location);		
		}

		private void AddToolstripGroup(string groupName,string itemText) {
			ToolStripItem[] tsiFound=MenuStatus.Items.Find(groupName,false);
			if(tsiFound==null || tsiFound.Length<=0) {
				return;
			}
			tsiFound[0].Text=itemText;
		}

		private void SetToolstripItemText(string toolStripItemName,bool isClockedIn) {
			ToolStripItem[] tsiFound=MenuStatus.Items.Find(toolStripItemName,false);
			if(tsiFound==null || tsiFound.Length<=0) {
				return;
			}
			//set back to default
			tsiFound[0].Text=tsiFound[0].Text.Replace(" (Not Clocked In)","");
			if(isClockedIn) {
				tsiFound[0].Enabled=true;				
			}
			else {
				tsiFound[0].Enabled=false;
				tsiFound[0].Text=tsiFound[0].Text+" (Not Clocked In)";
			}			
		}

		protected void OnSelectedTileChanged() {
			if(SelectedTileChanged!=null) {
				SelectedTileChanged(this,new EventArgs());
			}
		}

		private void phoneTile_Click(object sender,EventArgs e) {
			if(ScreenshotClick!=null) {
				ScreenshotClick(this,new EventArgs());
			}
		}

		private void timerFlash_Tick(object sender,EventArgs e) {
			bool isColored=true;
			Color flashColor=SystemColors.Control;
			if(labelTime.Tag!=null 
				&& labelTime.Tag is object[]
				&& ((object[])labelTime.Tag).Length>=2) 
			{
					if(((object[])labelTime.Tag)[0] is bool) {
						isColored=(bool)((object[])labelTime.Tag)[0];
					}
					if(((object[])labelTime.Tag)[1] is Color) {
						flashColor=(Color)((object[])labelTime.Tag)[1];
					}
			}
			labelTime.BackColor=isColored?this.BackColor:flashColor;
			labelTime.Tag=new object[2] { !isColored,flashColor };
		}

		

		


	}
}
