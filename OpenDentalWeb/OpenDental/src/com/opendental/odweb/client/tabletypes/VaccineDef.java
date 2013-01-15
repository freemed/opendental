package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public VaccineDef deepCopy() {
			VaccineDef vaccinedef=new VaccineDef();
			vaccinedef.VaccineDefNum=this.VaccineDefNum;
			vaccinedef.CVXCode=this.CVXCode;
			vaccinedef.VaccineName=this.VaccineName;
			vaccinedef.DrugManufacturerNum=this.DrugManufacturerNum;
			return vaccinedef;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<VaccineDef>");
			sb.append("<VaccineDefNum>").append(VaccineDefNum).append("</VaccineDefNum>");
			sb.append("<CVXCode>").append(Serializing.escapeForXml(CVXCode)).append("</CVXCode>");
			sb.append("<VaccineName>").append(Serializing.escapeForXml(VaccineName)).append("</VaccineName>");
			sb.append("<DrugManufacturerNum>").append(DrugManufacturerNum).append("</DrugManufacturerNum>");
			sb.append("</VaccineDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"VaccineDefNum")!=null) {
					VaccineDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"VaccineDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CVXCode")!=null) {
					CVXCode=Serializing.getXmlNodeValue(doc,"CVXCode");
				}
				if(Serializing.getXmlNodeValue(doc,"VaccineName")!=null) {
					VaccineName=Serializing.getXmlNodeValue(doc,"VaccineName");
				}
				if(Serializing.getXmlNodeValue(doc,"DrugManufacturerNum")!=null) {
					DrugManufacturerNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DrugManufacturerNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
