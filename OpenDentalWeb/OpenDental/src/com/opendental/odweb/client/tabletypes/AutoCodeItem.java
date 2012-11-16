package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class AutoCodeItem {
		/** Primary key. */
		public int AutoCodeItemNum;
		/** FK to autocode.AutoCodeNum */
		public int AutoCodeNum;
		/** Do not use */
		public String OldCode;
		/** FK to procedurecode.CodeNum */
		public int CodeNum;

		/** Deep copy of object. */
		public AutoCodeItem Copy() {
			AutoCodeItem autocodeitem=new AutoCodeItem();
			autocodeitem.AutoCodeItemNum=this.AutoCodeItemNum;
			autocodeitem.AutoCodeNum=this.AutoCodeNum;
			autocodeitem.OldCode=this.OldCode;
			autocodeitem.CodeNum=this.CodeNum;
			return autocodeitem;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutoCodeItem>");
			sb.append("<AutoCodeItemNum>").append(AutoCodeItemNum).append("</AutoCodeItemNum>");
			sb.append("<AutoCodeNum>").append(AutoCodeNum).append("</AutoCodeNum>");
			sb.append("<OldCode>").append(Serializing.EscapeForXml(OldCode)).append("</OldCode>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("</AutoCodeItem>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"AutoCodeItemNum")!=null) {
					AutoCodeItemNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AutoCodeItemNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AutoCodeNum")!=null) {
					AutoCodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AutoCodeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OldCode")!=null) {
					OldCode=Serializing.GetXmlNodeValue(doc,"OldCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CodeNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
