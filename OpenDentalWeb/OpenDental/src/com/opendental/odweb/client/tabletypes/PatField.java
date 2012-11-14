package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class PatField {
		/** Primary key. */
		public int PatFieldNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** FK to patfielddef.FieldName.  The full name is shown here for ease of use when running queries.  But the user is only allowed to change fieldNames in the patFieldDef setup window. */
		public String FieldName;
		/** Any text that the user types in.  For picklists, this will contain the picked text.  For dates, this is stored as the user typed it, after validating that it could be parsed.  So queries that involve dates won't work very well.  If we want better handling of date fields, we should add a column to this table.  Checkbox will either have a value of 1, or else the row will be deleted from the db.  Currency is handled in a culture neutral way, just like other currency in the db. */
		public String FieldValue;

		/** Deep copy of object. */
		public PatField Copy() {
			PatField patfield=new PatField();
			patfield.PatFieldNum=this.PatFieldNum;
			patfield.PatNum=this.PatNum;
			patfield.FieldName=this.FieldName;
			patfield.FieldValue=this.FieldValue;
			return patfield;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PatField>");
			sb.append("<PatFieldNum>").append(PatFieldNum).append("</PatFieldNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldValue>").append(Serializing.EscapeForXml(FieldValue)).append("</FieldValue>");
			sb.append("</PatField>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PatFieldNum=Integer.valueOf(doc.getElementsByTagName("PatFieldNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				FieldName=doc.getElementsByTagName("FieldName").item(0).getFirstChild().getNodeValue();
				FieldValue=doc.getElementsByTagName("FieldValue").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
