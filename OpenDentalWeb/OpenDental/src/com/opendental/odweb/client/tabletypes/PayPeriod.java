package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class PayPeriod {
		/** Primary key. */
		public int PayPeriodNum;
		/** The first day of the payperiod */
		public String DateStart;
		/** The last day of the payperiod. */
		public String DateStop;
		/** The date that paychecks will be dated.  A few days after the dateStop.  Optional. */
		public String DatePaycheck;

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
			sb.append("<DateStart>").append(Serializing.EscapeForXml(DateStart)).append("</DateStart>");
			sb.append("<DateStop>").append(Serializing.EscapeForXml(DateStop)).append("</DateStop>");
			sb.append("<DatePaycheck>").append(Serializing.EscapeForXml(DatePaycheck)).append("</DatePaycheck>");
			sb.append("</PayPeriod>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PayPeriodNum=Integer.valueOf(doc.getElementsByTagName("PayPeriodNum").item(0).getFirstChild().getNodeValue());
				DateStart=doc.getElementsByTagName("DateStart").item(0).getFirstChild().getNodeValue();
				DateStop=doc.getElementsByTagName("DateStop").item(0).getFirstChild().getNodeValue();
				DatePaycheck=doc.getElementsByTagName("DatePaycheck").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
