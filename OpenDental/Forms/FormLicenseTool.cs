using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using System.Text.RegularExpressions;

namespace OpenDental {
	public partial class FormLicenseTool:Form {
		private ProcLicense[] listProcLicenses;
		private PrintDocument pd2;
		public FormRpPrintPreview pView = new FormRpPrintPreview();

		public FormLicenseTool() {
			InitializeComponent();
		}

		private void FormLicenseTool_Load(object sender,EventArgs e) {
			RefreshGrid();
			string str=PrefB.GetString("ADAComplianceDateTime");
			DateTime complianceDate=PIn.PDateT(str);
			if(complianceDate.Year>1880) {
				butPrint.Enabled=true;
				butMerge.Enabled=false;
				checkcompliancebutton.Enabled=false;
				addButton.Enabled=false;
				MessageBox.Show("Tool has already been successfully completed.  You may reprint proof at any time.");
			}
			else {
				textCode.Text="D";
				textCode.Focus();
				textCode.Select(1,1);
			}
		}

		private void RefreshGrid(){
			listProcLicenses=ProcLicenses.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Code",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",80);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			for(int i=0;i<listProcLicenses.Length;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(listProcLicenses[i].ProcCode);
				row.Cells.Add(listProcLicenses[i].Descript);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollToEnd();
		}

		private void textCode_KeyPress(object sender,KeyPressEventArgs e) {
			if(e.KeyChar=='\r') {
				e.Handled=true;
				textDescription.Focus();
			}
		}

		private void description_KeyPress(object sender,KeyPressEventArgs e) {
			if(e.KeyChar=='\r') {
				e.Handled=true;
				addButton.Focus();
			}
		}

		private void addButton_Enter(object sender,EventArgs e) {
			if(!Regex.IsMatch(textCode.Text,"^D[0-9]{4}$")) {
				MessageBox.Show("Code must be in the form D####.");
				return;
			}
			if(textDescription.Text.Length<1) {
				MessageBox.Show("Description must be specified.");
				return;
			}
			if(!ProcLicenses.IsUniqueCode(textCode.Text)) {
				MessageBox.Show("That code already exists.");
				return;
			}
			ProcLicense procLicense=new ProcLicense();
			procLicense.ProcCode=textCode.Text;
			procLicense.Descript=textDescription.Text;
			ProcLicenses.Insert(procLicense);
			RefreshGrid();
			textCode.Text="D";
			textDescription.Text="";
			textCode.Focus();
			textCode.Select(1,1);
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormLicenseToolEdit formL=new FormLicenseToolEdit();
			formL.ProcLic=listProcLicenses[e.Row];
			formL.ShowDialog();
			if(formL.DialogResult==DialogResult.OK) {
				RefreshGrid();
			}
		}

		private void checkcompliancebutton_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			ArrayList AL=ProcLicenses.CheckCompliance();
			Cursor=Cursors.Default;
			if(AL.Count==0){
				MessageBox.Show("All necessary codes have been entered.  You may now Run Tool.");
				butMerge.Enabled=true;
				return;
			}
			FormLicenseMissing FormL=new FormLicenseMissing();
			FormL.Comments=AL;
			FormL.ShowDialog();
		}

		///<summary>Updates proccode descriptions from the proclicense table into the procedurecode table.</summary>
		private void butMerge_Click(object sender,EventArgs e){
			int affectedRows=ProcLicenses.MergeToProcedureCodes();
			MessageBox.Show(affectedRows.ToString()+" descriptions updated.");
			if(MessageBox.Show("Unused codes will now be purged.","",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			affectedRows=ProcLicenses.PurgeUnused();
			MessageBox.Show(affectedRows.ToString()+" codes purged.");
			if(MessageBox.Show("Claimform backgrounds will now be deleted.","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				return;
			}
			File.Delete(CodeBase.ODFileUtils.CombinePaths(PrefB.GetString("DocPath"),"ADA2000.jpg"));//no exception thrown
			File.Delete(CodeBase.ODFileUtils.CombinePaths(PrefB.GetString("DocPath"),"ADA2002.gif"));
			MessageBox.Show("Any claimforms that did not get deleted automatically should be deleted manually.");
			Prefs.UpdateString("ADAComplianceDateTime",DateTime.Now.ToString());
			butPrint.Enabled=true;
			MessageBox.Show("Done.  You may now print and sign proof of completion.");
			DataValid.SetInvalid(InvalidTypes.Prefs);
		}

		private void butPrint_Click(object sender,EventArgs e) {
			PrintReport();
		}

		///<summary></summary>
		public void PrintReport() {
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			//pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			//pd2.OriginAtMargins=true;
			//if(!PrefB.GetBool("RxOrientVert")) {
			//	pd2.DefaultPageSettings.Landscape=true;
			//}
#if DEBUG
			pView.printPreviewControl2.Document=pd2;
			pView.ShowDialog();
#else
			if(!Printers.SetPrinter(pd2,PrintSituation.Default)){
				return;
			}
			try{
				pd2.Print();
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
#endif
		}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			Graphics g=e.Graphics;
			SolidBrush brush=new SolidBrush(Color.Black);
			Pen pen=new Pen(brush);
			Font bigFont=new Font(FontFamily.GenericSansSerif,12,FontStyle.Bold);
			g.DrawString("ADA Code Update Tool",bigFont,brush,100,100);
			g.DrawString("Completed "+PIn.PDateT(PrefB.GetString("ADAComplianceDateTime")).ToString(),Font,brush,100,140);
			g.DrawString("By signing this form, I certify that I used a licensed CDT codebook to obtain the codes and descriptions "
				+"that I entered.  I also certify that all copies of claimform background images have been permanently deleted.",
				Font,brush,new RectangleF(100,180,600,100));
			g.DrawString("Printed Name",Font,brush,100,260);
			g.DrawLine(pen,170,272,660,272);
			g.DrawString("Signature",Font,brush,100,330);
			g.DrawLine(pen,150,342,660,342);
			g.DrawString("Date",Font,brush,100,400);
			g.DrawLine(pen,130,412,300,412);
			g.DrawString("Phone",Font,brush,100,470);
			g.DrawLine(pen,136,482,400,482);
		}

		

		





	}
}