package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Pref {
		/** Primary key. */
		public int PrefNum;
		/** The text 'key' in the key/value pairing. */
		public String PrefName;
		/** The stored value. */
		public String ValueString;
		/** Documentation on usage and values of each pref.  Mostly deprecated now in favor of using XML comments in the code. */
		public String Comments;

		/** Deep copy of object. */
		public Pref Copy() {
			Pref pref=new Pref();
			pref.PrefNum=this.PrefNum;
			pref.PrefName=this.PrefName;
			pref.ValueString=this.ValueString;
			pref.Comments=this.Comments;
			return pref;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Pref>");
			sb.append("<PrefNum>").append(PrefNum).append("</PrefNum>");
			sb.append("<PrefName>").append(Serializing.EscapeForXml(PrefName)).append("</PrefName>");
			sb.append("<ValueString>").append(Serializing.EscapeForXml(ValueString)).append("</ValueString>");
			sb.append("<Comments>").append(Serializing.EscapeForXml(Comments)).append("</Comments>");
			sb.append("</Pref>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PrefNum=Integer.valueOf(doc.getElementsByTagName("PrefNum").item(0).getFirstChild().getNodeValue());
				PrefName=doc.getElementsByTagName("PrefName").item(0).getFirstChild().getNodeValue();
				ValueString=doc.getElementsByTagName("ValueString").item(0).getFirstChild().getNodeValue();
				Comments=doc.getElementsByTagName("Comments").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
