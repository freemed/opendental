using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormInsPayFix:Form {
		public FormInsPayFix() {
			InitializeComponent();
			Lan.F(this);

		}

		private void butRun_Click(object sender,EventArgs e) {
			List<ClaimPaySplit> splits=Claims.GetInsPayNotAttached();
			if(splits.Count==0) {
				MsgBox.Show(this,"There are currently no insurance payments that are not attached to an insurance check.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			for(int i=0;i<splits.Count;i++) {
				Claim claim=Claims.GetClaim(splits[i].ClaimNum);
				ClaimPayment cp=new ClaimPayment();
				cp.CheckDate=claim.DateService;
				cp.CheckAmt=splits[i].InsPayAmt;
				cp.ClinicNum=claim.ClinicNum;
				cp.CarrierName=splits[i].Carrier;
				ClaimPayments.Insert(cp);
				List<ClaimProc> claimP=ClaimProcs.RefreshForClaim(splits[i].ClaimNum);
				for(int j=0;j<claimP.Count;j++) {
					claimP[j].DateCP=claim.DateService;
					claimP[j].ClaimPaymentNum=cp.ClaimPaymentNum;
					ClaimProcs.Update(claimP[j]);
				}
			}
			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Insurance checks created: ")+splits.Count);
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}