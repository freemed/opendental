using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Medication {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Medication medication) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Medication>");
			sb.Append("<MedicationNum>").Append(medication.MedicationNum).Append("</MedicationNum>");
			sb.Append("<MedName>").Append(SerializeStringEscapes.EscapeForXml(medication.MedName)).Append("</MedName>");
			sb.Append("<GenericNum>").Append(medication.GenericNum).Append("</GenericNum>");
			sb.Append("<Notes>").Append(SerializeStringEscapes.EscapeForXml(medication.Notes)).Append("</Notes>");
			sb.Append("<DateTStamp>").Append(medication.DateTStamp.ToLongDateString()).Append("</DateTStamp>");
			sb.Append("<RxCui>").Append(medication.RxCui).Append("</RxCui>");
			sb.Append("</Medication>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Medication Deserialize(string xml) {
			OpenDentBusiness.Medication medication=new OpenDentBusiness.Medication();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "MedicationNum":
							medication.MedicationNum=reader.ReadContentAsLong();
							break;
						case "MedName":
							medication.MedName=reader.ReadContentAsString();
							break;
						case "GenericNum":
							medication.GenericNum=reader.ReadContentAsLong();
							break;
						case "Notes":
							medication.Notes=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							medication.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "RxCui":
							medication.RxCui=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return medication;
		}


	}
}