using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
	public partial class FormWebFormSetup:Form {

		string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
		string SheetDefAddress="";
		WebSheets.Sheets wh=new WebSheets.Sheets();
		OpenDental.WebSheets.webforms_sheetdef[] sheetDefList;
		long DentalOfficeID=0;
		private String SynchUrlStaging="https://192.168.0.196/WebHostSynch/Sheets.asmx";
		private String SynchUrlDev="http://localhost:2923/Sheets.asmx";

		public FormWebFormSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebFormSetup_Load(object sender,EventArgs e) {
		}

		private void FormWebFormSetup_Shown(object sender,EventArgs e) {
			FetchValuesFromWebServer();
		}

		private void FetchValuesFromWebServer() {
			try {
				String WebHostSynchServerURL=PrefC.GetString(PrefName.WebHostSynchServerURL);
				textboxWebHostAddress.Text=WebHostSynchServerURL;
				butSave.Enabled=false;
				//#if DEBUG // ignore the certificate errors for the staging machine and development machine
				if((WebHostSynchServerURL==SynchUrlStaging)||(WebHostSynchServerURL==SynchUrlDev)) {
						IgnoreCertificateErrors();
					}
				//#endif
				Cursor=Cursors.WaitCursor;
				if(!TestWebServiceExists()) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
					return;
				}
				DentalOfficeID=wh.GetDentalOfficeID(RegistrationKey);
				if(wh.GetDentalOfficeID(RegistrationKey)==0) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				OpenDental.WebSheets.webforms_preference PrefObj=wh.GetPreferences(RegistrationKey);
				if(PrefObj==null) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"There has been an error retrieving values from the server");
				}
				butWebformBorderColor.BackColor=Color.FromArgb(PrefObj.ColorBorder);
				SheetDefAddress=wh.GetSheetDefAddress(RegistrationKey);
				//dennis: the below if statement is for backward compatibility only April 14 2011 and can be removed later.
				if(String.IsNullOrEmpty(PrefObj.CultureName)){
					PrefObj.CultureName=System.Globalization.CultureInfo.CurrentCulture.Name;
					wh.SetPreferencesV2(RegistrationKey,PrefObj);
					}
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			FillGrid();//Also gets sheet def list from server
			Cursor=Cursors.Default;
		}

		private void IgnoreCertificateErrors() {
			System.Net.ServicePointManager.ServerCertificateValidationCallback+=
			delegate(object sender,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
				System.Security.Cryptography.X509Certificates.X509Chain chain,
				System.Net.Security.SslPolicyErrors sslPolicyErrors){
				return true;//accept any certificate if debugging
			};
		}

		/// <summary>
		/// An empty method to test if the webservice is up and running. This was made with the intention of testing the correctness of the webservice URL. If an incorrect webservice URL is used in a background thread the exception cannot be handled easily to a point where even a correct URL cannot be keyed in by the user. Because an exception in a background thread closes the Form which spawned it.
		/// </summary>
		/// <returns></returns>
		private bool TestWebServiceExists() {
			try {
				wh.Url=textboxWebHostAddress.Text;
				if(textboxWebHostAddress.Text.Contains("192.168.0.196") || textboxWebHostAddress.Text.Contains("localhost")) {
					IgnoreCertificateErrors();// done so that TestWebServiceExists() does not thow an error.
				}
				if(wh.ServiceExists()){
					return true;
				}
			}
			catch{//(Exception ex) {
				return false;
			}
			return true;
		}

		///<summary>This now also gets a new list of sheet defs from the server.  But it's only called after testing that the web service exists.</summary>
		private void FillGrid() {
			wh.Url=textboxWebHostAddress.Text;
			sheetDefList=wh.DownloadSheetDefs(RegistrationKey);
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Description"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Browser Address For Patients"),510);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			for(int i=0;i<sheetDefList.Length;i++) {
				ODGridRow row=new ODGridRow();
				row.Tag=sheetDefList[i];
				row.Cells.Add(sheetDefList[i].Description);
				String SheetFormAddress=SheetDefAddress+"?DentalOfficeID="+DentalOfficeID+"&WebSheetDefID="+sheetDefList[i].WebSheetDefID;
				row.Cells.Add(SheetFormAddress);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			OpenBrowser();
		}

		private void gridMain_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button==MouseButtons.Right) {
				menuWebFormSetupRight.Show(gridMain,new Point(e.X,e.Y));
			}
		}

		private void menuItemNavigateURL_Click(object sender,EventArgs e) {
			OpenBrowser();
		}

		private void menuItemCopyURL_Click(object sender,EventArgs e) {
			OpenDental.WebSheets.webforms_sheetdef WebSheetDef=(OpenDental.WebSheets.webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			String SheetFormAddress=SheetDefAddress+"?DentalOfficeID="+DentalOfficeID+"&WebSheetDefID="+WebSheetDef.WebSheetDefID;
			Clipboard.SetText(SheetFormAddress);
		}

		private void OpenBrowser() {
			OpenDental.WebSheets.webforms_sheetdef WebSheetDef=(OpenDental.WebSheets.webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			String SheetFormAddress=SheetDefAddress+"?DentalOfficeID="+DentalOfficeID+"&WebSheetDefID="+WebSheetDef.WebSheetDefID;
			System.Diagnostics.Process.Start(SheetFormAddress);
		}

		private void textboxWebHostAddress_TextChanged(object sender,EventArgs e) {
			butSave.Enabled=true;
		}

		private void butSave_Click(object sender,EventArgs e) {
			//disabled unless user changed url
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			try {
				Prefs.UpdateString(PrefName.WebHostSynchServerURL,textboxWebHostAddress.Text.Trim());
				butSave.Enabled=false;
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			FetchValuesFromWebServer();
			Cursor=Cursors.Default;
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
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect.");
				return;
			}
			try {
				if(wh.GetDentalOfficeID(RegistrationKey)==0) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key incorrect.");
					return;
				}
				OpenDental.WebSheets.webforms_preference PrefObj=new OpenDental.WebSheets.webforms_preference();
				PrefObj.ColorBorder=butWebformBorderColor.BackColor.ToArgb();
				PrefObj.CultureName=System.Globalization.CultureInfo.CurrentCulture.Name;
				bool IsPrefSet=wh.SetPreferencesV2(RegistrationKey,PrefObj);
				Cursor=Cursors.Default;
				if(!IsPrefSet) {
					MsgBox.Show(this,"Error, color could not be saved to server.");
				}
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
		}

		private void LoadImagesToSheetDef(SheetDef sheetDefCur){
			for(int j=0;j<sheetDefCur.SheetFieldDefs.Count;j++) {
				try {
					if(sheetDefCur.SheetFieldDefs[j].FieldType==SheetFieldType.Image) {
						string filePathAndName=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),sheetDefCur.SheetFieldDefs[j].FieldName);
						Image img=null;
						if(sheetDefCur.SheetFieldDefs[j].FieldName=="Patient Info.gif") {
							img=Properties.Resources.Patient_Info;
						}
						else if(File.Exists(filePathAndName)) {
							img=Image.FromFile(filePathAndName);
						}
						//sheetDefCur.SheetFieldDefs[j].ImageData=POut.Bitmap(new Bitmap(img),ImageFormat.Png);//Because that's what we did before. Review this later. 
						long fileByteSize=0;
						using(MemoryStream ms = new MemoryStream()) {
							img.Save(ms,img.RawFormat); // done solely to compute the file size of the image
							fileByteSize = ms.Length;
						}
						if(fileByteSize>2000000) {
							//for large images greater that ~2MB use jpeg format for compression. Large images in the 4MB + range have difficulty being displayed. It could be an issue with MYSQL or ASP.NET
							sheetDefCur.SheetFieldDefs[j].ImageData=POut.Bitmap(new Bitmap(img),ImageFormat.Jpeg);
						}
						else {
							sheetDefCur.SheetFieldDefs[j].ImageData=POut.Bitmap(new Bitmap(img),img.RawFormat);
						}
						
						
					}
					else {
						sheetDefCur.SheetFieldDefs[j].ImageData="";// because null is not allowed
					}
				}
				catch(Exception ex) {
					sheetDefCur.SheetFieldDefs[j].ImageData="";
					MessageBox.Show(ex.Message);
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
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			for(int i=0;i<FormS.SelectedSheetDefs.Count;i++) {
				LoadImagesToSheetDef(FormS.SelectedSheetDefs[i]);
				wh.Timeout=300000; //for slow connections more timeout is provided.The  default is 100 seconds i.e 100000
				wh.UpLoadSheetDef(RegistrationKey,FormS.SelectedSheetDefs[i]);
			}
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select an item from the grid first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			OpenDental.WebSheets.webforms_sheetdef wf_sheetDef=(OpenDental.WebSheets.webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			wh.DeleteSheetDef(RegistrationKey,wf_sheetDef.WebSheetDefID);
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}




		

		








		








	}
}