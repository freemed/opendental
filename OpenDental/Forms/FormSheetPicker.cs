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
		public List<SheetDef> SelectedSheetDefs;
		//private bool showingInternalSheetDefs;
		//private bool showingInternalMed;
		///<summary>Stores the indices of the sheetDefs already added to SelectedSheetDefs.  Prevents adding the same one twice.  Only used with terminal.</summary>
		private List<int> alreadyAdded;
		///<summary>On closing, this will be true if the ToTerminal button was used and if the selected sheets should be sent to a terminal.</summary>
		public bool TerminalSend;

		public FormSheetPicker() {
			InitializeComponent();
			Lan.F(this);
			alreadyAdded=new List<int>();
		}

		private void FormSheetPicker_Load(object sender,EventArgs e) {
			listSheets=SheetDefs.GetCustomForType(SheetType);
			if(listSheets.Count==0 && SheetType==SheetTypeEnum.PatientForm) {
				//showingInternalSheetDefs=true;
				listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration));
				listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.FinancialAgreement));
				listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.HIPAA));
			}
			if(SheetType==SheetTypeEnum.PatientForm) {//we will also show medical history
				List<SheetDef> listMedSheets=SheetDefs.GetCustomForType(SheetTypeEnum.MedicalHistory);
				if(listMedSheets.Count==0) {
					//showingInternalMed=true;
					listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.MedicalHistory));
				}
				else {//if user has added any of their own medical history forms
					for(int i=0;i<listMedSheets.Count;i++) {
						listSheets.Add(listMedSheets[i]);
					}
				}
				labelSheetType.Text=Lan.g("this","Patient Forms and Medical Histories");
			}
			else {
				labelSheetType.Text=Lan.g("enumSheetTypeEnum",SheetType.ToString());
				butTerminal.Visible=false;
				labelTerminal.Visible=false;
			}
			for(int i=0;i<listSheets.Count;i++){
				listMain.Items.Add(listSheets[i].Description);
			}
		}

		private void listMain_DoubleClick(object sender,EventArgs e) {
			if(listMain.SelectedIndices.Count!=1) {
				return;
			}
			SelectedSheetDefs=new List<SheetDef>();
			SheetDef sheetDef=listSheets[listMain.SelectedIndices[0]];
			if(sheetDef.SheetDefNum!=0) {
				SheetDefs.GetFieldsAndParameters(sheetDef);
			}
			/*
			if(sheetDef.SheetType==SheetTypeEnum.PatientForm && !showingInternalSheetDefs) {
				SheetDefs.GetFieldsAndParameters(sheetDef);
			}
			if(sheetDef.SheetType==SheetTypeEnum.MedicalHistory && !showingInternalMed) {
				SheetDefs.GetFieldsAndParameters(sheetDef);
			}*/
			SelectedSheetDefs.Add(sheetDef);
			TerminalSend=false;
			DialogResult=DialogResult.OK;
		}

		private void butTerminal_Click(object sender,EventArgs e) {
			//only visible when used from patient forms window.
			if(listMain.SelectedIndices.Count==0) {
				MsgBox.Show(this,"Please select at least one item first.");
				return;
			}
			if(SelectedSheetDefs==null) {
				SelectedSheetDefs=new List<SheetDef>();
			}
			SheetDef sheetDef;
			for(int i=0;i<listMain.SelectedIndices.Count;i++) {
				//test to make sure this sheetDef was not already added
				if(alreadyAdded.Contains(listMain.SelectedIndices[i])){
					continue;
				}
				alreadyAdded.Add(listMain.SelectedIndices[i]);
				sheetDef=listSheets[listMain.SelectedIndices[i]];
				if(sheetDef.SheetDefNum!=0) {
					SheetDefs.GetFieldsAndParameters(sheetDef);
				}
				/*
				if(sheetDef.SheetType==SheetTypeEnum.PatientForm && !showingInternalSheetDefs) {
					SheetDefs.GetFieldsAndParameters(sheetDef);
				}
				if(sheetDef.SheetType==SheetTypeEnum.MedicalHistory && !showingInternalMed) {
					SheetDefs.GetFieldsAndParameters(sheetDef);
				}*/
				SelectedSheetDefs.Add(sheetDef);
			}
			TerminalSend=true;
			if(listMain.SelectedIndices.Count>1) {
				DialogResult=DialogResult.OK;
				return;
			}
			//otherwise, leave window open so more forms can be sent
			for(int i=0;i<listMain.SelectedIndices.Count;i++) {
				listMain.SetSelected(listMain.SelectedIndices[i],false);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listMain.SelectedIndices.Count!=1){
				MsgBox.Show(this,"Please select one item first.");
				return;
			}
			SelectedSheetDefs=new List<SheetDef>();
			SheetDef sheetDef=listSheets[listMain.SelectedIndices[0]];
			if(sheetDef.SheetDefNum!=0) {
				SheetDefs.GetFieldsAndParameters(sheetDef);
			}
			/*
			if(sheetDef.SheetType==SheetTypeEnum.PatientForm && !showingInternalSheetDefs) {
				SheetDefs.GetFieldsAndParameters(sheetDef);
			}
			if(sheetDef.SheetType==SheetTypeEnum.MedicalHistory && !showingInternalMed) {
				SheetDefs.GetFieldsAndParameters(sheetDef);
			}*/
			SelectedSheetDefs.Add(sheetDef);
			TerminalSend=false;
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender,EventArgs e) {
			if(SelectedSheetDefs==null || SelectedSheetDefs.Count==0) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			//TerminalSend will have already been set true in this case.
			DialogResult=DialogResult.OK;
		}

		

		

		


	}
}