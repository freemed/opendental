package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
			sb.append("<DateTimeEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeEntry)).append("</DateTimeEntry>");
			sb.append("<VoiceMails>").append(VoiceMails).append("</VoiceMails>");
			sb.append("<Triages>").append(Triages).append("</Triages>");
			sb.append("<MinutesBehind>").append(MinutesBehind).append("</MinutesBehind>");
			sb.append("</PhoneMetric>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"PhoneMetricNum")!=null) {
					PhoneMetricNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PhoneMetricNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeEntry")!=null) {
					DateTimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeEntry"));
				}
				if(Serializing.GetXmlNodeValue(doc,"VoiceMails")!=null) {
					VoiceMails=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"VoiceMails"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Triages")!=null) {
					Triages=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Triages"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MinutesBehind")!=null) {
					MinutesBehind=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MinutesBehind"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
