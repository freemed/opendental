package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class ProcButtonItem {
		/** Primary key. */
		public int ProcButtonItemNum;
		/** FK to procbutton.ProcButtonNum. */
		public int ProcButtonNum;
		/** Do not use. */
		public String OldCode;
		/** FK to autocode.AutoCodeNum.  0 if this is a procedure code. */
		public int AutoCodeNum;
		/** FK to procedurecode.CodeNum.  0 if this is an autocode. */
		public int CodeNum;

		/** Deep copy of object. */
		public ProcButtonItem deepCopy() {
			ProcButtonItem procbuttonitem=new ProcButtonItem();
			procbuttonitem.ProcButtonItemNum=this.ProcButtonItemNum;
			procbuttonitem.ProcButtonNum=this.ProcButtonNum;
			procbuttonitem.OldCode=this.OldCode;
			procbuttonitem.AutoCodeNum=this.AutoCodeNum;
			procbuttonitem.CodeNum=this.CodeNum;
			return procbuttonitem;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcButtonItem>");
			sb.append("<ProcButtonItemNum>").append(ProcButtonItemNum).append("</ProcButtonItemNum>");
			sb.append("<ProcButtonNum>").append(ProcButtonNum).append("</ProcButtonNum>");
			sb.append("<OldCode>").append(Serializing.escapeForXml(OldCode)).append("</OldCode>");
			sb.append("<AutoCodeNum>").append(AutoCodeNum).append("</AutoCodeNum>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("</ProcButtonItem>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ProcButtonItemNum")!=null) {
					ProcButtonItemNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcButtonItemNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcButtonNum")!=null) {
					ProcButtonNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcButtonNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"OldCode")!=null) {
					OldCode=Serializing.getXmlNodeValue(doc,"OldCode");
				}
				if(Serializing.getXmlNodeValue(doc,"AutoCodeNum")!=null) {
					AutoCodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutoCodeNum"));
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
