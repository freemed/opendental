package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ReqNeeded {
		/** Primary key. */
		public int ReqNeededNum;
		/** . */
		public String Descript;
		/** FK to schoolcourse.SchoolCourseNum.  Never 0. */
		public int SchoolCourseNum;
		/** FK to schoolclass.SchoolClassNum.  Never 0. */
		public int SchoolClassNum;

		/** Deep copy of object. */
		public ReqNeeded deepCopy() {
			ReqNeeded reqneeded=new ReqNeeded();
			reqneeded.ReqNeededNum=this.ReqNeededNum;
			reqneeded.Descript=this.Descript;
			reqneeded.SchoolCourseNum=this.SchoolCourseNum;
			reqneeded.SchoolClassNum=this.SchoolClassNum;
			return reqneeded;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ReqNeeded>");
			sb.append("<ReqNeededNum>").append(ReqNeededNum).append("</ReqNeededNum>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("<SchoolCourseNum>").append(SchoolCourseNum).append("</SchoolCourseNum>");
			sb.append("<SchoolClassNum>").append(SchoolClassNum).append("</SchoolClassNum>");
			sb.append("</ReqNeeded>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ReqNeededNum")!=null) {
					ReqNeededNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReqNeededNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
				if(Serializing.getXmlNodeValue(doc,"SchoolCourseNum")!=null) {
					SchoolCourseNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SchoolCourseNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SchoolClassNum")!=null) {
					SchoolClassNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SchoolClassNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ReqNeeded: "+e.getMessage());
			}
		}


}
