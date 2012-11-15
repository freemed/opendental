using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class VaccinePat {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.VaccinePat vaccinepat) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<VaccinePat>");
			sb.Append("<VaccinePatNum>").Append(vaccinepat.VaccinePatNum).Append("</VaccinePatNum>");
			sb.Append("<VaccineDefNum>").Append(vaccinepat.VaccineDefNum).Append("</VaccineDefNum>");
			sb.Append("<DateTimeStart>").Append(vaccinepat.DateTimeStart.ToString("yyyyMMddHHmmss")).Append("</DateTimeStart>");
			sb.Append("<DateTimeEnd>").Append(vaccinepat.DateTimeEnd.ToString("yyyyMMddHHmmss")).Append("</DateTimeEnd>");
			sb.Append("<AdministeredAmt>").Append(vaccinepat.AdministeredAmt).Append("</AdministeredAmt>");
			sb.Append("<DrugUnitNum>").Append(vaccinepat.DrugUnitNum).Append("</DrugUnitNum>");
			sb.Append("<LotNumber>").Append(SerializeStringEscapes.EscapeForXml(vaccinepat.LotNumber)).Append("</LotNumber>");
			sb.Append("<PatNum>").Append(vaccinepat.PatNum).Append("</PatNum>");
			sb.Append("<NotGiven>").Append((vaccinepat.NotGiven)?1:0).Append("</NotGiven>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(vaccinepat.Note)).Append("</Note>");
			sb.Append("</VaccinePat>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.VaccinePat Deserialize(string xml) {
			OpenDentBusiness.VaccinePat vaccinepat=new OpenDentBusiness.VaccinePat();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "VaccinePatNum":
							vaccinepat.VaccinePatNum=reader.ReadContentAsLong();
							break;
						case "VaccineDefNum":
							vaccinepat.VaccineDefNum=reader.ReadContentAsLong();
							break;
						case "DateTimeStart":
							vaccinepat.DateTimeStart=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateTimeEnd":
							vaccinepat.DateTimeEnd=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "AdministeredAmt":
							vaccinepat.AdministeredAmt=reader.ReadContentAsFloat();
							break;
						case "DrugUnitNum":
							vaccinepat.DrugUnitNum=reader.ReadContentAsLong();
							break;
						case "LotNumber":
							vaccinepat.LotNumber=reader.ReadContentAsString();
							break;
						case "PatNum":
							vaccinepat.PatNum=reader.ReadContentAsLong();
							break;
						case "NotGiven":
							vaccinepat.NotGiven=reader.ReadContentAsString()!="0";
							break;
						case "Note":
							vaccinepat.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return vaccinepat;
		}


	}
}