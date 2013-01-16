package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class DictCustom {
		/** Primary key. */
		public int DictCustomNum;
		/** No space or punctuation allowed. */
		public String WordText;

		/** Deep copy of object. */
		public DictCustom deepCopy() {
			DictCustom dictcustom=new DictCustom();
			dictcustom.DictCustomNum=this.DictCustomNum;
			dictcustom.WordText=this.WordText;
			return dictcustom;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DictCustom>");
			sb.append("<DictCustomNum>").append(DictCustomNum).append("</DictCustomNum>");
			sb.append("<WordText>").append(Serializing.escapeForXml(WordText)).append("</WordText>");
			sb.append("</DictCustom>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DictCustomNum")!=null) {
					DictCustomNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DictCustomNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"WordText")!=null) {
					WordText=Serializing.getXmlNodeValue(doc,"WordText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
