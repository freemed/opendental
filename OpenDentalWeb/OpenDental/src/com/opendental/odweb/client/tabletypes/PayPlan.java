package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class PayPlan {
		/** Primary key */
		public int PayPlanNum;
		/** FK to patient.PatNum.  The patient who had the treatment done. */
		public int PatNum;
		/** FK to patient.PatNum.  The person responsible for the payments.  Does not need to be in the same family as the patient.  Will be 0 if PlanNum and InsSubNum have values. */
		public int Guarantor;
		/** Date that the payment plan will display in the account. */
		public Date PayPlanDate;
		/** Annual percentage rate.  eg 18.  This does not take into consideration any late payments, but only the percentage used to calculate the amortization schedule. */
		public double APR;
		/** Generally used to archive the terms when the amortization schedule is created. */
		public String Note;
		/** FK to insplan.PlanNum.  Will be 0 if standard payment plan.  But if this is being used to track expected insurance payments, then this will be the foreign key to insplan.PlanNum, and Guarantor will be 0. */
		public int PlanNum;
		/** The amount of the treatment that has already been completed.  This should match the sum of the principal amounts for most situations.  But if the procedures have not yet been completed, and the payment plan is to make any sense, then this number must be changed. */
		public double CompletedAmt;
		/** FK to inssub.InsSubNum.  Will be 0 if standard payment plan.  But if this is being used to track expected insurance payments, then this will be the foreign key to inssub.InsSubNum, and Guarantor will be 0. */
		public int InsSubNum;

		/** Deep copy of object. */
		public PayPlan deepCopy() {
			PayPlan payplan=new PayPlan();
			payplan.PayPlanNum=this.PayPlanNum;
			payplan.PatNum=this.PatNum;
			payplan.Guarantor=this.Guarantor;
			payplan.PayPlanDate=this.PayPlanDate;
			payplan.APR=this.APR;
			payplan.Note=this.Note;
			payplan.PlanNum=this.PlanNum;
			payplan.CompletedAmt=this.CompletedAmt;
			payplan.InsSubNum=this.InsSubNum;
			return payplan;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PayPlan>");
			sb.append("<PayPlanNum>").append(PayPlanNum).append("</PayPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Guarantor>").append(Guarantor).append("</Guarantor>");
			sb.append("<PayPlanDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(PayPlanDate)).append("</PayPlanDate>");
			sb.append("<APR>").append(APR).append("</APR>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<CompletedAmt>").append(CompletedAmt).append("</CompletedAmt>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("</PayPlan>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PayPlanNum")!=null) {
					PayPlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayPlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Guarantor")!=null) {
					Guarantor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Guarantor"));
				}
				if(Serializing.getXmlNodeValue(doc,"PayPlanDate")!=null) {
					PayPlanDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"PayPlanDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"APR")!=null) {
					APR=Double.valueOf(Serializing.getXmlNodeValue(doc,"APR"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CompletedAmt")!=null) {
					CompletedAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"CompletedAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsSubNum")!=null) {
					InsSubNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsSubNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
