package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public UserQuery Copy() {
			UserQuery userquery=new UserQuery();
			userquery.QueryNum=this.QueryNum;
			userquery.Description=this.Description;
			userquery.FileName=this.FileName;
			userquery.QueryText=this.QueryText;
			return userquery;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<UserQuery>");
			sb.append("<QueryNum>").append(QueryNum).append("</QueryNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<FileName>").append(Serializing.EscapeForXml(FileName)).append("</FileName>");
			sb.append("<QueryText>").append(Serializing.EscapeForXml(QueryText)).append("</QueryText>");
			sb.append("</UserQuery>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"QueryNum")!=null) {
					QueryNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"QueryNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"FileName")!=null) {
					FileName=Serializing.GetXmlNodeValue(doc,"FileName");
				}
				if(Serializing.GetXmlNodeValue(doc,"QueryText")!=null) {
					QueryText=Serializing.GetXmlNodeValue(doc,"QueryText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
