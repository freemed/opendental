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
				RxAlertNum=Integer.valueOf(doc.getElementsByTagName("RxAlertNum").item(0).getFirstChild().getNodeValue());
				RxDefNum=Integer.valueOf(doc.getElementsByTagName("RxDefNum").item(0).getFirstChild().getNodeValue());
				DiseaseDefNum=Integer.valueOf(doc.getElementsByTagName("DiseaseDefNum").item(0).getFirstChild().getNodeValue());
				AllergyDefNum=Integer.valueOf(doc.getElementsByTagName("AllergyDefNum").item(0).getFirstChild().getNodeValue());
				MedicationNum=Integer.valueOf(doc.getElementsByTagName("MedicationNum").item(0).getFirstChild().getNodeValue());
				NotificationMsg=doc.getElementsByTagName("NotificationMsg").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
