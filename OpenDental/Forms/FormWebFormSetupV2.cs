using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {

	/// <summary>
	/// This Form is for the 'next' version of the Webforms.
	/// This Form is primarily used by the dental office to set various UI parameters of a webform: eg. border colors and heading text.
	/// </summary>
	public partial class FormWebFormSetupV2:Form {

		private string WebFormAddress="";
		string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
		string SheetDefAddress ="";
		WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
		OpenDental.WebHostSynch.webforms_sheetdef[] sheetDefList;
		List<SheetDef> SheetDefListLocal;
		List<long> SheetsDefsForDeletion;
		long DentalOfficeID=0;
		

		public FormWebFormSetupV2() {
			InitializeComponent();
			Lan.F(this);
			InitializeVariables();
		}

		private void FormWebFormSetupV2_Load(object sender,EventArgs e) {
			//this.backgroundWorker1.RunWorkerAsync();
			#if DEBUG
				IgnoreCertificateErrors();// used with faulty certificates only while debugging.
			#endif
				FillGrid();
				
		}


		private void InitializeVariables() {
			wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
			sheetDefList=wh.DownloadSheetDefs(RegistrationKey);
			SheetDefAddress= wh.GetSheetDefAddress(RegistrationKey);
			SheetDefListLocal= new List<SheetDef>();
			SheetsDefsForDeletion=new List<long>();
			DentalOfficeID=wh.GetDentalOfficeID(RegistrationKey);
			}


		/// <summary>
		/// Ignore this method - this is for the 'next' version of the Webforms.
		/// Here sheetDef can be uploaded to the web form Open Dental
		/// </summary>
		private void SheetDefUpload() {
			try {
				/* for this line to compile one must modify the Reference.cs file in to the Web references folder. The SheetDef and related classes with namespaces of WebHostSync must be removed so that the SheetDef Class of OpenDentBusiness is used
	*/
				wh.UpLoadSheetDef(RegistrationKey,SheetDefListLocal.ToArray());
				wh.DeleteSheetDefs(RegistrationKey,SheetsDefsForDeletion.ToArray());
				InitializeVariables();
				FillGrid();

			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
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
				///do stuff here and return true or false accordingly.
				///In this particular case it always returns true i.e accepts any certificate.
				/* sample code 
				if(sslPolicyErrors==System.Net.Security.SslPolicyErrors.None) return true;
				// the sample below allows expired certificates
				foreach(X509ChainStatus s in chain.ChainStatus) {
					// allows expired certificates
					if(string.Equals(s.Status.ToString(),"NotTimeValid",
						StringComparison.OrdinalIgnoreCase)) {
						return true;
					}						
				}*/
				return true;
			};
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
			//textBoxWebFormAddress.Text=WebFormAddress; //the textbox is set here because it will thow an error if put under _Dowork
		}

		private void backgroundWorker1_DoWork(object sender,DoWorkEventArgs e) {
			GetWebFormAddress();
		}

		/// <summary>Only called from worker thread.</summary>
		private void GetWebFormAddress() {
			try{
				if(wh.GetDentalOfficeID(RegistrationKey)==0) {
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				WebFormAddress=wh.GetWebFormAddress(RegistrationKey);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}


		private void butAdd_Click(object sender,EventArgs e) {
			
			FormSheetPicker FormS=new FormSheetPicker();
			FormS.SheetType=SheetTypeEnum.PatientForm;
			FormS.HideKioskButton=true;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}

			for(int i=0;i<FormS.SelectedSheetDefs.Count;i++) {

				if(!SheetDefListLocal.Exists(sd => sd.SheetDefNum==FormS.SelectedSheetDefs[i].SheetDefNum)) {
					SheetDefListLocal.Add(FormS.SelectedSheetDefs[i]);
				}

			}

			FillGrid();


		}

		private void butDelete_Click(object sender,EventArgs e) {
			//long sheetDefNum=(long)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;

			if(gridMain.Rows[gridMain.SelectedIndices[0]].Tag.GetType()==typeof(SheetDef)) {
				SheetDef sheetDef=(SheetDef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
				SheetDefListLocal.Remove(SheetDefListLocal.Find(sd => sd.SheetDefNum==sheetDef.SheetDefNum));
			}
			else {

				OpenDental.WebHostSynch.webforms_sheetdef WebSheetDef= (OpenDental.WebHostSynch.webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
				SheetsDefsForDeletion.Add(WebSheetDef.WebSheetDefNum);
			}

			
			FillGrid();
		}

		private void butUploadSheetDefs_Click(object sender,EventArgs e) {
			SheetDefUpload();
			

		}

		private void FillGrid() {

			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Sheet Num"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Status"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Browser Address For Patients"),510);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();



			for(int i=0;i<sheetDefList.Length;i++) {
				ODGridRow row=new ODGridRow();

				row.Tag=sheetDefList[i];
				row.Cells.Add(sheetDefList[i].SheetDefNum+"");
				row.Cells.Add(sheetDefList[i].Description);

				if(SheetsDefsForDeletion.Exists(wsn => wsn==sheetDefList[i].WebSheetDefNum)) {
					row.Cells.Add(Lan.g(this,"On Server- marked for deletion"));
				}
				else {
					row.Cells.Add(Lan.g(this,"On Server"));
				}

				String  SheetFormAddress = SheetDefAddress+"?DentalOfficeID="+DentalOfficeID+"&WebSheetDefNum="+sheetDefList[i].WebSheetDefNum;

				row.Cells.Add(SheetFormAddress);
				
				gridMain.Rows.Add(row);
			}


			
				SheetDef sheetDef;


				for(int i=0;i<SheetDefListLocal.Count;i++) {

					sheetDef=SheetDefListLocal[i];
					ODGridRow row=new ODGridRow();
					row.Tag=sheetDef;
					row.Cells.Add(sheetDef.SheetDefNum+"");
					row.Cells.Add(sheetDef.Description);
					row.Cells.Add(Lan.g(this,"Not uploaded"));
					row.Cells.Add(Lan.g(this,"N/A"));
					gridMain.Rows.Add(row);
				}



			gridMain.EndUpdate();

		}
	
	
		private void butOK_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try {
				// update preferences on server
				if(wh.GetDentalOfficeID(RegistrationKey)==0) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				bool PrefSet=true;
				/*bool PrefSet= wh.SetPreferences(RegistrationKey,PrefC.GetColor(PrefName.WebFormsBorderColor).ToArgb(),PrefC.GetStringSilent(PrefName.WebFormsHeading1),PrefC.GetStringSilent(PrefName.WebFormsHeading2));*/
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