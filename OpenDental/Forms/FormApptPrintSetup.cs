using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormApptPrintSetup:Form {
		public FormApptPrintSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormApptPrintSetup_Load(object sender,EventArgs e) {
			textStartTime.Text=PrefC.GetDateT(PrefName.ApptPrintTimeStart).ToShortTimeString();
			textStopTime.Text=PrefC.GetDateT(PrefName.ApptPrintTimeStop).ToShortTimeString();
			textFontSize.Text=PrefC.GetString(PrefName.ApptPrintFontSize);
			textColumnsPerPage.Text=PrefC.GetInt(PrefName.ApptPrintColumnsPerPage).ToString();
		}

		private void butSave_Click(object sender,EventArgs e) {
			if(!ValidEntries()) {
				return;
			}
			SaveChanges();
		}

		private bool ValidEntries() {
			string timePattern="^([1-9]|1[0-2]|0[1-9]){1}(:[0-5][0-9]\\s[aApP][mM]){1}$";
			if(!Regex.IsMatch(textStartTime.Text,timePattern)) {
				MsgBox.Show(this,"Start time invalid. Example of correct format - 5:00 AM");
				return false;
			}
			if(!Regex.IsMatch(textStopTime.Text,timePattern)) {
				MsgBox.Show(this,"Stop time invalid. Example of correct format - 5:00 PM");
				return false;
			}
			if(PIn.DateT(textStartTime.Text)>PIn.DateT(textStopTime.Text)) {
				MsgBox.Show(this,"Start time cannot excede stop time.");
				return false;
			}
			if(textColumnsPerPage.errorProvider1.GetError(textColumnsPerPage)!=""
				|| textFontSize.errorProvider1.GetError(textFontSize)!="") 
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			if(textColumnsPerPage.Text=="0") {
				MsgBox.Show(this,"Columns per page cannot be 0.");
				return false;
			}
			return true;
		}

		private void SaveChanges() {
			Prefs.UpdateString(PrefName.ApptPrintTimeStart,textStartTime.Text);
			Prefs.UpdateString(PrefName.ApptPrintTimeStop,textStopTime.Text);
			Prefs.UpdateString(PrefName.ApptPrintFontSize,textFontSize.Text);
			Prefs.UpdateInt(PrefName.ApptPrintColumnsPerPage,PIn.Int(textColumnsPerPage.Text));
		}

		private void butOK_Click(object sender,EventArgs e) {
			bool changed=false;
			if(!ValidEntries()) {
				return;
			}
			if(textStartTime.Text!=PrefC.GetDateT(PrefName.ApptPrintTimeStart).ToShortTimeString()
				|| textStopTime.Text!=PrefC.GetDateT(PrefName.ApptPrintTimeStop).ToShortTimeString()
				|| textFontSize.Text!=PrefC.GetString(PrefName.ApptPrintFontSize)
				|| textColumnsPerPage.Text!=PrefC.GetInt(PrefName.ApptPrintColumnsPerPage).ToString())
			{
				changed=true;
			}
			if(changed) {
				if(MsgBox.Show(this,true,"Save the changes that were made?")) {
					SaveChanges();
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}