package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		/** Not a database field.  The sounds and lights attached to the signal. */
		public SigElement[] ElementList;

		/** Deep copy of object. */
		public Signalod deepCopy() {
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
			signalod.ElementList=this.ElementList;
			return signalod;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Signalod>");
			sb.append("<SignalNum>").append(SignalNum).append("</SignalNum>");
			sb.append("<FromUser>").append(Serializing.escapeForXml(FromUser)).append("</FromUser>");
			sb.append("<ITypes>").append(Serializing.escapeForXml(ITypes)).append("</ITypes>");
			sb.append("<DateViewing>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateViewing)).append("</DateViewing>");
			sb.append("<SigType>").append(SigType.ordinal()).append("</SigType>");
			sb.append("<SigText>").append(Serializing.escapeForXml(SigText)).append("</SigText>");
			sb.append("<SigDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(SigDateTime)).append("</SigDateTime>");
			sb.append("<ToUser>").append(Serializing.escapeForXml(ToUser)).append("</ToUser>");
			sb.append("<AckTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AckTime)).append("</AckTime>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("</Signalod>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SignalNum")!=null) {
					SignalNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SignalNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FromUser")!=null) {
					FromUser=Serializing.getXmlNodeValue(doc,"FromUser");
				}
				if(Serializing.getXmlNodeValue(doc,"ITypes")!=null) {
					ITypes=Serializing.getXmlNodeValue(doc,"ITypes");
				}
				if(Serializing.getXmlNodeValue(doc,"DateViewing")!=null) {
					DateViewing=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateViewing"));
				}
				if(Serializing.getXmlNodeValue(doc,"SigType")!=null) {
					SigType=SignalType.valueOf(Serializing.getXmlNodeValue(doc,"SigType"));
				}
				if(Serializing.getXmlNodeValue(doc,"SigText")!=null) {
					SigText=Serializing.getXmlNodeValue(doc,"SigText");
				}
				if(Serializing.getXmlNodeValue(doc,"SigDateTime")!=null) {
					SigDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"SigDateTime"));
				}
				if(Serializing.getXmlNodeValue(doc,"ToUser")!=null) {
					ToUser=Serializing.getXmlNodeValue(doc,"ToUser");
				}
				if(Serializing.getXmlNodeValue(doc,"AckTime")!=null) {
					AckTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"AckTime"));
				}
				if(Serializing.getXmlNodeValue(doc,"TaskNum")!=null) {
					TaskNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Signalod: "+e.getMessage());
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
