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
	public partial class FormEhrLabOrders:Form {
		public List<EhrLab> ListEhrLabs;
		public Patient PatCur;

		public FormEhrLabOrders() {
			InitializeComponent();
		}

		private void FormEhrLabOrders_Load(object sender,EventArgs e) {
			FillGrid();
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
			col=new ODGridColumn("Results In",80);//Or date of latest result? or both?
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

		private void butImport_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste MBCP = new MsgBoxCopyPaste("Paste HL7 Lab Message Text Here.");
			MBCP.textMain.SelectAll();
			MBCP.ShowDialog();
			List<EhrLab> listEhrLabs;
			try {
				listEhrLabs=EhrLabs.ProcessHl7Message(MBCP.textMain.Text);//Not a typical use of the msg box copy paste
				if(listEhrLabs[0].PatNum==PatCur.PatNum) {//only need to check the first lab.
					//nothing to do here. Imported lab matches the current patient.
				}
				else if(listEhrLabs[0].PatNum==0) {
					if(MessageBox.Show("Lab patient does not match current patient. Lab patient name is "
						+MBCP.textMain.Text.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries)[1].Split('|')[5].Split('~')[0].Split('^')[1]+" "//first name
						+MBCP.textMain.Text.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries)[1].Split('|')[5].Split('~')[0].Split('^')[1]+" "//last name
						+"\r\nWould you like to import lab for the current patient?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) 
					{
						return;
					}
					//User agreed to import current lab(s) for current patient.
					for(int i=0;i<listEhrLabs.Count;i++) {
						listEhrLabs[i].PatNum=PatCur.PatNum;
						//TODO: Import external OIDs and PatIDs so that we can identify this patient next time.
					}
				}
				else {//Patnum is already associated with another patient.
					MessageBox.Show("This lab contains patient information for a different patient. Lab patient name is "
						+MBCP.textMain.Text.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries)[1].Split('|')[5].Split('~')[0].Split('^')[1]+" "//first name
						+MBCP.textMain.Text.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries)[1].Split('|')[5].Split('~')[0].Split('^')[1]);
					return;
				}
			}
			catch (Exception Ex){
				MessageBox.Show(this,"Unable to import lab.\r\n"+Ex.Message);
				return;
			}
			for(int i=0;i<listEhrLabs.Count;i++) {
				EhrLab tempLab=null;//lab from DB if it exists.
				tempLab=EhrLabs.GetByGUID(ListEhrLabs[i].PlacerOrderUniversalID,ListEhrLabs[i].PlacerOrderNum);
				if(tempLab==null){
					tempLab=EhrLabs.GetByGUID(ListEhrLabs[i].FillerOrderUniversalID,ListEhrLabs[i].FillerOrderNum);
				}
				if(tempLab!=null) {
					//Date validation.
					if(tempLab.ResultDateTime.CompareTo(ListEhrLabs[i].ResultDateTime)<=0) {//string compare dates will return 1+ if tempLab Date is greater.
						MsgBox.Show(this,"This lab already exists in the database and has a more recent timestamp.");
						continue;
					}
				}
				listEhrLabs[i]=EhrLabs.SaveToDB(listEhrLabs[i]);//SAVE
				for(int j=0;j<listEhrLabs[i].ListEhrLabResults.Count;j++) {//EHR TRIGGER
					if(Security.IsAuthorized(Permissions.EhrShowCDS,true)) {
						FormCDSIntervention FormCDSI=new FormCDSIntervention();
						FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(listEhrLabs[i].ListEhrLabResults[j],PatCur);
						FormCDSI.ShowIfRequired(false);
					}
				}
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrLabOrderEdit2014 FormLOE=new FormEhrLabOrderEdit2014();
			FormLOE.EhrLabCur=new EhrLab();
			FormLOE.EhrLabCur.PatNum=PatCur.PatNum;
			FormLOE.ShowDialog();
			//Save from the form??
			if(FormLOE.DialogResult!=DialogResult.OK) {
				return;
			}
			EhrMeasureEvent newMeasureEvent=new EhrMeasureEvent();
			newMeasureEvent.DateTEvent=DateTime.Now;
			Loinc loinc=Loincs.GetByCode(FormLOE.EhrLabCur.UsiID);
			if(loinc.ScaleType.Contains("Qn")) {
				newMeasureEvent.EventType=EhrMeasureEventType.CPOE_RadOrdered;
			}
			else {
				newMeasureEvent.EventType=EhrMeasureEventType.CPOE_LabOrdered;
			}
			newMeasureEvent.PatNum=FormLOE.EhrLabCur.PatNum;
			newMeasureEvent.MoreInfo="";
			newMeasureEvent.FKey=FormLOE.EhrLabCur.EhrLabNum;
			EhrMeasureEvents.Insert(newMeasureEvent);
			EhrLabs.SaveToDB(FormLOE.EhrLabCur);
			for(int i=0;i<FormLOE.EhrLabCur.ListEhrLabResults.Count;i++) {
				if(Security.IsAuthorized(Permissions.EhrShowCDS,true)) {
					FormCDSIntervention FormCDSI=new FormCDSIntervention();
					FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(FormLOE.EhrLabCur.ListEhrLabResults[i],PatCur);
					FormCDSI.ShowIfRequired(false);
				}
			}
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

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		






	}
}
