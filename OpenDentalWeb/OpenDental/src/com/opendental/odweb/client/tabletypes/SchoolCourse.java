package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class SchoolCourse {
		/** Primary key. */
		public int SchoolCourseNum;
		/** Alphanumeric.  eg PEDO 732. */
		public String CourseID;
		/** eg: Pediatric Dentistry Clinic II */
		public String Descript;

		/** Deep copy of object. */
		public SchoolCourse deepCopy() {
			SchoolCourse schoolcourse=new SchoolCourse();
			schoolcourse.SchoolCourseNum=this.SchoolCourseNum;
			schoolcourse.CourseID=this.CourseID;
			schoolcourse.Descript=this.Descript;
			return schoolcourse;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SchoolCourse>");
			sb.append("<SchoolCourseNum>").append(SchoolCourseNum).append("</SchoolCourseNum>");
			sb.append("<CourseID>").append(Serializing.escapeForXml(CourseID)).append("</CourseID>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("</SchoolCourse>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SchoolCourseNum")!=null) {
					SchoolCourseNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SchoolCourseNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CourseID")!=null) {
					CourseID=Serializing.getXmlNodeValue(doc,"CourseID");
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
