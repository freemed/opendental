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
using OpenDentBusiness;
using OpenDentBusiness.HL7;
using OpenDentBusiness.DataAccess;//this namespace is in the OpenDentBusiness project.

namespace OpenDentHL7 {
	public partial class ServiceHL7:ServiceBase {
		private System.Threading.Timer timer;
		private static string inFolder;
		///<summary>Indicates the standalone mode for eCW, or the use of Mountainside.  In both cases, chartNumber will be used instead of PatNum.</summary>
		private static bool IsStandalone;

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
			string appPath=Application.StartupPath;
			//EventLog.WriteEntry(appPath);
			document.Load(Path.Combine(appPath,"FreeDentalConfig.xml"));
			XPathNavigator Navigator=document.CreateNavigator();
			XPathNavigator nav;
			DataConnection.DBtype=DatabaseType.MySql;
			nav=Navigator.SelectSingleNode("//DatabaseConnection");
			string computerName=nav.SelectSingleNode("ComputerName").Value;
			string database=nav.SelectSingleNode("Database").Value;
			string user=nav.SelectSingleNode("User").Value;
			string password=nav.SelectSingleNode("Password").Value;
			OpenDentBusiness.DataConnection dcon=new OpenDentBusiness.DataConnection();
			//Try to connect to the database directly
			try {
				DataSettings.CreateConnectionString(computerName,database,user,password);
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
				throw new ApplicationException("Versions do not match.  Db version:"+dbVersion+".  Application version:"+Application.ProductVersion.ToString());
			}
			//inform od via signal that this service is running

			IsStandalone=true;//and for Mountainside
			if(Programs.IsEnabled("eClinicalWorks")
				&& ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") 
			{
				IsStandalone=false;
			}
			//start filewatcher

			string hl7folderOut=PrefC.GetString(PrefName.HL7FolderOut);
				//ProgramProperties.GetPropVal("eClinicalWorks","HL7FolderOut");
				//HL7Msgs.GetHL7FolderOut();
			if(!Directory.Exists(hl7folderOut)) {
				throw new ApplicationException(hl7folderOut+" does not exist.");
			}
			FileSystemWatcher watcher=new FileSystemWatcher(hl7folderOut);//'out' from eCW/Mountainside
			watcher.Created += new FileSystemEventHandler(OnCreated);
			watcher.Renamed += new RenamedEventHandler(OnRenamed);
			watcher.EnableRaisingEvents=true;
			//process all waiting messages
			string[] existingFiles=Directory.GetFiles(hl7folderOut);
			for(int i=0;i<existingFiles.Length;i++) {
				ProcessMessage(existingFiles[i]);
			}
			if(IsStandalone) {
				return;//do not continue with the HL7 sending code below
			}
			//start polling the db for new HL7 messages to send
			inFolder=PrefC.GetString(PrefName.HL7FolderIn);
				//ProgramProperties.GetPropVal("eClinicalWorks","HL7FolderIn");
				//HL7Msgs.GetHL7FolderIn();
			if(!Directory.Exists(inFolder)) {
				throw new ApplicationException(inFolder+" does not exist.");
			}
			TimerCallback callback=new TimerCallback(TimerCallbackFunction);
			timer=new System.Threading.Timer(callback,null,1800,1800);
			//timer.
			//timer.Tick+=new EventHandler(timer_Tick);
			//timer.Interval=1800;//just under 2 seconds.
		}

		private void OnCreated(object source,FileSystemEventArgs e) {
			ProcessMessage(e.FullPath);
		}

		private void OnRenamed(object source,RenamedEventArgs e) {
			ProcessMessage(e.FullPath);
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
					
					ADT.ProcessMessage(msg,IsStandalone);
				}
				else if(msg.MsgType==MessageType.SIU && !IsStandalone) {//appointments don't get imported if standalone mode.
					SIU.ProcessMessage(msg);
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
			File.Delete(fullPath);
		}

		protected override void OnStop() {
			//inform od via signal that this service has shut down
			//timer.Enabled=false;
			if(timer!=null) {
				timer.Dispose();
			}
		}

		private static void TimerCallbackFunction(Object stateInfo) {
			//string diagnosticMsg="";
			List<HL7Msg> list=HL7Msgs.GetAllPending();
			//if(list.Count==0) {
			//	EventLog.WriteEntry("No messages found.  Connection string and query: "+diagnosticMsg);
			//}
			//else {
			//	EventLog.WriteEntry("Messages found: "+list.Count.ToString());
			//}
			string filename;
			for(int i=0;i<list.Count;i++) {
				filename=Path.Combine(inFolder,list[i].AptNum.ToString()+".txt");
				//EventLog.WriteEntry("Attempting to create file: "+filename);
				File.WriteAllText(filename,list[i].MsgText);
				list[i].HL7Status=HL7MessageStatus.OutSent;
				HL7Msgs.Update(list[i]);//set the status to sent.
			}
		}
	}
}
