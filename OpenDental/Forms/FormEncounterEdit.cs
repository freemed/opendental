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
	public partial class FormEncounterEdit:Form {
		public Encounter EncCur;
		private Patient patCur;
		//public CodePickerContext CodePickerContextCur;

		public FormEncounterEdit() {
			InitializeComponent();
		}

		public void FormEncounters_Load(object sender,EventArgs e) {
			patCur=Patients.GetPat(EncCur.PatNum);
			this.Text+=" - "+patCur.GetNameLF();
			textDateTimeEnc.Text=EncCur.DateEncounter.ToString();
			comboProv.Items.Clear();
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				comboProv.Items.Add(ProviderC.ListShort[i].Abbr);
				if(ProviderC.ListShort[i].ProvNum==patCur.PriProv) {
					comboProv.SelectedIndex=i;
				}
			}
		}

		private void butPickProv_Click(object sender,EventArgs e) {
			FormProviderPick formp=new FormProviderPick();
			if(comboProv.SelectedIndex>-1) {
				formp.SelectedProvNum=ProviderC.ListShort[comboProv.SelectedIndex].ProvNum;
			}
			formp.ShowDialog();
			if(formp.DialogResult!=DialogResult.OK) {
				return;
			}
			comboProv.SelectedIndex=Providers.GetIndex(formp.SelectedProvNum);
		}

		//Not sure where to go from here yet, maybe just shortcut to procedurecode window.
		private void butCodePicker_Click(object sender,EventArgs e) {
			//FormCodePicker formcp=new FormCodePicker();
			//formcp.CodePickerContext=CodePickerContextCur;
			//formcp.ShowDialog();
			//if(formcp.DialogResult!=DialogResult.OK) {
			//	return;
			//}
			//textCodeValue.Text=formcp.CodeValue;
			//textCodeDescript.Text=formcp.CodeDescript;
			//EncCur.CodeSystemName=formcp.CodeSystemName;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(EncCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show("Delete?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				return;
			}
			Encounters.Delete(EncCur.EncounterNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {

		}

		private void butCancel_Click(object sender,EventArgs e) {

		}

		private void butSnomed_Click(object sender,EventArgs e) {

		}

		private void butHcpcs_Click(object sender,EventArgs e) {

		}

		private void butCdtCpt_Click(object sender,EventArgs e) {

		}
	}
}