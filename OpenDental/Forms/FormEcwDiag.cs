using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Security.Cryptography;

namespace OpenDental {
	public partial class FormEcwDiag:Form {
		private string connString;
		private string username="ecwUser";
		private string password="l69Rr4Rmj4CjiCTLxrIblg==";//encrypted
		private string server;
		private string port;
		private StringBuilder arbitraryStringName=new StringBuilder();

		public FormEcwDiag() {
			InitializeComponent();
			server=ProgramProperties.GetPropVal(Programs.GetProgramNum(ProgramName.eClinicalWorks),"eCWServer");
			port=ProgramProperties.GetPropVal(Programs.GetProgramNum(ProgramName.eClinicalWorks),"eCWPort");
			buildConnectionString();
			Lan.F(this);
		}

		private void FormEcwDiag_Load(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			Application.DoEvents();
			VerifyECW();
			Cursor=Cursors.Default;
			Application.DoEvents();
		}

		///<summary>Used to construct a default construction string.</summary>
		private void buildConnectionString() {
			connString=
				"Server="+server+";"
				+"Port="+port+";"//although this does seem to cause a bug in Mono.  We will revisit this bug if needed to exclude the port option only for Mono.
				+"Database=mobiledoc;"//ecwMaster;"
				//+"Connect Timeout=20;"
				+"User ID="+username+";"
				+"Password="+Decrypt(password)+";"
				+"CharSet=utf8;"
				+"Treat Tiny As Boolean=false;"
				+"Allow User Variables=true;"
				+"Default Command Timeout=300;"//default is 30seconds
				+"Pooling=false"
				;
		}

