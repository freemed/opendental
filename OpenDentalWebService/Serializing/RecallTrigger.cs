using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RecallTrigger {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RecallTrigger recalltrigger) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RecallTrigger>");
			sb.Append("<RecallTriggerNum>").Append(recalltrigger.RecallTriggerNum).Append("</RecallTriggerNum>");
			sb.Append("<RecallTypeNum>").Append(recalltrigger.RecallTypeNum).Append("</RecallTypeNum>");
			sb.Append("<CodeNum>").Append(recalltrigger.CodeNum).Append("</CodeNum>");
			sb.Append("</RecallTrigger>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RecallTrigger Deserialize(string xml) {
			OpenDentBusiness.RecallTrigger recalltrigger=new OpenDentBusiness.RecallTrigger();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RecallTriggerNum":
							recalltrigger.RecallTriggerNum=reader.ReadContentAsLong();
							break;
						case "RecallTypeNum":
							recalltrigger.RecallTypeNum=reader.ReadContentAsLong();
							break;
						case "CodeNum":
							recalltrigger.CodeNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return recalltrigger;
		}


	}
}