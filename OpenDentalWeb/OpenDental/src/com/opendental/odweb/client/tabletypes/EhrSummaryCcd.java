package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class EhrSummaryCcd {
		/** Primary key. */
		public int EhrSummaryCcdNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Date that this Ccd was received. */
		public Date DateSummary;
		/** The xml content of the received text file. */
		public String ContentSummary;

		/** Deep copy of object. */
		public EhrSummaryCcd Copy() {
			EhrSummaryCcd ehrsummaryccd=new EhrSummaryCcd();
			ehrsummaryccd.EhrSummaryCcdNum=this.EhrSummaryCcdNum;
			ehrsummaryccd.PatNum=this.PatNum;
			ehrsummaryccd.DateSummary=this.DateSummary;
			ehrsummaryccd.ContentSummary=this.ContentSummary;
			return ehrsummaryccd;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrSummaryCcd>");
			sb.append("<EhrSummaryCcdNum>").append(EhrSummaryCcdNum).append("</EhrSummaryCcdNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateSummary>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateSummary)).append("</DateSummary>");
			sb.append("<ContentSummary>").append(Serializing.EscapeForXml(ContentSummary)).append("</ContentSummary>");
			sb.append("</EhrSummaryCcd>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"EhrSummaryCcdNum")!=null) {
					EhrSummaryCcdNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EhrSummaryCcdNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateSummary")!=null) {
					DateSummary=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateSummary"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ContentSummary")!=null) {
					ContentSummary=Serializing.GetXmlNodeValue(doc,"ContentSummary");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
