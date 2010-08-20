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
			if(phoneList==null){
				return;
			}
			float wh=21.4f;
			float hTot=wh*3;
			float x=0f;
			float y=0f;
			for(int i=0;i<phoneList.Count;i++){
				using(SolidBrush brush=new SolidBrush(phoneList[i].ColorBar)){
					g.FillRectangle(brush,x*wh,y*wh,wh,wh);
				}
				x++;
				if(x>=7){
					x=0f;
					y++;
				}
			}
			//horiz lines
			for(int i=0;i<4;i++){
				g.DrawLine(Pens.Black,0,i*wh,Width,i*wh);
			}
			//Very bottom
			g.DrawLine(Pens.Black,0,Height-1,Width,Height-1);
			//vert
			for(int i=0;i<7;i++){
				g.DrawLine(Pens.Black,i*wh,0,i*wh,hTot);
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
