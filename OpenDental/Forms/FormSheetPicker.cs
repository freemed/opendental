using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSheetPicker:Form {
		public SheetTypeEnum SheetType;
		private List<SheetDef> listSheets;
		///<summary>Only if OK.</summary>
		public SheetDef SelectedSheetDef;
		private bool showingInternalSheetDefs;
		private bool showingInternalMed;

		public FormSheetPicker() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetPicker_Load(object sender,EventArgs e) {
			listSheets=SheetDefs.GetCustomForType(SheetType);
			if(listSheets.Count==0 && SheetType==SheetTypeEnum.PatientForm) {
				showingInternalSheetDefs=true;
				listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration));
				listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.FinancialAgreement));
				listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.HIPAA));
			}
			if(SheetType==SheetTypeEnum.PatientForm) {//we will also show medical history
				List<SheetDef> listMedSheets=SheetDefs.GetCustomForType(SheetTypeEnum.MedicalHistory);
				if(listMedSheets.Count==0) {
					showingInternalMed=true;
					listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.MedicalHistory));
				}
				else{//if user has added any of their own medical history forms
					for(int i=0;i<listMedSheets.Count;i++) {
						listSheets.Add(listMedSheets[i]);
					}
				}
			}
			labelSheetType.Text=Lan.g("enumSheetTypeEnum",SheetType.ToString());
			for(int i=0;i<listSheets.Count;i++){
				listMain.Items.Add(listSheets[i].Description);
			}
		}

		private void listMain_DoubleClick(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1){
				return;
			}
			SelectedSheetDef=listSheets[listMain.SelectedIndex];
			if(SelectedSheetDef.SheetType==SheetTypeEnum.PatientForm && !showingInternalSheetDefs) {
				SheetDefs.GetFieldsAndParameters(SelectedSheetDef);
			}
			if(SelectedSheetDef.SheetType==SheetTypeEnum.MedicalHistory && !showingInternalMed) {
				SheetDefs.GetFieldsAndParameters(SelectedSheetDef);
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedSheetDef=listSheets[listMain.SelectedIndex];
			if(SelectedSheetDef.SheetType==SheetTypeEnum.PatientForm && !showingInternalSheetDefs) {
				SheetDefs.GetFieldsAndParameters(SelectedSheetDef);
			}
			if(SelectedSheetDef.SheetType==SheetTypeEnum.MedicalHistory && !showingInternalMed) {
				SheetDefs.GetFieldsAndParameters(SelectedSheetDef);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}