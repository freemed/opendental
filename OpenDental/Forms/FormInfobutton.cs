using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormInfobutton:Form {
		public Patient PatCur;
		public Disease ProblemCur;//should this be named disease or problem? Also snomed/medication
		public Medication MedicationCur;
		public LabResult LabCur;

		public FormInfobutton() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormInfobutton_Load(object sender,EventArgs e) {
			fillComboBox();
			fillContext();
			//Fill context with provider and/or patient information.
			if(ProblemCur!=null) {
				groupBoxProblem.Show();
				fillProblem();
				comboRequestType.SelectedIndex=0;
			}
			else if(MedicationCur!=null) {
				groupBoxMedication.Show();
				fillMedication();
				comboRequestType.SelectedIndex=1;
			}
			else if(LabCur!=null) {
				groupBoxLab.Show();
				fillLabResult();
				comboRequestType.SelectedIndex=2;
			}
			else {
				//dislpay nothing until user selects a request type??
				//or should we get generic information?
			}
			displayGroupBoxesHelper();
		}

		private void fillComboBox() {
			comboRequestType.Items.Add(Lan.g(this,"Problem"));
			comboRequestType.Items.Add(Lan.g(this,"Medication"));
			comboRequestType.Items.Add(Lan.g(this,"Lab Result"));
		}

		private void displayGroupBoxesHelper() {
			groupBoxProblem.Hide();
			groupBoxMedication.Hide();
			groupBoxLab.Hide();
			switch(comboRequestType.SelectedIndex) {
				case 0://Problem
					groupBoxProblem.Show();
					break;
				case 1://Medication
					groupBoxMedication.Show();
					break;
				case 2://Lab Result
					groupBoxLab.Show();
					break;
				default:
					//should never happen.
					break;
			}
		}

		private void fillContext() {
		}

		private void fillProblem() {
			//ProblemCur.DiseaseDefNum
		}

		private void fillMedication() {
			textMedName.Text=MedicationCur.MedName;
			//throw new NotImplementedException();
		}

		private void fillLabResult() {
			//throw new NotImplementedException();
		}

		private void comboRequestType_SelectedIndexChanged(object sender,EventArgs e) {
			displayGroupBoxesHelper();
		}

		private void butPreview_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(generateKnowledgeRequestNotification());
		}

		///<summary></summary>
		private string generateKnowledgeRequestNotification() {//This is also known as the HL7 CDS message type.
			StringBuilder strb = new StringBuilder();
			XmlWriter xmlw = XmlWriter.Create(strb);
			//xmlw.
			return "";
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}