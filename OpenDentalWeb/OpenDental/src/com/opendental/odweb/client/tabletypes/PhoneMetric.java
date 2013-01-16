package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public PhoneMetric deepCopy() {
			PhoneMetric phonemetric=new PhoneMetric();
			phonemetric.PhoneMetricNum=this.PhoneMetricNum;
			phonemetric.DateTimeEntry=this.DateTimeEntry;
			phonemetric.VoiceMails=this.VoiceMails;
			phonemetric.Triages=this.Triages;
			phonemetric.MinutesBehind=this.MinutesBehind;
			return phonemetric;
		}

		/** Serialize the object into XML. */
		public String serialize() {
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
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PhoneMetricNum")!=null) {
					PhoneMetricNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PhoneMetricNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeEntry")!=null) {
					DateTimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeEntry"));
				}
				if(Serializing.getXmlNodeValue(doc,"VoiceMails")!=null) {
					VoiceMails=Integer.valueOf(Serializing.getXmlNodeValue(doc,"VoiceMails"));
				}
				if(Serializing.getXmlNodeValue(doc,"Triages")!=null) {
					Triages=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Triages"));
				}
				if(Serializing.getXmlNodeValue(doc,"MinutesBehind")!=null) {
					MinutesBehind=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MinutesBehind"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
