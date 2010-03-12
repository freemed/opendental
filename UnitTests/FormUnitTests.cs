using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;

namespace UnitTests {
	public partial class FormUnitTests:Form {
		public FormUnitTests() {
			InitializeComponent();
		}

		private void FormUnitTests_Load(object sender,EventArgs e) {
			throw new Exception("");
			//DatabaseTools.SetDbConnection("unittest");
			//if(!DatabaseTools.DbExists()){
			//	MessageBox.Show("Database does not exist: "+DatabaseTools.dbName);
			//}
		}

		

		private void butNewDb_Click(object sender,EventArgs e) {
			textResults.Text="";
			Cursor=Cursors.WaitCursor;
			DatabaseTools.FreshFromDump();
			textResults.Text+="Fresh database loaded from sql dump.";
			Cursor=Cursors.Default;
		}

		private void butRun_Click(object sender,EventArgs e) {
			//SetDbConnection();
			/*
			BenefitComputeRenewDate();
			ToothFormatRanges();
			textResults.Text+="Done.";
			textResults.SelectionStart=textResults.Text.Length;
			*/
		}




		

	




	}
}