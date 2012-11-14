package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EhrQuarterlyKeyNum=Integer.valueOf(doc.getElementsByTagName("EhrQuarterlyKeyNum").item(0).getFirstChild().getNodeValue());
				YearValue=Integer.valueOf(doc.getElementsByTagName("YearValue").item(0).getFirstChild().getNodeValue());
				QuarterValue=Integer.valueOf(doc.getElementsByTagName("QuarterValue").item(0).getFirstChild().getNodeValue());
				PracticeName=doc.getElementsByTagName("PracticeName").item(0).getFirstChild().getNodeValue();
				KeyValue=doc.getElementsByTagName("KeyValue").item(0).getFirstChild().getNodeValue();
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				Notes=doc.getElementsByTagName("Notes").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
