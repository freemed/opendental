using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using CodeBase;
using OpenDentBusiness;
using OpenDentBusiness.HL7;

namespace OpenDentHL7 {
	public partial class ServiceHL7:ServiceBase {
		private bool IsVerboseLogging;
		private System.Threading.Timer timerSendFiles;
		private System.Threading.Timer timerReceiveFiles;
		private System.Threading.Timer timerSendTCP;
		private Socket socketIncomingMain;
		private Socket socketIncomingWorker;
		///<summary></summary>
		private byte[] dataBufferIncoming;
		private StringBuilder strbFullMsg;
		private string hl7FolderIn;
		private string hl7FolderOut;
		private static bool isReceivingFiles;
		private const char MLLP_START_CHAR=(char)11;// HEX 0B
		private const char MLLP_END_CHAR=(char)28;// HEX 1C
		private const char MLLP_ENDMSG_CHAR=(char)13;// HEX 0D
		private const char MLLP_ACK_CHAR=(char)6;// HEX 06
		private const char MLLP_NACK_CHAR=(char)21;// HEX 15
		///<summary></summary>
		private HL7Def HL7DefEnabled;
		private static bool IsSendTCPConnected;

		public ServiceHL7() {
			InitializeComponent();
			CanStop = true;
			IsSendTCPConnected=true;//bool to keep track of whether connection attempts have failed in the past. At startup, the 'previous' attempt is assumed successful.
			EventLog.WriteEntry("OpenDentHL7",DateTime.Now.ToLongTimeString()+" - Initialized.");
		}

		protected override void OnStart(string[] args) {
			StartManually();
		}

