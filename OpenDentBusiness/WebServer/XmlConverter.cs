using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	public class XmlConverter {
		public static string TableToXml(DataTable table){
			StringBuilder strBuild=new StringBuilder();
			XmlWriter xmlWriter=XmlWriter.Create(strBuild);
			if(table.TableName==""){
				throw new ApplicationException("OpenDentBusiness.WebServer.XmlConverter.TableToXml requires a tablename matching the type of the collection.");
			}
			table.WriteXml(xmlWriter);
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
