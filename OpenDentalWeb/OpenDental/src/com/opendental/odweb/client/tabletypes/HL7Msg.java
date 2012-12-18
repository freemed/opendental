package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class HL7Msg {
		/** Primary key. */
		public int HL7MsgNum;
		/** Enum:HL7MessageStatus Out/In are relative to Open Dental.  This is in contrast to the names of the old ecw folders, which were relative to the other program.  OutPending, OutSent, InReceived, InProcessed. */
		public HL7MessageStatus HL7Status;
		/** The actual HL7 message in its entirity. */
		public String MsgText;
		/** FK to appointment.AptNum.  Many of the messages contain "Visit ID" which is equivalent to our AptNum. */
		public int AptNum;
		/** Used to determine which messages are old so that they can be cleaned up. */
		public Date DateTStamp;
		/** Patient number. */
		public int PatNum;
		/** Maximum size 2000 characters. */
		public String Note;

		/** Deep copy of object. */
		public HL7Msg deepCopy() {
			HL7Msg hl7msg=new HL7Msg();
			hl7msg.HL7MsgNum=this.HL7MsgNum;
			hl7msg.HL7Status=this.HL7Status;
			hl7msg.MsgText=this.MsgText;
			hl7msg.AptNum=this.AptNum;
			hl7msg.DateTStamp=this.DateTStamp;
			hl7msg.PatNum=this.PatNum;
			hl7msg.Note=this.Note;
			return hl7msg;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<HL7Msg>");
			sb.append("<HL7MsgNum>").append(HL7MsgNum).append("</HL7MsgNum>");
			sb.append("<HL7Status>").append(HL7Status.ordinal()).append("</HL7Status>");
			sb.append("<MsgText>").append(Serializing.escapeForXml(MsgText)).append("</MsgText>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("</HL7Msg>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"HL7MsgNum")!=null) {
					HL7MsgNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"HL7MsgNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"HL7Status")!=null) {
					HL7Status=HL7MessageStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"HL7Status"))];
				}
				if(Serializing.getXmlNodeValue(doc,"MsgText")!=null) {
					MsgText=Serializing.getXmlNodeValue(doc,"MsgText");
				}
				if(Serializing.getXmlNodeValue(doc,"AptNum")!=null) {
					AptNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AptNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum HL7MessageStatus {
			/** 0 */
			OutPending,
			/** 1 */
			OutSent,
			/** 2-Tried to send, but there was a problem.  Will keep trying. */
			OutFailed,
			/** 3 */
			InProcessed,
			/** 4 */
			InFailed
		}


}
