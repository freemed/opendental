package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public Adjustment Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Adjustment>");
			sb.append("<AdjNum>").append(AdjNum).append("</AdjNum>");
			sb.append("<AdjDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AdjDate)).append("</AdjDate>");
			sb.append("<AdjAmt>").append(AdjAmt).append("</AdjAmt>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<AdjType>").append(AdjType).append("</AdjType>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<AdjNote>").append(Serializing.EscapeForXml(AdjNote)).append("</AdjNote>");
			sb.append("<ProcDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(ProcDate)).append("</ProcDate>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<DateEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEntry)).append("</DateEntry>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<StatementNum>").append(StatementNum).append("</StatementNum>");
			sb.append("</Adjustment>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				AdjNum=Integer.valueOf(doc.getElementsByTagName("AdjNum").item(0).getFirstChild().getNodeValue());
				AdjDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("AdjDate").item(0).getFirstChild().getNodeValue());
				AdjAmt=Double.valueOf(doc.getElementsByTagName("AdjAmt").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				AdjType=Integer.valueOf(doc.getElementsByTagName("AdjType").item(0).getFirstChild().getNodeValue());
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				AdjNote=doc.getElementsByTagName("AdjNote").item(0).getFirstChild().getNodeValue();
				ProcDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("ProcDate").item(0).getFirstChild().getNodeValue());
				ProcNum=Integer.valueOf(doc.getElementsByTagName("ProcNum").item(0).getFirstChild().getNodeValue());
				DateEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateEntry").item(0).getFirstChild().getNodeValue());
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
				StatementNum=Integer.valueOf(doc.getElementsByTagName("StatementNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
