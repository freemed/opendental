package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Signalod {
		/** Primary key. */
		public int SignalNum;
		/** Text version of 'user' this message was sent from, which can actually be any description of a group or individual. */
		public String FromUser;
		/** Enum:InvalidType List of InvalidType long values separated by commas.  Can be empty.  When Date or Tasks are used, they are used all alone with no other flags present. */
		public String ITypes;
		/** If IType=Date, then this is the affected date in the Appointments module. */
		public Date DateViewing;
		/** Enum:SignalType  Button, or Invalid. */
		public SignalType SigType;
		/** This is only used if the type is button, and the user types in some text.  This is the typed portion and does not include any of the text that was on the buttons.  These types of signals are displayed in their own separate list in addition to any light and sound that they may cause. */
		public String SigText;
		/** The exact server time when this signal was entered into db.  This does not need to be set by sender since it's handled automatically. */
		public Date SigDateTime;
		/** Text version of 'user' this message was sent to, which can actually be any description of a group or individual. */
		public String ToUser;
		/** If this signal has been acknowledged, then this will contain the date and time.  This is how lights get turned off also. */
		public Date AckTime;
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
			sb.append("<DateViewing>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateViewing)).append("</DateViewing>");
			sb.append("<SigType>").append(SigType.ordinal()).append("</SigType>");
			sb.append("<SigText>").append(Serializing.EscapeForXml(SigText)).append("</SigText>");
			sb.append("<SigDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(SigDateTime)).append("</SigDateTime>");
			sb.append("<ToUser>").append(Serializing.EscapeForXml(ToUser)).append("</ToUser>");
			sb.append("<AckTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AckTime)).append("</AckTime>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("</Signalod>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"SignalNum")!=null) {
					SignalNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SignalNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FromUser")!=null) {
					FromUser=Serializing.GetXmlNodeValue(doc,"FromUser");
				}
				if(Serializing.GetXmlNodeValue(doc,"ITypes")!=null) {
					ITypes=Serializing.GetXmlNodeValue(doc,"ITypes");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateViewing")!=null) {
					DateViewing=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateViewing"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SigType")!=null) {
					SigType=SignalType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SigType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"SigText")!=null) {
					SigText=Serializing.GetXmlNodeValue(doc,"SigText");
				}
				if(Serializing.GetXmlNodeValue(doc,"SigDateTime")!=null) {
					SigDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"SigDateTime"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ToUser")!=null) {
					ToUser=Serializing.GetXmlNodeValue(doc,"ToUser");
				}
				if(Serializing.GetXmlNodeValue(doc,"AckTime")!=null) {
					AckTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"AckTime"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskNum")!=null) {
					TaskNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskNum"));
				}
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
