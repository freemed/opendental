using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class LabPanel {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.LabPanel labpanel) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<LabPanel>");
			sb.Append("<LabPanelNum>").Append(labpanel.LabPanelNum).Append("</LabPanelNum>");
			sb.Append("<PatNum>").Append(labpanel.PatNum).Append("</PatNum>");
			sb.Append("<RawMessage>").Append(SerializeStringEscapes.EscapeForXml(labpanel.RawMessage)).Append("</RawMessage>");
			sb.Append("<LabNameAddress>").Append(SerializeStringEscapes.EscapeForXml(labpanel.LabNameAddress)).Append("</LabNameAddress>");
			sb.Append("<DateTStamp>").Append(labpanel.DateTStamp.ToString()).Append("</DateTStamp>");
			sb.Append("<SpecimenCondition>").Append(SerializeStringEscapes.EscapeForXml(labpanel.SpecimenCondition)).Append("</SpecimenCondition>");
			sb.Append("<SpecimenSource>").Append(SerializeStringEscapes.EscapeForXml(labpanel.SpecimenSource)).Append("</SpecimenSource>");
			sb.Append("<ServiceId>").Append(SerializeStringEscapes.EscapeForXml(labpanel.ServiceId)).Append("</ServiceId>");
			sb.Append("<ServiceName>").Append(SerializeStringEscapes.EscapeForXml(labpanel.ServiceName)).Append("</ServiceName>");
			sb.Append("<MedicalOrderNum>").Append(labpanel.MedicalOrderNum).Append("</MedicalOrderNum>");
			sb.Append("</LabPanel>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.LabPanel Deserialize(string xml) {
			OpenDentBusiness.LabPanel labpanel=new OpenDentBusiness.LabPanel();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "LabPanelNum":
							labpanel.LabPanelNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							labpanel.PatNum=reader.ReadContentAsLong();
							break;
						case "RawMessage":
							labpanel.RawMessage=reader.ReadContentAsString();
							break;
						case "LabNameAddress":
							labpanel.LabNameAddress=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							labpanel.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "SpecimenCondition":
							labpanel.SpecimenCondition=reader.ReadContentAsString();
							break;
						case "SpecimenSource":
							labpanel.SpecimenSource=reader.ReadContentAsString();
							break;
						case "ServiceId":
							labpanel.ServiceId=reader.ReadContentAsString();
							break;
						case "ServiceName":
							labpanel.ServiceName=reader.ReadContentAsString();
							break;
						case "MedicalOrderNum":
							labpanel.MedicalOrderNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return labpanel;
		}


	}
}