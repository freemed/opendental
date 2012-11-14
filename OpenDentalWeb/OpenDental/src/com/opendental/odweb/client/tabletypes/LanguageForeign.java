package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class LanguageForeign {
		/** Primary key. */
		public int LanguageForeignNum;
		/** A string representing the class where the translation is used. */
		public String ClassType;
		/** The English version of the phrase.  Case sensitive. */
		public String English;
		/** The specific culture name.  Almost always in 5 digit format like this: en-US. */
		public String Culture;
		/** The foreign translation.  Remember we use Unicode-8, so this translation can be in any language, including Russian, Hebrew, and Chinese. */
		public String Translation;
		/** Comments for other translators for the foreign language. */
		public String Comments;

		/** Deep copy of object. */
		public LanguageForeign Copy() {
			LanguageForeign languageforeign=new LanguageForeign();
			languageforeign.LanguageForeignNum=this.LanguageForeignNum;
			languageforeign.ClassType=this.ClassType;
			languageforeign.English=this.English;
			languageforeign.Culture=this.Culture;
			languageforeign.Translation=this.Translation;
			languageforeign.Comments=this.Comments;
			return languageforeign;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LanguageForeign>");
			sb.append("<LanguageForeignNum>").append(LanguageForeignNum).append("</LanguageForeignNum>");
			sb.append("<ClassType>").append(Serializing.EscapeForXml(ClassType)).append("</ClassType>");
			sb.append("<English>").append(Serializing.EscapeForXml(English)).append("</English>");
			sb.append("<Culture>").append(Serializing.EscapeForXml(Culture)).append("</Culture>");
			sb.append("<Translation>").append(Serializing.EscapeForXml(Translation)).append("</Translation>");
			sb.append("<Comments>").append(Serializing.EscapeForXml(Comments)).append("</Comments>");
			sb.append("</LanguageForeign>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				LanguageForeignNum=Integer.valueOf(doc.getElementsByTagName("LanguageForeignNum").item(0).getFirstChild().getNodeValue());
				ClassType=doc.getElementsByTagName("ClassType").item(0).getFirstChild().getNodeValue();
				English=doc.getElementsByTagName("English").item(0).getFirstChild().getNodeValue();
				Culture=doc.getElementsByTagName("Culture").item(0).getFirstChild().getNodeValue();
				Translation=doc.getElementsByTagName("Translation").item(0).getFirstChild().getNodeValue();
				Comments=doc.getElementsByTagName("Comments").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
