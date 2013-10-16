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
		private DateTime _timeNist;
		private DateTime _timeServer;
		private DateTime _timeLocal;
		///<summary>Set true when launched while OpenDental starts.  Will automatically check times and close form silently if times are all in synch.</summary>
		public bool IsAutoLaunch;

		public FormEhrTimeSynch() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrTime_Load(object sender,EventArgs e) {
			if(IsAutoLaunch) { //Already updated time fields. Dont need to check again, just open.
				return;
			}
			textNistUrl.Text=PrefC.GetString(PrefName.NistTimeServerUrl);
			RefreshTimes();			
		}

		///<summary>Called from FormOpenDental.Load.  Checks to see if all times are in synch, with a fast db call (only acurate to seconds, not miliseconds).</summary>
		public bool TimesInSynchFast() {
			textNistUrl.Text=PrefC.GetString(PrefName.NistTimeServerUrl);
			//Get NistTime Offset
			double ntpOffset=GetNistOffset();
			if(ntpOffset==double.MaxValue) { //Timed out
				MsgBox.Show(this,"Nist request timed out.  Try again in four seconds.");
				this.Cursor=Cursors.Default;
				return false;
			}
			if(ntpOffset==double.MinValue) { //Invalid Nist Server Address
				this.Cursor=Cursors.Default;
				return false;
			}
			//Get current times from offsets
			_timeLocal=DateTime.Now;
			_timeNist=_timeLocal.AddMilliseconds(ntpOffset);
			//Cannot get milliseconds from Now() in Mysql Pre-5.6.4, Only gets whole seconds.
			double serverOffset=(MiscData.GetNowDateTime()-_timeLocal).TotalSeconds;
			_timeServer=_timeLocal.AddSeconds(serverOffset);
			if((ServerInSynchFast() && LocalInSynch())) { //All times in synch
				return true;
			}
			//Some times out of synch, so form will open, but we don't want to make another call to NIST server
			//_timeServer needs to be more accurate before displaying
			serverOffset=(MiscData.GetNowDateTimeWithMilli()-DateTime.Now).TotalSeconds;
			//Update current times from offsets to match new server offset
			_timeLocal=DateTime.Now;
			_timeNist=_timeLocal.AddMilliseconds(ntpOffset);
			_timeServer=_timeLocal.AddSeconds(serverOffset);
			//Updates displays to show when form is open
			labelDatabaseOutOfSynch.Visible=!ServerInSynchFast();
			labelLocalOutOfSynch.Visible=!LocalInSynch();
			textNistTime.Text=_timeNist.ToString("hh:mm:ss.fff tt");
			textServerTime.Text=_timeServer.ToString("hh:mm:ss.fff tt");
			textLocalTime.Text=_timeLocal.ToString("hh:mm:ss.fff tt");
			return false;
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
			if(ntpOffset==double.MinValue) { //Invalid Nist Server Address
				this.Cursor=Cursors.Default;
				return;
			}
			//Cannot get milliseconds from Now() in Mysql Pre-5.6.4, uses a workaround by continuously querying server until second ticks over (milliseconds will be close to 0).
			double serverOffset=(MiscData.GetNowDateTimeWithMilli()-DateTime.Now).TotalMilliseconds;
			//Get current times from offsets
			_timeLocal=DateTime.Now;
			_timeNist=_timeLocal.AddMilliseconds(ntpOffset);
			_timeServer=_timeLocal.AddMilliseconds(serverOffset);
			//Update textboxes
			textNistTime.Text=_timeNist.ToString("hh:mm:ss.fff tt");
			textServerTime.Text=_timeServer.ToString("hh:mm:ss.fff tt");
			textLocalTime.Text=_timeLocal.ToString("hh:mm:ss.fff tt");
			//Update NistURL preference
			Prefs.UpdateString(PrefName.NistTimeServerUrl,textNistUrl.Text);
			this.Cursor=Cursors.Default;
			//Display labels if out of synch.
			labelDatabaseOutOfSynch.Visible=!ServerInSynch();
			labelLocalOutOfSynch.Visible=!LocalInSynch();
			if(labelDatabaseOutOfSynch.Visible || labelLocalOutOfSynch.Visible) {
				labelAllSynched.Visible=false; 
			}
			else {
				labelAllSynched.Visible=true; //All times in synch
			}
		}

		///<summary>Returns true if server time is out of synch with local machine.</summary>
		private bool ServerInSynch() {
			//Would be better to check against NIST time, but doing it this way to match 2014 EHR Proctor Sheet conditions.
			double difference=Math.Abs(_timeServer.Subtract(_timeLocal).TotalSeconds);
			if(difference>.99) {
				return false;
			}
			return true;
		}

		///<summary>Used when launching check automatically on startup.  Rounds to whole seconds.  Returns true if server time is out of synch with local machine.</summary>
		private bool ServerInSynchFast() {
			double difference=Math.Abs(_timeServer.Subtract(_timeLocal).TotalSeconds);
			if(Math.Floor(difference)>1) {
				return false;
			}
			return true;
		}

		///<summary>Returns true if local time is out of synch with NIST server.</summary>
		private bool LocalInSynch() {
			double difference=Math.Abs(_timeLocal.Subtract(_timeNist).TotalSeconds);
			if(difference>.99) {
				return false;
			}
			return true;
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