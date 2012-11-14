package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class SupplyNeeded {
		/** Primary key. */
		public int SupplyNeededNum;
		/** . */
		public String Description;
		/** . */
		public String DateAdded;

		/** Deep copy of object. */
		public SupplyNeeded Copy() {
			SupplyNeeded supplyneeded=new SupplyNeeded();
			supplyneeded.SupplyNeededNum=this.SupplyNeededNum;
			supplyneeded.Description=this.Description;
			supplyneeded.DateAdded=this.DateAdded;
			return supplyneeded;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SupplyNeeded>");
			sb.append("<SupplyNeededNum>").append(SupplyNeededNum).append("</SupplyNeededNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<DateAdded>").append(Serializing.EscapeForXml(DateAdded)).append("</DateAdded>");
			sb.append("</SupplyNeeded>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SupplyNeededNum=Integer.valueOf(doc.getElementsByTagName("SupplyNeededNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				DateAdded=doc.getElementsByTagName("DateAdded").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
