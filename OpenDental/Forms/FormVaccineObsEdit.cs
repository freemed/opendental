using System;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormVaccineObsEdit:Form {

		private VaccineObs _vaccineObsCur;

		public FormVaccineObsEdit(VaccineObs vaccineObs) {
			InitializeComponent();
			Lan.F(this);
			_vaccineObsCur=vaccineObs;
		}

		private void FormVaccineObsEdit_Load(object sender,EventArgs e) {
			comboObservationQuestion.Items.Clear();
			string[] arrayQuestionNames=Enum.GetNames(typeof(VaccineObsIdentifier));
			for(int i=0;i<arrayQuestionNames.Length;i++) {
				comboObservationQuestion.Items.Add(arrayQuestionNames[i]);
				VaccineObsIdentifier vaccineObsIdentifier=(VaccineObsIdentifier)i;
				if(_vaccineObsCur.IdentifyingCode==vaccineObsIdentifier) {
					comboObservationQuestion.SelectedIndex=i;
				}
			}
			listValueType.Items.Clear();
			string[] arrayValueTypeNames=Enum.GetNames(typeof(VaccineObsType));
			for(int i=0;i<arrayValueTypeNames.Length;i++) {
				listValueType.Items.Add(arrayValueTypeNames[i]);
				VaccineObsType vaccineObsType=(VaccineObsType)i;
				if(_vaccineObsCur.ValType==vaccineObsType) {
					listValueType.SelectedIndex=i;
				}
			}
			listCodeSystem.Items.Clear();
			string[] arrayCodeSystems=Enum.GetNames(typeof(VaccineObsValCodeSystem));
			for(int i=0;i<arrayCodeSystems.Length;i++) {
				listCodeSystem.Items.Add(arrayCodeSystems[i].ToString());
				VaccineObsValCodeSystem valCodeSystem=(VaccineObsValCodeSystem)i;
				if(_vaccineObsCur.ValCodeSystem==valCodeSystem) {
					listCodeSystem.SelectedIndex=i;
				}
			}
			textValue.Text=_vaccineObsCur.ValReported;
			comboUnits.Items.Clear();
			comboUnits.Items.Add("none");
			comboUnits.SelectedIndex=0;
			for(int i=0;i<DrugUnits.Listt.Count;i++) {
				comboUnits.Items.Add(DrugUnits.Listt[i].UnitIdentifier);
				if(DrugUnits.Listt[i].UnitIdentifier==_vaccineObsCur.ValUnit) {
					comboUnits.SelectedIndex=i+1;
				}
			}
			if(_vaccineObsCur.DateObs.Year>1880) {
				textDateObserved.Text=_vaccineObsCur.DateObs.ToShortDateString();
			}
			textMethodCode.Text=_vaccineObsCur.MethodCode;
			SetFlags();
		}

		private void comboObservationQuestion_SelectionChangeCommitted(object sender,EventArgs e) {
			SetFlags();
		}

		private void listValueType_SelectedIndexChanged(object sender,EventArgs e) {
			SetFlags();
		}

		private void SetFlags() {
			VaccineObsType vaccineObsType=(VaccineObsType)listValueType.SelectedIndex;
			if(vaccineObsType==VaccineObsType.Coded) {
				listCodeSystem.Enabled=true;
			}
			else {
				listCodeSystem.Enabled=false;
			}
			if(vaccineObsType==VaccineObsType.Numeric) {
				comboUnits.Enabled=true;
			}
			else {
				comboUnits.Enabled=false;
			}
			VaccineObsIdentifier vaccineObsIdentifier=(VaccineObsIdentifier)comboObservationQuestion.SelectedIndex;
			if(vaccineObsIdentifier==VaccineObsIdentifier.FundPgmEligCat) {
				textMethodCode.ReadOnly=false;
			}
			else {
				textMethodCode.ReadOnly=true;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			_vaccineObsCur.VaccinePatNum=0;//So the calling code knows that the vaccineobs was deleted.
			if(_vaccineObsCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show("Delete?","Delete?",MessageBoxButtons.OKCancel)==DialogResult.Cancel) {
				return;
			}
			VaccineObses.Delete(_vaccineObsCur.VaccinePatNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textValue.Text.Trim()=="") {
				MsgBox.Show(this,"Missing value.");
				return;
			}
			VaccineObsType vaccineObsType=(VaccineObsType)listValueType.SelectedIndex;
			if(vaccineObsType==VaccineObsType.Coded) {
				//Any value is allowed.
			}
			else if(vaccineObsType==VaccineObsType.Dated) {
				try {
					DateTime.Parse(textValue.Text);
				}
				catch(Exception) {
					MsgBox.Show(this,"Value must be a valid date.");
					return;
				}
			}
			else if(vaccineObsType==VaccineObsType.Numeric) {
				try {
					double.Parse(textValue.Text);
				}
				catch(Exception) {
					MsgBox.Show(this,"Value must be a valid number.");
					return;
				}
			}
			else if(vaccineObsType==VaccineObsType.Text) {
				//Any value is allowed.
			}
			else { //DateAndTime
				try {
					DateTime.Parse(textValue.Text);
				}
				catch(Exception) {
					MsgBox.Show(this,"Value must be a valid date and time.");
					return;
				}
			}
			if(comboUnits.Enabled && comboUnits.SelectedIndex==0) {
				MsgBox.Show(this,"Missing units.");
				return;
			}
			if(textDateObserved.errorProvider1.GetError(textDateObserved)!="") {
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(!textMethodCode.ReadOnly && textMethodCode.Text.Trim()=="") {
				MsgBox.Show(this,"Missing method code.");
				return;
			}
			_vaccineObsCur.IdentifyingCode=(VaccineObsIdentifier)comboObservationQuestion.SelectedIndex;
			_vaccineObsCur.ValType=(VaccineObsType)listValueType.SelectedIndex;
			_vaccineObsCur.ValCodeSystem=(VaccineObsValCodeSystem)listCodeSystem.SelectedIndex;
			_vaccineObsCur.ValReported=textValue.Text;
			_vaccineObsCur.ValUnit=comboUnits.Items[comboUnits.SelectedIndex].ToString();
			_vaccineObsCur.DateObs=DateTime.MinValue;
			if(textDateObserved.Text!="") {
				_vaccineObsCur.DateObs=PIn.Date(textDateObserved.Text);
			}
			_vaccineObsCur.MethodCode=textMethodCode.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}