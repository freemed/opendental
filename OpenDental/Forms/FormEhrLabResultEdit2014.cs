using System;
using System.Collections.Generic;
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
			textObsDateTime.Text=_ehrLabResultCur.ObservationDateTime;
			textAnalysisDateTime.Text=_ehrLabResultCur.AnalysisDateTime;
			#region Observation Identifier (LOINC Codes)
			textObsIDCodeSystemName.Text		=_ehrLabResultCur.ObservationIdentifierCodeSystemName;
			textObsID.Text									=_ehrLabResultCur.ObservationIdentifierID;
			textObsIDText.Text							=_ehrLabResultCur.ObservationIdentifierText;
			textObsIDCodeSystemNameAlt.Text	=_ehrLabResultCur.ObservationIdentifierCodeSystemNameAlt;
			textObsIDAlt.Text								=_ehrLabResultCur.ObservationIdentifierIDAlt;
			textObsIDTextAlt.Text						=_ehrLabResultCur.ObservationIdentifierTextAlt;
			textObsIDOrigText.Text					=_ehrLabResultCur.ObservationIdentifierTextOriginal;
			textObsSub.Text									=_ehrLabResultCur.ObservationIdentifierSub;
			#endregion
			#region Observation Value
			textObsDateTime.Text=_ehrLabResultCur.ObservationValueDateTime;
			textAnalysisDateTime.Text=_ehrLabResultCur.AnalysisDateTime;
			#region Observation Status
			comboObsStatus.Items.Clear();
			//Fill obs status combo with HL70085 enum.  Not sure if blank is acceptable.
			List<string> listObsStatus=EhrLabResults.GetHL70085Descriptions();
			comboObsStatus.Items.AddRange(listObsStatus.ToArray());
			comboObsStatus.SelectedIndex=(int)Enum.Parse(typeof(HL70085),_ehrLabResultCur.ObservationResultStatus.ToString(),true)+1;
			#endregion
			#region Value Type
			comboObsValueType.Items.Clear();
			//Fill obs value type combo with HL70125 enum.  Not sure if blank is acceptable.
			List<string> listObsValueType=EhrLabResults.GetHL70125Descriptions();
			comboObsValueType.Items.AddRange(listObsValueType.ToArray());
			comboObsValueType.SelectedIndex=(int)Enum.Parse(typeof(HL70125),_ehrLabResultCur.ValueType.ToString(),true)+1;
			#endregion
			textObsValue.Text=GetObservationText();
			#region Coded Elements
			textObsElementCodeSystem.Text		=_ehrLabResultCur.ObservationValueCodedElementCodeSystemName;
			textObsElementID.Text						=_ehrLabResultCur.ObservationValueCodedElementID;
			textObsElementText.Text					=_ehrLabResultCur.ObservationValueCodedElementText;
			textObsElementCodeSystemAlt.Text=_ehrLabResultCur.ObservationValueCodedElementCodeSystemNameAlt;
			textObsElementIDAlt.Text				=_ehrLabResultCur.ObservationValueCodedElementIDAlt;
			textObsElementTextAlt.Text			=_ehrLabResultCur.ObservationValueCodedElementTextAlt;
			textObsElementOrigText.Text			=_ehrLabResultCur.ObservationValueCodedElementTextOriginal;
			#endregion
			#region Structured Numeric
			textStructNumComp.Text			=_ehrLabResultCur.ObservationValueComparator;
			textStructNumFirst.Text			=_ehrLabResultCur.ObservationValueNumber1.ToString();
			textStructNumSeparator.Text	=_ehrLabResultCur.ObservationValueSeparatorOrSuffix;
			textStructNumSecond.Text		=_ehrLabResultCur.ObservationValueNumber2.ToString();
			#endregion
			#region Unit of Measure
			textObsCodeSystem.Text		=_ehrLabResultCur.UnitsCodeSystemName;
			textObsUnitsID.Text				=_ehrLabResultCur.UnitsID;
			textObsUnitsText.Text			=_ehrLabResultCur.UnitsText;
			textObsCodeSystemAlt.Text	=_ehrLabResultCur.UnitsCodeSystemNameAlt;
			textObsUnitsIDAlt.Text		=_ehrLabResultCur.UnitsIDAlt;
			textObsUnitsTextAlt.Text	=_ehrLabResultCur.UnitsTextAlt;
			textObsUnitsTextOrig.Text	=_ehrLabResultCur.UnitsTextOriginal;
			#endregion
			#endregion
			#region Performing Organization
			#region Name
			textPerfOrgName.Text=_ehrLabResultCur.PerformingOrganizationName;
			#region Identifier Type
			comboPerfOrgIdType.Items.Clear();
			//Fill identifier type combo with HL70203 enum.  Not sure if blank is acceptable.
			List<string> listPerfOrgIdType=EhrLabs.GetHL70203Descriptions();
			comboPerfOrgIdType.Items.AddRange(listPerfOrgIdType.ToArray());
			comboPerfOrgIdType.SelectedIndex=(int)Enum.Parse(typeof(HL70203),_ehrLabResultCur.PerformingOrganizationIdentifierTypeCode.ToString(),true)+1;
			#endregion
			textPerfOrgIdentifier.Text=_ehrLabResultCur.PerformingOrganizationIdentifier;
			#region Assigning Authority
			textPerfOrgAssignIdType.Text=_ehrLabResultCur.PerformingOrganizationNameAssigningAuthorityUniversalIdType;
			textPerfOrgNamespaceID.Text=_ehrLabResultCur.PerformingOrganizationNameAssigningAuthorityNamespaceId;
			textPerfOrgUniversalID.Text=_ehrLabResultCur.PerformingOrganizationNameAssigningAuthorityUniversalId;
			#endregion
			#endregion
			#region Address
			#region Address Type
			comboPerfOrgAddressType.Items.Clear();
			//Fill address type combo with HL70190 enum.  Not sure if blank is acceptable.
			List<string> listPerfOrgAddressType=EhrLabResults.GetHL70190Descriptions();
			comboPerfOrgAddressType.Items.AddRange(listPerfOrgAddressType.ToArray());
			comboPerfOrgAddressType.SelectedIndex=(int)Enum.Parse(typeof(HL70190),_ehrLabResultCur.PerformingOrganizationAddressAddressType.ToString(),true)+1;
			#endregion
			textPerfOrgStreet.Text					=_ehrLabResultCur.PerformingOrganizationAddressStreet;
			textPerfOrgOtherDesignation.Text=_ehrLabResultCur.PerformingOrganizationAddressOtherDesignation;
			textPerfOrgCity.Text						=_ehrLabResultCur.PerformingOrganizationAddressCity;
			#region State or Province
			comboPerfOrgState.Items.Clear();
			//Fill state combo with USPSAlphaStateCode enum.  Not sure if blank is acceptable.
			List<string> listPerfOrgState=EhrLabResults.GetUSPSAlphaStateCodeDescriptions();
			comboPerfOrgState.Items.AddRange(listPerfOrgState.ToArray());
			comboPerfOrgState.SelectedIndex=(int)Enum.Parse(typeof(USPSAlphaStateCode),_ehrLabResultCur.PerformingOrganizationAddressStateOrProvince.ToString(),true)+1;
			#endregion
			textPerfOrgZip.Text			=_ehrLabResultCur.PerformingOrganizationAddressZipOrPostalCode;
			textPerfOrgCountry.Text	=_ehrLabResultCur.PerformingOrganizationAddressCountryCode;
			textPerfOrgCounty.Text	=_ehrLabResultCur.PerformingOrganizationAddressCountyOrParishCode;
			#endregion
			#region Medical Director
			#region Identifier Type
			comboMedDirIdType.Items.Clear();
			//Fill medical director type combo with HL70203 enum.  Not sure if blank is acceptable.
			List<string> listMedDirIdType=EhrLabs.GetHL70203Descriptions();
			comboMedDirIdType.Items.AddRange(listMedDirIdType.ToArray());
			comboMedDirIdType.SelectedIndex=(int)Enum.Parse(typeof(HL70203),_ehrLabResultCur.MedicalDirectorIdentifierTypeCode.ToString(),true)+1;
			#endregion
			textMedDirIdentifier.Text=_ehrLabResultCur.MedicalDirectorID;
			#region Name Type
			comboMedDirNameType.Items.Clear();
			//Fill medical director name combo with HL70200 enum.  Not sure if blank is acceptable.
			List<string> listMedDirNameType=EhrLabResults.GetHL70200Descriptions();
			comboMedDirNameType.Items.AddRange(listMedDirIdType.ToArray());
			comboMedDirNameType.SelectedIndex=(int)Enum.Parse(typeof(HL70200),_ehrLabResultCur.MedicalDirectorNameTypeCode.ToString(),true)+1;
			#endregion
			textMedDirLastName.Text		=_ehrLabResultCur.MedicalDirectorLName;
			textMedDirFirstName.Text	=_ehrLabResultCur.MedicalDirectorFName;
			textMedDirMiddleName.Text	=_ehrLabResultCur.MedicalDirectorMiddleNames;
			textMedDirSuffix.Text			=_ehrLabResultCur.MedicalDirectorSuffix;
			textMedDirPrefix.Text			=_ehrLabResultCur.MedicalDirectorPrefix;
			#region Assigning Authority
			textMedDirAssignIdType.Text=_ehrLabResultCur.MedicalDirectorAssigningAuthorityIDType;
			textMedDirNamespaceID.Text=_ehrLabResultCur.MedicalDirectorAssigningAuthorityNamespaceID;
			textMedDirUniversalID.Text=_ehrLabResultCur.MedicalDirectorAssigningAuthorityUniversalID;
			#endregion
			#endregion
			#endregion
			textReferenceRange.Text=_ehrLabResultCur.referenceRange;
		}

		///<summary>Gets the observation text dynamically from the result passed in.  Returns empty string if unknown value type.</summary>
		private string GetObservationText() {
			//No need to check RemotingRole;
			switch(_ehrLabResultCur.ValueType) {
				case HL70125.CE:
				case HL70125.CWE:
				case HL70125.SN:
					return "";//Handled later in Load.
				case HL70125.DT:
				case HL70125.TS:
					return _ehrLabResultCur.ObservationValueDateTime;
				case HL70125.NM:
					return _ehrLabResultCur.ObservationValueNumeric.ToString();
				case HL70125.FT:
				case HL70125.ST:
				case HL70125.TX:
					return _ehrLabResultCur.ObservationValueText;
				case HL70125.TM:
					return _ehrLabResultCur.ObservationValueTime.ToShortTimeString();
			}
			return "";//Unknown value type.
		}

		private void butAddAbnormalFlag_Click(object sender,EventArgs e) {

		}

		private void gridAbnormalFlags_MouseDoubleClick(object sender,MouseEventArgs e) {

		}

		private void butAddNote_Click(object sender,EventArgs e) {

		}

		private void gridNotes_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {

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
