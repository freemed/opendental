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
			string result=SendAndReceive(dto);
			try {
				return XmlConverter.XmlToDs(result);
			}
			catch {
				DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
				throw new Exception(exception.Message);
			}
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

		///<summary></summary>
		public static object ProcessGetObject(DtoGetObject dto) {
			string result=SendAndReceive(dto);//this might throw an exception if server unavailable
			try {
				XmlSerializer serializer=new XmlSerializer(Type.GetType("OpenDentBusiness."+dto.ObjectType));
				StringReader strReader=new StringReader(result);
				XmlReader xmlReader=XmlReader.Create(strReader);
				object obj=serializer.Deserialize(xmlReader);
				strReader.Close();
				xmlReader.Close();
				return obj;
			}
			catch {
				DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
				throw new Exception(exception.Message);
			}
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
		}

		
	}

}
