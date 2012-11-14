package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class PatientNote {
		/** FK to patient.PatNum.  Also the primary key for this table. Always one to one relationship with patient table.  A new patient might not have an entry here until needed. */
		public int PatNum;
		/** Only one note per family stored with guarantor. */
		public String FamFinancial;
		/** No longer used. */
		public String ApptPhone;
		/** Medical Summary */
		public String Medical;
		/** Service notes */
		public String Service;
		/** Complete current Medical History */
		public String MedicalComp;
		/** Shows in the Chart module just below the graphical tooth chart. */
		public String Treatment;

		/** Deep copy of object. */
		public PatientNote Copy() {
			PatientNote patientnote=new PatientNote();
			patientnote.PatNum=this.PatNum;
			patientnote.FamFinancial=this.FamFinancial;
			patientnote.ApptPhone=this.ApptPhone;
			patientnote.Medical=this.Medical;
			patientnote.Service=this.Service;
			patientnote.MedicalComp=this.MedicalComp;
			patientnote.Treatment=this.Treatment;
			return patientnote;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PatientNote>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<FamFinancial>").append(Serializing.EscapeForXml(FamFinancial)).append("</FamFinancial>");
			sb.append("<ApptPhone>").append(Serializing.EscapeForXml(ApptPhone)).append("</ApptPhone>");
			sb.append("<Medical>").append(Serializing.EscapeForXml(Medical)).append("</Medical>");
			sb.append("<Service>").append(Serializing.EscapeForXml(Service)).append("</Service>");
			sb.append("<MedicalComp>").append(Serializing.EscapeForXml(MedicalComp)).append("</MedicalComp>");
			sb.append("<Treatment>").append(Serializing.EscapeForXml(Treatment)).append("</Treatment>");
			sb.append("</PatientNote>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				FamFinancial=doc.getElementsByTagName("FamFinancial").item(0).getFirstChild().getNodeValue();
				ApptPhone=doc.getElementsByTagName("ApptPhone").item(0).getFirstChild().getNodeValue();
				Medical=doc.getElementsByTagName("Medical").item(0).getFirstChild().getNodeValue();
				Service=doc.getElementsByTagName("Service").item(0).getFirstChild().getNodeValue();
				MedicalComp=doc.getElementsByTagName("MedicalComp").item(0).getFirstChild().getNodeValue();
				Treatment=doc.getElementsByTagName("Treatment").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
