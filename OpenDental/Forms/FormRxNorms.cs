using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormRxNorms:Form {
		public long RxCui;
		public FormRxNorms() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRxNorms_Load(object sender,EventArgs e) {

		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormRxNorms","RxCui"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormRxNorms","Code"),60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormRxNorms","Description"),110);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			gridMain.EndUpdate();
		}

		private void butRxNorm_Click(object sender,EventArgs e) {
			RxNorms.CreateFreshRxNormTableFromZip();
			RxNorm rxNorm=RxNorms.GetOne(1);
			MsgBox.Show(this,rxNorm.RxNormNum+" "+rxNorm.RxCui+" "+rxNorm.MmslCode+" "+rxNorm.Description);
			MsgBox.Show(this,RxNorms.GetMmslCodeByRxCui("1000005")+" <-- should be 26420");
			MsgBox.Show(this,RxNorms.GetMmslCodeByRxCui("1000002")+" <-- should be blank");
		}

		private void butOK_Click(object sender,EventArgs e) {
			RxCui=1;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}