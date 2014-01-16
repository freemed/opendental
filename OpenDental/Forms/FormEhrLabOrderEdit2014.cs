using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Drawing.Printing;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrLabOrderEdit2014:Form {
		public EhrLab EhrLabCur;
		public bool IsNew;

		public FormEhrLabOrderEdit2014() {
			InitializeComponent();
		}

		private void FormLabPanelEdit_Load(object sender,EventArgs e) {
			FillGrid();//LabResults
			FillGridNotes();
			FillGridResultsCopyTo();
			FillGridClinicalInformation();
			FillGridSpecimen();
		}
	
		///<summary>Lab Results</summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Test Date",80);
			col.SortingStrategy=GridSortingStrategy.DateParse;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("LOINC",65);//LoincCode
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Test Performed",250);//ShortDescription
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Result Value",120);//Complicated
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Units",45);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListEhrLabResults.Count;i++) {
				row=new ODGridRow();
				string dateSt=EhrLabCur.ListEhrLabResults[i].ObservationDateTime.Substring(0,8);//stored in DB as yyyyMMdd[hh[mm[ss]]], []==optional components
				DateTime dateT=PIn.Date(dateSt.Substring(4,2)+"/"+dateSt.Substring(6,2)+"/"+dateSt.Substring(0,4));
				row.Cells.Add(dateT.ToShortDateString());//date only
				if(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierID!="") {
					row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierID);
					row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierID+" << convert that into a LOINC.NameShort for display.");
				}
				else if(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierIDAlt!="") {
					row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierIDAlt);
					row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierIDAlt+" << convert that into a LOINC.NameShort for display.");
				}
				else {
					row.Cells.Add("UNK");
					row.Cells.Add("Unknown, could not find valid test code.");
				}
				row.Cells.Add("TODO: value.");
				row.Cells.Add(EhrLabCur.ListEhrLabResults[i].UnitsID);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		///<summary>Lab Result Notes. Currently includes notes for results too... TODO: seperate notes for labs and results.</summary>
		private void FillGridNotes() {
			gridNotes.BeginUpdate();
			gridNotes.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Note Num",60);
			gridNotes.Columns.Add(col);
			col=new ODGridColumn("Comments",300);
			gridNotes.Columns.Add(col);
			gridNotes.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListEhrLabNotes.Count;i++) {
				for(int j=0;j<EhrLabCur.ListEhrLabNotes[i].Comments.Split('^').Length;j++) {
					row=new ODGridRow();
					row.Cells.Add((j==0?(i+1).ToString():""));//add note number if this is first comment for the note, otherwise add blank cell.
					row.Cells.Add(EhrLabCur.ListEhrLabNotes[i].Comments.Split('^')[j]);//Add each comment.
					gridNotes.Rows.Add(row);
				}
			}
			gridNotes.EndUpdate();
		}

		///<summary>Lab Result Notes. Currently includes notes for results too... TODO: seperate notes for labs and results.</summary>
		private void FillGridResultsCopyTo() {
			gridResultsCopyTo.BeginUpdate();
			gridResultsCopyTo.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Name",60);
			gridResultsCopyTo.Columns.Add(col);
			gridResultsCopyTo.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListEhrLabResultsCopyTo.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(EhrLabCur.ListEhrLabResultsCopyTo[i].CopyToPrefix+" "
					+EhrLabCur.ListEhrLabResultsCopyTo[i].CopyToFName+" "
					+EhrLabCur.ListEhrLabResultsCopyTo[i].CopyToLName+" "
					+EhrLabCur.ListEhrLabResultsCopyTo[i].CopyToSuffix);
				//TODO: Make this neater. Will display extra spaces if missing prefix suffix or middle names.
				gridResultsCopyTo.Rows.Add(row);
			}
			gridResultsCopyTo.EndUpdate();
		}

		private void FillGridClinicalInformation() {
			gridClinicalInformation.BeginUpdate();
			gridClinicalInformation.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",60);//arbitrary width, only column in grid.
			gridClinicalInformation.Columns.Add(col);
			gridClinicalInformation.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListRelevantClinicalInformations.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(EhrLabCur.ListRelevantClinicalInformations[i].ClinicalInfoText);//may be blank, if so, check the "alt" text
				gridClinicalInformation.Rows.Add(row);
			}
			gridClinicalInformation.EndUpdate();
		}

		///<summary>Lab Results</summary>
		private void FillGridSpecimen() {
			gridSpecimen.BeginUpdate();
			gridSpecimen.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Specimen Type",60);//arbitrary width, only column in grid.
			gridSpecimen.Columns.Add(col);
			gridSpecimen.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListEhrLabSpecimens.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(EhrLabCur.ListEhrLabSpecimens[i].SpecimenTypeText);//may be blank, if so, check the "alt" text
				gridSpecimen.Rows.Add(row);
			}
			gridSpecimen.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrLabResultEdit2014 FormLRE=new FormEhrLabResultEdit2014();
			FormLRE._ehrLabResultCur=EhrLabCur.ListEhrLabResults[e.Row];
			FormLRE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrLabResultEdit2014 FormLRE=new FormEhrLabResultEdit2014();
			FormLRE._ehrLabResultCur=new EhrLabResult();
			FormLRE._ehrLabResultCur.IsNew=true;
			if(FormLRE.ShowDialog()!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {

		}

		private void butOk_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	}
}
