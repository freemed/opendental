package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Disease deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Disease>");
			sb.append("<DiseaseNum>").append(DiseaseNum).append("</DiseaseNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DiseaseDefNum>").append(DiseaseDefNum).append("</DiseaseDefNum>");
			sb.append("<PatNote>").append(Serializing.escapeForXml(PatNote)).append("</PatNote>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<ICD9Num>").append(ICD9Num).append("</ICD9Num>");
			sb.append("<ProbStatus>").append(ProbStatus.ordinal()).append("</ProbStatus>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStart)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStop)).append("</DateStop>");
			sb.append("</Disease>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DiseaseNum")!=null) {
					DiseaseNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DiseaseNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DiseaseDefNum")!=null) {
					DiseaseDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DiseaseDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNote")!=null) {
					PatNote=Serializing.getXmlNodeValue(doc,"PatNote");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"ICD9Num")!=null) {
					ICD9Num=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ICD9Num"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProbStatus")!=null) {
					ProbStatus=ProblemStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProbStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"DateStart")!=null) {
					DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStart"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStop")!=null) {
					DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStop"));
				}
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
