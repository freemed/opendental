package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class RxAlert {
		/** Primary key. */
		public int RxAlertNum;
		/** FK to rxdef.RxDefNum. */
		public int RxDefNum;
		/** FK to diseasedef.DiseaseDefNum.  Only if DrugProblem interaction.  This is compared against disease.DiseaseDefNum using PatNum. */
		public int DiseaseDefNum;
		/** FK to allergydef.AllergyDefNum.  Only if DrugAllergy interaction.  The allergy and allergydef tables do not yet exist.  Once they are in place in place, this will be compared against allergy.AllergyDefNum using PatNum. */
		public int AllergyDefNum;
		/** FK to medication.MedicationNum.  Only if DrugDrug interaction.  This will be compared against medicationpat.MedicationNum using PatNum. */
		public int MedicationNum;
		/** This is typically blank, so a default message will be displayed by OD.  But if this contains a message, then this message will be used instead. */
		public String NotificationMsg;

		/** Deep copy of object. */
		public RxAlert deepCopy() {
			RxAlert rxalert=new RxAlert();
			rxalert.RxAlertNum=this.RxAlertNum;
			rxalert.RxDefNum=this.RxDefNum;
			rxalert.DiseaseDefNum=this.DiseaseDefNum;
			rxalert.AllergyDefNum=this.AllergyDefNum;
			rxalert.MedicationNum=this.MedicationNum;
			rxalert.NotificationMsg=this.NotificationMsg;
			return rxalert;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RxAlert>");
			sb.append("<RxAlertNum>").append(RxAlertNum).append("</RxAlertNum>");
			sb.append("<RxDefNum>").append(RxDefNum).append("</RxDefNum>");
			sb.append("<DiseaseDefNum>").append(DiseaseDefNum).append("</DiseaseDefNum>");
			sb.append("<AllergyDefNum>").append(AllergyDefNum).append("</AllergyDefNum>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("<NotificationMsg>").append(Serializing.escapeForXml(NotificationMsg)).append("</NotificationMsg>");
			sb.append("</RxAlert>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"RxAlertNum")!=null) {
					RxAlertNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RxAlertNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"RxDefNum")!=null) {
					RxDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RxDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DiseaseDefNum")!=null) {
					DiseaseDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DiseaseDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AllergyDefNum")!=null) {
					AllergyDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AllergyDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MedicationNum")!=null) {
					MedicationNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicationNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"NotificationMsg")!=null) {
					NotificationMsg=Serializing.getXmlNodeValue(doc,"NotificationMsg");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
