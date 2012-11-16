package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class LabCase {
		/** Primary key. */
		public int LabCaseNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** FK to laboratory.LaboratoryNum. The lab that the case gets sent to. */
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
		public LabCase Copy() {
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
		public String SerializeToXml() {
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
			sb.append("<Instructions>").append(Serializing.EscapeForXml(Instructions)).append("</Instructions>");
			sb.append("<LabFee>").append(LabFee).append("</LabFee>");
			sb.append("</LabCase>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"LabCaseNum")!=null) {
					LabCaseNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LabCaseNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LaboratoryNum")!=null) {
					LaboratoryNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LaboratoryNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AptNum")!=null) {
					AptNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AptNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PlannedAptNum")!=null) {
					PlannedAptNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlannedAptNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeDue")!=null) {
					DateTimeDue=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeDue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeCreated")!=null) {
					DateTimeCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeCreated"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeSent")!=null) {
					DateTimeSent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeSent"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeRecd")!=null) {
					DateTimeRecd=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeRecd"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeChecked")!=null) {
					DateTimeChecked=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeChecked"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Instructions")!=null) {
					Instructions=Serializing.GetXmlNodeValue(doc,"Instructions");
				}
				if(Serializing.GetXmlNodeValue(doc,"LabFee")!=null) {
					LabFee=Double.valueOf(Serializing.GetXmlNodeValue(doc,"LabFee"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
