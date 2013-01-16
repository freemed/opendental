package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public EhrQuarterlyKey deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrQuarterlyKey>");
			sb.append("<EhrQuarterlyKeyNum>").append(EhrQuarterlyKeyNum).append("</EhrQuarterlyKeyNum>");
			sb.append("<YearValue>").append(YearValue).append("</YearValue>");
			sb.append("<QuarterValue>").append(QuarterValue).append("</QuarterValue>");
			sb.append("<PracticeName>").append(Serializing.escapeForXml(PracticeName)).append("</PracticeName>");
			sb.append("<KeyValue>").append(Serializing.escapeForXml(KeyValue)).append("</KeyValue>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Notes>").append(Serializing.escapeForXml(Notes)).append("</Notes>");
			sb.append("</EhrQuarterlyKey>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EhrQuarterlyKeyNum")!=null) {
					EhrQuarterlyKeyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EhrQuarterlyKeyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"YearValue")!=null) {
					YearValue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"YearValue"));
				}
				if(Serializing.getXmlNodeValue(doc,"QuarterValue")!=null) {
					QuarterValue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"QuarterValue"));
				}
				if(Serializing.getXmlNodeValue(doc,"PracticeName")!=null) {
					PracticeName=Serializing.getXmlNodeValue(doc,"PracticeName");
				}
				if(Serializing.getXmlNodeValue(doc,"KeyValue")!=null) {
					KeyValue=Serializing.getXmlNodeValue(doc,"KeyValue");
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.getXmlNodeValue(doc,"Notes");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
