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
				if(Serializing.GetXmlNodeValue(doc,"VaccineDefNum")!=null) {
					VaccineDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"VaccineDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CVXCode")!=null) {
					CVXCode=Serializing.GetXmlNodeValue(doc,"CVXCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"VaccineName")!=null) {
					VaccineName=Serializing.GetXmlNodeValue(doc,"VaccineName");
				}
				if(Serializing.GetXmlNodeValue(doc,"DrugManufacturerNum")!=null) {
					DrugManufacturerNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DrugManufacturerNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
