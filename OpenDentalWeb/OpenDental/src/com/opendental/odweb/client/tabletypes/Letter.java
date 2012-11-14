package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				LetterNum=Integer.valueOf(doc.getElementsByTagName("LetterNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				BodyText=doc.getElementsByTagName("BodyText").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
