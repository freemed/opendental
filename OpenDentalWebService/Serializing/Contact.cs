using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Contact {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Contact contact) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Contact>");
			sb.Append("<ContactNum>").Append(contact.ContactNum).Append("</ContactNum>");
			sb.Append("<LName>").Append(SerializeStringEscapes.EscapeForXml(contact.LName)).Append("</LName>");
			sb.Append("<FName>").Append(SerializeStringEscapes.EscapeForXml(contact.FName)).Append("</FName>");
			sb.Append("<WkPhone>").Append(SerializeStringEscapes.EscapeForXml(contact.WkPhone)).Append("</WkPhone>");
			sb.Append("<Fax>").Append(SerializeStringEscapes.EscapeForXml(contact.Fax)).Append("</Fax>");
			sb.Append("<Category>").Append(contact.Category).Append("</Category>");
			sb.Append("<Notes>").Append(SerializeStringEscapes.EscapeForXml(contact.Notes)).Append("</Notes>");
			sb.Append("</Contact>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Contact Deserialize(string xml) {
			OpenDentBusiness.Contact contact=new OpenDentBusiness.Contact();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ContactNum":
							contact.ContactNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "LName":
							contact.LName=reader.ReadContentAsString();
							break;
						case "FName":
							contact.FName=reader.ReadContentAsString();
							break;
						case "WkPhone":
							contact.WkPhone=reader.ReadContentAsString();
							break;
						case "Fax":
							contact.Fax=reader.ReadContentAsString();
							break;
						case "Category":
							contact.Category=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Notes":
							contact.Notes=reader.ReadContentAsString();
							break;
					}
				}
			}
			return contact;
		}


	}
}