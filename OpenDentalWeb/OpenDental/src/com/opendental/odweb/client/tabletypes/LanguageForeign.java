package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"LanguageForeignNum")!=null) {
					LanguageForeignNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LanguageForeignNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClassType")!=null) {
					ClassType=Serializing.GetXmlNodeValue(doc,"ClassType");
				}
				if(Serializing.GetXmlNodeValue(doc,"English")!=null) {
					English=Serializing.GetXmlNodeValue(doc,"English");
				}
				if(Serializing.GetXmlNodeValue(doc,"Culture")!=null) {
					Culture=Serializing.GetXmlNodeValue(doc,"Culture");
				}
				if(Serializing.GetXmlNodeValue(doc,"Translation")!=null) {
					Translation=Serializing.GetXmlNodeValue(doc,"Translation");
				}
				if(Serializing.GetXmlNodeValue(doc,"Comments")!=null) {
					Comments=Serializing.GetXmlNodeValue(doc,"Comments");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
