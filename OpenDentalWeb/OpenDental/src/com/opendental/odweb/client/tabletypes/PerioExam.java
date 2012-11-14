package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class PerioExam {
		/** Primary key. */
		public int PerioExamNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** . */
		public String ExamDate;
		/** FK to provider.ProvNum. */
		public int ProvNum;

		/** Deep copy of object. */
		public PerioExam Copy() {
			PerioExam perioexam=new PerioExam();
			perioexam.PerioExamNum=this.PerioExamNum;
			perioexam.PatNum=this.PatNum;
			perioexam.ExamDate=this.ExamDate;
			perioexam.ProvNum=this.ProvNum;
			return perioexam;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PerioExam>");
			sb.append("<PerioExamNum>").append(PerioExamNum).append("</PerioExamNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ExamDate>").append(Serializing.EscapeForXml(ExamDate)).append("</ExamDate>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("</PerioExam>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PerioExamNum=Integer.valueOf(doc.getElementsByTagName("PerioExamNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ExamDate=doc.getElementsByTagName("ExamDate").item(0).getFirstChild().getNodeValue();
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
