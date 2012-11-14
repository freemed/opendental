package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public ReqNeeded Copy() {
			ReqNeeded reqneeded=new ReqNeeded();
			reqneeded.ReqNeededNum=this.ReqNeededNum;
			reqneeded.Descript=this.Descript;
			reqneeded.SchoolCourseNum=this.SchoolCourseNum;
			reqneeded.SchoolClassNum=this.SchoolClassNum;
			return reqneeded;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ReqNeeded>");
			sb.append("<ReqNeededNum>").append(ReqNeededNum).append("</ReqNeededNum>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<SchoolCourseNum>").append(SchoolCourseNum).append("</SchoolCourseNum>");
			sb.append("<SchoolClassNum>").append(SchoolClassNum).append("</SchoolClassNum>");
			sb.append("</ReqNeeded>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ReqNeededNum=Integer.valueOf(doc.getElementsByTagName("ReqNeededNum").item(0).getFirstChild().getNodeValue());
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				SchoolCourseNum=Integer.valueOf(doc.getElementsByTagName("SchoolCourseNum").item(0).getFirstChild().getNodeValue());
				SchoolClassNum=Integer.valueOf(doc.getElementsByTagName("SchoolClassNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
