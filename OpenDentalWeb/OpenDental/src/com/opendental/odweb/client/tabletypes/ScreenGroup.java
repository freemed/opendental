package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class ScreenGroup {
		/** Primary key */
		public int ScreenGroupNum;
		/** Up to the user. */
		public String Description;
		/** Date used to help order the groups. */
		public Date SGDate;

		/** Deep copy of object. */
		public ScreenGroup Copy() {
			ScreenGroup screengroup=new ScreenGroup();
			screengroup.ScreenGroupNum=this.ScreenGroupNum;
			screengroup.Description=this.Description;
			screengroup.SGDate=this.SGDate;
			return screengroup;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ScreenGroup>");
			sb.append("<ScreenGroupNum>").append(ScreenGroupNum).append("</ScreenGroupNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<SGDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(SGDate)).append("</SGDate>");
			sb.append("</ScreenGroup>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ScreenGroupNum")!=null) {
					ScreenGroupNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ScreenGroupNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"SGDate")!=null) {
					SGDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"SGDate"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
