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
	public partial class FormEhrElectronicCopy:Form {
		public Patient PatCur;
		private List<EhrMeasureEvent> listHistory;

		public FormEhrElectronicCopy() {
			InitializeComponent();
		}

		private void FormElectronicCopy_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("DateTime",140);
			gridMain.Columns.Add(col);
			col = new ODGridColumn("Type",600);
			gridMain.Columns.Add(col);
			listHistory=EhrMeasureEvents.RefreshByType(PatCur.PatNum,EhrMeasureEventType.ElectronicCopyRequested,EhrMeasureEventType.ElectronicCopyProvidedToPt);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listHistory.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listHistory[i].DateTEvent.ToString());
				switch(listHistory[i].EventType) {
					case EhrMeasureEventType.ElectronicCopyRequested:
						row.Cells.Add("Requested by patient");
						break;
					case EhrMeasureEventType.ElectronicCopyProvidedToPt:
						row.Cells.Add("Provided to patient");
						break;
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butRequest_Click(object sender,EventArgs e) {
			EhrMeasureEvent measureEvent = new EhrMeasureEvent();
			measureEvent.DateTEvent = DateTime.Now.AddMinutes(-1);
			measureEvent.EventType = EhrMeasureEventType.ElectronicCopyRequested;
			measureEvent.PatNum = PatCur.PatNum;
			measureEvent.MoreInfo = "";
			EhrMeasureEvents.Insert(measureEvent);
			FillGrid();
		}

		///<summary>When sent by email or exported, this records the event.  It also recordes a request if needed.</summary>
		private void RecordRequestAndProvide() {
			//If there's not an event for a request within the last 5 days, automatically add one.
			bool requestExists=false;
			for(int i=0;i<listHistory.Count;i++) {
				if(listHistory[i].EventType!=EhrMeasureEventType.ElectronicCopyRequested) {
					continue;
				}
				if(listHistory[i].DateTEvent.Date >= DateTime.Today.AddDays(-5)) {
					requestExists=true;
					break;
				}
			}
			EhrMeasureEvent measureEvent;
			if(!requestExists) {
				measureEvent = new EhrMeasureEvent();
				measureEvent.DateTEvent = DateTime.Now.AddMinutes(-1);
				measureEvent.EventType = EhrMeasureEventType.ElectronicCopyRequested;
				measureEvent.PatNum = PatCur.PatNum;
				measureEvent.MoreInfo = "";
				EhrMeasureEvents.Insert(measureEvent);
			}
			//Always add an event for providing the electronic copy
			measureEvent = new EhrMeasureEvent();
			measureEvent.DateTEvent = DateTime.Now;
			measureEvent.EventType = EhrMeasureEventType.ElectronicCopyProvidedToPt;
			measureEvent.PatNum = PatCur.PatNum;
			measureEvent.MoreInfo = "";
			EhrMeasureEvents.Insert(measureEvent);
			FillGrid();
		}

		private void butExport_Click(object sender,EventArgs e) {
			FolderBrowserDialog dlg=new FolderBrowserDialog();
			dlg.SelectedPath=ImageStore.GetPatientFolder(PatCur,ImageStore.GetPreferredAtoZpath());//Default to patient image folder.
			DialogResult result=dlg.ShowDialog();
			if(result!=DialogResult.OK) {
				return;
			}
			if(File.Exists(Path.Combine(dlg.SelectedPath,"ccd.xml"))) {
				if(MessageBox.Show("Overwrite existing ccd.xml?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
					return;
				}
			}
			string ccd=EhrCCD.GenerateCCD(PatCur);
			File.WriteAllText(Path.Combine(dlg.SelectedPath,"ccd.xml"),ccd);
			File.WriteAllText(Path.Combine(dlg.SelectedPath,"ccd.xsl"),FormEHR.GetEhrResource("CCD"));
			RecordRequestAndProvide();
			MessageBox.Show("Exported");
		}

		private void butSendEmail_Click(object sender,EventArgs e) {
			RecordRequestAndProvide();
			Cursor=Cursors.WaitCursor;
			string ccd=EhrCCD.GenerateCCD(PatCur);
			try {
				EmailMessages.SendTestUnsecure("Electronic Copy of Health Information","ccd.xml",ccd,"ccd.xsl",FormEHR.GetEhrResource("CCD"));
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
			FormEhrSummaryOfCare.DisplayCCD(ccd);
		}

		private void butShowXml_Click(object sender,EventArgs e) {
			string ccd=EhrCCD.GenerateCCD(PatCur);
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(ccd);
			msgbox.ShowDialog();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length < 1) {
				MessageBox.Show("Please select at least one record to delete.");
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				EhrMeasureEvents.Delete(listHistory[gridMain.SelectedIndices[i]].EhrMeasureEventNum);
			}
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		
	

		

	

	


	}
}
