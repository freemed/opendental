package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public Contact Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Contact>");
			sb.append("<ContactNum>").append(ContactNum).append("</ContactNum>");
			sb.append("<LName>").append(Serializing.EscapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.EscapeForXml(FName)).append("</FName>");
			sb.append("<WkPhone>").append(Serializing.EscapeForXml(WkPhone)).append("</WkPhone>");
			sb.append("<Fax>").append(Serializing.EscapeForXml(Fax)).append("</Fax>");
			sb.append("<Category>").append(Category).append("</Category>");
			sb.append("<Notes>").append(Serializing.EscapeForXml(Notes)).append("</Notes>");
			sb.append("</Contact>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ContactNum")!=null) {
					ContactNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ContactNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.GetXmlNodeValue(doc,"LName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.GetXmlNodeValue(doc,"FName");
				}
				if(Serializing.GetXmlNodeValue(doc,"WkPhone")!=null) {
					WkPhone=Serializing.GetXmlNodeValue(doc,"WkPhone");
				}
				if(Serializing.GetXmlNodeValue(doc,"Fax")!=null) {
					Fax=Serializing.GetXmlNodeValue(doc,"Fax");
				}
				if(Serializing.GetXmlNodeValue(doc,"Category")!=null) {
					Category=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Category"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.GetXmlNodeValue(doc,"Notes");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
