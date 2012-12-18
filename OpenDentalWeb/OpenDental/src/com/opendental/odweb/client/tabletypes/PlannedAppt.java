package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class PlannedAppt {
		/** Primary key. */
		public int PlannedApptNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** FK to appointment.AptNum. */
		public int AptNum;
		/** One-indexed order of item in group of planned appts. */
		public int ItemOrder;

		/** Deep copy of object. */
		public PlannedAppt deepCopy() {
			PlannedAppt plannedappt=new PlannedAppt();
			plannedappt.PlannedApptNum=this.PlannedApptNum;
			plannedappt.PatNum=this.PatNum;
			plannedappt.AptNum=this.AptNum;
			plannedappt.ItemOrder=this.ItemOrder;
			return plannedappt;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PlannedAppt>");
			sb.append("<PlannedApptNum>").append(PlannedApptNum).append("</PlannedApptNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("</PlannedAppt>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PlannedApptNum")!=null) {
					PlannedApptNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlannedApptNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AptNum")!=null) {
					AptNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AptNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
