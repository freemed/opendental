using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
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
			//throw new Exception("");
			//if(!DatabaseTools.SetDbConnection("unittest")){

			//}
			//if(!DatabaseTools.DbExists()){
			//	MessageBox.Show("Database does not exist: "+DatabaseTools.dbName);
			//}
			

		}

		private void butNewDb_Click(object sender,EventArgs e) {
			textResults.Text="";
			Application.DoEvents();
			Cursor=Cursors.WaitCursor;
			if(!DatabaseTools.SetDbConnection("")){
				MessageBox.Show("Could not connect");
				return;
			}
			DatabaseTools.FreshFromDump();
			textResults.Text+="Fresh database loaded from sql dump.";
			Cursor=Cursors.Default;
		}

		private void butRun_Click(object sender,EventArgs e) {
			textResults.Text="";
			Application.DoEvents();
			Cursor=Cursors.WaitCursor;
			if(!DatabaseTools.SetDbConnection("unittest")) {//if database doesn't exist
				DatabaseTools.SetDbConnection("");
				textResults.Text+=DatabaseTools.FreshFromDump();//this also sets database to be unittest.
			}
			else {
				textResults.Text+=DatabaseTools.ClearDb();
			}
			Application.DoEvents();
			int specificTest=PIn.Int(textSpecificTest.Text);//typically zero
			textResults.Text+=AllTests.TestOneTwo(specificTest);
			Application.DoEvents();
			textResults.Text+=AllTests.TestThree(specificTest);
			Application.DoEvents();
			textResults.Text+=AllTests.TestFour(specificTest);
			Application.DoEvents();
			textResults.Text+=AllTests.TestFive(specificTest);
			Cursor=Cursors.Default;



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