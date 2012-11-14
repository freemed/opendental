package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Signalod {
		/** Primary key. */
		public int SignalNum;
		/** Text version of 'user' this message was sent from, which can actually be any description of a group or individual. */
		public String FromUser;
		/** Enum:InvalidType List of InvalidType long values separated by commas.  Can be empty.  When Date or Tasks are used, they are used all alone with no other flags present. */
		public String ITypes;
		/** If IType=Date, then this is the affected date in the Appointments module. */
		public String DateViewing;
		/** Enum:SignalType  Button, or Invalid. */
		public SignalType SigType;
		/** This is only used if the type is button, and the user types in some text.  This is the typed portion and does not include any of the text that was on the buttons.  These types of signals are displayed in their own separate list in addition to any light and sound that they may cause. */
		public String SigText;
		/** The exact server time when this signal was entered into db.  This does not need to be set by sender since it's handled automatically. */
		public String SigDateTime;
		/** Text version of 'user' this message was sent to, which can actually be any description of a group or individual. */
		public String ToUser;
		/** If this signal has been acknowledged, then this will contain the date and time.  This is how lights get turned off also. */
		public String AckTime;
		/** FK to task.TaskNum.  If IType=Tasks, then this is the taskNum that was added. */
		public int TaskNum;

		/** Deep copy of object. */
		public Signalod Copy() {
			Signalod signalod=new Signalod();
			signalod.SignalNum=this.SignalNum;
			signalod.FromUser=this.FromUser;
			signalod.ITypes=this.ITypes;
			signalod.DateViewing=this.DateViewing;
			signalod.SigType=this.SigType;
			signalod.SigText=this.SigText;
			signalod.SigDateTime=this.SigDateTime;
			signalod.ToUser=this.ToUser;
			signalod.AckTime=this.AckTime;
			signalod.TaskNum=this.TaskNum;
			return signalod;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Signalod>");
			sb.append("<SignalNum>").append(SignalNum).append("</SignalNum>");
			sb.append("<FromUser>").append(Serializing.EscapeForXml(FromUser)).append("</FromUser>");
			sb.append("<ITypes>").append(Serializing.EscapeForXml(ITypes)).append("</ITypes>");
			sb.append("<DateViewing>").append(Serializing.EscapeForXml(DateViewing)).append("</DateViewing>");
			sb.append("<SigType>").append(SigType.ordinal()).append("</SigType>");
			sb.append("<SigText>").append(Serializing.EscapeForXml(SigText)).append("</SigText>");
			sb.append("<SigDateTime>").append(Serializing.EscapeForXml(SigDateTime)).append("</SigDateTime>");
			sb.append("<ToUser>").append(Serializing.EscapeForXml(ToUser)).append("</ToUser>");
			sb.append("<AckTime>").append(Serializing.EscapeForXml(AckTime)).append("</AckTime>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("</Signalod>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SignalNum=Integer.valueOf(doc.getElementsByTagName("SignalNum").item(0).getFirstChild().getNodeValue());
				FromUser=doc.getElementsByTagName("FromUser").item(0).getFirstChild().getNodeValue();
				ITypes=doc.getElementsByTagName("ITypes").item(0).getFirstChild().getNodeValue();
				DateViewing=doc.getElementsByTagName("DateViewing").item(0).getFirstChild().getNodeValue();
				SigType=SignalType.values()[Integer.valueOf(doc.getElementsByTagName("SigType").item(0).getFirstChild().getNodeValue())];
				SigText=doc.getElementsByTagName("SigText").item(0).getFirstChild().getNodeValue();
				SigDateTime=doc.getElementsByTagName("SigDateTime").item(0).getFirstChild().getNodeValue();
				ToUser=doc.getElementsByTagName("ToUser").item(0).getFirstChild().getNodeValue();
				AckTime=doc.getElementsByTagName("AckTime").item(0).getFirstChild().getNodeValue();
				TaskNum=Integer.valueOf(doc.getElementsByTagName("TaskNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** The type of signal being sent. */
		public enum SignalType {
			/** 0- Includes text messages. */
			Button,
			/** 1 */
			Invalid
		}


}
