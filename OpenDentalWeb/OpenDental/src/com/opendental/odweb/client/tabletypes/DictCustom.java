package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class DictCustom {
		/** Primary key. */
		public int DictCustomNum;
		/** No space or punctuation allowed. */
		public String WordText;

		/** Deep copy of object. */
		public DictCustom Copy() {
			DictCustom dictcustom=new DictCustom();
			dictcustom.DictCustomNum=this.DictCustomNum;
			dictcustom.WordText=this.WordText;
			return dictcustom;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DictCustom>");
			sb.append("<DictCustomNum>").append(DictCustomNum).append("</DictCustomNum>");
			sb.append("<WordText>").append(Serializing.EscapeForXml(WordText)).append("</WordText>");
			sb.append("</DictCustom>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				DictCustomNum=Integer.valueOf(doc.getElementsByTagName("DictCustomNum").item(0).getFirstChild().getNodeValue());
				WordText=doc.getElementsByTagName("WordText").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
