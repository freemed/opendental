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
	public partial class FormSheetFieldExam:Form {
		///<summary>This is the object we are editing.</summary>
		public SheetFieldDef SheetFieldDefCur;
		///<summary>We need access to a few other fields of the sheetDef.</summary>
		public SheetDef SheetDefCur;
		private List<SheetDef> AvailExamDefs;
		public string ExamField;

		public FormSheetFieldExam() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetFieldDefEdit_Load(object sender,EventArgs e) {
			AvailExamDefs=SheetDefs.GetCustomForType(SheetTypeEnum.ExamSheet);
			if(AvailExamDefs==null) {
				MsgBox.Show(this,"No custom Exam Sheets are defined.");
				return;
			}
			listExamSheets.Items.Clear();
			for(int i=0;i<AvailExamDefs.Count;i++) {
				listExamSheets.Items.Add(AvailExamDefs[i].Description);
			}
			listExamSheets.SetSelected(0,true);
			FillFieldList();
		}

		private void FillFieldList() {
			listAvailFields.Sorted=true;//will alphabetize, since we are adding either FieldName,ReportableName, or RadioButtonGroup depending on field type
			listAvailFields.Items.Clear();
			//Add internal exam sheet fields to the list
			List<SheetFieldDef> availFields=SheetFieldDefs.GetForExamSheet(AvailExamDefs[listExamSheets.SelectedIndex]);
			for(int i=0;i<availFields.Count;i++) {
				if(availFields[i].FieldName=="") {
					continue;
				}
				if(availFields[i].FieldName!="misc") {//This is an internally defined field
					listAvailFields.Items.Add(availFields[i].FieldName);
					continue;
				}
				if(availFields[i].RadioButtonGroup!="") {//Only gets set if field is a 'misc' check box and assigned to a group
					if(listAvailFields.Items.Contains(availFields[i].RadioButtonGroup)) {
						continue;
					}
					else {
						listAvailFields.Items.Add(availFields[i].RadioButtonGroup);
						continue;
					}
				}
				if(availFields[i].ReportableName!="") {//Not internal type or part of a RadioButtonGroup so just add the reportable name if available
					listAvailFields.Items.Add(availFields[i].ReportableName);
					continue;
				}
			}
		}

		private void listExamSheets_MouseClick(object sender,MouseEventArgs e) {
			FillFieldList();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listAvailFields.SelectedIndex==-1) {//if there is no selected reportable field
				MsgBox.Show(this,"You must select a field first.");
				return;
			}
			//example:  ExamSheet:NewPatient;Race
			ExamField=SheetTypeEnum.ExamSheet.ToString()+":"+AvailExamDefs[listExamSheets.SelectedIndex].Description+";"
				+listAvailFields.SelectedItem.ToString();//either RadioButtonGroup or ReportableName
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}