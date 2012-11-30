package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class LetterMergeField {
		/** Primary key. */
		public int FieldNum;
		/** FK to lettermerge.LetterMergeNum. */
		public int LetterMergeNum;
		/** One of the preset available field names. */
		public String FieldName;

		/** Deep copy of object. */
		public LetterMergeField Copy() {
			LetterMergeField lettermergefield=new LetterMergeField();
			lettermergefield.FieldNum=this.FieldNum;
			lettermergefield.LetterMergeNum=this.LetterMergeNum;
			lettermergefield.FieldName=this.FieldName;
			return lettermergefield;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LetterMergeField>");
			sb.append("<FieldNum>").append(FieldNum).append("</FieldNum>");
			sb.append("<LetterMergeNum>").append(LetterMergeNum).append("</LetterMergeNum>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("</LetterMergeField>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"FieldNum")!=null) {
					FieldNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FieldNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LetterMergeNum")!=null) {
					LetterMergeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LetterMergeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.GetXmlNodeValue(doc,"FieldName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
