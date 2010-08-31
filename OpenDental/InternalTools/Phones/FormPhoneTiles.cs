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
		///<summary>This thread fills labelMsg</summary>
		private Thread workerThread;
		private List<Phone> phoneList;

		public List<Phone> PhoneList {
			set { 
				phoneList = value;
				FillTiles(false);
				//Invalidate();
			}
		}
		
		public FormPhoneTiles() {
			InitializeComponent();
		}

		private void FormPhoneTiles_Load(object sender,EventArgs e) {
			#if !DEBUG
				if(Environment.MachineName.ToLower()!="jordans") {
					checkBoxAll.Visible=false;//so this will also be visible in debug
				}
			#endif
			timeDelta=MiscData.GetNowDateTime()-DateTime.Now;
			PhoneTile tile;
			int x=0;
			int y=0;
			for(int i=0;i<20;i++) {
				tile=new PhoneTile();
				tile.Name="phoneTile"+(i+1).ToString();
				tile.LayoutHorizontal=true;
				tile.Location=new Point(tile.Width*x,26+(tile.Height*y));
				//((PhoneTile)Controls.Find("phoneTile"+(i+1).ToString(),false)[0]);
				tile.GoToChanged += new System.EventHandler(this.phoneTile_GoToChanged);
				tile.SelectedTileChanged += new System.EventHandler(this.phoneTile_SelectedTileChanged);
				tile.MenuNumbers=menuNumbers;
				tile.MenuStatus=menuStatus;
				this.Controls.Add(tile);
				y++;
				if(y==10){
					y=0;
					x++;
				}
			}
			FillTiles(true);//initial fast load and anytime data changes.  After this, pumped in from main form.
		}

		private void FormPhoneTiles_Shown(object sender,EventArgs e) {
			DateTime now=DateTime.Now;
			while(now.AddSeconds(1)>DateTime.Now) {
				Application.DoEvents();
			}
			timerMsgs.Enabled=true;
			//SetLabelMsg();
		}

		private void FillTiles(bool refreshList) {
			if(refreshList) {
				phoneList=Phones.GetPhoneList();
			}
			if(phoneList==null){
				return;
			}
			PhoneTile tile;
			for(int i=0;i<20;i++) {
				Application.DoEvents();
				tile=((PhoneTile)Controls.Find("phoneTile"+(i+1).ToString(),false)[0]);
				tile.TimeDelta=timeDelta;
				tile.ShowImageForced=checkBoxAll.Checked;
				if(phoneList.Count>i){
					tile.PhoneCur=phoneList[i];
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

		//private void timerMain_Tick(object sender,EventArgs e) {
			//every 1.6 seconds
			//FillTiles(false);
		//}

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

		private void checkBoxAll_Click(object sender,EventArgs e) {
			FillTiles(false);
		}

		private void menuItemManage_Click(object sender,EventArgs e) {
			PhoneUI.Manage(selectedTile);
		}

		private void menuItemAdd_Click(object sender,EventArgs e) {
			PhoneUI.Add(selectedTile);
		}

		//Timecards-------------------------------------------------------------------------------------

		private void menuItemAvailable_Click(object sender,EventArgs e) {
			PhoneUI.Available(selectedTile);
			FillTiles(true);
		}

		private void menuItemTraining_Click(object sender,EventArgs e) {
			PhoneUI.Training(selectedTile);
			FillTiles(true);
		}

		private void menuItemTeamAssist_Click(object sender,EventArgs e) {
			PhoneUI.TeamAssist(selectedTile);
			FillTiles(true);
		}

		private void menuItemWrapUp_Click(object sender,EventArgs e) {
			PhoneUI.WrapUp(selectedTile);
			FillTiles(true);
		}

		private void menuItemOfflineAssist_Click(object sender,EventArgs e) {
			PhoneUI.OfflineAssist(selectedTile);
			FillTiles(true);
		}

		private void menuItemUnavailable_Click(object sender,EventArgs e) {
			PhoneUI.Unavailable(selectedTile);
			FillTiles(true);
		}

		//RingGroups---------------------------------------------------

		private void menuItemRinggroupAll_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupAll(selectedTile);
		}

		private void menuItemRinggroupNone_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupNone(selectedTile);
		}

		private void menuItemRinggroupsDefault_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupsDefault(selectedTile);
		}

		private void menuItemBackup_Click(object sender,EventArgs e) {
			PhoneUI.Backup(selectedTile);
			FillTiles(true);
		}

		//Timecard---------------------------------------------------

		private void menuItemLunch_Click(object sender,EventArgs e) {
			PhoneUI.Lunch(selectedTile);
			FillTiles(true);
		}

		private void menuItemHome_Click(object sender,EventArgs e) {
			PhoneUI.Home(selectedTile);
			FillTiles(true);
		}

		private void menuItemBreak_Click(object sender,EventArgs e) {
			PhoneUI.Break(selectedTile);
			FillTiles(true);
		}

		private void FormPhoneTiles_FormClosing(object sender,FormClosingEventArgs e) {
			if(workerThread!=null){
				workerThread.Abort();
			}
		}

	
		
		


	}

	
}
