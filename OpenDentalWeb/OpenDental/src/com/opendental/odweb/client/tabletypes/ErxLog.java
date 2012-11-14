package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ErxLog {
		/** Primary key. */
		public int ErxLogNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Holds up to 16MB. */
		public String MsgText;
		/** Automatically updated by MySQL every time a row is added or changed. */
		public String DateTStamp;

		/** Deep copy of object. */
		public ErxLog Copy() {
			ErxLog erxlog=new ErxLog();
			erxlog.ErxLogNum=this.ErxLogNum;
			erxlog.PatNum=this.PatNum;
			erxlog.MsgText=this.MsgText;
			erxlog.DateTStamp=this.DateTStamp;
			return erxlog;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ErxLog>");
			sb.append("<ErxLogNum>").append(ErxLogNum).append("</ErxLogNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<MsgText>").append(Serializing.EscapeForXml(MsgText)).append("</MsgText>");
			sb.append("<DateTStamp>").append(Serializing.EscapeForXml(DateTStamp)).append("</DateTStamp>");
			sb.append("</ErxLog>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ErxLogNum=Integer.valueOf(doc.getElementsByTagName("ErxLogNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				MsgText=doc.getElementsByTagName("MsgText").item(0).getFirstChild().getNodeValue();
				DateTStamp=doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
