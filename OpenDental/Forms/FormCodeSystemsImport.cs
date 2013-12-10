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
using System.Diagnostics;
using Ionic.Zip;

namespace OpenDental {
	public partial class FormCodeSystemsImport:Form {
		///<summary>used to populate lists</summary>
		private List<CodeSystem> ListCodeSystems;

		public FormCodeSystemsImport() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCodeSystemsImport_Load(object sender,EventArgs e) {
			if(FormEHR.QuarterlyKeyIsValid(DateTime.Now.ToString("yy")//year
				,Math.Ceiling(DateTime.Now.Month/3f).ToString()//quarter
				,PrefC.GetString(PrefName.PracticeTitle)//practice title
				,EhrQuarterlyKeys.GetKeyThisQuarter().KeyValue))//key
			{
				ListCodeSystems=CodeSystems.GetForCurrentVersion();//EHR enabled and valid.
			}
			else {//No EHR
				ListCodeSystems=CodeSystems.GetForCurrentVersionNoSnomed();
			}
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
			butDownload.Visible=true;
		}

		private void butDownload_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"No code systems selected.");
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				Cursor=Cursors.WaitCursor;//Within loop because requestCodeSystemDownloadHelper() causes the Cursor to go back to Default sometimes.
				if(!PreDownloadHelper(ListCodeSystems[gridMain.SelectedIndices[i]].CodeSystemName)){
					continue;
				}
				#region import CPT codes
				if(ListCodeSystems[gridMain.SelectedIndices[i]].CodeSystemName=="CPT") {
					try {
						importCPTHelper();
						CodeSystems.UpdateCurrentVersion(ListCodeSystems[gridMain.SelectedIndices[i]]);//set current version=available version
						MsgBox.Show(this,"Cpt codes imported successfully.");
					}
					catch (Exception ex){
						Cursor=Cursors.Default;
						MessageBox.Show(this,"CPT codes have not been imported:\r\n"+ex.Message);
					}
					continue;
				}
				#endregion
				try {
					Thread.Sleep(1000);
					if(requestCodeSystemDownloadHelper(ListCodeSystems[gridMain.SelectedIndices[i]].CodeSystemName)) {//can throw exceptions
						CodeSystems.UpdateCurrentVersion(ListCodeSystems[gridMain.SelectedIndices[i]]);//set current version=available version
					}
				}
				catch(Exception ex) {
					Cursor=Cursors.Default;//Just in case.
					MessageBox.Show(Lan.g(this,"Error encounter while importing code system")+":"+ListCodeSystems[gridMain.SelectedIndices[i]].CodeSystemName+"\r\n"+ex.Message);
					continue;
				}
			}
			FillGrid();
			Cursor=Cursors.Default;
		}

		///<summary>Surround with try/catch.  Launches all neccesary dialogs and throws exceptions.</summary>
		private void importCPTHelper() {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"CPT 2014 codes must be purchased from the American Medical Association seperately in the data file format and must be "
				+"named \"cpt-2014-data-files-download.zip\". More information can be found in the online manual. If you have already purchased the code file select continue "
				+"to browse to the downloaded file.")) 
			{
					throw new Exception("To purchase CPT 2014 codes go to https://commerce.ama-assn.org/store/");
			}
			FolderBrowserDialog fbd=new FolderBrowserDialog();
			if(fbd.ShowDialog()!=DialogResult.OK) {
				return;
			}
			if(!File.Exists(fbd.SelectedPath+"\\cpt-2014-data-files-download.zip")) {
				throw new Exception("Could not locate cpt-2014-data-files-download.zip in specified folder.");
			}
			Cursor=Cursors.WaitCursor;
			//Unzip the compressed file-----------------------------------------------------------------------------------------------------
			MemoryStream ms=new MemoryStream();
			using(ZipFile unzipped=ZipFile.Read(fbd.SelectedPath+"\\cpt-2014-data-files-download.zip")) {
				for(int i=0;i<unzipped.Count;i++) {//unzip/write all files to the temp directory
					ZipEntry ze=unzipped[i];
					ze.Extract(Path.GetTempPath()+"CPT\\",ExtractExistingFileAction.OverwriteSilently);
				}
				//return Path.GetTempPath()+"CPT\\MEDU.txt.txt";
			}
			CodeSystems.ImportCpt(Path.GetTempPath()+"CPT\\MEDU.txt.txt");//MEDU.txt.txt is not a typo. That is litterally how the resource file is realeased to the public!
			Cursor=Cursors.Default;
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
4. End Users, that do not hold an IHTSDO Affiliate License, may access SNOMED CT® using [ProductName] subject to acceptance of and adherence to the following sub-license limitations:
  a) The sub-licensee is only permitted to access SNOMED CT® using this software (or service) for the purpose of exploring and evaluating the terminology.
  b) The sub-licensee is not permitted the use of this software as part of a system that constitutes a SNOMED CT “Data Creation System” or “Data Analysis System”, as defined in the IHTSDO Affiliate License. This means that the sub-licensee must not use Open Dental "+programVersion+@" to add or copy SNOMED CT identifiers into any type of record system, database or document.
  c) The sub-licensee is not permitted to translate or modify SNOMED CT Content or Derivatives.
  d) The sub-licensee is not permitted to distribute or share SNOMED CT Content or Derivatives.
