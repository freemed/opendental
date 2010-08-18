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



namespace OpenDental {
	public partial class FormWebForms:Form {
		public FormWebForms() {
			InitializeComponent();
			Lan.F(this);

		}
		private void FillGrid() {
			try {
				gridMain.BeginUpdate();
				gridMain.Columns.Clear();
				ODGridColumn col=new ODGridColumn(Lan.g("TableWebforms","Last Name"),100);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableWebforms","First Name"),100);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableWebforms","Birth Date"),100);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableWebforms","Status"),100);
				gridMain.Columns.Add(col);
				gridMain.Rows.Clear();


				DateTime dateFrom=PIn.Date(textDateFrom.Text);
				DateTime dateTo=PIn.Date(textDateTo.Text);

				/* this line will continue and accept the security certificate if there is
				 *  a problem with the security certiicate. An exception will not be thrown in this case.
				 * */


				System.Net.ServicePointManager.ServerCertificateValidationCallback +=
				delegate(object sender,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
										System.Security.Cryptography.X509Certificates.X509Chain chain,
										System.Net.Security.SslPolicyErrors sslPolicyErrors) {
					/*do stuff here in necessary and return true or false accordingly.
					 * In this particular case it always returns true i/e accepts any certificate.
					 * */
					return true;
				};
				WebHostSynch.WebHostSynch wh = new WebHostSynch.WebHostSynch();
				//The url  is to be obtained from the preferenes table in the db
				//wh.Url ="https://localhost/WebHostSynch/WebHostSynch.asmx";
				string RegistrationKey = PrefC.GetString(PrefName.RegistrationKey);
				if(wh.CheckRegistrationKey(RegistrationKey)==false) {
					MessageBox.Show(Lan.g(this,"Registration key provided by the dental office is incorrect"));
					return;
				}



				OpenDental.WebHostSynch.webforms_sheetfield[] wbsf = wh.GetSheetData(1,"RegistrationKeyxxxxx",dateFrom,dateTo);

				if(wbsf.Count()==0) {
					MessageBox.Show(Lan.g(this,"No Patient Forms retrieved"));
					return;
				}

