package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;
import java.util.ArrayList;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		/** Not a database column. */
		public ArrayList<EmailAttach> Attachments;

		/** Deep copy of object. */
		public EmailMessage deepCopy() {
			EmailMessage emailmessage=new EmailMessage();
			emailmessage.EmailMessageNum=this.EmailMessageNum;
			emailmessage.PatNum=this.PatNum;
			emailmessage.ToAddress=this.ToAddress;
			emailmessage.FromAddress=this.FromAddress;
			emailmessage.Subject=this.Subject;
			emailmessage.BodyText=this.BodyText;
			emailmessage.MsgDateTime=this.MsgDateTime;
			emailmessage.SentOrReceived=this.SentOrReceived;
			emailmessage.Attachments=this.Attachments;
			return emailmessage;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EmailMessage>");
			sb.append("<EmailMessageNum>").append(EmailMessageNum).append("</EmailMessageNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ToAddress>").append(Serializing.escapeForXml(ToAddress)).append("</ToAddress>");
			sb.append("<FromAddress>").append(Serializing.escapeForXml(FromAddress)).append("</FromAddress>");
			sb.append("<Subject>").append(Serializing.escapeForXml(Subject)).append("</Subject>");
			sb.append("<BodyText>").append(Serializing.escapeForXml(BodyText)).append("</BodyText>");
			sb.append("<MsgDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(MsgDateTime)).append("</MsgDateTime>");
			sb.append("<SentOrReceived>").append(SentOrReceived.ordinal()).append("</SentOrReceived>");
			sb.append("</EmailMessage>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EmailMessageNum")!=null) {
					EmailMessageNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmailMessageNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ToAddress")!=null) {
					ToAddress=Serializing.getXmlNodeValue(doc,"ToAddress");
				}
				if(Serializing.getXmlNodeValue(doc,"FromAddress")!=null) {
					FromAddress=Serializing.getXmlNodeValue(doc,"FromAddress");
				}
				if(Serializing.getXmlNodeValue(doc,"Subject")!=null) {
					Subject=Serializing.getXmlNodeValue(doc,"Subject");
				}
				if(Serializing.getXmlNodeValue(doc,"BodyText")!=null) {
					BodyText=Serializing.getXmlNodeValue(doc,"BodyText");
				}
				if(Serializing.getXmlNodeValue(doc,"MsgDateTime")!=null) {
					MsgDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"MsgDateTime"));
				}
				if(Serializing.getXmlNodeValue(doc,"SentOrReceived")!=null) {
					SentOrReceived=CommSentOrReceived.valueOf(Serializing.getXmlNodeValue(doc,"SentOrReceived"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing EmailMessage: "+e.getMessage());
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
