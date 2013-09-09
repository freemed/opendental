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
		private RxAlert RxAlertCur;
		private RxDef RxDefCur;

		public FormRxAlertEdit(RxAlert rxAlertCur,RxDef rxDefCur) {
			InitializeComponent();
			Lan.F(this);
			RxAlertCur=rxAlertCur;
			RxDefCur=rxDefCur;
		}

		private void FormRxAlertEdit_Load(object sender,EventArgs e) {
			textRxName.Text=RxDefCur.Drug;
			textName.Text=AllergyDefs.GetOne(RxAlertCur.AllergyDefNum).Description;
			textMessage.Text=RxAlertCur.NotificationMsg;
		}

		private void butOK_Click(object sender,EventArgs e) {
			RxAlertCur.NotificationMsg=PIn.String(textMessage.Text);
			RxAlerts.Update(RxAlertCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			RxAlerts.Delete(RxAlertCur);
			DialogResult=DialogResult.OK;
		}
	}
}