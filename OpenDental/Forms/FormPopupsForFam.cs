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
	public partial class FormPopupsForFam:Form {
		public Patient PatCur;
		private List<Popup> PopupList;

		public FormPopupsForFam() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPopupsForFam_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			PopupList=Popups.GetForFamily(PatCur);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePopupsForFamily","Patient"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePopupsForFamily","Level"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePopupsForFamily","Disabled"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePopupsForFamily","Popup Message"),120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<PopupList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(Patients.GetPat(PopupList[i].PatNum).GetNameLF());
				row.Cells.Add(Lan.g("enumEnumPopupLevel",PopupList[i].PopupLevel.ToString()));
				row.Cells.Add(PopupList[i].IsDisabled?"X":"");
				row.Cells.Add(PopupList[i].Description);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPopupEdit FormPE=new FormPopupEdit();
			FormPE.PopupCur=PopupList[e.Row];
			FormPE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormPopupEdit FormPE=new FormPopupEdit();
			Popup popup=new Popup();
			popup.PatNum=PatCur.PatNum;
			popup.PopupLevel=EnumPopupLevel.Patient;
			popup.IsNew=true;
			FormPE.PopupCur=popup;
			FormPE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}
	}
}