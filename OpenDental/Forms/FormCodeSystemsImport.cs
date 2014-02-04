using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using CodeBase;
using Ionic.Zip;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCodeSystemsImport:Form {
		///<summary>All code systems available.</summary>
		private List<CodeSystem> _listCodeSystems;
		///<summary>Indicates if quarterly EHR key is valid. If true then SNOMED CT codes will be made available for download.</summary>
		private bool _isMemberNation;
		///<summary>Track current status of each code system.</summary>
		private Dictionary<string,string> _mapCodeSystemStatus=new Dictionary<string /*code system name*/,string /*status to be printed to grid*/>();
		
		public FormCodeSystemsImport() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCodeSystemsImport_Load(object sender,EventArgs e) {
			_isMemberNation=false;
			//This check is here to prevent Snomeds from being available in non-member nations.
			List<EhrQuarterlyKey> ehrKeys=EhrQuarterlyKeys.GetAllKeys();
			for(int i=0;i<ehrKeys.Count;i++) {
				if(FormEHR.QuarterlyKeyIsValid(ehrKeys[i].YearValue.ToString(),ehrKeys[i].QuarterValue.ToString(),ehrKeys[i].PracticeName,ehrKeys[i].KeyValue)) {
					_isMemberNation=true;
					break;
				}
			}
			UpdateCodeSystemThread.Finished+=new EventHandler(UpdateCodeSystemThread_FinishedSafe);
		}
		
		///<summary>If there are still import threads running then prompt the user to see if they want to abort the imports prematurely.</summary>
		private void FormCodeSystemsImport_FormClosing(object sender,FormClosingEventArgs e) {
			if(!UpdateCodeSystemThread.IsRunning) { //All done, exit.
				return;
			}
			if(MsgBox.Show("CodeSystemImporter",true,"Import in progress. Would you like to abort?")) {
				//User wants abort the threads.
				UpdateCodeSystemThread.StopAll();
				return;
			}
			//User elected to continue waiting so cancel the Close event.
			e.Cancel=true;
		}
		
		private void FillGrid() {
			_listCodeSystems=CodeSystems.GetForCurrentVersion(_isMemberNation);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Code System",200,false);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Current Version",100,false);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Available Version",100,false);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Download Status",100,false);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_listCodeSystems.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(_listCodeSystems[i].CodeSystemName);
				row.Cells.Add(_listCodeSystems[i].VersionCur);
				row.Cells.Add(_listCodeSystems[i].VersionAvail);
				//Initialize with the status which may have been set during pre-download in butDownload_Click. This cell will be updated on download progress updates.
				string status="";
				_mapCodeSystemStatus.TryGetValue(_listCodeSystems[i].CodeSystemName,out status);
				row.Cells.Add(status);
				row.Tag=_listCodeSystems[i];
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}
				
		private void butCheckUpdates_Click(object sender,EventArgs e) {
			butDownload.Enabled=false;
			try {
				string result="";
				result=RequestCodeSystemsXml();
				XmlDocument doc=new XmlDocument();
				doc.LoadXml(result);
				List<CodeSystem> listCodeSystemsAvailable=CodeSystems.GetForCurrentVersion(_isMemberNation);
				for(int i=0;i<listCodeSystemsAvailable.Count;i++) {
					string codeSystemName=listCodeSystemsAvailable[i].CodeSystemName;
					try {
						XmlNode node=doc.SelectSingleNode("//"+codeSystemName);
						if(node!=null) {
							listCodeSystemsAvailable[i].VersionAvail=node.Attributes["VersionAvailable"].InnerText;
						}
						else {
							listCodeSystemsAvailable[i].VersionAvail=@"N\A";
						}
						CodeSystems.Update(listCodeSystemsAvailable[i]);						
					}
					catch {
						//Might happen if they are running this tool without the right rows in the CodeSystem table? Maybe.
						//Don't prevent the rest of the code systems from being downloaded just because 1 failed.
						continue;
					}
				}
				FillGrid();
				//It is now safe to allow downloading.
				butDownload.Enabled=true;
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g("CodeSystemImporter","Error"+": "+ex.Message));
			}
		}

		private void butDownload_Click(object sender,EventArgs e) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {//Do not let users download code systems when using the middle tier.
				MsgBox.Show("CodeSystemImporter","Cannot download code systems when using the middle tier.");
				return;
			}
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show("CodeSystemImporter","No code systems selected.");
				return;
			}
			_mapCodeSystemStatus.Clear();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {		
				CodeSystem codeSystem=_listCodeSystems[gridMain.SelectedIndices[i]];
				try {
					//Show warnings and prompts
					if(!PreDownloadHelper(codeSystem.CodeSystemName)) {
						_mapCodeSystemStatus[codeSystem.CodeSystemName]=Lan.g("CodeSystemImporter","Import cancelled");
						continue;
					}
					//CPT codes require user to choose a local file so we will not do this on a thread.
					//We will handle the CPT import right here on the main thread before we start all other imports in parallel below.
					if(codeSystem.CodeSystemName=="CPT") {
						#region Import CPT codes
						//Default status for CPT codes. We will clear this below if the file is selected and unzipped succesfully.
						_mapCodeSystemStatus[codeSystem.CodeSystemName]=Lan.g("CodeSystemImporter","To purchase CPT 2014 codes go to https://commerce.ama-assn.org/store/");
						if(!MsgBox.Show("CodeSystemImporter",MsgBoxButtons.OKCancel,"CPT 2014 codes must be purchased from the American Medical Association seperately in the data file format and must be named \"cpt-2014-data-files-download.zip\". "
							+"More information can be found in the online manual. "
							+"If you have already purchased the code file click OK to browse to the downloaded file.")) {
							continue;
						}
						OpenFileDialog fdlg=new OpenFileDialog();
						fdlg.Title=Lan.g("CodeSystemImporter","Choose cpt-2014-data-files-download.zip CPT File");
						fdlg.InitialDirectory=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
						fdlg.Filter="zip|*.zip";
						fdlg.RestoreDirectory=true;
						fdlg.Multiselect=false;
						if(fdlg.ShowDialog()!=DialogResult.OK) {
							continue;
						}
						if(!fdlg.FileName.ToLower().Contains("cpt-2014-data-files-download.zip")) {
							_mapCodeSystemStatus[codeSystem.CodeSystemName]=Lan.g("CodeSystemImporter","Could not locate cpt-2014-data-files-download.zip in specified folder.");
							continue;
						}
						//Unzip the compressed file-----------------------------------------------------------------------------------------------------
						bool foundFile=false;
						MemoryStream ms=new MemoryStream();
						using(ZipFile unzipped=ZipFile.Read(fdlg.FileName)) {
							for(int unzipIndex=0;unzipIndex<unzipped.Count;unzipIndex++) {//unzip/write all files to the temp directory
								ZipEntry ze=unzipped[unzipIndex];
								if(!ze.FileName.ToLower().EndsWith("medu.txt.txt")) {
									continue;
								}
								ze.Extract(Path.GetTempPath(),ExtractExistingFileAction.OverwriteSilently);
								foundFile=true;
							}
						}
						if(!foundFile) {
							_mapCodeSystemStatus[codeSystem.CodeSystemName]=Lan.g("CodeSystemImporter","MEDU.txt.txt file not found in zip archive.");
							continue;
						}
						//Add a new thread. We will run these all in parallel once we have them all queued.
						//MEDU.txt.txt is not a typo. That is litterally how the resource file is realeased to the public!
						UpdateCodeSystemThread.Add(ODFileUtils.CombinePaths(Path.GetTempPath(),"MEDU.txt.txt"),_listCodeSystems[gridMain.SelectedIndices[i]],new UpdateCodeSystemThread.UpdateCodeSystemArgs(UpdateCodeSystemThread_UpdateSafe));
						//We got this far so the local file was retreived successfully. No initial status to report.
						_mapCodeSystemStatus[codeSystem.CodeSystemName]="";
						#endregion
					}
					else {
						#region Import all other codes
						//Add a new thread. We will run these all in parallel once we have them all queued.
						//This codes system file does not exist on the system so it will be downloaded before being imported.
						UpdateCodeSystemThread.Add(_listCodeSystems[gridMain.SelectedIndices[i]],new UpdateCodeSystemThread.UpdateCodeSystemArgs(UpdateCodeSystemThread_UpdateSafe));
						#endregion
					}
				}
				catch(Exception ex) {
					//Set status for this code system.
					_mapCodeSystemStatus[codeSystem.CodeSystemName]=Lan.g("CodeSystemImporter",ex.Message);
				}
			}
			//Threads are all ready to go start them all in parallel. We will re-enable these buttons when we handle the UpdateCodeSystemThread.Finished event.
			if(UpdateCodeSystemThread.StartAll()) {
				butDownload.Enabled=false;
				butCheckUpdates.Enabled=false;
			}
			FillGrid();
		}

		///<summary>Returns a list of available code systems.  Throws exceptions, put in try catch block.</summary>
		private static string RequestCodeSystemsXml() {
#if DEBUG
			OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
#else
			OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
			updateService.Url=PrefC.GetString(PrefName.UpdateServerAddress);
#endif
			if(PrefC.GetString(PrefName.UpdateWebProxyAddress) !="") {
				IWebProxy proxy=new WebProxy(PrefC.GetString(PrefName.UpdateWebProxyAddress));
				ICredentials cred=new NetworkCredential(PrefC.GetString(PrefName.UpdateWebProxyUserName),PrefC.GetString(PrefName.UpdateWebProxyPassword));
				proxy.Credentials=cred;
				updateService.Proxy=proxy;
			}
			return updateService.RequestCodeSystems("");//may throw error.  No security on this webmethod.
		}

		///<summary>Used to show EULA or other pre-download actions.  Displays message boxes. Returns false if pre-download checks not satisfied.</summary>
		private bool PreDownloadHelper(string codeSystemName) {
			string programVersion=PrefC.GetString(PrefName.ProgramVersion);
			switch(codeSystemName) {
				//Code system specific pre-download actions.
				case "SNOMEDCT":
					#region SNOMEDCT EULA
					string EULA=@"Open Dental "+programVersion+@" includes SNOMED Clinical Terms® (SNOMED CT®) which is used by permission of the International Health Terminology Standards Development Organization (IHTSDO). All rights reserved. SNOMED CT® was originally created by the College of American Pathologists. “SNOMED”, “SNOMED CT” and “SNOMED Clinical Terms” are registered trademarks of the IHTSDO (www.ihtsdo.org).
Use of SNOMED CT in Open Dental "+programVersion+@" is governed by the conditions of the following SNOMED CT Sub-license issued by Open Dental Software Inc.
1. The meaning of the terms “Affiliate”, or “Data Analysis System”, “Data Creation System”, “Derivative”, “End User”, “Extension”, “Member”, “Non-Member Territory”, “SNOMED CT” and “SNOMED CT Content” are as defined in the IHTSDO Affiliate License Agreement (see www.ihtsdo.org/license.pdf).
2. Information about Affiliate Licensing is available at www.ihtsdo.org/license. Individuals or organizations wishing to register as IHTSDO Affiliates can register at www.ihtsdo.org/salsa, subject to acceptance of the Affiliate License Agreement (see www.ihtsdo.org/license.pdf).
3. The current list of IHTSDO Member Territories can be viewed at www.ihtsdo.org/members. Countries not included in that list are “Non-Member Territories”.
4. End Users, that do not hold an IHTSDO Affiliate License, may access SNOMED CT® using Open Dental subject to acceptance of and adherence to the following sub-license limitations:
  a) The sub-licensee is only permitted to access SNOMED CT® using this software (or service) for the purpose of exploring and evaluating the terminology.
  b) The sub-licensee is not permitted the use of this software as part of a system that constitutes a SNOMED CT “Data Creation System” or “Data Analysis System”, as defined in the IHTSDO Affiliate License. This means that the sub-licensee must not use Open Dental "+programVersion+@" to add or copy SNOMED CT identifiers into any type of record system, database or document.
  c) The sub-licensee is not permitted to translate or modify SNOMED CT Content or Derivatives.
  d) The sub-licensee is not permitted to distribute or share SNOMED CT Content or Derivatives.
