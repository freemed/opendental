package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class SigButDefElement {
		/** Primary key. */
		public int ElementNum;
		/** FK to sigbutdef.SigButDefNum  A few elements are usually attached to a single button. */
		public int SigButDefNum;
		/** FK to sigelementdef.SigElementDefNum, which contains the actual sound or light. */
		public int SigElementDefNum;

		/** Deep copy of object. */
		public SigButDefElement deepCopy() {
			SigButDefElement sigbutdefelement=new SigButDefElement();
			sigbutdefelement.ElementNum=this.ElementNum;
			sigbutdefelement.SigButDefNum=this.SigButDefNum;
			sigbutdefelement.SigElementDefNum=this.SigElementDefNum;
			return sigbutdefelement;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SigButDefElement>");
			sb.append("<ElementNum>").append(ElementNum).append("</ElementNum>");
			sb.append("<SigButDefNum>").append(SigButDefNum).append("</SigButDefNum>");
			sb.append("<SigElementDefNum>").append(SigElementDefNum).append("</SigElementDefNum>");
			sb.append("</SigButDefElement>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ElementNum")!=null) {
					ElementNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ElementNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SigButDefNum")!=null) {
					SigButDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SigButDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SigElementDefNum")!=null) {
					SigElementDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SigElementDefNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing SigButDefElement: "+e.getMessage());
			}
		}


}
