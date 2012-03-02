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
	public partial class FormReferenceEdit:Form {
		private CustReference RefCur;
		private List<CustRefEntry> RefEntryList;

		public FormReferenceEdit(CustReference refCur) {
			InitializeComponent();
			Lan.F(this);
			RefCur=refCur;
		}

		private void FormReferenceEdit_Load(object sender,EventArgs e) {
			Patient patCur=Patients.GetLim(RefCur.PatNum);
			textName.Text=Patients.GetNameFL(patCur.LName,patCur.FName,patCur.Preferred,patCur.MiddleI);
			textNote.Text=RefCur.Note;
			checkBadRef.Checked=RefCur.IsBadRef;
			if(RefCur.DateMostRecent.Year>1880) {
				textRecentDate.Text=RefCur.DateMostRecent.ToShortDateString();
			}
			FillMain();
		}

		private void FillMain() {
			RefEntryList=CustRefEntries.GetEntryListForCustomer(RefCur.PatNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("PatNum",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Last Name",75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("First Name",75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Date Entry",50);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<RefEntryList.Count;i++) {
				row=new ODGridRow();
				Patient pat=Patients.GetLim(RefEntryList[i].PatNumRef);
				row.Cells.Add(pat.PatNum.ToString());
				row.Cells.Add(pat.LName);
				row.Cells.Add(pat.FName);
				row.Cells.Add(RefEntryList[i].DateEntry.ToShortDateString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormReferenceEntryEdit FormREE=new FormReferenceEntryEdit(RefEntryList[e.Row]);
			FormREE.ShowDialog();
			FillMain();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//No need to validate anything.
			RefCur.IsBadRef=checkBadRef.Checked;
			RefCur.Note=textNote.Text;
			CustReferences.Update(RefCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}