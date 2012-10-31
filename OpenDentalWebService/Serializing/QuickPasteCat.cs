using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class QuickPasteCat {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.QuickPasteCat quickpastecat) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<QuickPasteCat>");
			sb.Append("<QuickPasteCatNum>").Append(quickpastecat.QuickPasteCatNum).Append("</QuickPasteCatNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(quickpastecat.Description)).Append("</Description>");
			sb.Append("<ItemOrder>").Append(quickpastecat.ItemOrder).Append("</ItemOrder>");
			sb.Append("<DefaultForTypes>").Append(SerializeStringEscapes.EscapeForXml(quickpastecat.DefaultForTypes)).Append("</DefaultForTypes>");
			sb.Append("</QuickPasteCat>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.QuickPasteCat Deserialize(string xml) {
			OpenDentBusiness.QuickPasteCat quickpastecat=new OpenDentBusiness.QuickPasteCat();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "QuickPasteCatNum":
							quickpastecat.QuickPasteCatNum=reader.ReadContentAsLong();
							break;
						case "Description":
							quickpastecat.Description=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							quickpastecat.ItemOrder=reader.ReadContentAsInt();
							break;
						case "DefaultForTypes":
							quickpastecat.DefaultForTypes=reader.ReadContentAsString();
							break;
					}
				}
			}
			return quickpastecat;
		}


	}
}