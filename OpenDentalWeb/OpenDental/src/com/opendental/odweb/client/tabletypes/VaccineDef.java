package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class VaccineDef {
		/** Primary key. */
		public int VaccineDefNum;
		/** RXA-5-1. */
		public String CVXCode;
		/** Name of vaccine.  RXA-5-2. */
		public String VaccineName;
		/** FK to drugmanufacturer.DrugManufacturerNum. */
		public int DrugManufacturerNum;

		/** Deep copy of object. */
		public VaccineDef Copy() {
			VaccineDef vaccinedef=new VaccineDef();
			vaccinedef.VaccineDefNum=this.VaccineDefNum;
			vaccinedef.CVXCode=this.CVXCode;
			vaccinedef.VaccineName=this.VaccineName;
			vaccinedef.DrugManufacturerNum=this.DrugManufacturerNum;
			return vaccinedef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<VaccineDef>");
			sb.append("<VaccineDefNum>").append(VaccineDefNum).append("</VaccineDefNum>");
			sb.append("<CVXCode>").append(Serializing.EscapeForXml(CVXCode)).append("</CVXCode>");
			sb.append("<VaccineName>").append(Serializing.EscapeForXml(VaccineName)).append("</VaccineName>");
			sb.append("<DrugManufacturerNum>").append(DrugManufacturerNum).append("</DrugManufacturerNum>");
			sb.append("</VaccineDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				VaccineDefNum=Integer.valueOf(doc.getElementsByTagName("VaccineDefNum").item(0).getFirstChild().getNodeValue());
				CVXCode=doc.getElementsByTagName("CVXCode").item(0).getFirstChild().getNodeValue();
				VaccineName=doc.getElementsByTagName("VaccineName").item(0).getFirstChild().getNodeValue();
				DrugManufacturerNum=Integer.valueOf(doc.getElementsByTagName("DrugManufacturerNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
