package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class WikiPageHist {
		/** Primary key. */
		public int WikiPageNum;
		/** FK to userod.UserNum. */
		public int UserNum;
		/** Will not be unique because there are multiple revisions per page. */
		public String PageTitle;
		/** The entire contents of the revision are stored in "wiki markup language".  This should never be updated.  Medtext (16M) */
		public String PageContent;
		/** The DateTime from the original WikiPage object. */
		public Date DateTimeSaved;
		/** This flag will only be set for the revision where the user marked it deleted, not the ones prior. */
		public boolean IsDeleted;

		/** Deep copy of object. */
		public WikiPageHist deepCopy() {
			WikiPageHist wikipagehist=new WikiPageHist();
			wikipagehist.WikiPageNum=this.WikiPageNum;
			wikipagehist.UserNum=this.UserNum;
			wikipagehist.PageTitle=this.PageTitle;
			wikipagehist.PageContent=this.PageContent;
			wikipagehist.DateTimeSaved=this.DateTimeSaved;
			wikipagehist.IsDeleted=this.IsDeleted;
			return wikipagehist;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<WikiPageHist>");
			sb.append("<WikiPageNum>").append(WikiPageNum).append("</WikiPageNum>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<PageTitle>").append(Serializing.escapeForXml(PageTitle)).append("</PageTitle>");
			sb.append("<PageContent>").append(Serializing.escapeForXml(PageContent)).append("</PageContent>");
			sb.append("<DateTimeSaved>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeSaved)).append("</DateTimeSaved>");
			sb.append("<IsDeleted>").append((IsDeleted)?1:0).append("</IsDeleted>");
			sb.append("</WikiPageHist>");
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
				if(Serializing.getXmlNodeValue(doc,"PageContent")!=null) {
					PageContent=Serializing.getXmlNodeValue(doc,"PageContent");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeSaved")!=null) {
					DateTimeSaved=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeSaved"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsDeleted")!=null) {
					IsDeleted=(Serializing.getXmlNodeValue(doc,"IsDeleted")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
