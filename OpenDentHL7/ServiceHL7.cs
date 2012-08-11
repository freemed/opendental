using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
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
		private string hl7FolderIn;
		private string hl7FolderOut;
		private static bool isReceivingFiles;

		public ServiceHL7() {
			InitializeComponent();
			CanStop = true;
			ServiceName = "OpenDentHL7";
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
			//Later: inform od via signal that this service is running
			if(Programs.IsEnabled(ProgramName.eClinicalWorks)) {
				EcwOldSendAndReceive();
			}
			HL7Def hL7Def=HL7Defs.GetOneDeepEnabled();
			if(hL7Def==null) {
				return;
			}
			if(hL7Def.ModeTx==ModeTxHL7.File) {
				hl7FolderOut=hL7Def.OutgoingFolder;
				hl7FolderIn=hL7Def.IncomingFolder;
//todo: check to make sure both folders exist.  Errors if not.
				//start polling the folder for waiting messages to import.  Every 5 seconds.
				TimerCallback timercallbackReceive=new TimerCallback(TimerCallbackReceiveFiles);
				timerReceiveFiles=new System.Threading.Timer(timercallbackReceive,null,5000,5000);
				//start polling the db for new HL7 messages to send. Every 1.8 seconds.
				TimerCallback timercallbackSend=new TimerCallback(TimerCallbackSendFiles);
				timerSendFiles=new System.Threading.Timer(timercallbackSend,null,1800,1800);
			}
			else {
				//tcp/ip later.  Use MLLP protocol.
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
				MessageParser.Process(msg);
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
				HL7Msgs.DeleteOldMessages();//This is inside the loop so that it happens less frequently.  To clean up incoming messages, we may move this someday.
			}
		}
		
	}
}
