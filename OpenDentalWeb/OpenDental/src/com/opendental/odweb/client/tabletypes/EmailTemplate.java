package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class EmailTemplate {
		/** Primary key. */
		public int EmailTemplateNum;
		/** Default subject line. */
		public String Subject;
		/** Body of the email */
		public String BodyText;

		/** Deep copy of object. */
		public EmailTemplate deepCopy() {
			EmailTemplate emailtemplate=new EmailTemplate();
			emailtemplate.EmailTemplateNum=this.EmailTemplateNum;
			emailtemplate.Subject=this.Subject;
			emailtemplate.BodyText=this.BodyText;
			return emailtemplate;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EmailTemplate>");
			sb.append("<EmailTemplateNum>").append(EmailTemplateNum).append("</EmailTemplateNum>");
			sb.append("<Subject>").append(Serializing.escapeForXml(Subject)).append("</Subject>");
			sb.append("<BodyText>").append(Serializing.escapeForXml(BodyText)).append("</BodyText>");
			sb.append("</EmailTemplate>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EmailTemplateNum")!=null) {
					EmailTemplateNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmailTemplateNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Subject")!=null) {
					Subject=Serializing.getXmlNodeValue(doc,"Subject");
				}
				if(Serializing.getXmlNodeValue(doc,"BodyText")!=null) {
					BodyText=Serializing.getXmlNodeValue(doc,"BodyText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
