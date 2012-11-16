package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class DrugManufacturer {
		/** Primary key. */
		public int DrugManufacturerNum;
		/** . */
		public String ManufacturerName;
		/** An abbreviation of the manufacturer name. */
		public String ManufacturerCode;

		/** Deep copy of object. */
		public DrugManufacturer Copy() {
			DrugManufacturer drugmanufacturer=new DrugManufacturer();
			drugmanufacturer.DrugManufacturerNum=this.DrugManufacturerNum;
			drugmanufacturer.ManufacturerName=this.ManufacturerName;
			drugmanufacturer.ManufacturerCode=this.ManufacturerCode;
			return drugmanufacturer;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DrugManufacturer>");
			sb.append("<DrugManufacturerNum>").append(DrugManufacturerNum).append("</DrugManufacturerNum>");
			sb.append("<ManufacturerName>").append(Serializing.EscapeForXml(ManufacturerName)).append("</ManufacturerName>");
			sb.append("<ManufacturerCode>").append(Serializing.EscapeForXml(ManufacturerCode)).append("</ManufacturerCode>");
			sb.append("</DrugManufacturer>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"DrugManufacturerNum")!=null) {
					DrugManufacturerNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DrugManufacturerNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ManufacturerName")!=null) {
					ManufacturerName=Serializing.GetXmlNodeValue(doc,"ManufacturerName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ManufacturerCode")!=null) {
					ManufacturerCode=Serializing.GetXmlNodeValue(doc,"ManufacturerCode");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
