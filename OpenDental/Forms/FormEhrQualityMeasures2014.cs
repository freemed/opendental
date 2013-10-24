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
using EHR;

namespace OpenDental {
	public partial class FormEhrQualityMeasures2014:Form {
		private List<QualityMeasure> listQ;
		private List<Provider> listProvsKeyed;

		public FormEhrQualityMeasures2014() {
			InitializeComponent();
		}

		private void FormQualityMeasures_Load(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			listProvsKeyed=new List<Provider>();
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				if(ProvKey.ProvKeyIsValid(ProviderC.ListShort[i].LName,ProviderC.ListShort[i].FName,true,ProviderC.ListShort[i].EhrKey)) {
					listProvsKeyed.Add(ProviderC.ListShort[i]);
				}
			}
			if(listProvsKeyed.Count==0) {
				Cursor=Cursors.Default;
				MessageBox.Show("No providers found with ehr keys.");
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
				return;
			}
			try {
				DateTime.Parse(textDateStart.Text);
				DateTime.Parse(textDateEnd.Text);
			}
			catch {
				MessageBox.Show(this,"Fix date format and try again.");
				return;
			}
			DateTime dateStart=PIn.Date(textDateStart.Text);
			DateTime dateEnd=PIn.Date(textDateEnd.Text);
			long provNum=listProvsKeyed[comboProv.SelectedIndex].ProvNum;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Id",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Denom",50,HorizontalAlignment.Center);
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
			listQ=QualityMeasures.GetAll2014(dateStart,dateEnd,provNum);
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
		
		public string GenerateQRDA_xml() {
			
			return "";
		}

		///<summary>Launches edit window for double clicked item.</summary>
		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			try {
				DateTime.Parse(textDateStart.Text);
				DateTime.Parse(textDateEnd.Text);
			}
			catch {
				MessageBox.Show("Please fix dates first.");
				return;
			}
			FormEhrQualityMeasureEdit2014 formQe=new FormEhrQualityMeasureEdit2014();
			formQe.DateStart=PIn.Date(textDateStart.Text);
			formQe.DateEnd=PIn.Date(textDateEnd.Text);
			formQe.ProvNum=listProvsKeyed[comboProv.SelectedIndex].ProvNum;
			formQe.Qcur=listQ[e.Row];
			formQe.ShowDialog();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butShow_Click(object sender,EventArgs e) {
			if(comboProv.SelectedIndex==-1) {
				MessageBox.Show("Please select a provider first.");
				return;
			}
			try {
				DateTime.Parse(textDateStart.Text);
				DateTime.Parse(textDateEnd.Text);
			}
			catch {
				MessageBox.Show("Invalid dates.");
				return;
			}
			if(listQ==null) {
				MessageBox.Show("Click Refresh first.");
				return;
			}
			MsgBoxCopyPaste MsgBoxCP = new MsgBoxCopyPaste(GenerateQRDA_xml());
			MsgBoxCP.ShowDialog();
		}

		private void butSubmit_Click(object sender,EventArgs e) {
			if(comboProv.SelectedIndex==-1) {
				MessageBox.Show("Please select a provider first.");
				return;
			}
			try {
				DateTime.Parse(textDateStart.Text);
				DateTime.Parse(textDateEnd.Text);
			}
			catch {
				MessageBox.Show("Invalid dates.");
				return;
			}
			if(listQ==null) {
				MessageBox.Show("Click Refresh first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			try {
				EmailMessages.SendTestUnsecure("QRDA","qrda.xml",GenerateQRDA_xml());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			MessageBox.Show("Sent");
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}

	

	

		

	}
}
