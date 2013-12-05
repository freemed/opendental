using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenDentBusiness;
using CodeBase;
using System.Globalization;
using System.Xml.XPath;
using System.IO;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrTriggers:Form {
		public List<EhrTrigger> ListEhrTriggers;

		public FormEhrTriggers() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrTriggers_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Description",200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Cardinality",140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Trigger Categories",200);
			gridMain.Columns.Add(col);
			ListEhrTriggers=EhrTriggers.GetAll();
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListEhrTriggers.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListEhrTriggers[i].Description);
				row.Cells.Add(ListEhrTriggers[i].Cardinality.ToString());
				row.Cells.Add(ListEhrTriggers[i].GetTriggerCategories());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAddTrigger_Click(object sender,EventArgs e) {
			FormEhrTriggerEdit FormETE=new FormEhrTriggerEdit();
			FormETE.EhrTriggerCur=new EhrTrigger();
			FormETE.IsNew=true;
			FormETE.ShowDialog();
			if(FormETE.DialogResult!=DialogResult.OK) {
				return;
			}
			ListEhrTriggers=EhrTriggers.GetAll();
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrTriggerEdit FormETE=new FormEhrTriggerEdit();
			FormETE.EhrTriggerCur=ListEhrTriggers[e.Row];
			FormETE.ShowDialog();
			if(FormETE.DialogResult!=DialogResult.OK) {
				return;
			}
			ListEhrTriggers=EhrTriggers.GetAll();
			FillGrid();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}