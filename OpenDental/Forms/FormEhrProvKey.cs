using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrProvKey:Form {
		public Provider ProvCur;

		public FormEhrProvKey() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrProvKey_Load(object sender,EventArgs e) {
			textLName.Text=ProvCur.LName;
			textFName.Text=ProvCur.FName;
			checkHasReportAccess.Checked=ProvCur.EhrHasReportAccess;
			textEhrKey.Text=ProvCur.EhrKey;
		}

		private void butOK_Click(object sender,EventArgs e) {
			bool provKeyIsValid=false;
			#if EHRTEST
				provKeyIsValid=((EHR.FormEHR)FormOpenDental.FormEHR).ProvKeyIsValid(ProvCur.LName,ProvCur.FName,checkHasReportAccess.Checked,textEhrKey.Text);
			#else
				Type type=FormOpenDental.AssemblyEHR.GetType("EHR.FormEHR");//namespace.class
				object[] args=new object[] { ProvCur.LName,ProvCur.FName,checkHasReportAccess.Checked,textEhrKey.Text };
				provKeyIsValid=(bool)type.InvokeMember("ProvKeyIsValid",System.Reflection.BindingFlags.InvokeMethod,null,FormOpenDental.FormEHR,args);
			#endif
			if(!provKeyIsValid) {
				MsgBox.Show(this,"Invalid provider key.  Check capitalization, exact spelling, and report access status.");
				return;
			}
			ProvCur.EhrHasReportAccess=checkHasReportAccess.Checked;
			ProvCur.EhrKey=textEhrKey.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}