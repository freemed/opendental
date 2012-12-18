package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public ReqStudent deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ReqStudent>");
			sb.append("<ReqStudentNum>").append(ReqStudentNum).append("</ReqStudentNum>");
			sb.append("<ReqNeededNum>").append(ReqNeededNum).append("</ReqNeededNum>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("<SchoolCourseNum>").append(SchoolCourseNum).append("</SchoolCourseNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<InstructorNum>").append(InstructorNum).append("</InstructorNum>");
			sb.append("<DateCompleted>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateCompleted)).append("</DateCompleted>");
			sb.append("</ReqStudent>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ReqStudentNum")!=null) {
					ReqStudentNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReqStudentNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ReqNeededNum")!=null) {
					ReqNeededNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReqNeededNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
				if(Serializing.getXmlNodeValue(doc,"SchoolCourseNum")!=null) {
					SchoolCourseNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SchoolCourseNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AptNum")!=null) {
					AptNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AptNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"InstructorNum")!=null) {
					InstructorNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InstructorNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateCompleted")!=null) {
					DateCompleted=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateCompleted"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
