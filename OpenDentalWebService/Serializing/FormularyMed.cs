using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class FormularyMed {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.FormularyMed formularymed) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<FormularyMed>");
			sb.Append("<FormularyMedNum>").Append(formularymed.FormularyMedNum).Append("</FormularyMedNum>");
			sb.Append("<FormularyNum>").Append(formularymed.FormularyNum).Append("</FormularyNum>");
			sb.Append("<MedicationNum>").Append(formularymed.MedicationNum).Append("</MedicationNum>");
			sb.Append("</FormularyMed>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.FormularyMed Deserialize(string xml) {
			OpenDentBusiness.FormularyMed formularymed=new OpenDentBusiness.FormularyMed();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "FormularyMedNum":
							formularymed.FormularyMedNum=reader.ReadContentAsLong();
							break;
						case "FormularyNum":
							formularymed.FormularyNum=reader.ReadContentAsLong();
							break;
						case "MedicationNum":
							formularymed.MedicationNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return formularymed;
		}


	}
}