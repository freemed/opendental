package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class Adjustment {
		/** Primary key. */
		public int AdjNum;
		/** The date that the adjustment shows in the patient account. */
		public Date AdjDate;
		/** Amount of adjustment.  Can be pos or neg. */
		public double AdjAmt;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** FK to definition.DefNum. */
		public int AdjType;
		/** FK to provider.ProvNum. */
		public int ProvNum;
		/** Note for this adjustment. */
		public String AdjNote;
		/** Procedure date.  Not when the adjustment was entered.  This is what the aging will be based on in a future version. */
		public Date ProcDate;
		/** FK to procedurelog.ProcNum.  Only used if attached to a procedure.  Otherwise, 0. */
		public int ProcNum;
		/** Timestamp automatically generated and user not allowed to change.  The actual date of entry. */
		public Date DateEntry;
		/** FK to clinic.ClinicNum. */
		public int ClinicNum;
		/** FK to statement.StatementNum.  Only used when the statement in an invoice. */
		public int StatementNum;

		/** Deep copy of object. */
		public Adjustment deepCopy() {
			Adjustment adjustment=new Adjustment();
			adjustment.AdjNum=this.AdjNum;
			adjustment.AdjDate=this.AdjDate;
			adjustment.AdjAmt=this.AdjAmt;
			adjustment.PatNum=this.PatNum;
			adjustment.AdjType=this.AdjType;
			adjustment.ProvNum=this.ProvNum;
			adjustment.AdjNote=this.AdjNote;
			adjustment.ProcDate=this.ProcDate;
			adjustment.ProcNum=this.ProcNum;
			adjustment.DateEntry=this.DateEntry;
			adjustment.ClinicNum=this.ClinicNum;
			adjustment.StatementNum=this.StatementNum;
			return adjustment;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Adjustment>");
			sb.append("<AdjNum>").append(AdjNum).append("</AdjNum>");
			sb.append("<AdjDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AdjDate)).append("</AdjDate>");
			sb.append("<AdjAmt>").append(AdjAmt).append("</AdjAmt>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<AdjType>").append(AdjType).append("</AdjType>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<AdjNote>").append(Serializing.escapeForXml(AdjNote)).append("</AdjNote>");
			sb.append("<ProcDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(ProcDate)).append("</ProcDate>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<DateEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEntry)).append("</DateEntry>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<StatementNum>").append(StatementNum).append("</StatementNum>");
			sb.append("</Adjustment>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AdjNum")!=null) {
					AdjNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AdjNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AdjDate")!=null) {
					AdjDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"AdjDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"AdjAmt")!=null) {
					AdjAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"AdjAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AdjType")!=null) {
					AdjType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AdjType"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AdjNote")!=null) {
					AdjNote=Serializing.getXmlNodeValue(doc,"AdjNote");
				}
				if(Serializing.getXmlNodeValue(doc,"ProcDate")!=null) {
					ProcDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"ProcDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcNum")!=null) {
					ProcNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateEntry")!=null) {
					DateEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateEntry"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"StatementNum")!=null) {
					StatementNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"StatementNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
