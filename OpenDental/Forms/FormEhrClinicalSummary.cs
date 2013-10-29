using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using System.Xml;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrClinicalSummary:Form {
		public Patient PatCur;
		private List<EhrMeasureEvent> summariesSentList;

		public FormEhrClinicalSummary() {
			InitializeComponent();
		}

		private void FormClinicalSummary_Load(object sender,EventArgs e) {
			FillGridEHRMeasureEvents();
		}

		private void FillGridEHRMeasureEvents() {
			gridEHRMeasureEvents.BeginUpdate();
			gridEHRMeasureEvents.Columns.Clear();
			ODGridColumn col = new ODGridColumn("DateTime",140);
			gridEHRMeasureEvents.Columns.Add(col);
			//col = new ODGridColumn("Details",600);
			//gridEHRMeasureEvents.Columns.Add(col);
			summariesSentList = EhrMeasureEvents.RefreshByType(PatCur.PatNum,EhrMeasureEventType.ClinicalSummaryProvidedToPt);
			gridEHRMeasureEvents.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<summariesSentList.Count;i++) {
				row = new ODGridRow();
				row.Cells.Add(summariesSentList[i].DateTEvent.ToString());
				//row.Cells.Add(summariesSentList[i].EventType.ToString());
				gridEHRMeasureEvents.Rows.Add(row);
			}
			gridEHRMeasureEvents.EndUpdate();
		}

		private void butExport_Click(object sender,EventArgs e) {
			FolderBrowserDialog dlg=new FolderBrowserDialog();
			DialogResult result=dlg.ShowDialog();
			if(result!=DialogResult.OK) {
				return;
			}
			if(File.Exists(Path.Combine(dlg.SelectedPath,"ccd.xml"))){
				if(MessageBox.Show("Overwrite existing ccd.xml?","",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			string ccd=EhrCCD.GenerateCCD(PatCur);
			File.WriteAllText(Path.Combine(dlg.SelectedPath,"ccd.xml"),ccd);
			File.WriteAllText(Path.Combine(dlg.SelectedPath,"ccd.xsl"),EHR.Properties.Resources.CCD);
			EhrMeasureEvent newMeasureEvent = new EhrMeasureEvent();
			newMeasureEvent.DateTEvent = DateTime.Now;
			newMeasureEvent.EventType = EhrMeasureEventType.ClinicalSummaryProvidedToPt;
			newMeasureEvent.PatNum = PatCur.PatNum;
			EhrMeasureEvents.Insert(newMeasureEvent);
			FillGridEHRMeasureEvents();
			MessageBox.Show("Exported");	
		}

		private void butSendEmail_Click(object sender,EventArgs e) {
			EhrMeasureEvent newMeasureEvent=new EhrMeasureEvent();
			newMeasureEvent.DateTEvent=DateTime.Now;
			newMeasureEvent.EventType=EhrMeasureEventType.ClinicalSummaryProvidedToPt;
			newMeasureEvent.PatNum=PatCur.PatNum;
			EhrMeasureEvents.Insert(newMeasureEvent);
			FillGridEHRMeasureEvents();
			Cursor=Cursors.WaitCursor;
			string ccd=EhrCCD.GenerateCCD(PatCur);
			try {
				EmailMessages.SendTestUnsecure("Clinical Summary","ccd.xml",ccd,"ccd.xsl",EHR.Properties.Resources.CCD);
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			MessageBox.Show("Sent");
		}

		private void butShowXhtml_Click(object sender,EventArgs e) {
			string ccd=EhrCCD.GenerateCCD(PatCur);
			bool didPrint=FormEhrSummaryOfCare.DisplayCCD(ccd);
			if(didPrint) {
				//we are printing a ccd so add new measure event.					
				EhrMeasureEvent measureEvent = new EhrMeasureEvent();
				measureEvent.DateTEvent = DateTime.Now;
				measureEvent.EventType = EhrMeasureEventType.ClinicalSummaryProvidedToPt;
				measureEvent.PatNum = PatCur.PatNum;
				EhrMeasureEvents.Insert(measureEvent);
				FillGridEHRMeasureEvents();
			}		
		}

		private void butShowXml_Click(object sender,EventArgs e) {
			string ccd=EhrCCD.GenerateCCD(PatCur);
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(ccd);
			msgbox.ShowDialog();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridEHRMeasureEvents.SelectedIndices.Length < 1) {
				MessageBox.Show("Please select at least one record to delete.");
				return;
			}
			for(int i=0;i<gridEHRMeasureEvents.SelectedIndices.Length;i++) {
				EhrMeasureEvents.Delete(summariesSentList[gridEHRMeasureEvents.SelectedIndices[i]].EhrMeasureEventNum);
			}
			FillGridEHRMeasureEvents();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

	

		











	}
}
