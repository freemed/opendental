using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;

namespace OpenDental {
	public partial class FormEtrans835Edit:Form {

		public Etrans EtransCur;
		private string MessageText;

		public FormEtrans835Edit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEtrans277Edit_Load(object sender,EventArgs e) {
			MessageText=EtransMessageTexts.GetMessageText(EtransCur.EtransMessageTextNum);
			//837 CLM01 -> 835 CLP01 (even for split claims)
			//835 TRN = Reassociation Key Segmen. See TRN02 (pg. 19)
			//835 Table 2 PLB contains claim level adjustments.
			//835 Table 3 PLB contains the provider/check level adjustments.
			//SVC02-(CAS03+CAS06+CAS09+CAS12+CAS15+CAS18)=SVC03
			//When the service payment information loop is not present, then: CLP03-(CAS03+CAS06+CAS09+CAS12+CAS15+CAS18)=CLP04
			//Otherwise, CAS must also be considered from the service adjustment segment.
			//Reassociation (pg. 20): Use the trace # in TRN02 and the company ID number in TRN03 to uniquely identify the claim payment/data.
			//Institutional (pg. 23): CAS reason code 78 requires special handling.
			//Advance payments (pg. 23): in PLB segment with adjustment reason code PI. Can be yearly or monthly.
			//Bundled procs (pg. 27): have the original proc listed in SV06.
			//Line Item Control Number (pg. 28): REF*6B from the 837 is returned when procedures are unbundled.
			//Predetermination (pg. 28): claim adjustment reason code is 101. Identified by claim status code 25 in CLP02.

		}

		private void butRawMessage_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(MessageText);
			msgbox.ShowDialog();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}
		
	}
}