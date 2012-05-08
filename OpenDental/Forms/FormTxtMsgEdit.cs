using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormTxtMsgEdit:Form {
		public Patient PatCur;

		public FormTxtMsgEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTxtMsgEdit_Load(object sender,EventArgs e) {
			textWirelessPhone.Text=PatCur.WirelessPhone;
		}

		private void butOK_Click(object sender,EventArgs e) {
			Commlog commlog=new Commlog();
			commlog.CommDateTime=DateTime.Now;
			commlog.DateTStamp=DateTime.Now;
//Should we create this commlog type if it doesn't exist or get it from a preference if we want to allow it to be variable?
			commlog.CommType=DefC.GetByExactName(DefCat.CommLogTypes,"Text Message");//Returns 0 if category "Text Message" does not exist.
			commlog.Mode_=CommItemMode.TxtMsg;
			commlog.Note=textMessage.Text;
			commlog.PatNum=PatCur.PatNum;
			commlog.SentOrReceived=CommSentOrReceived.Sent;
			commlog.UserNum=Security.CurUser.UserNum;
			commlog.DateTimeEnd=DateTime.Now;
			Commlogs.Insert(commlog);
			SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,commlog.PatNum,"Insert Text Message");
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}