using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrQuarterlyKeys:Form {
		private List<EhrQuarterlyKey> listKeys;

		public FormEhrQuarterlyKeys() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrQuarterlyKeys_Load(object sender,EventArgs e) {
			textPracticeTitle.Text=PrefC.GetString(PrefName.PracticeTitle);
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Year",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Quarter",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Key",100);
			gridMain.Columns.Add(col);
			listKeys=EhrQuarterlyKeys.Refresh(0);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listKeys.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listKeys[i].YearValue.ToString());
				row.Cells.Add(listKeys[i].QuarterValue.ToString());
				row.Cells.Add(listKeys[i].KeyValue);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrQuarterlyKeyEdit formE=new FormEhrQuarterlyKeyEdit();
			EhrQuarterlyKey keycur=listKeys[e.Row];
			keycur.IsNew=false;
			formE.KeyCur=keycur;
			formE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrQuarterlyKeyEdit formE=new FormEhrQuarterlyKeyEdit();
			EhrQuarterlyKey keycur=new EhrQuarterlyKey();
			keycur.IsNew=true;
			formE.KeyCur=keycur;
			formE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

	

		
	}
}