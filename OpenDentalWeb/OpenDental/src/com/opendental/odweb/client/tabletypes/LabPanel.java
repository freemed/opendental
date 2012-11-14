package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class LabPanel {
		/** Primary key. */
		public int LabPanelNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** The entire raw HL7 message.  Can contain other labpanels in addition to this one. */
		public String RawMessage;
		/** Both name and address in a single field.  OBR-20. */
		public String LabNameAddress;
		/** To be used for synch with web server. */
		public String DateTStamp;
		/** OBR-13.  Usually blank.  Example: hemolyzed. */
		public String SpecimenCondition;
		/** OBR-15.  Usually blank.  Example: LNA&Arterial Catheter&HL70070. */
		public String SpecimenSource;
		/** OBR-4-0, Service performed, id portion, LOINC.  For example, 24331-1. */
		public String ServiceId;
		/** OBR-4-1, Service performed description.  Example, Lipid Panel. */
		public String ServiceName;
		/** FK to medicalorder.MedicalOrderNum.  Used to attach in imported lab panel to a lab order.  Multiple panels may be attached to an order. */
		public int MedicalOrderNum;

		/** Deep copy of object. */
		public LabPanel Copy() {
			LabPanel labpanel=new LabPanel();
			labpanel.LabPanelNum=this.LabPanelNum;
			labpanel.PatNum=this.PatNum;
			labpanel.RawMessage=this.RawMessage;
			labpanel.LabNameAddress=this.LabNameAddress;
			labpanel.DateTStamp=this.DateTStamp;
			labpanel.SpecimenCondition=this.SpecimenCondition;
			labpanel.SpecimenSource=this.SpecimenSource;
			labpanel.ServiceId=this.ServiceId;
			labpanel.ServiceName=this.ServiceName;
			labpanel.MedicalOrderNum=this.MedicalOrderNum;
			return labpanel;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LabPanel>");
			sb.append("<LabPanelNum>").append(LabPanelNum).append("</LabPanelNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<RawMessage>").append(Serializing.EscapeForXml(RawMessage)).append("</RawMessage>");
			sb.append("<LabNameAddress>").append(Serializing.EscapeForXml(LabNameAddress)).append("</LabNameAddress>");
			sb.append("<DateTStamp>").append(Serializing.EscapeForXml(DateTStamp)).append("</DateTStamp>");
			sb.append("<SpecimenCondition>").append(Serializing.EscapeForXml(SpecimenCondition)).append("</SpecimenCondition>");
			sb.append("<SpecimenSource>").append(Serializing.EscapeForXml(SpecimenSource)).append("</SpecimenSource>");
			sb.append("<ServiceId>").append(Serializing.EscapeForXml(ServiceId)).append("</ServiceId>");
			sb.append("<ServiceName>").append(Serializing.EscapeForXml(ServiceName)).append("</ServiceName>");
			sb.append("<MedicalOrderNum>").append(MedicalOrderNum).append("</MedicalOrderNum>");
			sb.append("</LabPanel>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				LabPanelNum=Integer.valueOf(doc.getElementsByTagName("LabPanelNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				RawMessage=doc.getElementsByTagName("RawMessage").item(0).getFirstChild().getNodeValue();
				LabNameAddress=doc.getElementsByTagName("LabNameAddress").item(0).getFirstChild().getNodeValue();
				DateTStamp=doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue();
				SpecimenCondition=doc.getElementsByTagName("SpecimenCondition").item(0).getFirstChild().getNodeValue();
				SpecimenSource=doc.getElementsByTagName("SpecimenSource").item(0).getFirstChild().getNodeValue();
				ServiceId=doc.getElementsByTagName("ServiceId").item(0).getFirstChild().getNodeValue();
				ServiceName=doc.getElementsByTagName("ServiceName").item(0).getFirstChild().getNodeValue();
				MedicalOrderNum=Integer.valueOf(doc.getElementsByTagName("MedicalOrderNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
