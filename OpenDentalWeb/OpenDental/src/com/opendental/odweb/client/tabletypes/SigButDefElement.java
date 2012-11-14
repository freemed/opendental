package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class SigButDefElement {
		/** Primary key. */
		public int ElementNum;
		/** FK to sigbutdef.SigButDefNum  A few elements are usually attached to a single button. */
		public int SigButDefNum;
		/** FK to sigelementdef.SigElementDefNum, which contains the actual sound or light. */
		public int SigElementDefNum;

		/** Deep copy of object. */
		public SigButDefElement Copy() {
			SigButDefElement sigbutdefelement=new SigButDefElement();
			sigbutdefelement.ElementNum=this.ElementNum;
			sigbutdefelement.SigButDefNum=this.SigButDefNum;
			sigbutdefelement.SigElementDefNum=this.SigElementDefNum;
			return sigbutdefelement;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SigButDefElement>");
			sb.append("<ElementNum>").append(ElementNum).append("</ElementNum>");
			sb.append("<SigButDefNum>").append(SigButDefNum).append("</SigButDefNum>");
			sb.append("<SigElementDefNum>").append(SigElementDefNum).append("</SigElementDefNum>");
			sb.append("</SigButDefElement>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ElementNum=Integer.valueOf(doc.getElementsByTagName("ElementNum").item(0).getFirstChild().getNodeValue());
				SigButDefNum=Integer.valueOf(doc.getElementsByTagName("SigButDefNum").item(0).getFirstChild().getNodeValue());
				SigElementDefNum=Integer.valueOf(doc.getElementsByTagName("SigElementDefNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
