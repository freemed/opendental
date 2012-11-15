using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ToolButItem {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ToolButItem toolbutitem) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ToolButItem>");
			sb.Append("<ToolButItemNum>").Append(toolbutitem.ToolButItemNum).Append("</ToolButItemNum>");
			sb.Append("<ProgramNum>").Append(toolbutitem.ProgramNum).Append("</ProgramNum>");
			sb.Append("<ToolBar>").Append((int)toolbutitem.ToolBar).Append("</ToolBar>");
			sb.Append("<ButtonText>").Append(SerializeStringEscapes.EscapeForXml(toolbutitem.ButtonText)).Append("</ButtonText>");
			sb.Append("</ToolButItem>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ToolButItem Deserialize(string xml) {
			OpenDentBusiness.ToolButItem toolbutitem=new OpenDentBusiness.ToolButItem();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ToolButItemNum":
							toolbutitem.ToolButItemNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProgramNum":
							toolbutitem.ProgramNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ToolBar":
							toolbutitem.ToolBar=(OpenDentBusiness.ToolBarsAvail)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ButtonText":
							toolbutitem.ButtonText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return toolbutitem;
		}


	}
}