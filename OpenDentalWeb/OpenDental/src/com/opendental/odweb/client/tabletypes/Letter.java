package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class Letter {
		/** Primary key. */
		public int LetterNum;
		/** Description of the Letter. */
		public String Description;
		/** Text of the letter */
		public String BodyText;

		/** Deep copy of object. */
		public Letter Copy() {
			Letter letter=new Letter();
			letter.LetterNum=this.LetterNum;
			letter.Description=this.Description;
			letter.BodyText=this.BodyText;
			return letter;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Letter>");
			sb.append("<LetterNum>").append(LetterNum).append("</LetterNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<BodyText>").append(Serializing.EscapeForXml(BodyText)).append("</BodyText>");
			sb.append("</Letter>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"LetterNum")!=null) {
					LetterNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LetterNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"BodyText")!=null) {
					BodyText=Serializing.GetXmlNodeValue(doc,"BodyText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
