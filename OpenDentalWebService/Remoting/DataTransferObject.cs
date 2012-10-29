using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace OpenDentalWebService {
	///<summary>Provides a base class for DTO classes.  A DTO class is a simple data storage object.</summary>
	public abstract class DataTransferObject {
		///<summary></summary>
		public Credentials Credentials;
		///<summary>This is the name of the method that we need to call.  "Class.Method" format.  Not used with GetTableLow.</summary>
		public string MethodName;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public List<DtoObject> Params;
		///<summary>This is a list of parameter types that we are passing.  This array must directly match the count of Params.</summary>
		public List<String> ParamTypes;
		///<summary>Used to quickly tell the type of DataTransferObject.</summary>
		public string Type;

		public string Serialize() {
			StringBuilder strBuild=new StringBuilder();
			//XmlWriter writer=XmlWriter.Create(strBuild);
			//XmlSerializer serializer=GetDtoForType(this.Type);
			//serializer.Serialize(writer,this);
			//writer.Close();
			return strBuild.ToString();
		}

		public static DataTransferObject Deserialize(string data) {
			StringReader strReader=new StringReader(data);
			XmlTextReader reader=new XmlTextReader(strReader);
			XmlDocument document=new XmlDocument();
			DataTransferObject dto=null;
			string strNodeName="";
			while(reader.Read()) {
				if(reader.NodeType!=XmlNodeType.Element) {
					continue;
				}
				strNodeName=reader.Name;
				break;
			}
			dto=GetDtoForType(strNodeName);
			dto.Type=strNodeName;
			if(dto==null) {//This should never happen... Not sure if returning null is best solution.
				CloseReaders(strReader,reader);
				return null;
			}
			try {//Manually deserialize dto object without using XmlSerializer.  Old code: DataTransferObject retVal=(DataTransferObject)serializer.Deserialize(reader);
				document.Load(reader);
				XPathNavigator navigator=document.CreateNavigator();
				XPathNavigator nav;
				#region Credentials
				nav=navigator.SelectSingleNode("//Credentials");
				if(nav==null) {//There will always be Credentials.
					CloseReaders(strReader,reader);
					return null;
				}
				Credentials creds=new Credentials();
				creds.Username=nav.SelectSingleNode("UserName").Value;
				creds.Password=nav.SelectSingleNode("Password").Value;
				dto.Credentials=creds;
				#endregion
				#region MethodName
				nav=navigator.SelectSingleNode("//MethodName");
				if(nav==null) {//There will always be a Method to call.  Otherwise, what is the point of this dto.
					CloseReaders(strReader,reader);
					return null;
				}
				dto.MethodName=nav.Value;
				#endregion
				#region Params
				nav=navigator.SelectSingleNode("//Params");
				if(nav==null) {//There will always be a params node but there might be no params.
					CloseReaders(strReader,reader);
					return null;
				}
				if(nav.Value!="") {//Parameters are present.  Deserialize them.
					SetParamsAndParamTypes(nav,dto);
				}
				#endregion
			}
			catch(Exception) {
				CloseReaders(strReader,reader);
				return null;
			}
			CloseReaders(strReader,reader);
			return dto;
		}

		///<summary>Returns the correct DataTransferObject for the type of object passed in.</summary>
		private static DataTransferObject GetDtoForType(string dtoType) {
			DataTransferObject dto=null;
			switch(dtoType) {
				case "DtoGetDS":
					dto=new DtoGetDS();
					break;
				case "DtoGetTable":
					dto=new DtoGetTable();
					break;
				case "DtoGetTableLow":
					dto=new DtoGetTableLow();
					break;
				case "DtoGetLong":
					dto=new DtoGetLong();
					break;
				case "DtoGetInt":
					dto=new DtoGetInt();
					break;
				case "DtoGetVoid":
					dto=new DtoGetVoid();
					break;
				case "DtoGetObject":
					dto=new DtoGetObject();
					break;
				case "DtoGetString":
					dto=new DtoGetString();
					break;
				case "DtoGetBool":
					dto=new DtoGetBool();
					break;
				case "DtoException":
					dto=new DtoException();
					break;
			}
			return dto;
		}

		///<summary>Correctly sets Params AND ParamTypes on the dto object based on the navigator passed in.</summary>
		private static void SetParamsAndParamTypes(XPathNavigator nav,DataTransferObject dto) {
			//The Params node is going to be a list of DtoObjects. Each has </TypeName> and </Obj>.  TypeName will give us the type and Obj will contain the serialized object.
			//Loop through the nodes and call a function that will call each corresponding S class to deserialize the desired object correctly.
			for(int i=0;i<5;i++) {//Not going to be a for loop.
				DtoObject dtoObj=new DtoObject();
				string typeName="";//Read the xml to figure out the type of object.
				dtoObj.TypeName=typeName;
				dto.ParamTypes.Add(typeName);
				//Write code to strip out JUST the object and then call the get method.
				//(Special logic most likely needed for List<> objects and primitives.)
				string xml="";
				object obj=DtoMethods.CallClassDeserializer(dtoObj.TypeName,xml);
				dto.Params.Add(dtoObj);
			}
		}

		///<summary>Helper function to close the string and xml readers from the Deserialize method.</summary>
		private static void CloseReaders(StringReader strReader,XmlTextReader reader) {
			if(strReader!=null) {
				strReader.Close();
			}
			if(reader!=null) {
				reader.Close();
			}
		}


	}

	///<summary>The username and password are internal to OD.  They are not the MySQL username and password.</summary>
	public class Credentials {
		public string Username;
		///<summary>If using Ecw, then the password is actually just a hash because we don't know the real password.</summary>
		public string Password;
	}

	///<summary></summary>
	public class DtoGetDS:DataTransferObject {

	}

	///<summary></summary>
	public class DtoGetTable:DataTransferObject {

	}

	///<summary></summary>
	public class DtoGetTableLow:DataTransferObject {

	}

	///<summary>Gets a long.</summary>
	public class DtoGetLong:DataTransferObject {

	}

	///<summary>Gets an int.</summary>
	public class DtoGetInt:DataTransferObject {

	}

	///<summary>Used when the return type is void.  It will still return 0 to ack.</summary>
	public class DtoGetVoid:DataTransferObject {

	}

	///<summary>Gets an object which must be serializable.  Calling code will convert object to specific type.</summary>
	public class DtoGetObject:DataTransferObject {
		///<summary>This is the "FullName" string representation of the type of object that we expect back as a result.  Examples: System.Int32, OpenDentBusiness.Patient, OpenDentBusiness.Patient[], List&lt;OpenDentBusiness.Patient&gt;.  DataTable and DataSet not allowed.</summary>
		public string ObjectType;
	}

	///<summary>Gets a simple string.</summary>
	public class DtoGetString:DataTransferObject {

	}

	///<summary>Gets a bool.</summary>
	public class DtoGetBool:DataTransferObject {

	}

	///<summary>OpenDentBusiness and all the DA classes are designed to throw an exception if something goes wrong.  If using OpenDentBusiness through the remote server, then the server catches the exception and passes it back to the main program using this DTO.  The client then turns it back into an exception so that it behaves just as if OpenDentBusiness was getting called locally.</summary>
	public class DtoException:DataTransferObject {
		public string Message;
	}





}
