using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AutomationCondition {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AutomationCondition automationcondition) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AutomationCondition>");
			sb.Append("<AutomationConditionNum>").Append(automationcondition.AutomationConditionNum).Append("</AutomationConditionNum>");
			sb.Append("<AutomationNum>").Append(automationcondition.AutomationNum).Append("</AutomationNum>");
			sb.Append("<CompareField>").Append((int)automationcondition.CompareField).Append("</CompareField>");
			sb.Append("<Comparison>").Append((int)automationcondition.Comparison).Append("</Comparison>");
			sb.Append("<CompareString>").Append(SerializeStringEscapes.EscapeForXml(automationcondition.CompareString)).Append("</CompareString>");
			sb.Append("</AutomationCondition>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AutomationCondition Deserialize(string xml) {
			OpenDentBusiness.AutomationCondition automationcondition=new OpenDentBusiness.AutomationCondition();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AutomationConditionNum":
							automationcondition.AutomationConditionNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AutomationNum":
							automationcondition.AutomationNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CompareField":
							automationcondition.CompareField=(OpenDentBusiness.AutoCondField)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Comparison":
							automationcondition.Comparison=(OpenDentBusiness.AutoCondComparison)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CompareString":
							automationcondition.CompareString=reader.ReadContentAsString();
							break;
					}
				}
			}
			return automationcondition;
		}


	}
}