package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FamFinancial")!=null) {
					FamFinancial=Serializing.GetXmlNodeValue(doc,"FamFinancial");
				}
				if(Serializing.GetXmlNodeValue(doc,"ApptPhone")!=null) {
					ApptPhone=Serializing.GetXmlNodeValue(doc,"ApptPhone");
				}
				if(Serializing.GetXmlNodeValue(doc,"Medical")!=null) {
					Medical=Serializing.GetXmlNodeValue(doc,"Medical");
				}
				if(Serializing.GetXmlNodeValue(doc,"Service")!=null) {
					Service=Serializing.GetXmlNodeValue(doc,"Service");
				}
				if(Serializing.GetXmlNodeValue(doc,"MedicalComp")!=null) {
					MedicalComp=Serializing.GetXmlNodeValue(doc,"MedicalComp");
				}
				if(Serializing.GetXmlNodeValue(doc,"Treatment")!=null) {
					Treatment=Serializing.GetXmlNodeValue(doc,"Treatment");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
