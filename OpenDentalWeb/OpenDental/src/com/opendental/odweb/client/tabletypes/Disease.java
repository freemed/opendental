package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Disease {
		/** Primary key. */
		public int DiseaseNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** FK to diseasedef.DiseaseDefNum.  The disease description is in that table.  Will be zero if ICD9Num has a value. */
		public int DiseaseDefNum;
		/** Any note about this disease that is specific to this patient. */
		public String PatNote;
		/** The last date and time this row was altered.  Not user editable.  Will be set to NOW by OD if this patient gets an OnlinePassword assigned. */
		public Date DateTStamp;
		/** FK to icd9.ICD9Num.  Will be zero if DiseaseDefNum has a value. */
		public int ICD9Num;
		/** Enum:ProblemStatus Active=0, Resolved=1, Inactive=2. */
		public ProblemStatus ProbStatus;
		/** Date that the disease was diagnosed.  Can be minval if unknown. */
		public Date DateStart;
		/** Date that the disease was set resolved or inactive.  Will be minval if still active.  ProbStatus should be used to determine if it is active or not. */
		public Date DateStop;

		/** Deep copy of object. */
		public Disease Copy() {
			Disease disease=new Disease();
			disease.DiseaseNum=this.DiseaseNum;
			disease.PatNum=this.PatNum;
			disease.DiseaseDefNum=this.DiseaseDefNum;
			disease.PatNote=this.PatNote;
			disease.DateTStamp=this.DateTStamp;
			disease.ICD9Num=this.ICD9Num;
			disease.ProbStatus=this.ProbStatus;
			disease.DateStart=this.DateStart;
			disease.DateStop=this.DateStop;
			return disease;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Disease>");
			sb.append("<DiseaseNum>").append(DiseaseNum).append("</DiseaseNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DiseaseDefNum>").append(DiseaseDefNum).append("</DiseaseDefNum>");
			sb.append("<PatNote>").append(Serializing.EscapeForXml(PatNote)).append("</PatNote>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTStamp>");
			sb.append("<ICD9Num>").append(ICD9Num).append("</ICD9Num>");
			sb.append("<ProbStatus>").append(ProbStatus.ordinal()).append("</ProbStatus>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateStop>");
			sb.append("</Disease>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				DiseaseNum=Integer.valueOf(doc.getElementsByTagName("DiseaseNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DiseaseDefNum=Integer.valueOf(doc.getElementsByTagName("DiseaseDefNum").item(0).getFirstChild().getNodeValue());
				PatNote=doc.getElementsByTagName("PatNote").item(0).getFirstChild().getNodeValue();
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				ICD9Num=Integer.valueOf(doc.getElementsByTagName("ICD9Num").item(0).getFirstChild().getNodeValue());
				ProbStatus=ProblemStatus.values()[Integer.valueOf(doc.getElementsByTagName("ProbStatus").item(0).getFirstChild().getNodeValue())];
				DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateStart").item(0).getFirstChild().getNodeValue());
				DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateStop").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** 0=Active, 1=Resolved, 2=Inactive */
		public enum ProblemStatus {
			/** 0 */
			Active,
			/** 1 */
			Resolved,
			/** 2 */
			Inactive
		}


}
