package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ScreenPat {
		/** Primary key. */
		public int ScreenPatNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** FK to screengroup.ScreenGroupNum. Every screening is attached to a group (classroom) */
		public int ScreenGroupNum;
		/** FK to sheet.SheetNum. Starts out 0 to indicate a potential screening. Gets linked to an exam sheet once the screening is done. */
		public int SheetNum;

		/** Deep copy of object. */
		public ScreenPat Copy() {
			ScreenPat screenpat=new ScreenPat();
			screenpat.ScreenPatNum=this.ScreenPatNum;
			screenpat.PatNum=this.PatNum;
			screenpat.ScreenGroupNum=this.ScreenGroupNum;
			screenpat.SheetNum=this.SheetNum;
			return screenpat;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ScreenPat>");
			sb.append("<ScreenPatNum>").append(ScreenPatNum).append("</ScreenPatNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ScreenGroupNum>").append(ScreenGroupNum).append("</ScreenGroupNum>");
			sb.append("<SheetNum>").append(SheetNum).append("</SheetNum>");
			sb.append("</ScreenPat>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ScreenPatNum=Integer.valueOf(doc.getElementsByTagName("ScreenPatNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ScreenGroupNum=Integer.valueOf(doc.getElementsByTagName("ScreenGroupNum").item(0).getFirstChild().getNodeValue());
				SheetNum=Integer.valueOf(doc.getElementsByTagName("SheetNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
