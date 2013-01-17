package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class InstallmentPlan {
		/** Primary key. */
		public int InstallmentPlanNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Date payment plan agreement was made. */
		public Date DateAgreement;
		/** Date of first payment. */
		public Date DateFirstPayment;
		/** Amount of monthly payment. */
		public double MonthlyPayment;
		/** Annual Percentage Rate. e.g. 12. */
		public float APR;
		/** Note */
		public String Note;

		/** Deep copy of object. */
		public InstallmentPlan deepCopy() {
			InstallmentPlan installmentplan=new InstallmentPlan();
			installmentplan.InstallmentPlanNum=this.InstallmentPlanNum;
			installmentplan.PatNum=this.PatNum;
			installmentplan.DateAgreement=this.DateAgreement;
			installmentplan.DateFirstPayment=this.DateFirstPayment;
			installmentplan.MonthlyPayment=this.MonthlyPayment;
			installmentplan.APR=this.APR;
			installmentplan.Note=this.Note;
			return installmentplan;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InstallmentPlan>");
			sb.append("<InstallmentPlanNum>").append(InstallmentPlanNum).append("</InstallmentPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateAgreement>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateAgreement)).append("</DateAgreement>");
			sb.append("<DateFirstPayment>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateFirstPayment)).append("</DateFirstPayment>");
			sb.append("<MonthlyPayment>").append(MonthlyPayment).append("</MonthlyPayment>");
			sb.append("<APR>").append(APR).append("</APR>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("</InstallmentPlan>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"InstallmentPlanNum")!=null) {
					InstallmentPlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InstallmentPlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateAgreement")!=null) {
					DateAgreement=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateAgreement"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateFirstPayment")!=null) {
					DateFirstPayment=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateFirstPayment"));
				}
				if(Serializing.getXmlNodeValue(doc,"MonthlyPayment")!=null) {
					MonthlyPayment=Double.valueOf(Serializing.getXmlNodeValue(doc,"MonthlyPayment"));
				}
				if(Serializing.getXmlNodeValue(doc,"APR")!=null) {
					APR=Float.valueOf(Serializing.getXmlNodeValue(doc,"APR"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing InstallmentPlan: "+e.getMessage());
			}
		}


}
