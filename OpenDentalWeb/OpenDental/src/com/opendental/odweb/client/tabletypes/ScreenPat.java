package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public ScreenPat deepCopy() {
			ScreenPat screenpat=new ScreenPat();
			screenpat.ScreenPatNum=this.ScreenPatNum;
			screenpat.PatNum=this.PatNum;
			screenpat.ScreenGroupNum=this.ScreenGroupNum;
			screenpat.SheetNum=this.SheetNum;
			return screenpat;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ScreenPat>");
			sb.append("<ScreenPatNum>").append(ScreenPatNum).append("</ScreenPatNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ScreenGroupNum>").append(ScreenGroupNum).append("</ScreenGroupNum>");
			sb.append("<SheetNum>").append(SheetNum).append("</SheetNum>");
			sb.append("</ScreenPat>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ScreenPatNum")!=null) {
					ScreenPatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ScreenPatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ScreenGroupNum")!=null) {
					ScreenGroupNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ScreenGroupNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SheetNum")!=null) {
					SheetNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SheetNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
