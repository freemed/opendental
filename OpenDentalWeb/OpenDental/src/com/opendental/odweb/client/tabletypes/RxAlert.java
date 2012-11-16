package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public RxAlert Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RxAlert>");
			sb.append("<RxAlertNum>").append(RxAlertNum).append("</RxAlertNum>");
			sb.append("<RxDefNum>").append(RxDefNum).append("</RxDefNum>");
			sb.append("<DiseaseDefNum>").append(DiseaseDefNum).append("</DiseaseDefNum>");
			sb.append("<AllergyDefNum>").append(AllergyDefNum).append("</AllergyDefNum>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("<NotificationMsg>").append(Serializing.EscapeForXml(NotificationMsg)).append("</NotificationMsg>");
			sb.append("</RxAlert>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"RxAlertNum")!=null) {
					RxAlertNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RxAlertNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RxDefNum")!=null) {
					RxDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RxDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DiseaseDefNum")!=null) {
					DiseaseDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DiseaseDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AllergyDefNum")!=null) {
					AllergyDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AllergyDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MedicationNum")!=null) {
					MedicationNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MedicationNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"NotificationMsg")!=null) {
					NotificationMsg=Serializing.GetXmlNodeValue(doc,"NotificationMsg");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
