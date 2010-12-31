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
		private bool isOracle;
		public FormUnitTests() {
			InitializeComponent();
		}

		private void FormUnitTests_Load(object sender,EventArgs e) {
			listType.Items.Add("MySql");
			listType.Items.Add("Oracle");
			listType.SelectedIndex=0;
			//throw new Exception("");
			//if(!DatabaseTools.SetDbConnection("unittest")){

			//}
			//if(!DatabaseTools.DbExists()){
			//	MessageBox.Show("Database does not exist: "+DatabaseTools.dbName);
			//}
		}

		private void listType_SelectedIndexChanged(object sender,EventArgs e) {
			if(listType.SelectedIndex==0) { //Only two selections, MySQL or Oracle.
				isOracle=false;
			}
			else {
				isOracle=true;
			}
		}

		private void butWebService_Click(object sender,EventArgs e) {
			RemotingClient.ServerURI="http://localhost:49262/ServiceMain.asmx";
			Cursor=Cursors.WaitCursor;
			try{
				if(!isOracle){
					Userod user=Security.LogInWeb("Admin","","",Application.ProductVersion,false);//Userods.EncryptPassword("pass",false)
					Security.CurUser=user;
					RemotingClient.RemotingRole=RemotingRole.ClientWeb;
				}
				else if(isOracle) {
					MsgBox.Show(this,"Oracle does not have unit test for web service yet.");
					Cursor=Cursors.Default;
					return;
				}
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

		private void butCore_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			textResults.Text="";
			Application.DoEvents();
			textResults.Text+=CoreTypesT.CreateTempTable(isOracle);
			Application.DoEvents();
			if(isOracle) {
				textResults.Text+=CoreTypesT.RunAllOracle();
			}
			else {
				textResults.Text+=CoreTypesT.RunAllMySql();
			}
			Cursor=Cursors.Default;
		}

		private void butNewDb_Click(object sender,EventArgs e) {
			textResults.Text="";
			Application.DoEvents();
			Cursor=Cursors.WaitCursor;
			if(!isOracle) {
				if(!DatabaseTools.SetDbConnection("",isOracle)) {
					MessageBox.Show("Could not connect");
					return;
				}
			}
			DatabaseTools.FreshFromDump(isOracle);
			textResults.Text+="Fresh database loaded from sql dump.";
			Cursor=Cursors.Default;
		}

		private void butRun_Click(object sender,EventArgs e) {
			textResults.Text="";
			Application.DoEvents();
			Cursor=Cursors.WaitCursor;
			if(!DatabaseTools.SetDbConnection("unittest",false)) {//if database doesn't exist
				DatabaseTools.SetDbConnection("",false);
				textResults.Text+=DatabaseTools.FreshFromDump(false);//this also sets database to be unittest.
			}
			else {
				textResults.Text+=DatabaseTools.ClearDb();
			}
			int specificTest=0;
			try {
				Application.DoEvents();
				specificTest=PIn.Int(textSpecificTest.Text);//typically zero
				textResults.Text+=AllTests.TestOneTwo(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="1&2: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestThree(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="3: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestFour(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="4: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestFive(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="5: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestSix(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="6: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestSeven(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="7: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestEight(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="8: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestNine(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="9: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestTen(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="10: Failed. "+ex.Message;
			}
			try {
				Application.DoEvents();
				textResults.Text+=AllTests.TestEleven(specificTest);
			}
			catch(Exception ex) {
				textResults.Text+="11: Failed. "+ex.Message;
			}
			textResults.Text+="Done";
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