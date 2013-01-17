package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class FormularyMed {
		/** Primary key. */
		public int FormularyMedNum;
		/** FK to Formulary. */
		public int FormularyNum;
		/** FK to Medication. */
		public int MedicationNum;

		/** Deep copy of object. */
		public FormularyMed deepCopy() {
			FormularyMed formularymed=new FormularyMed();
			formularymed.FormularyMedNum=this.FormularyMedNum;
			formularymed.FormularyNum=this.FormularyNum;
			formularymed.MedicationNum=this.MedicationNum;
			return formularymed;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<FormularyMed>");
			sb.append("<FormularyMedNum>").append(FormularyMedNum).append("</FormularyMedNum>");
			sb.append("<FormularyNum>").append(FormularyNum).append("</FormularyNum>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("</FormularyMed>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"FormularyMedNum")!=null) {
					FormularyMedNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FormularyMedNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FormularyNum")!=null) {
					FormularyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FormularyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MedicationNum")!=null) {
					MedicationNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicationNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing FormularyMed: "+e.getMessage());
			}
		}


}
