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
using System.Xml;
using System.Xml.XPath;
using CodeBase;
using System.IO;
#if EHRTEST
using EHR;
#endif

namespace OpenDental {
	public partial class FormEhrQualityMeasures2014:Form {
		private List<QualityMeasure> listQ;
		private List<Provider> listProvsKeyed;
		private long _provNum;
		private DateTime _dateStart;
		private DateTime _dateEnd;
		public long selectedPatNum;

		public FormEhrQualityMeasures2014() {
			InitializeComponent();
		}

		private void FormQualityMeasures_Load(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			listProvsKeyed=new List<Provider>();
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				if(FormEHR.ProvKeyIsValid(ProviderC.ListShort[i].LName,ProviderC.ListShort[i].FName,true,ProviderC.ListShort[i].EhrKey)) {
					listProvsKeyed.Add(ProviderC.ListShort[i]);
				}
			}
			if(listProvsKeyed.Count==0) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"No providers found with ehr keys.");
				return;
			}
			for(int i=0;i<listProvsKeyed.Count;i++) {
				comboProv.Items.Add(listProvsKeyed[i].GetLongDesc());
				if(Security.CurUser.ProvNum==listProvsKeyed[i].ProvNum) {
					comboProv.SelectedIndex=i;
				}
			}
			textDateStart.Text=(new DateTime(DateTime.Now.Year,1,1)).ToShortDateString();
			textDateEnd.Text=(new DateTime(DateTime.Now.Year,12,31)).ToShortDateString();
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void FillGrid() {
			if(comboProv.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a provider first.");
				return;
			}
			DateTime dateStart=PIn.Date(textDateStart.Text);
			DateTime dateEnd=PIn.Date(textDateEnd.Text);
			if(dateStart==DateTime.MinValue || dateEnd==DateTime.MinValue) {
				MsgBox.Show(this,"Fix date format and try again.");
				return;
			}
			_dateStart=dateStart;
			_dateEnd=dateEnd;
			_provNum=listProvsKeyed[comboProv.SelectedIndex].ProvNum;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Id",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Denominator",75,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Numerator",65,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Exclusion",60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Exception",60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("NotMet",50,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("PerformanceRate",100,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			listQ=QualityMeasures.GetAll2014(dateStart,dateEnd,_provNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listQ.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listQ[i].Id);
				row.Cells.Add(listQ[i].Descript);
				row.Cells.Add(listQ[i].Denominator.ToString());
				row.Cells.Add(listQ[i].Numerator.ToString());
				row.Cells.Add(listQ[i].Exclusions.ToString());
				row.Cells.Add(listQ[i].Exceptions.ToString());
				row.Cells.Add(listQ[i].NotMet.ToString());
				row.Cells.Add(listQ[i].Numerator.ToString()+"/"+(listQ[i].Numerator+listQ[i].NotMet).ToString()
					+"  = "+listQ[i].PerformanceRate.ToString()+"%");
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		///<summary>Launches edit window for double clicked item.</summary>
		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			FormEhrQualityMeasureEdit2014 FormQME=new FormEhrQualityMeasureEdit2014();
			FormQME.MeasureCur=listQ[e.Row];
			FormQME.ShowDialog();
			if(FormQME.DialogResult==DialogResult.OK && FormQME.selectedPatNum!=0) {
				selectedPatNum=FormQME.selectedPatNum;
				DialogResult=DialogResult.OK;
				Close();
				return;
			}
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butCreateQRDAs_Click(object sender,EventArgs e) {
			if(comboProv.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a provider first.");
				return;
			}
			if(listQ==null) {
				MsgBox.Show(this,"Click Refresh first.");
				return;
			}
			long provSelected=listProvsKeyed[comboProv.SelectedIndex].ProvNum;
			if(_provNum!=provSelected) {
				MsgBox.Show(this,"The values in the grid do not apply to the provider selected.  Click Refresh first.");
				return;
			}
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			if(fbd.ShowDialog()!=DialogResult.OK) {
				return;
			}
			string folderPath=fbd.SelectedPath+"\\"+"CQMs_"+DateTime.Today.ToString("MM_dd_yyyy");
			if(System.IO.Directory.Exists(folderPath)) {//if the folder already exists
				//find a unique folder name
				int uniqueID=1;
				string originalPath=folderPath;
				do {
					folderPath=originalPath+"_"+uniqueID.ToString();
					uniqueID++;
				}
				while(System.IO.Directory.Exists(folderPath));
			}
			try {
				System.IO.Directory.CreateDirectory(folderPath);
				for(int i=0;i<listQ.Count;i++) {
					if(System.IO.Directory.Exists(folderPath+"\\Measure_"+listQ[i].eMeasureNum)) {
						continue;
					}
					System.IO.Directory.CreateDirectory(folderPath+"\\Measure_"+listQ[i].eMeasureNum);
				}
			}
			catch(Exception ex) {
				MessageBox.Show("Folder was not created: "+ex.Message);
				return;
			}
			Cursor=Cursors.WaitCursor;
			try {
				QualityMeasures.GenerateQRDA(listQ,_provNum,_dateStart,_dateEnd,folderPath);//folderPath is a new directory created within the chosen directory
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			MsgBox.Show(this,"QRDA files have been created within the selected directory.");
		}

		private void butSubmit_Click(object sender,EventArgs e) {
			if(listQ==null) {
				MsgBox.Show(this,"Click Refresh first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			try {
				//EmailMessages.SendTestUnsecure("QRDA","qrda.xml",GenerateQRDA());
				//code to export will need to include the cda.xsl style sheet as well as the cda.xsd
				//FolderBrowserDialog dlg=new FolderBrowserDialog();
				//dlg.SelectedPath=ImageStore.GetPatientFolder(PatCur,ImageStore.GetPreferredAtoZpath());//Default to patient image folder.
				//DialogResult result=dlg.ShowDialog();
				//if(result!=DialogResult.OK) {
				//	return;
				//}
				//if(File.Exists(Path.Combine(dlg.SelectedPath,"ccd.xml"))) {
				//	if(MessageBox.Show("Overwrite existing ccd.xml?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				//		return;
				//	}
				//}
				//File.WriteAllText(Path.Combine(dlg.SelectedPath,"ccd.xml"),ccd);
				//File.WriteAllText(Path.Combine(dlg.SelectedPath,"ccd.xsl"),FormEHR.GetEhrResource("CCD"));
				//EhrMeasureEvent newMeasureEvent = new EhrMeasureEvent();
				//newMeasureEvent.DateTEvent = DateTime.Now;
				//newMeasureEvent.EventType = EhrMeasureEventType.ClinicalSummaryProvidedToPt;
				//newMeasureEvent.PatNum = PatCur.PatNum;
				//EhrMeasureEvents.Insert(newMeasureEvent);
				//FillGridEHRMeasureEvents();
				//MessageBox.Show("Exported");	
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Sent");
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}

	

	

		

	}
}
