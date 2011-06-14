using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
			//Test the time text boxes here:
			if(textColumnsPerPage.errorProvider1.GetError(textColumnsPerPage)!=""
				|| textFontSize.errorProvider1.GetError(textFontSize)!="") 
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
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