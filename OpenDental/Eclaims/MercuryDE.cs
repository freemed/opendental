using System;
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

namespace OpenDental.Eclaims
{
	/// <summary>
	/// aka Mercury Dental Exchange.
	/// </summary>
	public class MercuryDE{

		private static Socket clientSocket=null;
		private static Stream stream=null;
		private static Stream stream2=null;
		private static string reply=null;
		private static string remotePath=null;
		private static int retValue=0;
		private static bool isUpload=false;
#if DEBUG
		private static string remoteHost="ftp://qaftp.mercurydataexchange.com";
#else
		private static string remoteHost="ftp://ftp.mercurydataexchange.com";
#endif
		private static int remotePort=990;//Standard SSL port.
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
			if(batchNum==0){
				//Only retrieving reports so do not send.
				return true;
			}
			try{
				//Connect and login to the MercuryDE clearinghouse SFTP for the currently configured user within the clearinghouse setup.
				Socket clientSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				IPEndPoint ep=new IPEndPoint(Dns.GetHostEntry(remoteHost).AddressList[0],remotePort);
				try {
					clientSocket.Connect(ep);
				} catch(Exception) {
					throw new IOException("Couldn't connect to remote server");
				}
				int responseVal=ReadResponseMsgFTP();
				if(responseVal!=220) {
					if(clientSocket!=null) {
						sendCommand("QUIT");
					}
					cleanup();
					throw new IOException(reply.Substring(4));
				}
				sendCommand("AUTH SSL");
				getSslStream(clientSocket);
				login();
				//Send the claims one batch at a time including batches which previously failed to upload.


				//Close the SFTP connection.
				close();
			}catch{
			}
			return true;
		}

		private static int ReadResponseMsgFTP() {
			System.Text.ASCIIEncoding enc=new System.Text.ASCIIEncoding();
			byte[] serverbuff=new Byte[1024];
			int count=0;
			while(true) {
				byte[] buff=new Byte[2];
				int bytes=stream.Read(buff,0,1);
				if(bytes==1) {
					serverbuff[count]=buff[0];
					count++;
					if(buff[0]=='\n'){
						break;
					}
				} else {
					break;
				};
			};
			reply=enc.GetString(serverbuff,0,count);
			retValue=Int32.Parse(reply.Substring(0,3));
			return retValue;
		}

		public static void sendCommand(string command) {
			Byte[] cmdBytes=Encoding.ASCII.GetBytes((command+"\r\n").ToCharArray());
			WriteMsg(command+"\r\n");
			ReadResponseMsgFTP();
		}

		private static void WriteMsg(string message) {
			System.Text.ASCIIEncoding en=new System.Text.ASCIIEncoding();
			byte[] WriteBuffer=new byte[1024];
			WriteBuffer=en.GetBytes(message);
			stream.Write(WriteBuffer,0,WriteBuffer.Length);
		}

		private static void cleanup() {
			if(clientSocket!=null) {
				clientSocket.Close();
				clientSocket=null;
			}
		}

		public static void getSslStream(Socket Csocket) {
			RemoteCertificateValidationCallback callback=new RemoteCertificateValidationCallback(OnCertificateValidation);
			SslStream _sslStream=new SslStream(new NetworkStream(Csocket));
			try {
				_sslStream.AuthenticateAsClient(
						remoteHost,
						null,
						System.Security.Authentication.SslProtocols.Ssl3|System.Security.Authentication.SslProtocols.Tls,
						true);
				if(_sslStream.IsAuthenticated){
					if(isUpload){
						stream2=_sslStream;
					}else{
						stream=_sslStream;
					}
				}
			} catch(Exception ex) {
				throw new IOException(ex.Message);
			}
		}

		private static bool OnCertificateValidation(object sender,X509Certificate certificate,X509Chain chain,SslPolicyErrors errors) {
			// Return true if there are no policy errors
			// The certificate can also be manually verified to 
			//make sure it meets your specific // policies by 
			//     interrogating the x509Certificate object.
			if(errors!=SslPolicyErrors.None) {
				return false;
			} else {
				return true;
			}
		}

