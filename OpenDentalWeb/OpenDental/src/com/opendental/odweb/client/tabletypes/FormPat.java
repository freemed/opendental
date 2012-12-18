package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class FormPat {
		/** Primary key. */
		public int FormPatNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** The date and time that this questionnaire was filled out. */
		public Date FormDateTime;

		/** Deep copy of object. */
		public FormPat deepCopy() {
			FormPat formpat=new FormPat();
			formpat.FormPatNum=this.FormPatNum;
			formpat.PatNum=this.PatNum;
			formpat.FormDateTime=this.FormDateTime;
			return formpat;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<FormPat>");
			sb.append("<FormPatNum>").append(FormPatNum).append("</FormPatNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<FormDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(FormDateTime)).append("</FormDateTime>");
			sb.append("</FormPat>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"FormPatNum")!=null) {
					FormPatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FormPatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FormDateTime")!=null) {
					FormDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"FormDateTime"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
