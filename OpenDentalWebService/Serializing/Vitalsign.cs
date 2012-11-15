using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Vitalsign {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Vitalsign vitalsign) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Vitalsign>");
			sb.Append("<VitalsignNum>").Append(vitalsign.VitalsignNum).Append("</VitalsignNum>");
			sb.Append("<PatNum>").Append(vitalsign.PatNum).Append("</PatNum>");
			sb.Append("<Height>").Append(vitalsign.Height).Append("</Height>");
			sb.Append("<Weight>").Append(vitalsign.Weight).Append("</Weight>");
			sb.Append("<BpSystolic>").Append(vitalsign.BpSystolic).Append("</BpSystolic>");
			sb.Append("<BpDiastolic>").Append(vitalsign.BpDiastolic).Append("</BpDiastolic>");
			sb.Append("<DateTaken>").Append(vitalsign.DateTaken.ToString("yyyyMMddHHmmss")).Append("</DateTaken>");
			sb.Append("<HasFollowupPlan>").Append((vitalsign.HasFollowupPlan)?1:0).Append("</HasFollowupPlan>");
			sb.Append("<IsIneligible>").Append((vitalsign.IsIneligible)?1:0).Append("</IsIneligible>");
			sb.Append("<Documentation>").Append(SerializeStringEscapes.EscapeForXml(vitalsign.Documentation)).Append("</Documentation>");
			sb.Append("<ChildGotNutrition>").Append((vitalsign.ChildGotNutrition)?1:0).Append("</ChildGotNutrition>");
			sb.Append("<ChildGotPhysCouns>").Append((vitalsign.ChildGotPhysCouns)?1:0).Append("</ChildGotPhysCouns>");
			sb.Append("</Vitalsign>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Vitalsign Deserialize(string xml) {
			OpenDentBusiness.Vitalsign vitalsign=new OpenDentBusiness.Vitalsign();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "VitalsignNum":
							vitalsign.VitalsignNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							vitalsign.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Height":
							vitalsign.Height=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "Weight":
							vitalsign.Weight=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "BpSystolic":
							vitalsign.BpSystolic=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "BpDiastolic":
							vitalsign.BpDiastolic=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DateTaken":
							vitalsign.DateTaken=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "HasFollowupPlan":
							vitalsign.HasFollowupPlan=reader.ReadContentAsString()!="0";
							break;
						case "IsIneligible":
							vitalsign.IsIneligible=reader.ReadContentAsString()!="0";
							break;
						case "Documentation":
							vitalsign.Documentation=reader.ReadContentAsString();
							break;
						case "ChildGotNutrition":
							vitalsign.ChildGotNutrition=reader.ReadContentAsString()!="0";
							break;
						case "ChildGotPhysCouns":
							vitalsign.ChildGotPhysCouns=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return vitalsign;
		}


	}
}