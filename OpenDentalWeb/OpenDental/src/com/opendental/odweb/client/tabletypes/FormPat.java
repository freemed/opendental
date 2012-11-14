package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class FormPat {
		/** Primary key. */
		public int FormPatNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** The date and time that this questionnaire was filled out. */
		public String FormDateTime;

		/** Deep copy of object. */
		public FormPat Copy() {
			FormPat formpat=new FormPat();
			formpat.FormPatNum=this.FormPatNum;
			formpat.PatNum=this.PatNum;
			formpat.FormDateTime=this.FormDateTime;
			return formpat;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<FormPat>");
			sb.append("<FormPatNum>").append(FormPatNum).append("</FormPatNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<FormDateTime>").append(Serializing.EscapeForXml(FormDateTime)).append("</FormDateTime>");
			sb.append("</FormPat>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				FormPatNum=Integer.valueOf(doc.getElementsByTagName("FormPatNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				FormDateTime=doc.getElementsByTagName("FormDateTime").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
