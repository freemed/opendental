using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrProvKeysCustomer:Form {
		private List<EhrProvKey> listKeys;
		private List<EhrQuarterlyKey> listKeysQ;
		public long Guarantor;

		public FormEhrProvKeysCustomer() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrProvKeysCustomer_Load(object sender,EventArgs e) {
			FillGrid();
			FillGridQ();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"LName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"FName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Key"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Charge"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"FTE"),35,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Notes"),100);
			gridMain.Columns.Add(col);
			listKeys=EhrProvKeys.RefreshForFam(Guarantor);
			gridMain.Rows.Clear();
			ODGridRow row;
			decimal feeTotal=0;
			decimal fee=0;
			for(int i=0;i<listKeys.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listKeys[i].LName);
				row.Cells.Add(listKeys[i].FName);
				row.Cells.Add(listKeys[i].ProvKey);
				fee=(decimal)(60f*listKeys[i].FullTimeEquiv);
				feeTotal+=fee;
				row.Cells.Add(fee.ToString("c"));
				row.Cells.Add(listKeys[i].FullTimeEquiv.ToString());
				row.Cells.Add(listKeys[i].Notes);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			textCharge.Text=feeTotal.ToString("c");
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormEhrProvKeyEditCust formK=new FormEhrProvKeyEditCust();
			formK.KeyCur=listKeys[e.Row];
			formK.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrProvKeyEditCust formK=new FormEhrProvKeyEditCust();
			formK.KeyCur=new EhrProvKey();
			formK.KeyCur.PatNum=Guarantor;
			formK.KeyCur.FullTimeEquiv=1;
			formK.KeyCur.IsNew=true;
			formK.ShowDialog();
			FillGrid();
		}

		private void FillGridQ(){
			gridQ.BeginUpdate();
			gridQ.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Year"),40);
			gridQ.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Quarter"),50);
			gridQ.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Key"),100);
			gridQ.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Notes"),100);
			gridQ.Columns.Add(col);
			listKeysQ=EhrQuarterlyKeys.Refresh(Guarantor);
			gridQ.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listKeysQ.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listKeysQ[i].YearValue.ToString());
				row.Cells.Add(listKeysQ[i].QuarterValue.ToString());
				row.Cells.Add(listKeysQ[i].KeyValue);
				row.Cells.Add(listKeysQ[i].Notes);
				gridQ.Rows.Add(row);
			}
			gridQ.EndUpdate();
		}

		private void gridQ_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormEhrQuarterlyKeyEditCust formK=new FormEhrQuarterlyKeyEditCust();
			/*formK.KeyCur=listKeys[e.Row];
			formK.ShowDialog();
			FillGrid();*/
		}

		private void butAddQuarterly_Click(object sender,EventArgs e) {
			FormEhrQuarterlyKeyEditCust formK=new FormEhrQuarterlyKeyEditCust();
			/*
			formK.KeyCur=new EhrProvKey();
			formK.KeyCur.PatNum=Guarantor;
			formK.KeyCur.FullTimeEquiv=1;
			formK.KeyCur.IsNew=true;
			formK.ShowDialog();
			FillGrid();*/
		}

		private void butSave_Click(object sender,EventArgs e) {
			long defNum=DefC.GetList(DefCat.ImageCats)[0].DefNum;
			Bitmap bitmap=new Bitmap(this.Width,this.Height);
			this.DrawToBitmap(bitmap,new Rectangle(0,0,this.Width,this.Height));
			Patient guar=Patients.GetPat(Guarantor);
			try {
				ImageStore.Import(bitmap,defNum,ImageType.Photo,guar);
			}
			catch(Exception ex) {
				MessageBox.Show("Unable to save file: "+ex.Message);
				return;
			}
			MsgBox.Show(this,"Saved.");
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

	

	

		

		
	}
}