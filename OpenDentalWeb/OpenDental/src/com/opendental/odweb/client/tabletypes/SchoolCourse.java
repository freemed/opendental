package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class SchoolCourse {
		/** Primary key. */
		public int SchoolCourseNum;
		/** Alphanumeric.  eg PEDO 732. */
		public String CourseID;
		/** eg: Pediatric Dentistry Clinic II */
		public String Descript;

		/** Deep copy of object. */
		public SchoolCourse Copy() {
			SchoolCourse schoolcourse=new SchoolCourse();
			schoolcourse.SchoolCourseNum=this.SchoolCourseNum;
			schoolcourse.CourseID=this.CourseID;
			schoolcourse.Descript=this.Descript;
			return schoolcourse;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SchoolCourse>");
			sb.append("<SchoolCourseNum>").append(SchoolCourseNum).append("</SchoolCourseNum>");
			sb.append("<CourseID>").append(Serializing.EscapeForXml(CourseID)).append("</CourseID>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("</SchoolCourse>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"SchoolCourseNum")!=null) {
					SchoolCourseNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SchoolCourseNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CourseID")!=null) {
					CourseID=Serializing.GetXmlNodeValue(doc,"CourseID");
				}
				if(Serializing.GetXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.GetXmlNodeValue(doc,"Descript");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
