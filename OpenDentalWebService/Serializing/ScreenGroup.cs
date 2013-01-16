using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ScreenGroup {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ScreenGroup screengroup) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ScreenGroup>");
			sb.Append("<ScreenGroupNum>").Append(screengroup.ScreenGroupNum).Append("</ScreenGroupNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(screengroup.Description)).Append("</Description>");
			sb.Append("<SGDate>").Append(screengroup.SGDate.ToString("yyyyMMddHHmmss")).Append("</SGDate>");
			sb.Append("<ProvName>").Append(SerializeStringEscapes.EscapeForXml(screengroup.ProvName)).Append("</ProvName>");
			sb.Append("<ProvNum>").Append(screengroup.ProvNum).Append("</ProvNum>");
			sb.Append("<PlaceService>").Append((int)screengroup.PlaceService).Append("</PlaceService>");
			sb.Append("<County>").Append(SerializeStringEscapes.EscapeForXml(screengroup.County)).Append("</County>");
			sb.Append("<GradeSchool>").Append(SerializeStringEscapes.EscapeForXml(screengroup.GradeSchool)).Append("</GradeSchool>");
			sb.Append("</ScreenGroup>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ScreenGroup Deserialize(string xml) {
			OpenDentBusiness.ScreenGroup screengroup=new OpenDentBusiness.ScreenGroup();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ScreenGroupNum":
							screengroup.ScreenGroupNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							screengroup.Description=reader.ReadContentAsString();
							break;
						case "SGDate":
							screengroup.SGDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ProvName":
							screengroup.ProvName=reader.ReadContentAsString();
							break;
						case "ProvNum":
							screengroup.ProvNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PlaceService":
							screengroup.PlaceService=(OpenDentBusiness.PlaceOfService)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "County":
							screengroup.County=reader.ReadContentAsString();
							break;
						case "GradeSchool":
							screengroup.GradeSchool=reader.ReadContentAsString();
							break;
					}
				}
			}
			return screengroup;
		}


	}
}