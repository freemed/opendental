package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class MedicationPat {
		/** Primary key. */
		public int MedicationPatNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** FK to medication.MedicationNum. */
		public int MedicationNum;
		/** Medication notes specific to this patient. */
		public String PatNote;
		/** The last date and time this row was altered.  Not user editable.  Will be set to NOW by OD if this patient gets an OnlinePassword assigned. */
		public Date DateTStamp;
		/** Date that the medication was started.  Can be minval if unknown. */
		public Date DateStart;
		/** Date that the medication was stopped.  Can be minval if unknown.  If not minval, then this medication is "discontinued". */
		public Date DateStop;
		/** FK to provider.ProvNum. Can be 0. Gets set to the patient's primary provider when adding a new med.  If adding the med from EHR, gets set to the ProvNum of the logged-in user. */
		public int ProvNum;

		/** Deep copy of object. */
		public MedicationPat Copy() {
			MedicationPat medicationpat=new MedicationPat();
			medicationpat.MedicationPatNum=this.MedicationPatNum;
			medicationpat.PatNum=this.PatNum;
			medicationpat.MedicationNum=this.MedicationNum;
			medicationpat.PatNote=this.PatNote;
			medicationpat.DateTStamp=this.DateTStamp;
			medicationpat.DateStart=this.DateStart;
			medicationpat.DateStop=this.DateStop;
			medicationpat.ProvNum=this.ProvNum;
			return medicationpat;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<MedicationPat>");
			sb.append("<MedicationPatNum>").append(MedicationPatNum).append("</MedicationPatNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("<PatNote>").append(Serializing.EscapeForXml(PatNote)).append("</PatNote>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTStamp>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateStop>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("</MedicationPat>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				MedicationPatNum=Integer.valueOf(doc.getElementsByTagName("MedicationPatNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				MedicationNum=Integer.valueOf(doc.getElementsByTagName("MedicationNum").item(0).getFirstChild().getNodeValue());
				PatNote=doc.getElementsByTagName("PatNote").item(0).getFirstChild().getNodeValue();
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateStart").item(0).getFirstChild().getNodeValue());
				DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateStop").item(0).getFirstChild().getNodeValue());
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
