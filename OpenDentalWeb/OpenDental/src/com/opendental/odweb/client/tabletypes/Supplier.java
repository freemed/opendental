package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public Supplier Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Supplier>");
			sb.append("<SupplierNum>").append(SupplierNum).append("</SupplierNum>");
			sb.append("<Name>").append(Serializing.EscapeForXml(Name)).append("</Name>");
			sb.append("<Phone>").append(Serializing.EscapeForXml(Phone)).append("</Phone>");
			sb.append("<CustomerId>").append(Serializing.EscapeForXml(CustomerId)).append("</CustomerId>");
			sb.append("<Website>").append(Serializing.EscapeForXml(Website)).append("</Website>");
			sb.append("<UserName>").append(Serializing.EscapeForXml(UserName)).append("</UserName>");
			sb.append("<Password>").append(Serializing.EscapeForXml(Password)).append("</Password>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("</Supplier>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SupplierNum=Integer.valueOf(doc.getElementsByTagName("SupplierNum").item(0).getFirstChild().getNodeValue());
				Name=doc.getElementsByTagName("Name").item(0).getFirstChild().getNodeValue();
				Phone=doc.getElementsByTagName("Phone").item(0).getFirstChild().getNodeValue();
				CustomerId=doc.getElementsByTagName("CustomerId").item(0).getFirstChild().getNodeValue();
				Website=doc.getElementsByTagName("Website").item(0).getFirstChild().getNodeValue();
				UserName=doc.getElementsByTagName("UserName").item(0).getFirstChild().getNodeValue();
				Password=doc.getElementsByTagName("Password").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
