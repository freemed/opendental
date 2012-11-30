package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class CreditCard {
		/** Primary key. */
		public int CreditCardNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** . */
		public String Address;
		/** Postal code. */
		public String Zip;
		/** Token for X-Charge. Alphanumeric, upper and lower case, about 15 char long.  Passed into Xcharge instead of the actual card number. */
		public String XChargeToken;
		/** Credit Card Number.  Will be stored masked: XXXXXXXXXXXX1234. */
		public String CCNumberMasked;
		/** Only month and year are used, the day will usually be 1. */
		public Date CCExpiration;
		/** The order that multiple cards will show.  Zero-based.  First one will be default. */
		public int ItemOrder;
		/** Amount set for recurring charges. */
		public double ChargeAmt;
		/** Start date for recurring charges. */
		public Date DateStart;
		/** Stop date for recurring charges. */
		public Date DateStop;
		/** Any notes about the credit card or account goes here. */
		public String Note;
		/** FK to payplan.PayPlanNum. */
		public int PayPlanNum;

		/** Deep copy of object. */
		public CreditCard Copy() {
			CreditCard creditcard=new CreditCard();
			creditcard.CreditCardNum=this.CreditCardNum;
			creditcard.PatNum=this.PatNum;
			creditcard.Address=this.Address;
			creditcard.Zip=this.Zip;
			creditcard.XChargeToken=this.XChargeToken;
			creditcard.CCNumberMasked=this.CCNumberMasked;
			creditcard.CCExpiration=this.CCExpiration;
			creditcard.ItemOrder=this.ItemOrder;
			creditcard.ChargeAmt=this.ChargeAmt;
			creditcard.DateStart=this.DateStart;
			creditcard.DateStop=this.DateStop;
			creditcard.Note=this.Note;
			creditcard.PayPlanNum=this.PayPlanNum;
			return creditcard;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CreditCard>");
			sb.append("<CreditCardNum>").append(CreditCardNum).append("</CreditCardNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Address>").append(Serializing.EscapeForXml(Address)).append("</Address>");
			sb.append("<Zip>").append(Serializing.EscapeForXml(Zip)).append("</Zip>");
			sb.append("<XChargeToken>").append(Serializing.EscapeForXml(XChargeToken)).append("</XChargeToken>");
			sb.append("<CCNumberMasked>").append(Serializing.EscapeForXml(CCNumberMasked)).append("</CCNumberMasked>");
			sb.append("<CCExpiration>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CCExpiration)).append("</CCExpiration>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<ChargeAmt>").append(ChargeAmt).append("</ChargeAmt>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStart)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStop)).append("</DateStop>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<PayPlanNum>").append(PayPlanNum).append("</PayPlanNum>");
			sb.append("</CreditCard>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"CreditCardNum")!=null) {
					CreditCardNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CreditCardNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.GetXmlNodeValue(doc,"Address");
				}
				if(Serializing.GetXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.GetXmlNodeValue(doc,"Zip");
				}
				if(Serializing.GetXmlNodeValue(doc,"XChargeToken")!=null) {
					XChargeToken=Serializing.GetXmlNodeValue(doc,"XChargeToken");
				}
				if(Serializing.GetXmlNodeValue(doc,"CCNumberMasked")!=null) {
					CCNumberMasked=Serializing.GetXmlNodeValue(doc,"CCNumberMasked");
				}
				if(Serializing.GetXmlNodeValue(doc,"CCExpiration")!=null) {
					CCExpiration=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"CCExpiration"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ChargeAmt")!=null) {
					ChargeAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"ChargeAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateStart")!=null) {
					DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateStart"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateStop")!=null) {
					DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateStop"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"PayPlanNum")!=null) {
					PayPlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PayPlanNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
