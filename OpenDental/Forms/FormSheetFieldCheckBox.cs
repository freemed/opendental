using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSheetFieldCheckBox:Form {
		///<summary>This is the object we are editing.</summary>
		public SheetFieldDef SheetFieldDefCur;
		///<summary>We need access to a few other fields of the sheetDef.</summary>
		public SheetDef SheetDefCur;
		private List<SheetFieldDef> AvailFields;
		public bool IsReadOnly;
		private List<string> radioButtonValues;
		private List<AllergyDef> allergyList;
		private List<Medication> medicationList;
		///<summary>True if the sheet type is MedicalHistory.</summary>
		private bool isMedHistSheet;
		public bool IsNew;

		public FormSheetFieldCheckBox() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetFieldCheckBox_Load(object sender,EventArgs e) {
			if(IsReadOnly){
				butOK.Enabled=false;
				butDelete.Enabled=false;
			}
			//not allowed to change sheettype or fieldtype once created.  So get all avail fields for this sheettype
			AvailFields=SheetFieldsAvailable.GetList(SheetDefCur.SheetType,OutInCheck.Check);
			isMedHistSheet=SheetDefCur.SheetType==SheetTypeEnum.MedicalHistory;
			listFields.Items.Clear();
			for(int i=0;i<AvailFields.Count;i++) {
				//static text is not one of the options.
				listFields.Items.Add(AvailFields[i].FieldName);
				//Sheets will have dynamic field names like "medication:Sudafed".  They will always start with a valid FieldName.
				if(SheetFieldDefCur.FieldName.StartsWith(AvailFields[i].FieldName)) {
					listFields.SelectedIndex=i;
				}
			}
			if(isMedHistSheet) {
				radioYes.Checked=true;
				if(SheetFieldDefCur.FieldName.StartsWith("allergy:")) {
					FillListMedical(MedicalListType.allergy);
					if(SheetFieldDefCur.RadioButtonValue.StartsWith("N:")){
						radioNo.Checked=true;
						radioYes.Checked=false;
					}
					SetListMedicalSelectedIndex(MedicalListType.allergy,SheetFieldDefCur.FieldName.Remove(0,8));
				}
				else if(SheetFieldDefCur.FieldName.StartsWith("medication:")) {
					FillListMedical(MedicalListType.medication);
					if(SheetFieldDefCur.RadioButtonValue.StartsWith("N:")) {
						radioNo.Checked=true;
						radioYes.Checked=false;
					}
					SetListMedicalSelectedIndex(MedicalListType.medication,SheetFieldDefCur.FieldName.Remove(0,11));
				}
				else if(SheetFieldDefCur.FieldName.StartsWith("problem:")) {
					FillListMedical(MedicalListType.problem);
					if(SheetFieldDefCur.RadioButtonValue.StartsWith("N:")) {
						radioNo.Checked=true;
						radioYes.Checked=false;
					}
					SetListMedicalSelectedIndex(MedicalListType.problem,SheetFieldDefCur.FieldName.Remove(0,8));
				}
			}
			textXPos.Text=SheetFieldDefCur.XPos.ToString();
			textYPos.Text=SheetFieldDefCur.YPos.ToString();
			textWidth.Text=SheetFieldDefCur.Width.ToString();
			textHeight.Text=SheetFieldDefCur.Height.ToString();
			textRadioGroupName.Text=SheetFieldDefCur.RadioButtonGroup;
			checkRequired.Checked=SheetFieldDefCur.IsRequired;
			textTabOrder.Text=SheetFieldDefCur.TabOrder.ToString();
		}

		///<summary>Fills listMedical with the corresponding list type.  This saves on load time by only filling necessary lists.</summary>
		private void FillListMedical(MedicalListType medListType) {
			switch(medListType) {
				case MedicalListType.allergy:
					if(allergyList==null) {
						allergyList=AllergyDefs.GetAll(false);
					}
					listMedical.Items.Clear();
					for(int i=0;i<allergyList.Count;i++) {
						listMedical.Items.Add(allergyList[i].Description);
					}
					break;
				case MedicalListType.medication:
					if(medicationList==null) {
						medicationList=Medications.GetList("");
					}
					listMedical.Items.Clear();
					for(int i=0;i<medicationList.Count;i++) {
						listMedical.Items.Add(Medications.GetDescription(medicationList[i].MedicationNum));
					}
					break;
				case MedicalListType.problem:
					listMedical.Items.Clear();
					for(int i=0;i<DiseaseDefs.List.Length;i++) {
						listMedical.Items.Add(DiseaseDefs.List[i].DiseaseName);
					}
					break;
			}
		}

		///<summary>Loops through corresponding list and sets the index to the item matching fieldName passed in.  Only called on load.</summary>
		private void SetListMedicalSelectedIndex(MedicalListType medListType,string fieldName) {
			switch(medListType) {
				case MedicalListType.allergy:
					for(int i=0;i<allergyList.Count;i++) {
						if(AllergyDefs.GetDescription(allergyList[i].AllergyDefNum)==fieldName) {
							listMedical.SelectedIndex=i;
						}
					}
					break;
				case MedicalListType.medication:
					for(int i=0;i<medicationList.Count;i++) {
						if(Medications.GetDescription(medicationList[i].MedicationNum)==fieldName) {
							listMedical.SelectedIndex=i;
						}
					}
					break;
				case MedicalListType.problem:
					for(int i=0;i<DiseaseDefs.List.Length;i++) {
						if(DiseaseDefs.List[i].DiseaseName==fieldName) {
							listMedical.SelectedIndex=i;
						}
					}
					break;
			}
		}

		private void listFields_SelectedIndexChanged(object sender,EventArgs e) {
			groupRadio.Visible=false;
			groupRadioMisc.Visible=false;
			checkRequired.Visible=false;
			labelMedical.Visible=false;
			listMedical.Visible=false;
			radioYes.Visible=false;
			radioNo.Visible=false;
			if(listFields.SelectedIndex==-1) {
				return;
			}
			if(isMedHistSheet) {
				switch(AvailFields[listFields.SelectedIndex].FieldName) {
					case "allergy":
						radioYes.Visible=true;
						radioNo.Visible=true;
						labelMedical.Visible=true;
						listMedical.Visible=true;
						labelMedical.Text="Allergies";
						FillListMedical(MedicalListType.allergy);
						break;
					case "medication":
						radioYes.Visible=true;
						radioNo.Visible=true;
						labelMedical.Visible=true;
						listMedical.Visible=true;
						labelMedical.Text="Medications";
						FillListMedical(MedicalListType.medication);
						break;
					case "problem":
						radioYes.Visible=true;
						radioNo.Visible=true;
						labelMedical.Visible=true;
						listMedical.Visible=true;
						labelMedical.Text="Problems";
						FillListMedical(MedicalListType.problem);
						break;
				}
			}
			if(AvailFields[listFields.SelectedIndex].FieldName=="misc") {
				groupRadioMisc.Visible=true;
				checkRequired.Visible=true;
			}
			else {
				radioButtonValues=SheetFieldsAvailable.GetRadio(AvailFields[listFields.SelectedIndex].FieldName);
				if(radioButtonValues.Count==0) {
					return;
				}
				groupRadio.Visible=true;
				checkRequired.Visible=true;
				listRadio.Items.Clear();
				for(int i=0;i<radioButtonValues.Count;i++) {
					listRadio.Items.Add(radioButtonValues[i]);
					if(SheetFieldDefCur.RadioButtonValue==radioButtonValues[i]) {
						listRadio.SelectedIndex=i;
					}
				}
			}
		}

		private void listRadio_Click(object sender,EventArgs e) {
			if(listRadio.SelectedIndex==-1){
				return;
			}
			SheetFieldDefCur.RadioButtonValue=radioButtonValues[listRadio.SelectedIndex];
		}

		private void listFields_DoubleClick(object sender,EventArgs e) {
			SaveAndClose();
		}

		private void listMedical_DoubleClick(object sender,EventArgs e) {
			SaveAndClose();
		}

		private void radioYes_Click(object sender,EventArgs e) {
			if(radioYes.Checked) {
				radioNo.Checked=false;
			}
			else {
				radioNo.Checked=true;
			}
		}

		private void radioNo_Click(object sender,EventArgs e) {
			if(radioNo.Checked) {
				radioYes.Checked=false;
			}
			else {
				radioYes.Checked=true;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			SheetFieldDefCur=null;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			SaveAndClose();
		}

		private void SaveAndClose(){
			if(textXPos.errorProvider1.GetError(textXPos)!=""
				|| textYPos.errorProvider1.GetError(textYPos)!=""
				|| textWidth.errorProvider1.GetError(textWidth)!=""
				|| textHeight.errorProvider1.GetError(textHeight)!=""
				|| textTabOrder.errorProvider1.GetError(textTabOrder)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(listFields.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a field name first.");
				return;
			}
			string fieldName=AvailFields[listFields.SelectedIndex].FieldName;
			string radioButtonValue="";
			#region Medical History Sheet
			if(isMedHistSheet && listMedical.Visible) {
				if(listMedical.SelectedIndex==-1) {
					switch(fieldName) {
						case "allergy":
							MsgBox.Show(this,"Please select an allergy first.");
							return;
						case "medication":
							MsgBox.Show(this,"Please select a medication first.");
							return;
						case "problem":
							MsgBox.Show(this,"Please select a problem first.");
							return;
					}
				}
				//Loop through to make sure they don't already have this exact check box.
				for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++) {
					if(!IsNew && SheetDefCur.SheetFieldDefs[i].SheetFieldDefNum==SheetFieldDefCur.SheetFieldDefNum) {
						continue;
					}
					if(radioNo.Checked && SheetDefCur.SheetFieldDefs[i].RadioButtonValue=="N:"+listMedical.SelectedItem) {
						MsgBox.Show(this,"This exact field already exists on the current sheet.");
						return;
					}
					if(radioYes.Checked && SheetDefCur.SheetFieldDefs[i].RadioButtonValue=="Y:"+listMedical.SelectedItem) {
						MsgBox.Show(this,"This exact field already exists on the current sheet.");
						return;
					}
				}				
				fieldName+=":"+listMedical.SelectedItem;//Medical check boxes will always have a radio button value.
				if(radioNo.Checked) {
					radioButtonValue="N:"+listMedical.SelectedItem;
				}
				else {
					radioButtonValue="Y:"+listMedical.SelectedItem;
				}
			}
			#endregion
			SheetFieldDefCur.FieldName=fieldName;
			SheetFieldDefCur.XPos=PIn.Int(textXPos.Text);
			SheetFieldDefCur.YPos=PIn.Int(textYPos.Text);
			SheetFieldDefCur.Width=PIn.Int(textWidth.Text);
			SheetFieldDefCur.Height=PIn.Int(textHeight.Text);
			SheetFieldDefCur.RadioButtonGroup="";
			SheetFieldDefCur.RadioButtonValue=radioButtonValue;
			if(groupRadio.Visible && listRadio.SelectedIndex>=0) {
				SheetFieldDefCur.RadioButtonValue=radioButtonValues[listRadio.SelectedIndex];
			}
			else if(groupRadioMisc.Visible){
				SheetFieldDefCur.RadioButtonGroup=textRadioGroupName.Text;
			}
			SheetFieldDefCur.IsRequired=checkRequired.Checked;
			SheetFieldDefCur.TabOrder=PIn.Int(textTabOrder.Text);
			//don't save to database here.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}









		private enum MedicalListType {
			allergy,
			medication,
			problem
		}
	}
}