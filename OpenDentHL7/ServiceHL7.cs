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
		private System.Threading.Timer timerSend;
		private System.Threading.Timer timerReceive;
		private static string hl7FolderIn;
		private static string hl7FolderOut;
		///<summary>Indicates the standalone mode for eCW, or the use of Mountainside.  In both cases, chartNumber will be used instead of PatNum.</summary>
		private static bool IsStandalone;
		private bool IsVerboseLogging;
		private static bool isReceiving;

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
			//inform od via signal that this service is running
			IsStandalone=true;//and for Mountainside
			//if(Programs.UsingEcwTight()){
			if(Programs.UsingEcwTightOrFull()){
				IsStandalone=false;
			}
			//#if DEBUG//just so I don't forget to remove it later.
			//IsStandalone=false;
			//#endif
			hl7FolderOut=PrefC.GetString(PrefName.HL7FolderOut);
			if(!Directory.Exists(hl7FolderOut)) {
				throw new ApplicationException(hl7FolderOut+" does not exist.");
			}
			//start polling the folder for waiting messages to import.  Every 5 seconds.
			TimerCallback timercallbackReceive=new TimerCallback(TimerCallbackReceiveFunction);
			timerReceive=new System.Threading.Timer(timercallbackReceive,null,5000,5000);
			if(IsStandalone) {
				return;//do not continue with the HL7 sending code below
			}
			//start polling the db for new HL7 messages to send. Every 1.8 seconds.
			hl7FolderIn=PrefC.GetString(PrefName.HL7FolderIn);
			if(!Directory.Exists(hl7FolderIn)) {
				throw new ApplicationException(hl7FolderIn+" does not exist.");
			}
			TimerCallback timercallbackSend=new TimerCallback(TimerCallbackSendFunction);
			timerSend=new System.Threading.Timer(timercallbackSend,null,1800,1800);
		}

		private void TimerCallbackReceiveFunction(Object stateInfo) {
			//process all waiting messages
			if(isReceiving) {
				return;//already in the middle of processing files
			}
			isReceiving=true;
			string[] existingFiles=Directory.GetFiles(hl7FolderOut);
			for(int i=0;i<existingFiles.Length;i++) {
				ProcessMessage(existingFiles[i]);
			}
			isReceiving=false;
		}
		
		private void ProcessMessage(string fullPath) {
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
				if(msg.MsgType==MessageType.ADT) {
					if(IsVerboseLogging) {
						EventLog.WriteEntry("OpenDentHL7","Processed ADT message",EventLogEntryType.Information);
					}
					ADT.ProcessMessage(msg,IsStandalone,IsVerboseLogging);
				}
				else if(msg.MsgType==MessageType.SIU && !IsStandalone) {//appointments don't get imported if standalone mode.
					if(IsVerboseLogging) {
						EventLog.WriteEntry("OpenDentHL7","Processed SUI message",EventLogEntryType.Information);
					}
					SIU.ProcessMessage(msg,IsStandalone,IsVerboseLogging);
				}
			}
			catch(Exception ex) {
				EventLog.WriteEntry(ex.Message+"\r\n"+ex.StackTrace,EventLogEntryType.Error);
				return;
			}
			//we won't be processing DFT messages.
			//else if(msg.MsgType==MessageType.DFT) {
				//ADT.ProcessMessage(msg);
			//}
			try {
				File.Delete(fullPath);
			}
			catch(Exception ex) {
				EventLog.WriteEntry("Delete failed for "+fullPath+"\r\n"+ex.Message,EventLogEntryType.Error);
			}
		}

		protected override void OnStop() {
			//inform od via signal that this service has shut down
			if(timerSend!=null) {
				timerSend.Dispose();
			}
		}

		private void TimerCallbackSendFunction(Object stateInfo) {
			//does not happen for standalone
			List<HL7Msg> list=HL7Msgs.GetOnePending();
			string filename;
			for(int i=0;i<list.Count;i++) {//Right now, there will only be 0 or 1 item in the list.
				if(list[i].AptNum==0){
					filename=ODFileUtils.CreateRandomFile(hl7FolderIn,".txt");
				}
				else{
					filename=Path.Combine(hl7FolderIn,list[i].AptNum.ToString()+".txt");
				}
				//EventLog.WriteEntry("Attempting to create file: "+filename);
				File.WriteAllText(filename,list[i].MsgText);
				list[i].HL7Status=HL7MessageStatus.OutSent;
				HL7Msgs.Update(list[i]);//set the status to sent.
				//put delete here//This is inside the loop so that it happens less frequently.
			}
		}
	}
}
