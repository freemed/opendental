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
							patplan.PatPlanNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							patplan.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Ordinal":
							patplan.Ordinal=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "IsPending":
							patplan.IsPending=reader.ReadContentAsString()!="0";
							break;
						case "Relationship":
							patplan.Relationship=(OpenDentBusiness.Relat)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "PatID":
							patplan.PatID=reader.ReadContentAsString();
							break;
						case "InsSubNum":
							patplan.InsSubNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return patplan;
		}


	}
}