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
		}

		private void InitializeVariables() {
			RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
			MobileSyncServerURL=PrefC.GetString(PrefName.MobileSyncServerURL);
			MobileSyncDateTimeLastRun=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			MobileSyncIntervalMinutes=PrefC.GetInt(PrefName.MobileSyncIntervalMinutes);
			MobileExcludeApptsBeforeDate=PrefC.GetDateT(PrefName.MobileExcludeApptsBeforeDate);
		}

		public void SynchPatientRecordsOnMobileWeb() {
			try {
			#if DEBUG
				IgnoreCertificateErrors();// used with faulty certificates only while debugging.
			#endif
			DateTime MobileSyncDateTimeLastRunNew= MiscData.GetNowDateTime();
			List<Patientm> ChangedPatientmList=Patientms.GetChanged(MobileSyncDateTimeLastRun);
			mb.SynchRecords(RegistrationKey,ChangedPatientmList.ToArray());
			Prefs.UpdateDateT(PrefName.MobileSyncDateTimeLastRun,MobileSyncDateTimeLastRunNew);
			textDateTimeLastRun.Text=MobileSyncDateTimeLastRunNew.ToShortDateString()+" "+MobileSyncDateTimeLastRunNew.ToShortTimeString();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
			
		}

		/// <summary>
		/// An empty method to test if the webservice is up and running. This was made with the intention of testing the correctness of the webservice URL. If an incorrect webservice URL is used in a background thread the exception cannot be handled easily to a point where even a correct URL cannot be keyed in by the user. Because an exception in a background thread closes the Form which spawned it.
		/// </summary>
		/// <returns></returns>
		private bool TestWebServiceExists() {
			try {
				mb.Url=url;
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
			InitializeVariables();
			textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
		}

		private void butSavePreferences_Click(object sender,EventArgs e) {
			Prefs.UpdateString(PrefName.MobileSyncServerURL,textboxMobileSyncServerURL.Text.Trim());
			Prefs.UpdateDateT(PrefName.MobileExcludeApptsBeforeDate,PIn.Date(textDateBefore.Text));
			Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,PIn.Int(textBoxSynchMinutes.Text));
		}

		private void butSync_Click(object sender,EventArgs e) {
			//disabled unless user changed url
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			try {
				SynchPatientRecordsOnMobileWeb();
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
			if(textDateBefore.errorProvider1.GetError(textDateBefore)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			SynchPatientRecordsOnMobileWeb();
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