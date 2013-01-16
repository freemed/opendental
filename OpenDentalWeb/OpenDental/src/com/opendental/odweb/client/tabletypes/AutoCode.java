package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public AutoCode deepCopy() {
			AutoCode autocode=new AutoCode();
			autocode.AutoCodeNum=this.AutoCodeNum;
			autocode.Description=this.Description;
			autocode.IsHidden=this.IsHidden;
			autocode.LessIntrusive=this.LessIntrusive;
			return autocode;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutoCode>");
			sb.append("<AutoCodeNum>").append(AutoCodeNum).append("</AutoCodeNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<LessIntrusive>").append((LessIntrusive)?1:0).append("</LessIntrusive>");
			sb.append("</AutoCode>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AutoCodeNum")!=null) {
					AutoCodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutoCodeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"LessIntrusive")!=null) {
					LessIntrusive=(Serializing.getXmlNodeValue(doc,"LessIntrusive")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
