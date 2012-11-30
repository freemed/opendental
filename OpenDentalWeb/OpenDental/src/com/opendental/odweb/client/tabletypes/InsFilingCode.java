package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

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
		public InsFilingCode Copy() {
			InsFilingCode insfilingcode=new InsFilingCode();
			insfilingcode.InsFilingCodeNum=this.InsFilingCodeNum;
			insfilingcode.Descript=this.Descript;
			insfilingcode.EclaimCode=this.EclaimCode;
			insfilingcode.ItemOrder=this.ItemOrder;
			return insfilingcode;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InsFilingCode>");
			sb.append("<InsFilingCodeNum>").append(InsFilingCodeNum).append("</InsFilingCodeNum>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<EclaimCode>").append(Serializing.EscapeForXml(EclaimCode)).append("</EclaimCode>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("</InsFilingCode>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"InsFilingCodeNum")!=null) {
					InsFilingCodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"InsFilingCodeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.GetXmlNodeValue(doc,"Descript");
				}
				if(Serializing.GetXmlNodeValue(doc,"EclaimCode")!=null) {
					EclaimCode=Serializing.GetXmlNodeValue(doc,"EclaimCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
