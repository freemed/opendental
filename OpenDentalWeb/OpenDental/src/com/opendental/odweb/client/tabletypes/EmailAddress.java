package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class EmailAddress {
		/** Primary key. */
		public int EmailAddressNum;
		/** For example smtp.gmail.com */
		public String SMTPserver;
		/** . */
		public String EmailUsername;
		/** . */
		public String EmailPassword;
		/** Usually 587, sometimes 25 */
		public int ServerPort;
		/** . */
		public boolean UseSSL;
		/** The email address of the sender as it should appear to the recipient. */
		public String SenderAddress;

		/** Deep copy of object. */
		public EmailAddress deepCopy() {
			EmailAddress emailaddress=new EmailAddress();
			emailaddress.EmailAddressNum=this.EmailAddressNum;
			emailaddress.SMTPserver=this.SMTPserver;
			emailaddress.EmailUsername=this.EmailUsername;
			emailaddress.EmailPassword=this.EmailPassword;
			emailaddress.ServerPort=this.ServerPort;
			emailaddress.UseSSL=this.UseSSL;
			emailaddress.SenderAddress=this.SenderAddress;
			return emailaddress;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EmailAddress>");
			sb.append("<EmailAddressNum>").append(EmailAddressNum).append("</EmailAddressNum>");
			sb.append("<SMTPserver>").append(Serializing.escapeForXml(SMTPserver)).append("</SMTPserver>");
			sb.append("<EmailUsername>").append(Serializing.escapeForXml(EmailUsername)).append("</EmailUsername>");
			sb.append("<EmailPassword>").append(Serializing.escapeForXml(EmailPassword)).append("</EmailPassword>");
			sb.append("<ServerPort>").append(ServerPort).append("</ServerPort>");
			sb.append("<UseSSL>").append((UseSSL)?1:0).append("</UseSSL>");
			sb.append("<SenderAddress>").append(Serializing.escapeForXml(SenderAddress)).append("</SenderAddress>");
			sb.append("</EmailAddress>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EmailAddressNum")!=null) {
					EmailAddressNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmailAddressNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SMTPserver")!=null) {
					SMTPserver=Serializing.getXmlNodeValue(doc,"SMTPserver");
				}
				if(Serializing.getXmlNodeValue(doc,"EmailUsername")!=null) {
					EmailUsername=Serializing.getXmlNodeValue(doc,"EmailUsername");
				}
				if(Serializing.getXmlNodeValue(doc,"EmailPassword")!=null) {
					EmailPassword=Serializing.getXmlNodeValue(doc,"EmailPassword");
				}
				if(Serializing.getXmlNodeValue(doc,"ServerPort")!=null) {
					ServerPort=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ServerPort"));
				}
				if(Serializing.getXmlNodeValue(doc,"UseSSL")!=null) {
					UseSSL=(Serializing.getXmlNodeValue(doc,"UseSSL")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"SenderAddress")!=null) {
					SenderAddress=Serializing.getXmlNodeValue(doc,"SenderAddress");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
