package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Letter {
		/** Primary key. */
		public int LetterNum;
		/** Description of the Letter. */
		public String Description;
		/** Text of the letter */
		public String BodyText;

		/** Deep copy of object. */
		public Letter deepCopy() {
			Letter letter=new Letter();
			letter.LetterNum=this.LetterNum;
			letter.Description=this.Description;
			letter.BodyText=this.BodyText;
			return letter;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Letter>");
			sb.append("<LetterNum>").append(LetterNum).append("</LetterNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<BodyText>").append(Serializing.escapeForXml(BodyText)).append("</BodyText>");
			sb.append("</Letter>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"LetterNum")!=null) {
					LetterNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LetterNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"BodyText")!=null) {
					BodyText=Serializing.getXmlNodeValue(doc,"BodyText");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Letter: "+e.getMessage());
			}
		}


}
