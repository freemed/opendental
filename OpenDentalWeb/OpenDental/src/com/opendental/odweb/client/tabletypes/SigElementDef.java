package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class SigElementDef {
		/** Primary key. */
		public int SigElementDefNum;
		/** If this element should cause a button to light up, this would be the row.  0 means none. */
		public byte LightRow;
		/** If a light row is set, this is the color it will turn when triggered.  Ack sets it back to white.  Note that color and row can be in two separate elements of the same signal. */
		public int LightColor;
		/** Enum:SignalElementType  0=User,1=Extra,2=Message. */
		public SignalElementType SigElementType;
		/** The text that shows for the element, like the user name or the two word message.  No long text is stored here. */
		public String SigText;
		/** The sound to play for this element.  Wav file stored in the database in string format until "played".  If empty string, then no sound. */
		public String Sound;
		/** The order of this element within the list of the same type. */
		public int ItemOrder;

		/** Deep copy of object. */
		public SigElementDef Copy() {
			SigElementDef sigelementdef=new SigElementDef();
			sigelementdef.SigElementDefNum=this.SigElementDefNum;
			sigelementdef.LightRow=this.LightRow;
			sigelementdef.LightColor=this.LightColor;
			sigelementdef.SigElementType=this.SigElementType;
			sigelementdef.SigText=this.SigText;
			sigelementdef.Sound=this.Sound;
			sigelementdef.ItemOrder=this.ItemOrder;
			return sigelementdef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SigElementDef>");
			sb.append("<SigElementDefNum>").append(SigElementDefNum).append("</SigElementDefNum>");
			sb.append("<LightRow>").append(LightRow).append("</LightRow>");
			sb.append("<LightColor>").append(LightColor).append("</LightColor>");
			sb.append("<SigElementType>").append(SigElementType.ordinal()).append("</SigElementType>");
			sb.append("<SigText>").append(Serializing.EscapeForXml(SigText)).append("</SigText>");
			sb.append("<Sound>").append(Serializing.EscapeForXml(Sound)).append("</Sound>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("</SigElementDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SigElementDefNum=Integer.valueOf(doc.getElementsByTagName("SigElementDefNum").item(0).getFirstChild().getNodeValue());
				LightRow=Byte.valueOf(doc.getElementsByTagName("LightRow").item(0).getFirstChild().getNodeValue());
				LightColor=Integer.valueOf(doc.getElementsByTagName("LightColor").item(0).getFirstChild().getNodeValue());
				SigElementType=SignalElementType.values()[Integer.valueOf(doc.getElementsByTagName("SigElementType").item(0).getFirstChild().getNodeValue())];
				SigText=doc.getElementsByTagName("SigText").item(0).getFirstChild().getNodeValue();
				Sound=doc.getElementsByTagName("Sound").item(0).getFirstChild().getNodeValue();
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** 0=User,1=Extra,2=Message. */
		public enum SignalElementType {
			/** 0-To and From lists.  Not tied in any way to the users that are part of security. */
			User,
			/** Typically used to insert "family" before "phone" signals. */
			Extra,
			/** Elements of this type show in the last column and trigger the message to be sent. */
			Message
		}


}