5. IHTSDO Affiliates may use Open Dental "+programVersion+@" as part of a “Data Creation System” or “Data Analysis System” subject to the following conditions:
  a) The IHTSDO Affiliate, using Open Dental "+programVersion+@" must accept full responsibility for any reporting and fees due for use or deployment of such a system in a Non-Member Territory.
  b) The IHTSDO Affiliate must not use Open Dental "+programVersion+@" to access or interact with SNOMED CT in any way that is not permitted by the Affiliate License Agreement.
  c) In the event of termination of the Affiliate License Agreement, the use of Open Dental "+programVersion+@" will be 
subject to the End User limitations noted in 4.";
					#endregion
					MsgBoxCopyPaste FormMBCP=new MsgBoxCopyPaste(EULA);
					FormMBCP.ShowDialog();
					if(FormMBCP.DialogResult!=DialogResult.OK) {
						MsgBox.Show(this,"SNOMED CT codes will not be imported.");
						return false;//next selected index
					}
					break;
			}
			return true;
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
			Thread.Sleep(2000);//wait 2 seconds between downloads.
			string tempFile="";
			try {
				tempFile=downloadFileHelper(codeSystemURL,codeSystemName);//shows progress bar.
			}
			catch(Exception ex) {
				throw ex;
			}
			try {//moved try/catch outside of switch statement to make code more readable, functions the same.
				//The cursor gets set back to default within downloadFileHelper().  Put it back to waiting cursor when importing the codes.  They could take a while.
				Cursor=Cursors.WaitCursor;
				switch(codeSystemName) {
					case "AdministrativeSex":
						//should never happen
						return false;
					case "CDCREC":
						CodeSystems.ImportCdcrec(tempFile);
						MsgBox.Show(this,"CDCREC codes imported successfully.");
						break;
					case "CDT":
						//should never happen, Handled in the code above.
						return false;
					case "CPT":
						//Handled slightly differenly before getting here. User must provide resource file using file picker, which will be pre-processed elsewhere.
						CodeSystems.ImportCpt(tempFile);
						MsgBox.Show(this,"CPT codes imported successfully.");
						break;
					case "CVX":
						CodeSystems.ImportCvx(tempFile);
						MsgBox.Show(this,"CVX codes imported successfully.");
						break;
					case "HCPCS":
						CodeSystems.ImportHcpcs(tempFile);
						MsgBox.Show(this,"HCPCS codes imported successfully.");
						break;
					case "ICD10CM":
						CodeSystems.ImportIcd10(tempFile);
						MsgBox.Show(this,"ICD10CM codes imported successfully.");
						break;
					case "ICD9CM":
						CodeSystems.ImportIcd9(tempFile);
						MsgBox.Show(this,"ICD9CM codes imported successfully.");
						break;
					case "LOINC":
						CodeSystems.ImportLoinc(tempFile);
						MsgBox.Show(this,"LOINC codes imported successfully.");
						break;
					case "RXNORM":
						CodeSystems.ImportRxNorm(tempFile);
						MsgBox.Show(this,"RXNORM codes imported successfully.");
						break;
					case "SNOMEDCT":
						CodeSystems.ImportSnomed(tempFile);
						MsgBox.Show(this,"SNOMED CT codes imported successfully.");
						break;
					case "SOP":
						CodeSystems.ImportSop(tempFile);
						MsgBox.Show(this,"SOP codes imported successfully.");
						break;
					case "UCUM":
						CodeSystems.ImportUcum(tempFile);
						MsgBox.Show(this,"UCUM codes imported successfully.");
						break;
					default:
						//should never happen
						return false;
				}
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(this,ex.Message);
				return false;
			}
			return true;
		}

		///<summary>Returns temp file name used to download file.  Returns null if the file was not downloaded.  Throws errors if problem unzipping.</summary>
		/// <param name="codeSystemURL">Passed to download thread to begin downloading target file.</param>
		/// <param name="codeSystemName">Used for display purposes only.</param>
		/// <returns></returns>
		private static string downloadFileHelper(string codeSystemURL,string codeSystemName) {
			string zipFileDestination=Path.GetTempFileName();//@"c:\users\ryan\desktop\"+codeSystemName+".txt";
			File.Delete(zipFileDestination);
			WebRequest wr=WebRequest.Create(codeSystemURL);
			WebResponse webResp=null;
			try {
				webResp=wr.GetResponse();
			}
			catch(Exception ex) {
				CodeBase.MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(ex.Message+"\r\nUri: "+codeSystemURL);
				msgbox.ShowDialog();
				return null;
			}
			int fileSize=(int)webResp.ContentLength/1024;//KB
			FormProgress FormP=new FormProgress();
			//display the progress dialog to the user:
			FormP.MaxVal=(double)fileSize/1024;//MB
			FormP.NumberMultiplication=100;
			FormP.DisplayText="?currentVal MB of ?maxVal MB copied";
			FormP.NumberFormat="F";
			FormP.TickMS=10;
			if(codeSystemName!=""){
				FormP.Text=codeSystemName;
			}
			//FormP.Text=codeSystemURL.
			//FormP.MaxVal=fileSize;//to keep the form from closing until the real MaxVal is set.
			//FormP.NumberMultiplication=1;
			//FormP.DisplayText="Preparing records for upload.";
			//FormP.NumberFormat="F0";
			//start the thread that will perform the download
			System.Threading.ThreadStart downloadDelegate= delegate { DownloadFileWorker(codeSystemURL,zipFileDestination,ref FormP); };
			Thread workerThread=new System.Threading.Thread(downloadDelegate);
			workerThread.Start();
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel) {
				workerThread.Abort();
				return null;
			}
			FormP.Dispose();
			Application.DoEvents();//allow progress window to go away when download is complete.
			Thread.Sleep(100);//allow file to be released for use by the unzipper.
			//Unzip the compressed file-----------------------------------------------------------------------------------------------------
			MemoryStream ms=new MemoryStream();
			using(ZipFile unzipped=ZipFile.Read(zipFileDestination)) {
				ZipEntry ze=unzipped[0];
				ze.Extract(Path.GetTempPath(),ExtractExistingFileAction.OverwriteSilently);
				return Path.GetTempPath()+unzipped[0].FileName;
			}
		}

		///<summary>This is the function that the worker thread uses to actually perform the download.  Can also call this method in the ordinary way if the file to be transferred is short.</summary>
		private static void DownloadFileWorker(string downloadUri,string destinationPath,ref FormProgress progressIndicator) {
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

		///<summary>Returns a list of available code systems.  Throws exceptions, put in try catch block.</summary>
		private static string RequestCodeSystemsXml() {
			//No xml needed...? ----------------------------------------------------------------------------------------------------
#if DEBUG
			OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
			//OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
			//updateService.Url=PrefC.GetString(PrefName.UpdateServerAddress);
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

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}