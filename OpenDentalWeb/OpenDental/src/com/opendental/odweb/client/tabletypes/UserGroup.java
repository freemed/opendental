package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class UserGroup {
		/** Primary key. */
		public int UserGroupNum;
		/** . */
		public String Description;

		/** Deep copy of object. */
		public UserGroup deepCopy() {
			UserGroup usergroup=new UserGroup();
			usergroup.UserGroupNum=this.UserGroupNum;
			usergroup.Description=this.Description;
			return usergroup;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<UserGroup>");
			sb.append("<UserGroupNum>").append(UserGroupNum).append("</UserGroupNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("</UserGroup>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"UserGroupNum")!=null) {
					UserGroupNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserGroupNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing UserGroup: "+e.getMessage());
			}
		}


}
