using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OpenDentBusiness;

namespace OpenDental {

	/// <summary>
	/// This Form is primarily used by the dental office to set various UI parameters of a webform: eg. border colors and heading text.
	/// </summary>
	public partial class FormWebFormSetup:Form {

		private string WebFormAddress="";
		private OpenDental.WebHostSynch.webforms_preference PrefObj=null;

		public FormWebFormSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebFormSetup_Load(object sender,EventArgs e) {

		}
		private void FormWebForms_Shown(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			///the line below will allow the code to continue by not throwing an exception.
			///It will accept the security certificate if there is a problem with the security certificate.
			/*
			System.Net.ServicePointManager.ServerCertificateValidationCallback+=
				delegate(object sender2,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
				System.Security.Cryptography.X509Certificates.X509Chain chain,
				System.Net.Security.SslPolicyErrors sslPolicyErrors) {
					///do stuff here and return true or false accordingly.
					///In this particular case it always returns true i.e accepts any certificate.
					return true;
				};
			*/
			//The function of the background thread fetch the settings from the web server.
			this.backgroundWorker1.RunWorkerAsync();
			textboxWebHostAddress.Text=PrefC.GetString(PrefName.WebHostSynchServerURL);
			/* Dennis delete later
			butWebformBorderColor.BackColor=PrefC.GetColor(PrefName.WebFormsBorderColor);
			textBoxWebformsHeading1.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading1);
			textBoxWebformsHeading2.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading2);
			 * */
		}

		private void butWebformBorderColor_Click(object sender,EventArgs e) {
			ShowColorDialog();
		}

		private void butChange_Click(object sender,EventArgs e) {
			ShowColorDialog();
		}

		private void ShowColorDialog(){
			colorDialog1.Color=butWebformBorderColor.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK) {
				return;
			}
			butWebformBorderColor.BackColor=colorDialog1.Color;
		}


		private void backgroundWorker1_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
			//these values are set here because it will thow an error if put under _Dowork
			textBoxWebFormAddress.Text=WebFormAddress;
			butWebformBorderColor.BackColor=Color.FromArgb(PrefObj.ColorBorder);
			textBoxWebformsHeading1.Text=PrefObj.Heading1;
			textBoxWebformsHeading2.Text=PrefObj.Heading2;
			Cursor=Cursors.Default;
	
		}

		private void backgroundWorker1_DoWork(object sender,DoWorkEventArgs e) {
			GetFieldValuesFromServer();
		}

		/// <summary>Only called from worker thread.</summary>
		private void GetFieldValuesFromServer() {
			try{
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				if(wh.CheckRegistrationKey(RegistrationKey)==false) {
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				WebFormAddress=wh.GetWebFormAddress(RegistrationKey);
				PrefObj=wh.GetPreferences(RegistrationKey);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				// the code below used to prevent this form from being automatically closed. It appears that if the backgroundWorker1 is abrubtly exited then the form also closes. this code allows it to gracefully terminate.
				// this has to be replace by a more elegent method because the hourglass still shows.
				backgroundWorker1.WorkerSupportsCancellation=true;
				backgroundWorker1.CancelAsync();   // ask the backgroundWorker1 to stop
				// Wait when it really exits
				while(backgroundWorker1.IsBusy) {
					System.Threading.Thread.Sleep(100);
				}
				
				
			}
		}
		
		private void butOK_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try {
				/* Dennis delete later
				Prefs.UpdateLong(PrefName.WebFormsBorderColor,butWebformBorderColor.BackColor.ToArgb());
				Prefs.UpdateString(PrefName.WebFormsHeading1,textBoxWebformsHeading1.Text.Trim());
				Prefs.UpdateString(PrefName.WebFormsHeading2,textBoxWebformsHeading2.Text.Trim());
				 * */
				Prefs.UpdateString(PrefName.WebHostSynchServerURL,textboxWebHostAddress.Text.Trim());
				// update preferences on server
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				if(wh.CheckRegistrationKey(RegistrationKey)==false) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				wh.SetPreferences(RegistrationKey,butWebformBorderColor.BackColor.ToArgb(),textBoxWebformsHeading1.Text.Trim(),textBoxWebformsHeading2.Text.Trim());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}





	}
}