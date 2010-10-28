using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	/// <summary>
	/// This Form is primarily used by the dental office to upload sheetDefs
	/// </summary>
	public partial class FormWebFormSetupV2:Form {

		private string WebFormAddress="";
		private OpenDental.WebHostSynch.webforms_preference PrefObj=null;
		string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
		string SheetDefAddress="";
		WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
		OpenDental.WebHostSynch.webforms_sheetdef[] sheetDefList;
		List<SheetDef> SheetDefListLocal;
		List<long> SheetsDefsForDeletion;
		long DentalOfficeID=0;
		bool SheetDefUploaded=false;
		

		public FormWebFormSetupV2() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebFormSetupV2_Load(object sender,EventArgs e) {
		}

		private void FormWebFormSetupV2_Shown(object sender,EventArgs e) {
			textboxWebHostAddress.Text=PrefC.GetString(PrefName.WebHostSynchServerURL);
		#if DEBUG
			IgnoreCertificateErrors();// used with faulty certificates only while debugging.
		#endif
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Please wait for a few seconds while values for this form are fetched from the server")) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(TestWebServiceExists()==true) {
				Cursor=Cursors.WaitCursor;
				this.backgroundWorker1.RunWorkerAsync();
			}
		}

		/// <summary>
		/// An empty method to test if the webservice is up and running. This was made with the intention of testing the correctness of the webservice URL. If an incorrect webservice URL is used in a background thread the exception cannot be handled easily to a point where even a correct URL cannot be keyed in by the user. Because an exception in a background thread closes the Form which spawned it.
		/// </summary>
		/// <returns></returns>
		private bool TestWebServiceExists() {
			try {
			wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				if(wh.ServiceExists()){
				return true;
				}
			}
			catch(Exception ex) {
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return false;
			}
			return true;
		}


		private void InitializeVariables() {
			try {
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				PrefObj=wh.GetPreferences(RegistrationKey);
				sheetDefList=wh.DownloadSheetDefs(RegistrationKey);
				SheetDefAddress=wh.GetSheetDefAddress(RegistrationKey);
				SheetDefListLocal=new List<SheetDef>();
				SheetsDefsForDeletion=new List<long>();
				DentalOfficeID=wh.GetDentalOfficeID(RegistrationKey);
				GetWebFormAddress();
			}
			catch(Exception ex) {
				throw ex;
				//MessageBox.Show(ex.Message);
			}

			}



		private void SheetDefUpload() {
			try {
				for(int i=0;i<SheetDefListLocal.Count;i++){
					LoadImagesToSheetsDefs(SheetDefListLocal[i]);
				}
				wh.SetPreferences(RegistrationKey,butWebformBorderColor.BackColor.ToArgb(),"","");
                /*for this line to compile one must modify the Reference.cs file in to the Web references folder. The folowing changes must be made to the Reference.cs file:
                1)The SheetDef and related classes with namespaces of WebHostSync must be removed 
                2) Add "using OpenDentBusiness;" at the top of the Reference.cs file
                This is done so that the SheetDef Class of OpenDentBusiness is used.
                */
                wh.UpLoadSheetDef(RegistrationKey,SheetDefListLocal.ToArray());
				wh.DeleteSheetDefs(RegistrationKey,SheetsDefsForDeletion.ToArray());
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
			if(e.Error!=null) {
                Cursor=Cursors.Default;
				MessageBox.Show(e.Error.Message);
                return;
			}
            if (PrefObj==null)
            {
                MsgBox.Show(this, "There has been an error in fetching values from the server");
            }
			butWebformBorderColor.BackColor=Color.FromArgb(PrefObj.ColorBorder);
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void backgroundWorker1_DoWork(object sender,DoWorkEventArgs e) {
			if(SheetDefUploaded==true) {
				SheetDefUpload();
				SheetDefUploaded=false;
			}
			InitializeVariables();
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

		private void LoadImagesToSheetsDefs(SheetDef SheetDefCur){
			for(int j=0;j<SheetDefCur.SheetFieldDefs.Count;j++) {
				if(SheetDefCur.SheetFieldDefs[j].FieldType==SheetFieldType.Image) {
					string filePathAndName=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),SheetDefCur.SheetFieldDefs[j].FieldName);
					Image img=null;
					if(SheetDefCur.SheetFieldDefs[j].FieldName=="Patient Info.gif") {
						img=Properties.Resources.Patient_Info;
					}
					else if(File.Exists(filePathAndName)) {
						img=Image.FromFile(filePathAndName);
					}
						SheetDefCur.SheetFieldDefs[j].ImageData=POut.Bitmap(new Bitmap(img));
				}else{
						SheetDefCur.SheetFieldDefs[j].ImageData="";// because null is not allowed
				}
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
				if(!SheetDefListLocal.Exists(sd=>sd.SheetDefNum==FormS.SelectedSheetDefs[i].SheetDefNum)) {
					SheetDefListLocal.Add(FormS.SelectedSheetDefs[i]);
				}
				//internal sheets have SheetDefNum 0
				if(SheetDefListLocal.Exists(sd=>sd.SheetDefNum==FormS.SelectedSheetDefs[i].SheetDefNum && sd.SheetDefNum==0)) {
					if(!SheetDefListLocal.Exists(sd=>sd.Description==FormS.SelectedSheetDefs[i].Description)) {
						SheetDefListLocal.Add(FormS.SelectedSheetDefs[i]);
					}
				}
			}
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.Rows[gridMain.SelectedIndices[0]].Tag.GetType()==typeof(SheetDef)) {
				SheetDef sheetDef=(SheetDef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
				SheetDefListLocal.Remove(SheetDefListLocal.Find(sd=>sd.SheetDefNum==sheetDef.SheetDefNum));
			}
			else {
				OpenDental.WebHostSynch.webforms_sheetdef WebSheetDef=(OpenDental.WebHostSynch.webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
				SheetsDefsForDeletion.Add(WebSheetDef.WebSheetDefID);
			}
			FillGrid();
		}

		private void butUploadSheetDefs_Click(object sender,EventArgs e) {
			SheetDefUploaded=true;
			this.backgroundWorker1.RunWorkerAsync();
		}

		private void FillGrid() {
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Sheet Num"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Location"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Browser Address For Patients"),510);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			for(int i=0;i<sheetDefList.Length;i++) {
				ODGridRow row=new ODGridRow();
				row.Tag=sheetDefList[i];
				row.Cells.Add(sheetDefList[i].WebSheetDefID+"");
				row.Cells.Add(sheetDefList[i].Description);
				if(SheetsDefsForDeletion.Exists(wsn=>wsn==sheetDefList[i].WebSheetDefID)) {
					row.Cells.Add(Lan.g(this,"On Server- marked for deletion"));
				}
				else {
					row.Cells.Add(Lan.g(this,"On Server"));
				}
				String  SheetFormAddress=SheetDefAddress+"?DentalOfficeID="+DentalOfficeID+"&WebSheetDefID="+sheetDefList[i].WebSheetDefID;
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
		 
		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(gridMain.Rows[gridMain.SelectedIndices[0]].Tag.GetType()==typeof(SheetDef)) {
			}
			else {
				OpenDental.WebHostSynch.webforms_sheetdef WebSheetDef=(OpenDental.WebHostSynch.webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
				String SheetFormAddress=SheetDefAddress+"?DentalOfficeID="+DentalOfficeID+"&WebSheetDefID="+WebSheetDef.WebSheetDefID;
				System.Diagnostics.Process.Start(SheetFormAddress);
			}
		}
	
		private void butOK_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try {
				Prefs.UpdateString(PrefName.WebHostSynchServerURL,textboxWebHostAddress.Text.Trim());
				// update preferences on server
				if(wh.GetDentalOfficeID(RegistrationKey)==0) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				bool PrefSet=true;
				PrefSet=wh.SetPreferences(RegistrationKey,butWebformBorderColor.BackColor.ToArgb(),"","");
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