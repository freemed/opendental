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
	public partial class FormPatientPickWebForm:Form {
		private List<Patient> listPats;
		///<summary>If OK.  Can be zero to indicate create new patient.  A result of Cancel indicates quit importing altogether.</summary>
		public long SelectedPatNum;
		public string LnameEntered;
		public string FnameEntered;
		public DateTime BdateEntered;

		public FormPatientPickWebForm() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPatientPickWebForm_Load(object sender,EventArgs e) {
			textLName.Text=LnameEntered;
			textFName.Text=FnameEntered;
			textBirthdate.Text=BdateEntered.ToShortDateString();
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Last Name"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"First Name"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Birthdate"),110);
			gridMain.Columns.Add(col);
			listPats=Patients.GetSimilarList(LnameEntered,FnameEntered,BdateEntered);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listPats.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listPats[i].LName);
				row.Cells.Add(listPats[i].FName);
				row.Cells.Add(listPats[i].Birthdate.ToShortDateString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			SelectedPatNum=listPats[e.Row].PatNum;
			//Security log for patient select.
			Patient pat=Patients.GetPat(SelectedPatNum);
			SecurityLogs.MakeLogEntry(Permissions.SheetEdit,SelectedPatNum,"In the 'Pick Patient for Web Form', this user double clicked a name in the suggested list.\r\n"
				+"This caused the web form for this patient: "+LnameEntered+", "+FnameEntered+" "+BdateEntered.ToShortDateString()+"\r\n"
				+"to be manually attached to this other patient: "+pat.LName+", "+pat.FName+" "+pat.Birthdate.ToShortDateString());
			DialogResult=DialogResult.OK;
		}

		private void butSelect_Click(object sender,EventArgs e) {
			FormPatientSelect FormPs=new FormPatientSelect();
			FormPs.SelectionModeOnly=true;
			FormPs.ShowDialog();
			if(FormPs.DialogResult!=DialogResult.OK) {
				return;
			}
			SelectedPatNum=FormPs.SelectedPatNum;
			//Security log for patient select.
			Patient pat=Patients.GetPat(SelectedPatNum);
			SecurityLogs.MakeLogEntry(Permissions.SheetEdit,SelectedPatNum,"In the 'Pick Patient for Web Form', this user clicked the 'Select' button.\r\n"
				+"By clicking the 'Select' button, the web form for this patient: "+LnameEntered+", "+FnameEntered+" "+BdateEntered.ToShortDateString()+"\r\n"
				+"was manually attached to this other patient: "+pat.LName+", "+pat.FName+" "+pat.Birthdate.ToShortDateString());
			DialogResult=DialogResult.OK;
		}

		private void butNew_Click(object sender,EventArgs e) {
			SelectedPatNum=0;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}
}