package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
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
