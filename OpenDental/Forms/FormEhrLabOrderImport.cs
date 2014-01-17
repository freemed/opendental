using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;
using CodeBase;

namespace OpenDental {
	public partial class FormEhrLabOrderImport:Form {
		public string Hl7LabMessage;
		private List<EhrLab> ListEhrLabs;
		public Patient PatCur;

		public FormEhrLabOrderImport() {
			InitializeComponent();
		}

		private void FormEhrLabOrders_Load(object sender,EventArgs e) {
			if(PatCur==null) {
				butSave.Enabled=false;
			}
			ListEhrLabs=EhrLabs.ProcessHl7Message(Hl7LabMessage);
			AttachPatientHelper();
			FillPatientPicker();
			FillPatientInfo();
			FillGrid();
		}

		private void AttachPatientHelper() {
			Patient patAttach=EhrLabs.FindAttachedPatient(Hl7LabMessage);
			if(patAttach==null){
				return;//no reccomended patient
			}
			else if(PatCur==null){
				PatCur=patAttach;
			}
			else if(PatCur.PatNum!=patAttach.PatNum) {
				MsgBox.Show(this,"Patient mismatch. Selected patient does not match detected patient.");//will only happen if we set PatCur from somewhere else. Probably wont ever happen.
				PatCur=patAttach;
			}
			else {
				//I dunno what to put here; maybe a little picture of a dog wearing a fireman costume?
			}
			butSave.Enabled=true;
		}

		private void FillPatientPicker() {
			if(PatCur==null) {
				textName.Text="";
				return;
			}
			textName.Text=PatCur.GetNameFL();
		}

		///<summary>Fills patient information from message contents, not from PatCur.</summary>
		private void FillPatientInfo() {
			string[] PIDFields;
			try {
				PIDFields=Hl7LabMessage.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries)[1].Split('|');
			}
			catch {
				return;//invalid HL7Message
			}
			//patient name(s)
			for(int i=0;i<PIDFields[5].Split('~').Length;i++) {
				listBoxNames.Items.Add(PIDFields[5].Split('~')[i].Split('^')[5]+" "//Prefix
										+PIDFields[5].Split('~')[i].Split('^')[2]+" "//FName
										+PIDFields[5].Split('~')[i].Split('^')[3]+" "//Middle Name(s)
										+PIDFields[5].Split('~')[i].Split('^')[1]+" "//Last Name
										+PIDFields[5].Split('~')[i].Split('^')[4]);   //Suffix
			}
			//Birthdate
			textBirthdate.Text=PIDFields[7];
			//Gender
			textGender.Text=PIDFields[8];
			//Race(s)
			for(int i=0;i<PIDFields[10].Split('~').Length;i++) {
				if(PIDFields[10].Split('~')[i].Split('^')[1]!=""){//Text of 1st triplet
					listBoxRaces.Items.Add(PIDFields[10].Split('~')[i].Split('^')[1]);
				}
				if(PIDFields[10].Split('~')[i].Split('^')[4]!=""){//Text of second triplet
					listBoxRaces.Items.Add(PIDFields[10].Split('~')[i].Split('^')[4]);
				}
			}
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Date Time",80);//Formatted yyyyMMdd
			col.SortingStrategy=GridSortingStrategy.DateParse;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Placer Order Number",180);//Should be PK but might not be. Instead use Placer Order Num.
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Filler Order Number",180);//Should be PK but might not be. Instead use Placer Order Num.
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Results",80);//Or date of latest result? or both?
			gridMain.Columns.Add(col);
			ListEhrLabs = EhrLabs.GetAllForPat(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListEhrLabs.Count;i++) {
				row=new ODGridRow();
				string dateSt=ListEhrLabs[i].ObservationDateTimeStart.Substring(0,8);//stored in DB as yyyyMMddhhmmss-zzzz
				DateTime dateT=PIn.Date(dateSt.Substring(4,2)+"/"+dateSt.Substring(6,2)+"/"+dateSt.Substring(0,4));
				row.Cells.Add(dateT.ToShortDateString());//date only
				row.Cells.Add(ListEhrLabs[i].PlacerOrderNum);
				row.Cells.Add(ListEhrLabs[i].FillerOrderNum);
				row.Cells.Add(ListEhrLabs[i].ListEhrLabResults.Count.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrLabOrderEdit2014 FormLOE=new FormEhrLabOrderEdit2014();
			FormLOE.EhrLabCur=ListEhrLabs[e.Row];
			FormLOE.ShowDialog();
			if(FormLOE.DialogResult!=DialogResult.OK) {
				return;
			}
			EhrLabs.SaveToDB(FormLOE.EhrLabCur);
			for(int i=0;i<FormLOE.EhrLabCur.ListEhrLabResults.Count;i++) {
				if(Security.IsAuthorized(Permissions.EhrShowCDS,true)) {
					FormCDSIntervention FormCDSI=new FormCDSIntervention();
					FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(FormLOE.EhrLabCur.ListEhrLabResults[i],PatCur);
					FormCDSI.ShowIfRequired(false);
				}
			}
			//TODO:maybe add more code here for when we come back from form... In case we delete a lab from the form.
		}

		private void butPatSelect_Click(object sender,EventArgs e) {
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK) {
				return;
			}
			PatCur=Patients.GetPat(FormPS.SelectedPatNum);
			FillPatientPicker();
		}

		private void butSave_Click(object sender,EventArgs e) {
			if(PatCur==null) {
				MsgBox.Show(this,"Please attach to patient first.");
			}
			for(int i=0;i<ListEhrLabs.Count;i++) {
				ListEhrLabs[i].PatNum=PatCur.PatNum;
				ListEhrLabs[i]=EhrLabs.SaveToDB(ListEhrLabs[i]);//SAVE
				for(int j=0;j<ListEhrLabs[i].ListEhrLabResults.Count;j++) {//EHR TRIGGER
					if(Security.IsAuthorized(Permissions.EhrShowCDS,true)) {
						FormCDSIntervention FormCDSI=new FormCDSIntervention();
						FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(ListEhrLabs[i].ListEhrLabResults[j],PatCur);
						FormCDSI.ShowIfRequired(false);
					}
				}
			}
			//Done!
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		






	}
}
