using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using OpenDental.DataAccess;
using System.Diagnostics;

namespace OpenDentBusiness {
	///<summary>Provides a base class for the many types of DTO classes that we will need.  A DTO class is a simple data storage object.  A DTO is the only format accepted by OpenDentBusiness.dll.</summary>
	public abstract class DataTransferObject {
			
		public byte[] Serialize(){
			XmlSerializer serializer = new XmlSerializer(this.GetType());
			byte[] retVal = null;
			using(MemoryStream memStream=new MemoryStream()){
				serializer.Serialize(memStream,this);
				retVal=memStream.ToArray();
			}
			Debug.Assert(retVal[retVal.Length - 1] != '\0', "The serialized data cannot contain NULL characters.");
			return retVal;
		}

		public static DataTransferObject Deserialize(byte[] data) {
			Debug.Assert(data[data.Length - 1] != '\0', "The serialized data cannot contain NULL characters.");
			MemoryStream memStream=new MemoryStream(data);
			XmlDocument doc=new XmlDocument();
			doc.Load(memStream);
			XmlNode node=doc.SelectSingleNode("/*");
			// Get the type of the object that was serialized.
			// In case of a generic DTO, the type is FactoryTransferObject<T>, and we need special logic to handle that.
			Type type;
			if(node.Name.StartsWith("FactoryTransferObjectOf")) {
				type = typeof(FactoryTransferObject<>);
				int length = "FactoryTransferObjectOf".Length;
				string genericArgument = "OpenDentBusiness." + node.Name.Substring(length);
				Type genericType = Type.GetType(genericArgument);
				type = type.MakeGenericType(genericType);
			}
			else {
				type = Type.GetType("OpenDentBusiness." + node.Name);
			}
			memStream=new MemoryStream(data);//resets to beginning of stream
			XmlSerializer serializer = new XmlSerializer(type);
			DataTransferObject retVal=(DataTransferObject)serializer.Deserialize(memStream);		
			memStream.Close();
			return retVal;
		}
	}

	///<summary>This is used for initial login.</summary>
	public class DtoLogin:DtoCommandBase {
		public string Database;
		public string OdUser;
		public string OdPassHash;
	}

	///<summary>All commands should inherit from this rather than directly from the DTO.  This kind of DTO will trigger a DtoServerAck (int) instead of a Dataset.</summary>
	public class DtoCommandBase:DataTransferObject {
	}

	///<summary>All queries should inherit from this rather than directly from the DTO.  This kind of DTO will trigger a Dataset or DataTable result.</summary>
	public class DtoQueryBase:DataTransferObject {
	}

	///<summary>This DTO is planned to replace all the DtoQueryBase types.  Having multiple DTO types takes too long to program, so there should only be one type which is used for everything.  We will also eventually move to simpler XML representation of the datasets instead of using the dotNet serialization.</summary>
	public class DtoGetDS:DataTransferObject{
		///<summary>This is the name of the method that we need to call.  "Class_Method" format.</summary>
		public MethodNameDS MethodNameDS;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public object[] Parameters;
	}

	///<summary></summary>
	public class DtoGetTable:DataTransferObject{
		///<summary>This is the name of the method that we need to call.  "Class_Method" format.</summary>
		public MethodNameTable MethodNameTable;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public object[] Parameters;
	}

	public class DtoGeneralGetTable:DtoQueryBase {
		public string Command;
	}

	public class DtoGeneralGetTableLow:DtoQueryBase {
		public string Command;
	}

	public class DtoGeneralGetDataSet:DtoQueryBase {
		public string Commands;
	}

	public class DtoGeneralNonQ:DtoCommandBase {
		public string Command;
		public bool GetInsertID;
	}

	///<summary>IDorRows will be the InsertID for insert type commands.  For some other commands, it will be the rows changed, and for some commands, it will just be 0.</summary>
	public class DtoServerAck:DataTransferObject {
		public int IDorRows;
	}

	///<summary>OpenDentBusiness and all the DA classes are designed to throw an exception if something goes wrong.  If using OpenDentBusiness through the remote server, then the server catches the exception and passes it back to the main program using this DTO.  The client then turns it back into an exception so that it behaves just like if OpenDentBusiness was getting called locally.</summary>
	public class DtoException{
		public string Message;
	}

	



}
