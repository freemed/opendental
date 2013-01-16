package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Contact {
		/** Primary key. */
		public int ContactNum;
		/** Last name or, frequently, the entire name. */
		public String LName;
		/** First name is optional. */
		public String FName;
		/** Work phone. */
		public String WkPhone;
		/** Fax number. */
		public String Fax;
		/** FK to definition.DefNum */
		public int Category;
		/** Note for this contact. */
		public String Notes;

		/** Deep copy of object. */
		public Contact deepCopy() {
			Contact contact=new Contact();
			contact.ContactNum=this.ContactNum;
			contact.LName=this.LName;
			contact.FName=this.FName;
			contact.WkPhone=this.WkPhone;
			contact.Fax=this.Fax;
			contact.Category=this.Category;
			contact.Notes=this.Notes;
			return contact;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Contact>");
			sb.append("<ContactNum>").append(ContactNum).append("</ContactNum>");
			sb.append("<LName>").append(Serializing.escapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.escapeForXml(FName)).append("</FName>");
			sb.append("<WkPhone>").append(Serializing.escapeForXml(WkPhone)).append("</WkPhone>");
			sb.append("<Fax>").append(Serializing.escapeForXml(Fax)).append("</Fax>");
			sb.append("<Category>").append(Category).append("</Category>");
			sb.append("<Notes>").append(Serializing.escapeForXml(Notes)).append("</Notes>");
			sb.append("</Contact>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ContactNum")!=null) {
					ContactNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ContactNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.getXmlNodeValue(doc,"LName");
				}
				if(Serializing.getXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.getXmlNodeValue(doc,"FName");
				}
				if(Serializing.getXmlNodeValue(doc,"WkPhone")!=null) {
					WkPhone=Serializing.getXmlNodeValue(doc,"WkPhone");
				}
				if(Serializing.getXmlNodeValue(doc,"Fax")!=null) {
					Fax=Serializing.getXmlNodeValue(doc,"Fax");
				}
				if(Serializing.getXmlNodeValue(doc,"Category")!=null) {
					Category=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Category"));
				}
				if(Serializing.getXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.getXmlNodeValue(doc,"Notes");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
