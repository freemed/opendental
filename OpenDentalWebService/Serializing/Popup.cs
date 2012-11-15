using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Popup {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Popup popup) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Popup>");
			sb.Append("<PopupNum>").Append(popup.PopupNum).Append("</PopupNum>");
			sb.Append("<PatNum>").Append(popup.PatNum).Append("</PatNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(popup.Description)).Append("</Description>");
			sb.Append("<IsDisabled>").Append((popup.IsDisabled)?1:0).Append("</IsDisabled>");
			sb.Append("<PopupLevel>").Append((int)popup.PopupLevel).Append("</PopupLevel>");
			sb.Append("</Popup>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Popup Deserialize(string xml) {
			OpenDentBusiness.Popup popup=new OpenDentBusiness.Popup();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PopupNum":
							popup.PopupNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							popup.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							popup.Description=reader.ReadContentAsString();
							break;
						case "IsDisabled":
							popup.IsDisabled=reader.ReadContentAsString()!="0";
							break;
						case "PopupLevel":
							popup.PopupLevel=(OpenDentBusiness.EnumPopupLevel)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return popup;
		}


	}
}