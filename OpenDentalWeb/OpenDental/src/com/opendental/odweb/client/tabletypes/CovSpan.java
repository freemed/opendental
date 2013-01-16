package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public CovSpan deepCopy() {
			CovSpan covspan=new CovSpan();
			covspan.CovSpanNum=this.CovSpanNum;
			covspan.CovCatNum=this.CovCatNum;
			covspan.FromCode=this.FromCode;
			covspan.ToCode=this.ToCode;
			return covspan;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CovSpan>");
			sb.append("<CovSpanNum>").append(CovSpanNum).append("</CovSpanNum>");
			sb.append("<CovCatNum>").append(CovCatNum).append("</CovCatNum>");
			sb.append("<FromCode>").append(Serializing.escapeForXml(FromCode)).append("</FromCode>");
			sb.append("<ToCode>").append(Serializing.escapeForXml(ToCode)).append("</ToCode>");
			sb.append("</CovSpan>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CovSpanNum")!=null) {
					CovSpanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CovSpanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CovCatNum")!=null) {
					CovCatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CovCatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FromCode")!=null) {
					FromCode=Serializing.getXmlNodeValue(doc,"FromCode");
				}
				if(Serializing.getXmlNodeValue(doc,"ToCode")!=null) {
					ToCode=Serializing.getXmlNodeValue(doc,"ToCode");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
