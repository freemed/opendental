package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"SigElementDefNum")!=null) {
					SigElementDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SigElementDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LightRow")!=null) {
					LightRow=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"LightRow"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LightColor")!=null) {
					LightColor=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LightColor"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SigElementType")!=null) {
					SigElementType=SignalElementType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SigElementType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"SigText")!=null) {
					SigText=Serializing.GetXmlNodeValue(doc,"SigText");
				}
				if(Serializing.GetXmlNodeValue(doc,"Sound")!=null) {
					Sound=Serializing.GetXmlNodeValue(doc,"Sound");
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
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