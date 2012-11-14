package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				InsFilingCodeNum=Integer.valueOf(doc.getElementsByTagName("InsFilingCodeNum").item(0).getFirstChild().getNodeValue());
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				EclaimCode=doc.getElementsByTagName("EclaimCode").item(0).getFirstChild().getNodeValue();
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
