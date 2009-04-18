using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using OpenDentBusiness.DataAccess;
using System.Diagnostics;

namespace OpenDentBusiness {
	///<summary>Provides a base class for the many types of DTO classes that we will need.  A DTO class is a simple data storage object.  A DTO is the only format accepted by OpenDentBusiness.dll.</summary>
	public abstract class DataTransferObject {


		/*	
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
		}*/

		public string Serialize(){
			StringBuilder strBuild=new StringBuilder();
			XmlWriter writer=XmlWriter.Create(strBuild);
			XmlSerializer serializer = new XmlSerializer(this.GetType());
			serializer.Serialize(writer,this);
			writer.Close();
			return strBuild.ToString();
		}

		public static DataTransferObject Deserialize(string data) {
			StringReader strReader=new StringReader(data);
			XmlReader reader=XmlReader.Create(strReader);
			string strNodeName="";
			while(reader.Read()){
				if(reader.NodeType!=XmlNodeType.Element){
					continue;
				}
				strNodeName=reader.Name;
				break;
			}
			strReader.Close();
			reader.Close();
			Type type = Type.GetType("OpenDentBusiness." +strNodeName);
			StringReader strReader2=new StringReader(data);
			XmlReader reader2=XmlReader.Create(strReader2);
			XmlSerializer serializer = new XmlSerializer(type);
			DataTransferObject retVal=(DataTransferObject)serializer.Deserialize(reader2);
			strReader2.Close();
			reader2.Close();
			return retVal;
		}
	}

	/*
	///<summary>This is used for initial login.</summary>
	public class DtoLogin:DtoCommandBase {
		public string OdUser;
		public string OdPassHash;
	}

	///<summary>All commands should inherit from this rather than directly from the DTO.  This kind of DTO will trigger a DtoServerAck (int) instead of a Dataset.</summary>
	public class DtoCommandBase:DataTransferObject {
	}

	///<summary>All queries should inherit from this rather than directly from the DTO.  This kind of DTO will trigger a Dataset or DataTable result.</summary>
	public class DtoQueryBase:DataTransferObject {
	}*/

	///<summary>The username and password are internal to OD.  They are not the MySQL username and password.</summary>
	public class Credentials{
		public string Username;
		public string PassHash;
	}

	///<summary>This DTO is planned to replace all the DtoQueryBase types.  Having multiple DTO types takes too long to program, so there should only be one type which is used for everything.  We will also eventually move to simpler XML representation of the datasets instead of using the dotNet serialization.</summary>
	public class DtoGetDS:DataTransferObject{
		///<summary>Always passed with new web service.    Never null.</summary>
		public Credentials Credentials;
		///<summary>This is the name of the method that we need to call.  "Class.Method" format.</summary>
		public string MethodName;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public object[] Parameters;
	}

	///<summary></summary>
	public class DtoGetTable:DataTransferObject{
		///<summary>Always passed with new web service.  Never null.</summary>
		public Credentials Credentials;
		///<summary>This is the name of the method that we need to call.  "Class.Method" format.</summary>
		public string MethodName;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public object[] Parameters;
	}

	///<summary>Gets a simple string.</summary>
	public class DtoGetString:DataTransferObject{
		///<summary>Always passed with new web service.  Never null.</summary>
		public Credentials Credentials;
		///<summary>This is the name of the method that we need to call.  "Class.Method" format.</summary>
		public string MethodName;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public object[] Parameters;
	}

	///<summary>Gets an object which must be serializable.  Calling code will convert object to specific type.</summary>
	public class DtoGetObject:DataTransferObject{
		///<summary>Always passed with new web service.  Never null.</summary>
		public Credentials Credentials;
		///<summary>This is the name of the method that we need to call.  "Class.Method" format.</summary>
		public string MethodName;
		///<summary>This is the string representation of the type of object that we expect back as a result.</summary>
		public string ObjectType;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public object[] Parameters;
	}

	///<summary>Sends a command.  A command cannot contain a query.  This is more like an RPC.  The result will be an ack or exception.  The return type for these classes is always void.</summary>
	public class DtoSendCmd:DataTransferObject{
		///<summary>Always passed with new web service.  Never null.</summary>
		public Credentials Credentials;
		///<summary>This is the name of the method that we need to call.  "Class.Method" format.</summary>
		public string MethodName;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public object[] Parameters;
	}



	//public class DtoGeneralGetTableLow:DtoQueryBase {
	//	public string Command;
	//}
	
	/*
	public class DtoGeneralGetTable:DtoQueryBase {
		public string Command;
	}

	
	public class DtoGeneralGetDataSet:DtoQueryBase {
		public string Commands;
	}

	public class DtoGeneralNonQ:DtoCommandBase {
		public string Command;
		public bool GetInsertID;
	}*/

	///<summary>IDorRows will be the InsertID for insert type commands.  For some other commands, it will be the rows changed, and for some commands, it will just be 0.</summary>
	public class DtoServerAck:DataTransferObject {
		public int IDorRows;
	}

	///<summary>OpenDentBusiness and all the DA classes are designed to throw an exception if something goes wrong.  If using OpenDentBusiness through the remote server, then the server catches the exception and passes it back to the main program using this DTO.  The client then turns it back into an exception so that it behaves just as if OpenDentBusiness was getting called locally.</summary>
	public class DtoException:DataTransferObject {
		public string Message;
	}

	



}
