package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class SupplyNeeded {
		/** Primary key. */
		public int SupplyNeededNum;
		/** . */
		public String Description;
		/** . */
		public Date DateAdded;

		/** Deep copy of object. */
		public SupplyNeeded deepCopy() {
			SupplyNeeded supplyneeded=new SupplyNeeded();
			supplyneeded.SupplyNeededNum=this.SupplyNeededNum;
			supplyneeded.Description=this.Description;
			supplyneeded.DateAdded=this.DateAdded;
			return supplyneeded;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SupplyNeeded>");
			sb.append("<SupplyNeededNum>").append(SupplyNeededNum).append("</SupplyNeededNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<DateAdded>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateAdded)).append("</DateAdded>");
			sb.append("</SupplyNeeded>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SupplyNeededNum")!=null) {
					SupplyNeededNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplyNeededNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"DateAdded")!=null) {
					DateAdded=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateAdded"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
