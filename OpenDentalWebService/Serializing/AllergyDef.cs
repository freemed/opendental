using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AllergyDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AllergyDef allergydef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AllergyDef>");
			sb.Append("<AllergyDefNum>").Append(allergydef.AllergyDefNum).Append("</AllergyDefNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(allergydef.Description)).Append("</Description>");
			sb.Append("<IsHidden>").Append((allergydef.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<DateTStamp>").Append(allergydef.DateTStamp.ToLongDateString()).Append("</DateTStamp>");
			sb.Append("<Snomed>").Append((int)allergydef.Snomed).Append("</Snomed>");
			sb.Append("<MedicationNum>").Append(allergydef.MedicationNum).Append("</MedicationNum>");
			sb.Append("</AllergyDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AllergyDef Deserialize(string xml) {
			OpenDentBusiness.AllergyDef allergydef=new OpenDentBusiness.AllergyDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AllergyDefNum":
							allergydef.AllergyDefNum=reader.ReadContentAsLong();
							break;
						case "Description":
							allergydef.Description=reader.ReadContentAsString();
							break;
						case "IsHidden":
							allergydef.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "DateTStamp":
							allergydef.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "Snomed":
							allergydef.Snomed=(OpenDentBusiness.SnomedAllergy)reader.ReadContentAsInt();
							break;
						case "MedicationNum":
							allergydef.MedicationNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return allergydef;
		}


	}
}