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
			XmlSerializer serializer=new XmlSerializer(typeof(DataTable));
			serializer.Serialize(xmlWriter,table);
			xmlWriter.Close();
			return strBuild.ToString();
		}
	}
}
