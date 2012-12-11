package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class LetterMerge {
		/** Primary key. */
		public int LetterMergeNum;
		/** Description of this letter. */
		public String Description;
		/** The filename of the Word template. eg MyTemplate.doc. */
		public String TemplateName;
		/** The name of the data file. eg MyTemplate.txt. */
		public String DataFileName;
		/** FK to definition.DefNum. */
		public int Category;

		/** Deep copy of object. */
		public LetterMerge Copy() {
			LetterMerge lettermerge=new LetterMerge();
			lettermerge.LetterMergeNum=this.LetterMergeNum;
			lettermerge.Description=this.Description;
			lettermerge.TemplateName=this.TemplateName;
			lettermerge.DataFileName=this.DataFileName;
			lettermerge.Category=this.Category;
			return lettermerge;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LetterMerge>");
			sb.append("<LetterMergeNum>").append(LetterMergeNum).append("</LetterMergeNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<TemplateName>").append(Serializing.EscapeForXml(TemplateName)).append("</TemplateName>");
			sb.append("<DataFileName>").append(Serializing.EscapeForXml(DataFileName)).append("</DataFileName>");
			sb.append("<Category>").append(Category).append("</Category>");
			sb.append("</LetterMerge>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"LetterMergeNum")!=null) {
					LetterMergeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LetterMergeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"TemplateName")!=null) {
					TemplateName=Serializing.GetXmlNodeValue(doc,"TemplateName");
				}
				if(Serializing.GetXmlNodeValue(doc,"DataFileName")!=null) {
					DataFileName=Serializing.GetXmlNodeValue(doc,"DataFileName");
				}
				if(Serializing.GetXmlNodeValue(doc,"Category")!=null) {
					Category=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Category"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}