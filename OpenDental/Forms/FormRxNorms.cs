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
		public RxNorm selectedRxNorm;

		public FormRxNorms() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRxNorms_Load(object sender,EventArgs e) {
			#if DEBUG
			butRxNorm.Visible=true;
			#endif
		}

		private void FillGrid() {
			rxList=RxNorms.GetListByCodeOrDesc(textCode.Text);
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
		}

		private void gridMain_DoubleClick(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()<0) {
				return;
			}
			selectedRxNorm=rxList[gridMain.GetSelectedIndex()];
			DialogResult=DialogResult.OK;
		}

		private void butSearch_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butRxNorm_Click(object sender,EventArgs e) {
			RxNorms.CreateFreshRxNormTableFromZip();
			RxNorm rxNorm=RxNorms.GetOne(1);
			MsgBox.Show(this,rxNorm.RxNormNum+" "+rxNorm.RxCui+" "+rxNorm.MmslCode+" "+rxNorm.Description);
			MsgBox.Show(this,RxNorms.GetMmslCodeByRxCui("1000005")+" <-- should be 26420");
			MsgBox.Show(this,RxNorms.GetMmslCodeByRxCui("1000002")+" <-- should be blank");
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()<0) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			selectedRxNorm=rxList[gridMain.GetSelectedIndex()];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}