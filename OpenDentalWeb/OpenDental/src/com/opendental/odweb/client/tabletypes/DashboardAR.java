package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public DashboardAR deepCopy() {
			DashboardAR dashboardar=new DashboardAR();
			dashboardar.DashboardARNum=this.DashboardARNum;
			dashboardar.DateCalc=this.DateCalc;
			dashboardar.BalTotal=this.BalTotal;
			dashboardar.InsEst=this.InsEst;
			return dashboardar;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DashboardAR>");
			sb.append("<DashboardARNum>").append(DashboardARNum).append("</DashboardARNum>");
			sb.append("<DateCalc>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateCalc)).append("</DateCalc>");
			sb.append("<BalTotal>").append(BalTotal).append("</BalTotal>");
			sb.append("<InsEst>").append(InsEst).append("</InsEst>");
			sb.append("</DashboardAR>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DashboardARNum")!=null) {
					DashboardARNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DashboardARNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateCalc")!=null) {
					DateCalc=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateCalc"));
				}
				if(Serializing.getXmlNodeValue(doc,"BalTotal")!=null) {
					BalTotal=Double.valueOf(Serializing.getXmlNodeValue(doc,"BalTotal"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsEst")!=null) {
					InsEst=Double.valueOf(Serializing.getXmlNodeValue(doc,"InsEst"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing DashboardAR: "+e.getMessage());
			}
		}


}
