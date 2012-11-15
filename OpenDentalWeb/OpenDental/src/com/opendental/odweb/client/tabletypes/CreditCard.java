package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				CreditCardNum=Integer.valueOf(doc.getElementsByTagName("CreditCardNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				Address=doc.getElementsByTagName("Address").item(0).getFirstChild().getNodeValue();
				Zip=doc.getElementsByTagName("Zip").item(0).getFirstChild().getNodeValue();
				XChargeToken=doc.getElementsByTagName("XChargeToken").item(0).getFirstChild().getNodeValue();
				CCNumberMasked=doc.getElementsByTagName("CCNumberMasked").item(0).getFirstChild().getNodeValue();
				CCExpiration=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("CCExpiration").item(0).getFirstChild().getNodeValue());
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				ChargeAmt=Double.valueOf(doc.getElementsByTagName("ChargeAmt").item(0).getFirstChild().getNodeValue());
				DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateStart").item(0).getFirstChild().getNodeValue());
				DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateStop").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				PayPlanNum=Integer.valueOf(doc.getElementsByTagName("PayPlanNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
