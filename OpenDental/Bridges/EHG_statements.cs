using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	public class EHG_statements {
		//these are temporary:
		//private static string vendorID="68";
		//private static string vendorPMScode="144";
		//private static string clientAccountNumber="8011";//the dental office number set by EHG
		//private static string creditCardChoices="MC,D,V,A";//MasterCard,Discover,Visa,AmericanExpress
		//private static string userName="";
		//private static string password="";

		///<summary>Generates all the xml up to the point where the first statement would go.</summary>
		public static void GeneratePracticeInfo(XmlWriter writer) {
			writer.WriteProcessingInstruction("xml","version = \"1.0\" standalone=\"yes\"");
			writer.WriteStartElement("EISStatementFile");
			writer.WriteAttributeString("VendorID",PrefC.GetString("BillingElectVendorId"));
			writer.WriteAttributeString("OutputFormat","StmOut_Blue6Col");
			writer.WriteAttributeString("Version","2");
			writer.WriteElementString("SubmitDate",DateTime.Today.ToString("yyyy-MM-dd"));
			writer.WriteElementString("PrimarySubmitter",PrefC.GetString("BillingElectVendorPMSCode"));
			writer.WriteElementString("Transmitter","EHG");
			writer.WriteStartElement("Practice");
			writer.WriteAttributeString("AccountNumber",PrefC.GetString("BillingElectClientAcctNumber"));
			//sender address----------------------------------------------------------
			writer.WriteStartElement("SenderAddress");
			writer.WriteElementString("Name",PrefC.GetString("PracticeTitle"));
			writer.WriteElementString("Address1",PrefC.GetString("PracticeAddress"));
			writer.WriteElementString("Address2",PrefC.GetString("PracticeAddress2"));
			writer.WriteElementString("City",PrefC.GetString("PracticeCity"));
			writer.WriteElementString("State",PrefC.GetString("PracticeST"));
			writer.WriteElementString("Zip",PrefC.GetString("PracticeZip"));
			writer.WriteElementString("Phone",PrefC.GetString("PracticePhone"));//enforced to be 10 digit fairly rigidly by the UI
			writer.WriteEndElement();//senderAddress
			//remit address----------------------------------------------------------
			writer.WriteStartElement("RemitAddress");
			writer.WriteElementString("Name",PrefC.GetString("PracticeTitle"));
			if(PrefC.GetString("PracticeBillingAddress")=="") {//same as sender address
				writer.WriteElementString("Address1",PrefC.GetString("PracticeAddress"));
				writer.WriteElementString("Address2",PrefC.GetString("PracticeAddress2"));
				writer.WriteElementString("City",PrefC.GetString("PracticeCity"));
				writer.WriteElementString("State",PrefC.GetString("PracticeST"));
				writer.WriteElementString("Zip",PrefC.GetString("PracticeZip"));
			}
			else {
				writer.WriteElementString("Address1",PrefC.GetString("PracticeBillingAddress"));
				writer.WriteElementString("Address2",PrefC.GetString("PracticeBillingAddress2"));
				writer.WriteElementString("City",PrefC.GetString("PracticeBillingCity"));
				writer.WriteElementString("State",PrefC.GetString("PracticeBillingST"));
				writer.WriteElementString("Zip",PrefC.GetString("PracticeBillingZip"));				
			}
			writer.WriteElementString("Phone",PrefC.GetString("PracticePhone"));//phone is same in either case
			writer.WriteEndElement();//remitAddress
			//Rendering provider------------------------------------------------------
			Provider prov=Providers.GetProv(PrefC.GetInt("PracticeDefaultProv"));
			writer.WriteStartElement("RenderingProvider");
			writer.WriteElementString("Name",prov.GetFormalName());
			writer.WriteElementString("LicenseNumber",prov.StateLicense);
			writer.WriteElementString("State",PrefC.GetString("PracticeST"));
			writer.WriteEndElement();//Rendering provider
		}

		///<summary>Adds the xml for one statement.</summary>
		public static void GenerateOneStatement(XmlWriter writer,Statement stmt,Patient pat,Family fam,DataSet dataSet){
			writer.WriteStartElement("EisStatement");
			writer.WriteAttributeString("OutputFormat","StmOut_Blue6Col");
			writer.WriteAttributeString("CreditCardChoice",PrefC.GetString("BillingElectCreditCardChoices"));
			writer.WriteStartElement("Patient");
			Patient guar=fam.ListPats[0];
			writer.WriteElementString("Name",guar.GetNameFLFormal());
			writer.WriteElementString("Account",guar.PatNum.ToString());
			writer.WriteElementString("Address1",guar.Address);
			writer.WriteElementString("Address2",guar.Address2);
			writer.WriteElementString("City",guar.City);
			writer.WriteElementString("State",guar.State);
			writer.WriteElementString("Zip",guar.Zip);
			writer.WriteElementString("EMail","");//leave this blank until we figure out how to set this trigger.
			//Account summary-----------------------------------------------------------------------
			writer.WriteStartElement("AccountSummary");
			writer.WriteElementString("PriorStatementDate",stmt.DateRangeFrom.AddDays(-1).ToString("MM/dd/yyyy"));
			DateTime dueDate;
			if(PrefC.GetInt("StatementsCalcDueDate")==-1){
				dueDate=DateTime.Today.AddDays(10);
			}
			else{
				dueDate=DateTime.Today.AddDays(PrefC.GetInt("StatementsCalcDueDate"));
			}
			writer.WriteElementString("DueDate",dueDate.ToString("MM/dd/yyyy"));
			writer.WriteElementString("StatementDate",stmt.DateSent.ToString("MM/dd/yyyy"));
			double balanceForward=0;
			for(int r=0;r<dataSet.Tables["misc"].Rows.Count;r++){
				if(dataSet.Tables["misc"].Rows[r]["descript"].ToString()=="balanceForward"){
					balanceForward=PIn.PDouble(dataSet.Tables["misc"].Rows[r]["value"].ToString());
				}
			}
			writer.WriteElementString("PriorBalance",balanceForward.ToString("F2"));
			writer.WriteElementString("RunningBalance","");//for future use
			writer.WriteElementString("PerPayAdj","");//optional
			writer.WriteElementString("InsPayAdj","");//optional
			writer.WriteElementString("Adjustments","");//for future use
			writer.WriteElementString("NewCharges","");//optional
			writer.WriteElementString("FinanceCharges","");//for future use
			DataTable tableAccount=null;
			for(int i=0;i<dataSet.Tables.Count;i++) {
				if(dataSet.Tables[i].TableName.StartsWith("account")) {
					tableAccount=dataSet.Tables[i];
				}
			}
			double credits=0;
			for(int i=0;i<tableAccount.Rows.Count;i++) {
				credits+=PIn.PDouble(tableAccount.Rows[i]["creditsDouble"].ToString());
			}
			writer.WriteElementString("Credits",credits.ToString("F2"));
			//on a regular printed statement, the amount due at the top might be different from the balance at the middle right.
			//This is because of payment plan balances.
			//But in e-bills, there is only one amount due.
			//Insurance estimate is already subtracted, and payment plan balance is already added.
			double amountDue=guar.BalTotal;
			//add payplan due amt:
			for(int m=0;m<dataSet.Tables["misc"].Rows.Count;m++) {
				if(dataSet.Tables["misc"].Rows[m]["descript"].ToString()=="payPlanDue") {
					amountDue+=PIn.PDouble(dataSet.Tables["misc"].Rows[m]["value"].ToString());
				}
			}
			if(PrefC.GetBool("BalancesDontSubtractIns")) {
				writer.WriteElementString("EstInsPayments","");//optional.
				writer.WriteElementString("PatientShare",amountDue.ToString("F2"));
				//this is ambiguous.  It seems to be AmountDue, but it could possibly be 0-30 days aging
				writer.WriteElementString("CurrentBalance",amountDue.ToString("F2"));
			}
			else {//this is typical
				writer.WriteElementString("EstInsPayments",guar.InsEst.ToString("F2"));//optional.
				amountDue-=guar.InsEst;
				writer.WriteElementString("PatientShare",amountDue.ToString("F2"));
				writer.WriteElementString("CurrentBalance",amountDue.ToString("F2"));
			}
			writer.WriteElementString("PastDue30",guar.Bal_31_60.ToString("F2"));//optional
			writer.WriteElementString("PastDue60",guar.Bal_61_90.ToString("F2"));//optional
			writer.WriteElementString("PastDue90",guar.BalOver90.ToString("F2"));//optional
			writer.WriteElementString("PastDue120","");//optional
			writer.WriteEndElement();//AccountSummary
			//Notes-----------------------------------------------------------------------------------
			writer.WriteStartElement("Notes");
			if(stmt.NoteBold!="") {
				writer.WriteStartElement("Note");
				writer.WriteAttributeString("FgColor",ColorToHexString(Color.DarkRed));
				writer.WriteAttributeString("BgColor",ColorToHexString(Color.White));
				writer.WriteCData(stmt.NoteBold);
				writer.WriteEndElement();//Note
			}
			if(stmt.Note!="") {
				writer.WriteStartElement("Note");
				writer.WriteAttributeString("FgColor",ColorToHexString(Color.Black));
				writer.WriteAttributeString("BgColor",ColorToHexString(Color.White));
				writer.WriteCData(stmt.Note);
				writer.WriteEndElement();//Note
			}
			writer.WriteEndElement();//Notes
			//Detail items------------------------------------------------------------------------------
			writer.WriteStartElement("DetailItems");
			//string note;
			string descript;
			string fulldesc;
			string procCode;
			string tth;
			//string linedesc;
			string[] lineArray;
			List<string> lines;
			DateTime date;
			int seq=0;
			for(int i=0;i<tableAccount.Rows.Count;i++) {
				procCode=tableAccount.Rows[i]["ProcCode"].ToString();
				tth=tableAccount.Rows[i]["tth"].ToString();
				descript=tableAccount.Rows[i]["description"].ToString();
				fulldesc=procCode+" "+tth+" "+descript;
				lineArray=fulldesc.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
				lines=new List<string>(lineArray);
				//The specs say that the line limit is 30 char.  But in testing, it will take 50 char.
				//We will use 40 char to be safe.
				if(lines[0].Length>40) {
					string newline=lines[0].Substring(40);
					lines[0]=lines[0].Substring(0,40);//first half
					lines.Insert(1,newline);//second half
				}
				for(int li=0;li<lines.Count;li++) {
					writer.WriteStartElement("DetailItem");//has a child item. We won't add optional child note
					writer.WriteAttributeString("sequence",seq.ToString());
					writer.WriteStartElement("Item");
					if(li==0) {
						date=(DateTime)tableAccount.Rows[i]["DateTime"];
						writer.WriteElementString("Date",date.ToString("MM/dd/yyyy"));
						writer.WriteElementString("PatientName",tableAccount.Rows[i]["patient"].ToString());
					}
					else {
						writer.WriteElementString("Date","");
						writer.WriteElementString("PatientName","");
					}
					writer.WriteElementString("Description",lines[li]);
					if(li==0) {
						writer.WriteElementString("Charges",tableAccount.Rows[i]["charges"].ToString());
						writer.WriteElementString("Credits",tableAccount.Rows[i]["credits"].ToString());
						writer.WriteElementString("Balance",tableAccount.Rows[i]["balance"].ToString());
					}
					else {
						writer.WriteElementString("Charges","");
						writer.WriteElementString("Credits","");
						writer.WriteElementString("Balance","");
					}
					writer.WriteEndElement();//Item
					writer.WriteEndElement();//DetailItem
					seq++;
				}
				/*The code below just didn't work because notes don't get displayed on the statement.
				linedesc=lines[0];
				note="";
				if(linedesc.Length>30) {
					note=linedesc.Substring(30);
					linedesc=linedesc.Substring(0,30);
				}
				for(int l=1;l<lines.Length;l++) {
					if(note!="") {
						note+="\r\n";
					}
					note+=lines[l];
				}
				
				if(note!="") {
					writer.WriteStartElement("Note");
					//we're not going to specify colors here since they're optional
					writer.WriteCData(note);
					writer.WriteEndElement();//Note
				}*/
			}
			writer.WriteEndElement();//DetailItems
			writer.WriteEndElement();//Patient
			writer.WriteEndElement();//EisStatement
		}

		///<summary>Converts a .net color to a hex string.  Includes the #.</summary>
		private static string ColorToHexString(Color color) {
			char[] hexDigits={'0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'};
			byte[] bytes = new byte[3];
			bytes[0] = color.R;
			bytes[1] = color.G;
			bytes[2] = color.B;
			char[] chars=new char[bytes.Length * 2];
			for(int i=0;i<bytes.Length;i++){
				int b=bytes[i];
				chars[i*2]=hexDigits[b >> 4];
				chars[i*2+1]=hexDigits[b & 0xF];
			}
			string retVal=new string(chars);
			retVal="#"+retVal;
			return retVal;
		}

		///<summary>After statements are added, this adds the necessary closing xml elements.</summary>
		public static void GenerateWrapUp(XmlWriter writer) {
			writer.WriteEndElement();//Practice
			writer.WriteEndElement();//EISStatementFile
		}

		///<summary>Surround with try catch.  The "data" is the previously constructed xml.</summary>
		public static void Send(string data) {
			//Step 1: Post authentication request:
			Version myVersion=new Version(Application.ProductVersion);
			HttpWebRequest webReq;
			WebResponse response;
			StreamReader readStream;
			string str;
			string[] responseParams;
			string status="";
			string group="";
			string userid="";
			string authid="";
			string errormsg="";
			string alertmsg="";
			string curParam="";
			string serverName=//"https://prelive.dentalxchange.com/dci/upload.svl";
				"https://claimconnect.dentalxchange.com/dci/upload.svl";
			webReq=(HttpWebRequest)WebRequest.Create(serverName);
			string postData=
				"Function=Auth"//CONSTANT; signifies that this is an authentication request
				+"&Source=STM"//CONSTANT; file format
				+"&UploaderName=OpenDental"//CONSTANT
				+"&UploaderVersion="+myVersion.Major.ToString()+"."+myVersion.Minor.ToString()//eg 3.4
				+"&Username="+PrefC.GetString("BillingElectUserName")
				+"&Password="+PrefC.GetString("BillingElectPassword");
			webReq.KeepAlive=false;
			webReq.Method="POST";
			webReq.ContentType="application/x-www-form-urlencoded";
			webReq.ContentLength=postData.Length;
			ASCIIEncoding encoding=new ASCIIEncoding();
			byte[] bytes=encoding.GetBytes(postData);
			Stream streamOut=webReq.GetRequestStream();
			streamOut.Write(bytes,0,bytes.Length);
			streamOut.Close();
			response=webReq.GetResponse();
			//Process the authentication response:
			readStream=new StreamReader(response.GetResponseStream(),Encoding.ASCII);
			str=readStream.ReadToEnd();
			readStream.Close();
			//Debug.WriteLine(str);
			//MessageBox.Show(str);
			responseParams=str.Split('&');
			for(int i=0;i<responseParams.Length;i++) {
				curParam=GetParam(responseParams[i]);
				switch(curParam) {
					case "Status":
						status=GetParamValue(responseParams[i]);
						break;
					case "GROUP":
						group=GetParamValue(responseParams[i]);
						break;
					case "UserID":
						userid=GetParamValue(responseParams[i]);
						break;
					case "AuthenticationID":
						authid=GetParamValue(responseParams[i]);
						break;
					case "ErrorMessage":
						errormsg=GetParamValue(responseParams[i]);
						break;
					case "AlertMessage":
						alertmsg=GetParamValue(responseParams[i]);
						break;
					default:
						throw new Exception("Unexpected parameter: "+curParam);
				}
			}
			//Process response for errors:
			if(alertmsg!="") {
				MessageBox.Show(alertmsg);
			}
			switch(status) {
				case "0":
					//MessageBox.Show("Authentication successful.");
					break;
				case "1":
					throw new Exception("Authentication failed. "+errormsg);
				case "2":
					throw new Exception("Cannot authenticate at this time. "+errormsg);
				case "3":
					throw new Exception("Invalid authentication request. "+errormsg);
				case "4":
					throw new Exception("Invalid program version. "+errormsg);
				case "5":
					throw new Exception("No customer contract. "+errormsg);
			}
			//Step 2: Post upload request:
			//string fileName=Directory.GetFiles(clearhouse.ExportPath)[0];
			string boundary="------------7d13e425b00d0";
			postData=
				"--"+boundary+"\r\n"
				+"Content-Disposition: form-data; name=\"Function\"\r\n"
				+"\r\n"
				+"Upload\r\n"
				+"--"+boundary+"\r\n"
				+"Content-Disposition: form-data; name=\"Source\"\r\n"
				+"\r\n"
				+"STM\r\n"
				+"--"+boundary+"\r\n"
				+"Content-Disposition: form-data; name=\"AuthenticationID\"\r\n"
				+"\r\n"
				+authid+"\r\n"
				+"--"+boundary+"\r\n"
				+"Content-Disposition: form-data; name=\"File\"; filename=\""+"stmt.xml"+"\"\r\n"
				+"Content-Type: text/plain\r\n"
				+"\r\n"
			//using(StreamReader sr=new StreamReader(fileName)) {
			//	postData+=sr.ReadToEnd()+"\r\n"
				+data+"\r\n"
				+"--"+boundary+"--";
			//}
			//Debug.WriteLine(postData);
			//MessageBox.Show(postData);
			webReq=(HttpWebRequest)WebRequest.Create(serverName);
			webReq.KeepAlive=false;
			webReq.Method="POST";
			webReq.ContentType="multipart/form-data; boundary="+boundary;
			webReq.ContentLength=postData.Length;
			bytes=encoding.GetBytes(postData);
			streamOut=webReq.GetRequestStream();
			streamOut.Write(bytes,0,bytes.Length);
			streamOut.Close();
			response=webReq.GetResponse();
			//Process the response
			readStream=new StreamReader(response.GetResponseStream(),Encoding.ASCII);
			str=readStream.ReadToEnd();
			readStream.Close();
			errormsg="";
			status="";
			str=str.Replace("\r\n","");
			//Debug.Write(str);
			if(str.Length>300) {
				throw new Exception("Unknown lengthy error message received.");
			}
			responseParams=str.Split('&');
			for(int i=0;i<responseParams.Length;i++){
				curParam=GetParam(responseParams[i]);
				switch(curParam){
					case "Status":
						status=GetParamValue(responseParams[i]);
						break;
					case "Error Message":
					case "ErrorMessage":
						errormsg=GetParamValue(responseParams[i]);
						break;
					case "Filename":
					case "Timestamp":
						break;
					case ""://errorMessage blank
						break;
					default:
						throw new Exception(str);//"Unexpected parameter: "+str);//curParam+"*");
				}
			}
			switch(status){
				case "0":
					//MessageBox.Show("Upload successful.");
					break;
				case "1":
					throw new Exception("Authentication failed. "+errormsg);
				case "2":
					throw new Exception("Cannot upload at this time. "+errormsg);
			}
		}

		private static string GetParam(string paramAndValue) {
			if(paramAndValue=="") {
				return "";
			}
			string[] pair=paramAndValue.Split('=');
			//if(pair.Length!=2){
			//	throw new Exception("Unexpected parameter from server: "+paramAndValue);
			return pair[0];
		}

		private static string GetParamValue(string paramAndValue) {
			if(paramAndValue=="") {
				return "";
			}
			string[] pair=paramAndValue.Split('=');
			//if(pair.Length!=2){
			//	throw new Exception("Unexpected parameter from server: "+paramAndValue);
			//}
			if(pair.Length==1) {
				return "";
			}
			return pair[1];
		}


	}
}
