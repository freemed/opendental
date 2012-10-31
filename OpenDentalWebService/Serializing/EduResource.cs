using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EduResource {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EduResource eduresource) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EduResource>");
			sb.Append("<EduResourceNum>").Append(eduresource.EduResourceNum).Append("</EduResourceNum>");
			sb.Append("<DiseaseDefNum>").Append(eduresource.DiseaseDefNum).Append("</DiseaseDefNum>");
			sb.Append("<MedicationNum>").Append(eduresource.MedicationNum).Append("</MedicationNum>");
			sb.Append("<LabResultID>").Append(SerializeStringEscapes.EscapeForXml(eduresource.LabResultID)).Append("</LabResultID>");
			sb.Append("<LabResultName>").Append(SerializeStringEscapes.EscapeForXml(eduresource.LabResultName)).Append("</LabResultName>");
			sb.Append("<LabResultCompare>").Append(SerializeStringEscapes.EscapeForXml(eduresource.LabResultCompare)).Append("</LabResultCompare>");
			sb.Append("<ResourceUrl>").Append(SerializeStringEscapes.EscapeForXml(eduresource.ResourceUrl)).Append("</ResourceUrl>");
			sb.Append("<Icd9Num>").Append(eduresource.Icd9Num).Append("</Icd9Num>");
			sb.Append("</EduResource>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EduResource Deserialize(string xml) {
			OpenDentBusiness.EduResource eduresource=new OpenDentBusiness.EduResource();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EduResourceNum":
							eduresource.EduResourceNum=reader.ReadContentAsLong();
							break;
						case "DiseaseDefNum":
							eduresource.DiseaseDefNum=reader.ReadContentAsLong();
							break;
						case "MedicationNum":
							eduresource.MedicationNum=reader.ReadContentAsLong();
							break;
						case "LabResultID":
							eduresource.LabResultID=reader.ReadContentAsString();
							break;
						case "LabResultName":
							eduresource.LabResultName=reader.ReadContentAsString();
							break;
						case "LabResultCompare":
							eduresource.LabResultCompare=reader.ReadContentAsString();
							break;
						case "ResourceUrl":
							eduresource.ResourceUrl=reader.ReadContentAsString();
							break;
						case "Icd9Num":
							eduresource.Icd9Num=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return eduresource;
		}


	}
}