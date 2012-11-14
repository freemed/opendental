package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class PhoneNumber {
		/** Primary key. */
		public int PhoneNumberNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** The actual phone number for the patient.  Includes any punctuation.  No leading 1 or plus, so almost always 10 digits. */
		public String PhoneNumberVal;

		/** Deep copy of object. */
		public PhoneNumber Copy() {
			PhoneNumber phonenumber=new PhoneNumber();
			phonenumber.PhoneNumberNum=this.PhoneNumberNum;
			phonenumber.PatNum=this.PatNum;
			phonenumber.PhoneNumberVal=this.PhoneNumberVal;
			return phonenumber;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PhoneNumber>");
			sb.append("<PhoneNumberNum>").append(PhoneNumberNum).append("</PhoneNumberNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<PhoneNumberVal>").append(Serializing.EscapeForXml(PhoneNumberVal)).append("</PhoneNumberVal>");
			sb.append("</PhoneNumber>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PhoneNumberNum=Integer.valueOf(doc.getElementsByTagName("PhoneNumberNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				PhoneNumberVal=doc.getElementsByTagName("PhoneNumberVal").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
