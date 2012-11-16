package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public PayPeriod Copy() {
			PayPeriod payperiod=new PayPeriod();
			payperiod.PayPeriodNum=this.PayPeriodNum;
			payperiod.DateStart=this.DateStart;
			payperiod.DateStop=this.DateStop;
			payperiod.DatePaycheck=this.DatePaycheck;
			return payperiod;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PayPeriod>");
			sb.append("<PayPeriodNum>").append(PayPeriodNum).append("</PayPeriodNum>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStart)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStop)).append("</DateStop>");
			sb.append("<DatePaycheck>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DatePaycheck)).append("</DatePaycheck>");
			sb.append("</PayPeriod>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"PayPeriodNum")!=null) {
					PayPeriodNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PayPeriodNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateStart")!=null) {
					DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateStart"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateStop")!=null) {
					DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateStop"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DatePaycheck")!=null) {
					DatePaycheck=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DatePaycheck"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
