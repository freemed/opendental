using System;
using System.Collections.Generic;
using System.Data;
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
			//xmlWriter.WriteStartElement("DataSet");
			//DataTable table;
			//for(int i=0;i<ds.Tables.Count;i++){
				//table=ds.Tables[i];
				//if(table.TableName==""){
				//	throw new ApplicationException("OpenDentBusiness.WebServer.XmlConverter.TableToXml requires a tablename matching the type of the collection.");
				//}
				//table.WriteXml(xmlWriter);
			//}
			ds.WriteXml(xmlWriter);
			//xmlWriter.WriteEndElement();
			xmlWriter.Close();
			return strBuild.ToString();
		}





	}
}
