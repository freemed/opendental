using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class MedicationPat {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.MedicationPat medicationpat) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<MedicationPat>");
			sb.Append("<MedicationPatNum>").Append(medicationpat.MedicationPatNum).Append("</MedicationPatNum>");
			sb.Append("<PatNum>").Append(medicationpat.PatNum).Append("</PatNum>");
			sb.Append("<MedicationNum>").Append(medicationpat.MedicationNum).Append("</MedicationNum>");
			sb.Append("<PatNote>").Append(SerializeStringEscapes.EscapeForXml(medicationpat.PatNote)).Append("</PatNote>");
			sb.Append("<DateTStamp>").Append(medicationpat.DateTStamp.ToLongDateString()).Append("</DateTStamp>");
			sb.Append("<DateStart>").Append(medicationpat.DateStart.ToLongDateString()).Append("</DateStart>");
			sb.Append("<DateStop>").Append(medicationpat.DateStop.ToLongDateString()).Append("</DateStop>");
			sb.Append("<ProvNum>").Append(medicationpat.ProvNum).Append("</ProvNum>");
			sb.Append("</MedicationPat>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.MedicationPat Deserialize(string xml) {
			OpenDentBusiness.MedicationPat medicationpat=new OpenDentBusiness.MedicationPat();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "MedicationPatNum":
							medicationpat.MedicationPatNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							medicationpat.PatNum=reader.ReadContentAsLong();
							break;
						case "MedicationNum":
							medicationpat.MedicationNum=reader.ReadContentAsLong();
							break;
						case "PatNote":
							medicationpat.PatNote=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							medicationpat.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateStart":
							medicationpat.DateStart=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateStop":
							medicationpat.DateStop=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ProvNum":
							medicationpat.ProvNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return medicationpat;
		}


	}
}