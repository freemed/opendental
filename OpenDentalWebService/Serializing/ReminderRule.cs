using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ReminderRule {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ReminderRule reminderrule) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ReminderRule>");
			sb.Append("<ReminderRuleNum>").Append(reminderrule.ReminderRuleNum).Append("</ReminderRuleNum>");
			sb.Append("<ReminderCriterion>").Append((int)reminderrule.ReminderCriterion).Append("</ReminderCriterion>");
			sb.Append("<CriterionFK>").Append(reminderrule.CriterionFK).Append("</CriterionFK>");
			sb.Append("<CriterionValue>").Append(SerializeStringEscapes.EscapeForXml(reminderrule.CriterionValue)).Append("</CriterionValue>");
			sb.Append("<Message>").Append(SerializeStringEscapes.EscapeForXml(reminderrule.Message)).Append("</Message>");
			sb.Append("</ReminderRule>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ReminderRule Deserialize(string xml) {
			OpenDentBusiness.ReminderRule reminderrule=new OpenDentBusiness.ReminderRule();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ReminderRuleNum":
							reminderrule.ReminderRuleNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ReminderCriterion":
							reminderrule.ReminderCriterion=(OpenDentBusiness.EhrCriterion)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CriterionFK":
							reminderrule.CriterionFK=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CriterionValue":
							reminderrule.CriterionValue=reader.ReadContentAsString();
							break;
						case "Message":
							reminderrule.Message=reader.ReadContentAsString();
							break;
					}
				}
			}
			return reminderrule;
		}


	}
}