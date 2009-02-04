using System;
using System.Collections.Generic;
using System.Data;
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
			Patient guar=fam.List[0];
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
			writer.WriteElementString("EstInsPayments",guar.InsEst.ToString("F2"));//optional
			double patientPortion=guar.BalTotal-guar.InsEst;
			writer.WriteElementString("PatientShare",patientPortion.ToString("F2"));
			writer.WriteElementString("CurrentBalance",guar.Bal_0_30.ToString("F2"));
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
				writer.WriteString(stmt.NoteBold);
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
			string note;
			string descript;
			string fulldesc;
			string procCode;
			string tth;
			string linedesc;
			string[] lines;
			DateTime date;
			for(int i=0;i<tableAccount.Rows.Count;i++) {
				writer.WriteStartElement("DetailItem");//has a child item and a child note
				writer.WriteAttributeString("sequence",i.ToString());
				writer.WriteStartElement("Item");
				date=(DateTime)tableAccount.Rows[i]["DateTime"];
				writer.WriteElementString("Date",date.ToString("MM/dd/yyyy"));
				writer.WriteElementString("PatientName",tableAccount.Rows[i]["patient"].ToString());
				procCode=tableAccount.Rows[i]["ProcCode"].ToString();
				tth=tableAccount.Rows[i]["tth"].ToString();
				descript=tableAccount.Rows[i]["description"].ToString();
				fulldesc=procCode+" "+tth+" "+descript;
				lines=fulldesc.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
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
				writer.WriteElementString("Description",linedesc);
				writer.WriteElementString("Charges",tableAccount.Rows[i]["charges"].ToString());
				writer.WriteElementString("Credits",tableAccount.Rows[i]["credits"].ToString());
				writer.WriteElementString("Balance",tableAccount.Rows[i]["balance"].ToString());
				writer.WriteEndElement();//Item
				if(note!="") {
					writer.WriteStartElement("Note");
					//we're not going to specify colors here since they're optional
					writer.WriteCData(note);
					writer.WriteEndElement();//Note
				}
				writer.WriteEndElement();//DetailItem
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
			string serverName="https://prelive.dentalxchange.com/dci/upload.svl";
				//"https://claimconnect.dentalxchange.com/dci/upload.svl";
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
			//Debug.Write(postData);
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
