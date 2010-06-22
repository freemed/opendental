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

#if DEBUG
		private static string remoteHost="qaftp.mercurydataexchange.com";
#else
		private static string remoteHost="ftp.mercurydataexchange.com";
#endif
		private static Clearinghouse clearinghouse=null;

		///<summary></summary>
		public MercuryDE(){			
		}

		///<summary>Returns true if the communications were successful, and false if they failed. Both sends and retrieves.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum){
			clearinghouse=clearhouse;
			if(!Directory.Exists(clearhouse.ExportPath)){
				throw new Exception("Clearinghouse export path is invalid.");
			}
			//if(!Directory.Exists(clearhouse.ResponsePath)){
			//    throw new Exception("Clearinghouse response path is invalid.");
			//}
			//Before this function is called, the X12 file for the current batch has already been generated in
			//the clearinghouse export folder. The export folder will also contain batch files which have failed
			//to upload from previous attempts and we must attempt to upload these older batch files again if
			//there are any.
			//Step 1: Retrieve reports regarding the existing pending claim statuses.
			//Step 2: Send new claims in a new batch.
			if(batchNum==0){
				//Only retrieving reports so do not send.
				return true;
			}
			JSch jsch=new JSch();
			Session session=jsch.getSession(clearinghouse.LoginID,remoteHost,22);
			session.setPassword(clearinghouse.Password);
			Hashtable config=new Hashtable();
			config.Add("StrictHostKeyChecking", "no");
			session.setConfig(config);
			session.connect();
			Channel channel=session.openChannel("sftp");
			channel.connect();
			ChannelSftp ch=(ChannelSftp)channel;			
			//First upload the batch to the temporary directory.
			string[] files=Directory.GetFiles(clearhouse.ExportPath);
			for(int i=0;i<files.Length;i++){
				string accountNumber=clearinghouse.ISA08;
				string dateTimeStr=DateTime.Now.ToString("yyyyMMddhhmmss");
				string remoteFileName=accountNumber+dateTimeStr+i.ToString().PadLeft(3,'0')+".837D.txt";
				string remoteTempFilePath="/Testing/In/837D/Temp/"+remoteFileName;
				ch.put(files[i],remoteTempFilePath);
				ch.chmod((((7<<3)|7)<<3)|7,remoteTempFilePath);//Read, Write and Execute permissions for everyone.
				string remoteFilePath="Testing/In/837D/"+remoteFileName;
				ch.rename(remoteTempFilePath,remoteFilePath);
			}
			channel.disconnect();
			ch.disconnect();
			session.disconnect();
			return true;
		}

	}
}
