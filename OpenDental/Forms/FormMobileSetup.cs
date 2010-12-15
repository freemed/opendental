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

		static string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
		long DentalOfficeID=0;
		static MobileWeb.Mobile mb = new MobileWeb.Mobile();

		public FormMobileSetup() {
			InitializeComponent();
			Lan.F(this);

		}

		public static void SynchPatientRecordsOnMobileWeb() {
			int RecordCountOfPatientm=Patientms.GetRecordCount(RegistrationKey);
			int RecordCountOfPatient= Patients.GetNumberPatients();

			if(RecordCountOfPatient>RecordCountOfPatientm) {
				SendMissingRecords();
			}

			DateTime changedSince= new DateTime(6,6,6);
			List<Patientm> ChangedPatientmList=Patientms.GetChanged(changedSince);
			mb.SynchRecords(RegistrationKey,ChangedPatientmList.ToArray());
			
		}

		public static void SendMissingRecords() {


		}

		/// <summary>
		/// An empty method to test if the webservice is up and running. This was made with the intention of testing the correctness of the webservice URL. If an incorrect webservice URL is used in a background thread the exception cannot be handled easily to a point where even a correct URL cannot be keyed in by the user. Because an exception in a background thread closes the Form which spawned it.
		/// </summary>
		/// <returns></returns>
		private bool TestWebServiceExists() {
			try {
				//mb.Url="";
				if(mb.ServiceExists()) {
					return true;
				}
			}
			catch {//(Exception ex) {
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			#if DEBUG
				SynchPatientRecordsOnMobileWeb();
			#endif

				//disabled unless user changed url
				Cursor=Cursors.WaitCursor;
				if(!TestWebServiceExists()) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
					return;
				}
				try {
					//Prefs.UpdateString(PrefName.WebHostSynchServerURL,textboxWebHostAddress.Text.Trim());
					//butSave.Enabled=false;
				}
				catch(Exception ex) {
					Cursor=Cursors.Default;
					MessageBox.Show(ex.Message);
				}
			#if DEBUG
				SynchPatientRecordsOnMobileWeb();
			#endif
				Cursor=Cursors.Default;


			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}