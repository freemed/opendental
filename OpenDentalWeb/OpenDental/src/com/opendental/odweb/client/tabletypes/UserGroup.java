package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class UserGroup {
		/** Primary key. */
		public int UserGroupNum;
		/** . */
		public String Description;

		/** Deep copy of object. */
		public UserGroup Copy() {
			UserGroup usergroup=new UserGroup();
			usergroup.UserGroupNum=this.UserGroupNum;
			usergroup.Description=this.Description;
			return usergroup;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<UserGroup>");
			sb.append("<UserGroupNum>").append(UserGroupNum).append("</UserGroupNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("</UserGroup>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				UserGroupNum=Integer.valueOf(doc.getElementsByTagName("UserGroupNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
