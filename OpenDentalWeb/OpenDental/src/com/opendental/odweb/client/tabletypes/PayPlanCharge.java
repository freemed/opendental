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
			sb.append("<ChargeDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(ChargeDate)).append("</ChargeDate>");
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
				if(Serializing.GetXmlNodeValue(doc,"PayPlanChargeNum")!=null) {
					PayPlanChargeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PayPlanChargeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PayPlanNum")!=null) {
					PayPlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PayPlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Guarantor")!=null) {
					Guarantor=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Guarantor"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ChargeDate")!=null) {
					ChargeDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"ChargeDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Principal")!=null) {
					Principal=Double.valueOf(Serializing.GetXmlNodeValue(doc,"Principal"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Interest")!=null) {
					Interest=Double.valueOf(Serializing.GetXmlNodeValue(doc,"Interest"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
