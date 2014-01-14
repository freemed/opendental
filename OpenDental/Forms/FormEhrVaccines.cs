using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrVaccines:Form {
		public Patient PatCur;
		private List<VaccinePat> VaccineList;

		public FormEhrVaccines() {
			InitializeComponent();
		}

		private void FormVaccines_Load(object sender,EventArgs e) {
			FillGridVaccine();
			listVacShareOk.SelectedIndex=(int)PatCur.VacShareOk;
		}

		private void FillGridVaccine() {
			gridVaccine.BeginUpdate();
			gridVaccine.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date",90);
			gridVaccine.Columns.Add(col);
			col=new ODGridColumn("Vaccine",100);
			gridVaccine.Columns.Add(col);
			VaccineList=VaccinePats.Refresh(PatCur.PatNum);
			gridVaccine.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<VaccineList.Count;i++) {
				row=new ODGridRow();
				if(VaccineList[i].DateTimeStart.Year<1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(VaccineList[i].DateTimeStart.ToShortDateString());
				}
				string str=VaccineDefs.GetOne(VaccineList[i].VaccineDefNum).VaccineName;
				if(VaccineList[i].NotGiven) {
					str+=" (not given: "+VaccineList[i].Note+")";
				}
				row.Cells.Add(str);
				gridVaccine.Rows.Add(row);
			}
			gridVaccine.EndUpdate();
		}

		private void gridVaccine_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			if(gridVaccine.GetSelectedIndex()==-1) {
				return;
			}
			FormEhrVaccinePatEdit FormV=new FormEhrVaccinePatEdit();
			FormV.VaccinePatCur=VaccineList[gridVaccine.GetSelectedIndex()];
			FormV.ShowDialog();
			FillGridVaccine();
		}

		private void butAddVaccine_Click(object sender,EventArgs e) {
			FormEhrVaccinePatEdit FormV=new FormEhrVaccinePatEdit();
			FormV.VaccinePatCur=new VaccinePat();
			FormV.VaccinePatCur.PatNum=PatCur.PatNum;
			FormV.VaccinePatCur.DateTimeStart=DateTime.Now;
			FormV.VaccinePatCur.DateTimeEnd=DateTime.Now;
			FormV.IsNew=true;
			FormV.ShowDialog();
			FillGridVaccine();
			}

		private void butExport_Click(object sender,EventArgs e) {
			if(gridVaccine.SelectedIndices.Length==0) {
				MessageBox.Show("Please select at least one vaccine.");
				return;
			}
			List<VaccinePat> vaccines=new List<VaccinePat>();
			for(int i=0;i<gridVaccine.SelectedIndices.Length;i++) {
				vaccines.Add(VaccineList[gridVaccine.SelectedIndices[i]]);
			}
			OpenDentBusiness.HL7.EhrVXU vxu=null;
			try {
				vxu=new OpenDentBusiness.HL7.EhrVXU(PatCur,vaccines);
			}
			catch(Exception ex) {//Exception happens when validation fails.
				MessageBox.Show(ex.Message);//Show validation error messages.
				return;
			}
			string outputStr=vxu.GenerateMessage();
			SaveFileDialog dlg=new SaveFileDialog();
			dlg.FileName="vxu.txt";
			DialogResult result=dlg.ShowDialog();
			if(result!=DialogResult.OK){
				return;
			}
			if(File.Exists(dlg.FileName)) {
				if(MessageBox.Show("Overwrite existing file?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
					return;
				}
			}
			File.WriteAllText(dlg.FileName,outputStr);
			MessageBox.Show("Saved");
		}

		private void butSubmitImmunization_Click(object sender,EventArgs e) {
			if(gridVaccine.SelectedIndices.Length==0) {
				MessageBox.Show("Please select at least one vaccine.");
				return;
			}
			List<VaccinePat> vaccines=new List<VaccinePat>();
			for(int i=0;i<gridVaccine.SelectedIndices.Length;i++) {
				vaccines.Add(VaccineList[gridVaccine.SelectedIndices[i]]);
			}
			OpenDentBusiness.HL7.EhrVXU vxu=null;
			try {
				vxu=new OpenDentBusiness.HL7.EhrVXU(PatCur,vaccines);
			}
			catch(Exception ex) {//Exception happens when validation fails.
				MessageBox.Show(ex.Message);//Show validation error messages.
				return;
			}
			string outputStr=vxu.GenerateMessage();
			Cursor=Cursors.WaitCursor;
			try {
				EmailMessages.SendTestUnsecure("Immunization Submission","vxu.txt",outputStr);
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			MessageBox.Show("Sent");
		}

		private void listVacShareOk_MouseClick(object sender,MouseEventArgs e) {
			Patient patOld=PatCur.Copy();
			PatCur.VacShareOk=(YN)listVacShareOk.SelectedIndex;
			Patients.Update(PatCur,patOld);
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

	}
}