				// Select distinct Web sheet ids
				var wbs = (from w in wbsf select w.webforms_sheetReference.EntityKey.EntityKeyValues.First().Value).Distinct();
				var SheetIdArray = wbs.ToArray();
				List<long> SheetsForDeletion = new List<long>();
				// loop through each sheet
				for(int LoopVariable = 0;LoopVariable < SheetIdArray.Length;LoopVariable++) {
					long SheetID=(long)SheetIdArray[LoopVariable];
					var SingleSheet = from w in wbsf where (long)w.webforms_sheetReference.EntityKey.EntityKeyValues.First().Value ==  SheetID
									  select w;
					ODGridRow row=new ODGridRow();
					string LastName="";
					string FirstName="";
					string BirthDate="";
					for(int LoopVariable1 = 0;LoopVariable1 < SingleSheet.Count();LoopVariable1++) {
						String FieldName = SingleSheet.ElementAt(LoopVariable1).FieldName;
						String FieldValue = SingleSheet.ElementAt(LoopVariable1).FieldValue;
						if(FieldName.ToLower().Contains("lastname")) {
							LastName=FieldValue;
						}
						if(FieldName.ToLower().Contains("firstname")) {
							FirstName=FieldValue;
						}
						if(FieldName.ToLower().Contains("birthdate")) {
							BirthDate=FieldValue;
						}
					}

					DateTime birthDate = PIn.Date(BirthDate);
					if(birthDate.Year==1) {
						//log invalid birth date  format
					}
					long PatNum=Patients.GetPatNumByNameAndBirthday(LastName,FirstName,birthDate);
					long NewPatNum = 0;
					Patient newPat= null;
					Sheet newSheet= null;
					row.Cells.Add(LastName);
					row.Cells.Add(FirstName);
					row.Cells.Add(BirthDate);
					if(PatNum==0) {
						newPat= CreateNewPatient(LastName,FirstName,BirthDate);
						NewPatNum = newPat.PatNum;
						row.Cells.Add("New Patient");
						row.Tag=NewPatNum;
					}
					else {
						newSheet = CreateSheet(PatNum,LastName,FirstName,BirthDate);
						row.Cells.Add("Imported");
						row.Tag=PatNum;
					}
					gridMain.Rows.Add(row);
					gridMain.EndUpdate();
					if(DataExistsInDb(newPat,newSheet)==true) {
						SheetsForDeletion.Add(SheetID);
					}
				}// end of for loop
				wh.DeleteSheetData(SheetsForDeletion.ToArray());
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}

		}


		private bool DataExistsInDb(Patient newPat,Sheet newSheet) {

			bool dataExistsInDb=true;
			if(newPat!=null) {
				long PatNum = newPat.PatNum;
				Patient patientFromDb = Patients.GetPat(PatNum);
				if(patientFromDb!=null) {
					dataExistsInDb=ComparePatients(patientFromDb,newPat);
				}
			}

			if(newSheet!=null) {
				long SheetNum = newSheet.SheetNum;
				Sheet sheetFromDb = Sheets.GetSheet(SheetNum);

				if(sheetFromDb!=null) {
					dataExistsInDb=CompareSheets(sheetFromDb,newSheet);
				}
			}

			return dataExistsInDb;
		}

		private Patient CreateNewPatient(string LastName,string FirstName,string BirthDate) {
			Patient newPat=null;
			try {
				newPat=new Patient();
				newPat.LName=LastName;
				newPat.FName=FirstName;
				newPat.Birthdate= PIn.Date(BirthDate);

				/*
						newPat.LName      =PatCur.LName;
						newPat.PatStatus  =PatientStatus.Patient;
						newPat.Address    =PatCur.Address;
						newPat.Address2   =PatCur.Address2;
						newPat.City       =PatCur.City;
						newPat.State      =PatCur.State;
						newPat.Zip        =PatCur.Zip;
						newPat.HmPhone    =PatCur.HmPhone;
						newPat.Guarantor  =PatCur.Guarantor;
						newPat.CreditType =PatCur.CreditType;
						newPat.PriProv    =PatCur.PriProv;
						newPat.SecProv    =PatCur.SecProv;
						newPat.FeeSched   =PatCur.FeeSched;
						newPat.BillingType=PatCur.BillingType;
						newPat.AddrNote   =PatCur.AddrNote;
						newPat.ClinicNum  =PatCur.ClinicNum;
						*/
				Patients.Insert(newPat,false);
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			return newPat;

		}

		private Sheet CreateSheet(long PatNum,string LastName,string FirstName,string BirthDate) {
			Sheet sheet = null;//only useful if not Terminal
			try {
				FormSheetPicker FormS = new FormSheetPicker();
				SheetDef sheetDef;

				sheetDef = SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration);
				sheet = SheetUtil.CreateSheet(sheetDef,PatNum);
				SheetParameter.SetParameter(sheet,"PatNum",PatNum);
				//SheetFiller.FillFields(sheet);
				//SheetUtil.CalculateHeights(sheet, this.CreateGraphics());
				// if (FormS.TerminalSend)
				// {
				sheet.InternalNote = "";//because null not ok
				// sheet.ShowInTerminal = (byte)(Sheets.GetBiggestShowInTerminal(PatNum) + 1);

				// }

				foreach(SheetField fld in sheet.SheetFields) {
					if(fld.FieldName == "LName") {
						fld.FieldValue = LastName;
					}
					if(fld.FieldName == "FName") {
						fld.FieldValue = FirstName;
					}
					if(fld.FieldName == "Birthdate") {
						fld.FieldValue = BirthDate;
					}

				}
				Sheets.SaveNewSheet(sheet);
				return sheet;
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			return sheet;
		}

		private bool ComparePatients(Patient patientFromDb,Patient newPat) {

			bool isEqual = true;
			foreach(FieldInfo fieldinfo in patientFromDb.GetType().GetFields()) {


				/* these field are to be ignored while comparing because they have 
				 * different values when extracted from the db
				*/
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
				if( dbPatientFieldValue!=newPatientFieldValue) {
					isEqual = false;
				}
			}
			return isEqual;
		}


		private bool CompareSheets(Sheet sheetFromDb,Sheet newSheet) {
			bool isEqual = true;
			for(int LoopVariable = 0;LoopVariable < sheetFromDb.SheetFields.Count;LoopVariable++){
				string dbSheetFieldValue=sheetFromDb.SheetFields[LoopVariable].FieldValue.ToString();
				string newSheetFieldValue=newSheet.SheetFields[LoopVariable].FieldValue.ToString();
				if(dbSheetFieldValue!=newSheetFieldValue) {
					isEqual = false;
				}
			}
			return isEqual;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormWebForms_Load(object sender,EventArgs e) {
			SetDates();
		}

		private void butRetrieve_Click(object sender,EventArgs e) {
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				) {
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}

			FillGrid();
		}
		private void SetDates() {
			textDateFrom.Text=DateTime.Today.ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			long PatNum = (long)gridMain.Rows[e.Row].Tag;
			FormPatientForms formP=new FormPatientForms();
			formP.PatNum=PatNum;
			formP.ShowDialog();
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormWebFormSetup formW= new FormWebFormSetup();
			formW.ShowDialog();
		}
	}
}