		public void StartManually() {
			//connect to OD db.
			XmlDocument document=new XmlDocument();
			string pathXml=Path.Combine(Application.StartupPath,"FreeDentalConfig.xml");
			try{
				document.Load(pathXml);
			}
			catch{
				EventLog.WriteEntry("OpenDentHL7",DateTime.Now.ToLongTimeString()+" - Could not find "+pathXml,EventLogEntryType.Error);
				throw new ApplicationException("Could not find "+pathXml);
			}
			XPathNavigator Navigator=document.CreateNavigator();
			XPathNavigator nav;
			DataConnection.DBtype=DatabaseType.MySql;
			nav=Navigator.SelectSingleNode("//DatabaseConnection");
			string computerName=nav.SelectSingleNode("ComputerName").Value;
			string database=nav.SelectSingleNode("Database").Value;
			string user=nav.SelectSingleNode("User").Value;
			string password=nav.SelectSingleNode("Password").Value;
			XPathNavigator verboseNav=Navigator.SelectSingleNode("//HL7verbose");
			if(verboseNav!=null && verboseNav.Value=="True") {
				IsVerboseLogging=true;
				EventLog.WriteEntry("OpenDentHL7","Verbose mode.",EventLogEntryType.Information);
			}
			OpenDentBusiness.DataConnection dcon=new OpenDentBusiness.DataConnection();
			//Try to connect to the database directly
			try {
				dcon.SetDb(computerName,database,user,password,"","",DataConnection.DBtype);
				//a direct connection does not utilize lower privileges.
				RemotingClient.RemotingRole=RemotingRole.ClientDirect;
			}
			catch {//(Exception ex){
				throw new ApplicationException("Connection to database failed.");
			}
			//check db version
			string dbVersion=PrefC.GetString(PrefName.ProgramVersion);
			if(Application.ProductVersion.ToString() != dbVersion) {
				EventLog.WriteEntry("OpenDentHL7","Versions do not match.  Db version:"+dbVersion+".  Application version:"+Application.ProductVersion.ToString(),EventLogEntryType.Error);
				throw new ApplicationException("Versions do not match.  Db version:"+dbVersion+".  Application version:"+Application.ProductVersion.ToString());
			}
			if(Programs.IsEnabled(ProgramName.eClinicalWorks) && !HL7Defs.IsExistingHL7Enabled()) {//eCW enabled, and no HL7def enabled.
				//prevent startup:
				long progNum=Programs.GetProgramNum(ProgramName.eClinicalWorks);
				string hl7Server=ProgramProperties.GetPropVal(progNum,"HL7Server");
				string hl7ServiceName=ProgramProperties.GetPropVal(progNum,"HL7ServiceName");
				if(hl7Server=="") {//for the first time run
					ProgramProperties.SetProperty(progNum,"HL7Server",System.Environment.MachineName);
					hl7Server=System.Environment.MachineName;
				}
				if(hl7ServiceName=="") {//for the first time run
					ProgramProperties.SetProperty(progNum,"HL7ServiceName",this.ServiceName);
					hl7ServiceName=this.ServiceName;
				}
				if(hl7Server.ToLower()!=System.Environment.MachineName.ToLower()) {
					EventLog.WriteEntry("OpenDentHL7","The HL7 Server name does not match the name set in Program Links eClinicalWorks Setup.  Server name: "+System.Environment.MachineName
						+", Server name in Program Links: "+hl7Server,EventLogEntryType.Error);
					throw new ApplicationException("The HL7 Server name does not match the name set in Program Links eClinicalWorks Setup.  Server name: "+System.Environment.MachineName
						+", Server name in Program Links: "+hl7Server);
				}
				if(hl7ServiceName.ToLower()!=this.ServiceName.ToLower()) {
					EventLog.WriteEntry("OpenDentHL7","The HL7 Service Name does not match the name set in Program Links eClinicalWorks Setup.  Service name: "+this.ServiceName+", Service name in Program Links: "
						+hl7ServiceName,EventLogEntryType.Error);
					throw new ApplicationException("The HL7 Service Name does not match the name set in Program Links eClinicalWorks Setup.  Service name: "+this.ServiceName+", Service name in Program Links: "
						+hl7ServiceName);
				}
				EcwOldSendAndReceive();
				return;
			}
			HL7Def hL7Def=HL7Defs.GetOneDeepEnabled();
			if(hL7Def==null) {
				return;
			}
			if(hL7Def.HL7Server=="") {
				hL7Def.HL7Server=System.Environment.MachineName;
				HL7Defs.Update(hL7Def);
			}
			if(hL7Def.HL7ServiceName=="") {
				hL7Def.HL7ServiceName=this.ServiceName;
				HL7Defs.Update(hL7Def);
			}
			if(hL7Def.HL7Server.ToLower()!=System.Environment.MachineName.ToLower()) {
				EventLog.WriteEntry("OpenDentHL7","The HL7 Server name does not match the name in the enabled HL7Def Setup.  Server name: "+System.Environment.MachineName+", Server name in HL7Def: "+hL7Def.HL7Server,
					EventLogEntryType.Error);
				throw new ApplicationException("The HL7 Server name does not match the name in the enabled HL7Def Setup.  Server name: "+System.Environment.MachineName+", Server name in HL7Def: "+hL7Def.HL7Server);
			}
			if(hL7Def.HL7ServiceName.ToLower()!=this.ServiceName.ToLower()) {
				EventLog.WriteEntry("OpenDentHL7","The HL7 Service Name does not match the name in the enabled HL7Def Setup.  Service name: "+this.ServiceName+", Service name in HL7Def: "+hL7Def.HL7ServiceName,
					EventLogEntryType.Error);
				throw new ApplicationException("The HL7 Service Name does not match the name in the enabled HL7Def Setup.  Service name: "+this.ServiceName+", Service name in HL7Def: "+hL7Def.HL7ServiceName);
			}
			HL7DefEnabled=hL7Def;//so we can access it later from other methods
			if(HL7DefEnabled.ModeTx==ModeTxHL7.File) {
				hl7FolderOut=HL7DefEnabled.OutgoingFolder;
				hl7FolderIn=HL7DefEnabled.IncomingFolder;
				if(!Directory.Exists(hl7FolderOut)) {
					EventLog.WriteEntry("OpenDentHL7","The outgoing HL7 folder does not exist.  Path is set to: "+hl7FolderOut,EventLogEntryType.Error);
					throw new ApplicationException("The outgoing HL7 folder does not exist.  Path is set to: "+hl7FolderOut);
				}
				if(!Directory.Exists(hl7FolderIn)) {
					EventLog.WriteEntry("OpenDentHL7","The incoming HL7 folder does not exist.  Path is set to: "+hl7FolderIn,EventLogEntryType.Error);
					throw new ApplicationException("The incoming HL7 folder does not exist.  Path is set to: "+hl7FolderIn);
				}
				//start polling the folder for waiting messages to import.  Every 5 seconds.
				TimerCallback timercallbackReceive=new TimerCallback(TimerCallbackReceiveFiles);
				timerReceiveFiles=new System.Threading.Timer(timercallbackReceive,null,5000,5000);
				//start polling the db for new HL7 messages to send. Every 1.8 seconds.
				TimerCallback timercallbackSend=new TimerCallback(TimerCallbackSendFiles);
				timerSendFiles=new System.Threading.Timer(timercallbackSend,null,1800,1800);
			}
			else {//TCP/IP
				CreateIncomingTcpListener();
				//start a timer to poll the database and to send messages as needed.  Every 6 seconds.  We increased the time between polling the database from 3 seconds to 6 seconds because we are now waiting 5 seconds for a message acknowledgment from eCW.
				TimerCallback timercallbackSendTCP=new TimerCallback(TimerCallbackSendTCP);
				timerSendTCP=new System.Threading.Timer(timercallbackSendTCP,null,1800,6000);
			}
		}

