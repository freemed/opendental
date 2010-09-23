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
	/// This Form is for the 'next' version of the Webforms.
	/// This Form is primarily used by the dental office to set various UI parameters of a webform: eg. border colors and heading text.
	/// </summary>
	public partial class FormWebFormSetupV2:Form {

		private string WebFormAddress="";

		public FormWebFormSetupV2() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebFormSetupV2_Load(object sender,EventArgs e) {
			///the line below will allow the code to continue by not throwing an exception.
			///It will accept the security certificate if there is a problem with the security certificate.
			System.Net.ServicePointManager.ServerCertificateValidationCallback+=
				delegate(object sender2,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
				System.Security.Cryptography.X509Certificates.X509Chain chain,
				System.Net.Security.SslPolicyErrors sslPolicyErrors) {
					///do stuff here and return true or false accordingly.
					///In this particular case it always returns true i.e accepts any certificate.
					return true;
				};
			//The only function of the background thread is to fill the WebFormAddressBox from the web server.
			this.backgroundWorker1.RunWorkerAsync();
			textboxWebHostAddress.Text=PrefC.GetString(PrefName.WebHostSynchServerURL);
			butWebformBorderColor.BackColor=PrefC.GetColor(PrefName.WebFormsBorderColor);
			textBoxWebformsHeading1.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading1);
			textBoxWebformsHeading2.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading2);
			//TestSheetUpload();
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
			textBoxWebFormAddress.Text=WebFormAddress; //the textbox is set here because it will thow an error if put under _Dowork
		}

		private void backgroundWorker1_DoWork(object sender,DoWorkEventArgs e) {
			GetWebFormAddress();
		}

		/// <summary>Only called from worker thread.</summary>
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
			try {
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				SheetDef sheetDef=SheetDefs.GetSheetDef(5);
				SheetDef sheetDef1=SheetDefs.GetSheetDef(8);
				List<SheetDef> sheetDefList = new List<SheetDef>();
				sheetDefList.Add(sheetDef);
				sheetDefList.Add(sheetDef1);
				/* for this line to compile one must modify the Reference.cs file in to the Web references folder. The SheetDef and related classes with namespaces of WebHostSync must be removed so that the SheetDef Class of OpenDentBusiness is used
	*/
				wh.UpdateSheetDef(RegistrationKey,sheetDefList.ToArray());
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
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
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				bool PrefSet= wh.SetPreferences(RegistrationKey,PrefC.GetColor(PrefName.WebFormsBorderColor).ToArgb(),PrefC.GetStringSilent(PrefName.WebFormsHeading1),PrefC.GetStringSilent(PrefName.WebFormsHeading2));
				if(PrefSet==false) {
				MsgBox.Show(this,"Preferences could not be set on the server");
				}
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