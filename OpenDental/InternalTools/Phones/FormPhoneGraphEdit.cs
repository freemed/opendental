using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPhoneGraphEdit:Form {
		private long EmployeeNum;
		public PhoneGraph PhoneGraphCur;

		public FormPhoneGraphEdit(long employeeNum) {
			InitializeComponent();
			Lan.F(this);
			EmployeeNum=employeeNum;
		}
		
		private void FormPhoneGraphCreate_Load(object sender,System.EventArgs e) {
			if(PhoneGraphCur.IsNew) {
				return;
			}
			textDateEntry.Text=PhoneGraphCur.DateEntry.ToShortDateString();
			checkIsGraphed.Checked=PhoneGraphCur.IsGraphed;
		}

		private void butToday_Click(object sender,System.EventArgs e) {
			textDateEntry.Text=DateTime.Today.ToShortDateString();
		}

		private void butDelete_Click(object sender,System.EventArgs e) {
			if(PhoneGraphCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			PhoneGraphs.Delete(PhoneGraphCur.PhoneGraphNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,System.EventArgs e) {
			if(textDateEntry.Text=="" || textDateEntry.errorProvider1.GetError(textDateEntry)!="") {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			//user brought in an existing entry and may have modified it so get rid of it and recreate it in its entirety
			//EG... 
			//user brought in 9/27/13 Checked: TRUE... 
			//then changed date to 9/28/13 Checked: FALSE... 
			//user has expectation that the 9/27 entry will be gone and new 9/28 entry will be created
			if(!PhoneGraphCur.IsNew) {
				PhoneGraphs.Delete(PhoneGraphCur.PhoneGraphNum);
			}
			PhoneGraph pg=new PhoneGraph();
			pg.EmployeeNum=EmployeeNum;
			pg.DateEntry=PIn.Date(textDateEntry.Text);
			pg.IsGraphed=checkIsGraphed.Checked;
			PhoneGraphs.InsertOrUpdate(pg);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			this.DialogResult=DialogResult.Cancel;
		}
	}
}
