using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormMobile:Form {
		public FormMobile() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormMobile_Load(object sender,EventArgs e) {
			textPath.Text=PrefC.GetString("MobileSyncPath");
			textDateStart.Text=DateTime.Today.AddDays(-14).ToShortDateString();
			textDateEnd.Text=DateTime.Today.AddDays(14).ToShortDateString();
		}

		private void textPath_TextChanged(object sender,EventArgs e) {
			if(Directory.Exists(textPath.Text)){
				labelValid.Visible=false;
				butSync.Enabled=true;
			}
			else{
				labelValid.Visible=true;
				butSync.Enabled=false;
			}
		}

		private void butSync_Click(object sender,EventArgs e) {
			//no need to see if directory is valid because button will be disabled if not.
			string command=
				@"SELECT LName,FName,Birthdate,PatNum patNum
				FROM patient
				WHERE PatStatus=0/*patient*/
				OR PatStatus=2/*inactive*/
				OR PatStatus=1/*nonpatient*/
				ORDER BY LName,FName";
			DataTable table=General.GetTable(command);
			table=FormQuery.MakeReadable(table);

		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		private void FormMobile_FormClosing(object sender,FormClosingEventArgs e) {

		}

		

		

		
	}
}