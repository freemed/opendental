package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Medication {
		/** Primary key. */
		public int MedicationNum;
		/** Name of the medication.  User can change this.  If an RxCui is present, the RxNorm string can be pulled from the in-memory table for UI display in addition to the MedName. */
		public String MedName;
		/** FK to medication.MedicationNum.  If this is a generic drug, then the GenericNum will be the same as the MedicationNum. */
		public int GenericNum;
		/** Notes. */
		public String Notes;
		/** The last date and time this row was altered.  Not user editable. */
		public String DateTStamp;
		/** RxNorm Code identifier. */
		public int RxCui;

		/** Deep copy of object. */
		public Medication Copy() {
			Medication medication=new Medication();
			medication.MedicationNum=this.MedicationNum;
			medication.MedName=this.MedName;
			medication.GenericNum=this.GenericNum;
			medication.Notes=this.Notes;
			medication.DateTStamp=this.DateTStamp;
			medication.RxCui=this.RxCui;
			return medication;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Medication>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("<MedName>").append(Serializing.EscapeForXml(MedName)).append("</MedName>");
			sb.append("<GenericNum>").append(GenericNum).append("</GenericNum>");
			sb.append("<Notes>").append(Serializing.EscapeForXml(Notes)).append("</Notes>");
			sb.append("<DateTStamp>").append(Serializing.EscapeForXml(DateTStamp)).append("</DateTStamp>");
			sb.append("<RxCui>").append(RxCui).append("</RxCui>");
			sb.append("</Medication>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				MedicationNum=Integer.valueOf(doc.getElementsByTagName("MedicationNum").item(0).getFirstChild().getNodeValue());
				MedName=doc.getElementsByTagName("MedName").item(0).getFirstChild().getNodeValue();
				GenericNum=Integer.valueOf(doc.getElementsByTagName("GenericNum").item(0).getFirstChild().getNodeValue());
				Notes=doc.getElementsByTagName("Notes").item(0).getFirstChild().getNodeValue();
				DateTStamp=doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue();
				RxCui=Integer.valueOf(doc.getElementsByTagName("RxCui").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
