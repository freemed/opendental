using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using OpenDental.UI;
using OpenDentBusiness;
using System.Threading;

namespace OpenDental {
	public partial class FormWebFormsV2:Form {
		
		/// <summary>
		/// This Form does 3 things: 
		/// 1) Retrieve data of filled out web forms from a web service and convert them into sheets and patients. Using the first name, last name and birth date it will check for existing patients. If an existing patient is found a new sheet is created. If no patient is found, a  patient and a sheet is created.
		/// 2) Send a list of the Sheets that have been created to the Server for deletion.
		/// 3)Show all the sheets that have been created in 1) using a date filter.
		/// </summary>
		public FormWebFormsV2() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebFormsV2_Load(object sender,EventArgs e) {

		}

		/// <summary>
		/// Code in this method was not put into the Form load event because often the "No Patient forms available" Meassage would popup even before a form is loaded - which could confuse the user.
		/// </summary>
		private void FormWebFormsV2_Shown(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
			FillGrid();
		}

		/// <summary>
		/// </summary>
		private void FillGrid() {
			DateTime dateFrom=DateTime.Today;
			DateTime dateTo=DateTime.Today;
			try {
				dateFrom=PIn.Date(textDateStart.Text);//handles blank
				if(textDateEnd.Text!=""){//if it is blank, default to today
					dateTo=PIn.Date(textDateEnd.Text);
				}
			}
			catch{
				MsgBox.Show(this,"Invalid date");
				return;
			}
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Time"),42);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Patient Last Name"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Patient First Name"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),210);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			DataTable table=Sheets.GetWebFormSheetsTable(dateFrom,dateTo);
			for(int i=0;i<table.Rows.Count;i++) {
				long patNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
				long sheetNum=PIn.Long(table.Rows[i]["SheetNum"].ToString());
				Patient pat=Patients.GetPat(patNum);
				if(pat!=null) {
					ODGridRow row=new ODGridRow();
					row.Cells.Add(table.Rows[i]["date"].ToString());
					row.Cells.Add(table.Rows[i]["time"].ToString());
					row.Cells.Add(pat.LName);
					row.Cells.Add(pat.FName);
					row.Cells.Add(table.Rows[i]["description"].ToString());
					row.Tag=sheetNum;
					gridMain.Rows.Add(row);
				}
			} 
			gridMain.EndUpdate();
		}

