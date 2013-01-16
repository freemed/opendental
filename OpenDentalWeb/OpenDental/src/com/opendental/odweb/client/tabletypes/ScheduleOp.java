package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ScheduleOp {
		/** Primary key. */
		public int ScheduleOpNum;
		/** FK to schedule.ScheduleNum. */
		public int ScheduleNum;
		/** FK to operatory.OperatoryNum. */
		public int OperatoryNum;

		/** Deep copy of object. */
		public ScheduleOp deepCopy() {
			ScheduleOp scheduleop=new ScheduleOp();
			scheduleop.ScheduleOpNum=this.ScheduleOpNum;
			scheduleop.ScheduleNum=this.ScheduleNum;
			scheduleop.OperatoryNum=this.OperatoryNum;
			return scheduleop;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ScheduleOp>");
			sb.append("<ScheduleOpNum>").append(ScheduleOpNum).append("</ScheduleOpNum>");
			sb.append("<ScheduleNum>").append(ScheduleNum).append("</ScheduleNum>");
			sb.append("<OperatoryNum>").append(OperatoryNum).append("</OperatoryNum>");
			sb.append("</ScheduleOp>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ScheduleOpNum")!=null) {
					ScheduleOpNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ScheduleOpNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ScheduleNum")!=null) {
					ScheduleNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ScheduleNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"OperatoryNum")!=null) {
					OperatoryNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OperatoryNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
