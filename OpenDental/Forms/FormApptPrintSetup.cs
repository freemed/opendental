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
		public DateTime ApptPrintStartTime;
		public DateTime ApptPrintStopTime;
		public int ApptPrintFontSize;
		public int ApptPrintColsPerPage;

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
			SaveChanges(false);
		}

		private bool ValidEntries() {
			DateTime start=PIn.DateT(textStartTime.Text);
			DateTime stop=PIn.DateT(textStopTime.Text);
			if(start.Minute>0 || stop.Minute>0) {
				MsgBox.Show(this,"Please use hours only, no minutes.");
				return false;
			}
			if(stop.Hour==start.Hour) {//If stop time is the same as start time.
				MsgBox.Show(this,"Start time must be different than stop time.");
				return false;
			}
			if(stop.Hour!=0 && stop.Hour<start.Hour) {//If stop time is earlier than start time.
				MsgBox.Show(this,"Start time cannot exceed stop time.");
				return false;
			}
			if(start==DateTime.MinValue) {
				MsgBox.Show(this,"Please enter a valid start time.");
				return false;
			}
			if(stop==DateTime.MinValue) {
				MsgBox.Show(this,"Please enter a valid stop time.");
				return false;
			}
			if(textColumnsPerPage.errorProvider1.GetError(textColumnsPerPage)!=""
				|| textFontSize.errorProvider1.GetError(textFontSize)!="") 
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			if(PIn.Int(textColumnsPerPage.Text)<1) {
				MsgBox.Show(this,"Columns per page cannot be 0 or less.");
				return false;
			}
			return true;
		}

		private void SaveChanges(bool suppressMessage) {
			if(ValidEntries()) {
				Prefs.UpdateDateT(PrefName.ApptPrintTimeStart,PIn.DateT(textStartTime.Text));
				Prefs.UpdateDateT(PrefName.ApptPrintTimeStop,PIn.DateT(textStopTime.Text));
				Prefs.UpdateString(PrefName.ApptPrintFontSize,textFontSize.Text);
				Prefs.UpdateInt(PrefName.ApptPrintColumnsPerPage,PIn.Int(textColumnsPerPage.Text));
				if(!suppressMessage) {
					MsgBox.Show(this,"Settings saved.");
				}
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			bool changed=false;
			if(!ValidEntries()) {
				return;
			}
			if(PIn.DateT(textStartTime.Text).Hour!=PrefC.GetDateT(PrefName.ApptPrintTimeStart).Hour
				|| PIn.DateT(textStopTime.Text).Hour!=PrefC.GetDateT(PrefName.ApptPrintTimeStop).Hour
				|| textFontSize.Text!=PrefC.GetString(PrefName.ApptPrintFontSize)
				|| textColumnsPerPage.Text!=PrefC.GetInt(PrefName.ApptPrintColumnsPerPage).ToString())
			{
				changed=true;
			}
			if(changed) {
				if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Save the changes that were made?")) {
					SaveChanges(true);
				}
			}
			ApptPrintStartTime=PIn.DateT(textStartTime.Text);
			ApptPrintStopTime=PIn.DateT(textStopTime.Text);
			ApptPrintFontSize=PIn.Int(textFontSize.Text);
			ApptPrintColsPerPage=PIn.Int(textColumnsPerPage.Text);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}