using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CodeBase;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDatabaseMaintTemp:Form {
		public FormDatabaseMaintTemp() {
			InitializeComponent();
			Lan.F(this);

		}

		private void FormDatabaseMaintTemp_Load(object sender,EventArgs e) {
			FillDatabaseNames();
		}

		private void FillDatabaseNames(){
			comboDbs.Items.Clear();
			List<string> dbNames=DatabaseMaintenance.GetDatabaseNames();
			for(int i=0;i<dbNames.Count;i++){
				comboDbs.Items.Add(dbNames[i]);
			}
			//automatic selection will come later.

		}

		private void butRun_Click(object sender,EventArgs e) {
			if(comboDbs.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a backup database first.");
				return;
			}
			//make sure it's not this database

			Cursor=Cursors.WaitCursor;
			textResults.Text=DatabaseMaintenance.GetDuplicateClaimProcs();
			textResults.Text+=DatabaseMaintenance.GetMissingClaimProcs(comboDbs.SelectedItem.ToString());
			Cursor=Cursors.Default;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butPrint_Click(object sender,EventArgs e) {
			string fileName=CodeBase.ODFileUtils.CombinePaths(Path.GetTempPath(),"results.txt");
			File.WriteAllText(fileName,textResults.Text);
			Process.Start(fileName);
			MsgBox.Show(this,"Please print from the text editor.");
		}

		private void linkLabel1_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e) {
			Process.Start("http://www.opendental.com/manual/bugcp.html");
		}

		
	}
}