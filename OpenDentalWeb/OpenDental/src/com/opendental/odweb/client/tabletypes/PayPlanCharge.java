package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class PayPlanCharge {
		/** Primary key. */
		public int PayPlanChargeNum;
		/** FK to payplan.PayPlanNum. */
		public int PayPlanNum;
		/** FK to patient.PatNum.  The guarantor account that each charge will affect. */
		public int Guarantor;
		/** FK to patient.PatNum.  The patient account that the principal gets removed from. */
		public int PatNum;
		/** The date that the charge will show on the patient account.  Any charge with a future date will not show on the account yet and will not affect the balance. */
		public Date ChargeDate;
		/** The principal portion of this payment. */
		public double Principal;
		/** The interest portion of this payment. */
		public double Interest;
		/** Any note about this particular payment plan charge */
		public String Note;
		/** FK to provider.ProvNum.  Since there is no ProvNum field at the payplan level, the provider must be the same for all payplancharges.  It's initially assigned as the patient priProv.  Payments applied should be to this provnum, although the current user interface does not help with this. */
		public int ProvNum;
		/** FK to clinic.ClinicNum.  Since there is no ClincNum field at the payplan level, the clinic must be the same for all payplancharges.  It's initially assigned using the patient clinic.  Payments applied should be to this clinic, although the current user interface does not help with this. */
		public int ClinicNum;

		/** Deep copy of object. */
		public PayPlanCharge Copy() {
			PayPlanCharge payplancharge=new PayPlanCharge();
			payplancharge.PayPlanChargeNum=this.PayPlanChargeNum;
			payplancharge.PayPlanNum=this.PayPlanNum;
			payplancharge.Guarantor=this.Guarantor;
			payplancharge.PatNum=this.PatNum;
			payplancharge.ChargeDate=this.ChargeDate;
			payplancharge.Principal=this.Principal;
			payplancharge.Interest=this.Interest;
			payplancharge.Note=this.Note;
			payplancharge.ProvNum=this.ProvNum;
			payplancharge.ClinicNum=this.ClinicNum;
			return payplancharge;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PayPlanCharge>");
			sb.append("<PayPlanChargeNum>").append(PayPlanChargeNum).append("</PayPlanChargeNum>");
			sb.append("<PayPlanNum>").append(PayPlanNum).append("</PayPlanNum>");
			sb.append("<Guarantor>").append(Guarantor).append("</Guarantor>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ChargeDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</ChargeDate>");
			sb.append("<Principal>").append(Principal).append("</Principal>");
			sb.append("<Interest>").append(Interest).append("</Interest>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("</PayPlanCharge>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PayPlanChargeNum=Integer.valueOf(doc.getElementsByTagName("PayPlanChargeNum").item(0).getFirstChild().getNodeValue());
				PayPlanNum=Integer.valueOf(doc.getElementsByTagName("PayPlanNum").item(0).getFirstChild().getNodeValue());
				Guarantor=Integer.valueOf(doc.getElementsByTagName("Guarantor").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ChargeDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("ChargeDate").item(0).getFirstChild().getNodeValue());
				Principal=Double.valueOf(doc.getElementsByTagName("Principal").item(0).getFirstChild().getNodeValue());
				Interest=Double.valueOf(doc.getElementsByTagName("Interest").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
