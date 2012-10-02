using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormScreenPatEdit:Form {
		public ScreenPat ScreenPatCur;
		private Patient PatCur;
		private ScreenGroup ScreenGroupCur;
		private Sheet ExamSheetCur;

		public FormScreenPatEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormScreenPatEdit_Load(object sender,EventArgs e) {
			PatCur=Patients.GetPat(ScreenPatCur.PatNum);
			textPatient.Text=PatCur.GetNameLF();
			ScreenGroupCur=ScreenGroups.GetScreenGroup(ScreenPatCur.ScreenGroupNum);
			textScreenGroup.Text=ScreenGroupCur.Description;
			ExamSheetCur=Sheets.GetSheet(ScreenPatCur.SheetNum);
			textSheet.Text=ExamSheetCur.Description;
		}

		private void butPatSelect_Click(object sender,EventArgs e) {
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.ShowDialog();
			PatCur=Patients.GetPat(FormPS.SelectedPatNum);
		}

		private void butScreenGroupSelect_Click(object sender,EventArgs e) {
			FormScreenGroups FormSG=new FormScreenGroups();
			FormSG.IsSelectionMode=true;
			FormSG.ShowDialog();
			ScreenGroupCur=FormSG.ScreenGroupCur;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}