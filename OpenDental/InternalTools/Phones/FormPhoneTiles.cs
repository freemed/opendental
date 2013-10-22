using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
		private PhoneTile selectedTile;
		///<summary>This thread fills labelMsg</summary>
		private List<Phone> PhoneList;
		private List<PhoneEmpDefault> PhoneEmpDefaultList;
		///<summary>Max number of tiles that can be shown. Columns and tiles which are not needed will be hidden and the window will be sized accordingly.</summary>
		private int TileCount=120;
		///<summary>How many phone tiles should show up in each column before creating a new column.</summary>
		private int TilesPerColumn=15;
		private Phones.PhoneComparer.SortBy SortBy=Phones.PhoneComparer.SortBy.name;
		
		public void SetPhoneList(List<PhoneEmpDefault> peds,List<Phone> phones) {
			//create a new list so our sorting doesn't affect this list elsewhere
			PhoneList=new List<Phone>(phones);
			PhoneList.Sort(new Phones.PhoneComparer(SortBy));
			PhoneEmpDefaultList=peds;
			FillTiles(false);
		}

		public void SetVoicemailCount(int voiceMailCount) {
			if(voiceMailCount==0) {
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Regular);
				labelMsg.ForeColor=Color.Black;
			}
			else {
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,10f,FontStyle.Bold);
				labelMsg.ForeColor=Color.Firebrick;
			}
			labelMsg.Text="Voice Mails: "+voiceMailCount.ToString();
		}

		public FormPhoneTiles() {
			InitializeComponent();
		}

		private void FormPhoneTiles_Load(object sender,EventArgs e) {
#if !DEBUG
				if(Environment.MachineName.ToLower()!="jordans"
					&& Environment.MachineName.ToLower()!="nathan") 
				{
					checkBoxAll.Visible=false;//so this will also be visible in debug
				}
#endif
			timeDelta=MiscData.GetNowDateTime()-DateTime.Now;
			PhoneTile tile;
			int x=0;
			int y=0;
			for(int i=1;i<TileCount+1;i++) {
				tile=new PhoneTile();
				tile.Name="phoneTile"+(i).ToString();
				tile.Location=new Point(tile.Width*x,butOverride.Bottom+15+(tile.Height*y));				
				tile.GoToChanged += new System.EventHandler(this.phoneTile_GoToChanged);
				tile.SelectedTileChanged += new System.EventHandler(this.phoneTile_SelectedTileChanged);
				tile.ScreenshotClick += new EventHandler(this.phoneTile_ScreenshotClick);
				tile.MenuNumbers=menuNumbers;
				tile.MenuStatus=menuStatus;
				this.Controls.Add(tile);
				y++;
				if(i%(TilesPerColumn)==0) {//If number is divisble by the number of tiles per column, move over to a new column.  TilesPerColumn subtracts one because i is zero based.
					y=0;
					x++;
				}
			}
			FillTiles(true);//initial fast load and anytime data changes.  After this, pumped in from main form.
			Control[] topLeftMatch=Controls.Find("phoneTile1",false);
			if(PhoneList.Count>=1 
				&& topLeftMatch!=null
				&& topLeftMatch.Length>=1) { //Size the window to fit contents
				tile=((PhoneTile)topLeftMatch[0]);
				int columns=(int)Math.Ceiling((double)PhoneList.Count/TilesPerColumn);
				this.ClientSize=new Size(columns*tile.Width,tile.Top+(tile.Height*TilesPerColumn));
			}
			radioByExt.CheckedChanged+=radioSort_CheckedChanged;
			radioByName.CheckedChanged+=radioSort_CheckedChanged;
		}

		private void FormPhoneTiles_Shown(object sender,EventArgs e) {
			DateTime now=DateTime.Now;
			while(now.AddSeconds(1)>DateTime.Now) {
				Application.DoEvents();
			}
		}

		private void FillTiles(bool refreshList) {
			if(refreshList) { //Refresh the phone list. This will cause a database refresh for our list and call this function again with the new list.
				SetPhoneList(PhoneEmpDefaults.Refresh(),Phones.GetPhoneList());
				return;
			}
			if(PhoneList==null) {
				return;
			}
			PhoneTile tile;
			for(int i=0;i<TileCount;i++) {
				//Application.DoEvents();
				Control[] controlMatches=Controls.Find("phoneTile"+(i+1).ToString(),false);
				if(controlMatches.Length==0) {//no match found for some reason.
					continue;
				}
				tile=((PhoneTile)controlMatches[0]);
				tile.TimeDelta=timeDelta;
				tile.ShowImageForced=checkBoxAll.Checked;
				if(PhoneList.Count>i) {
					tile.SetPhone(PhoneList[i],PhoneEmpDefaults.GetEmpDefaultFromList(PhoneList[i].EmployeeNum,PhoneEmpDefaultList),PhoneEmpDefaults.IsTriageOperatorForExtension(PhoneList[i].Extension,PhoneEmpDefaultList));
				}
				else {
					tile.SetPhone(null,null,false);
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

		private void phoneTile_ScreenshotClick(object sender,EventArgs e) {
			PhoneTile tile=(PhoneTile)sender;
			if(tile.PhoneCur==null) {
				return;
			}
			if(tile.PhoneCur.ScreenshotPath=="") {
				MessageBox.Show("No screenshots available yet.");
				return;
			}
			if(!File.Exists(tile.PhoneCur.ScreenshotPath)) {
				MessageBox.Show("Could not find file: "+tile.PhoneCur.ScreenshotPath);
				return;
			}
			Cursor=Cursors.WaitCursor;
			FormScreenshotBrowse formSB=new FormScreenshotBrowse();
			formSB.ScreenshotPath=tile.PhoneCur.ScreenshotPath;
			formSB.ShowDialog();
			Cursor=Cursors.Default;
		}

		//private void timerMain_Tick(object sender,EventArgs e) {
		//every 1.6 seconds
		//FillTiles(false);
		//}

		//Phones.SetWebCamImage(intTest+101,(Bitmap)pictureWebCam.Image,PhoneList);

		private void butOverride_Click(object sender,EventArgs e) {
			FormPhoneEmpDefaults formPED=new FormPhoneEmpDefaults();
			formPED.ShowDialog();
		}

		private void checkBoxAll_Click(object sender,EventArgs e) {
			Phones.ClearImages();
			FillTiles(false);
		}

		private void radioSort_CheckedChanged(object sender,EventArgs e) {
			if(sender==null
				|| !(sender is RadioButton)
				|| ((RadioButton)sender).Checked==false) 
			{
				return;
			}
			if(radioByExt.Checked) {
				SortBy=Phones.PhoneComparer.SortBy.ext;
			}
			else {
				SortBy=Phones.PhoneComparer.SortBy.name;
			}
			//Get the phone tiles anew. This will force a resort according the preference we just set.
			FillTiles(true);
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

		private void menuItemNeedsHelp_Click(object sender,EventArgs e) {
			PhoneUI.NeedsHelp(selectedTile);
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

		private void menuItemBackup_Click(object sender,EventArgs e) {
			PhoneUI.Backup(selectedTile);
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

		private void menuItemRinggroupsBackup_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupsBackup(selectedTile);
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
		
	}
}
