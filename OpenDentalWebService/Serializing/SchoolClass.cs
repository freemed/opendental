using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SchoolClass {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SchoolClass schoolclass) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SchoolClass>");
			sb.Append("<SchoolClassNum>").Append(schoolclass.SchoolClassNum).Append("</SchoolClassNum>");
			sb.Append("<GradYear>").Append(schoolclass.GradYear).Append("</GradYear>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(schoolclass.Descript)).Append("</Descript>");
			sb.Append("</SchoolClass>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SchoolClass Deserialize(string xml) {
			OpenDentBusiness.SchoolClass schoolclass=new OpenDentBusiness.SchoolClass();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SchoolClassNum":
							schoolclass.SchoolClassNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "GradYear":
							schoolclass.GradYear=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Descript":
							schoolclass.Descript=reader.ReadContentAsString();
							break;
					}
				}
			}
			return schoolclass;
		}


	}
}