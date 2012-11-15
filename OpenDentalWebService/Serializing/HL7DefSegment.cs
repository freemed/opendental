using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class HL7DefSegment {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.HL7DefSegment hl7defsegment) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<HL7DefSegment>");
			sb.Append("<HL7DefSegmentNum>").Append(hl7defsegment.HL7DefSegmentNum).Append("</HL7DefSegmentNum>");
			sb.Append("<HL7DefMessageNum>").Append(hl7defsegment.HL7DefMessageNum).Append("</HL7DefMessageNum>");
			sb.Append("<ItemOrder>").Append(hl7defsegment.ItemOrder).Append("</ItemOrder>");
			sb.Append("<CanRepeat>").Append((hl7defsegment.CanRepeat)?1:0).Append("</CanRepeat>");
			sb.Append("<IsOptional>").Append((hl7defsegment.IsOptional)?1:0).Append("</IsOptional>");
			sb.Append("<SegmentName>").Append((int)hl7defsegment.SegmentName).Append("</SegmentName>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(hl7defsegment.Note)).Append("</Note>");
			sb.Append("</HL7DefSegment>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.HL7DefSegment Deserialize(string xml) {
			OpenDentBusiness.HL7DefSegment hl7defsegment=new OpenDentBusiness.HL7DefSegment();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "HL7DefSegmentNum":
							hl7defsegment.HL7DefSegmentNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "HL7DefMessageNum":
							hl7defsegment.HL7DefMessageNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ItemOrder":
							hl7defsegment.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CanRepeat":
							hl7defsegment.CanRepeat=reader.ReadContentAsString()!="0";
							break;
						case "IsOptional":
							hl7defsegment.IsOptional=reader.ReadContentAsString()!="0";
							break;
						case "SegmentName":
							hl7defsegment.SegmentName=(OpenDentBusiness.SegmentNameHL7)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Note":
							hl7defsegment.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return hl7defsegment;
		}


	}
}