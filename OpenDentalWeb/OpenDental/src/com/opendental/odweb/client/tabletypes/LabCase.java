package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public String DateTimeDue;
		/** When this lab case was created. User can edit. */
		public String DateTimeCreated;
		/** Time that it actually went out to the lab. */
		public String DateTimeSent;
		/** Date/time received back from the lab.  If this is filled, then the case is considered received. */
		public String DateTimeRecd;
		/** Date/time that quality was checked.  It is now completely ready for the patient. */
		public String DateTimeChecked;
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
			sb.append("<DateTimeDue>").append(Serializing.EscapeForXml(DateTimeDue)).append("</DateTimeDue>");
			sb.append("<DateTimeCreated>").append(Serializing.EscapeForXml(DateTimeCreated)).append("</DateTimeCreated>");
			sb.append("<DateTimeSent>").append(Serializing.EscapeForXml(DateTimeSent)).append("</DateTimeSent>");
			sb.append("<DateTimeRecd>").append(Serializing.EscapeForXml(DateTimeRecd)).append("</DateTimeRecd>");
			sb.append("<DateTimeChecked>").append(Serializing.EscapeForXml(DateTimeChecked)).append("</DateTimeChecked>");
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
				LabCaseNum=Integer.valueOf(doc.getElementsByTagName("LabCaseNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				LaboratoryNum=Integer.valueOf(doc.getElementsByTagName("LaboratoryNum").item(0).getFirstChild().getNodeValue());
				AptNum=Integer.valueOf(doc.getElementsByTagName("AptNum").item(0).getFirstChild().getNodeValue());
				PlannedAptNum=Integer.valueOf(doc.getElementsByTagName("PlannedAptNum").item(0).getFirstChild().getNodeValue());
				DateTimeDue=doc.getElementsByTagName("DateTimeDue").item(0).getFirstChild().getNodeValue();
				DateTimeCreated=doc.getElementsByTagName("DateTimeCreated").item(0).getFirstChild().getNodeValue();
				DateTimeSent=doc.getElementsByTagName("DateTimeSent").item(0).getFirstChild().getNodeValue();
				DateTimeRecd=doc.getElementsByTagName("DateTimeRecd").item(0).getFirstChild().getNodeValue();
				DateTimeChecked=doc.getElementsByTagName("DateTimeChecked").item(0).getFirstChild().getNodeValue();
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				Instructions=doc.getElementsByTagName("Instructions").item(0).getFirstChild().getNodeValue();
				LabFee=Double.valueOf(doc.getElementsByTagName("LabFee").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
