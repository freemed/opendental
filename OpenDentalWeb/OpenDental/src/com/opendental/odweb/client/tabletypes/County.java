package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class County {
		/** Primary Key. */
		public int CountyNum;
		/** Frequently used as the primary key of this table.  But it's allowed to change.  Change is programmatically synchronized. */
		public String CountyName;
		/** Optional. Usage varies. */
		public String CountyCode;

		/** Deep copy of object. */
		public County deepCopy() {
			County county=new County();
			county.CountyNum=this.CountyNum;
			county.CountyName=this.CountyName;
			county.CountyCode=this.CountyCode;
			return county;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<County>");
			sb.append("<CountyNum>").append(CountyNum).append("</CountyNum>");
			sb.append("<CountyName>").append(Serializing.escapeForXml(CountyName)).append("</CountyName>");
			sb.append("<CountyCode>").append(Serializing.escapeForXml(CountyCode)).append("</CountyCode>");
			sb.append("</County>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CountyNum")!=null) {
					CountyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CountyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CountyName")!=null) {
					CountyName=Serializing.getXmlNodeValue(doc,"CountyName");
				}
				if(Serializing.getXmlNodeValue(doc,"CountyCode")!=null) {
					CountyCode=Serializing.getXmlNodeValue(doc,"CountyCode");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
