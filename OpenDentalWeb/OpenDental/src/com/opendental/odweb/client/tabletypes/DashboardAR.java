package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class DashboardAR {
		/** Primary key. */
		public int DashboardARNum;
		/** This date will always be the last day of a month. */
		public Date DateCalc;
		/** Bal_0_30+Bal_31_60+Bal_61_90+BalOver90 for all patients.  This should also exactly equal BalTotal for all patients with positive amounts.  Negative BalTotals are credits, not A/R. */
		public double BalTotal;
		/** Sum of all InsEst for all patients for the month. */
		public double InsEst;

		/** Deep copy of object. */
		public DashboardAR Copy() {
			DashboardAR dashboardar=new DashboardAR();
			dashboardar.DashboardARNum=this.DashboardARNum;
			dashboardar.DateCalc=this.DateCalc;
			dashboardar.BalTotal=this.BalTotal;
			dashboardar.InsEst=this.InsEst;
			return dashboardar;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DashboardAR>");
			sb.append("<DashboardARNum>").append(DashboardARNum).append("</DashboardARNum>");
			sb.append("<DateCalc>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateCalc)).append("</DateCalc>");
			sb.append("<BalTotal>").append(BalTotal).append("</BalTotal>");
			sb.append("<InsEst>").append(InsEst).append("</InsEst>");
			sb.append("</DashboardAR>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				DashboardARNum=Integer.valueOf(doc.getElementsByTagName("DashboardARNum").item(0).getFirstChild().getNodeValue());
				DateCalc=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateCalc").item(0).getFirstChild().getNodeValue());
				BalTotal=Double.valueOf(doc.getElementsByTagName("BalTotal").item(0).getFirstChild().getNodeValue());
				InsEst=Double.valueOf(doc.getElementsByTagName("InsEst").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
