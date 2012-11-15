using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PatientNote {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PatientNote patientnote) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PatientNote>");
			sb.Append("<PatNum>").Append(patientnote.PatNum).Append("</PatNum>");
			sb.Append("<FamFinancial>").Append(SerializeStringEscapes.EscapeForXml(patientnote.FamFinancial)).Append("</FamFinancial>");
			sb.Append("<ApptPhone>").Append(SerializeStringEscapes.EscapeForXml(patientnote.ApptPhone)).Append("</ApptPhone>");
			sb.Append("<Medical>").Append(SerializeStringEscapes.EscapeForXml(patientnote.Medical)).Append("</Medical>");
			sb.Append("<Service>").Append(SerializeStringEscapes.EscapeForXml(patientnote.Service)).Append("</Service>");
			sb.Append("<MedicalComp>").Append(SerializeStringEscapes.EscapeForXml(patientnote.MedicalComp)).Append("</MedicalComp>");
			sb.Append("<Treatment>").Append(SerializeStringEscapes.EscapeForXml(patientnote.Treatment)).Append("</Treatment>");
			sb.Append("</PatientNote>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PatientNote Deserialize(string xml) {
			OpenDentBusiness.PatientNote patientnote=new OpenDentBusiness.PatientNote();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PatNum":
							patientnote.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FamFinancial":
							patientnote.FamFinancial=reader.ReadContentAsString();
							break;
						case "ApptPhone":
							patientnote.ApptPhone=reader.ReadContentAsString();
							break;
						case "Medical":
							patientnote.Medical=reader.ReadContentAsString();
							break;
						case "Service":
							patientnote.Service=reader.ReadContentAsString();
							break;
						case "MedicalComp":
							patientnote.MedicalComp=reader.ReadContentAsString();
							break;
						case "Treatment":
							patientnote.Treatment=reader.ReadContentAsString();
							break;
					}
				}
			}
			return patientnote;
		}


	}
}