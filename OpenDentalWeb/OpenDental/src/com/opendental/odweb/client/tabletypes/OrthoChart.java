package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public OrthoChart deepCopy() {
			OrthoChart orthochart=new OrthoChart();
			orthochart.OrthoChartNum=this.OrthoChartNum;
			orthochart.PatNum=this.PatNum;
			orthochart.DateService=this.DateService;
			orthochart.FieldName=this.FieldName;
			orthochart.FieldValue=this.FieldValue;
			return orthochart;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<OrthoChart>");
			sb.append("<OrthoChartNum>").append(OrthoChartNum).append("</OrthoChartNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateService>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateService)).append("</DateService>");
			sb.append("<FieldName>").append(Serializing.escapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldValue>").append(Serializing.escapeForXml(FieldValue)).append("</FieldValue>");
			sb.append("</OrthoChart>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"OrthoChartNum")!=null) {
					OrthoChartNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OrthoChartNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateService")!=null) {
					DateService=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateService"));
				}
				if(Serializing.getXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.getXmlNodeValue(doc,"FieldName");
				}
				if(Serializing.getXmlNodeValue(doc,"FieldValue")!=null) {
					FieldValue=Serializing.getXmlNodeValue(doc,"FieldValue");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
