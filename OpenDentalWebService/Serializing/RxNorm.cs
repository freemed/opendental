using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RxNorm {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RxNorm rxnorm) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RxNorm>");
			sb.Append("<RxNormNum>").Append(rxnorm.RxNormNum).Append("</RxNormNum>");
			sb.Append("<RxCui>").Append(SerializeStringEscapes.EscapeForXml(rxnorm.RxCui)).Append("</RxCui>");
			sb.Append("<MmslCode>").Append(SerializeStringEscapes.EscapeForXml(rxnorm.MmslCode)).Append("</MmslCode>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(rxnorm.Description)).Append("</Description>");
			sb.Append("</RxNorm>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RxNorm Deserialize(string xml) {
			OpenDentBusiness.RxNorm rxnorm=new OpenDentBusiness.RxNorm();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RxNormNum":
							rxnorm.RxNormNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "RxCui":
							rxnorm.RxCui=reader.ReadContentAsString();
							break;
						case "MmslCode":
							rxnorm.MmslCode=reader.ReadContentAsString();
							break;
						case "Description":
							rxnorm.Description=reader.ReadContentAsString();
							break;
					}
				}
			}
			return rxnorm;
		}


	}
}