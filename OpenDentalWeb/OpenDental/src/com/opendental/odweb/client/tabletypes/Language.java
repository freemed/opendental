package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class Language {
		/** Primary key. */
		public int LanguageNum;
		/** No longer used. */
		public String EnglishComments;
		/** A string representing the class where the translation is used. Maximum length is 25 characters. */
		public String ClassType;
		/** The English version of the phrase, case sensitive. */
		public String English;
		/** As this gets more sophisticated, we will use this field to mark some phrases obsolete instead of just deleting them outright.  That way, translators will still have access to them.  For now, this is not used at all. */
		public boolean IsObsolete;

		/** Deep copy of object. */
		public Language deepCopy() {
			Language language=new Language();
			language.LanguageNum=this.LanguageNum;
			language.EnglishComments=this.EnglishComments;
			language.ClassType=this.ClassType;
			language.English=this.English;
			language.IsObsolete=this.IsObsolete;
			return language;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Language>");
			sb.append("<LanguageNum>").append(LanguageNum).append("</LanguageNum>");
			sb.append("<EnglishComments>").append(Serializing.escapeForXml(EnglishComments)).append("</EnglishComments>");
			sb.append("<ClassType>").append(Serializing.escapeForXml(ClassType)).append("</ClassType>");
			sb.append("<English>").append(Serializing.escapeForXml(English)).append("</English>");
			sb.append("<IsObsolete>").append((IsObsolete)?1:0).append("</IsObsolete>");
			sb.append("</Language>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"LanguageNum")!=null) {
					LanguageNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LanguageNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"EnglishComments")!=null) {
					EnglishComments=Serializing.getXmlNodeValue(doc,"EnglishComments");
				}
				if(Serializing.getXmlNodeValue(doc,"ClassType")!=null) {
					ClassType=Serializing.getXmlNodeValue(doc,"ClassType");
				}
				if(Serializing.getXmlNodeValue(doc,"English")!=null) {
					English=Serializing.getXmlNodeValue(doc,"English");
				}
				if(Serializing.getXmlNodeValue(doc,"IsObsolete")!=null) {
					IsObsolete=(Serializing.getXmlNodeValue(doc,"IsObsolete")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
