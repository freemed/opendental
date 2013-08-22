using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;

namespace OpenDental {
	public partial class FormCodeSystemsImport:Form {
		///<summary>used to populate lists</summary>
		private List<CodeSystem> ListCodeSystems;

		public FormCodeSystemsImport() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCodeSystemsImport_Load(object sender,EventArgs e) {
			ListCodeSystems=CodeSystems.GetForCurrentVersion();
		}

		/// <summary></summary>
		private void FillGrid() {
			ListCodeSystems=CodeSystems.GetForCurrentVersion();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Code System",200,false);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Current Version",100,false);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Available Version",100,false);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListCodeSystems.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(ListCodeSystems[i].CodeSystemName);
				row.Cells.Add(ListCodeSystems[i].VersionCur);
				row.Cells.Add(ListCodeSystems[i].VersionAvail);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butCheckUpdates_Click(object sender,EventArgs e) {
			if(!IsValidUserRequest()) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			string result="";
			try {
				result=RequestCodeSystemsXml();
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return;
			}
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			List<CodeSystem> listCodeSystemsAvailable=CodeSystems.GetForCurrentVersion();
			for(int i=0;i<listCodeSystemsAvailable.Count;i++) {
				try {
					XmlNode node=doc.SelectSingleNode("//"+listCodeSystemsAvailable[i].CodeSystemName);
					if(node!=null) {
						listCodeSystemsAvailable[i].VersionAvail=node.Attributes["VersionAvailable"].InnerText;
						CodeSystems.Update(listCodeSystemsAvailable[i]);
					}
				}
				catch (Exception ex){//should never happen
					//Might happen if they are running this tool without the right rows in the CodeSystem table? Maybe.
					MessageBox.Show(this,Lan.g(this,"Error checking for code system.\r\n")+ex.Message);
					continue;
				}
			}
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butDownload_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"No code systems selected.");
				return;
			}
			if(!IsValidUserRequest()) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			Application.DoEvents();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				try {
					if(requestCodeSystemDownloadHelper(ListCodeSystems[gridMain.SelectedIndices[i]].CodeSystemName)) {//can throw exceptions
						CodeSystems.UpdateCurrentVersion(ListCodeSystems[gridMain.SelectedIndices[i]]);//set current version=available version
					}
				}
				catch(Exception ex) {
					MessageBox.Show(Lan.g(this,"Error encounter while importing code system")+":"+ListCodeSystems[gridMain.SelectedIndices[i]].CodeSystemName+"\r\n"+ex.Message);
					continue;
				}
			}
			FillGrid();
			Cursor=Cursors.Default;
		}

		///<summary>Throws exceptions, put in try catch block. Will request, download, and import codeSystem from webservice. Returns false if unsuccessful.</summary>
		private bool requestCodeSystemDownloadHelper(string codeSystemName) {
			string result="";
			try {
				result=SendAndReceiveDownloadXml(codeSystemName);
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return false;
			}
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			XmlNode node=doc.SelectSingleNode("//Error");
			if(node!=null) {
				throw new Exception(node.InnerText);
			}
			node=doc.SelectSingleNode("//CodeSystemURL");
			string codeSystemURL="";
			if(node!=null){
				codeSystemURL=node.InnerText;
			}
			//Download File to local machine
			downloadFileHelper(codeSystemURL,codeSystemName);//shows progress bar.
			switch(codeSystemName) {
				case "AdministrativeSex":
					try {
						CodeSystems.ImportAdministrativeSex();
						//Do not fill grid here, or update UI, happens at end of all updates.
						MsgBox.Show(this,"AdministrativeSex codes imported successfully.");
					}
					catch(Exception ex){
						MessageBox.Show(this,ex.Message);
						return false;
					}
					break;
				default:
					//should never happen
					break;
			}
			return true;
		}

		private static void downloadFileHelper(string codeSystemURL,string codeSystemName) {
			string destinationPath=@"c:\users\ryan\desktop\"+codeSystemName+".txt";
			//TODO:determine destination path
			File.Delete(destinationPath);
			WebRequest wr=WebRequest.Create(codeSystemURL);
			WebResponse webResp=null;
			try {
				webResp=wr.GetResponse();
			}
			catch(Exception ex) {
				CodeBase.MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(ex.Message+"\r\nUri: "+codeSystemURL);
				msgbox.ShowDialog();
				return;
			}
			int fileSize=(int)webResp.ContentLength/1024;
			FormProgress FormP=new FormProgress();
			//start the thread that will perform the download
			System.Threading.ThreadStart downloadDelegate= delegate { DownloadInstallPatchWorker(codeSystemURL,destinationPath,ref FormP); };
			Thread workerThread=new System.Threading.Thread(downloadDelegate);
			workerThread.Start();
			//display the progress dialog to the user:
			FormP.MaxVal=(double)fileSize/1024;
			FormP.NumberMultiplication=100;
			FormP.DisplayText="?currentVal MB of ?maxVal MB copied";
			FormP.NumberFormat="F";
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel) {
				workerThread.Abort();
				return;
			}
			//copy the Setup.exe to the AtoZ folders for the other db's.
			List<string> atozNameList=new List<string>();//MiscData.GetAtoZforDb(new string[] {MiscData.GetCurrentDatabase()});//TODO:make this better
			atozNameList.Add(@"C:\OpenDentImages\");
			for(int i=0;i<atozNameList.Count;i++) {
				if(destinationPath==Path.Combine(atozNameList[i],"CodeSystems\\"+codeSystemName+".txt")) {//if they are sharing an AtoZ folder.
					continue;
				}
				if(Directory.Exists(atozNameList[i])) {
					File.Copy(destinationPath,//copy the CodeSystem.txt that was just downloaded to this AtoZ folder
						Path.Combine(atozNameList[i],"CodeSystems\\"+codeSystemName+".txt"),//to the other atozFolder
						true);//overwrite
				}
			}
		}

		///<summary>This is the function that the worker thread uses to actually perform the download.  Can also call this method in the ordinary way if the file to be transferred is short.</summary>
		private static void DownloadInstallPatchWorker(string downloadUri,string destinationPath,ref FormProgress progressIndicator) {
			int chunk=10;//KB
			byte[] buffer;
			int i=0;
			WebClient myWebClient=new WebClient();
			Stream readStream=myWebClient.OpenRead(downloadUri);
			BinaryReader br=new BinaryReader(readStream);
			FileStream writeStream=new FileStream(destinationPath,FileMode.Create);
			BinaryWriter bw=new BinaryWriter(writeStream);
			try {
				while(true) {
					buffer=br.ReadBytes(chunk*1024);
					if(buffer.Length==0) {
						break;
					}
					double curVal=((double)(chunk*i)+((double)buffer.Length/1024))/1024;
					progressIndicator.CurrentVal=curVal;
					bw.Write(buffer);
					i++;
				}
			}
			catch {//for instance, if abort.
				br.Close();
				bw.Close();
				File.Delete(destinationPath);
			}
			finally {
				br.Close();
				bw.Close();
			}
			//myWebClient.DownloadFile(downloadUri,ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"Setup.exe"));
		}

		///<summary><para>Returns false if not allowed. Generates error messages. Checks remoting role, and replication status.</para>
		///<para>This is different than the server side validation that happens when codes are requested.</para></summary>
		private bool IsValidUserRequest() {
			if(RemotingClient.RemotingRole==RemotingRole.ServerWeb) {
				MsgBox.Show(this,"Importing codes is only allowed from the web server");
				return false;
			}
			if(PrefC.GetString(PrefName.WebServiceServerName)!="" //using web service
				&&!ODEnvironment.IdIsThisComputer(PrefC.GetString(PrefName.WebServiceServerName).ToLower()))//and not on web server 
			{
				MessageBox.Show(Lan.g(this,"Importing codes is only allowed from the web server: ")+PrefC.GetString(PrefName.WebServiceServerName));
				return false;
			}
			if(ReplicationServers.ServerIsBlocked()) {
				MsgBox.Show(this,"Importing codes is not allowed on this replication server");
				return false;
			}
			return true;
		}

		///<summary>Throws exceptions, put in try catch block.</summary>
		private static string RequestCodeSystemsXml() {
			//No xml needed...? ----------------------------------------------------------------------------------------------------
#if DEBUG
			OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
#else
			OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
			updateService.Url=PrefC.GetString(PrefName.UpdateServerAddress);
#endif
			if(PrefC.GetString(PrefName.UpdateWebProxyAddress) !="") {
				IWebProxy proxy = new WebProxy(PrefC.GetString(PrefName.UpdateWebProxyAddress));
				ICredentials cred=new NetworkCredential(PrefC.GetString(PrefName.UpdateWebProxyUserName),PrefC.GetString(PrefName.UpdateWebProxyPassword));
				proxy.Credentials=cred;
				updateService.Proxy=proxy;
			}
			return updateService.RequestCodeSystems("");//may throw error.  No security on this webmethod.
		}

		private static string SendAndReceiveDownloadXml(string codeSystemName) {
			//prepare the xml document to send--------------------------------------------------------------------------------------
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			StringBuilder strbuild=new StringBuilder();
			using(XmlWriter writer=XmlWriter.Create(strbuild,settings)) {
				//TODO: include more user information
				writer.WriteStartElement("UpdateRequest");
				writer.WriteStartElement("RegistrationKey");
				writer.WriteString(PrefC.GetString(PrefName.RegistrationKey));
				writer.WriteEndElement();
				writer.WriteStartElement("PracticeTitle");
				writer.WriteString(PrefC.GetString(PrefName.PracticeTitle));
				writer.WriteEndElement();
				writer.WriteStartElement("PracticePhone");
				writer.WriteString(PrefC.GetString(PrefName.PracticePhone));
				writer.WriteEndElement();
				writer.WriteStartElement("ProgramVersion");
				writer.WriteString(PrefC.GetString(PrefName.ProgramVersion));
				writer.WriteEndElement();
				writer.WriteStartElement("CodeSystemRequested");
				writer.WriteString(codeSystemName);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
#if DEBUG
			OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
#else
				OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
				updateService.Url=PrefC.GetString(PrefName.UpdateServerAddress);
#endif
			if(PrefC.GetString(PrefName.UpdateWebProxyAddress) !="") {
				IWebProxy proxy = new WebProxy(PrefC.GetString(PrefName.UpdateWebProxyAddress));
				ICredentials cred=new NetworkCredential(PrefC.GetString(PrefName.UpdateWebProxyUserName),PrefC.GetString(PrefName.UpdateWebProxyPassword));
				proxy.Credentials=cred;
				updateService.Proxy=proxy;
			}
			string result="";
			try {
				result=updateService.RequestCodeSystemDownload(strbuild.ToString());//may throw error
			}
			catch(Exception ex) {
				//Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return "";
			}
			return result;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//if(MessageBox.Show("Would you like to update the selected code system :","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
			//	return;
			//}
			//Import selected code system
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}