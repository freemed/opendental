package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class EmailTemplate {
		/** Primary key. */
		public int EmailTemplateNum;
		/** Default subject line. */
		public String Subject;
		/** Body of the email */
		public String BodyText;

		/** Deep copy of object. */
		public EmailTemplate Copy() {
			EmailTemplate emailtemplate=new EmailTemplate();
			emailtemplate.EmailTemplateNum=this.EmailTemplateNum;
			emailtemplate.Subject=this.Subject;
			emailtemplate.BodyText=this.BodyText;
			return emailtemplate;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EmailTemplate>");
			sb.append("<EmailTemplateNum>").append(EmailTemplateNum).append("</EmailTemplateNum>");
			sb.append("<Subject>").append(Serializing.EscapeForXml(Subject)).append("</Subject>");
			sb.append("<BodyText>").append(Serializing.EscapeForXml(BodyText)).append("</BodyText>");
			sb.append("</EmailTemplate>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"EmailTemplateNum")!=null) {
					EmailTemplateNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmailTemplateNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Subject")!=null) {
					Subject=Serializing.GetXmlNodeValue(doc,"Subject");
				}
				if(Serializing.GetXmlNodeValue(doc,"BodyText")!=null) {
					BodyText=Serializing.GetXmlNodeValue(doc,"BodyText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
