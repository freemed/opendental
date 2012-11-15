package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class EmailMessage {
		/** Primary key. */
		public int EmailMessageNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** Single valid email address. Bcc field might be added later, although it won't be very useful.  We will never allow visible cc for privacy reasons. */
		public String ToAddress;
		/** Valid email address. */
		public String FromAddress;
		/** Subject line. */
		public String Subject;
		/** Body of the email */
		public String BodyText;
		/** Date and time the message was sent. Automated at the UI level. */
		public Date MsgDateTime;
		/** 0=neither, 1=sent, 2=received. */
		public CommSentOrReceived SentOrReceived;

		/** Deep copy of object. */
		public EmailMessage Copy() {
			EmailMessage emailmessage=new EmailMessage();
			emailmessage.EmailMessageNum=this.EmailMessageNum;
			emailmessage.PatNum=this.PatNum;
			emailmessage.ToAddress=this.ToAddress;
			emailmessage.FromAddress=this.FromAddress;
			emailmessage.Subject=this.Subject;
			emailmessage.BodyText=this.BodyText;
			emailmessage.MsgDateTime=this.MsgDateTime;
			emailmessage.SentOrReceived=this.SentOrReceived;
			return emailmessage;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EmailMessage>");
			sb.append("<EmailMessageNum>").append(EmailMessageNum).append("</EmailMessageNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ToAddress>").append(Serializing.EscapeForXml(ToAddress)).append("</ToAddress>");
			sb.append("<FromAddress>").append(Serializing.EscapeForXml(FromAddress)).append("</FromAddress>");
			sb.append("<Subject>").append(Serializing.EscapeForXml(Subject)).append("</Subject>");
			sb.append("<BodyText>").append(Serializing.EscapeForXml(BodyText)).append("</BodyText>");
			sb.append("<MsgDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(MsgDateTime)).append("</MsgDateTime>");
			sb.append("<SentOrReceived>").append(SentOrReceived.ordinal()).append("</SentOrReceived>");
			sb.append("</EmailMessage>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EmailMessageNum=Integer.valueOf(doc.getElementsByTagName("EmailMessageNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ToAddress=doc.getElementsByTagName("ToAddress").item(0).getFirstChild().getNodeValue();
				FromAddress=doc.getElementsByTagName("FromAddress").item(0).getFirstChild().getNodeValue();
				Subject=doc.getElementsByTagName("Subject").item(0).getFirstChild().getNodeValue();
				BodyText=doc.getElementsByTagName("BodyText").item(0).getFirstChild().getNodeValue();
				MsgDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("MsgDateTime").item(0).getFirstChild().getNodeValue());
				SentOrReceived=CommSentOrReceived.values()[Integer.valueOf(doc.getElementsByTagName("SentOrReceived").item(0).getFirstChild().getNodeValue())];
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** 0=neither, 1=sent, 2=received. */
		public enum CommSentOrReceived {
			/** 0 */
			Neither,
			/** 1 */
			Sent,
			/** 2 */
			Received
		}


}
