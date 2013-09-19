using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCQM2014Setup:Form {

		public FormCQM2014Setup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCQM2014Setup_Load(object sender,EventArgs e) {
			listRecommendCodes.Items.Add("none");
			listRecommendCodes.Items.Add("90526000");
			listRecommendCodes.Items.Add("185349003");
			listRecommendCodes.Items.Add("185463005");
			listRecommendCodes.Items.Add("185465003");
			listRecommendCodes.Items.Add("270427003");
			listRecommendCodes.Items.Add("270430005");
			listRecommendCodes.Items.Add("308335008");
			listRecommendCodes.Items.Add("390906007");
			listRecommendCodes.Items.Add("406547006");
			listRecommendCodes.SetSelected(7,true);
			textCodeDescript.Text=Snomeds.GetByCode(listRecommendCodes.SelectedItem.ToString()).Description;
			labelEncounterWarning.Visible=false;
		}

		private void listRecommendEncCodes_Click(object sender,EventArgs e) {
			textCodeValue.Text="";
			if(listRecommendCodes.SelectedIndex==0) {//none
				textCodeDescript.Clear();
				labelEncounterWarning.Visible=true;
			}
			else {
				textCodeDescript.Text=Snomeds.GetByCode(listRecommendCodes.SelectedItem.ToString()).Description;
				labelEncounterWarning.Visible=false;
			}
		}

		private void butSnomed_Click(object sender,EventArgs e) {
			FormSnomeds formS=new FormSnomeds();
			formS.IsSelectionMode=true;
			formS.ShowDialog();
			if(formS.DialogResult==DialogResult.OK) {
				listRecommendCodes.ClearSelected();
				textCodeValue.Text=formS.SelectedSnomed.SnomedCode;
				textCodeDescript.Text=formS.SelectedSnomed.Description;
				labelEncounterWarning.Visible=true;
			}
		}

		private void butHcpcs_Click(object sender,EventArgs e) {
			//FormHcpcs formH=new FormHcpcs();
			//formH.ShowDialog();
			//if(formH.DialogResult==DialogResult.OK) {
			//	listRecommendCodes.ClearSelected();
			//	textCodeValue.Text=formH.SelectedHcpcs.HcpcsCode;
			//	textCodeDescript.Text=formH.SelectedHcpcs.Description;
			//	labelEncounterWarning.Visible=true;
			//}
		}

		private void butCdtCpt_Click(object sender,EventArgs e) {
			FormProcCodes formP=new FormProcCodes();
			formP.IsSelectionMode=true;
			formP.ShowDialog();
			if(formP.DialogResult==DialogResult.OK) {
				listRecommendCodes.ClearSelected();
				ProcedureCode procCur=ProcedureCodes.GetProcCode(formP.SelectedCodeNum);
				textCodeValue.Text=procCur.ProcCode;
				textCodeDescript.Text=procCur.Descript;
				labelEncounterWarning.Visible=true;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}