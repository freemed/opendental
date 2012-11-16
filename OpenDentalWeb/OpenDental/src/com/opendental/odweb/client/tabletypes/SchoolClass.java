package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class SchoolClass {
		/** Primary key. */
		public int SchoolClassNum;
		/** The year this class will graduate */
		public int GradYear;
		/** Description of this class. eg Dental or Hygiene */
		public String Descript;

		/** Deep copy of object. */
		public SchoolClass Copy() {
			SchoolClass schoolclass=new SchoolClass();
			schoolclass.SchoolClassNum=this.SchoolClassNum;
			schoolclass.GradYear=this.GradYear;
			schoolclass.Descript=this.Descript;
			return schoolclass;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SchoolClass>");
			sb.append("<SchoolClassNum>").append(SchoolClassNum).append("</SchoolClassNum>");
			sb.append("<GradYear>").append(GradYear).append("</GradYear>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("</SchoolClass>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"SchoolClassNum")!=null) {
					SchoolClassNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SchoolClassNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"GradYear")!=null) {
					GradYear=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"GradYear"));
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
