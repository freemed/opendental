package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class SchoolClass {
		/** Primary key. */
		public int SchoolClassNum;
		/** The year this class will graduate */
		public int GradYear;
		/** Description of this class. eg Dental or Hygiene */
		public String Descript;

		/** Deep copy of object. */
		public SchoolClass deepCopy() {
			SchoolClass schoolclass=new SchoolClass();
			schoolclass.SchoolClassNum=this.SchoolClassNum;
			schoolclass.GradYear=this.GradYear;
			schoolclass.Descript=this.Descript;
			return schoolclass;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SchoolClass>");
			sb.append("<SchoolClassNum>").append(SchoolClassNum).append("</SchoolClassNum>");
			sb.append("<GradYear>").append(GradYear).append("</GradYear>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("</SchoolClass>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SchoolClassNum")!=null) {
					SchoolClassNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SchoolClassNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"GradYear")!=null) {
					GradYear=Integer.valueOf(Serializing.getXmlNodeValue(doc,"GradYear"));
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing SchoolClass: "+e.getMessage());
			}
		}


}
