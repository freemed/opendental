using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class UserControlPhoneSmall:UserControl {
		private List<Phone> phoneList;
		///<summary>When the GoToChanged event fires, this tells us which patnum.</summary>
		public long GotoPatNum;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;
		public int Extension;

		public List<Phone> PhoneList {
			set { 
				phoneList = value;
				Invalidate();
			}
		}

		///<summary>Set to null to not show an employee.</summary>
		public Phone PhoneCur {
			set { 
				phoneTile.PhoneCur= value;
				//Invalidate();
			}
		}

		public UserControlPhoneSmall() {
			InitializeComponent();
			phoneTile.GoToChanged += new System.EventHandler(this.phoneTile_GoToChanged);
			phoneTile.MenuNumbers=menuNumbers;
			phoneTile.MenuStatus=menuStatus;
		}

		private void FillTile() {
			phoneList=Phones.GetPhoneList();
			Invalidate();//as long as we have a new list anyway.
			if(phoneList==null) {
				phoneTile.PhoneCur=null;
				return;
			}
			Phone phone=Phones.GetPhoneForExtension(phoneList,Extension);
			phoneTile.PhoneCur=phone;//could be null
		}

		private void UserControlPhoneSmall_Paint(object sender,PaintEventArgs e) {
			Graphics g=e.Graphics;
			g.FillRectangle(SystemBrushes.Control,this.Bounds);
			if(phoneList==null) {
				return;
			}
			int rows=7;
			int columns=7;
			float boxWidth=21.4f;
			float boxHeight=17f;
			float hTot=boxHeight*rows;
			float x=0f;
			float y=0f;
			//Create a white "background" rectangle so that any empty squares (no employees) will show as white boxes instead of no color.
			g.FillRectangle(new SolidBrush(Color.White),x,y,boxWidth*columns,boxHeight*rows);
			for(int i=0;i<phoneList.Count;i++) {
				//Colors the box a color based on the corresponding phone's status.
				using(SolidBrush brush=new SolidBrush(phoneList[i].ColorBar)) {
					g.FillRectangle(brush,x*boxWidth,y*boxHeight,boxWidth,boxHeight);
				}
				//Draw the extension number if a person is available at that extension.
				if(phoneList[i].ClockStatus!=ClockStatusEnum.Home
					&& phoneList[i].ClockStatus!=ClockStatusEnum.None) 
				{
					Font baseFont=new Font("Arial",7);
					SizeF extSize=g.MeasureString(phoneList[i].Extension.ToString(),baseFont);
					float padX=(boxWidth-extSize.Width)/2;
					float padY=(boxHeight-extSize.Height)/2;
					g.DrawString(phoneList[i].Extension.ToString(),baseFont,new SolidBrush(Color.Black),(x*boxWidth)+(padX),(y*boxHeight)+(padY));
				}
				x++;
				if(x>=columns) {
					x=0f;
					y++;
				}
			}
			//horiz lines
			for(int i=0;i<rows+1;i++) {
				g.DrawLine(Pens.Black,0,i*boxHeight,Width,i*boxHeight);
			}
			//Very bottom
			g.DrawLine(Pens.Black,0,Height-1,Width,Height-1);
			//vert
			for(int i=0;i<columns;i++) {
				g.DrawLine(Pens.Black,i*boxWidth,0,i*boxWidth,hTot);
			}
			g.DrawLine(Pens.Black,Width-1,0,Width-1,hTot);
		}

		private void phoneTile_GoToChanged(object sender,EventArgs e) {
			if(phoneTile.PhoneCur==null) {
				return;
			}
			if(phoneTile.PhoneCur.PatNum==0) {
				return;
			}
			GotoPatNum=phoneTile.PhoneCur.PatNum;
			OnGoToChanged();
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}

		private void menuItemManage_Click(object sender,EventArgs e) {
			PhoneUI.Manage(phoneTile);
		}

		private void menuItemAdd_Click(object sender,EventArgs e) {
			PhoneUI.Add(phoneTile);
		}

		//Timecards-------------------------------------------------------------------------------------

		private void menuItemAvailable_Click(object sender,EventArgs e) {
			PhoneUI.Available(phoneTile);
			FillTile();
		}

		private void menuItemTraining_Click(object sender,EventArgs e) {
			PhoneUI.Training(phoneTile);
			FillTile();
		}

		private void menuItemTeamAssist_Click(object sender,EventArgs e) {
			PhoneUI.TeamAssist(phoneTile);
			FillTile();
		}

		private void menuItemNeedsHelp_Click(object sender,EventArgs e) {
			PhoneUI.NeedsHelp(phoneTile);
			FillTile();
		}

		private void menuItemWrapUp_Click(object sender,EventArgs e) {
			PhoneUI.WrapUp(phoneTile);
			FillTile();
		}

		private void menuItemOfflineAssist_Click(object sender,EventArgs e) {
			PhoneUI.OfflineAssist(phoneTile);
			FillTile();
		}

		private void menuItemUnavailable_Click(object sender,EventArgs e) {
			PhoneUI.Unavailable(phoneTile);
			FillTile();
		}

		//RingGroups---------------------------------------------------

		private void menuItemRinggroupAll_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupAll(phoneTile);
		}

		private void menuItemRinggroupNone_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupNone(phoneTile);
		}

		private void menuItemRinggroupsDefault_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupsDefault(phoneTile);
		}

		private void menuItemBackup_Click(object sender,EventArgs e) {
			PhoneUI.Backup(phoneTile);
			FillTile();
		}

		//Timecard---------------------------------------------------

		private void menuItemLunch_Click(object sender,EventArgs e) {
			PhoneUI.Lunch(phoneTile);
			FillTile();
		}

		private void menuItemHome_Click(object sender,EventArgs e) {
			PhoneUI.Home(phoneTile);
			FillTile();
		}

		private void menuItemBreak_Click(object sender,EventArgs e) {
			PhoneUI.Break(phoneTile);
			FillTile();
		}



	}
}
