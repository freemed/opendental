package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public MedicationPat deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<MedicationPat>");
			sb.append("<MedicationPatNum>").append(MedicationPatNum).append("</MedicationPatNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("<PatNote>").append(Serializing.escapeForXml(PatNote)).append("</PatNote>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStart)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStop)).append("</DateStop>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("</MedicationPat>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"MedicationPatNum")!=null) {
					MedicationPatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicationPatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MedicationNum")!=null) {
					MedicationNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicationNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNote")!=null) {
					PatNote=Serializing.getXmlNodeValue(doc,"PatNote");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStart")!=null) {
					DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStart"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStop")!=null) {
					DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStop"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
