package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class AutoCode {
		/** Primary key. */
		public int AutoCodeNum;
		/** Displays meaningful decription, like "Amalgam". */
		public String Description;
		/** User can hide autocodes */
		public boolean IsHidden;
		/** This will be true if user no longer wants to see this autocode message when closing a procedure. This makes it less intrusive, but it can still be used in procedure buttons. */
		public boolean LessIntrusive;

		/** Deep copy of object. */
		public AutoCode Copy() {
			AutoCode autocode=new AutoCode();
			autocode.AutoCodeNum=this.AutoCodeNum;
			autocode.Description=this.Description;
			autocode.IsHidden=this.IsHidden;
			autocode.LessIntrusive=this.LessIntrusive;
			return autocode;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutoCode>");
			sb.append("<AutoCodeNum>").append(AutoCodeNum).append("</AutoCodeNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<LessIntrusive>").append((LessIntrusive)?1:0).append("</LessIntrusive>");
			sb.append("</AutoCode>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				AutoCodeNum=Integer.valueOf(doc.getElementsByTagName("AutoCodeNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				IsHidden=(doc.getElementsByTagName("IsHidden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				LessIntrusive=(doc.getElementsByTagName("LessIntrusive").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}


}
