package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public CreditCard deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CreditCard>");
			sb.append("<CreditCardNum>").append(CreditCardNum).append("</CreditCardNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Address>").append(Serializing.escapeForXml(Address)).append("</Address>");
			sb.append("<Zip>").append(Serializing.escapeForXml(Zip)).append("</Zip>");
			sb.append("<XChargeToken>").append(Serializing.escapeForXml(XChargeToken)).append("</XChargeToken>");
			sb.append("<CCNumberMasked>").append(Serializing.escapeForXml(CCNumberMasked)).append("</CCNumberMasked>");
			sb.append("<CCExpiration>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CCExpiration)).append("</CCExpiration>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<ChargeAmt>").append(ChargeAmt).append("</ChargeAmt>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStart)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStop)).append("</DateStop>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<PayPlanNum>").append(PayPlanNum).append("</PayPlanNum>");
			sb.append("</CreditCard>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CreditCardNum")!=null) {
					CreditCardNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CreditCardNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.getXmlNodeValue(doc,"Address");
				}
				if(Serializing.getXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.getXmlNodeValue(doc,"Zip");
				}
				if(Serializing.getXmlNodeValue(doc,"XChargeToken")!=null) {
					XChargeToken=Serializing.getXmlNodeValue(doc,"XChargeToken");
				}
				if(Serializing.getXmlNodeValue(doc,"CCNumberMasked")!=null) {
					CCNumberMasked=Serializing.getXmlNodeValue(doc,"CCNumberMasked");
				}
				if(Serializing.getXmlNodeValue(doc,"CCExpiration")!=null) {
					CCExpiration=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"CCExpiration"));
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"ChargeAmt")!=null) {
					ChargeAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"ChargeAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStart")!=null) {
					DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStart"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStop")!=null) {
					DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStop"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"PayPlanNum")!=null) {
					PayPlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayPlanNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
