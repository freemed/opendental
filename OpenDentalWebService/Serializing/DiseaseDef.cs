using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DiseaseDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.DiseaseDef diseasedef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<DiseaseDef>");
			sb.Append("<DiseaseDefNum>").Append(diseasedef.DiseaseDefNum).Append("</DiseaseDefNum>");
			sb.Append("<DiseaseName>").Append(SerializeStringEscapes.EscapeForXml(diseasedef.DiseaseName)).Append("</DiseaseName>");
			sb.Append("<ItemOrder>").Append(diseasedef.ItemOrder).Append("</ItemOrder>");
			sb.Append("<IsHidden>").Append((diseasedef.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<DateTStamp>").Append(diseasedef.DateTStamp.ToLongDateString()).Append("</DateTStamp>");
			sb.Append("</DiseaseDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.DiseaseDef Deserialize(string xml) {
			OpenDentBusiness.DiseaseDef diseasedef=new OpenDentBusiness.DiseaseDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DiseaseDefNum":
							diseasedef.DiseaseDefNum=reader.ReadContentAsLong();
							break;
						case "DiseaseName":
							diseasedef.DiseaseName=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							diseasedef.ItemOrder=reader.ReadContentAsInt();
							break;
						case "IsHidden":
							diseasedef.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "DateTStamp":
							diseasedef.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return diseasedef;
		}


	}
}