		private void TimerCallbackReceiveFiles(Object stateInfo) {
			//process all waiting messages
			if(isReceivingFiles) {
				return;//already in the middle of processing files
			}
			isReceivingFiles=true;
			string[] existingFiles=Directory.GetFiles(hl7FolderIn);
			for(int i=0;i<existingFiles.Length;i++) {
				ProcessMessageFile(existingFiles[i]);
			}
			isReceivingFiles=false;
		}

		private void ProcessMessageFile(string fullPath) {
			string msgtext="";
			int i=0;
			while(i<5) {
				try {
					msgtext=File.ReadAllText(fullPath);
					break;
				}
				catch {
				}
				Thread.Sleep(200);
				i++;
				if(i==5) {
					EventLog.WriteEntry("Could not read text from file due to file locking issues.",EventLogEntryType.Error);
					return;
				}
			}
			try {
				MessageHL7 msg=new MessageHL7(msgtext);//this creates an entire heirarchy of objects.
				MessageParser.Process(msg,IsVerboseLogging);
				if(IsVerboseLogging) {
					EventLog.WriteEntry("OpenDentHL7","Processed message "+msg.MsgType.ToString(),EventLogEntryType.Information);
				}
			}
			catch(Exception ex) {
				EventLog.WriteEntry(ex.Message+"\r\n"+ex.StackTrace,EventLogEntryType.Error);
				return;
			}
			try {
				File.Delete(fullPath);
			}
			catch(Exception ex) {
				EventLog.WriteEntry("Delete failed for "+fullPath+"\r\n"+ex.Message,EventLogEntryType.Error);
			}
		}
		
		protected override void OnStop() {
			//later: inform od via signal that this service has shut down
			EcwOldStop();
			if(timerSendFiles!=null) {
				timerSendFiles.Dispose();
			}
		}

		private void TimerCallbackSendFiles(Object stateInfo) {
			List<HL7Msg> list=HL7Msgs.GetOnePending();
			string filename;
			for(int i=0;i<list.Count;i++) {//Right now, there will only be 0 or 1 item in the list.
				filename=ODFileUtils.CreateRandomFile(hl7FolderOut,".txt");
				File.WriteAllText(filename,list[i].MsgText);
				list[i].HL7Status=HL7MessageStatus.OutSent;
				HL7Msgs.Update(list[i]);//set the status to sent.
				HL7Msgs.DeleteOldMsgText();//This is inside the loop so that it happens less frequently.  To clean up incoming messages, we may move this someday.
			}
		}

		private void CreateIncomingTcpListener() {
				//Use Minimal Lower Layer Protocol (MLLP):
				//To send a message:              StartBlockChar(11) -          Payload            - EndBlockChar(28) - EndDataChar(13).
				//An ack message looks like this: StartBlockChar(11) - AckChar(0x06)/NakChar(0x15) - EndBlockChar(28) - EndDataChar(13).
				//Ack is part of MLLP V2.  In it, every message requires an ack or nak.  It's unclear when a nak would be useful.
				//Also in V2, every incoming message must be persisted by storing in our db.
				//We will just start with version 1 and not do acks at first unless needed.
			try {
				socketIncomingMain=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				IPEndPoint endpointLocal=new IPEndPoint(IPAddress.Any,int.Parse(HL7DefEnabled.IncomingPort));
				socketIncomingMain.Bind(endpointLocal);
				socketIncomingMain.Listen(1);//Listen for and queue incoming connection requests.  There should only be one.
				//Asynchronously process incoming connection attempts:
				socketIncomingMain.BeginAccept(new AsyncCallback(OnConnectionAccepted),socketIncomingMain);
			}
			catch(Exception ex) {
				EventLog.WriteEntry("OpenDentHL7","Error creating incoming TCP listener\r\n"+ex.Message+"\r\n"+ex.StackTrace,EventLogEntryType.Error);
				throw ex;
				//service will stop working at this point.
			}
		}

