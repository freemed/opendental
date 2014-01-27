using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrAptObsEdit:Form {

		private EhrAptObs _ehrAptObs=null;
		private Loinc _loincCur=null;
		private string _strValCodeSystem="";
		private Loinc _loincValue=null;
		private Snomed _snomedValue=null;
		private ICD9 _icd9Value=null;
		private Icd10 _icd10Value=null;

		public FormEhrAptObsEdit(EhrAptObs ehrAptObs) {
			InitializeComponent();
			Lan.F(this);
			_ehrAptObs=ehrAptObs;
		}

		private void FormEhrAptObsEdit_Load(object sender,EventArgs e) {
			_loincCur=Loincs.GetByCode(_ehrAptObs.LoincCode);
			textLoinc.Text="";
			if(_loincCur!=null) {
				textLoinc.Text=_loincCur.NameShort;
			}
			listValueType.Items.Clear();
			string[] arrayValueTypeNames=Enum.GetNames(typeof(EhrAptObsType));
			for(int i=0;i<arrayValueTypeNames.Length;i++) {
				listValueType.Items.Add(arrayValueTypeNames[i]);
				EhrAptObsType ehrAptObsType=(EhrAptObsType)i;
				if(_ehrAptObs.ValType==ehrAptObsType) {
					listValueType.SelectedIndex=i;
				}
			}
			if(_ehrAptObs.ValType==EhrAptObsType.Coded) {
				_strValCodeSystem=_ehrAptObs.ValCodeSystem;
				if(_ehrAptObs.ValCodeSystem=="LOINC") {
					_loincValue=Loincs.GetByCode(_ehrAptObs.ValReported);
					textValue.Text=_loincValue.NameShort;
				}
				else if(_ehrAptObs.ValCodeSystem=="SNOMEDCT") {
					_snomedValue=Snomeds.GetByCode(_ehrAptObs.ValReported);
					textValue.Text=_snomedValue.Description;
				}
				else if(_ehrAptObs.ValCodeSystem=="ICD9") {
					_icd9Value=ICD9s.GetByCode(_ehrAptObs.ValReported);
					textValue.Text=_icd9Value.Description;
				}
				else if(_ehrAptObs.ValCodeSystem=="ICD10") {
					_icd10Value=Icd10s.GetByCode(_ehrAptObs.ValReported);
					textValue.Text=_icd10Value.Description;
				}
			}
			else {
				textValue.Text=_ehrAptObs.ValReported;
			}
			comboUnits.Items.Clear();
			comboUnits.Items.Add("none");
			comboUnits.SelectedIndex=0;
			for(int i=0;i<DrugUnits.Listt.Count;i++) {
				comboUnits.Items.Add(DrugUnits.Listt[i].UnitIdentifier);
				if(DrugUnits.Listt[i].UnitIdentifier==_ehrAptObs.ValUnit) {
					comboUnits.SelectedIndex=i+1;
				}
			}
			SetFlags();
		}

		private void listValueType_SelectedIndexChanged(object sender,EventArgs e) {
			textValue.Text="";
			_loincValue=null;
			_snomedValue=null;
			_icd9Value=null;
			_icd10Value=null;
			SetFlags();
		}

		private void SetFlags() {
			labelValue.Text="Value";
			textValue.ReadOnly=false;
			butPickValueLoinc.Enabled=false;
			butPickValueSnomedct.Enabled=false;
			butPickValueIcd9.Enabled=false;
			butPickValueIcd10.Enabled=false;
			if(listValueType.SelectedIndex==(int)EhrAptObsType.Coded) {
				labelValue.Text=_strValCodeSystem+" Value";
				textValue.ReadOnly=true;
				butPickValueLoinc.Enabled=true;
				butPickValueSnomedct.Enabled=true;
				butPickValueIcd9.Enabled=true;
				butPickValueIcd10.Enabled=true;
			}
			if(listValueType.SelectedIndex==(int)EhrAptObsType.Numeric) {
				comboUnits.Enabled=true;
			}
			else {
				comboUnits.Enabled=false;
			}
		}

		private void butPickLoinc_Click(object sender,EventArgs e) {
			FormLoincs formL=new FormLoincs();
			formL.IsSelectionMode=true;
			if(formL.ShowDialog()==DialogResult.OK) {
				_loincCur=formL.SelectedLoinc;
				textLoinc.Text=_loincCur.NameShort;
			}
		}

		private void butPickValueLoinc_Click(object sender,EventArgs e) {
			FormLoincs formL=new FormLoincs();
			formL.IsSelectionMode=true;
			if(formL.ShowDialog()==DialogResult.OK) {
				_loincValue=formL.SelectedLoinc;
				textValue.Text=_loincValue.NameShort;
				_strValCodeSystem="LOINC";
				labelValue.Text=_strValCodeSystem+" Value";
			}
		}

		private void butPickValueSnomedct_Click(object sender,EventArgs e) {
			FormSnomeds formS=new FormSnomeds();
			formS.IsSelectionMode=true;
			if(formS.ShowDialog()==DialogResult.OK) {
				_snomedValue=formS.SelectedSnomed;
				textValue.Text=_snomedValue.Description;
				_strValCodeSystem="SNOMEDCT";
				labelValue.Text=_strValCodeSystem+" Value";
			}
		}

		private void butPickValueIcd9_Click(object sender,EventArgs e) {
			FormIcd9s formI=new FormIcd9s();
			formI.IsSelectionMode=true;
			if(formI.ShowDialog()==DialogResult.OK) {
				_icd9Value=formI.SelectedIcd9;
				textValue.Text=_icd9Value.Description;
				_strValCodeSystem="ICD9";
				labelValue.Text=_strValCodeSystem+" Value";
			}
		}

		private void butPickValueIcd10_Click(object sender,EventArgs e) {
			FormIcd10s formI=new FormIcd10s();
			formI.IsSelectionMode=true;
			if(formI.ShowDialog()==DialogResult.OK) {
				_icd10Value=formI.SelectedIcd10;
				textValue.Text=_icd10Value.Description;
				_strValCodeSystem="ICD10";
				labelValue.Text=_strValCodeSystem+" Value";
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(_ehrAptObs.IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			else {
				EhrAptObses.Delete(_ehrAptObs.EhrAptObsNum);
				_ehrAptObs.EhrAptObsNum=0;//Signal to the calling code that the object has been deleted.
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(_loincCur==null) {
				MsgBox.Show(this,"Missing LOINC.");
				return;
			}
			if(comboUnits.Enabled && comboUnits.SelectedIndex==0) {
				MsgBox.Show(this,"Missing units.");
				return;
			}
			if(listValueType.SelectedIndex==(int)EhrAptObsType.Coded && _loincValue==null && _snomedValue==null && _icd9Value==null && _icd10Value==null) {
				MsgBox.Show(this,"Missing value code.");
				return;
			}
			if(listValueType.SelectedIndex!=(int)EhrAptObsType.Coded && textValue.Text=="") {
				MsgBox.Show(this,"Missing value.");
				return;
			}
			_ehrAptObs.LoincCode=_loincCur.LoincCode;
			_ehrAptObs.ValType=(EhrAptObsType)listValueType.SelectedIndex;
			if(_ehrAptObs.ValType==EhrAptObsType.Coded) {
				_ehrAptObs.ValCodeSystem=_strValCodeSystem;
				if(_strValCodeSystem=="LOINC") {
					_ehrAptObs.ValReported=_loincValue.LoincCode;
				}
				else if(_strValCodeSystem=="SNOMEDCT") {
					_ehrAptObs.ValReported=_snomedValue.SnomedCode;
				}
				else if(_strValCodeSystem=="ICD9") {
					_ehrAptObs.ValReported=_icd9Value.ICD9Code;
				}
				else if(_strValCodeSystem=="ICD10") {
					_ehrAptObs.ValReported=_icd10Value.Icd10Code;
				}
			}
			else {
				_ehrAptObs.ValCodeSystem="";
				_ehrAptObs.ValReported=textValue.Text;
			}
			_ehrAptObs.ValUnit=comboUnits.Items[comboUnits.SelectedIndex].ToString();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}