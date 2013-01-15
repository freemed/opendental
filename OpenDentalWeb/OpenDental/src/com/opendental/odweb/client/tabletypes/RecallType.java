package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class RecallType {
		/** Primary key. */
		public int RecallTypeNum;
		/** . */
		public String Description;
		/** The interval between recalls.  The Interval struct combines years, months, weeks, and days into a single integer value. */
		public int DefaultInterval;
		/** For scheduling the appointment. */
		public String TimePattern;
		/** What procedures to put on the recall appointment.  Comma delimited set of ProcCodes.  (We may change this to CodeNums). */
		public String Procedures;

		/** Deep copy of object. */
		public RecallType deepCopy() {
			RecallType recalltype=new RecallType();
			recalltype.RecallTypeNum=this.RecallTypeNum;
			recalltype.Description=this.Description;
			recalltype.DefaultInterval=this.DefaultInterval;
			recalltype.TimePattern=this.TimePattern;
			recalltype.Procedures=this.Procedures;
			return recalltype;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RecallType>");
			sb.append("<RecallTypeNum>").append(RecallTypeNum).append("</RecallTypeNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<DefaultInterval>").append(DefaultInterval).append("</DefaultInterval>");
			sb.append("<TimePattern>").append(Serializing.escapeForXml(TimePattern)).append("</TimePattern>");
			sb.append("<Procedures>").append(Serializing.escapeForXml(Procedures)).append("</Procedures>");
			sb.append("</RecallType>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"RecallTypeNum")!=null) {
					RecallTypeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RecallTypeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"DefaultInterval")!=null) {
					DefaultInterval=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DefaultInterval"));
				}
				if(Serializing.getXmlNodeValue(doc,"TimePattern")!=null) {
					TimePattern=Serializing.getXmlNodeValue(doc,"TimePattern");
				}
				if(Serializing.getXmlNodeValue(doc,"Procedures")!=null) {
					Procedures=Serializing.getXmlNodeValue(doc,"Procedures");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
