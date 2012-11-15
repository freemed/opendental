using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class TreatPlan {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.TreatPlan treatplan) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<TreatPlan>");
			sb.Append("<TreatPlanNum>").Append(treatplan.TreatPlanNum).Append("</TreatPlanNum>");
			sb.Append("<PatNum>").Append(treatplan.PatNum).Append("</PatNum>");
			sb.Append("<DateTP>").Append(treatplan.DateTP.ToString("yyyyMMddHHmmss")).Append("</DateTP>");
			sb.Append("<Heading>").Append(SerializeStringEscapes.EscapeForXml(treatplan.Heading)).Append("</Heading>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(treatplan.Note)).Append("</Note>");
			sb.Append("<Signature>").Append(SerializeStringEscapes.EscapeForXml(treatplan.Signature)).Append("</Signature>");
			sb.Append("<SigIsTopaz>").Append((treatplan.SigIsTopaz)?1:0).Append("</SigIsTopaz>");
			sb.Append("<ResponsParty>").Append(treatplan.ResponsParty).Append("</ResponsParty>");
			sb.Append("</TreatPlan>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.TreatPlan Deserialize(string xml) {
			OpenDentBusiness.TreatPlan treatplan=new OpenDentBusiness.TreatPlan();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TreatPlanNum":
							treatplan.TreatPlanNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							treatplan.PatNum=reader.ReadContentAsLong();
							break;
						case "DateTP":
							treatplan.DateTP=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "Heading":
							treatplan.Heading=reader.ReadContentAsString();
							break;
						case "Note":
							treatplan.Note=reader.ReadContentAsString();
							break;
						case "Signature":
							treatplan.Signature=reader.ReadContentAsString();
							break;
						case "SigIsTopaz":
							treatplan.SigIsTopaz=reader.ReadContentAsString()!="0";
							break;
						case "ResponsParty":
							treatplan.ResponsParty=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return treatplan;
		}


	}
}