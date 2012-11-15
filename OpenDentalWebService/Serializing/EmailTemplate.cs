using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EmailTemplate {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EmailTemplate emailtemplate) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EmailTemplate>");
			sb.Append("<EmailTemplateNum>").Append(emailtemplate.EmailTemplateNum).Append("</EmailTemplateNum>");
			sb.Append("<Subject>").Append(SerializeStringEscapes.EscapeForXml(emailtemplate.Subject)).Append("</Subject>");
			sb.Append("<BodyText>").Append(SerializeStringEscapes.EscapeForXml(emailtemplate.BodyText)).Append("</BodyText>");
			sb.Append("</EmailTemplate>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EmailTemplate Deserialize(string xml) {
			OpenDentBusiness.EmailTemplate emailtemplate=new OpenDentBusiness.EmailTemplate();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EmailTemplateNum":
							emailtemplate.EmailTemplateNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Subject":
							emailtemplate.Subject=reader.ReadContentAsString();
							break;
						case "BodyText":
							emailtemplate.BodyText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return emailtemplate;
		}


	}
}