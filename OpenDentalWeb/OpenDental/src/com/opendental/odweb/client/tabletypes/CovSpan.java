package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class CovSpan {
		/** Primary key. */
		public int CovSpanNum;
		/** FK to covcat.CovCatNum. */
		public int CovCatNum;
		/** Lower range of the span.  Does not need to be a valid code. */
		public String FromCode;
		/** Upper range of the span.  Does not need to be a valid code. */
		public String ToCode;

		/** Deep copy of object. */
		public CovSpan Copy() {
			CovSpan covspan=new CovSpan();
			covspan.CovSpanNum=this.CovSpanNum;
			covspan.CovCatNum=this.CovCatNum;
			covspan.FromCode=this.FromCode;
			covspan.ToCode=this.ToCode;
			return covspan;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CovSpan>");
			sb.append("<CovSpanNum>").append(CovSpanNum).append("</CovSpanNum>");
			sb.append("<CovCatNum>").append(CovCatNum).append("</CovCatNum>");
			sb.append("<FromCode>").append(Serializing.EscapeForXml(FromCode)).append("</FromCode>");
			sb.append("<ToCode>").append(Serializing.EscapeForXml(ToCode)).append("</ToCode>");
			sb.append("</CovSpan>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				CovSpanNum=Integer.valueOf(doc.getElementsByTagName("CovSpanNum").item(0).getFirstChild().getNodeValue());
				CovCatNum=Integer.valueOf(doc.getElementsByTagName("CovCatNum").item(0).getFirstChild().getNodeValue());
				FromCode=doc.getElementsByTagName("FromCode").item(0).getFirstChild().getNodeValue();
				ToCode=doc.getElementsByTagName("ToCode").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
