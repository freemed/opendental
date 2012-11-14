package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class SigElement {
		/** Primary key. */
		public int SigElementNum;
		/** FK to sigelementdef.SigElementDefNum */
		public int SigElementDefNum;
		/** FK to signalod.SignalNum. */
		public int SignalNum;

		/** Deep copy of object. */
		public SigElement Copy() {
			SigElement sigelement=new SigElement();
			sigelement.SigElementNum=this.SigElementNum;
			sigelement.SigElementDefNum=this.SigElementDefNum;
			sigelement.SignalNum=this.SignalNum;
			return sigelement;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SigElement>");
			sb.append("<SigElementNum>").append(SigElementNum).append("</SigElementNum>");
			sb.append("<SigElementDefNum>").append(SigElementDefNum).append("</SigElementDefNum>");
			sb.append("<SignalNum>").append(SignalNum).append("</SignalNum>");
			sb.append("</SigElement>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SigElementNum=Integer.valueOf(doc.getElementsByTagName("SigElementNum").item(0).getFirstChild().getNodeValue());
				SigElementDefNum=Integer.valueOf(doc.getElementsByTagName("SigElementDefNum").item(0).getFirstChild().getNodeValue());
				SignalNum=Integer.valueOf(doc.getElementsByTagName("SignalNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
