package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public AutoCodeItem deepCopy() {
			AutoCodeItem autocodeitem=new AutoCodeItem();
			autocodeitem.AutoCodeItemNum=this.AutoCodeItemNum;
			autocodeitem.AutoCodeNum=this.AutoCodeNum;
			autocodeitem.OldCode=this.OldCode;
			autocodeitem.CodeNum=this.CodeNum;
			return autocodeitem;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutoCodeItem>");
			sb.append("<AutoCodeItemNum>").append(AutoCodeItemNum).append("</AutoCodeItemNum>");
			sb.append("<AutoCodeNum>").append(AutoCodeNum).append("</AutoCodeNum>");
			sb.append("<OldCode>").append(Serializing.escapeForXml(OldCode)).append("</OldCode>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("</AutoCodeItem>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AutoCodeItemNum")!=null) {
					AutoCodeItemNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutoCodeItemNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AutoCodeNum")!=null) {
					AutoCodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutoCodeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"OldCode")!=null) {
					OldCode=Serializing.getXmlNodeValue(doc,"OldCode");
				}
				if(Serializing.getXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CodeNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
