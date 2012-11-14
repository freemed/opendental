package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Formulary {
		/** Primary key. */
		public int FormularyNum;
		/** Description. */
		public String Description;

		/** Deep copy of object. */
		public Formulary Copy() {
			Formulary formulary=new Formulary();
			formulary.FormularyNum=this.FormularyNum;
			formulary.Description=this.Description;
			return formulary;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Formulary>");
			sb.append("<FormularyNum>").append(FormularyNum).append("</FormularyNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("</Formulary>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				FormularyNum=Integer.valueOf(doc.getElementsByTagName("FormularyNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
