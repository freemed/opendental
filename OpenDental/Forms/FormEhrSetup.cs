using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.Forms;
using System.Xml;
using System.Threading;
using System.Net;
using System.IO;
using Ionic.Zip;

namespace OpenDental {
	public partial class FormEhrSetup:Form {
		public FormEhrSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrSetup_Load(object sender,EventArgs e) {
			//hidden until EHR 2014
			menuItemSettings.Text="";
			menuItemSettings.Enabled=false;
			if(PrefC.GetBool(PrefName.EhrEmergencyNow)) {
				panelEmergencyNow.BackColor=Color.Red;
			}
			else {
				panelEmergencyNow.BackColor=SystemColors.Control;
			}
			if(!Security.IsAuthorized(Permissions.Setup,true)) {
				//Hide all the buttons except Emergency Now and Close.
				butICD9s.Visible=false;
				butAllergies.Visible=false;
				//Forumularies will now be checked through New Crop
				//butFormularies.Visible=false;
				butVaccineDef.Visible=false;
				butDrugManufacturer.Visible=false;
				butDrugUnit.Visible=false;
				butInboundEmail.Visible=false;
				butReminderRules.Visible=false;
				butEducationalResources.Visible=false;
				butRxNorm.Visible=false;
				butKeys.Visible=false;
				menuItemSettings.Enabled=false;
			}
		}

		private void menuItemSettings_Click(object sender,EventArgs e) {
			FormEhrSettings FormES=new FormEhrSettings();
			FormES.ShowDialog();
		}

		private void butICD9s_Click(object sender,EventArgs e) {
			FormIcd9s FormE=new FormIcd9s();
			FormE.ShowDialog();
		}

		private void butAllergies_Click(object sender,EventArgs e) {
			FormAllergySetup FAS=new FormAllergySetup();
			FAS.ShowDialog();
		}

		//Formularies will now be checked through New Crop
		//private void butFormularies_Click(object sender,EventArgs e) {
		//	FormFormularies FormE=new FormFormularies();
		//	FormE.ShowDialog();
		//}

		private void butVaccineDef_Click(object sender,EventArgs e) {
			FormVaccineDefSetup FormE=new FormVaccineDefSetup();
			FormE.ShowDialog();
		}

		private void butDrugManufacturer_Click(object sender,EventArgs e) {
			FormDrugManufacturerSetup FormE=new FormDrugManufacturerSetup();
			FormE.ShowDialog();
		}

		private void butDrugUnit_Click(object sender,EventArgs e) {
			FormDrugUnitSetup FormE=new FormDrugUnitSetup();
			FormE.ShowDialog();
		}

		private void butInboundEmail_Click(object sender,EventArgs e) {
			FormEmailSetupEHR FormES=new FormEmailSetupEHR();
			FormES.ShowDialog();
		}

		private void butEmergencyNow_Click(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.EhrEmergencyNow)) {
				panelEmergencyNow.BackColor=SystemColors.Control;
				Prefs.UpdateBool(PrefName.EhrEmergencyNow,false);
			}
			else {
				panelEmergencyNow.BackColor=Color.Red;
				Prefs.UpdateBool(PrefName.EhrEmergencyNow,true);
			}
			DataValid.SetInvalid(InvalidType.Prefs);
		}
		
		private void butReminderRules_Click(object sender,EventArgs e) {
			FormReminderRules FormRR = new FormReminderRules();
			FormRR.ShowDialog();
		}

		private void butEducationalResources_Click(object sender,EventArgs e) {
			FormEduResourceSetup FormEDUSetup = new FormEduResourceSetup();
			FormEDUSetup.ShowDialog();
		}

		private void butRxNorm_Click(object sender,EventArgs e) {
			FormRxNorms FormR=new FormRxNorms();
			FormR.ShowDialog();
		}

		private void butLoincs_Click(object sender,EventArgs e) {
			FormLoincs FormL=new FormLoincs();
			FormL.ShowDialog();
		}

		private void butSnomeds_Click(object sender,EventArgs e) {
			FormSnomeds FormS=new FormSnomeds();
			FormS.ShowDialog();
		}

