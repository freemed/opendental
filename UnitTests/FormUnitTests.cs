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

		private void butWebService_Click(object sender,EventArgs e) {
			RemotingClient.ServerURI="http://localhost:49262/ServiceMain.asmx";
			Cursor=Cursors.WaitCursor;
			try{
				Userod user=Security.LogInWeb("Admin","","",Application.ProductVersion);//Userods.EncryptPassword("pass",false)
				Security.CurUser=user;
				RemotingClient.RemotingRole=RemotingRole.ClientWeb;
			}
			catch(Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			textResults.Text="";
			Application.DoEvents();
			textResults.Text+=WebServiceT.RunAll();
			Cursor=Cursors.Default;
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
			Application.DoEvents();
			textResults.Text+=AllTests.TestSix(specificTest);
			Application.DoEvents();
			textResults.Text+=AllTests.TestSeven(specificTest);
			Application.DoEvents();
			textResults.Text+=AllTests.TestEight(specificTest);
			Application.DoEvents();
			textResults.Text+=AllTests.TestNine(specificTest);
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