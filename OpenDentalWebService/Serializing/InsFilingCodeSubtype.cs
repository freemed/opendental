using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class InsFilingCodeSubtype {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.InsFilingCodeSubtype insfilingcodesubtype) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<InsFilingCodeSubtype>");
			sb.Append("<InsFilingCodeSubtypeNum>").Append(insfilingcodesubtype.InsFilingCodeSubtypeNum).Append("</InsFilingCodeSubtypeNum>");
			sb.Append("<InsFilingCodeNum>").Append(insfilingcodesubtype.InsFilingCodeNum).Append("</InsFilingCodeNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(insfilingcodesubtype.Descript)).Append("</Descript>");
			sb.Append("</InsFilingCodeSubtype>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.InsFilingCodeSubtype Deserialize(string xml) {
			OpenDentBusiness.InsFilingCodeSubtype insfilingcodesubtype=new OpenDentBusiness.InsFilingCodeSubtype();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "InsFilingCodeSubtypeNum":
							insfilingcodesubtype.InsFilingCodeSubtypeNum=reader.ReadContentAsLong();
							break;
						case "InsFilingCodeNum":
							insfilingcodesubtype.InsFilingCodeNum=reader.ReadContentAsLong();
							break;
						case "Descript":
							insfilingcodesubtype.Descript=reader.ReadContentAsString();
							break;
					}
				}
			}
			return insfilingcodesubtype;
		}


	}
}