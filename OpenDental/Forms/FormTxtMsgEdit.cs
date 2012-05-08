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
			string key=ProgramProperties.GetPropVal(ProgramName.CallFire,"Key From CallFire");
			string msg=textWirelessPhone.Text+","+textMessage.Text.Replace(",","");//ph#,msg Commas in msg cause error.
			try {
				CallFireService.SMSService callFire=new CallFireService.SMSService();
				callFire.sendSMSCampaign(
					key,
					new string[] {msg},
					"Open Dental");
			}
			catch(Exception ex) {
				MsgBox.Show(this,"Error sending text message.\r\n\r\n"+ex.Message);
			}
			Commlog commlog=new Commlog();
			commlog.CommDateTime=DateTime.Now;
			commlog.DateTStamp=DateTime.Now;
			commlog.CommType=DefC.Short[(int)DefCat.CommLogTypes][0].DefNum;//The first one in the list.  We can enhance later.
			commlog.Mode_=CommItemMode.TxtMsg;
			commlog.Note=msg;//phone,note
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