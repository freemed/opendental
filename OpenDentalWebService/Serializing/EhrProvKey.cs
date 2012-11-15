using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EhrProvKey {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EhrProvKey ehrprovkey) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EhrProvKey>");
			sb.Append("<EhrProvKeyNum>").Append(ehrprovkey.EhrProvKeyNum).Append("</EhrProvKeyNum>");
			sb.Append("<PatNum>").Append(ehrprovkey.PatNum).Append("</PatNum>");
			sb.Append("<LName>").Append(SerializeStringEscapes.EscapeForXml(ehrprovkey.LName)).Append("</LName>");
			sb.Append("<FName>").Append(SerializeStringEscapes.EscapeForXml(ehrprovkey.FName)).Append("</FName>");
			sb.Append("<ProvKey>").Append(SerializeStringEscapes.EscapeForXml(ehrprovkey.ProvKey)).Append("</ProvKey>");
			sb.Append("<FullTimeEquiv>").Append(ehrprovkey.FullTimeEquiv).Append("</FullTimeEquiv>");
			sb.Append("<Notes>").Append(SerializeStringEscapes.EscapeForXml(ehrprovkey.Notes)).Append("</Notes>");
			sb.Append("<HasReportAccess>").Append((ehrprovkey.HasReportAccess)?1:0).Append("</HasReportAccess>");
			sb.Append("</EhrProvKey>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EhrProvKey Deserialize(string xml) {
			OpenDentBusiness.EhrProvKey ehrprovkey=new OpenDentBusiness.EhrProvKey();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EhrProvKeyNum":
							ehrprovkey.EhrProvKeyNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							ehrprovkey.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "LName":
							ehrprovkey.LName=reader.ReadContentAsString();
							break;
						case "FName":
							ehrprovkey.FName=reader.ReadContentAsString();
							break;
						case "ProvKey":
							ehrprovkey.ProvKey=reader.ReadContentAsString();
							break;
						case "FullTimeEquiv":
							ehrprovkey.FullTimeEquiv=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "Notes":
							ehrprovkey.Notes=reader.ReadContentAsString();
							break;
						case "HasReportAccess":
							ehrprovkey.HasReportAccess=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return ehrprovkey;
		}


	}
}