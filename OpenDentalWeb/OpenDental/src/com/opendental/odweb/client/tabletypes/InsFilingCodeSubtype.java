package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"InsFilingCodeSubtypeNum")!=null) {
					InsFilingCodeSubtypeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"InsFilingCodeSubtypeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"InsFilingCodeNum")!=null) {
					InsFilingCodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"InsFilingCodeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.GetXmlNodeValue(doc,"Descript");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
