package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class PhoneMetric {
		/** Primary key. */
		public int PhoneMetricNum;
		/**  */
		public Date DateTimeEntry;
		/** Smallint -32768 to 32767. -1 means was unable to reach the server. */
		public int VoiceMails;
		/** Smallint -32768 to 32767 */
		public int Triages;
		/** Smallint -32768 to 32767 */
		public int MinutesBehind;

		/** Deep copy of object. */
		public PhoneMetric Copy() {
			PhoneMetric phonemetric=new PhoneMetric();
			phonemetric.PhoneMetricNum=this.PhoneMetricNum;
			phonemetric.DateTimeEntry=this.DateTimeEntry;
			phonemetric.VoiceMails=this.VoiceMails;
			phonemetric.Triages=this.Triages;
			phonemetric.MinutesBehind=this.MinutesBehind;
			return phonemetric;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PhoneMetric>");
			sb.append("<PhoneMetricNum>").append(PhoneMetricNum).append("</PhoneMetricNum>");
			sb.append("<DateTimeEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTimeEntry>");
			sb.append("<VoiceMails>").append(VoiceMails).append("</VoiceMails>");
			sb.append("<Triages>").append(Triages).append("</Triages>");
			sb.append("<MinutesBehind>").append(MinutesBehind).append("</MinutesBehind>");
			sb.append("</PhoneMetric>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PhoneMetricNum=Integer.valueOf(doc.getElementsByTagName("PhoneMetricNum").item(0).getFirstChild().getNodeValue());
				DateTimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeEntry").item(0).getFirstChild().getNodeValue());
				VoiceMails=Integer.valueOf(doc.getElementsByTagName("VoiceMails").item(0).getFirstChild().getNodeValue());
				Triages=Integer.valueOf(doc.getElementsByTagName("Triages").item(0).getFirstChild().getNodeValue());
				MinutesBehind=Integer.valueOf(doc.getElementsByTagName("MinutesBehind").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
