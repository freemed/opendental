package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class PayPlan {
		/** Primary key */
		public int PayPlanNum;
		/** FK to patient.PatNum.  The patient who had the treatment done. */
		public int PatNum;
		/** FK to patient.PatNum.  The person responsible for the payments.  Does not need to be in the same family as the patient.  Will be 0 if planNum has a value. */
		public int Guarantor;
		/** Date that the payment plan will display in the account. */
		public Date PayPlanDate;
		/** Annual percentage rate.  eg 18.  This does not take into consideration any late payments, but only the percentage used to calculate the amortization schedule. */
		public double APR;
		/** Generally used to archive the terms when the amortization schedule is created. */
		public String Note;
		/** FK to insplan.PlanNum.  Will be 0 if standard payment plan.  But if this is being used to track expected insurance payments, then this will be the foreign key to insplan.PlanNum and Guarantor will be 0. */
		public int PlanNum;
		/** The amount of the treatment that has already been completed.  This should match the sum of the principal amounts for most situations.  But if the procedures have not yet been completed, and the payment plan is to make any sense, then this number must be changed. */
		public double CompletedAmt;
		/** FK to inssub.InsSubNum. */
		public int InsSubNum;

		/** Deep copy of object. */
		public PayPlan Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PayPlan>");
			sb.append("<PayPlanNum>").append(PayPlanNum).append("</PayPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Guarantor>").append(Guarantor).append("</Guarantor>");
			sb.append("<PayPlanDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</PayPlanDate>");
			sb.append("<APR>").append(APR).append("</APR>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<CompletedAmt>").append(CompletedAmt).append("</CompletedAmt>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("</PayPlan>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PayPlanNum=Integer.valueOf(doc.getElementsByTagName("PayPlanNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				Guarantor=Integer.valueOf(doc.getElementsByTagName("Guarantor").item(0).getFirstChild().getNodeValue());
				PayPlanDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("PayPlanDate").item(0).getFirstChild().getNodeValue());
				APR=Double.valueOf(doc.getElementsByTagName("APR").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				PlanNum=Integer.valueOf(doc.getElementsByTagName("PlanNum").item(0).getFirstChild().getNodeValue());
				CompletedAmt=Double.valueOf(doc.getElementsByTagName("CompletedAmt").item(0).getFirstChild().getNodeValue());
				InsSubNum=Integer.valueOf(doc.getElementsByTagName("InsSubNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
