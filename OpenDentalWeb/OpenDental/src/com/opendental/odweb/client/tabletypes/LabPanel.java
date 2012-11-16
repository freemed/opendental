package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public Date DateTStamp;
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
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
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
				if(Serializing.GetXmlNodeValue(doc,"LabPanelNum")!=null) {
					LabPanelNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LabPanelNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RawMessage")!=null) {
					RawMessage=Serializing.GetXmlNodeValue(doc,"RawMessage");
				}
				if(Serializing.GetXmlNodeValue(doc,"LabNameAddress")!=null) {
					LabNameAddress=Serializing.GetXmlNodeValue(doc,"LabNameAddress");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SpecimenCondition")!=null) {
					SpecimenCondition=Serializing.GetXmlNodeValue(doc,"SpecimenCondition");
				}
				if(Serializing.GetXmlNodeValue(doc,"SpecimenSource")!=null) {
					SpecimenSource=Serializing.GetXmlNodeValue(doc,"SpecimenSource");
				}
				if(Serializing.GetXmlNodeValue(doc,"ServiceId")!=null) {
					ServiceId=Serializing.GetXmlNodeValue(doc,"ServiceId");
				}
				if(Serializing.GetXmlNodeValue(doc,"ServiceName")!=null) {
					ServiceName=Serializing.GetXmlNodeValue(doc,"ServiceName");
				}
				if(Serializing.GetXmlNodeValue(doc,"MedicalOrderNum")!=null) {
					MedicalOrderNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MedicalOrderNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
