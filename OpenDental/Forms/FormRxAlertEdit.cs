using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormRxAlertEdit:Form {
		private RxAlert rxAlertCur;
		private string RxName;

		public FormRxAlertEdit(RxAlert RxAlertCur,string rxName) {
			InitializeComponent();
			Lan.F(this);
			rxAlertCur=RxAlertCur;
			RxName=rxName;
		}

		private void FormRxAlertEdit_Load(object sender,EventArgs e) {
			textRxName.Text=RxName;
			if(rxAlertCur.DiseaseDefNum>0) {
				labelName.Text="Problem";
				textName.Text=DiseaseDefs.GetName(rxAlertCur.DiseaseDefNum);
			}
			if(rxAlertCur.AllergyDefNum>0) {
				labelName.Text="Allergy";
				textName.Text=AllergyDefs.GetOne(rxAlertCur.AllergyDefNum).Description;
			}
			if(rxAlertCur.MedicationNum>0) {
				labelName.Text="Medication";
				Medications.Refresh();
				textName.Text=Medications.GetMedication(rxAlertCur.MedicationNum).MedName;
			}
			textMessage.Text=rxAlertCur.NotificationMsg;
		}

		private void butOK_Click(object sender,EventArgs e) {
			rxAlertCur.NotificationMsg=PIn.String(textMessage.Text);
			RxAlerts.Update(rxAlertCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			RxAlerts.Delete(rxAlertCur);
			DialogResult=DialogResult.OK;
		}
	}
}