		///<summary>Runs in a separate thread</summary>
		private void OnConnectionAccepted(IAsyncResult asyncResult) {
			try {
				socketIncomingWorker=socketIncomingMain.EndAccept(asyncResult);//end the BeginAccept.  Get reference to new Socket.
				//Use the worker socket to wait for data.
				//This is very short for testing.  Once we are confident in splicing together multiple chunks, lengthen this.
				dataBufferIncoming=new byte[8];
				strbFullMsg=new StringBuilder();
				//We will keep reusing the same workerSocket instead of maintaining a list of worker sockets
				//because this program is guaranteed to only have one incoming connection at a time.
				socketIncomingWorker.BeginReceive(dataBufferIncoming,0,dataBufferIncoming.Length,SocketFlags.None,new AsyncCallback(OnDataReceived),null);
				//the main socket is now free to wait for another connection.
				socketIncomingMain.BeginAccept(new AsyncCallback(OnConnectionAccepted),socketIncomingMain);
			}
			catch(ObjectDisposedException){      
				//Socket has been closed.  Try to start over.
				CreateIncomingTcpListener();//If this fails, service stops running
			}   
			catch(Exception ex){      
				//not sure what went wrong.
				EventLog.WriteEntry("OpenDentHL7","Error in OnConnectionAccpeted:\r\n"+ex.Message+"\r\n"+ex.StackTrace,EventLogEntryType.Error);
				throw;//service will stop working at this point.
			}
		}

		///<summary>Runs in a separate thread</summary>
		private void OnDataReceived(IAsyncResult asyncResult) {
			int byteCountReceived=socketIncomingWorker.EndReceive(asyncResult);//blocks until data is recieved.
			char[] chars=new char[byteCountReceived];
			Decoder decoder=Encoding.UTF8.GetDecoder();
			decoder.GetChars(dataBufferIncoming,0,byteCountReceived,chars,0);//doesn't necessarily get all bytes from the buffer because buffer could be half full.
			strbFullMsg.Append(chars);//strbFullMsg might already have partial data
			//I think we are guaranteed to have received at least one char.
			bool isFullMsg=false;
			bool isMalformed=false;
			if(strbFullMsg.Length==1 && strbFullMsg[0]==MLLP_ENDMSG_CHAR){//the only char in the message is the end char
				strbFullMsg.Clear();//this must be the very end of a previously processed message.  Discard.
				isFullMsg=false;
			}
			else if(strbFullMsg[0]!=MLLP_START_CHAR){
				//Malformed message. 
				isFullMsg=true;//we're going to do this so that the error gets saved in the database further down.
				isMalformed=true;
			}
			else if(strbFullMsg.Length>=3//so that the next two lines won't crash
				&& strbFullMsg[strbFullMsg.Length-1]==MLLP_ENDMSG_CHAR//last char is the endmsg char.
				&& strbFullMsg[strbFullMsg.Length-2]==MLLP_END_CHAR)//the second-to-the-last char is the end char.
			{
				//we have a complete message
				strbFullMsg.Remove(0,1);//strip off the start char
				strbFullMsg.Remove(strbFullMsg.Length-2,2);//strip off the end chars
				isFullMsg=true;
			}
			else if(strbFullMsg.Length>=2//so that the next line won't crash
				&& strbFullMsg[strbFullMsg.Length-1]==MLLP_END_CHAR)//the last char is the end char.
			{
				//we will treat this as a complete message, because the endmsg char is optional.
				//if the endmsg char gets sent in a subsequent block, the code above will discard it.
				strbFullMsg.Remove(0,1);//strip off the start char
				strbFullMsg.Remove(strbFullMsg.Length-1,1);//strip off the end char
				isFullMsg=true;
			}
			else{
				isFullMsg=false;//this is an incomplete message.  Continue to receive more blocks.
			}
			//end of big if statement-------------------------------------------------
			if(!isFullMsg) {
				dataBufferIncoming=new byte[8];//clear the buffer
				socketIncomingWorker.BeginReceive(dataBufferIncoming,0,dataBufferIncoming.Length,SocketFlags.None,new AsyncCallback(OnDataReceived),null);
				return;//get another block
			}
			//Prepare to save message to database if malformed and not processed
			HL7Msg hl7Msg=new HL7Msg();
			hl7Msg.MsgText=strbFullMsg.ToString();		
			strbFullMsg.Clear();//ready for the next message
			bool isProcessed=true;
			string messageControlId="";
			EventTypeHL7 eventType=EventTypeHL7.A04;
			if(isMalformed){
				hl7Msg.HL7Status=HL7MessageStatus.InFailed;
				hl7Msg.Note="This message is malformed so it was not processed.";
				HL7Msgs.Insert(hl7Msg);
				isProcessed=false;
			}
			else{
				MessageHL7 messageHl7Object=new MessageHL7(hl7Msg.MsgText);//this creates an entire heirarchy of objects.
				try {
					MessageParser.Process(messageHl7Object,IsVerboseLogging);//also saves the message to the db
					messageControlId=messageHl7Object.ControlId;
					eventType=messageHl7Object.EventType;
				}
				catch(Exception ex) {
					EventLog.WriteEntry("OpenDentHL7","Error in OnDataRecieved when processing message:\r\n"+ex.Message+"\r\n"+ex.StackTrace,EventLogEntryType.Error);
					isProcessed=false;
				}
			}
			MessageHL7 hl7Ack=MessageConstructor.GenerateACK(messageControlId,eventType,isProcessed);
			if(hl7Ack==null) {
				EventLog.WriteEntry("OpenDentHL7","No ACK defined for the enabled HL7 definition or no HL7 definition enabled.",EventLogEntryType.Information);
				return;
			}
			byte[] ackByteOutgoing=Encoding.ASCII.GetBytes(MLLP_START_CHAR+hl7Ack.ToString()+MLLP_END_CHAR+MLLP_ENDMSG_CHAR);
			socketIncomingWorker.Send(ackByteOutgoing);//this is a locking call
		}
		
