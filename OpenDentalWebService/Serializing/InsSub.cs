using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class InsSub {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.InsSub inssub) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<InsSub>");
			sb.Append("<InsSubNum>").Append(inssub.InsSubNum).Append("</InsSubNum>");
			sb.Append("<PlanNum>").Append(inssub.PlanNum).Append("</PlanNum>");
			sb.Append("<Subscriber>").Append(inssub.Subscriber).Append("</Subscriber>");
			sb.Append("<DateEffective>").Append(inssub.DateEffective.ToString()).Append("</DateEffective>");
			sb.Append("<DateTerm>").Append(inssub.DateTerm.ToString()).Append("</DateTerm>");
			sb.Append("<ReleaseInfo>").Append((inssub.ReleaseInfo)?1:0).Append("</ReleaseInfo>");
			sb.Append("<AssignBen>").Append((inssub.AssignBen)?1:0).Append("</AssignBen>");
			sb.Append("<SubscriberID>").Append(SerializeStringEscapes.EscapeForXml(inssub.SubscriberID)).Append("</SubscriberID>");
			sb.Append("<BenefitNotes>").Append(SerializeStringEscapes.EscapeForXml(inssub.BenefitNotes)).Append("</BenefitNotes>");
			sb.Append("<SubscNote>").Append(SerializeStringEscapes.EscapeForXml(inssub.SubscNote)).Append("</SubscNote>");
			sb.Append("</InsSub>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.InsSub Deserialize(string xml) {
			OpenDentBusiness.InsSub inssub=new OpenDentBusiness.InsSub();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "InsSubNum":
							inssub.InsSubNum=reader.ReadContentAsLong();
							break;
						case "PlanNum":
							inssub.PlanNum=reader.ReadContentAsLong();
							break;
						case "Subscriber":
							inssub.Subscriber=reader.ReadContentAsLong();
							break;
						case "DateEffective":
							inssub.DateEffective=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateTerm":
							inssub.DateTerm=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ReleaseInfo":
							inssub.ReleaseInfo=reader.ReadContentAsString()!="0";
							break;
						case "AssignBen":
							inssub.AssignBen=reader.ReadContentAsString()!="0";
							break;
						case "SubscriberID":
							inssub.SubscriberID=reader.ReadContentAsString();
							break;
						case "BenefitNotes":
							inssub.BenefitNotes=reader.ReadContentAsString();
							break;
						case "SubscNote":
							inssub.SubscNote=reader.ReadContentAsString();
							break;
					}
				}
			}
			return inssub;
		}


	}
}