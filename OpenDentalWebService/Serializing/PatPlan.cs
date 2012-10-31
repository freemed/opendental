using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PatPlan {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PatPlan patplan) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PatPlan>");
			sb.Append("<PatPlanNum>").Append(patplan.PatPlanNum).Append("</PatPlanNum>");
			sb.Append("<PatNum>").Append(patplan.PatNum).Append("</PatNum>");
			sb.Append("<Ordinal>").Append(patplan.Ordinal).Append("</Ordinal>");
			sb.Append("<IsPending>").Append((patplan.IsPending)?1:0).Append("</IsPending>");
			sb.Append("<Relationship>").Append((int)patplan.Relationship).Append("</Relationship>");
			sb.Append("<PatID>").Append(SerializeStringEscapes.EscapeForXml(patplan.PatID)).Append("</PatID>");
			sb.Append("<InsSubNum>").Append(patplan.InsSubNum).Append("</InsSubNum>");
			sb.Append("</PatPlan>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PatPlan Deserialize(string xml) {
			OpenDentBusiness.PatPlan patplan=new OpenDentBusiness.PatPlan();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PatPlanNum":
							patplan.PatPlanNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							patplan.PatNum=reader.ReadContentAsLong();
							break;
						case "Ordinal":
							patplan.Ordinal=(byte)reader.ReadContentAsInt();
							break;
						case "IsPending":
							patplan.IsPending=reader.ReadContentAsString()!="0";
							break;
						case "Relationship":
							patplan.Relationship=(OpenDentBusiness.Relat)reader.ReadContentAsInt();
							break;
						case "PatID":
							patplan.PatID=reader.ReadContentAsString();
							break;
						case "InsSubNum":
							patplan.InsSubNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return patplan;
		}


	}
}