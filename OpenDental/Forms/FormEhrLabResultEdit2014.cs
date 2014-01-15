using System;
using System.Windows.Forms;
using EhrLaboratories;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrLabResultEdit2014:Form {
		public EhrLabResult _ehrLabResultCur;

		public FormEhrLabResultEdit2014() {
			InitializeComponent();
		}

		private void FormLabResultEdit_Load(object sender,EventArgs e) {
			textDateTimeObs.Text=_ehrLabResultCur.ObservationDateTime;
			textDateTimeAnalysis.Text=_ehrLabResultCur.AnalysisDateTime;
			#region Lab Result Status
			comboObsStatus.Items.Clear();
			//TODO: fill lab result status combo
			#endregion
			textLOINC.Text=_ehrLabResultCur.ObservationIdentifierID;
			textLOINCDescription.Text=_ehrLabResultCur.ObservationIdentifierText;
			#region Performing Organization
			#endregion
		}

		///<summary></summary>
		private bool EntriesAreValid() {
			//TODO: validate the controls
			return true;
		}

		private void butOk_Click(object sender,EventArgs e) {
			if(!EntriesAreValid()) {
				return;
			}
			//TODO: Insert the lab result
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(_ehrLabResultCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"Delete Lab Result?")) {
				return;
			}
			//TODO: Actually delete lab result?
			DialogResult=DialogResult.OK;
		}

		
	}
}
