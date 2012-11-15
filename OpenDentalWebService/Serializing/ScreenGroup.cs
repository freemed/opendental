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
					}
				}
			}
			return screengroup;
		}


	}
}