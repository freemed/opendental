package com.opendental.opendentbusiness.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.opendentbusiness.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public LabPanel deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LabPanel>");
			sb.append("<LabPanelNum>").append(LabPanelNum).append("</LabPanelNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<RawMessage>").append(Serializing.escapeForXml(RawMessage)).append("</RawMessage>");
			sb.append("<LabNameAddress>").append(Serializing.escapeForXml(LabNameAddress)).append("</LabNameAddress>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<SpecimenCondition>").append(Serializing.escapeForXml(SpecimenCondition)).append("</SpecimenCondition>");
			sb.append("<SpecimenSource>").append(Serializing.escapeForXml(SpecimenSource)).append("</SpecimenSource>");
			sb.append("<ServiceId>").append(Serializing.escapeForXml(ServiceId)).append("</ServiceId>");
			sb.append("<ServiceName>").append(Serializing.escapeForXml(ServiceName)).append("</ServiceName>");
			sb.append("<MedicalOrderNum>").append(MedicalOrderNum).append("</MedicalOrderNum>");
			sb.append("</LabPanel>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"LabPanelNum")!=null) {
					LabPanelNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LabPanelNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"RawMessage")!=null) {
					RawMessage=Serializing.getXmlNodeValue(doc,"RawMessage");
				}
				if(Serializing.getXmlNodeValue(doc,"LabNameAddress")!=null) {
					LabNameAddress=Serializing.getXmlNodeValue(doc,"LabNameAddress");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"SpecimenCondition")!=null) {
					SpecimenCondition=Serializing.getXmlNodeValue(doc,"SpecimenCondition");
				}
				if(Serializing.getXmlNodeValue(doc,"SpecimenSource")!=null) {
					SpecimenSource=Serializing.getXmlNodeValue(doc,"SpecimenSource");
				}
				if(Serializing.getXmlNodeValue(doc,"ServiceId")!=null) {
					ServiceId=Serializing.getXmlNodeValue(doc,"ServiceId");
				}
				if(Serializing.getXmlNodeValue(doc,"ServiceName")!=null) {
					ServiceName=Serializing.getXmlNodeValue(doc,"ServiceName");
				}
				if(Serializing.getXmlNodeValue(doc,"MedicalOrderNum")!=null) {
					MedicalOrderNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicalOrderNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing LabPanel: "+e.getMessage());
			}
		}


}
