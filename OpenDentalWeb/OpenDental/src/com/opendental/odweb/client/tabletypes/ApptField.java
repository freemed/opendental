package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ApptField {
		/** Primary key. */
		public int ApptFieldNum;
		/** FK to appointment.AptNum */
		public int AptNum;
		/** FK to apptfielddef.FieldName.  The full name is shown here for ease of use when running queries.  But the user is only allowed to change fieldNames in the patFieldDef setup window. */
		public String FieldName;
		/** Any text that the user types in.  Will later allow some automation. */
		public String FieldValue;

		/** Deep copy of object. */
		public ApptField Copy() {
			ApptField apptfield=new ApptField();
			apptfield.ApptFieldNum=this.ApptFieldNum;
			apptfield.AptNum=this.AptNum;
			apptfield.FieldName=this.FieldName;
			apptfield.FieldValue=this.FieldValue;
			return apptfield;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ApptField>");
			sb.append("<ApptFieldNum>").append(ApptFieldNum).append("</ApptFieldNum>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldValue>").append(Serializing.EscapeForXml(FieldValue)).append("</FieldValue>");
			sb.append("</ApptField>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ApptFieldNum=Integer.valueOf(doc.getElementsByTagName("ApptFieldNum").item(0).getFirstChild().getNodeValue());
				AptNum=Integer.valueOf(doc.getElementsByTagName("AptNum").item(0).getFirstChild().getNodeValue());
				FieldName=doc.getElementsByTagName("FieldName").item(0).getFirstChild().getNodeValue();
				FieldValue=doc.getElementsByTagName("FieldValue").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
