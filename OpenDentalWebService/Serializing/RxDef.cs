using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RxDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RxDef rxdef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RxDef>");
			sb.Append("<RxDefNum>").Append(rxdef.RxDefNum).Append("</RxDefNum>");
			sb.Append("<Drug>").Append(SerializeStringEscapes.EscapeForXml(rxdef.Drug)).Append("</Drug>");
			sb.Append("<Sig>").Append(SerializeStringEscapes.EscapeForXml(rxdef.Sig)).Append("</Sig>");
			sb.Append("<Disp>").Append(SerializeStringEscapes.EscapeForXml(rxdef.Disp)).Append("</Disp>");
			sb.Append("<Refills>").Append(SerializeStringEscapes.EscapeForXml(rxdef.Refills)).Append("</Refills>");
			sb.Append("<Notes>").Append(SerializeStringEscapes.EscapeForXml(rxdef.Notes)).Append("</Notes>");
			sb.Append("<IsControlled>").Append((rxdef.IsControlled)?1:0).Append("</IsControlled>");
			sb.Append("<RxCui>").Append(rxdef.RxCui).Append("</RxCui>");
			sb.Append("</RxDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RxDef Deserialize(string xml) {
			OpenDentBusiness.RxDef rxdef=new OpenDentBusiness.RxDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RxDefNum":
							rxdef.RxDefNum=reader.ReadContentAsLong();
							break;
						case "Drug":
							rxdef.Drug=reader.ReadContentAsString();
							break;
						case "Sig":
							rxdef.Sig=reader.ReadContentAsString();
							break;
						case "Disp":
							rxdef.Disp=reader.ReadContentAsString();
							break;
						case "Refills":
							rxdef.Refills=reader.ReadContentAsString();
							break;
						case "Notes":
							rxdef.Notes=reader.ReadContentAsString();
							break;
						case "IsControlled":
							rxdef.IsControlled=reader.ReadContentAsString()!="0";
							break;
						case "RxCui":
							rxdef.RxCui=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return rxdef;
		}


	}
}