		private string Decrypt(string encString) {
			try {
				byte[] encrypted=Convert.FromBase64String(encString);
				MemoryStream ms=null;
				CryptoStream cs=null;
				StreamReader sr=null;
				Aes aes=new AesManaged();
				UTF8Encoding enc=new UTF8Encoding();
				aes.Key=enc.GetBytes("AKQjlLUjlcABVbqp");//Random string will be key
				aes.IV=new byte[16];
				ICryptoTransform decryptor=aes.CreateDecryptor(aes.Key,aes.IV);
				ms=new MemoryStream(encrypted);
				cs=new CryptoStream(ms,decryptor,CryptoStreamMode.Read);
				sr=new StreamReader(cs);
				string decrypted=sr.ReadToEnd();
				ms.Dispose();
				cs.Dispose();
				sr.Dispose();
				if(aes!=null) {
					aes.Clear();
				}
				return decrypted;
			}
			catch {
				MessageBox.Show("Text entered was not valid encrypted text.");
				return "";
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butRunCheck_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			Application.DoEvents();
			VerifyECW();
			Cursor=Cursors.Default;
			Application.DoEvents();
		}

		///<summary>Surround with wait cursor.</summary>
		private void VerifyECW() {
			//buildConnectionString();
			bool verbose=checkShow.Checked;
			StringBuilder strB=new StringBuilder();
			strB.Append('-',90);
			textLog.Text=DateTime.Now.ToString()+strB.ToString()+"\r\n";
			Application.DoEvents();
			//--------eCW Function Tests Below This Line-------
			try {
				MySql.Data.MySqlClient.MySqlHelper.ExecuteDataRow(connString,"SELECT VERSION();");//meaningless query to test connection.
			}
			catch(Exception ex) {
				textLog.Text+="Cannot detect eCW server named \""+server+"\".\r\n";
				Cursor=Cursors.Default;
				return;
			}
			HL7Verification(verbose);//composite check
			Application.DoEvents();
			textLog.Text+=checkDentalVisitTypes(verbose);
			Application.DoEvents();

			//textLog.Text+=appointmentTriggersForHl7(verbose);
			//Application.DoEvents();
			//textLog.Text+=Test1(verbose);
			//Application.DoEvents();
			textLog.Text+="Done.";
		}

		private string appointmentTriggersForHl7(bool verbose) {
			string retVal="";
			DataTable appTriggers = new DataTable();
			try {
				appTriggers=MySqlHelper.ExecuteDataset(connString,"SELECT * FROM pmitemkeys WHERE name LIKE '%Filter_for_%';").Tables[0];
			}
			catch(Exception ex) {
				return ex.Message+"\r\n";
			}
			foreach(DataRow trigger in appTriggers.Rows) {
				if(trigger["value"].ToString()!="no") {
					if(verbose) {
						retVal+=trigger["name"].ToString().Split('_')[3]+" messages are configured to be sent based on "+trigger["name"].ToString().Split('_')[0]+" filter.\r\n";
					}
					continue;
				}
				if(trigger["value"].ToString()=="no"&&verbose) {
					retVal+=trigger["name"].ToString().Split('_')[3]+" messages are sent for any "+trigger["name"].ToString().Split('_')[0]+".\r\n";
					continue;
				}
			}
			if(retVal!="") {
				string header="\r\n";
				header+="   HL7 Message Triggers\r\n";
				header+="".PadRight(90,'*')+"\r\n";
				retVal=header+retVal;
			}
			return retVal;
		}

		private void HL7Verification(bool verbose) {
			List<int> hl7InterfaceIDs=ColumnToListHelper(MySqlHelper.ExecuteDataset(connString,"SELECT DISTINCT InterfaceId FROM hl7segment_details;").Tables[0],"InterfaceId");
			List<int> interfaceErrorCount=new List<int>(hl7InterfaceIDs.Count);
			List<string> interfaceErrorLogs=new List<string>(hl7InterfaceIDs.Count);//Cache error logs for each interface until we determine which interface to report on.
			for(int ifaceIndex=0;ifaceIndex<hl7InterfaceIDs.Count;ifaceIndex++){//validate one interface at a time.
				int interfaceID=hl7InterfaceIDs[ifaceIndex];
				interfaceErrorLogs.Add("");//start each interface with a blank error log
				interfaceErrorCount.Add(0);//start each interface with 0 error count
				int errorsFromCurMessage=0;
				//Itterate through and validate all messages defined on this interface.
				List<int> hl7MessageIDs=ColumnToListHelper(MySqlHelper.ExecuteDataset(connString,"SELECT DISTINCT Messageid FROM hl7segment_details WHERE InterfaceID="+interfaceID+";").Tables[0],"Messageid");
				foreach(int messageID in hl7MessageIDs) {//2, in our sample
					string messageType=MySqlHelper.ExecuteDataRow(connString,"SELECT DISTINCT MessageType FROM hl7message_types WHERE MessageTypeId="+messageID)["MessageType"].ToString();
					//Validate each message individually if needed.
					errorsFromCurMessage=0;
					if(messageType.Contains("ADT")){
						interfaceErrorLogs[ifaceIndex]+=verifyAsADTMessage(interfaceID,messageID,out errorsFromCurMessage,verbose);
					}
					else if(messageType.Contains("SIU")){
						interfaceErrorLogs[ifaceIndex]+=verifyAsSIUMessage(interfaceID,messageID,out errorsFromCurMessage,verbose);
					}
				}
				interfaceErrorCount[ifaceIndex]+=errorsFromCurMessage;
			}//end foreach interface
			int leastErrorIndex=0;
			for(int i=0;i<hl7InterfaceIDs.Count;i++) {
				if(interfaceErrorCount[i]<interfaceErrorCount[leastErrorIndex]) {
					leastErrorIndex=i;
				}
			}
			if(interfaceErrorLogs[leastErrorIndex]!="" || verbose) {
				textLog.Text+="\r\n";
				textLog.Text+="   HL7 Messages\r\n";
				textLog.Text+="".PadRight(90,'*')+"\r\n";
			}
			if(verbose) {
				textLog.Text+="HL7 Interface "+hl7InterfaceIDs[leastErrorIndex]+" had "+interfaceErrorCount[leastErrorIndex]+" errors.\r\n";
			}
			textLog.Text+=interfaceErrorLogs[leastErrorIndex];
			Application.DoEvents();
		}

		private string verifyAsADTMessage(int interfaceID,int messageID,out int errors,bool verbose) {
			string retVal="";
			errors=0;
			bool validMessage=true;
			List<string> segmentsContained=new List<string>();
			DataTable hl7Segments=MySqlHelper.ExecuteDataset(connString,"SELECT SegmentData FROM hl7segment_details WHERE InterfaceID="+interfaceID+" AND Messageid="+messageID+";").Tables[0];
			//validate segments based on content
			foreach(DataRow segment in hl7Segments.Rows) {
				string[] segmentValues=segment["SegmentData"].ToString().Split('|');
				segmentsContained.Add(segmentValues[0]);//used later to validate existance of segments.
				switch(segmentValues[0]) {
					case "PID":
						if(segmentValues[2]!="{PID}") {
							retVal+="ADT HL7 message is not sending eCW's internal patient number in field PID.2\r\n";
							errors++;
							validMessage=false;
						}
						if(segmentValues[4]!="{CONTNO}") {
							retVal+="ADT HL7 message is not sending eCW's account number in field PID.4\r\n";
							errors++;
							validMessage=false;
						}
						continue;
					default:
						continue;
				}
			}
			//Validate existance of segments
			if(!segmentsContained.Contains("PID")) {
				retVal+="No PID segment found in ADT HL7 message.\r\n";
				errors+=3;//No segment +2 sub errors
				validMessage=false;
			}
			//If everything above checks out return a success message
			if(validMessage && verbose) {
				retVal+="Found properly formed ADT HL7 message definition.\r\n";
			}
			return retVal;
		}

		private string verifyAsSIUMessage(int interfaceID,int messageID,out int errors,bool verbose) {
			string retVal="";
			errors=0;
			bool validMessage=true;
			List<string> segmentsContained=new List<string>();
			DataTable hl7Segments=MySqlHelper.ExecuteDataset(connString,"SELECT SegmentData FROM hl7segment_details WHERE InterfaceID="+interfaceID+" AND Messageid="+messageID+";").Tables[0];
			//validate segments based on content
			foreach(DataRow segment in hl7Segments.Rows) {
				string[] segmentValues=segment["SegmentData"].ToString().Split('|');
				segmentsContained.Add(segmentValues[0]);//used later to validate existance of segments.
				switch(segmentValues[0]) {
					case "PID":
						if(segmentValues[2]!="{PID}") {
							retVal+="SIU HL7 message is not sending eCW's internal patient number in field PID.2\r\n";
							errors++;
							validMessage=false;
						}
						if(segmentValues[4]!="{CONTNO}") {
							retVal+="SIU HL7 message is not sending eCW's account number in field PID.4\r\n";
							errors++;
							validMessage=false;
						}
						continue;
					case "SCH":
						if(segmentValues[2]!="{ENCID}") {
							retVal+="SIU HL7 message is not sending appointment number in field SCH.2\r\n";
							errors++;
							validMessage=false;
						}
						if(segmentValues[7]!="{ENCREASON}") {
							retVal+="SIU HL7 message is not sending appointment notes in field SCH.7\r\n";
							errors++;
							validMessage=false;
						}
						string[] SCH11=segmentValues[11].Split('^');
						if(SCH11[3]!="{ENCSDATETIME}") {
							retVal+="SIU HL7 message is not sending appointment start time in field SCH.11.3\r\n";
							errors++;
							validMessage=false;
						}
						if(SCH11[4]!="{ENCEDATETIME}") {
							retVal+="SIU HL7 message is not sending appointment end time in field SCH.11.4\r\n";
							errors++;
							validMessage=false;
						}
						continue;
					default:
						continue;
				}
			}
			//Validate existance of segments
			if(!segmentsContained.Contains("PID")) {
				retVal+="No PID segment found in SIU HL7 message.\r\n";
				errors+=3;//no segment plus 2 sub errors.
				validMessage=false;
			}
			if(!segmentsContained.Contains("SCH")) {
				retVal+="No SCH segment found in SIU HL7 message.\r\n";
				errors+=5;//no segment plus the 4 sub errors.
				validMessage=false;
			}
			if(!segmentsContained.Contains("AIG") && !segmentsContained.Contains("PV1") && verbose) {
				retVal+="No AIG or PV1 segment found in SIU HL7 message. Appointments will use patient's default primary provider.\r\n";//ecwSIU.cs sets this when in-processing SIU message.
				validMessage=false;
			}
			//If everything above checks out return a success message
			if(validMessage && verbose) {
				retVal+="Found properly formed SIU HL7 message definition.\r\n";
			}
			return retVal;
		}

		/*private string verifyAsDFTMessage(int interfaceID,int messageID,bool verbose) {
			string retVal="";
			bool validMessage=true;
			List<string> segmentsContained=new List<string>();
			DataTable hl7Segments=MySqlHelper.ExecuteDataset(connString,"SELECT SegmentData FROM hl7segment_details WHERE InterfaceID="+interfaceID+" AND Messageid="+messageID+";").Tables[0];
			//validate segments based on content
			foreach(DataRow segment in hl7Segments.Rows) {
				string[] segmentValues=segment["SegmentData"].ToString().Split('|');
				segmentsContained.Add(segmentValues[0]);//used later to validate existance of segments.
				switch(segmentValues[0]) {
					case "PID":
						if(segmentValues[2]!="{CONTNO}") {
							retVal+="DFT HL7 message is not sending eCW's account number in field PID.2\r\n";
							validMessage=false;
						}
						if(segmentValues[3]!="{PID}") {
							retVal+="DFT HL7 message is not sending eCW's account number in field PID.4\r\n";
							validMessage=false;
						}
						continue;
					case "PV1":
						//TODO: Need example of a valid DFT message to validate this segment.
						continue;
					case "ZX1":
						//TODO: Need example of a valid DFT message to validate this segment.
						continue;
					default:
						continue;
				}
			}
			//Validate existance of segments
			if(!segmentsContained.Contains("PID")) {
				retVal+="No PID segment found in DFT HL7 message.\r\n";
				validMessage=false;
			}
			if(!segmentsContained.Contains("PV1")) {
				retVal+="No PV1 segment found in DFT HL7 message.\r\n";
				validMessage=false;
			}
			if(!segmentsContained.Contains("FT1")) {
				retVal+="No FT1 segment found in DFT HL7 message.\r\n";
				validMessage=false;
			}
			if(!segmentsContained.Contains("ZX1")) {
				retVal+="No ZX1 segment found in DFT HL7 message.\r\n";
				validMessage=false;
			}
			//If everything above checks out return a success message
			if(validMessage && verbose) {
				retVal+="Found properly formed DFT HL7 message definition.\r\n";
			}
			return retVal;
		}*/

		private string checkDentalVisitTypes(bool verbose) {
			string retval = ""; 
			DataTable queryResult = new DataTable();
			try {
				queryResult=MySqlHelper.ExecuteDataset(connString,"SELECT * FROM pmcodes WHERE (ecwcode LIKE '%dental%' OR externalCode LIKE '%dental%') AND pmid=1 AND flag='VS';").Tables[0];
			}
			catch(Exception ex) {
				return ex.Message+"\r\n";
			}
			if(queryResult.Rows.Count==0 || verbose) {
				retval+="Number of dental visit codes found : "+queryResult.Rows.Count+".\r\n";
			}
			return retval;
		}

		private List<int> ColumnToListHelper(DataTable dataTable,string colName) {
			List<int> retVal = new List<int>();
			foreach(DataRow dRow in dataTable.Rows) {
				retVal.Add((int)dRow[colName]);
			}
			return retVal;
		}

		private string TestTemplate(bool verbose) {
			StringBuilder retVal=new StringBuilder();
			bool failed=true;
			string command="SHOW TABLES;";
			DataTable qResult=MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connString,command).Tables[0];
			MySql.Data.MySqlClient.MySqlDataReader mtDataReader;
			//Place check code here. Also, use a reader, table or both as shown above.

			if(verbose||failed) {
				retVal.Clear();
				retVal.Append("HL7 message definitions are not formed properly. //TODO: maybe add some more specific details here.");
			}
			return retVal.ToString();
		}

