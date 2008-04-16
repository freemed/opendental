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
	}
}
