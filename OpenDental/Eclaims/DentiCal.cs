using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using OpenDentBusiness;
using Tamir.SharpSsh.jsch;

namespace OpenDental.Eclaims {
	/// <summary>
	/// aka Denti-Cal.
	/// </summary>
	public class DentiCal {

		private static string remoteHost="";//todo: get address for denti-cal sftp server.
		private static Clearinghouse clearinghouse=null;

		///<summary></summary>
		public DentiCal() {
		}

		///<summary>Returns true if the communications were successful, and false if they failed. Both sends and retrieves.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum) {
			clearinghouse=clearhouse;
			//Before this function is called, the X12 file for the current batch has already been generated in
			//the clearinghouse export folder. The export folder will also contain batch files which have failed
			//to upload from previous attempts and we must attempt to upload these older batch files again if
			//there are any.
			//Step 1: Retrieve reports regarding the existing pending claim statuses.
			//Step 2: Send new claims in a new batch.
			bool success=true;
			//Connect to the Denti-Cal SFTP server.
			Session session=null;
			Channel channel=null;
			ChannelSftp ch=null;
			JSch jsch=new JSch();
			try {
				session=jsch.getSession(clearinghouse.LoginID,remoteHost,22);//TODO: is this the right port number?
				session.setPassword(clearinghouse.Password);
				Hashtable config=new Hashtable();
				config.Add("StrictHostKeyChecking","no");
				session.setConfig(config);
				session.connect();
				channel=session.openChannel("sftp");
				channel.connect();
				ch=(ChannelSftp)channel;
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g("DentiCal","Connection Failed")+": "+ex.Message);
				return false;
			}
			try {
				//At this point we are connected to the Denti-Cal SFTP server.
				if(batchNum==0) { //Retrieve reports.
					if(!Directory.Exists(clearhouse.ResponsePath)) {
						throw new Exception("Clearinghouse response path is invalid.");
					}
					//Only retrieving reports so do not send new claims.
					string retrievePath="/Out/";
					Tamir.SharpSsh.java.util.Vector fileList=ch.ls(retrievePath);
					for(int i=0;i<fileList.Count;i++) {
						string listItem=fileList[i].ToString().Trim();
						if(listItem[0]=='d') {
							continue;//Skip directories and focus on files.
						}
						Match fileNameMatch=Regex.Match(listItem,".*\\s+(.*)$");
						string getFileName=fileNameMatch.Result("$1");
						string getFilePath=retrievePath+getFileName;
						string exportFilePath=CodeBase.ODFileUtils.CombinePaths(clearhouse.ResponsePath,getFileName);
						Tamir.SharpSsh.java.io.InputStream fileStream=null;
						FileStream exportFileStream=null;
						try {
							fileStream=ch.get(getFilePath);
							exportFileStream=File.Open(exportFilePath,FileMode.Create,FileAccess.Write);//Creates or overwrites.
							byte[] dataBytes=new byte[4096];
							int numBytes=fileStream.Read(dataBytes,0,dataBytes.Length);
							while(numBytes>0) {
								exportFileStream.Write(dataBytes,0,numBytes);
								numBytes=fileStream.Read(dataBytes,0,dataBytes.Length);
							}
						}
						catch {
							success=false;
						}
						finally {
							if(exportFileStream!=null) {
								exportFileStream.Dispose();
							}
							if(fileStream!=null) {
								fileStream.Dispose();
							}
						}
						//TODO: Is the following archive step allowed?
						if(success) {
							//Move the processed files into the Archive folder within the Out folder on the Denti-Cal sftp so that history is preserved.
							string archiveFilePath=retrievePath+"Archive/"+getFileName;
							try {
								ch.rm(archiveFilePath);
							}
							catch {
								//Remove any destination files by the same exact name. The file will most likely not be present.
							}
							ch.rename(getFilePath,archiveFilePath);
						}
					}
				}
				else { //Send batch of claims.
					if(!Directory.Exists(clearhouse.ExportPath)) {
						throw new Exception("Clearinghouse export path is invalid.");
					}
					//First upload the batch to the temporary directory.
					string[] files=Directory.GetFiles(clearhouse.ExportPath);
					for(int i=0;i<files.Length;i++) {
						string remoteFileName="claim"+DateTime.Now.ToString("yyyyMMddhhmmss")+i.ToString().PadLeft(5,'0')+".txt";
						//TODO:Is a temp subdirectory allowed?
						string remoteTempFilePath="/In/Temp/"+remoteFileName;
						ch.put(files[i],remoteTempFilePath);
						//Read, Write and Execute permissions for everyone. This appears to cause no effect.
						ch.chmod((((7<<3)|7)<<3)|7,remoteTempFilePath);
						string remoteFilePath="/In/"+remoteFileName;
						ch.rename(remoteTempFilePath,remoteFilePath);
						File.Delete(files[i]);//Remove the processed file.
					}
				}
			}
			catch {
				success=false;
			}
			finally {
				//Disconnect from the Denti-Cal SFTP server.
				channel.disconnect();
				ch.disconnect();
				session.disconnect();
			}
			return success;
		}

	}
}
