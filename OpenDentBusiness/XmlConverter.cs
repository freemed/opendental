using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
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

		///<summary>Should accept any type, including simple types, OD types, Arrays, Lists, and arrays of DtoObject.  But not DataTable or DataSet.  If we find a type that isn't supported, then we need to add it.</summary>
		public static T Deserialize<T>(string xmlData) {
			Type type = typeof(T);
			StringReader strReader=new StringReader(xmlData);
			XmlReader reader=XmlReader.Create(strReader);
			XmlSerializer serializer = new XmlSerializer(type);
			T retVal=(T)serializer.Deserialize(reader);
			strReader.Close();
			reader.Close();
			return retVal;
		}
	
		public static string TableToXml(DataTable table){
			StringBuilder strBuild=new StringBuilder();
			XmlWriter xmlWriter=XmlWriter.Create(strBuild);
			if(table.TableName==""){
				throw new ApplicationException("OpenDentBusiness.WebServer.XmlConverter.TableToXml requires a tablename matching the type of the collection.");
			}
			table.WriteXml(xmlWriter,XmlWriteMode.WriteSchema);
			xmlWriter.Close();
			return strBuild.ToString();
		}

		public static string DsToXml(DataSet ds){
			StringBuilder strBuild=new StringBuilder();
			XmlWriter xmlWriter=XmlWriter.Create(strBuild);
			ds.WriteXml(xmlWriter);
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