//		private void butICD9CM31_Click(object sender,EventArgs e) {
//			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This button does not perform error checking and only works from Ryan's computer.")) {
//				return;
//			}
//			Cursor=Cursors.WaitCursor;
//			try {
//				//Import ICD9-CM DX codes with long description----------------------------------------------------------------------------------------------------------
//				MsgBox.Show(this,"Importing ICD9-CM DX codes with long description.");
//				System.IO.StreamReader sr=new System.IO.StreamReader(@"C:\Users\Ryan\Desktop\ICD9CM31\DXL.txt");
//				string command=@"DROP TABLE IF EXISTS ICD9CMDX";
//				DataCore.NonQ(command);
//				command=@"CREATE TABLE `ICD9CMDX` (
//									`ICD9CMDXNum` BIGINT(20) NOT NULL AUTO_INCREMENT,
//									`CodeString` VARCHAR(255) DEFAULT '',
//									`DescriptionLong` VARCHAR(255) DEFAULT '',
//									`DescriptionShort` VARCHAR(255) DEFAULT '',
//									INDEX(CodeString),
//									PRIMARY KEY (`ICD9CMDXNum`)
//									) DEFAULT CHARSET=utf8;";
//				DataCore.NonQ(command);
//				string[] arrayICD9CM=new string[2];
//				string line="";
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code.
//					line=sr.ReadLine();
//					arrayICD9CM[0]=line.Substring(0,5).Trim();//fixed width column
//					if(arrayICD9CM[0].Length>3){
//						arrayICD9CM[0]=arrayICD9CM[0].Substring(0,3)+"."+arrayICD9CM[0].Substring(3);
//					}
//					arrayICD9CM[1]=line.Substring(6);
//					command="INSERT INTO ICD9CMDX (CodeString,DescriptionLong) VALUES ('"+POut.String(arrayICD9CM[0])+"','"+POut.String(arrayICD9CM[1])+"')";
//					DataCore.NonQ(command);
//				}
//				//Import ICD9-CM DX codes with short description----------------------------------------------------------------------------------------------------------
//				MsgBox.Show(this,"Importing ICD9-CM DX codes with short description.");
//				sr.Dispose();
//				sr=new System.IO.StreamReader(@"C:\Users\Ryan\Desktop\ICD9CM31\DXS.txt");
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code.
//					line=sr.ReadLine();
//					arrayICD9CM[0]=line.Substring(0,5).Trim();//fixed width column
//					if(arrayICD9CM[0].Length>3) {
//						arrayICD9CM[0]=arrayICD9CM[0].Substring(0,3)+"."+arrayICD9CM[0].Substring(3);
//					}
//					arrayICD9CM[1]=line.Substring(6);
//					command="UPDATE ICD9CMDX SET DescriptionShort='"+POut.String(arrayICD9CM[1])+"' WHERE CodeString='"+POut.String(arrayICD9CM[0])+"'";
//					DataCore.NonQ(command);
//				}
//				//Import ICD9-CM SG codes with long description----------------------------------------------------------------------------------------------------------
//				MsgBox.Show(this,"Importing ICD9-CM SG codes with long description.");
//				sr.Dispose();
//				sr=new System.IO.StreamReader(@"C:\Users\Ryan\Desktop\ICD9CM31\SGL.txt");
//				command=@"DROP TABLE IF EXISTS ICD9CMSG";
//				DataCore.NonQ(command);
//				command=@"CREATE TABLE `ICD9CMSG` (
//									`ICD9CMSGNum` BIGINT(20) NOT NULL AUTO_INCREMENT,
//									`CodeString` VARCHAR(255) DEFAULT '',
//									`DescriptionLong` VARCHAR(255) DEFAULT '',
//									`DescriptionShort` VARCHAR(255) DEFAULT '',
//									INDEX(CodeString),
//									PRIMARY KEY (`ICD9CMSGNum`)
//									) DEFAULT CHARSET=utf8;";
//				DataCore.NonQ(command);
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code.
//					line=sr.ReadLine();
//					arrayICD9CM[0]=line.Substring(0,5).Trim();//fixed width column
//					if(arrayICD9CM[0].Length>2) {
//						arrayICD9CM[0]=arrayICD9CM[0].Substring(0,2)+"."+arrayICD9CM[0].Substring(2);
//					}
//					arrayICD9CM[1]=line.Substring(5);
//					command="INSERT INTO ICD9CMSG (CodeString,DescriptionLong) VALUES ('"+POut.String(arrayICD9CM[0])+"','"+POut.String(arrayICD9CM[1])+"')";
//					DataCore.NonQ(command);
//				}
//				//Import ICD9-CM SG codes with short description----------------------------------------------------------------------------------------------------------
//				MsgBox.Show(this,"Importing ICD9-CM SG codes with short description.");
//				sr.Dispose();
//				sr=new System.IO.StreamReader(@"C:\Users\Ryan\Desktop\ICD9CM31\SGS.txt");
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code.
//					line=sr.ReadLine();
//					arrayICD9CM[0]=line.Substring(0,5).Trim();//fixed width column
//					if(arrayICD9CM[0].Length>2) {
//						arrayICD9CM[0]=arrayICD9CM[0].Substring(0,2)+"."+arrayICD9CM[0].Substring(2);
//					}
//					arrayICD9CM[1]=line.Substring(5);
//					command="UPDATE ICD9CMSG SET DescriptionShort='"+POut.String(arrayICD9CM[1])+"' WHERE CodeString='"+POut.String(arrayICD9CM[0])+"'";
//					DataCore.NonQ(command);
//				}
//			}
//			catch(Exception ex) {
//				MessageBox.Show(this,Lan.g(this,"Error importing ICD9CM codes:")+"\r\n"+ex.Message);
//			}
//			Cursor=Cursors.Default;
//		}

		private void butTimeSynch_Click(object sender,EventArgs e) {
			FormEhrTimeSynch formET = new FormEhrTimeSynch();
			formET.ShowDialog();
		}

		private void butKeys_Click(object sender,EventArgs e) {
			FormEhrQuarterlyKeys formK=new FormEhrQuarterlyKeys();
			formK.ShowDialog();
		}

		private void butCodeImport_Click(object sender,EventArgs e) {
			FormCodeSystemsImport FormCSI=new FormCodeSystemsImport();
			FormCSI.ShowDialog();
		}

		//private void butEHRCodes_Click(object sender,EventArgs e) {
		//	if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will import codes required for CQMs to function properly, and may take up to 5 minutes.  Before running this tool, you should use the code system import tool.")) {
		//		return;
		//	}
		//	Cursor=Cursors.WaitCursor;
		//	//request URL from webservice
		//	string result="";
		//	try {
		//		result=SendAndReceiveDownloadXml("EHRCODES");
		//	}
		//	catch(Exception ex) {
		//		Cursor=Cursors.Default;
		//		MessageBox.Show("Error: "+ex.Message);
		//		return;
		//	}
		//	XmlDocument doc=new XmlDocument();
		//	doc.LoadXml(result);
		//	XmlNode node=doc.SelectSingleNode("//Error");
		//	if(node!=null) {
		//		throw new Exception(node.InnerText);
		//	}
		//	node=doc.SelectSingleNode("//CodeSystemURL");
		//	string codeSystemURL="";
		//	if(node!=null) {
		//		codeSystemURL=node.InnerText;
		//	}
		//	//Download File to local machine
		//	Thread.Sleep(2000);//wait 2 seconds between downloads.
		//	string tempFile="";
		//	int newCodeCount=0;
		//	int totalCodeCount=0;
		//	int availableCodeCount=0;
		//	try {
		//		tempFile=downloadFileHelper(codeSystemURL,"EHRCODES");//shows progress bar.
		//		CodeSystems.ImportEhrCodes(tempFile,out newCodeCount,out totalCodeCount,out availableCodeCount);
		//	}
		//	catch(Exception ex) {
		//		Cursor=Cursors.Default;//Just in case.
		//		MessageBox.Show(Lan.g(this,"Error encountered while setting up codes required for EHR")+":\r\n"+ex.Message);
		//	}
		//	Cursor=Cursors.Default;
		//	if(totalCodeCount<availableCodeCount) {
		//		MessageBox.Show(newCodeCount+" new codes imported.  The database now contains "+totalCodeCount+" out of "+availableCodeCount+" total available codes.  To import additional codes, you must import the corresponding code systems using the code system import tool.");
		//	}
		//	else {
		//		MessageBox.Show(newCodeCount+" new codes imported.  The database now all "+availableCodeCount+" available codes.");
		//	}
		//}

		private static string downloadFileHelper(string codeSystemURL,string codeSystemName) {
			string zipFileDestination=Path.GetTempFileName();//@"c:\users\ryan\desktop\"+codeSystemName+".txt";
			File.Delete(zipFileDestination);
			WebRequest wr=WebRequest.Create(codeSystemURL);
			WebResponse webResp=null;
			try {
				webResp=wr.GetResponse();
			}
			catch(Exception ex) {
				return null;
			}
			DownloadFileWorker(codeSystemURL,zipFileDestination);
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
		private static void DownloadFileWorker(string downloadUri,string destinationPath) {
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