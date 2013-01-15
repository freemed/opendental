package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public PatientNote deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PatientNote>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<FamFinancial>").append(Serializing.escapeForXml(FamFinancial)).append("</FamFinancial>");
			sb.append("<ApptPhone>").append(Serializing.escapeForXml(ApptPhone)).append("</ApptPhone>");
			sb.append("<Medical>").append(Serializing.escapeForXml(Medical)).append("</Medical>");
			sb.append("<Service>").append(Serializing.escapeForXml(Service)).append("</Service>");
			sb.append("<MedicalComp>").append(Serializing.escapeForXml(MedicalComp)).append("</MedicalComp>");
			sb.append("<Treatment>").append(Serializing.escapeForXml(Treatment)).append("</Treatment>");
			sb.append("</PatientNote>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FamFinancial")!=null) {
					FamFinancial=Serializing.getXmlNodeValue(doc,"FamFinancial");
				}
				if(Serializing.getXmlNodeValue(doc,"ApptPhone")!=null) {
					ApptPhone=Serializing.getXmlNodeValue(doc,"ApptPhone");
				}
				if(Serializing.getXmlNodeValue(doc,"Medical")!=null) {
					Medical=Serializing.getXmlNodeValue(doc,"Medical");
				}
				if(Serializing.getXmlNodeValue(doc,"Service")!=null) {
					Service=Serializing.getXmlNodeValue(doc,"Service");
				}
				if(Serializing.getXmlNodeValue(doc,"MedicalComp")!=null) {
					MedicalComp=Serializing.getXmlNodeValue(doc,"MedicalComp");
				}
				if(Serializing.getXmlNodeValue(doc,"Treatment")!=null) {
					Treatment=Serializing.getXmlNodeValue(doc,"Treatment");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
