using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProcButton {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProcButton procbutton) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProcButton>");
			sb.Append("<ProcButtonNum>").Append(procbutton.ProcButtonNum).Append("</ProcButtonNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(procbutton.Description)).Append("</Description>");
			sb.Append("<ItemOrder>").Append(procbutton.ItemOrder).Append("</ItemOrder>");
			sb.Append("<Category>").Append(procbutton.Category).Append("</Category>");
			sb.Append("<ButtonImage>").Append(SerializeStringEscapes.EscapeForXml(procbutton.ButtonImage)).Append("</ButtonImage>");
			sb.Append("</ProcButton>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProcButton Deserialize(string xml) {
			OpenDentBusiness.ProcButton procbutton=new OpenDentBusiness.ProcButton();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProcButtonNum":
							procbutton.ProcButtonNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							procbutton.Description=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							procbutton.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Category":
							procbutton.Category=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ButtonImage":
							procbutton.ButtonImage=reader.ReadContentAsString();
							break;
					}
				}
			}
			return procbutton;
		}


	}
}