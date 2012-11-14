package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ICD9 {
		/** Primary key. */
		public int ICD9Num;
		/** Not allowed to edit this column once saved in the database. */
		public String ICD9Code;
		/** Description. */
		public String Description;
		/** The last date and time this row was altered.  Not user editable. */
		public String DateTStamp;

		/** Deep copy of object. */
		public ICD9 Copy() {
			ICD9 icd9=new ICD9();
			icd9.ICD9Num=this.ICD9Num;
			icd9.ICD9Code=this.ICD9Code;
			icd9.Description=this.Description;
			icd9.DateTStamp=this.DateTStamp;
			return icd9;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ICD9>");
			sb.append("<ICD9Num>").append(ICD9Num).append("</ICD9Num>");
			sb.append("<ICD9Code>").append(Serializing.EscapeForXml(ICD9Code)).append("</ICD9Code>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<DateTStamp>").append(Serializing.EscapeForXml(DateTStamp)).append("</DateTStamp>");
			sb.append("</ICD9>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ICD9Num=Integer.valueOf(doc.getElementsByTagName("ICD9Num").item(0).getFirstChild().getNodeValue());
				ICD9Code=doc.getElementsByTagName("ICD9Code").item(0).getFirstChild().getNodeValue();
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				DateTStamp=doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
