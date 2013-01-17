package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class WikiPage {
		/** Primary key. */
		public int WikiPageNum;
		/** FK to userod.UserNum. */
		public int UserNum;
		/** Must be unique.  Any character is allowed except: \r, \n, and ".  Needs to be tested, especially with apostrophes. */
		public String PageTitle;
		/** Automatically filled from the [[Keywords:]] tab in the PageContent field as page is being saved. */
		public String KeyWords;
		/** Content of page stored in "wiki markup language".  This should never be updated.  Medtext (16M) */
		public String PageContent;
		/** The DateTime that the page was saved to the DB.  User can't edit. */
		public Date DateTimeSaved;

		/** Deep copy of object. */
		public WikiPage deepCopy() {
			WikiPage wikipage=new WikiPage();
			wikipage.WikiPageNum=this.WikiPageNum;
			wikipage.UserNum=this.UserNum;
			wikipage.PageTitle=this.PageTitle;
			wikipage.KeyWords=this.KeyWords;
			wikipage.PageContent=this.PageContent;
			wikipage.DateTimeSaved=this.DateTimeSaved;
			return wikipage;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<WikiPage>");
			sb.append("<WikiPageNum>").append(WikiPageNum).append("</WikiPageNum>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<PageTitle>").append(Serializing.escapeForXml(PageTitle)).append("</PageTitle>");
			sb.append("<KeyWords>").append(Serializing.escapeForXml(KeyWords)).append("</KeyWords>");
			sb.append("<PageContent>").append(Serializing.escapeForXml(PageContent)).append("</PageContent>");
			sb.append("<DateTimeSaved>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeSaved)).append("</DateTimeSaved>");
			sb.append("</WikiPage>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"WikiPageNum")!=null) {
					WikiPageNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"WikiPageNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PageTitle")!=null) {
					PageTitle=Serializing.getXmlNodeValue(doc,"PageTitle");
				}
				if(Serializing.getXmlNodeValue(doc,"KeyWords")!=null) {
					KeyWords=Serializing.getXmlNodeValue(doc,"KeyWords");
				}
				if(Serializing.getXmlNodeValue(doc,"PageContent")!=null) {
					PageContent=Serializing.getXmlNodeValue(doc,"PageContent");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeSaved")!=null) {
					DateTimeSaved=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeSaved"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing WikiPage: "+e.getMessage());
			}
		}


}
