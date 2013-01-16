package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Formulary {
		/** Primary key. */
		public int FormularyNum;
		/** Description. */
		public String Description;

		/** Deep copy of object. */
		public Formulary deepCopy() {
			Formulary formulary=new Formulary();
			formulary.FormularyNum=this.FormularyNum;
			formulary.Description=this.Description;
			return formulary;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Formulary>");
			sb.append("<FormularyNum>").append(FormularyNum).append("</FormularyNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("</Formulary>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"FormularyNum")!=null) {
					FormularyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FormularyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
