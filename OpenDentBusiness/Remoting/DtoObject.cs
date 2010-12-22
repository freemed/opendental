using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	///<summary>Packages any object with a TypeName so that it can be serialized and deserialized better.</summary>
	public class DtoObject:IXmlSerializable {
		///<summary>Fully qualified name, including the namespace but not the assembly.  Examples: System.Int32, OpenDentBusiness.Patient, OpenDentBusiness.Patient[], List&lt;OpenDentBusiness.Patient&gt;.  When the xml element is created for the Obj, the namespace is not included.  So this field properly stores it.</summary>
		public string TypeName;
		///<summary>The actual object.</summary>
		public object Obj;

		///<summary>Empty constructor as required by IXmlSerializable</summary>
		public DtoObject() {
		}

		///<summary>This is the constructor that should be used normally because it automatically creates the TypeName.</summary>
		public DtoObject(object obj,Type objType) {
			Obj=obj;
			//Type type=obj.GetType();
			//This will eventually become much more complex:
			//Arrays automatically become "ArrayOf..." and serialize just fine, with TypeName=...[]
			//Lists:
			if(objType.IsGenericType) {
				Type listType=objType.GetGenericArguments()[0];
				TypeName="List<"+listType.FullName+">";
			}
			else {
				TypeName=objType.FullName;
			}
		}

		public void WriteXml(XmlWriter writer) {
			/* we want the result to look like this:
			<TypeName>Patient</TypeName>
			<Obj>
				<Patient>
					<LName>Smith</LName>
					<PatNum>22</PatNum>
					<IsGuar>True</IsGuar>
				</Patient>
			</Obj>
			*/
			writer.WriteStartElement("TypeName");
			writer.WriteString(TypeName);
			writer.WriteEndElement();//TypeName
			writer.WriteStartElement("Obj");
			if(TypeName=="System.Drawing.Color") {
				XmlSerializer serializer = new XmlSerializer(typeof(int));
				serializer.Serialize(writer,((Color)Obj).ToArgb());
			}
			else {
				string assemb=Assembly.GetAssembly(typeof(Db)).FullName;
				Type type=ConvertNameToType(TypeName,assemb);
				XmlSerializer serializer = new XmlSerializer(type);
				serializer.Serialize(writer,Obj);
			}
			writer.WriteEndElement();//Obj
		}

		public void ReadXml(XmlReader reader) {
			reader.ReadToFollowing("TypeName");
			reader.ReadStartElement("TypeName");
			TypeName=reader.ReadString();
			reader.ReadEndElement();//TypeName
			while(reader.NodeType!=XmlNodeType.Element) {
				reader.Read();//gets rid of whitespace if in debug mode.
			}
			reader.ReadStartElement("Obj");
			while(reader.NodeType!=XmlNodeType.Element) {
				reader.Read();
			}
			string strObj=reader.ReadOuterXml();
			//now get the reader to the correct location
			while(reader.NodeType!=XmlNodeType.EndElement) {
				reader.Read();
			}
			reader.ReadEndElement();//Obj
			while(reader.NodeType!=XmlNodeType.EndElement) {
				reader.Read();
			}
			reader.ReadEndElement();//DtoObject
			Type type=null;
			if(TypeName.StartsWith("List<")) {
				Type typeGen=Type.GetType(TypeName.Substring(5,TypeName.Length-6));
				Type typeList=typeof(List<>);
				type=typeList.MakeGenericType(typeGen);
			}
			else if(TypeName=="System.Drawing.Color") {
				type=typeof(int);
			}
			else {
				//This works fine for non-system types as well without specifying the assembly,
				//because we are already in the OpenDentBusiness assembly.
				type=Type.GetType(TypeName);
			}
			XmlSerializer serializer = new XmlSerializer(type);
			XmlReader reader2=XmlReader.Create(new StringReader(strObj));
			if(TypeName=="System.Drawing.Color") {
				Obj=Color.FromArgb((int)serializer.Deserialize(reader2));
			}
			else {
				Obj=serializer.Deserialize(reader2);
			}
				//Convert.ChangeType(serializer.Deserialize(reader2),type);
		}

		///<summary>Required by IXmlSerializable</summary>
		public XmlSchema GetSchema() {
			return (null);
		}

		///<summary>We must pass in a matching array of types for situations where nulls are used in parameters.  Otherwise, we won't know the parameter type.</summary>
		public static DtoObject[] ConstructArray(object[] objArray,Type[] objTypes) {
			DtoObject[] retVal=new DtoObject[objArray.Length];
			for(int i=0;i<objArray.Length;i++) {
				retVal[i]=new DtoObject(objArray[i],objTypes[i]);
			}
			return retVal;
		}

		public static object[] GenerateObjects(DtoObject[] parameters) {
			object[] retVal=new object[parameters.Length];
			for(int i=0;i<parameters.Length;i++) {
				retVal[i]=parameters[i].Obj;
			}
			return retVal;
		}

		public static Type[] GenerateTypes(DtoObject[] parameters,string assemb) {
			Type[] retVal=new Type[parameters.Length];
			for(int i=0;i<parameters.Length;i++) {
				retVal[i]=ConvertNameToType(parameters[i].TypeName,assemb);
			}
			return retVal;
		}

		private static Type ConvertNameToType(string FullName,string assemb) {
			Type typeObj=null;
			if(FullName.StartsWith("List<")) {
				string strTypeGenName=FullName.Substring(5,FullName.Length-6);//strips off the List<>
				Type typeGen=null;
				if(strTypeGenName.StartsWith("OpenDentBusiness")) {
					typeGen=Type.GetType(strTypeGenName+","+assemb);
				}
				else {//system types
					typeGen=Type.GetType(strTypeGenName);
				}
				Type typeList=typeof(List<>);
				typeObj=typeList.MakeGenericType(typeGen);
			}
			else if(FullName.StartsWith("OpenDentBusiness")) {
				typeObj=Type.GetType(FullName+","+assemb);
			}
			else if(FullName=="System.Drawing.Color") {
				typeObj=typeof(Color);
			}
			else {//system types
				typeObj=Type.GetType(FullName);
			}
			return typeObj;
		}

	}
}
