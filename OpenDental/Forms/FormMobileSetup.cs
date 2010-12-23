using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace OpenDental {
	public partial class FormMobileSetup:Form {
		static string RegistrationKey;
		static MobileWeb.Mobile mb = new MobileWeb.Mobile();
		static DateTime MobileSyncDateTimeLastRun;
		static string MobileSyncServerURL;
		static int MobileSyncIntervalMinutes;
		static DateTime MobileExcludeApptsBeforeDate;

		public FormMobileSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormMobileSetup_Load(object sender,EventArgs e) {
			InitializeVariables();
			textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
			textboxMobileSyncServerURL.Text=MobileSyncServerURL;
			textBoxSynchMinutes.Text=MobileSyncIntervalMinutes+"";
			textDateBefore.Text=MobileExcludeApptsBeforeDate.ToShortDateString();
			butSavePreferences.Enabled=false;
		}

		private void InitializeVariables() {
			
			RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
			MobileSyncServerURL=PrefC.GetString(PrefName.MobileSyncServerURL);
			MobileSyncDateTimeLastRun=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			MobileSyncIntervalMinutes=PrefC.GetInt(PrefName.MobileSyncIntervalMinutes);
			MobileExcludeApptsBeforeDate=PrefC.GetDateT(PrefName.MobileExcludeApptsBeforeDate);
		}

		public void SynchPatientRecordsOnMobileWeb(DateTime GetChangedSince) {
			try {
				#if DEBUG
					IgnoreCertificateErrors();// used with faulty certificates only while debugging.
				#endif
					if(mb.GetCustomerNum(RegistrationKey)==0) {
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				DateTime MobileSyncDateTimeLastRunNew= MiscData.GetNowDateTime();
				List<Patientm> ChangedPatientmList=Patientms.GetChanged(GetChangedSince);
				mb.SynchPatients(RegistrationKey,ChangedPatientmList.ToArray());
				//m
				Prefs.UpdateDateT(PrefName.MobileSyncDateTimeLastRun,MobileSyncDateTimeLastRunNew);
				MobileSyncDateTimeLastRun=MobileSyncDateTimeLastRunNew;
				
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void SynchPatientRecordsOnMobileWeb() {
			SynchPatientRecordsOnMobileWeb(MobileSyncDateTimeLastRun);
		}

		public void SynchPatientFull() {
			DateTime FullSynchDateTime=new DateTime(1880,1,1);
			SynchPatientRecordsOnMobileWeb(FullSynchDateTime);
		}
					
		/// <summary>
		/// An empty method to test if the webservice is up and running. This was made with the intention of testing the correctness of the webservice URL. If an incorrect webservice URL is used in a background thread the exception cannot be handled easily to a point where even a correct URL cannot be keyed in by the user. Because an exception in a background thread closes the Form which spawned it.
		/// </summary>
		/// <returns></returns>
		private bool TestWebServiceExists() {
			try {
				mb.Url=MobileSyncServerURL;
				if(mb.ServiceExists()) {
					return true;
				}
			}
			catch {//(Exception ex) {
				return false;
			}
			return true;
		}

		/// <summary>
		///  This method is used only for testing with security certificates that has problems.
		/// </summary>
		private void IgnoreCertificateErrors() {
			///the line below will allow the code to continue by not throwing an exception.
			///It will accept the security certificate if there is a problem with the security certificate.
			System.Net.ServicePointManager.ServerCertificateValidationCallback+=
			delegate(object sender,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
									System.Security.Cryptography.X509Certificates.X509Chain chain,
									System.Net.Security.SslPolicyErrors sslPolicyErrors) {
				return true;
			};
		}

		private void timerRefreshLastSynchTime_Tick(object sender,EventArgs e) {
			textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
		}

		private void butSavePreferences_Click(object sender,EventArgs e) {
			Prefs.UpdateString(PrefName.MobileSyncServerURL,textboxMobileSyncServerURL.Text.Trim());
			Prefs.UpdateDateT(PrefName.MobileExcludeApptsBeforeDate,PIn.Date(textDateBefore.Text));
			Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,PIn.Int(textBoxSynchMinutes.Text));
			butSavePreferences.Enabled=false;
			MobileSyncServerURL=textboxMobileSyncServerURL.Text.Trim();
			MobileSyncIntervalMinutes=PIn.Int(textBoxSynchMinutes.Text);
			MobileExcludeApptsBeforeDate=PIn.Date(textDateBefore.Text);
		}

		private void textboxMobileSyncServerURL_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private void textBoxSynchMinutes_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private void butSync_Click(object sender,EventArgs e) {
			if(MobileSyncDateTimeLastRun.Year<1880) {
				// should a patient full synch be forced at startup?
				//MsgBox.Show(this,"Sync has never been run.  You must do a full sync first.");
				//return;
			}
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			if(textDateBefore.errorProvider1.GetError(textDateBefore)!=""){
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			Prefs.UpdateDateT(PrefName.MobileExcludeApptsBeforeDate,PIn.Date(textDateBefore.Text));
			MobileExcludeApptsBeforeDate=PIn.Date(textDateBefore.Text);
			try {
				SynchPatientRecordsOnMobileWeb();
				textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			Cursor=Cursors.Default;
		}

		private void butFullSync_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"This will be time consuming.  Continue anyway?")) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			SynchPatientFull();
			Cursor=Cursors.Default;
			/*if(objCount==0) {
				MsgBox.Show(this,"Done. No sync necessary.");
			}
			else {
				MessageBox.Show(Lan.g(this,"Done.  Objects exported: ")+objCount.ToString());
			}*/
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}










	}
}