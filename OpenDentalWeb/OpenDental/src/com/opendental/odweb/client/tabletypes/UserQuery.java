package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class UserQuery {
		/** Primary key. */
		public int QueryNum;
		/** Description. */
		public String Description;
		/** The name of the file to export to. */
		public String FileName;
		/** The text of the query. */
		public String QueryText;

		/** Deep copy of object. */
		public UserQuery deepCopy() {
			UserQuery userquery=new UserQuery();
			userquery.QueryNum=this.QueryNum;
			userquery.Description=this.Description;
			userquery.FileName=this.FileName;
			userquery.QueryText=this.QueryText;
			return userquery;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<UserQuery>");
			sb.append("<QueryNum>").append(QueryNum).append("</QueryNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<FileName>").append(Serializing.escapeForXml(FileName)).append("</FileName>");
			sb.append("<QueryText>").append(Serializing.escapeForXml(QueryText)).append("</QueryText>");
			sb.append("</UserQuery>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"QueryNum")!=null) {
					QueryNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"QueryNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"FileName")!=null) {
					FileName=Serializing.getXmlNodeValue(doc,"FileName");
				}
				if(Serializing.getXmlNodeValue(doc,"QueryText")!=null) {
					QueryText=Serializing.getXmlNodeValue(doc,"QueryText");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing UserQuery: "+e.getMessage());
			}
		}


}