		private void RetrieveAndSaveData() {
			try {
				#if DEBUG
				//IgnoreCertificateErrors();// used with faulty certificates only while debugging.
				#endif
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				if(wh.CheckRegistrationKey(RegistrationKey)==false) {
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}

				OpenDental.WebHostSynch.SheetAndSheetField[] sAnds=wh.GetSheets(RegistrationKey);
				List<long> SheetsForDeletion=new List<long>();
				if(sAnds.Count()==0) {
					MsgBox.Show(this,"No Patient forms retrieved from server");
					return;
				}
				//loop through all incoming sheets
				for(int i=0;i<sAnds.Length;i++) {

					long PatNum=0;

					string LastName="";
					string FirstName="";
					string BirthDate="";
					//loop through each variable in a single sheetfield to get First name, last name and DOB
					for(int j=0;j<sAnds[i].sf.Count();j++) {
	
						if(sAnds[i].sf[j].FieldName.ToLower().Contains("lname")||sAnds[i].sf[j].FieldName.ToLower().Contains("lastname")) {
							LastName=sAnds[i].sf[j].FieldValue;
						}
						if(sAnds[i].sf[j].FieldName.ToLower().Contains("fname")||sAnds[i].sf[j].FieldName.ToLower().Contains("firstname")) {
							FirstName=sAnds[i].sf[j].FieldValue;
						}
						if(sAnds[i].sf[j].FieldName.ToLower().Contains("bdate")||sAnds[i].sf[j].FieldName.ToLower().Contains("birthdate")) {
							BirthDate=sAnds[i].sf[j].FieldValue;
						}

					}// end of j loop

					DateTime birthDate=PIn.Date(BirthDate);
					if(birthDate.Year==1) {
						//log invalid birth date  format
					}
					PatNum=Patients.GetPatNumByNameAndBirthday(LastName,FirstName,birthDate);
					Patient newPat=null;

					if(PatNum==0) {
						newPat=CreatePatient(LastName,FirstName,birthDate);
						PatNum=newPat.PatNum;
					}
					Sheet newSheet=CreateSheet(PatNum,sAnds[i]);
					if(DataExistsInDb(newSheet)==true) {
						SheetsForDeletion.Add(sAnds[i].sh.SheetID);
					}
				}// end of for loop
				wh.DeleteSheetData(RegistrationKey,SheetsForDeletion.ToArray());
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// compare values of the new patient or the new sheet with values that have been inserted into the db if false is returned then there is a mismatch.
		/// </summary>
		private bool DataExistsInDb(Sheet newSheet) {
			bool dataExistsInDb=true;
			if(newSheet!=null) {
				long SheetNum=newSheet.SheetNum;
				Sheet sheetFromDb=Sheets.GetSheet(SheetNum);
				if(sheetFromDb!=null) {
					dataExistsInDb=CompareSheets(sheetFromDb,newSheet);
				}
			}
			return dataExistsInDb;
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

		/// <summary>
		/// </summary>
		private Patient CreatePatient(String LastName,String FirstName,DateTime birthDate) {
			Patient newPat=new Patient();
			newPat.LName=LastName;
			newPat.FName=FirstName;
			newPat.Birthdate=birthDate;
			try{
				Patients.Insert(newPat,false);
				//set Guarantor field the same as PatNum
				Patient patOld=newPat.Copy();
				newPat.Guarantor=newPat.PatNum;
				Patients.Update(newPat,patOld);
			}
			catch(Exception e) {
				gridMain.EndUpdate();
				MessageBox.Show(e.Message);
			}
			return newPat;
		}

		/// <summary>
		/// </summary>
		private Sheet CreateSheet(long PatNum,WebHostSynch.SheetAndSheetField sAnds) {
			Sheet newSheet=null;
			try{
					SheetDef sheetDef=new SheetDef((SheetTypeEnum)sAnds.sh.SheetType);
					newSheet=SheetUtil.CreateSheet(sheetDef,PatNum);
					SheetParameter.SetParameter(newSheet,"PatNum",PatNum);
					newSheet.DateTimeSheet=sAnds.sh.DateTimeSheet;
					//newSheet.Description=sAnds.sh.de add the descrption fields also
					newSheet.Height=sAnds.sh.Height;
					newSheet.Width=sAnds.sh.Width;
					newSheet.FontName=sAnds.sh.FontName;
					newSheet.FontSize=sAnds.sh.FontSize;
					newSheet.SheetType=(SheetTypeEnum)sAnds.sh.SheetType;
					newSheet.IsLandscape=sAnds.sh.IsLandscape==(sbyte)1?true:false;

					newSheet.InternalNote="";
					newSheet.IsWebForm=true;

					//loop through each variable in a single sheetfield
					for(int i=0;i<sAnds.sf.Count();i++) {
						SheetField sheetfield=new SheetField();
						sheetfield.FieldName=sAnds.sf[i].FieldName;
						sheetfield.FieldType=(SheetFieldType)sAnds.sf[i].FieldType;
						sheetfield.FontIsBold=sAnds.sf[i].FontIsBold==(sbyte)1?true:false; ;
						sheetfield.FontName=sAnds.sf[i].FontName;
						sheetfield.FontSize=sAnds.sf[i].FontSize;
						sheetfield.Height=sAnds.sf[i].Height;
						sheetfield.Width=sAnds.sf[i].Width;
						sheetfield.XPos=sAnds.sf[i].XPos;
						sheetfield.YPos=sAnds.sf[i].YPos;
						sheetfield.IsRequired=sAnds.sf[i].IsRequired==(sbyte)1?true:false; ;
						sheetfield.RadioButtonGroup=sAnds.sf[i].RadioButtonGroup;
						sheetfield.RadioButtonValue=sAnds.sf[i].RadioButtonValue;
						sheetfield.GrowthBehavior=(GrowthBehaviorEnum)sAnds.sf[i].GrowthBehavior;
						sheetfield.FieldValue=sAnds.sf[i].FieldValue;
						newSheet.SheetFields.Add(sheetfield);

					}// end of j loop

					Sheets.SaveNewSheet(newSheet);
					return newSheet;
			}
			catch(Exception e) {
				gridMain.EndUpdate();
				MessageBox.Show(e.Message);
			}
			return newSheet;
		}


		/// <summary>
		/// </summary>
		private void FillPatientFields(Patient newPat,FieldInfo field,string SheetWebFieldValue) {
			try {
				switch(field.Name) {
					case "Birthdate":
						DateTime birthDate=PIn.Date(SheetWebFieldValue);
						field.SetValue(newPat,birthDate);
						break;
					case "Gender":
						if(SheetWebFieldValue=="M") {
							field.SetValue(newPat,PatientGender.Male);
						}
						if(SheetWebFieldValue=="F") {
							field.SetValue(newPat,PatientGender.Female);
						}
						break;
					case "Position":
						if(SheetWebFieldValue=="Y") {
							field.SetValue(newPat,PatientPosition.Married);
						}
						if(SheetWebFieldValue=="N") {
							field.SetValue(newPat,PatientPosition.Single);
						}
						break;
					case "PreferContactMethod":
					case "PreferConfirmMethod":
					case "PreferRecallMethod":
						if(SheetWebFieldValue=="HmPhone") {
							field.SetValue(newPat,ContactMethod.HmPhone);
						}
						if(SheetWebFieldValue=="WkPhone") {
							field.SetValue(newPat,ContactMethod.WkPhone);
						}
						if(SheetWebFieldValue=="WirelessPh") {
							field.SetValue(newPat,ContactMethod.WirelessPh);
						}
						if(SheetWebFieldValue=="Email") {
							field.SetValue(newPat,ContactMethod.Email);
						}
						break;
					case "StudentStatus":
						if(SheetWebFieldValue=="Nonstudent") {
							field.SetValue(newPat,"");
						}
						if(SheetWebFieldValue=="Fulltime") {
							field.SetValue(newPat,"F");
						}
						if(SheetWebFieldValue=="Parttime") {
							field.SetValue(newPat,"P");
						}
						break;
					case "ins1Relat":
					case "ins2Relat":
						if(SheetWebFieldValue=="Self") {
							field.SetValue(newPat,Relat.Self);
						}
						if(SheetWebFieldValue=="Spouse") {
							field.SetValue(newPat,Relat.Spouse);
						}
						if(SheetWebFieldValue=="Child") {
							field.SetValue(newPat,Relat.Child);
						}
						break;
					default:
						field.SetValue(newPat,SheetWebFieldValue);
						break;
				}//switch case
			}
			catch(Exception e) {
				gridMain.EndUpdate();
				MessageBox.Show(field.Name + e.Message);
			}
		}

		/// <summary> Dennis: This function is not being used - delete later.
		/// </summary>
		private bool ComparePatients(Patient patientFromDb,Patient newPat) {
			bool isEqual=true;
			foreach(FieldInfo fieldinfo in patientFromDb.GetType().GetFields()) {
				/* these field are to be ignored while comparing because they have different values when extracted from the db */
				if(fieldinfo.Name=="DateTStamp" ||
					fieldinfo.Name=="Age") {
					continue; // code below this line will not be executed for this loop.
				}
				string dbPatientFieldValue="";
				string newPatientFieldValue="";
				//.ToString() works for Int64, Int32, Enum, DateTime(bithdate), Boolean, Double
				if(fieldinfo.GetValue(patientFromDb)!=null) {
					dbPatientFieldValue=fieldinfo.GetValue(patientFromDb).ToString();
				}
				if(fieldinfo.GetValue(newPat)!=null) {
					newPatientFieldValue=fieldinfo.GetValue(newPat).ToString();
				}
				if(dbPatientFieldValue!=newPatientFieldValue) {
					isEqual=false;
				}
			}
			return isEqual;
		}

		/// <summary>
		/// </summary>
		private bool CompareSheets(Sheet sheetFromDb,Sheet newSheet) {
			bool isEqual=true;
			for(int i=0;i<sheetFromDb.SheetFields.Count;i++) {
				// read each parameter of the SheetField like Fontsize,FieldValue, FontIsBold, XPos, YPos etc.
				foreach(FieldInfo fieldinfo in sheetFromDb.SheetFields[i].GetType().GetFields()) {
					string dbSheetFieldValue="";
					string newSheetFieldValue="";
					//.ToString() works for Int64, Int32, Enum, DateTime(bithdate), Boolean, Double
					if(fieldinfo.GetValue(sheetFromDb.SheetFields[i])!=null) {
						dbSheetFieldValue=fieldinfo.GetValue(sheetFromDb.SheetFields[i]).ToString();
					}
					if(fieldinfo.GetValue(newSheet.SheetFields[i])!=null) {
						newSheetFieldValue=fieldinfo.GetValue(newSheet.SheetFields[i]).ToString();
					}
					if(dbSheetFieldValue!=newSheetFieldValue) {
						isEqual=false;
					}


				}
			}
			return isEqual;
		}

		private void butRetrieve_Click(object sender,System.EventArgs e) {
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
				) {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			//if a thread is not used, the RetrieveAndSaveData() Method will freeze the application if the web is slow 
			this.backgroundWorker1.RunWorkerAsync();
		}

		private void butToday_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
			FillGrid();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			//Dennis: For some reason an exception is thrown here when there is a remote exception not found error in the FormWebFormSetup. Will figure out the reason later.
			try {
				FormWebFormSetupV2 formW=new FormWebFormSetupV2();
				formW.ShowDialog();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			long sheetNum=(long)gridMain.Rows[e.Row].Tag;
			Sheet sheet=Sheets.GetSheet(sheetNum);
			FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
			FormSF.ShowDialog();
		}

		private void gridMain_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button==MouseButtons.Right) {
				menuWebFormsRight.Show(gridMain,new Point(e.X,e.Y));
			}
		}

		private void menuItemViewSheet_Click(object sender,EventArgs e) {
			long sheetNum=(long)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			Sheet sheet=Sheets.GetSheet(sheetNum);
			FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
			FormSF.ShowDialog();
		}

		private void menuItemImportSheet_Click(object sender,EventArgs e) {
			long sheetNum=(long)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			Sheet sheet=Sheets.GetSheet(sheetNum);
			FormSheetImport formSI=new FormSheetImport();
			formSI.SheetCur=sheet;
			formSI.ShowDialog();
		}

		private void menuItemViewAllSheets_Click(object sender,EventArgs e) {
			long sheetNum=(long)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			Sheet sheet=Sheets.GetSheet(sheetNum);
			FormPatientForms formP=new FormPatientForms();
			formP.PatNum=sheet.PatNum;
			formP.ShowDialog();
		}
		private void backgroundWorker1_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
			FillGrid(); 
		}

		private void backgroundWorker1_DoWork(object sender,DoWorkEventArgs e) {
			RetrieveAndSaveData();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}









		

	











	}
}