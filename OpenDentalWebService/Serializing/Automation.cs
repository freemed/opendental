using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Automation {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Automation automation) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Automation>");
			sb.Append("<AutomationNum>").Append(automation.AutomationNum).Append("</AutomationNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(automation.Description)).Append("</Description>");
			sb.Append("<Autotrigger>").Append((int)automation.Autotrigger).Append("</Autotrigger>");
			sb.Append("<ProcCodes>").Append(SerializeStringEscapes.EscapeForXml(automation.ProcCodes)).Append("</ProcCodes>");
			sb.Append("<AutoAction>").Append((int)automation.AutoAction).Append("</AutoAction>");
			sb.Append("<SheetDefNum>").Append(automation.SheetDefNum).Append("</SheetDefNum>");
			sb.Append("<CommType>").Append(automation.CommType).Append("</CommType>");
			sb.Append("<MessageContent>").Append(SerializeStringEscapes.EscapeForXml(automation.MessageContent)).Append("</MessageContent>");
			sb.Append("</Automation>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Automation Deserialize(string xml) {
			OpenDentBusiness.Automation automation=new OpenDentBusiness.Automation();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AutomationNum":
							automation.AutomationNum=reader.ReadContentAsLong();
							break;
						case "Description":
							automation.Description=reader.ReadContentAsString();
							break;
						case "Autotrigger":
							automation.Autotrigger=(OpenDentBusiness.AutomationTrigger)reader.ReadContentAsInt();
							break;
						case "ProcCodes":
							automation.ProcCodes=reader.ReadContentAsString();
							break;
						case "AutoAction":
							automation.AutoAction=(OpenDentBusiness.AutomationAction)reader.ReadContentAsInt();
							break;
						case "SheetDefNum":
							automation.SheetDefNum=reader.ReadContentAsLong();
							break;
						case "CommType":
							automation.CommType=reader.ReadContentAsLong();
							break;
						case "MessageContent":
							automation.MessageContent=reader.ReadContentAsString();
							break;
					}
				}
			}
			return automation;
		}


	}
}