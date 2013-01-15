package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class LabCase {
		/** Primary key. */
		public int LabCaseNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** FK to laboratory.LaboratoryNum. The lab that the case gets sent to.  Required. */
		public int LaboratoryNum;
		/** FK to appointment.AptNum.  This is how a lab case is attached to a scheduled appointment. 1:1 relationship for now.  Only one labcase per appointment, and (obviously) only one appointment per labcase.  Labcase can exist without being attached to any appointments at all, making this zero. */
		public int AptNum;
		/** FK to appointment.AptNum.  This is how a lab case is attached to a planned appointment in addition to the scheduled appointment. */
		public int PlannedAptNum;
		/** The due date that is put on the labslip.  NOT when you really need the labcase back, which is usually a day or two later and is the date of the appointment this case is attached to. */
		public Date DateTimeDue;
		/** When this lab case was created. User can edit. */
		public Date DateTimeCreated;
		/** Time that it actually went out to the lab. */
		public Date DateTimeSent;
		/** Date/time received back from the lab.  If this is filled, then the case is considered received. */
		public Date DateTimeRecd;
		/** Date/time that quality was checked.  It is now completely ready for the patient. */
		public Date DateTimeChecked;
		/** FK to provider.ProvNum. */
		public int ProvNum;
		/** The text instructions for this labcase. */
		public String Instructions;
		/** There is no UI built yet for this field.  Plugins might be making use of this field. */
		public double LabFee;

		/** Deep copy of object. */
		public LabCase deepCopy() {
			LabCase labcase=new LabCase();
			labcase.LabCaseNum=this.LabCaseNum;
			labcase.PatNum=this.PatNum;
			labcase.LaboratoryNum=this.LaboratoryNum;
			labcase.AptNum=this.AptNum;
			labcase.PlannedAptNum=this.PlannedAptNum;
			labcase.DateTimeDue=this.DateTimeDue;
			labcase.DateTimeCreated=this.DateTimeCreated;
			labcase.DateTimeSent=this.DateTimeSent;
			labcase.DateTimeRecd=this.DateTimeRecd;
			labcase.DateTimeChecked=this.DateTimeChecked;
			labcase.ProvNum=this.ProvNum;
			labcase.Instructions=this.Instructions;
			labcase.LabFee=this.LabFee;
			return labcase;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LabCase>");
			sb.append("<LabCaseNum>").append(LabCaseNum).append("</LabCaseNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<LaboratoryNum>").append(LaboratoryNum).append("</LaboratoryNum>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<PlannedAptNum>").append(PlannedAptNum).append("</PlannedAptNum>");
			sb.append("<DateTimeDue>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeDue)).append("</DateTimeDue>");
			sb.append("<DateTimeCreated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeCreated)).append("</DateTimeCreated>");
			sb.append("<DateTimeSent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeSent)).append("</DateTimeSent>");
			sb.append("<DateTimeRecd>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeRecd)).append("</DateTimeRecd>");
			sb.append("<DateTimeChecked>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeChecked)).append("</DateTimeChecked>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<Instructions>").append(Serializing.escapeForXml(Instructions)).append("</Instructions>");
			sb.append("<LabFee>").append(LabFee).append("</LabFee>");
			sb.append("</LabCase>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"LabCaseNum")!=null) {
					LabCaseNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LabCaseNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LaboratoryNum")!=null) {
					LaboratoryNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LaboratoryNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AptNum")!=null) {
					AptNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AptNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PlannedAptNum")!=null) {
					PlannedAptNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlannedAptNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeDue")!=null) {
					DateTimeDue=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeDue"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeCreated")!=null) {
					DateTimeCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeCreated"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeSent")!=null) {
					DateTimeSent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeSent"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeRecd")!=null) {
					DateTimeRecd=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeRecd"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeChecked")!=null) {
					DateTimeChecked=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeChecked"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Instructions")!=null) {
					Instructions=Serializing.getXmlNodeValue(doc,"Instructions");
				}
				if(Serializing.getXmlNodeValue(doc,"LabFee")!=null) {
					LabFee=Double.valueOf(Serializing.getXmlNodeValue(doc,"LabFee"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
