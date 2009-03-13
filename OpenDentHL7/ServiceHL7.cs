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
using OpenDental.DataAccess;//this namespace is in the OpenDentBusiness project.

namespace OpenDentHL7 {
	public partial class ServiceHL7:ServiceBase {
		//private string 

		public ServiceHL7() {
			InitializeComponent();
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
			string dbVersion=PrefC.GetString("ProgramVersion");
			if(Application.ProductVersion.ToString() != dbVersion) {
				throw new ApplicationException("Versions do not match.  Db version:"+dbVersion+".  Application version:"+Application.ProductVersion.ToString());
			}
			//inform od via signal that this service is running


			//start filewatcher
			string command=@"SELECT PropertyValue FROM programproperty,program
				WHERE programproperty.ProgramNum=program.ProgramNum
				AND program.ProgName='eClinicalWorks'
				AND programproperty.PropertyDesc='HL7FolderOut'";
			DataTable table=General.GetTable(command);
			string hl7folderOut=table.Rows[0][0].ToString();
			FileSystemWatcher watcher=new FileSystemWatcher(hl7folderOut);//'out' from eCW
			watcher.Created += new FileSystemEventHandler(OnCreated);
			watcher.Renamed += new RenamedEventHandler(OnRenamed);
			watcher.EnableRaisingEvents=true;
			//process all waiting messages
			string[] existingFiles=Directory.GetFiles(hl7folderOut);
			for(int i=0;i<existingFiles.Length;i++) {
				ProcessMessage(File.ReadAllText(existingFiles[i]));
			}
		}

		private static void OnCreated(object source,FileSystemEventArgs e) {
			ProcessMessage(File.ReadAllText(e.FullPath));
		}

		private static void OnRenamed(object source,RenamedEventArgs e) {
			ProcessMessage(File.ReadAllText(e.FullPath));
		}

		private static void ProcessMessage(string msgtext) {
			MessageHL7 msg=new MessageHL7(msgtext);//this creates an entire heirarchy of objects.
			if(msg.MsgType==MessageType.ADT) {
				ADT.ProcessMessage(msg);
			}
			else if(msg.MsgType==MessageType.SIU) {
				SIU.ProcessMessage(msg);
			}
			//we won't be processing DFT messages.
			//else if(msg.MsgType==MessageType.DFT) {
				//ADT.ProcessMessage(msg);
			//}
		}

		protected override void OnStop() {
			//inform od via signal that this service has shut down
		}
	}
}
