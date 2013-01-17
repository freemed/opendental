package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import java.util.ArrayList;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		/** Not a database column.  Filled using fk from the lettermergefields table.  A collection of strings representing field names. */
		public ArrayList<String> Fields;

		/** Deep copy of object. */
		public LetterMerge deepCopy() {
			LetterMerge lettermerge=new LetterMerge();
			lettermerge.LetterMergeNum=this.LetterMergeNum;
			lettermerge.Description=this.Description;
			lettermerge.TemplateName=this.TemplateName;
			lettermerge.DataFileName=this.DataFileName;
			lettermerge.Category=this.Category;
			lettermerge.Fields=this.Fields;
			return lettermerge;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LetterMerge>");
			sb.append("<LetterMergeNum>").append(LetterMergeNum).append("</LetterMergeNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<TemplateName>").append(Serializing.escapeForXml(TemplateName)).append("</TemplateName>");
			sb.append("<DataFileName>").append(Serializing.escapeForXml(DataFileName)).append("</DataFileName>");
			sb.append("<Category>").append(Category).append("</Category>");
			sb.append("</LetterMerge>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"LetterMergeNum")!=null) {
					LetterMergeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LetterMergeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"TemplateName")!=null) {
					TemplateName=Serializing.getXmlNodeValue(doc,"TemplateName");
				}
				if(Serializing.getXmlNodeValue(doc,"DataFileName")!=null) {
					DataFileName=Serializing.getXmlNodeValue(doc,"DataFileName");
				}
				if(Serializing.getXmlNodeValue(doc,"Category")!=null) {
					Category=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Category"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing LetterMerge: "+e.getMessage());
			}
		}


}
