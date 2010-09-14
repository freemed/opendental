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

		public FormWebFormSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebFormSetup_Load(object sender,EventArgs e) {
			ShowPreferences();
		}

		private void ShowPreferences() {
			butWebformBorderColor.BackColor=PrefC.GetColor(PrefName.WebFormsBorderColor);
			textBoxWebformsHeading1.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading1);
			textBoxWebformsHeading2.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading2);
			textboxWebHostAddress.Text=PrefC.GetString(PrefName.WebHostSynchServerURL);
			textBoxWebFormAddress.ReadOnly=true;
			///the line below will allow the code to continue by not throwing an exception.
			///It will accept the security certificate if there is a problem with the security certificate.
			System.Net.ServicePointManager.ServerCertificateValidationCallback+=
				delegate(object sender,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
				System.Security.Cryptography.X509Certificates.X509Chain chain,
				System.Net.Security.SslPolicyErrors sslPolicyErrors) {
					///do stuff here and return true or false accordingly.
					///In this particular case it always returns true i.e accepts any certificate.
					return true;
				};
			//if a thread is not used, the GetWebFormAddress() Method will freeze the application if the web is slow 
			this.backgroundWorker1.RunWorkerAsync();
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

		private void SavePreferences() {
			try {
				Prefs.UpdateLong(PrefName.WebFormsBorderColor,butWebformBorderColor.BackColor.ToArgb());
				Prefs.UpdateString(PrefName.WebFormsHeading1,textBoxWebformsHeading1.Text.Trim());
				Prefs.UpdateString(PrefName.WebFormsHeading2,textBoxWebformsHeading2.Text.Trim());
				Prefs.UpdateString(PrefName.WebHostSynchServerURL,textboxWebHostAddress.Text.Trim());
				// update preferences on server
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				if(wh.CheckRegistrationKey(RegistrationKey)==false) {
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				wh.SetPreferences(RegistrationKey,PrefC.GetColor(PrefName.WebFormsBorderColor).ToArgb(),PrefC.GetStringSilent(PrefName.WebFormsHeading1),PrefC.GetStringSilent(PrefName.WebFormsHeading2));
				//TestSheetUpload();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
			textBoxWebFormAddress.Text=WebFormAddress; //the textbox is set here because it will thow an error if put under _Dowork
		}

		private void backgroundWorker1_DoWork(object sender,DoWorkEventArgs e) {
			GetWebFormAddress();
		}

		private void GetWebFormAddress() {
			try{
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				if(wh.CheckRegistrationKey(RegistrationKey)==false) {
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				WebFormAddress=wh.GetWebFormAddress(RegistrationKey);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
			
		}
		

		/// <summary>
		/// Ignore this method - this is for the 'next' version of the Webforms.
		/// Here sheetDef can be uploaded to the web form Open Dental
		/// </summary>
		private void TestSheetUpload() {
			//pass sheet to webservice
			string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
			WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
			wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
			
			OpenDentBusiness.SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration);

			//WebHostSynch.SheetDef sheetDef1= WebHostSynch.SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration);
			/* It's important to note that this is not a new sheetdef class it's the same as in OpenDentBusiness.SheetDef
			 * OpenDentBusiness.SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration);
			 * for this line to compile one must modify the Reference.cs file in to the Web references folder. The SheetDef and related classes with namespaces of WebHostSync must be removed so that the SheetDef Class of OpenDentBusiness is used
*/

			//wh.ReadSheetDef(sheetDef1);
		}

		private void butOK_Click(object sender,EventArgs e) {
			SavePreferences();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}





	}
}