package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				RxNormNum=Integer.valueOf(doc.getElementsByTagName("RxNormNum").item(0).getFirstChild().getNodeValue());
				RxCui=doc.getElementsByTagName("RxCui").item(0).getFirstChild().getNodeValue();
				MmslCode=doc.getElementsByTagName("MmslCode").item(0).getFirstChild().getNodeValue();
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
