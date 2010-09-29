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
	public partial class FormWebForms:Form {
		
		/// <summary>
		/// This Form does 3 things: 
		/// 1) Retrieve data of filled out web forms from a web service and convert them into sheets and patients. Using the first name, last name and birth date it will check for existing patients. If an existing patient is found a new sheet is created. If no patient is found, a  patient and a sheet is created.
		/// 2) Send a list of the Sheets that have been created to the Server for deletion.
		/// 3)Show all the sheets that have been created in 1) using a date filter.
		/// </summary>
		public FormWebForms() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebForms_Load(object sender,EventArgs e) {

		}

		/// <summary>
		/// Code in this method was not put into the Form load event because often the "No Patient forms available" Meassage would popup even before a form is loaded - which could confuse the user.
		/// </summary>
		private void FormWebForms_Shown(object sender,EventArgs e) {
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
			col=new ODGridColumn(Lan.g(this,"Patient Fist Name"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),210);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			DataTable table=Sheets.GetWebFormSheetsTable(dateFrom,dateTo);
			for(int i=0;i<table.Rows.Count;i++) {
				long patNum = PIn.Long(table.Rows[i]["PatNum"].ToString());
				long sheetNum = PIn.Long(table.Rows[i]["SheetNum"].ToString());
				Patient pat = Patients.GetPat(patNum);
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
				OpenDental.WebHostSynch.webforms_sheetfield[] wbsf=wh.GetSheetFieldData(RegistrationKey);
				// The second call GetSheetData is used to retrieve the Datetime the sheet was submitted because as of now I don't quite know how to get it elegently by calling a single method.
				OpenDental.WebHostSynch.webforms_sheet[] SheetDetails=wh.GetSheetData(RegistrationKey);
				if(wbsf.Count()==0) {
					MsgBox.Show(this,"No Patient forms retrieved from server");
					return;
				}
				// Select distinct Web sheet ids
				var wbs=(from w in wbsf select w.webforms_sheetReference.EntityKey.EntityKeyValues.First().Value).Distinct();
				var SheetIdArray=wbs.ToArray();
				List<long> SheetsForDeletion=new List<long>();
				// loop through each sheet
				for(int i=0;i<SheetIdArray.Length;i++) {
					long SheetID=(long)SheetIdArray[i];
					var SingleSheet=from w in wbsf where (long)w.webforms_sheetReference.EntityKey.EntityKeyValues.First().Value==SheetID
						select w;
					//ODGridRow row=new ODGridRow();
					string LastName="";
					string FirstName="";
					string BirthDate="";
					//loop through each variable in a single sheet
					for(int j=0;j<SingleSheet.Count();j++) {
						String FieldName=SingleSheet.ElementAt(j).FieldName;
						String FieldValue=SingleSheet.ElementAt(j).FieldValue;
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
					DateTime birthDate=PIn.Date(BirthDate);
					if(birthDate.Year==1) {
						//log invalid birth date  format
					}
					long PatNum=Patients.GetPatNumByNameAndBirthday(LastName,FirstName,birthDate);
					Patient newPat=null;
					Sheet newSheet=null;
					DateTime SheetDateTimeSubmitted= (from s in SheetDetails where s.SheetID==SheetID
						select s.DateTimeSubmitted).First();
					if(PatNum==0) {
						newPat=CreatePatient(SingleSheet.ToList());
						PatNum=newPat.PatNum;
					}
					newSheet=CreateSheet(PatNum,SheetDateTimeSubmitted,SingleSheet.ToList());
					if(DataExistsInDb(newSheet)==true) {
						SheetsForDeletion.Add(SheetID);
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
		private Patient CreatePatient(List<OpenDental.WebHostSynch.webforms_sheetfield> SingleSheet) {
			Patient newPat=null;
			newPat=new Patient();
			//PatFields must have a one to one mapping with the SheetWebFields
			String[] PatFields={ "LName","FName","MiddleI","Birthdate","Preferred", "Email","SSN",
				"Address","Address2","City","State","Zip",
				"HmPhone","Gender","Position","PreferContactMethod","PreferConfirmMethod",
				"PreferRecallMethod","StudentStatus","WirelessPhone","WkPhone"};
			//other PatFields="PatStatus","Guarantor","CreditType","PriProv","SecProv","FeeSched","BillingType","AddrNote","ClinicNum" EmployerNum, EmploymentNote, GradeLevel, HasIns, InsEst, };
			String[] SheetWebFields={"LastName","FirstName","MI","Birthdate","Preferred","Email","SS",
				"Address1","Address2","City","State","Zip",
				"HomePhone","Gender","Married","MethodContact","MethodConf",
				"MethodRecall","StudentStatus","WirelessPhone","WorkPhone"};
				/*Other SheetWebFields="WholeFamily","WirelessCarrier","Hear","Policy1GroupName","Policy1GroupNumber","Policy1Relationship","Policy1SubscriberName","Policy1SubscriberID","Policy1InsuranceCompany", "Policy1Phone","Policy1Employer","Policy2GroupName","Policy2GroupNumber","Policy2Relationship","Policy2SubscriberName","Policy2SubscriberID","Policy2InsuranceCompany", "Policy2Phone","Policy2Employer","Comments"
				 */
			Type t=newPat.GetType();
			FieldInfo[] fi=t.GetFields();
			try {
				for(int i=0;i<SingleSheet.Count();i++) {
					String SheetWebFieldName=SingleSheet.ElementAt(i).FieldName;
					String SheetWebFieldValue=SingleSheet.ElementAt(i).FieldValue;
					for(int j=0;j<SheetWebFields.Length;j++) {
						if(SheetWebFieldName==SheetWebFields[j]) {// SheetWebFields[j] and PatFields[j] should have a one to one correspondence
							foreach(FieldInfo field in fi) {
								if(field.Name==PatFields[j]) {
									FillPatientFields(newPat,field,SheetWebFieldValue);
								}
							}
						} 
					}// j loop
				}// i loop
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
		private Sheet CreateSheet(long PatNum,DateTime SheetDateTimeSubmitted, List<OpenDental.WebHostSynch.webforms_sheetfield> SingleSheet) {
			Sheet sheet=null;//only useful if not Terminal
			try {
				SheetDef sheetDef;
				sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration);
				sheet=SheetUtil.CreateSheet(sheetDef,PatNum);
				SheetParameter.SetParameter(sheet,"PatNum",PatNum);
				sheet.InternalNote="";//because null not ok
				//SheetFields elements must have a one to one mapping with the SheetWebFields elements.
				String[] SheetFields={"LName","FName","MiddleI","Birthdate","Preferred", "Email","SSN",
									"addressAndHmPhoneIsSameEntireFamily","Address","Address2","City","State","Zip",
									"HmPhone","Gender","Position","PreferContactMethod","PreferConfirmMethod",
									"PreferRecallMethod","StudentStatus","referredFrom","WirelessPhone","wirelessCarrier","WkPhone",
									"ins1GroupName","ins1GroupNum","ins1Relat","ins1SubscriberNameF","ins1SubscriberID","ins1CarrierName","ins1CarrierPhone","ins1EmployerName",
									"ins2GroupName","ins2GroupNum","ins2Relat","ins2SubscriberNameF","ins2SubscriberID","ins2CarrierName","ins2CarrierPhone","ins2EmployerName",
									  "misc"};
				//other SheetFields="PatStatus", "Patient Info.gif","Guarantor","CreditType","PriProv","SecProv","FeeSched","BillingType","AddrNote","ClinicNum" };
				String[] SheetWebFields={"LastName","FirstName","MI","Birthdate","Preferred","Email","SS",
									"WholeFamily","Address1","Address2","City","State","Zip",
									"HomePhone","Gender","Married","MethodContact","MethodConf",
									"MethodRecall","StudentStatus","Hear","WirelessPhone","WirelessCarrier","WorkPhone",
									"Policy1GroupName","Policy1GroupNumber","Policy1Relationship","Policy1SubscriberName","Policy1SubscriberID","Policy1InsuranceCompany", "Policy1Phone","Policy1Employer",
									"Policy2GroupName","Policy2GroupNumber","Policy2Relationship","Policy2SubscriberName","Policy2SubscriberID","Policy2InsuranceCompany", "Policy2Phone","Policy2Employer",
									"Comments",
									   };
				for(int i=0;i<SingleSheet.Count();i++) {
					String SheetWebFieldName=SingleSheet.ElementAt(i).FieldName;
					String SheetWebFieldValue=SingleSheet.ElementAt(i).FieldValue;
					for(int j=0;j<SheetWebFields.Length;j++) {
						if(SheetWebFieldName==SheetWebFields[j]) {// SheetWebFields[j] and SheetFields[j] should have a one to one correspondence
							foreach(SheetField fld in sheet.SheetFields) {
								if(fld.FieldName==SheetFields[j]) {
									FillSheetFields(fld,SheetWebFieldValue);
								}
							}
						} 
					}// j loop
				}// i loop
				sheet.IsWebForm=true;
				sheet.DateTimeSheet=SheetDateTimeSubmitted;
				Sheets.SaveNewSheet(sheet);
				return sheet;
			}
			catch(Exception e) {
				gridMain.EndUpdate();
				MessageBox.Show(e.Message);
			}
			return sheet;
		}

		/// <summary>
		/// </summary>
		private void FillSheetFields(SheetField fld,string SheetWebFieldValue) {
			try {
				switch(fld.FieldName) {
					case "Gender":
						if(fld.RadioButtonValue=="Male") {
							if(SheetWebFieldValue=="M") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="Female") {
							if(SheetWebFieldValue=="F") {
								fld.FieldValue="X";
							}
						}
						break;
					case "Position":
						if(fld.RadioButtonValue=="Married") {
							if(SheetWebFieldValue=="Y") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="Single") {
							if(SheetWebFieldValue=="N") {
								fld.FieldValue="X";
							}
						}
						break;
					case "PreferContactMethod":
					case "PreferConfirmMethod":
					case "PreferRecallMethod":
						if(fld.RadioButtonValue=="HmPhone") {
							if(SheetWebFieldValue=="HmPhone") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="WkPhone") {
							if(SheetWebFieldValue=="WkPhone") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="WirelessPh") {
							if(SheetWebFieldValue=="WirelessPh") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="Email") {
							if(SheetWebFieldValue=="Email") {
								fld.FieldValue="X";
							}
						}
						break;
					case "StudentStatus":
						if(fld.RadioButtonValue=="Nonstudent") {
							if(SheetWebFieldValue=="Nonstudent") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="Fulltime") {
							if(SheetWebFieldValue=="Fulltime") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="Parttime") {
							if(SheetWebFieldValue=="Parttime") {
								fld.FieldValue="X";
							}
						}
						break;
					case "ins1Relat":
					case "ins2Relat":
						if(fld.RadioButtonValue=="Self") {
							if(SheetWebFieldValue=="Self") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="Spouse") {
							if(SheetWebFieldValue=="Spouse") {
								fld.FieldValue="X";
							}
						}
						if(fld.RadioButtonValue=="Child") {
							if(SheetWebFieldValue=="Child") {
								fld.FieldValue="X";
							}
						}
						break;
					case "addressAndHmPhoneIsSameEntireFamily":
					if(SheetWebFieldValue=="True") {
						fld.FieldValue="X";
					}
					break;
					default:
						fld.FieldValue=SheetWebFieldValue;
					break;
				}//switch case
			}
			catch(Exception e) {
				gridMain.EndUpdate();
				MessageBox.Show(fld.FieldName + e.Message);
			}
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
				string dbSheetFieldValue=sheetFromDb.SheetFields[i].FieldValue.ToString();
				string newSheetFieldValue=newSheet.SheetFields[i].FieldValue.ToString();
				if(dbSheetFieldValue!=newSheetFieldValue) {
					isEqual=false;
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
				FormWebFormSetup formW=new FormWebFormSetup();
				formW.ShowDialog();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			/* delete later
			long PatNum=(long)gridMain.Rows[e.Row].Tag;
			if(PatNum!=0) {
				FormPatientForms formP=new FormPatientForms();
				formP.PatNum=PatNum;
				formP.ShowDialog();
			}
			*/
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