package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class LetterMergeField {
		/** Primary key. */
		public int FieldNum;
		/** FK to lettermerge.LetterMergeNum. */
		public int LetterMergeNum;
		/** One of the preset available field names. */
		public String FieldName;

		/** Deep copy of object. */
		public LetterMergeField deepCopy() {
			LetterMergeField lettermergefield=new LetterMergeField();
			lettermergefield.FieldNum=this.FieldNum;
			lettermergefield.LetterMergeNum=this.LetterMergeNum;
			lettermergefield.FieldName=this.FieldName;
			return lettermergefield;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LetterMergeField>");
			sb.append("<FieldNum>").append(FieldNum).append("</FieldNum>");
			sb.append("<LetterMergeNum>").append(LetterMergeNum).append("</LetterMergeNum>");
			sb.append("<FieldName>").append(Serializing.escapeForXml(FieldName)).append("</FieldName>");
			sb.append("</LetterMergeField>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"FieldNum")!=null) {
					FieldNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FieldNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LetterMergeNum")!=null) {
					LetterMergeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LetterMergeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.getXmlNodeValue(doc,"FieldName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
