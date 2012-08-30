using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormRxNorms:Form {
		private List<RxNorm> rxList;
		///<summary>When this window is used for selecting an RxNorm (medication.RxCui or rxpat.RxCui), then use must click OK, None, or double click in grid.  In those cases, this field will have a value.  If None was clicked, it will be a new RxNorm with an RxCui of zero.</summary>
		public RxNorm SelectedRxNorm;

		public FormRxNorms() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRxNorms_Load(object sender,EventArgs e) {
			checkIgnore.Checked=true;
		}

		private void FormRxNorms_Shown(object sender,EventArgs e) {
			if(RxNorms.IsRxNormTableEmpty()) {
				MessageBox.Show("The RxNorm table in the database is empty.  If you intend to use RxNorm codes, use the button at the bottom of this window to load the RxNorm table with codes.  It will add about 10 MB to your database size.");
			}
		}
		
		private void butSearch_Click(object sender,EventArgs e) {
			FillGrid(false);
		}

		private void butExact_Click(object sender,EventArgs e) {
			FillGrid(true);
		}

		private void FillGrid(bool isExact) {
			Cursor=Cursors.WaitCursor;
			rxList=RxNorms.GetListByCodeOrDesc(textCode.Text,isExact,checkIgnore.Checked);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormRxNorms","Code"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormRxNorms","Description"),110);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<rxList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(rxList[i].RxCui);
				row.Cells.Add(rxList[i].Description);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollValue=0;
			Cursor=Cursors.Default;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			SelectedRxNorm=rxList[e.Row];
			DialogResult=DialogResult.OK;
		}

		private void butRxNorm_Click(object sender,EventArgs e) {
			//if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will ")) {
			//	return;
			//}
			Cursor=Cursors.WaitCursor;
			RxNorms.CreateFreshRxNormTableFromZip();
			Cursor=Cursors.Default;
			MsgBox.Show(this,"done");
			//just making sure it worked:
			/*
			RxNorm rxNorm=RxNorms.GetOne(1);
			MsgBox.Show(this,rxNorm.RxNormNum+" "+rxNorm.RxCui+" "+rxNorm.MmslCode+" "+rxNorm.Description);
			MsgBox.Show(this,RxNorms.GetMmslCodeByRxCui("1000005")+" <-- should be 26420");
			MsgBox.Show(this,RxNorms.GetMmslCodeByRxCui("1000002")+" <-- should be blank");*/
		}

		private void butNone_Click(object sender,EventArgs e) {
			SelectedRxNorm=new RxNorm();//Seems to work correctly but might cause problems. 8/24/2012 v12.4
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()<0) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedRxNorm=rxList[gridMain.GetSelectedIndex()];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	
		
	}
}