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
using Ionic.Zip;
using System.Diagnostics;

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
			Cursor=Cursors.WaitCursor;
			Application.DoEvents();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(!PreDownloadHelper(ListCodeSystems[gridMain.SelectedIndices[i]].CodeSystemName)){
					continue;
				}
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

		///<summary>Used to show EULA or other pre-download actions.  Displays message boxes. Returns false if pre-download checks not satisfied.</summary>
		private bool PreDownloadHelper(string codeSystemName) {
			switch(codeSystemName) {
				//Code system specific pre-download actions.
				case "SNOMEDCT":
					#region SNOMEDCT EULA
					//TODO: make better UI.
					if(MessageBox.Show(@"//TODO: make better UI. Open Dental "+PrefC.GetString(PrefName.ProgramVersion)+@" includes SNOMED Clinical Terms® (SNOMED CT®) which is used by permission of the International Health Terminology Standards Development Organization (IHTSDO). All rights reserved. SNOMED CT® was originally created by the College of American Pathologists. “SNOMED”, “SNOMED CT” and “SNOMED Clinical Terms” are registered trademarks of the IHTSDO (www.ihtsdo.org).\r\n"
							+@"Use of SNOMED CT in Open Dental "+PrefC.GetString(PrefName.ProgramVersion)+@" is governed by the conditions of the following SNOMED CT Sub-license issued by Open Dental Software Inc.\r\n"
							+@"1. The meaning of the terms “Affiliate”, or “Data Analysis System”, “Data Creation System”, “Derivative”, “End User”, “Extension”, “Member”, “Non-Member Territory”, “SNOMED CT” and “SNOMED CT Content” are as defined in the IHTSDO Affiliate License Agreement (see www.ihtsdo.org/license.pdf).\r\n"
							+@"2. Information about Affiliate Licensing is available at www.ihtsdo.org/license. Individuals or organizations wishing to register as IHTSDO Affiliates can register at www.ihtsdo.org/salsa, subject to acceptance of the Affiliate License Agreement (see www.ihtsdo.org/license.pdf).\r\n"
							+@"3. The current list of IHTSDO Member Territories can be viewed at www.ihtsdo.org/members. Countries not included in that list are “Non-Member Territories”.\r\n"
							+@"4. End Users, that do not hold an IHTSDO Affiliate License, may access SNOMED CT® using [ProductName] subject to acceptance of and adherence to the following sub-license limitations:\r\n"
							+@"a) The sub-licensee is only permitted to access SNOMED CT® using this software (or service) for the purpose of exploring and evaluating the terminology.\r\n"
							+@"b) The sub-licensee is not permitted the use of this software as part of a system that constitutes a SNOMED CT “Data Creation System” or “Data Analysis System”, as defined in the IHTSDO Affiliate License. This means that the sub-licensee must not use Open Dental "+PrefC.GetString(PrefName.ProgramVersion)+@" to add or copy SNOMED CT identifiers into any type of record system, database or document.\r\n"
							+@"c) The sub-licensee is not permitted to translate or modify SNOMED CT Content or Derivatives.\r\n"
							+@"d) The sub-licensee is not permitted to distribute or share SNOMED CT Content or Derivatives.\r\n"
							+@"5. IHTSDO Affiliates may use Open Dental "+PrefC.GetString(PrefName.ProgramVersion)+@" as part of a “Data Creation System” or “Data Analysis System” subject to the following conditions:\r\n"
							+@"a) The IHTSDO Affiliate, using Open Dental "+PrefC.GetString(PrefName.ProgramVersion)+@" must accept full responsibility for any reporting and fees due for use or deployment of such a system in a Non-Member Territory.\r\n"
							+@"b) The IHTSDO Affiliate must not use Open Dental "+PrefC.GetString(PrefName.ProgramVersion)+@" to access or interact with SNOMED CT in any way that is not permitted by the Affiliate License Agreement.\r\n"
							+@"c) In the event of termination of the Affiliate License Agreement, the use of Open Dental "+PrefC.GetString(PrefName.ProgramVersion)+@" will be 
subject to the End User limitations noted in 4.","SNOMED CT sub-license End User Licence Agreement",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
						MsgBox.Show(this,"SNOMED CT codes will not be imported.");
					#endregion
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
				//throw new Exception(node.InnerText);
			}
			node=doc.SelectSingleNode("//CodeSystemURL");
			string codeSystemURL="";
			if(node!=null){
				codeSystemURL=node.InnerText;
			}
			//Download File to local machine
			string tempFile=downloadFileHelper("http://localhost/codesystems/SNOMEDCT.zip");//codeSystemURL);//shows progress bar.
			Stopwatch sw = new Stopwatch();
			sw.Start();
			try {//moved try/catch outside of switch statement to make code more readable, functions the same.
				switch(codeSystemName) {
					case "AdministrativeSex":
						//should never happen
						return false;
					case "CDCREC":
						CodeSystems.ImportCdcrec(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"CDCREC codes imported successfully.");
						break;
					case "CDT":
						//should never happen
						return false;
					case "CPT":
						//Handled slightly differenly before getting here. User must provide resource file using file picker, which will be pre-processed elsewhere.
						CodeSystems.ImportCpt(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"CPT codes imported successfully.");
						break;
					case "CVX":
						CodeSystems.ImportCvx(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"CVX codes imported successfully.");
						break;
					case "HCPCS":
						CodeSystems.ImportHcpcs(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"HCPCS codes imported successfully.");
						break;
					case "ICD10CM":
						CodeSystems.ImportIcd10(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"ICD10CM codes imported successfully.");
						break;
					case "ICD9CM":
						CodeSystems.ImportIcd9(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"ICD9CM codes imported successfully.");
						break;
					case "LOINC":
						CodeSystems.ImportLoinc(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"LOINC codes imported successfully.");
						break;
					case "RXNORM":
						CodeSystems.ImportRxNorm(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"RXNORM codes imported successfully.");
						break;
					case "SNOMEDCT":
						CodeSystems.ImportSnomed(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"SNOMED CT codes imported successfully.");
						break;
					case "SOP":
						CodeSystems.ImportSop(tempFile);
						sw.Stop();
						MessageBox.Show("Seconds elapsed:"+sw.ElapsedMilliseconds/1000);
						MsgBox.Show(this,"SOP codes imported successfully.");
						break;
					default:
						//should never happen
						return false;
				}
			}
			catch(Exception ex) {
				MessageBox.Show(this,ex.Message);
				return false;
			}
			return true;
		}

		///<summary>Returns temp file name used to download file.  Returns null if the file was not downloaded.  Throws errors if problem unzipping.</summary>
		private static string downloadFileHelper(string codeSystemURL) {
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
			int fileSize=(int)webResp.ContentLength/1024;
			FormProgress FormP=new FormProgress();
			//start the thread that will perform the download
			System.Threading.ThreadStart downloadDelegate= delegate { DownloadFileWorker(codeSystemURL,zipFileDestination,ref FormP); };
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
				return null;
			}
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

		/////<summary><para>Returns false if not allowed. Generates error messages. Checks remoting role, and replication status.</para>
		/////<para>This is different than the server side validation that happens when codes are requested.</para></summary>
		//private bool IsValidUserRequest() {
		//	return true;//no longer needed.
		//	if(RemotingClient.RemotingRole==RemotingRole.ServerWeb) {
		//		MsgBox.Show(this,"Importing codes is only allowed from the web server");
		//		return false;
		//	}
		//	if(PrefC.GetString(PrefName.WebServiceServerName)!="" //using web service
		//		&&!ODEnvironment.IdIsThisComputer(PrefC.GetString(PrefName.WebServiceServerName).ToLower()))//and not on web server 
		//	{
		//		MessageBox.Show(Lan.g(this,"Importing codes is only allowed from the web server: ")+PrefC.GetString(PrefName.WebServiceServerName));
		//		return false;
		//	}
		//	if(ReplicationServers.ServerIsBlocked()) {
		//		MsgBox.Show(this,"Importing codes is not allowed on this replication server");
		//		return false;
		//	}
		//	return true;
		//}

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

		private void butHCPCS_Click(object sender,EventArgs e) {
			return;
			string command="DROP TABLE IF EXISTS tempLoincImport";
			DataCore.NonQ(command);
			command=@"CREATE TABLE tempLoincImport (LoincNum BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
								LOINC_NUMCol VarChar(255) Not Null,
								COMPONENTCol VarChar(255) Not Null,
								PROPERTYCol VarChar(255) Not Null,
								TIME_ASPCTCol VarChar(255) Not Null,
								SYSTEMCol VarChar(255) Not Null,
								SCALE_TYPCol VarChar(255) Not Null,
								METHOD_TYPCol VarChar(255) Not Null,
								CLASSCol VarChar(255) Not Null,
								SOURCECol VarChar(255) Not Null,
								DATE_LAST_CHANGEDCol VarChar(255) Not Null,
								CHNG_TYPECol VarChar(255) Not Null,
								COMMENTSCol VarChar(255) Not Null,
								STATUSCol VarChar(255) Not Null,
								CONSUMER_NAMECol VarChar(255) Not Null,
								MOLAR_MASSCol VarChar(255) Not Null,
								CLASSTYPECol VarChar(255) Not Null,
								FORMULACol VarChar(255) Not Null,
								SPECIESCol VarChar(255) Not Null,
								EXMPL_ANSWERSCol VarChar(255) Not Null,
								ACSSYMCol VarChar(255) Not Null,
								BASE_NAMECol VarChar(255) Not Null,
								NAACCR_IDCol VarChar(255) Not Null,
								CODE_TABLECol VarChar(255) Not Null,
								SURVEY_QUEST_TEXTCol VarChar(255) Not Null,
								SURVEY_QUEST_SRCCol VarChar(255) Not Null,
								UNITSREQUIREDCol VarChar(255) Not Null,
								SUBMITTED_UNITSCol VarChar(255) Not Null,
								RELATEDNAMES2Col VarChar(255) Not Null,
								SHORTNAMECol VarChar(255) Not Null,
								ORDER_OBSCol VarChar(255) Not Null,
								CDISC_COMMON_TESTSCol VarChar(255) Not Null,
								HL7_FIELD_SUBFIELD_IDCol VarChar(255) Not Null,
								EXTERNAL_COPYRIGHT_NOTICECol Text Not Null,
								EXAMPLE_UNITSCol VarChar(255) Not Null,
								LONG_COMMON_NAMECol VarChar(255) Not Null,
								HL7_V2_DATATYPECol VarChar(255) Not Null,
								HL7_V3_DATATYPECol VarChar(255) Not Null,
								CURATED_RANGE_AND_UNITSCol VarChar(255) Not Null,
								DOCUMENT_SECTIONCol VarChar(255) Not Null,
								EXAMPLE_UCUM_UNITSCol VarChar(255) Not Null,
								EXAMPLE_SI_UCUM_UNITSCol VarChar(255) Not Null,
								STATUS_REASONCol VarChar(255) Not Null,
								STATUS_TEXTCol VarChar(255) Not Null,
								CHANGE_REASON_PUBLICCol VarChar(255) Not Null,
								COMMON_TEST_RANKCol VarChar(255) Not Null,
								COMMON_ORDER_RANKCol VarChar(255) Not Null,
								COMMON_SI_TEST_RANKCol VarChar(255) Not Null,
								HL7_ATTACHMENT_STRUCTURECol VarChar(255) Not Null
								) DEFAULT CHARSET=utf8;";
			DataCore.NonQ(command);
			string[] lines=File.ReadAllLines(@"C:\Users\Ryan\Desktop\LOINC.txt");
			string[] arrayLOINC;
			for(int i=1;i<lines.Length;i++) {//skip first line
				command="INSERT INTO tempLoincImport VALUES ('"+i+"'";//primary key
				arrayLOINC=lines[i].Split('\t');
				for(int j=0;j<arrayLOINC.Length;j++) {
					command+=",'"+POut.String(arrayLOINC[j].Trim('"'))+"'";
				}
				command+=")";
				DataCore.NonQ(command);
			}
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