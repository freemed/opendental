using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class InsFilingCode {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.InsFilingCode insfilingcode) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<InsFilingCode>");
			sb.Append("<InsFilingCodeNum>").Append(insfilingcode.InsFilingCodeNum).Append("</InsFilingCodeNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(insfilingcode.Descript)).Append("</Descript>");
			sb.Append("<EclaimCode>").Append(SerializeStringEscapes.EscapeForXml(insfilingcode.EclaimCode)).Append("</EclaimCode>");
			sb.Append("<ItemOrder>").Append(insfilingcode.ItemOrder).Append("</ItemOrder>");
			sb.Append("</InsFilingCode>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.InsFilingCode Deserialize(string xml) {
			OpenDentBusiness.InsFilingCode insfilingcode=new OpenDentBusiness.InsFilingCode();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "InsFilingCodeNum":
							insfilingcode.InsFilingCodeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Descript":
							insfilingcode.Descript=reader.ReadContentAsString();
							break;
						case "EclaimCode":
							insfilingcode.EclaimCode=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							insfilingcode.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return insfilingcode;
		}


	}
}