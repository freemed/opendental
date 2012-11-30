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
			sb.append("<DateService>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateService)).append("</DateService>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldValue>").append(Serializing.EscapeForXml(FieldValue)).append("</FieldValue>");
			sb.append("</OrthoChart>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"OrthoChartNum")!=null) {
					OrthoChartNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"OrthoChartNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateService")!=null) {
					DateService=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateService"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.GetXmlNodeValue(doc,"FieldName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FieldValue")!=null) {
					FieldValue=Serializing.GetXmlNodeValue(doc,"FieldValue");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
