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
		private List<SheetDef> AvailExamDefs;
		public string ExamFieldSelected;

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
			//Add exam sheet fields to the list
			List<SheetFieldDef> availFields=SheetFieldDefs.GetForExamSheet(AvailExamDefs[listExamSheets.SelectedIndex].SheetDefNum);
			for(int i=0;i<availFields.Count;i++) {
				if(availFields[i].FieldName=="") {
					continue;
				}
				if(availFields[i].FieldName!="misc") {//This is an internally defined field
					listAvailFields.Items.Add(availFields[i].FieldName);
					continue;
				}
				//misc:
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

		private void listAvailFields_DoubleClick(object sender,EventArgs e) {
			if(listAvailFields.SelectedIndex==-1) {
				return;
			}
			ExamFieldSelected=SheetTypeEnum.ExamSheet.ToString()+":"+AvailExamDefs[listExamSheets.SelectedIndex].Description+";"
				+listAvailFields.SelectedItem.ToString();//either RadioButtonGroup or ReportableName or internally defined field name
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listAvailFields.SelectedIndex==-1) {//if there is no selected reportable field
				MsgBox.Show(this,"You must select a field first.");
				return;
			}
			//example:  ExamSheet:NewPatient;Race
			ExamFieldSelected="ExamSheet:"+AvailExamDefs[listExamSheets.SelectedIndex].Description+";"
				+listAvailFields.SelectedItem.ToString();//either RadioButtonGroup or ReportableName or internally defined field name
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}