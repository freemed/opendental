package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"PrefNum")!=null) {
					PrefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PrefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PrefName")!=null) {
					PrefName=Serializing.GetXmlNodeValue(doc,"PrefName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ValueString")!=null) {
					ValueString=Serializing.GetXmlNodeValue(doc,"ValueString");
				}
				if(Serializing.GetXmlNodeValue(doc,"Comments")!=null) {
					Comments=Serializing.GetXmlNodeValue(doc,"Comments");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