		private void TimerCallbackSendTCP(Object stateInfo) {
			List<HL7Msg> list=HL7Msgs.GetOnePending();
			for(int i=0;i<list.Count;i++) {//Right now, there will only be 0 or 1 item in the list.
				string sendMsgControlId=HL7Msgs.GetControlId(list[i]);//could be empty string
				Socket socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				string[] strIpPort=HL7DefEnabled.OutgoingIpPort.Split(':');//this was already validated in the HL7DefEdit window.
				IPAddress ipaddress=IPAddress.Parse(strIpPort[0]);//already validated
				int port=int.Parse(strIpPort[1]);//already validated
				IPEndPoint endpoint=new IPEndPoint(ipaddress,port);
				try {
					socket.Connect(endpoint);
					if(!IsSendTCPConnected) {//Previous run failed to connect. This time connected so make log entry that connection was successful
						EventLog.WriteEntry("OpenDentHL7","The HL7 send TCP/IP socket connection failed to connect previously and was successful this time.",EventLogEntryType.Information);
						IsSendTCPConnected=true;
					}
				}
				catch(SocketException ex) {
					if(IsSendTCPConnected) {//Previous run connected fine, make log entry and set bool to false
						EventLog.WriteEntry("OpenDentHL7","The HL7 send TCP/IP socket connection failed to connect.\r\nException: "+ex.Message,EventLogEntryType.Warning);
						IsSendTCPConnected=false;
					}
					return;
				}
				string data=MLLP_START_CHAR+list[i].MsgText+MLLP_END_CHAR+MLLP_ENDMSG_CHAR;
				try {
					byte[] byteData=Encoding.ASCII.GetBytes(data);
					socket.Send(byteData);//this is a blocking call
					//For MLLP V2, do a blocking Receive here, along with a timeout.
					byte[] ackBuffer=new byte[256];//plenty big enough to receive the entire ack/nack response
					socket.ReceiveTimeout=5000;//5 second timeout. Database is polled every 6 seconds for a new message to send, 5 second wait for acknowledgment
					int byteCountReceived=socket.Receive(ackBuffer);//blocking Receive
					char[] chars=new char[byteCountReceived];
					Encoding.UTF8.GetDecoder().GetChars(ackBuffer,0,byteCountReceived,chars,0);
					StringBuilder strbAckMsg=new StringBuilder();
					strbAckMsg.Append(chars);
					if(strbAckMsg.Length>0 && strbAckMsg[0]!=MLLP_START_CHAR) {
						list[i].Note=list[i].Note+"Malformed acknowledgment.\r\n";
						HL7Msgs.Update(list[i]);
						throw new Exception("Malformed acknowledgment.");
					}
					else if(strbAckMsg.Length>=3
						&& strbAckMsg[strbAckMsg.Length-1]==MLLP_ENDMSG_CHAR//last char is the endmsg char.
						&& strbAckMsg[strbAckMsg.Length-2]==MLLP_END_CHAR)//the second-to-the-last char is the end char.
					{
						//we have a complete message
						strbAckMsg.Remove(0,1);//strip off the start char
						strbAckMsg.Remove(strbAckMsg.Length-2,2);//strip off the end chars
					}
					else if(strbAckMsg.Length>=2//so that the next line won't crash
						&& strbAckMsg[strbAckMsg.Length-1]==MLLP_END_CHAR)//the last char is the end char.
					{
						//we will treat this as a complete message, because the endmsg char is optional.
						strbAckMsg.Remove(0,1);//strip off the start char
						strbAckMsg.Remove(strbFullMsg.Length-1,1);//strip off the end char
					}
					else {
						list[i].Note=list[i].Note+"Malformed acknowledgment.\r\n";
						HL7Msgs.Update(list[i]);
						throw new Exception("Malformed acknowledgment.");
					}
					MessageHL7 ackMsg=new MessageHL7(strbAckMsg.ToString());
					try {
						MessageParser.ProcessAck(ackMsg,IsVerboseLogging);
					}
					catch(Exception ex) {
						list[i].Note=list[i].Note+ackMsg.ToString()+"\r\nError processing acknowledgment.\r\n";
						HL7Msgs.Update(list[i]);
						throw new Exception("Error processing acknowledgment.\r\n"+ex.Message);
					}
					if(ackMsg.AckCode=="" || ackMsg.ControlId=="") {
						list[i].Note=list[i].Note+ackMsg.ToString()+"\r\nInvalid ACK message.  Attempt to resend.\r\n";
						HL7Msgs.Update(list[i]);
						throw new Exception("Invalid ACK message received.");
					}
					if(ackMsg.ControlId==sendMsgControlId && ackMsg.AckCode=="AA") {//acknowledged received (Application acknowledgment: Accept)
						list[i].Note=list[i].Note+ackMsg.ToString()+"\r\nMessage ACK (acknowledgment) received.\r\n";
					}
					else if(ackMsg.ControlId==sendMsgControlId && ackMsg.AckCode!="AA") {//ACK received for this message, but ack code was not acknowledgment accepted
						if(list[i].Note.Contains("NACK4")) {//this is the 5th negative acknowledgment, don't try again
							list[i].Note=list[i].Note+"Ack code: "+ackMsg.AckCode+"\r\nThis is NACK5, the message status has been changed to OutFailed. We will not attempt to send again.\r\n";
							list[i].HL7Status=HL7MessageStatus.OutFailed;
						}
						else if(list[i].Note.Contains("NACK")) {//failed sending at least once already
							list[i].Note=list[i].Note+"Ack code: "+ackMsg.AckCode+"\r\nNACK"+list[i].Note.Split(new string[] { "NACK" },StringSplitOptions.None).Length+"\r\n";
						}
						else {
							list[i].Note=list[i].Note+"Ack code: "+ackMsg.AckCode+"\r\nMessage NACK (negative acknowlegment) received. We will try to send again.\r\n";
						}
						HL7Msgs.Update(list[i]);//Add NACK note to hl7msg table entry
						return;//socket closed in finally
					}
					else {//ack received for control ID that does not match the control ID of message just sent
						list[i].Note=list[i].Note+"Sent message control ID: "+sendMsgControlId+"\r\nAck message control ID: "+ackMsg.ControlId
							+"\r\nAck received for message other than message just sent.  We will try to send again.\r\n";
						HL7Msgs.Update(list[i]);
						return;
					}
				}
				catch(SocketException ex) {
					EventLog.WriteEntry("OpenDentHL7","The HL7 send TCP/IP socket encountered an error when sending message or receiving acknowledgment.\r\nSocket Exception: "+ex.Message,EventLogEntryType.Warning);
					return;//Try again in 3 seconds. socket closed in finally
				}
				catch(Exception ex) {
					EventLog.WriteEntry("OpenDentHL7","The HL7 send TCP/IP encountered an error when sending message or receiving acknowledgment.\r\nException: "+ex.Message,EventLogEntryType.Warning);
					return;//socket closed in finally
				}
				finally {
					if(socket!=null) {
						socket.Shutdown(SocketShutdown.Both);
						socket.Close();
					}
				}
				list[i].HL7Status=HL7MessageStatus.OutSent;
				HL7Msgs.Update(list[i]);//set the status to sent and save ack message in Note field.
				HL7Msgs.DeleteOldMsgText();//This is inside the loop so that it happens less frequently.  To clean up incoming messages, we may move this someday.
			}
		}
		
		
	}
}
