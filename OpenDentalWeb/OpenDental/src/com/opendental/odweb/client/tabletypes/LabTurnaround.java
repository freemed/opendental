package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public LabTurnaround Copy() {
			LabTurnaround labturnaround=new LabTurnaround();
			labturnaround.LabTurnaroundNum=this.LabTurnaroundNum;
			labturnaround.LaboratoryNum=this.LaboratoryNum;
			labturnaround.Description=this.Description;
			labturnaround.DaysPublished=this.DaysPublished;
			labturnaround.DaysActual=this.DaysActual;
			return labturnaround;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LabTurnaround>");
			sb.append("<LabTurnaroundNum>").append(LabTurnaroundNum).append("</LabTurnaroundNum>");
			sb.append("<LaboratoryNum>").append(LaboratoryNum).append("</LaboratoryNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<DaysPublished>").append(DaysPublished).append("</DaysPublished>");
			sb.append("<DaysActual>").append(DaysActual).append("</DaysActual>");
			sb.append("</LabTurnaround>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"LabTurnaroundNum")!=null) {
					LabTurnaroundNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LabTurnaroundNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LaboratoryNum")!=null) {
					LaboratoryNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LaboratoryNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"DaysPublished")!=null) {
					DaysPublished=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DaysPublished"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DaysActual")!=null) {
					DaysActual=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DaysActual"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
