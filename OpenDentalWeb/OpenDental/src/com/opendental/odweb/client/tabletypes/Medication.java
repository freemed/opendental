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
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<RxCui>").append(RxCui).append("</RxCui>");
			sb.append("</Medication>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"MedicationNum")!=null) {
					MedicationNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MedicationNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MedName")!=null) {
					MedName=Serializing.GetXmlNodeValue(doc,"MedName");
				}
				if(Serializing.GetXmlNodeValue(doc,"GenericNum")!=null) {
					GenericNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"GenericNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.GetXmlNodeValue(doc,"Notes");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RxCui")!=null) {
					RxCui=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RxCui"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
