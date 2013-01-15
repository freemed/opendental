package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class InsFilingCode {
		/** Primary key. */
		public int InsFilingCodeNum;
		/** Description of the insurance filing code. */
		public String Descript;
		/** Code for electronic claim. */
		public String EclaimCode;
		/** Display order for this filing code within the UI.  0-indexed. */
		public int ItemOrder;

		/** Deep copy of object. */
		public InsFilingCode deepCopy() {
			InsFilingCode insfilingcode=new InsFilingCode();
			insfilingcode.InsFilingCodeNum=this.InsFilingCodeNum;
			insfilingcode.Descript=this.Descript;
			insfilingcode.EclaimCode=this.EclaimCode;
			insfilingcode.ItemOrder=this.ItemOrder;
			return insfilingcode;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InsFilingCode>");
			sb.append("<InsFilingCodeNum>").append(InsFilingCodeNum).append("</InsFilingCodeNum>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("<EclaimCode>").append(Serializing.escapeForXml(EclaimCode)).append("</EclaimCode>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("</InsFilingCode>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"InsFilingCodeNum")!=null) {
					InsFilingCodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsFilingCodeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
				if(Serializing.getXmlNodeValue(doc,"EclaimCode")!=null) {
					EclaimCode=Serializing.getXmlNodeValue(doc,"EclaimCode");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
