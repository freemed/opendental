using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary>Only shows for EHR users.</summary>
	public partial class FormEhrTimeSynch:Form {
		private DateTime timeNist;
		private DateTime timeServer;
		private DateTime timeLocal;

		public FormEhrTimeSynch() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrTime_Load(object sender,EventArgs e) {
			textNistUrl.Text=PrefC.GetString(PrefName.NistTimeServerUrl);
			if(textNistUrl.Text=="") {
				return;
			}
			RefreshTimes();
		}

		///<summary>Get the offset from the nist server and DateTime.Now().  Returns double.MinValue if invalid NIST Server URL.  Returns double.MaxValue if request timed out.</summary>
		private double GetNistOffset() {
			//NistTime
			NTPv4 ntp=new NTPv4();
			double ntpOffset;
			try {
				ntpOffset=ntp.getTime(textNistUrl.Text);
			}
			catch {
				MsgBox.Show(this,"Invalid NIST Server URL");
				return double.MinValue;
			}
			timer1.Enabled=true;
			return ntpOffset;
		}

		///<summary>Refreshes all time textboxes.  Saves NIST server URL as preference.</summary>
		private void RefreshTimes() {
			this.Cursor=Cursors.WaitCursor;
			//Get NistTime Offset
			double ntpOffset=GetNistOffset();
			if(ntpOffset==double.MaxValue) { //Timed out
				MsgBox.Show(this,"Nist request timed out.  Try again in four seconds.");
				this.Cursor=Cursors.Default;
				return;
			}
			else if(ntpOffset==double.MinValue) { //Invalid Nist Server Address
				return;
			}
			//Get ServerTime Offset
			//Cannot get milliseconds from Now() in Mysql Pre-5.6.4, uses a workaround by continuously querying server until second ticks over (milliseconds will be close to 0).
			double serverOffset=(MiscData.GetNowDateTimeWithMilli()-DateTime.Now).TotalMilliseconds;
			//Get current times from offsets
			timeLocal=DateTime.Now;
			timeNist=timeLocal.AddMilliseconds(ntpOffset);
			timeServer=timeLocal.AddMilliseconds(serverOffset);
			//Update textboxes
			textNistTime.Text=timeNist.ToString("hh:mm:ss.fff tt");
			textServerTime.Text=timeServer.ToString("hh:mm:ss.fff tt");
			textLocalTime.Text=timeLocal.ToString("hh:mm:ss.fff tt");
			//Update NistURL preference
			Prefs.UpdateString(PrefName.NistTimeServerUrl,textNistUrl.Text);
			this.Cursor=Cursors.Default;
			//Display labels if out of synch.
			if(ServerOurOfSynch()) {
				labelDatabaseSynch.Visible=true;
			}
			else {
				labelDatabaseSynch.Visible=false;
			}
			if(LocalOutOfSynch()) {
				labelLocalSynch.Visible=true;
			}
			else {
				labelLocalSynch.Visible=false;
			}
			if(!ServerOurOfSynch()&&!LocalOutOfSynch()) { //If both in synch
				labelAllSynched.Visible=true;
			}
			else {
				labelAllSynched.Visible=false;
			}

		}

		///<summary>Returns true if server time is in synch with Nist server.</summary>
		private bool ServerOurOfSynch() {
			double difference=Math.Abs(timeServer.Subtract(timeNist).TotalSeconds);
			if(difference>.99) {
				return true;
			}
			return false;
		}

		///<summary>Returns true if local time is in synch with server.</summary>
		private bool LocalOutOfSynch() {
			double difference=Math.Abs(timeLocal.Subtract(timeServer).TotalSeconds);
			if(difference>.99) {
				return true;
			}
			return false;
		}

		///<summary>Refresh the time textboxes.  Stops users from sending requests to NIST server more than once every 4 seconds.</summary>
		private void butRefreshTime_Click(object sender,EventArgs e) {
			if(timer1.Enabled) {
				MsgBox.Show(this,"Cannot send a time request more than once every four seconds");
				return;
			}
			RefreshTimes();
		}

		///<summary>Pull the time from the server and use it to set the local time.</summary>
		private void butSynchTime_Click(object sender,EventArgs e) {
			this.Cursor=Cursors.WaitCursor;
			DateTime timeNew=MiscData.GetNowDateTimeWithMilli();
			WindowsTime.SetTime(timeNew);
			this.Cursor=Cursors.Default;
			MessageBox.Show("Local time updated to "+timeNew.ToLongTimeString());
		}

		///<summary>Do not allow user to send another request until timer has ticked.</summary>
		private void timer1_Tick(object sender,EventArgs e) {
			timer1.Enabled=false;
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}


	}
}