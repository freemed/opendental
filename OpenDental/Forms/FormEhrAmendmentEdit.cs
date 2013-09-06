using OpenDentBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormEhrAmendmentEdit:Form {
		public bool IsNew;
		public EhrAmendment AmdCur;

		public FormEhrAmendmentEdit() {
			InitializeComponent();
		}

		private void FormEhrAmendmentEdit_Load(object sender,EventArgs e) {
			string[] sourceList = Enum.GetNames(typeof(EhrAmendment.AmendmentSource));
			for(int i=0;i<sourceList.Length;i++) {
				comboSource.Items.Add(sourceList[i]);
			}
			if(!IsNew) {
				if(AmdCur.IsAccepted) {
					radioIsAccepted.Checked=true;
				}
				else {
					radioIsDenied.Checked=true; ;
				}
				comboSource.SelectedValue=AmdCur.Source;
				textDescription.Text=AmdCur.Description;
				if(AmdCur.FileName!="") {
					textAmdIsScanned.Text="Yes";
				}
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//whether new or not
			//if(MessageBox.Show("Delete Amendment?")) {
			//	return;
			//}
			try {
				EhrAmendments.Delete(AmdCur.EhrAmendmentNum);
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butView_Click(object sender,EventArgs e) {
			FormImages formI=new FormImages();
			//formI.ClaimPaymentNum=ClaimPaymentCur.ClaimPaymentNum;
			formI.ShowDialog();
			//if(EobAttaches.Exists(ClaimPaymentCur.ClaimPaymentNum)) {
			//	textEobIsScanned.Text=Lan.g(this,"Yes");
			//}
			//else {
			//	textEobIsScanned.Text=Lan.g(this,"No");
			//}
			//FillClaimPayment();//For customer 5769, who was getting ocassional Chinese chars in the Amount boxes.
			//FillGrids();//ditto
		}

		private void butOK_Click(object sender,EventArgs e) {
			try {
				EhrAmendments.Insert(AmdCur);
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}
	}
}
