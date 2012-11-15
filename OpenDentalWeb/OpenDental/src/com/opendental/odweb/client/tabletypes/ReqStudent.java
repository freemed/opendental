package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class ReqStudent {
		/** Primary key. */
		public int ReqStudentNum;
		/** FK to reqneeded.ReqNeededNum. */
		public int ReqNeededNum;
		/** . */
		public String Descript;
		/** FK to schoolcourse.SchoolCourseNum.  Never 0. */
		public int SchoolCourseNum;
		/** FK to provider.ProvNum.  The student.  Never 0. */
		public int ProvNum;
		/** FK to appointment.AptNum. */
		public int AptNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** FK to provider.ProvNum */
		public int InstructorNum;
		/** The date that the requirement was completed. */
		public Date DateCompleted;

		/** Deep copy of object. */
		public ReqStudent Copy() {
			ReqStudent reqstudent=new ReqStudent();
			reqstudent.ReqStudentNum=this.ReqStudentNum;
			reqstudent.ReqNeededNum=this.ReqNeededNum;
			reqstudent.Descript=this.Descript;
			reqstudent.SchoolCourseNum=this.SchoolCourseNum;
			reqstudent.ProvNum=this.ProvNum;
			reqstudent.AptNum=this.AptNum;
			reqstudent.PatNum=this.PatNum;
			reqstudent.InstructorNum=this.InstructorNum;
			reqstudent.DateCompleted=this.DateCompleted;
			return reqstudent;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ReqStudent>");
			sb.append("<ReqStudentNum>").append(ReqStudentNum).append("</ReqStudentNum>");
			sb.append("<ReqNeededNum>").append(ReqNeededNum).append("</ReqNeededNum>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<SchoolCourseNum>").append(SchoolCourseNum).append("</SchoolCourseNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<InstructorNum>").append(InstructorNum).append("</InstructorNum>");
			sb.append("<DateCompleted>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateCompleted>");
			sb.append("</ReqStudent>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ReqStudentNum=Integer.valueOf(doc.getElementsByTagName("ReqStudentNum").item(0).getFirstChild().getNodeValue());
				ReqNeededNum=Integer.valueOf(doc.getElementsByTagName("ReqNeededNum").item(0).getFirstChild().getNodeValue());
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				SchoolCourseNum=Integer.valueOf(doc.getElementsByTagName("SchoolCourseNum").item(0).getFirstChild().getNodeValue());
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				AptNum=Integer.valueOf(doc.getElementsByTagName("AptNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				InstructorNum=Integer.valueOf(doc.getElementsByTagName("InstructorNum").item(0).getFirstChild().getNodeValue());
				DateCompleted=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateCompleted").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
