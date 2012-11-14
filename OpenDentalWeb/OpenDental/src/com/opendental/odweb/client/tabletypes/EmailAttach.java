package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EmailAttachNum=Integer.valueOf(doc.getElementsByTagName("EmailAttachNum").item(0).getFirstChild().getNodeValue());
				EmailMessageNum=Integer.valueOf(doc.getElementsByTagName("EmailMessageNum").item(0).getFirstChild().getNodeValue());
				DisplayedFileName=doc.getElementsByTagName("DisplayedFileName").item(0).getFirstChild().getNodeValue();
				ActualFileName=doc.getElementsByTagName("ActualFileName").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
