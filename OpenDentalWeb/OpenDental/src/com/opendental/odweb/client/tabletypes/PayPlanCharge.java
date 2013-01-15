package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public PayPlanCharge deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PayPlanCharge>");
			sb.append("<PayPlanChargeNum>").append(PayPlanChargeNum).append("</PayPlanChargeNum>");
			sb.append("<PayPlanNum>").append(PayPlanNum).append("</PayPlanNum>");
			sb.append("<Guarantor>").append(Guarantor).append("</Guarantor>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ChargeDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(ChargeDate)).append("</ChargeDate>");
			sb.append("<Principal>").append(Principal).append("</Principal>");
			sb.append("<Interest>").append(Interest).append("</Interest>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("</PayPlanCharge>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PayPlanChargeNum")!=null) {
					PayPlanChargeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayPlanChargeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PayPlanNum")!=null) {
					PayPlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayPlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Guarantor")!=null) {
					Guarantor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Guarantor"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ChargeDate")!=null) {
					ChargeDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"ChargeDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"Principal")!=null) {
					Principal=Double.valueOf(Serializing.getXmlNodeValue(doc,"Principal"));
				}
				if(Serializing.getXmlNodeValue(doc,"Interest")!=null) {
					Interest=Double.valueOf(Serializing.getXmlNodeValue(doc,"Interest"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
