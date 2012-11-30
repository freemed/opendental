package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class ErxLog {
		/** Primary key. */
		public int ErxLogNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Holds up to 16MB. */
		public String MsgText;
		/** Automatically updated by MySQL every time a row is added or changed. */
		public Date DateTStamp;

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
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("</ErxLog>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ErxLogNum")!=null) {
					ErxLogNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ErxLogNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MsgText")!=null) {
					MsgText=Serializing.GetXmlNodeValue(doc,"MsgText");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
