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

		public FormSheetPicker() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetPicker_Load(object sender,EventArgs e) {
			listSheets=SheetDefs.GetCustomForType(SheetType);
			if(listSheets.Count==0 && SheetType==SheetTypeEnum.PatientForm) {
				showingInternalSheetDefs=true;
				listSheets.Add(SheetsInternal.GetSheetDef(SheetInternalType.PatientRegistration));
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
			if(!showingInternalSheetDefs) {
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
			if(!showingInternalSheetDefs) {
				SheetDefs.GetFieldsAndParameters(SelectedSheetDef);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}