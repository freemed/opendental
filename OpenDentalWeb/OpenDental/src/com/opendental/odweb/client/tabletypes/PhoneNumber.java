package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class PhoneNumber {
		/** Primary key. */
		public int PhoneNumberNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** The actual phone number for the patient.  Includes any punctuation.  No leading 1 or plus, so almost always 10 digits. */
		public String PhoneNumberVal;

		/** Deep copy of object. */
		public PhoneNumber deepCopy() {
			PhoneNumber phonenumber=new PhoneNumber();
			phonenumber.PhoneNumberNum=this.PhoneNumberNum;
			phonenumber.PatNum=this.PatNum;
			phonenumber.PhoneNumberVal=this.PhoneNumberVal;
			return phonenumber;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PhoneNumber>");
			sb.append("<PhoneNumberNum>").append(PhoneNumberNum).append("</PhoneNumberNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<PhoneNumberVal>").append(Serializing.escapeForXml(PhoneNumberVal)).append("</PhoneNumberVal>");
			sb.append("</PhoneNumber>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PhoneNumberNum")!=null) {
					PhoneNumberNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PhoneNumberNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PhoneNumberVal")!=null) {
					PhoneNumberVal=Serializing.getXmlNodeValue(doc,"PhoneNumberVal");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
