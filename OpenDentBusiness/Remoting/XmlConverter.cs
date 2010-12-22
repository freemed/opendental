using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	public class XmlConverter {
		///<summary>Should accept any type, including simple types, OD types, Arrays, Lists, and arrays of DtoObject.  But not DataTable or DataSet.  If we find a type that isn't supported, then we need to add it.</summary>
		public static string Serialize<T>(T obj) {
			StringBuilder strBuild=new StringBuilder();
			#if DEBUG
				XmlWriterSettings settings=new XmlWriterSettings();
				settings.Indent=true;
				settings.IndentChars="   ";
				//using the constructor decreases performance and leads to memory leaks.
				//But it makes the xml much more readable
				XmlWriter writer=XmlWriter.Create(strBuild,settings);
			#else
				XmlWriter writer=XmlWriter.Create(strBuild);
			#endif
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(writer,obj);
			writer.Close();
			return strBuild.ToString();
		}

		///<summary>For late binding of class type.</summary>
		public static string Serialize(Type classType,object obj) {
			StringBuilder strBuild=new StringBuilder();
			#if DEBUG
				XmlWriterSettings settings=new XmlWriterSettings();
				settings.Indent=true;
				settings.IndentChars="   ";
				//using the constructor decreases performance and leads to memory leaks.
				//But it makes the xml much more readable
				XmlWriter writer=XmlWriter.Create(strBuild,settings);
			#else
				XmlWriter writer=XmlWriter.Create(strBuild);
			#endif
			XmlSerializer serializer;
			if(classType==typeof(Color)) {
				serializer = new XmlSerializer(typeof(int));
				serializer.Serialize(writer,((Color)obj).ToArgb());
			}
			else {
				serializer = new XmlSerializer(classType);
				serializer.Serialize(writer,obj);
			}
			writer.Close();
			return strBuild.ToString();
		}

		///<summary>Should accept any type.  Tested types include System types, OD types, Arrays, Lists, arrays of DtoObject, null DataObjectBase, null arrays, null Lists.  But not DataTable or DataSet.  If we find a type that isn't supported, then we need to add it.  Types that are currently unsupported include Arrays of DataObjectBase that contain a null.  Lists that contain nulls are untested and may be an issue for DataObjectBase.</summary>
		public static T Deserialize<T>(string xmlData) {
			Type type = typeof(T);
			/*later.  I don't think arrays will null objects will be an issue.
			if(type.IsArray) {
				Type arrayType=type.GetElementType();
				if(arrayType.BaseType==typeof(DataObjectBase)) {
					//split into items
				}
			}*/
			if(type.IsGenericType) {//List<>
				//because the built-in deserializer does not handle null list<>, but instead returns an empty list.
				//<ArrayOfDocument xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:nil="true" />
				if(Regex.IsMatch(xmlData,"<ArrayOf[^>]*xsi:nil=\"true\"")) {
					return default(T);//null
				}
			}
			StringReader strReader=new StringReader(xmlData);
			XmlReader reader=XmlReader.Create(strReader);
			XmlSerializer serializer;
			T retVal;
			if(type==typeof(Color)) {
				serializer = new XmlSerializer(typeof(int));
				retVal=(T)((object)Color.FromArgb((int)serializer.Deserialize(reader)));
			}
			else {
				serializer = new XmlSerializer(type);
				retVal=(T)serializer.Deserialize(reader);
			}
			strReader.Close();
			reader.Close();
			return retVal;
		}
	
		public static string TableToXml(DataTable table){
			StringBuilder strBuild=new StringBuilder();
			XmlWriter xmlWriter=XmlWriter.Create(strBuild);
			if(table.TableName==""){
			//	throw new ApplicationException("OpenDentBusiness.WebServer.XmlConverter.TableToXml requires a tablename matching the type of the collection.");
				table.TableName="Table";
			}
			table.WriteXml(xmlWriter,XmlWriteMode.WriteSchema);
			xmlWriter.Close();
			return strBuild.ToString();
		}

		public static string DsToXml(DataSet ds){
			StringBuilder strBuild=new StringBuilder();
			XmlWriter xmlWriter=XmlWriter.Create(strBuild);
			ds.WriteXml(xmlWriter,XmlWriteMode.WriteSchema);
			xmlWriter.Close();
			return strBuild.ToString();
		}

		public static DataTable XmlToTable(string xmlData) {
			DataTable table=new DataTable();
			XmlReader xmlReader=XmlReader.Create(new StringReader(xmlData));
			table.ReadXml(xmlReader);
			xmlReader.Close();
			return table;
		}

		public static DataSet XmlToDs(string xmlData) {
			DataSet ds=new DataSet();
			XmlReader xmlReader=XmlReader.Create(new StringReader(xmlData));
			ds.ReadXml(xmlReader);
			xmlReader.Close();
			return ds;
		}
		

	}
}
