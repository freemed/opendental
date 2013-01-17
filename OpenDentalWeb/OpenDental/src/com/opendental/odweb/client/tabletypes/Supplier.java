package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Supplier {
		/** Primary key. */
		public int SupplierNum;
		/** . */
		public String Name;
		/** . */
		public String Phone;
		/** The customer ID that this office uses for transactions with the supplier */
		public String CustomerId;
		/** Full address to website.  We might make it clickable. */
		public String Website;
		/** The username used to log in to the supplier website. */
		public String UserName;
		/** The password to log in to the supplier website.  Not encrypted or hidden in any way. */
		public String Password;
		/** Any note regarding supplier.  Could hold address, CC info, etc. */
		public String Note;

		/** Deep copy of object. */
		public Supplier deepCopy() {
			Supplier supplier=new Supplier();
			supplier.SupplierNum=this.SupplierNum;
			supplier.Name=this.Name;
			supplier.Phone=this.Phone;
			supplier.CustomerId=this.CustomerId;
			supplier.Website=this.Website;
			supplier.UserName=this.UserName;
			supplier.Password=this.Password;
			supplier.Note=this.Note;
			return supplier;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Supplier>");
			sb.append("<SupplierNum>").append(SupplierNum).append("</SupplierNum>");
			sb.append("<Name>").append(Serializing.escapeForXml(Name)).append("</Name>");
			sb.append("<Phone>").append(Serializing.escapeForXml(Phone)).append("</Phone>");
			sb.append("<CustomerId>").append(Serializing.escapeForXml(CustomerId)).append("</CustomerId>");
			sb.append("<Website>").append(Serializing.escapeForXml(Website)).append("</Website>");
			sb.append("<UserName>").append(Serializing.escapeForXml(UserName)).append("</UserName>");
			sb.append("<Password>").append(Serializing.escapeForXml(Password)).append("</Password>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("</Supplier>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SupplierNum")!=null) {
					SupplierNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplierNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Name")!=null) {
					Name=Serializing.getXmlNodeValue(doc,"Name");
				}
				if(Serializing.getXmlNodeValue(doc,"Phone")!=null) {
					Phone=Serializing.getXmlNodeValue(doc,"Phone");
				}
				if(Serializing.getXmlNodeValue(doc,"CustomerId")!=null) {
					CustomerId=Serializing.getXmlNodeValue(doc,"CustomerId");
				}
				if(Serializing.getXmlNodeValue(doc,"Website")!=null) {
					Website=Serializing.getXmlNodeValue(doc,"Website");
				}
				if(Serializing.getXmlNodeValue(doc,"UserName")!=null) {
					UserName=Serializing.getXmlNodeValue(doc,"UserName");
				}
				if(Serializing.getXmlNodeValue(doc,"Password")!=null) {
					Password=Serializing.getXmlNodeValue(doc,"Password");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Supplier: "+e.getMessage());
			}
		}


}
