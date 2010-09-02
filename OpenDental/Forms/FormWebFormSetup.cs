using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormWebFormSetup:Form {
		public FormWebFormSetup() {
			InitializeComponent();
			Lan.F(this);
			butWebformBorderColor.BackColor=PrefC.GetColor(PrefName.WebFormsBorderColor);
			textBoxWebformsHeading1.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading1);
			textBoxWebformsHeading2.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading2);
			textboxWebHostAddress.Text=PrefC.GetString(PrefName.WebHostSynchServerURL);
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
			// hard coded for now
			textBoxWebFormAddress.Text = "https://192.168.0.196/WebForms/WebForm1.aspx?DentalOfficeID=" + GetDentalOfficeID();
			textBoxWebFormAddress.ReadOnly=true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			try {Prefs.UpdateLong(PrefName.WebFormsBorderColor,this.butWebformBorderColor.BackColor.ToArgb());
				Prefs.UpdateString(PrefName.WebFormsHeading1,textBoxWebformsHeading1.Text.Trim());
				Prefs.UpdateString(PrefName.WebFormsHeading2,textBoxWebformsHeading2.Text.Trim());
				Prefs.UpdateString(PrefName.WebHostSynchServerURL,textboxWebHostAddress.Text.Trim());
				// update preferences on server
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url =PrefC.GetString(PrefName.WebHostSynchServerURL);
				if(wh.CheckRegistrationKey(RegistrationKey)==false){
					MessageBox.Show(Lan.g(this,"Registration key provided by the dental office is incorrect"));
					return;
				}
				wh.SetPreferences(RegistrationKey,PrefC.GetColor(PrefName.WebFormsBorderColor).ToArgb(),PrefC.GetStringSilent(PrefName.WebFormsHeading1),PrefC.GetStringSilent(PrefName.WebFormsHeading2));
				//TestSheetUpload();
				DialogResult=DialogResult.OK;
			}catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butWebformBorderColor_Click(object sender,EventArgs e) {
			colorDialog1.Color=butWebformBorderColor.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK) {
				return;
			}
			butWebformBorderColor.BackColor=colorDialog1.Color;
		}

		private long GetDentalOfficeID() {
			long DentalOfficeID=0;
			try{
			string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
			WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
			wh.Url =PrefC.GetString(PrefName.WebHostSynchServerURL);
			if(wh.CheckRegistrationKey(RegistrationKey)==false) {
				MessageBox.Show(Lan.g(this,"Registration key provided by the dental office is incorrect"));
			}
			 DentalOfficeID=wh.GetDentalOfficeID(RegistrationKey);
			}catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
			return DentalOfficeID;
		}

		/// <summary>
		/// Ignore this method - this is for the 'next' version of the Webforms.
		/// Here sheetDef can be uploaded to the web form Open Dental
		/// </summary>
		private void TestSheetUpload(object sender,EventArgs e) {
//pass sheet to webservice
			string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url =PrefC.GetString(PrefName.WebHostSynchServerURL);
				OpenDentBusiness.SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration);
				// for this line to compile one must modify the Reference.cs file in to the Web references folder. The SheetDef and related classes with namespaces of WebHostSync must be removed so that the SheetDef Class of OpenDentBusiness is used
				//wh.ReadSheetDef(sheetDef);
		}
	}
}