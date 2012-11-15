using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Allergy {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Allergy allergy) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Allergy>");
			sb.Append("<AllergyNum>").Append(allergy.AllergyNum).Append("</AllergyNum>");
			sb.Append("<AllergyDefNum>").Append(allergy.AllergyDefNum).Append("</AllergyDefNum>");
			sb.Append("<PatNum>").Append(allergy.PatNum).Append("</PatNum>");
			sb.Append("<Reaction>").Append(SerializeStringEscapes.EscapeForXml(allergy.Reaction)).Append("</Reaction>");
			sb.Append("<StatusIsActive>").Append((allergy.StatusIsActive)?1:0).Append("</StatusIsActive>");
			sb.Append("<DateTStamp>").Append(allergy.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<DateAdverseReaction>").Append(allergy.DateAdverseReaction.ToString("yyyyMMddHHmmss")).Append("</DateAdverseReaction>");
			sb.Append("</Allergy>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Allergy Deserialize(string xml) {
			OpenDentBusiness.Allergy allergy=new OpenDentBusiness.Allergy();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AllergyNum":
							allergy.AllergyNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AllergyDefNum":
							allergy.AllergyDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							allergy.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Reaction":
							allergy.Reaction=reader.ReadContentAsString();
							break;
						case "StatusIsActive":
							allergy.StatusIsActive=reader.ReadContentAsString()!="0";
							break;
						case "DateTStamp":
							allergy.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateAdverseReaction":
							allergy.DateAdverseReaction=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return allergy;
		}


	}
}