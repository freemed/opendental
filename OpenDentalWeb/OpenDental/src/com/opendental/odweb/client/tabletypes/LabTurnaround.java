package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class LabTurnaround {
		/** Primary key. */
		public int LabTurnaroundNum;
		/** FK to laboratory.LaboratoryNum. The lab that this item is attached to. */
		public int LaboratoryNum;
		/** The description of the service that the lab is performing. */
		public String Description;
		/** The number of days that the lab publishes as the turnaround time for the service. */
		public int DaysPublished;
		/** The actual number of days.  Might be longer than DaysPublished due to travel time.  This is what the actual calculations will be done on. */
		public int DaysActual;

		/** Deep copy of object. */
		public LabTurnaround deepCopy() {
			LabTurnaround labturnaround=new LabTurnaround();
			labturnaround.LabTurnaroundNum=this.LabTurnaroundNum;
			labturnaround.LaboratoryNum=this.LaboratoryNum;
			labturnaround.Description=this.Description;
			labturnaround.DaysPublished=this.DaysPublished;
			labturnaround.DaysActual=this.DaysActual;
			return labturnaround;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LabTurnaround>");
			sb.append("<LabTurnaroundNum>").append(LabTurnaroundNum).append("</LabTurnaroundNum>");
			sb.append("<LaboratoryNum>").append(LaboratoryNum).append("</LaboratoryNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<DaysPublished>").append(DaysPublished).append("</DaysPublished>");
			sb.append("<DaysActual>").append(DaysActual).append("</DaysActual>");
			sb.append("</LabTurnaround>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"LabTurnaroundNum")!=null) {
					LabTurnaroundNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LabTurnaroundNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LaboratoryNum")!=null) {
					LaboratoryNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LaboratoryNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"DaysPublished")!=null) {
					DaysPublished=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DaysPublished"));
				}
				if(Serializing.getXmlNodeValue(doc,"DaysActual")!=null) {
					DaysActual=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DaysActual"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
