package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public ProcButtonItem Copy() {
			ProcButtonItem procbuttonitem=new ProcButtonItem();
			procbuttonitem.ProcButtonItemNum=this.ProcButtonItemNum;
			procbuttonitem.ProcButtonNum=this.ProcButtonNum;
			procbuttonitem.OldCode=this.OldCode;
			procbuttonitem.AutoCodeNum=this.AutoCodeNum;
			procbuttonitem.CodeNum=this.CodeNum;
			return procbuttonitem;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcButtonItem>");
			sb.append("<ProcButtonItemNum>").append(ProcButtonItemNum).append("</ProcButtonItemNum>");
			sb.append("<ProcButtonNum>").append(ProcButtonNum).append("</ProcButtonNum>");
			sb.append("<OldCode>").append(Serializing.EscapeForXml(OldCode)).append("</OldCode>");
			sb.append("<AutoCodeNum>").append(AutoCodeNum).append("</AutoCodeNum>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("</ProcButtonItem>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ProcButtonItemNum")!=null) {
					ProcButtonItemNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcButtonItemNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcButtonNum")!=null) {
					ProcButtonNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcButtonNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OldCode")!=null) {
					OldCode=Serializing.GetXmlNodeValue(doc,"OldCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"AutoCodeNum")!=null) {
					AutoCodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AutoCodeNum"));
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
