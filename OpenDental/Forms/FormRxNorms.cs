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
			//byte[] rxByte=RxNorms.GetRxNormByteArray();
			//MemoryStream ms=new MemoryStream();
			//using(ZipFile unzipped=ZipFile.Read(rxByte)) {
			//  ZipEntry ze=unzipped["rxnorm.txt"];
			//  ze.Extract(ms);
			//}
			//StreamReader reader=new StreamReader(ms);
			//ms.Position=0;
			//FillGrid(reader);


			//string line;
			//while((line=reader.ReadLine())!=null) {
			//  string[] lineSplit=line.Split('\t');
			//  row=new ODGridRow();
			//  row.Cells.Add(lineSplit[0]);
			//  row.Cells.Add(lineSplit[1]);
			//  row.Cells.Add(lineSplit[2]);
			//  gridMain.Rows.Add(row);
			//}


			//byte[] rxByte=RxNorms.GetRxNormByteArray();
			//MemoryStream ms=new MemoryStream();
			//using(ZipFile unzipped=ZipFile.Read(rxByte)) {
			//  ZipEntry ze=unzipped["rxnorm.txt"];
			//  ze.Extract(ms);
			//}
			//StreamReader reader=new StreamReader(ms);
			//ms.Position=0;
			//string test=reader.ReadToEnd();
			//reader.Close();
			//ms.Close();
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