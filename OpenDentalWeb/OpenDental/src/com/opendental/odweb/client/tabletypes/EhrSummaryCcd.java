package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
			sb.append("<DateSummary>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateSummary>");
			sb.append("<ContentSummary>").append(Serializing.EscapeForXml(ContentSummary)).append("</ContentSummary>");
			sb.append("</EhrSummaryCcd>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EhrSummaryCcdNum=Integer.valueOf(doc.getElementsByTagName("EhrSummaryCcdNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DateSummary=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateSummary").item(0).getFirstChild().getNodeValue());
				ContentSummary=doc.getElementsByTagName("ContentSummary").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
