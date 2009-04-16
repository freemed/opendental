using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using OpenDentBusiness;

namespace OpenDentBusiness {
		public class RemotingClient {
			///<summary>This dll will be in one of these three roles.  There can be a dll on the client and a dll on the server, both involved in the logic.  This keeps track of which one is which.</summary>
		public static RemotingRole RemotingRole;
		public static string ServerURI;

/*
		public static DataSet ProcessQuery(DtoQueryBase dto){
			throw new NotImplementedException();
			string result=SendAndReceive(dto);//this might throw an exception if server unavailable
			//byte[] buffer=
			//MemoryStream memStream=new MemoryStream(buffer);
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
		}*/

		///<summary></summary>
		public static DataSet ProcessGetDS(DtoGetDS dto) {
			throw new NotImplementedException();
			/*
			byte[] buffer=SendAndReceive(dto);//this might throw an exception if server unavailable
			MemoryStream memStream=new MemoryStream(buffer);
			XmlSerializer serializer;
			try {
				serializer = new XmlSerializer(typeof(DataSet));
				DataSet retVal=(DataSet)serializer.Deserialize(memStream);
				memStream.Close();
				return retVal;
			}
			catch {
				memStream=new MemoryStream(buffer);//just in case stream is in wrong position.
				serializer = new XmlSerializer(typeof(DtoException));
				DtoException exception=(DtoException)serializer.Deserialize(memStream);
				memStream.Close();
				throw new Exception(exception.Message);
			}*/
		}

		public static DataTable ProcessGetTable(DtoGetTable dto) {
			throw new NotImplementedException();
			/*
			byte[] buffer=SendAndReceive(dto);//this might throw an exception if server unavailable
			MemoryStream memStream=new MemoryStream(buffer);
			XmlSerializer serializer;
			try {
				serializer = new XmlSerializer(typeof(DataTable));
				DataTable retVal=(DataTable)serializer.Deserialize(memStream);
				memStream.Close();
				return retVal;
			}
			catch {
				memStream=new MemoryStream(buffer);//just in case stream is in wrong position.
				serializer = new XmlSerializer(typeof(DtoException));
				DtoException exception=(DtoException)serializer.Deserialize(memStream);
				memStream.Close();
				throw new Exception(exception.Message);
			}*/
		}

		///<summary>InsertID will be returned for Insert commands.  Other commands return the number of rows affected which is usually just ignored.</summary>
		public static int ProcessCommand(DtoSendCmd dto) {
			string result=SendAndReceive(dto);//this might throw an exception if server unavailable
			try {
				DtoServerAck ack=(DtoServerAck)DataTransferObject.Deserialize(result);
				return ack.IDorRows;
			}
			catch {
				DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
				throw new Exception(exception.Message);
			}
			/*
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
			}*/
		}		

		internal static string SendAndReceive(DataTransferObject dto){
			//throw new NotImplementedException();
			string dtoString=dto.Serialize();
			OpenDentalServer.ServiceMain service=new OpenDentBusiness.OpenDentalServer.ServiceMain();
			string result=service.ProcessRequest(dtoString);
			//try {
				//result=
			//}
			//catch {

			//}
			return result;
			/*
			byte[] data=dto.Serialize();
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
				WriteDataToStream(netStream, data);
			}
			catch (Exception e){
				netStream.Close();
				client.Close();
				client=null;
				throw e;
			}
			//Receive the TcpServer.response-------------------------------------
			data = ReadDataFromStream(netStream);
			return data;*/
		}

			/*
		public static void Disconnect() {
			if(netStream != null) {
				netStream.Close();
				netStream = null;
			}
			if(client != null) {
				client.Close();
				client = null;
			}
		}*/

			/*
		public static byte[] ReadDataFromStream(Stream stream) {
			byte[] value = null;

			using (MemoryStream memoryStream = new MemoryStream()) {
				byte[] buffer = new byte[BufferSize];
				// The number of bytes read from the stream to the buffer
				int bytesRead = 0;	
				// A boolean indicating if the message has ending (i.e. no more data available; indicated by
				// a '\0' charater at the end of the data
				bool messageEnded = false;

				while (!messageEnded) {
					bytesRead = stream.Read(buffer, 0, BufferSize);
					
					// Check for a '\0' character at the end of the data
					messageEnded = bytesRead > 0 && buffer[bytesRead - 1] == '\0';
					// The terminating character doesn't really count as data.
					if (messageEnded)
						bytesRead--;

					memoryStream.Write(buffer, 0, bytesRead);
				}

				value = memoryStream.ToArray();
			}

			return value;
		}

		public static void WriteDataToStream(Stream stream, byte[] data) {
			stream.Write(data, 0, data.Length);
			stream.WriteByte((byte)'\0');
			stream.Flush();
		}*/
	}

}
