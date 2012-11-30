package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class EmailAttach {
		/** Primary key. */
		public int EmailAttachNum;
		/** FK to emailmessage.EmailMessageNum */
		public int EmailMessageNum;
		/** The name of the file that shows on the email.  For example: tooth2.jpg. */
		public String DisplayedFileName;
		/** The actual file is stored in the A-Z folder in EmailAttachments.  This field stores the name of the file.  The files are named automatically based on Date/time along with a random number.  This ensures that they will be sequential as well as unique. */
		public String ActualFileName;

		/** Deep copy of object. */
		public EmailAttach Copy() {
			EmailAttach emailattach=new EmailAttach();
			emailattach.EmailAttachNum=this.EmailAttachNum;
			emailattach.EmailMessageNum=this.EmailMessageNum;
			emailattach.DisplayedFileName=this.DisplayedFileName;
			emailattach.ActualFileName=this.ActualFileName;
			return emailattach;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EmailAttach>");
			sb.append("<EmailAttachNum>").append(EmailAttachNum).append("</EmailAttachNum>");
			sb.append("<EmailMessageNum>").append(EmailMessageNum).append("</EmailMessageNum>");
			sb.append("<DisplayedFileName>").append(Serializing.EscapeForXml(DisplayedFileName)).append("</DisplayedFileName>");
			sb.append("<ActualFileName>").append(Serializing.EscapeForXml(ActualFileName)).append("</ActualFileName>");
			sb.append("</EmailAttach>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"EmailAttachNum")!=null) {
					EmailAttachNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmailAttachNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EmailMessageNum")!=null) {
					EmailMessageNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmailMessageNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DisplayedFileName")!=null) {
					DisplayedFileName=Serializing.GetXmlNodeValue(doc,"DisplayedFileName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ActualFileName")!=null) {
					ActualFileName=Serializing.GetXmlNodeValue(doc,"ActualFileName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
