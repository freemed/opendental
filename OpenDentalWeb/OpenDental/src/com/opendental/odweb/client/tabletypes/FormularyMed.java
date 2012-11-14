package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class FormularyMed {
		/** Primary key. */
		public int FormularyMedNum;
		/** FK to Formulary. */
		public int FormularyNum;
		/** FK to Medication. */
		public int MedicationNum;

		/** Deep copy of object. */
		public FormularyMed Copy() {
			FormularyMed formularymed=new FormularyMed();
			formularymed.FormularyMedNum=this.FormularyMedNum;
			formularymed.FormularyNum=this.FormularyNum;
			formularymed.MedicationNum=this.MedicationNum;
			return formularymed;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<FormularyMed>");
			sb.append("<FormularyMedNum>").append(FormularyMedNum).append("</FormularyMedNum>");
			sb.append("<FormularyNum>").append(FormularyNum).append("</FormularyNum>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("</FormularyMed>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				FormularyMedNum=Integer.valueOf(doc.getElementsByTagName("FormularyMedNum").item(0).getFirstChild().getNodeValue());
				FormularyNum=Integer.valueOf(doc.getElementsByTagName("FormularyNum").item(0).getFirstChild().getNodeValue());
				MedicationNum=Integer.valueOf(doc.getElementsByTagName("MedicationNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
