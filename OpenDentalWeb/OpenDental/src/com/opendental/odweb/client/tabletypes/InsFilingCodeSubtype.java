package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class InsFilingCodeSubtype {
		/** Primary key. */
		public int InsFilingCodeSubtypeNum;
		/** FK to insfilingcode.insfilingcodenum */
		public int InsFilingCodeNum;
		/** The description of the insurance filing code subtype. */
		public String Descript;

		/** Deep copy of object. */
		public InsFilingCodeSubtype Copy() {
			InsFilingCodeSubtype insfilingcodesubtype=new InsFilingCodeSubtype();
			insfilingcodesubtype.InsFilingCodeSubtypeNum=this.InsFilingCodeSubtypeNum;
			insfilingcodesubtype.InsFilingCodeNum=this.InsFilingCodeNum;
			insfilingcodesubtype.Descript=this.Descript;
			return insfilingcodesubtype;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InsFilingCodeSubtype>");
			sb.append("<InsFilingCodeSubtypeNum>").append(InsFilingCodeSubtypeNum).append("</InsFilingCodeSubtypeNum>");
			sb.append("<InsFilingCodeNum>").append(InsFilingCodeNum).append("</InsFilingCodeNum>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("</InsFilingCodeSubtype>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				InsFilingCodeSubtypeNum=Integer.valueOf(doc.getElementsByTagName("InsFilingCodeSubtypeNum").item(0).getFirstChild().getNodeValue());
				InsFilingCodeNum=Integer.valueOf(doc.getElementsByTagName("InsFilingCodeNum").item(0).getFirstChild().getNodeValue());
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
