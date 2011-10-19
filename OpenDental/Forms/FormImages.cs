using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormImages:Form {
		///<summary>Right now, this form only supports claimpayment mode.   Others will be added later.</summary>
		public long ClaimPaymentNum;

		public FormImages() {
			InitializeComponent();
			Lan.F(this);
			contrImagesMain.CloseClick+=new EventHandler(contrImagesMain_CloseClick);
		}

		private void FormImages_Load(object sender,EventArgs e) {
			contrImagesMain.InitializeOnStartup();
			contrImagesMain.ModuleSelectedClaimPayment(ClaimPaymentNum);
		}

		void contrImagesMain_CloseClick(object sender,EventArgs e) {
			Close();
		}
	}
}