using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ReqNeeded {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ReqNeeded reqneeded) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ReqNeeded>");
			sb.Append("<ReqNeededNum>").Append(reqneeded.ReqNeededNum).Append("</ReqNeededNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(reqneeded.Descript)).Append("</Descript>");
			sb.Append("<SchoolCourseNum>").Append(reqneeded.SchoolCourseNum).Append("</SchoolCourseNum>");
			sb.Append("<SchoolClassNum>").Append(reqneeded.SchoolClassNum).Append("</SchoolClassNum>");
			sb.Append("</ReqNeeded>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ReqNeeded Deserialize(string xml) {
			OpenDentBusiness.ReqNeeded reqneeded=new OpenDentBusiness.ReqNeeded();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ReqNeededNum":
							reqneeded.ReqNeededNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Descript":
							reqneeded.Descript=reader.ReadContentAsString();
							break;
						case "SchoolCourseNum":
							reqneeded.SchoolCourseNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SchoolClassNum":
							reqneeded.SchoolClassNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return reqneeded;
		}


	}
}