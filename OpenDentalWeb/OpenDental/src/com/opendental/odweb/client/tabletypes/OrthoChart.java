package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class OrthoChart {
		/** Primary key. */
		public int OrthoChartNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Date of service. */
		public Date DateService;
		/** . */
		public String FieldName;
		/** . */
		public String FieldValue;

		/** Deep copy of object. */
		public OrthoChart Copy() {
			OrthoChart orthochart=new OrthoChart();
			orthochart.OrthoChartNum=this.OrthoChartNum;
			orthochart.PatNum=this.PatNum;
			orthochart.DateService=this.DateService;
			orthochart.FieldName=this.FieldName;
			orthochart.FieldValue=this.FieldValue;
			return orthochart;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<OrthoChart>");
			sb.append("<OrthoChartNum>").append(OrthoChartNum).append("</OrthoChartNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateService>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateService>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldValue>").append(Serializing.EscapeForXml(FieldValue)).append("</FieldValue>");
			sb.append("</OrthoChart>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				OrthoChartNum=Integer.valueOf(doc.getElementsByTagName("OrthoChartNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DateService=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateService").item(0).getFirstChild().getNodeValue());
				FieldName=doc.getElementsByTagName("FieldName").item(0).getFirstChild().getNodeValue();
				FieldValue=doc.getElementsByTagName("FieldValue").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