		public static void login() {
			if(clientSocket==null||clientSocket.Connected==false){
				throw new IOException("Couldn't connect to remote server");
			}
			sendCommand("USER "+clearinghouse.LoginID);
			if(!(retValue==331||retValue==230)) {
				cleanup();
				throw new IOException(reply.Substring(4));
			}
			if(retValue!=230) {
				sendCommand("PASS "+clearinghouse.Password);
				if(!(retValue==230||retValue==202)) {
					cleanup();
					throw new IOException(reply.Substring(4));
				}
			}
			chdir(remotePath);
		}

		/// Change the current working directory on the remote FTP server.
		public static void chdir(string dirName) {
			if(dirName.Equals(".")) {
				return;
			}
			sendCommand("CWD "+dirName);
			if(retValue!=250) {
				throw new IOException(reply.Substring(4));
			}
			remotePath=dirName;
		}

		/// Secure Upload a file and set the resume flag.
		public static void uploadSecure(string fileName,Boolean resume) {
			isUpload=true;
			try{
				sendCommand("PASV");
				if(retValue!=227) {
					throw new IOException(reply.Substring(4));
				}
				Socket cSocket=createDataSocket();
				isUpload=true;
				getSslStream(cSocket);
				long offset=0;
				if(resume) {
					try {
						offset=getFileSize(fileName);
					} catch(Exception) {
						offset=0;
					}
				}
				if(offset>0) {
					sendCommand("REST "+offset);
					if(retValue!=350) {
						//Remote server may not support resuming.
						offset=0;
					}
				}
				sendCommand("STOR "+Path.GetFileName(fileName));
				if(!(retValue==125||retValue==150)) {
					throw new IOException(reply.Substring(4));
				}
				FileStream input=File.OpenRead(fileName);
				byte[] bufferFile=new byte[input.Length];
				input.Read(bufferFile,0,bufferFile.Length);
				input.Close();
				if(offset!=0) {
					input.Seek(offset,SeekOrigin.Begin);
				}
				if(cSocket.Connected) {
					stream2.Write(bufferFile,0,bufferFile.Length);
				}
				stream2.Close();
				if(cSocket.Connected) {
					cSocket.Close();
				}
				ReadResponseMsgFTP();
				if(!(retValue==226||retValue==250)) {
					throw new IOException(reply.Substring(4));
				}
			}finally{
				isUpload=false;
			}
		}

		public static void close() {
			if(clientSocket!=null) {
				sendCommand("QUIT");
			}
			cleanup();
		}

		private static Socket createDataSocket() {
			sendCommand("PASV");
			if(retValue!=227) {
				throw new IOException(reply.Substring(4));
			}
			int index1=reply.IndexOf('(');
			int index2=reply.IndexOf(')');
			string ipData=reply.Substring(index1+1,index2-index1-1);
			int[] parts=new int[6];
			int len=ipData.Length;
			int partCount=0;
			string buf="";
			for(int i=0;i<len&&partCount<=6;i++) {
				char ch=Char.Parse(ipData.Substring(i,1));
				if(Char.IsDigit(ch))
					buf+=ch;
				else if(ch!=',') {
					throw new IOException("Malformed PASV reply: "+reply);
				}
				if(ch==','||i+1==len) {
					try {
						parts[partCount++]=Int32.Parse(buf);
						buf="";
					} catch(Exception) {
						throw new IOException("Malformed PASV reply: "+reply);
					}
				}
			}
			string ipAddress=parts[0]+"."+parts[1]+"."+
				parts[2]+"."+parts[3];
			int port=(parts[4]<<8)+parts[5];
			Socket s=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			IPEndPoint ep=new IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0],port);
			try {
				s.Connect(ep);
			} catch(Exception) {
				throw new IOException("Can't connect to remoteserver");
			}
			return s;
		}

		private static long getFileSize(string fileName) {
			sendCommand("SIZE "+fileName);
			long size=0;
			if(retValue==213) {
				size=Int64.Parse(reply.Substring(4));
			} else {
				throw new IOException(reply.Substring(4));
			}
			return size;
		}

	}
}
