using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class HL7DefField {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.HL7DefField hl7deffield) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<HL7DefField>");
			sb.Append("<HL7DefFieldNum>").Append(hl7deffield.HL7DefFieldNum).Append("</HL7DefFieldNum>");
			sb.Append("<HL7DefSegmentNum>").Append(hl7deffield.HL7DefSegmentNum).Append("</HL7DefSegmentNum>");
			sb.Append("<OrdinalPos>").Append(hl7deffield.OrdinalPos).Append("</OrdinalPos>");
			sb.Append("<TableId>").Append(SerializeStringEscapes.EscapeForXml(hl7deffield.TableId)).Append("</TableId>");
			sb.Append("<DataType>").Append((int)hl7deffield.DataType).Append("</DataType>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(hl7deffield.FieldName)).Append("</FieldName>");
			sb.Append("<FixedText>").Append(SerializeStringEscapes.EscapeForXml(hl7deffield.FixedText)).Append("</FixedText>");
			sb.Append("</HL7DefField>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.HL7DefField Deserialize(string xml) {
			OpenDentBusiness.HL7DefField hl7deffield=new OpenDentBusiness.HL7DefField();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "HL7DefFieldNum":
							hl7deffield.HL7DefFieldNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "HL7DefSegmentNum":
							hl7deffield.HL7DefSegmentNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "OrdinalPos":
							hl7deffield.OrdinalPos=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "TableId":
							hl7deffield.TableId=reader.ReadContentAsString();
							break;
						case "DataType":
							hl7deffield.DataType=(OpenDentBusiness.DataTypeHL7)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "FieldName":
							hl7deffield.FieldName=reader.ReadContentAsString();
							break;
						case "FixedText":
							hl7deffield.FixedText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return hl7deffield;
		}


	}
}