package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public Date DateTStamp;
		/** RxNorm Code identifier. */
		public int RxCui;

		/** Deep copy of object. */
		public Medication deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Medication>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("<MedName>").append(Serializing.escapeForXml(MedName)).append("</MedName>");
			sb.append("<GenericNum>").append(GenericNum).append("</GenericNum>");
			sb.append("<Notes>").append(Serializing.escapeForXml(Notes)).append("</Notes>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<RxCui>").append(RxCui).append("</RxCui>");
			sb.append("</Medication>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"MedicationNum")!=null) {
					MedicationNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicationNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MedName")!=null) {
					MedName=Serializing.getXmlNodeValue(doc,"MedName");
				}
				if(Serializing.getXmlNodeValue(doc,"GenericNum")!=null) {
					GenericNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"GenericNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.getXmlNodeValue(doc,"Notes");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"RxCui")!=null) {
					RxCui=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RxCui"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
