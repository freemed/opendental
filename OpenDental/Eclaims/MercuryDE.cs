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

namespace OpenDental.Eclaims
{
	/// <summary>
	/// aka Mercury Dental Exchange.
	/// </summary>
	public class MercuryDE{

		private static string remoteHost="qaftp.mercurydataexchange.com";//Change to ftp.mercurydataexchange.com when released.
		private static string rootFolderName="Testing";//Change to "Production" when released.
		private static Clearinghouse clearinghouse=null;

		///<summary></summary>
		public MercuryDE(){			
		}

		///<summary>Returns true if the communications were successful, and false if they failed. Both sends and retrieves.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum){
			clearinghouse=clearhouse;
			//Before this function is called, the X12 file for the current batch has already been generated in
			//the clearinghouse export folder. The export folder will also contain batch files which have failed
			//to upload from previous attempts and we must attempt to upload these older batch files again if
			//there are any.
			//Step 1: Retrieve reports regarding the existing pending claim statuses.
			//Step 2: Send new claims in a new batch.
			bool success=true;
			//Connect to the MDE SFTP server.
			Session session=null;
			Channel channel=null;
			ChannelSftp ch=null;
			JSch jsch=new JSch();
			session=jsch.getSession(clearinghouse.LoginID,remoteHost,22);
			session.setPassword(clearinghouse.Password);
			Hashtable config=new Hashtable();
			config.Add("StrictHostKeyChecking", "no");
			session.setConfig(config);
			session.connect();
			channel=session.openChannel("sftp");
			channel.connect();
			ch=(ChannelSftp)channel;
			try{
				//At this point we are connected to the MDE SFTP server.
				if(batchNum==0){
					if(!Directory.Exists(clearhouse.ResponsePath)){
						throw new Exception("Clearinghouse response path is invalid.");
					}
					//Only retrieving reports so do not send new claims.
					string retrievePath="/"+rootFolderName+"/Out/997/";
					Tamir.SharpSsh.java.util.Vector fileList=ch.ls(retrievePath);
					for(int i=0;i<fileList.Count;i++){
						string listItem=fileList[i].ToString().Trim();
						if(listItem[0]=='d'){
							continue;//Skip directories and focus on files.
						}
						Match fileNameMatch=Regex.Match(listItem,".*\\s+(.*)$");
						string getFileName=fileNameMatch.Result("$1");
						string getFilePath=retrievePath+getFileName;
						string exportFilePath=CodeBase.ODFileUtils.CombinePaths(clearhouse.ResponsePath,getFileName);						
						Tamir.SharpSsh.java.io.InputStream fileStream=null;
						FileStream exportFileStream=null;
						try{
							fileStream=ch.get(getFilePath);
							exportFileStream=File.Open(exportFilePath,FileMode.Create,FileAccess.Write);//Creates or overwrites.
							byte[] dataBytes=new byte[4096];
							int numBytes=fileStream.Read(dataBytes,0,dataBytes.Length);
							while(numBytes>0){
								exportFileStream.Write(dataBytes,0,numBytes);
								numBytes=fileStream.Read(dataBytes,0,dataBytes.Length);
							}
						}catch{
							success=false;
						}finally{
							if(exportFileStream!=null){
								exportFileStream.Dispose();
							}
							if(fileStream!=null){
								fileStream.Dispose();
							}
						}
						string archiveFilePath=retrievePath+"Archive/"+getFileName;
						try{
							ch.rm(archiveFilePath);
						}catch{
							//Remove any destination files by the same exact name. The file will most likely not be present.
						}
						ch.rename(getFilePath,archiveFilePath);
					}
				}else{
					if(!Directory.Exists(clearhouse.ExportPath)){
						throw new Exception("Clearinghouse export path is invalid.");
					}
					//First upload the batch to the temporary directory.
					string[] files=Directory.GetFiles(clearhouse.ExportPath);
					for(int i=0;i<files.Length;i++){
						string accountNumber=clearinghouse.ISA08;
						string dateTimeStr=DateTime.Now.ToString("yyyyMMddhhmmss");
						string remoteFileName=accountNumber+dateTimeStr+i.ToString().PadLeft(3,'0')+".837D.txt";
						string remoteTempFilePath="/"+rootFolderName+"/In/837D/Temp/"+remoteFileName;
						ch.put(files[i],remoteTempFilePath);
						//Read, Write and Execute permissions for everyone. This appears to cause no effect.
						ch.chmod((((7<<3)|7)<<3)|7,remoteTempFilePath);
						string remoteFilePath="/"+rootFolderName+"/In/837D/"+remoteFileName;
						ch.rename(remoteTempFilePath,remoteFilePath);
						File.Delete(files[i]);//Remove the processed file.
					}
				}
			}catch{
				success=false;
			}finally{
				//Disconnect from the MDE SFTP server.
				channel.disconnect();
				ch.disconnect();
				session.disconnect();
			}
			return success;
		}

	}
}
