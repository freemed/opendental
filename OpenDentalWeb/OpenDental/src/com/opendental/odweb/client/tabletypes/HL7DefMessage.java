package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class HL7DefMessage {
		/** Primary key. */
		public int HL7DefMessageNum;
		/** FK to HL7Def.HL7DefNum */
		public int HL7DefNum;
		/** Stored in db as string, but used in OD as enum MessageTypeHL7. Example: ADT */
		public MessageTypeHL7 MessageType;
		/** Stored in db as string, but used in OD as enum EventTypeHL7. Example: A04, which is only used iwth ADT/ACK. */
		public EventTypeHL7 EventType;
		/** Enum:InOutHL7 Incoming, Outgoing */
		public InOutHL7 InOrOut;
		/** The only purpose of this column is to let you change the order in the HL7 Def windows.  It's just for convenience. */
		public int ItemOrder;
		/** text */
		public String Note;

		/** Deep copy of object. */
		public HL7DefMessage Copy() {
			HL7DefMessage hl7defmessage=new HL7DefMessage();
			hl7defmessage.HL7DefMessageNum=this.HL7DefMessageNum;
			hl7defmessage.HL7DefNum=this.HL7DefNum;
			hl7defmessage.MessageType=this.MessageType;
			hl7defmessage.EventType=this.EventType;
			hl7defmessage.InOrOut=this.InOrOut;
			hl7defmessage.ItemOrder=this.ItemOrder;
			hl7defmessage.Note=this.Note;
			return hl7defmessage;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<HL7DefMessage>");
			sb.append("<HL7DefMessageNum>").append(HL7DefMessageNum).append("</HL7DefMessageNum>");
			sb.append("<HL7DefNum>").append(HL7DefNum).append("</HL7DefNum>");
			sb.append("<MessageType>").append(MessageType.ordinal()).append("</MessageType>");
			sb.append("<EventType>").append(EventType.ordinal()).append("</EventType>");
			sb.append("<InOrOut>").append(InOrOut.ordinal()).append("</InOrOut>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("</HL7DefMessage>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				HL7DefMessageNum=Integer.valueOf(doc.getElementsByTagName("HL7DefMessageNum").item(0).getFirstChild().getNodeValue());
				HL7DefNum=Integer.valueOf(doc.getElementsByTagName("HL7DefNum").item(0).getFirstChild().getNodeValue());
				MessageType=MessageTypeHL7.values()[Integer.valueOf(doc.getElementsByTagName("MessageType").item(0).getFirstChild().getNodeValue())];
				EventType=EventTypeHL7.values()[Integer.valueOf(doc.getElementsByTagName("EventType").item(0).getFirstChild().getNodeValue())];
				InOrOut=InOutHL7.values()[Integer.valueOf(doc.getElementsByTagName("InOrOut").item(0).getFirstChild().getNodeValue())];
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** The items in this enumeration can be freely rearranged without damaging the database.  But can't change spelling or remove existing item. */
		public enum MessageTypeHL7 {
			/** Demographics - A01,A04,A08,A28,A31 */
			ADT,
			/** Detailed Financial Transaction - P03 */
			DFT,
			/** Unsolicited Observation Message - R01 */
			ORU,
			/** Scheduling - S12,S13,S14,S15,S22 */
			SIU,
			/** Unsolicited Vaccination Record Update - V04 */
			VXU
		}

		/** The items in this enumeration can be freely rearranged without damaging the database.  But can't change spelling or remove existing item. */
		public enum EventTypeHL7 {
			/** Only used with ADT/ACK. */
			A04,
			/** Only used with DFT/Ack. */
			P03,
			/** Only used with SUI/ACK. */
			S12
		}

		/**  */
		public enum InOutHL7 {
			/** 0 */
			Incoming,
			/** 1 */
			Outgoing
		}


}
