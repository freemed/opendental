package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class SigElement {
		/** Primary key. */
		public int SigElementNum;
		/** FK to sigelementdef.SigElementDefNum */
		public int SigElementDefNum;
		/** FK to signalod.SignalNum. */
		public int SignalNum;

		/** Deep copy of object. */
		public SigElement deepCopy() {
			SigElement sigelement=new SigElement();
			sigelement.SigElementNum=this.SigElementNum;
			sigelement.SigElementDefNum=this.SigElementDefNum;
			sigelement.SignalNum=this.SignalNum;
			return sigelement;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SigElement>");
			sb.append("<SigElementNum>").append(SigElementNum).append("</SigElementNum>");
			sb.append("<SigElementDefNum>").append(SigElementDefNum).append("</SigElementDefNum>");
			sb.append("<SignalNum>").append(SignalNum).append("</SignalNum>");
			sb.append("</SigElement>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SigElementNum")!=null) {
					SigElementNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SigElementNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SigElementDefNum")!=null) {
					SigElementDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SigElementDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SignalNum")!=null) {
					SignalNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SignalNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
