package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class PayPeriod {
		/** Primary key. */
		public int PayPeriodNum;
		/** The first day of the payperiod */
		public Date DateStart;
		/** The last day of the payperiod. */
		public Date DateStop;
		/** The date that paychecks will be dated.  A few days after the dateStop.  Optional. */
		public Date DatePaycheck;

		/** Deep copy of object. */
		public PayPeriod deepCopy() {
			PayPeriod payperiod=new PayPeriod();
			payperiod.PayPeriodNum=this.PayPeriodNum;
			payperiod.DateStart=this.DateStart;
			payperiod.DateStop=this.DateStop;
			payperiod.DatePaycheck=this.DatePaycheck;
			return payperiod;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PayPeriod>");
			sb.append("<PayPeriodNum>").append(PayPeriodNum).append("</PayPeriodNum>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStart)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStop)).append("</DateStop>");
			sb.append("<DatePaycheck>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DatePaycheck)).append("</DatePaycheck>");
			sb.append("</PayPeriod>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PayPeriodNum")!=null) {
					PayPeriodNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayPeriodNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStart")!=null) {
					DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStart"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStop")!=null) {
					DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStop"));
				}
				if(Serializing.getXmlNodeValue(doc,"DatePaycheck")!=null) {
					DatePaycheck=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DatePaycheck"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing PayPeriod: "+e.getMessage());
			}
		}


}
