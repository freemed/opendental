package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class EhrQuarterlyKey {
		/** Primary key. */
		public int EhrQuarterlyKeyNum;
		/** Example 11 */
		public int YearValue;
		/** Example 2 */
		public int QuarterValue;
		/** The customer must have this exact practice name entered in practice setup. */
		public String PracticeName;
		/** The calculated key value, tied to year, quarter, and practice name. */
		public String KeyValue;
		/** Always zero for customer databases.  When used by OD customer support, this is the customer num. */
		public int PatNum;
		/** Any notes that the tech wishes to include regarding this situation. */
		public String Notes;

		/** Deep copy of object. */
		public EhrQuarterlyKey Copy() {
			EhrQuarterlyKey ehrquarterlykey=new EhrQuarterlyKey();
			ehrquarterlykey.EhrQuarterlyKeyNum=this.EhrQuarterlyKeyNum;
			ehrquarterlykey.YearValue=this.YearValue;
			ehrquarterlykey.QuarterValue=this.QuarterValue;
			ehrquarterlykey.PracticeName=this.PracticeName;
			ehrquarterlykey.KeyValue=this.KeyValue;
			ehrquarterlykey.PatNum=this.PatNum;
			ehrquarterlykey.Notes=this.Notes;
			return ehrquarterlykey;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrQuarterlyKey>");
			sb.append("<EhrQuarterlyKeyNum>").append(EhrQuarterlyKeyNum).append("</EhrQuarterlyKeyNum>");
			sb.append("<YearValue>").append(YearValue).append("</YearValue>");
			sb.append("<QuarterValue>").append(QuarterValue).append("</QuarterValue>");
			sb.append("<PracticeName>").append(Serializing.EscapeForXml(PracticeName)).append("</PracticeName>");
			sb.append("<KeyValue>").append(Serializing.EscapeForXml(KeyValue)).append("</KeyValue>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Notes>").append(Serializing.EscapeForXml(Notes)).append("</Notes>");
			sb.append("</EhrQuarterlyKey>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"EhrQuarterlyKeyNum")!=null) {
					EhrQuarterlyKeyNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EhrQuarterlyKeyNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"YearValue")!=null) {
					YearValue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"YearValue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"QuarterValue")!=null) {
					QuarterValue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"QuarterValue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PracticeName")!=null) {
					PracticeName=Serializing.GetXmlNodeValue(doc,"PracticeName");
				}
				if(Serializing.GetXmlNodeValue(doc,"KeyValue")!=null) {
					KeyValue=Serializing.GetXmlNodeValue(doc,"KeyValue");
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.GetXmlNodeValue(doc,"Notes");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
