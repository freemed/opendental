package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public PatField deepCopy() {
			PatField patfield=new PatField();
			patfield.PatFieldNum=this.PatFieldNum;
			patfield.PatNum=this.PatNum;
			patfield.FieldName=this.FieldName;
			patfield.FieldValue=this.FieldValue;
			return patfield;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PatField>");
			sb.append("<PatFieldNum>").append(PatFieldNum).append("</PatFieldNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<FieldName>").append(Serializing.escapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldValue>").append(Serializing.escapeForXml(FieldValue)).append("</FieldValue>");
			sb.append("</PatField>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PatFieldNum")!=null) {
					PatFieldNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatFieldNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.getXmlNodeValue(doc,"FieldName");
				}
				if(Serializing.getXmlNodeValue(doc,"FieldValue")!=null) {
					FieldValue=Serializing.getXmlNodeValue(doc,"FieldValue");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
