package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public InstallmentPlan Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InstallmentPlan>");
			sb.append("<InstallmentPlanNum>").append(InstallmentPlanNum).append("</InstallmentPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateAgreement>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateAgreement)).append("</DateAgreement>");
			sb.append("<DateFirstPayment>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateFirstPayment)).append("</DateFirstPayment>");
			sb.append("<MonthlyPayment>").append(MonthlyPayment).append("</MonthlyPayment>");
			sb.append("<APR>").append(APR).append("</APR>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("</InstallmentPlan>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				InstallmentPlanNum=Integer.valueOf(doc.getElementsByTagName("InstallmentPlanNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DateAgreement=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateAgreement").item(0).getFirstChild().getNodeValue());
				DateFirstPayment=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateFirstPayment").item(0).getFirstChild().getNodeValue());
				MonthlyPayment=Double.valueOf(doc.getElementsByTagName("MonthlyPayment").item(0).getFirstChild().getNodeValue());
				APR=Float.valueOf(doc.getElementsByTagName("APR").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
