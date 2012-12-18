package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class PerioExam {
		/** Primary key. */
		public int PerioExamNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** . */
		public Date ExamDate;
		/** FK to provider.ProvNum. */
		public int ProvNum;

		/** Deep copy of object. */
		public PerioExam deepCopy() {
			PerioExam perioexam=new PerioExam();
			perioexam.PerioExamNum=this.PerioExamNum;
			perioexam.PatNum=this.PatNum;
			perioexam.ExamDate=this.ExamDate;
			perioexam.ProvNum=this.ProvNum;
			return perioexam;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PerioExam>");
			sb.append("<PerioExamNum>").append(PerioExamNum).append("</PerioExamNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ExamDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(ExamDate)).append("</ExamDate>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("</PerioExam>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PerioExamNum")!=null) {
					PerioExamNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PerioExamNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ExamDate")!=null) {
					ExamDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"ExamDate"));
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
