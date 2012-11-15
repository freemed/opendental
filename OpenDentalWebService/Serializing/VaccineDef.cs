using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class VaccineDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.VaccineDef vaccinedef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<VaccineDef>");
			sb.Append("<VaccineDefNum>").Append(vaccinedef.VaccineDefNum).Append("</VaccineDefNum>");
			sb.Append("<CVXCode>").Append(SerializeStringEscapes.EscapeForXml(vaccinedef.CVXCode)).Append("</CVXCode>");
			sb.Append("<VaccineName>").Append(SerializeStringEscapes.EscapeForXml(vaccinedef.VaccineName)).Append("</VaccineName>");
			sb.Append("<DrugManufacturerNum>").Append(vaccinedef.DrugManufacturerNum).Append("</DrugManufacturerNum>");
			sb.Append("</VaccineDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.VaccineDef Deserialize(string xml) {
			OpenDentBusiness.VaccineDef vaccinedef=new OpenDentBusiness.VaccineDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "VaccineDefNum":
							vaccinedef.VaccineDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CVXCode":
							vaccinedef.CVXCode=reader.ReadContentAsString();
							break;
						case "VaccineName":
							vaccinedef.VaccineName=reader.ReadContentAsString();
							break;
						case "DrugManufacturerNum":
							vaccinedef.DrugManufacturerNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return vaccinedef;
		}


	}
}