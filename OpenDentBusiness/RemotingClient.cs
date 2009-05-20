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

		public static DataTable ProcessGetTable(DtoGetTable dto) {
			string result=SendAndReceive(dto);
			try {
				return XmlConverter.XmlToTable(result);
			}
			catch {
				DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
				throw new Exception(exception.Message);
			}
		}

		public static DataTable ProcessGetTableLow(DtoGetTableLow dto) {
			string result=SendAndReceive(dto);
			try {
				return XmlConverter.XmlToTable(result);
			}
			catch {
				DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
				throw new Exception(exception.Message);
			}
		}

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

		///<summary></summary>
		public static int ProcessGetInt(DtoGetInt dto) {
			string result=SendAndReceive(dto);//this might throw an exception if server unavailable
			try {
				return PIn.PInt(result);
			}
			catch {
				DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
				throw new Exception(exception.Message);
			}
		}

		///<summary></summary>
		public static void ProcessGetVoid(DtoGetVoid dto) {
			string result=SendAndReceive(dto);//this might throw an exception if server unavailable
			if(result!="0"){
				DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
				throw new Exception(exception.Message);
			}
		}

		///<summary></summary>
		public static T ProcessGetObject<T>(DtoGetObject dto) {
			string result=SendAndReceive(dto);//this might throw an exception if server unavailable
			try {
				return XmlConverter.Deserialize<T>(result);
				/*
				XmlSerializer serializer=new XmlSerializer(typeof(T));
					//Type.GetType("OpenDentBusiness."+dto.ObjectType));
				StringReader strReader=new StringReader(result);
				XmlReader xmlReader=XmlReader.Create(strReader);
				object obj=serializer.Deserialize(xmlReader);
				strReader.Close();
				xmlReader.Close();
				return (T)obj;*/
			}
			catch {
				DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
				throw new Exception(exception.Message);
			}
		}

		///<summary></summary>
		public static string ProcessGetString(DtoGetString dto) {
			string result=SendAndReceive(dto);//this might throw an exception if server unavailable
			DtoException exception;
			try {
				exception=(DtoException)DataTransferObject.Deserialize(result);
			}
			catch {
				return result;
			}
			throw new Exception(exception.Message);
		}

		///<summary></summary>
		public static bool ProcessGetBool(DtoGetBool dto) {
			string result=SendAndReceive(dto);
			if(result=="True") {
				return true;
			}
			if(result=="False") {
				return false;
			}
			DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
			throw new Exception(exception.Message);
		}

		internal static string SendAndReceive(DataTransferObject dto){
			string dtoString=dto.Serialize();
			OpenDentalServer.ServiceMain service=new OpenDentBusiness.OpenDentalServer.ServiceMain();
			service.Url=ServerURI;
			string result=service.ProcessRequest(dtoString);
			return result;
		}

		
	}

}
