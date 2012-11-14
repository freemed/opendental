package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				FieldNum=Integer.valueOf(doc.getElementsByTagName("FieldNum").item(0).getFirstChild().getNodeValue());
				LetterMergeNum=Integer.valueOf(doc.getElementsByTagName("LetterMergeNum").item(0).getFirstChild().getNodeValue());
				FieldName=doc.getElementsByTagName("FieldName").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
