package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class RxNorm {
		/** Primary key. */
		public int RxNormNum;
		/** RxNorm Concept universal ID.  Throughout the program, this is actually used as the Primary Key of this table rather than the RxNormNum. */
		public String RxCui;
		/** Multum code.  Only used for import/export with electronic Rx program.  User cannot see multum codes. */
		public String MmslCode;
		/** Only used for RxNorms, not Multums. */
		public String Description;

		/** Deep copy of object. */
		public RxNorm Copy() {
			RxNorm rxnorm=new RxNorm();
			rxnorm.RxNormNum=this.RxNormNum;
			rxnorm.RxCui=this.RxCui;
			rxnorm.MmslCode=this.MmslCode;
			rxnorm.Description=this.Description;
			return rxnorm;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RxNorm>");
			sb.append("<RxNormNum>").append(RxNormNum).append("</RxNormNum>");
			sb.append("<RxCui>").append(Serializing.EscapeForXml(RxCui)).append("</RxCui>");
			sb.append("<MmslCode>").append(Serializing.EscapeForXml(MmslCode)).append("</MmslCode>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("</RxNorm>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"RxNormNum")!=null) {
					RxNormNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RxNormNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RxCui")!=null) {
					RxCui=Serializing.GetXmlNodeValue(doc,"RxCui");
				}
				if(Serializing.GetXmlNodeValue(doc,"MmslCode")!=null) {
					MmslCode=Serializing.GetXmlNodeValue(doc,"MmslCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