		private void checkShow_KeyPress(object sender,KeyPressEventArgs e) {
			KeysConverter kc=new KeysConverter();
			try {
				arbitraryStringName.Append(e.KeyChar);
			}
			catch(Exception ex) {
				//fail VERY silently. Mwa Ha Ha.
			}
			if(arbitraryStringName[arbitraryStringName.Length-1]=='X') {//Clear string if 'X' is pressed.
				arbitraryStringName.Clear();
			}
			if(arbitraryStringName.ToString()=="open" || arbitraryStringName.ToString()=="There is no cow level") {
				FormEcwDiagAdv FormECWA=new FormEcwDiagAdv();
				FormECWA.ShowDialog();
			}

		}

		//private string Test1(bool verbose) {
		//  StringBuilder retVal=new StringBuilder();
		//  bool failed=true;
		//  MySql.Data.MySqlClient.MySqlDataReader myDrTables;
		//  MySql.Data.MySqlClient.MySqlDataReader myDrFields;
		//  try {
		//    myDrTables=MySql.Data.MySqlClient.MySqlHelper.ExecuteReader(connString,"SHOW TABLES"+(textTables.Text==""?"":" LIKE '%"+POut.String(textTables.Text)+"%'"));
		//    if(myDrTables.HasRows) {
		//      failed=false;
		//    }
		//    while(myDrTables.Read()) {
		//      if(myDrTables.GetValue(0).ToString().Contains("copy")//copy is a reserved word
		//        ) {
		//        continue;
		//      }
		//      try {
		//        if(textValue.Text!="" && MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connString,queryTableByValue(myDrTables.GetValue(0).ToString())).Tables[0].Rows.Count==0) {
		//          continue;//skip tables with no rows that match the match by value.
		//        }
		//      retVal.Append("**********************************************************\r\n"
		//                   +"**             "+myDrTables.GetValue(0).ToString()+" Rows:"+MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connString,"SELECT COUNT(*) AS numRows FROM "+myDrTables.GetValue(0).ToString()).Tables[0].Rows[0]["numRows"].ToString()+"\r\n"
		//                   +"**********************************************************\r\n");
		//      myDrFields=MySql.Data.MySqlClient.MySqlHelper.ExecuteReader(connString,queryTableByValue(myDrTables.GetValue(0).ToString()));//"SELECT * FROM `"+POut.String(myDrTables.GetValue(0).ToString())+"` LIMIT 100");
		//        int row=0;
		//        while(myDrFields.Read()) {
		//          retVal.Append("Row "+row+":  ");
		//          row++;
		//          for(int i=0;i<myDrFields.FieldCount;i++) {
		//            retVal.Append(myDrFields.GetValue(i).ToString()+"  ||  ");
		//          }
		//          retVal.Append("\r\n");
		//        }
		//      }
		//      catch(Exception ex2) {
		//        retVal.Append("Error accesing table:"+myDrTables.GetValue(0).ToString()+"\r\nError Message:"+ex2.Message+"\r\n");
		//      }
		//    }
		//  }
		//  catch(Exception ex) {
		//  #if DEBUG
		//    retVal.Append(ex.Message+"\r\n");
		//    return retVal.ToString();
		//  #endif
		//  }
		//  //  if(myDrTables.HasRows) {
		//  //    failed=false;
		//  //  }
		//  //  while(myDrTables.Read()){
		//  //    for(int i=0;i<myDrTables.FieldCount;i++){
		//  //      retVal+=myDrTables.GetValue(i).ToString()+"  :  ";
		//  //    }
		//  //    retVal+="\r\n";
		//  //  }
		//  //  //DataTable myDT=MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connString,"SELECT * FROM userod").Tables[0];
		//  //  //retVal+="UserOD Table Contents (abridged):\r\n";
		//  //  //for(int i=0;i<myDT.Rows.Count;i++) {
		//  //  //  retVal+="Username :"+myDT.Rows[i]["UserName"]+"\r\n     EmployeeNum :"+myDT.Rows[i]["EmployeeNum"]+"\r\n";
		//  //  //}
		//  //if(verbose || failed) {
		//  //  return retVal+"Test1 has "+(failed?"failed. Please do these things or tell your \"eCW Commander\", or whatever they are called, about this.":"passed.")+"\r\n";
		//  //}
		//  return retVal.ToString();
		//}

		/////<summary><para>SELECT * FROM &lt;tablename&gt; WHERE &lt;Any column contains textValue.text&gt;</para>
		/////<para>_</para>
		/////<para>Used for searching all fields of a table for a specified value</para></summary>
		///// <param name="tableName"></param>
		///// <returns>SQL query that selects applicable rows.</returns>
		//private string queryTableByValue(string tableName) {
		//  StringBuilder retVal=new StringBuilder();
		//  retVal.Append("SELECT * FROM "+tableName);
		//  if(textValue.Text=="") {
		//    return retVal.ToString()+" LIMIT 100;";
		//  }
		//  DataTable cols=MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connString,"SHOW COLUMNS FROM "+tableName).Tables[0];
		//  retVal.Append(" WHERE ");
		//  for(int i=0;i<cols.Rows.Count;i++) {
		//    if(i!=0) {
		//      retVal.Append("OR");
		//    }
		//    retVal.Append(" `"+cols.Rows[i]["Field"].ToString()+"` LIKE '%"+POut.String(textValue.Text)+"%' ");
		//  }
		//  return retVal.ToString()+" LIMIT 100;";
		//}


	}
}