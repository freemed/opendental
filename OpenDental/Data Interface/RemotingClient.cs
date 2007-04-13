using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	class RemotingClient {
		public static bool OpenDentBusinessIsLocal;
		private static TcpClient client;
		private static NetworkStream netStream;
		public static string ServerName;
		public static int ServerPort;

		public static DataSet ProcessQuery(DtoQueryBase dto){
			byte[] buffer=SendAndReceive(dto);//this might throw an exception if server unavailable
			MemoryStream memStream=new MemoryStream(buffer);
			XmlSerializer serializer;
			try{
				serializer = new XmlSerializer(typeof(DataSet));
				DataSet retVal=(DataSet)serializer.Deserialize(memStream);
				memStream.Close();
				return retVal;
			}
			catch{
				memStream=new MemoryStream(buffer);//just in case stream is in wrong position.
				serializer = new XmlSerializer(typeof(DtoException));
				DtoException exception=(DtoException)serializer.Deserialize(memStream);
				memStream.Close();
				throw new Exception(exception.Message);
			}
		}

		///<summary>InsertID will be returned for Insert commands.  Other commands return the number of rows affected which is usually just ignored.</summary>
		public static int ProcessCommand(DtoCommandBase dto){
			byte[] buffer=SendAndReceive(dto);//this might throw an exception if server unavailable
			MemoryStream memStream=new MemoryStream(buffer);
			XmlSerializer serializer;
			try{
				serializer = new XmlSerializer(typeof(DtoServerAck));
				DtoServerAck ack=(DtoServerAck)serializer.Deserialize(memStream);
				memStream.Close();
				return ack.IDorRows;
			}
			catch{
				memStream=new MemoryStream(buffer);//just in case stream is in wrong position.
				serializer = new XmlSerializer(typeof(DtoException));
				DtoException exception=(DtoException)serializer.Deserialize(memStream);
				memStream.Close();
				throw new Exception(exception.Message);
			}
		}

		private static byte[] SendAndReceive(DataTransferObject dto){
			byte[] data=dto.Serialize();
			//#if DEBUG
			//	string xmlString=Encoding.UTF8.GetString(data);
			//	Debug.WriteLine("Client Sent: "+xmlString);
			//#endif
			if(client==null){
				try{
					client = new TcpClient(ServerName,ServerPort);
					netStream = client.GetStream();
				}
				catch{
					throw new Exception("Server is refusing the connection. Either the server program is not running, or a port on the server is being blocked by a firewall.");
				}
			}
			try{
				netStream.Write(data,0,data.Length);
			}
			catch (Exception e){
				netStream.Close();
				client.Close();
				client=null;
				throw e;
			}
			//Receive the TcpServer.response-------------------------------------
			Byte[] buffer = new Byte[16384];//a power of 2 that will easily cover average size result sets.
			//MemoryStream memStream=new MemoryStream();
			int numberOfBytesRead = 0;
			StringBuilder strBuild = new StringBuilder();
			//int readValue;
			Decoder decoder = Encoding.UTF8.GetDecoder();
			char[] chars;
			do{
				//this next line blocks until the response comes back
				numberOfBytesRead=netStream.Read(buffer,0,buffer.Length);
				//readValue=netStream.ReadByte();
				// Use Decoder class to convert from bytes to UTF8
				// in case a character spans two buffers.
				chars = new char[decoder.GetCharCount(buffer,0,numberOfBytesRead)];
				decoder.GetChars(buffer,0,numberOfBytesRead,chars,0);
				strBuild.Append(chars);
				// Check for EOF or an empty message.
				if(strBuild.ToString().IndexOf("<EOF>") != -1) {
					break;
				}
				//memStream.WriteByte((byte)readValue);
				//memStream.Write(buffer,0,numberOfBytesRead);
			}
			while(numberOfBytesRead!=0);//netStream.DataAvailable);
			strBuild.Replace("<EOF>","");
			//memStream must be decoupled by converting to byte[] and then back to memStream.
			buffer=Encoding.UTF8.GetBytes(strBuild.ToString());//memStream.ToArray();
			//memStream.Close();
			//netStream.Close();
			//client.Close();
			//#if DEBUG
			//string responseString=Encoding.UTF8.GetString(buffer);
			//Debug.WriteLine("Client Received: "+responseString);
			//#endif
			return buffer;		
		}
				
			//} 
			//catch (ArgumentNullException e) {
				//Console.WriteLine("ArgumentNullException: {0}", e);
			//} 
			//catch (SocketException e) {
				//Console.WriteLine("SocketException: {0}", e);
			//} 
			//Console.WriteLine("\n Press Enter to continue...");
			//Console.Read();
		//return null;
		//}

	}
}