5. IHTSDO Affiliates may use Open Dental "+programVersion+@" as part of a “Data Creation System” or “Data Analysis System” subject to the following conditions:
  a) The IHTSDO Affiliate, using Open Dental "+programVersion+@" must accept full responsibility for any reporting and fees due for use or deployment of such a system in a Non-Member Territory.
  b) The IHTSDO Affiliate must not use Open Dental "+programVersion+@" to access or interact with SNOMED CT in any way that is not permitted by the Affiliate License Agreement.
  c) In the event of termination of the Affiliate License Agreement, the use of Open Dental "+programVersion+@" will be subject to the End User limitations noted in 4.";
					#endregion
					MsgBoxCopyPaste FormMBCP=new MsgBoxCopyPaste(EULA);
					FormMBCP.ShowDialog();
					if(FormMBCP.DialogResult!=DialogResult.OK) {
						MsgBox.Show("CodeSystemImporter","SNOMED CT codes will not be imported.");
						return false;//next selected index
					}
					break;
				case "LOINC"://Main + third party
					//TODO
					break;
				case "UCUM":
					//TODO
					break;
			}
			return true;
		}

		#region thread handlers
		///<summary>Call this from external thread. Invokes to main thread to avoid cross-thread collision.</summary>
		private void UpdateCodeSystemThread_FinishedSafe(object sender,EventArgs e) {
			try {
				this.BeginInvoke(new EventHandler(UpdateCodeSystemThread_FinishedUnsafe),new object[] { sender,e });				
			}
			//most likely because form is no longer available to invoke to
			catch { }
		}

		///<summary>Do not call this directly from external thread. Use UpdateCodeSystemThread_FinishedSafe.</summary>
		private void UpdateCodeSystemThread_FinishedUnsafe(object sender,EventArgs e) {
				butCheckUpdates.Enabled=true;
				butDownload.Enabled=true;
		}

		///<summary>Call this from external thread. Invokes to main thread to avoid cross-thread collision.</summary>
		private void UpdateCodeSystemThread_UpdateSafe(CodeSystem codeSystem,string status,double percentDone,bool done,bool success) {
			try {
				this.BeginInvoke(new UpdateCodeSystemThread.UpdateCodeSystemArgs(UpdateCodeSystemThread_UpdateUnsafe),new object[] { codeSystem,status,percentDone,done,success });
			}
			//most likely because form is no longer available to invoke to
			catch { }			
		}

		///<summary>Do not call this directly from external thread. Use UpdateCodeSystemThread_UpdateSafe.</summary>
		private void UpdateCodeSystemThread_UpdateUnsafe(CodeSystem codeSystem,string status,double percentDone,bool done,bool success) {
			//This is called a lot from the import threads so don't bother with the full FillGrid. Just find our row and column and update the cell's text.
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(gridMain.Rows[i].Tag==null 
					|| !(gridMain.Rows[i].Tag is CodeSystem)
					|| !(((CodeSystem)gridMain.Rows[i].Tag).CodeSystemName==codeSystem.CodeSystemName)) {
					continue;
				}
				string cellText=((int)percentDone)+"%"+" -- "+status;
				if(done) {
					if(success) {
						cellText=Lan.g("CodeSystemImporter","Import complete")+"!";
					}
					else {
						cellText=Lan.g("CodeSystemImporter","Import failed")+"! -- "+status;
					}
				}
				gridMain.Rows[i].Cells[3].Text=cellText;
			}
			gridMain.Invalidate();
		}
		#endregion

		///<summary>Worker thread class. 1 thread will be spawned for each code sytem being downloaded. All threads will run in parallel.</summary>
		private class UpdateCodeSystemThread {
			///<summary>Number of bytes in a kilobyte.</summary>
			private const int KB_SIZE=1024;
			///<summary>Number of kilobytes to download in each chunk.</summary>
			private const int CHUNK_SIZE=10;
			///<summary>Static lis of threads. All managed internally. Must always be locked by _lock when accessed!!!</summary>
			private static List<UpdateCodeSystemThread> _threads=new List<UpdateCodeSystemThread>();
			///<summary>All access of _threads member MUST BE enclosed with lock statment in order to prevent thread-lock and race conditions.</summary>
			private static object _lock=new object();
			///<summary>The code system being updated.</summary>
			private CodeSystem _codeSystem;			
			///<summary>Download and import functions will check this flag occasionally to see if they should abort prematurely.</summary>
			private bool _quit=false;
			///<summary>Function signature required to send an update.</summary>			
			public delegate void UpdateCodeSystemArgs(CodeSystem codeSystem,string status,double percentDone,bool done,bool success);			
			///<summary>Required by ctor. Used to keep main thread aware of update progress.</summary>			
			private UpdateCodeSystemArgs _updateHandler;
			///<summary>Event will be fired when the final thread has finished and all threads have been cleared from the list.</summary>
			public static EventHandler Finished;
			///<summary>If this is a CPT import then the file must exist localally and the file location will be provided by the user. All other code system files are held behind the Customer Update web service and will be downloaded to a temp file location in order to be imported.</summary>
			private string _localFilePath;

			///<summary>Aborts the thread. Only called by StopAll.</summary>
			private void Quit() {
				_quit=true;
			}

			///<summary>Indicates if there are still 1 or more active threads.</summary>
			public static bool IsRunning {
				get {
					lock(_lock){
						return _threads.Count>=1;
					}					
				}
			}

			///<summary>Private ctor. Will only be used internally by Add. If localFilePath is set here then it is assumed that the file exists locally and file download will be skipped before importing data from the file. This will only happen for the CPT code system.</summary>
			private UpdateCodeSystemThread(string localFilePath,CodeSystem codeSystem,UpdateCodeSystemArgs onUpdateHandler) {
				_localFilePath=localFilePath;
				_codeSystem=codeSystem;
				_updateHandler+=onUpdateHandler;
			}
			
			///<summary>Provide a nice ledgible identifier.</summary>
			public override string ToString() {
				return _codeSystem.CodeSystemName;
			}

			///<summary>Thread list manager needs this to remove threads. Required for List.Contains.</summary>
			public override bool Equals(object obj) {
				return ((UpdateCodeSystemThread)obj)._codeSystem.CodeSystemNum==_codeSystem.CodeSystemNum;
			}

			///<summary>Add a thread to the queue. These threads will not be started until StartAll is called subsequent to adding all necessary threads. If localFilePath is set here then it is assumed that the file exists locally and file download will be skipped before importing data from the file. This will only happen for the CPT code system.</summary>
			public static void Add(string localFilePath,CodeSystem codeSystem,UpdateCodeSystemArgs onUpdateHandler) {
				UpdateCodeSystemThread thread=new UpdateCodeSystemThread(localFilePath,codeSystem,onUpdateHandler);
				lock(_lock) {
					_threads.Add(thread);
				}
			}

			///<summary>Add a thread to the queue. These threads will not be started until StartAll is called subsequent to adding all necessary threads. This version assures that code system file will be downloaded before import. Use for all code system except CPT.</summary>
			public static void Add(CodeSystem codeSystem,UpdateCodeSystemArgs onUpdateHandler) {
				Add("",codeSystem,onUpdateHandler);
			}

			///<summary>Use this to start the threads once all threads have been added using Add.</summary>
			public static bool StartAll() {				
				bool startedAtLeastOne=false;
				lock(_lock) {
					foreach(UpdateCodeSystemThread thread in _threads) {
						Thread th=new Thread(new ThreadStart(thread.Run));
						th.Name=thread.ToString();
						th.Start();
						startedAtLeastOne=true;
					}
				}
				return startedAtLeastOne;
			}

			///<summary>Sets the Quit flag for all threads. Use this if early abort is desired.</summary>
			public static void StopAll() {
				lock(_lock) {
					foreach(UpdateCodeSystemThread thread in _threads) {
						thread.Quit();
					}
					_threads.Clear();
				}
			}

			///<summary>Called internally each time time a thread has completed. Will trigger the Finished event if this is the last thread to complete.</summary>
			private void Done(string status,bool success) {
				_updateHandler(_codeSystem,status,100,true,success);
				bool finished=false;
				lock(_lock) {
					if(_threads.Contains(this)) {
						_threads.Remove(this);
					}
					finished=_threads.Count<=0;
				}
				if(finished && Finished!=null) {
					Finished("UpdateCodeSystemThread",new EventArgs());
				}
			}

			///<summary>Update the current status of this import thread. Thread owner is required to handle this as the delegat is required in the ctor.</summary>
			private void Update(string status,int numDone,int numTotal) {
				double percentDone=0;
				//Guard against illegal division.
				if(numTotal>0) {
					percentDone=100*(numDone/(double)numTotal);
				}
				_updateHandler(_codeSystem,status,percentDone,false,true);
			}

			///<summary>Helper used internally.</summary>
			private void ImportProgress(int numDone,int numTotal) {
				Update(Lan.g("CodeSystemImporter","Importing"),numDone,numTotal);
			}

			///<summary>Helper used internally.</summary>
			private void DownloadProgress(int numDone,int numTotal) {
				Update(Lan.g("CodeSystemImporter","Downloading"),numDone,numTotal);
			}

			///<summary>The thread function.</summary>
			private void Run() {
				try {
					string failText="";
					if(!RequestCodeSystemDownloadHelper(ref failText)) {
						throw new Exception(failText);						
					}
					//set current version=available version
					CodeSystems.UpdateCurrentVersion(_codeSystem);
					//All good!
					Done(Lan.g("CodeSystemImporter","Import Complete"),true);
				}
				catch(Exception ex) {
					//Something failed!
					Done(Lan.g("CodeSystemImporter","Error")+": "+ex.Message,false);
				}
			}

			///<summary>Will request, download, and import codeSystem from webservice. Returns false if unsuccessful.</summary>
			private bool RequestCodeSystemDownloadHelper(ref string failText) {
				try {
					//If local file was not provided then try to download it from Customer Update web service. 
					//Local file will only be provided for CPT code system.
					if(string.IsNullOrEmpty(_localFilePath)) { 
						string result=SendAndReceiveDownloadXml(_codeSystem.CodeSystemName);
						XmlDocument doc=new XmlDocument();
						doc.LoadXml(result);
						XmlNode node=doc.SelectSingleNode("//Error");
						if(node!=null) {
							throw new Exception(node.InnerText);
						}
						node=doc.SelectSingleNode("//CodeSystemURL");
						if(node==null) {
							throw new Exception(Lan.g("CodeSystemImporter","Code System URL is empty for ")+": "+_codeSystem.CodeSystemName);
						}
						//Node's inner text contains the URL
						_localFilePath=DownloadFileHelper(node.InnerText);					
					}
					if(!File.Exists(_localFilePath)) {
						throw new Exception(Lan.g("CodeSystemImporter","Local file not found ")+": "+_localFilePath);
					}
					switch(_codeSystem.CodeSystemName) {
						case "CDCREC":
							CodeSystems.ImportCdcrec(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "CVX":
							CodeSystems.ImportCvx(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "HCPCS":
							CodeSystems.ImportHcpcs(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "ICD10CM":
							CodeSystems.ImportIcd10(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "ICD9CM":
							CodeSystems.ImportIcd9(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "LOINC":
							CodeSystems.ImportLoinc(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "RXNORM":
							CodeSystems.ImportRxNorm(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "SNOMEDCT":
							CodeSystems.ImportSnomed(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "SOP":
							CodeSystems.ImportSop(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "UCUM":
							CodeSystems.ImportUcum(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "CPT":
							CodeSystems.ImportCpt(_localFilePath,new CodeSystems.ProgressArgs(ImportProgress),ref _quit);
							break;
						case "CDT":  //import not supported
						case "AdministrativeSex":  //import not supported
						default:  //new code system perhaps?
							throw new Exception(Lan.g("CodeSystemImporter","Unsupported Code System")+": "+_codeSystem.CodeSystemName);
					}
					//Import succeded so delete the import file where necessary.
					DeleteImportFileIfNecessary();
					//We got here so everything succeeded.
					return true;
				}
				catch(Exception ex) {
					failText=ex.Message;
				}
				//We got here so something failed.
				return false;
			}

			///<summary>Delete the import file which was created locally. This file was either downloaded or extracted from a zip archive. Either way it is temporary and can be deleted.</summary>
			private void DeleteImportFileIfNecessary() {
				//Don't bother if the file isn't there.
				if(!File.Exists(_localFilePath)) {
					return;
				}				
				//We got this far so assume the file is safe to delete.
				File.Delete(_localFilePath);
			}

			///<summary>Returns temp file name used to download file.  Can throw exception.</summary>
			private string DownloadFileHelper(string codeSystemURL) {
				string zipFileDestination=Path.GetTempFileName();
				//Cleanup existing.
				File.Delete(zipFileDestination);
				try {
					//Perform the download
					DownloadFileWorker(codeSystemURL,zipFileDestination);
					Thread.Sleep(100);//allow file to be released for use by the unzipper.
					//Unzip the compressed file-----------------------------------------------------------------------------------------------------
					using(MemoryStream ms=new MemoryStream())
					using(ZipFile unzipped=ZipFile.Read(zipFileDestination)) {
						ZipEntry ze=unzipped[0];
						ze.Extract(Path.GetTempPath(),ExtractExistingFileAction.OverwriteSilently);
						return Path.GetTempPath()+unzipped[0].FileName;
					}
				}
				finally{
					//We are done with the zip file.
					File.Delete(zipFileDestination);
				}
			}

			///<summary>Download given URI to given local path. Can throw exception.</summary>
			private void DownloadFileWorker(string codeSystemURL,string destinationPath) {
				byte[] buffer;
				int chunkIndex=0;
				WebRequest wr=WebRequest.Create(codeSystemURL);
				int fileSize=0;
				using(WebResponse webResp=wr.GetResponse()) { //Quickly get the size of the entire package to be downloaded.
					fileSize=(int)webResp.ContentLength;
				}
				using(WebClient myWebClient=new WebClient())
				using(Stream readStream=myWebClient.OpenRead(codeSystemURL))
				using(BinaryReader br=new BinaryReader(readStream))
				using(FileStream writeStream=new FileStream(destinationPath,FileMode.Create))
				using(BinaryWriter bw=new BinaryWriter(writeStream)) {
					while(true) {
						if(_quit) {
							throw new Exception(Lan.g("CodeSystemImporter","Download aborted"));
						}
						//Update the progress.
						DownloadProgress(CHUNK_SIZE*KB_SIZE*chunkIndex,fileSize);
						//Download another chunk.
						buffer=br.ReadBytes(CHUNK_SIZE*KB_SIZE);
						if(buffer.Length==0) { //Nothing left to download so we are done.
							break;
						}
						//Write out to the file.
						bw.Write(buffer);
						chunkIndex++;
					}
				}
			}

			///<summary>Can throw exception.</summary>
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
					writer.WriteStartElement("PracticeAddress");
					writer.WriteString(PrefC.GetString(PrefName.PracticeAddress));
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
				//may throw error
				return updateService.RequestCodeSystemDownload(strbuild.ToString());
			}
		}
		
		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}		
	}
}