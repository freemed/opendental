package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class EobAttach {
		/** Primary key. */
		public int EobAttachNum;
		/** FK to claimpayment.ClaimPaymentNum */
		public int ClaimPaymentNum;
		/** Date/time created. */
		public Date DateTCreated;
		/** The file is stored in the A-Z folder in 'EOBs' folder.  This field stores the name of the file.  The files are named automatically based on Date/time along with EobAttachNum for uniqueness. */
		public String FileName;
		/** The raw file data encoded as base64.  Only used if there is no AtoZ folder. */
		public String RawBase64;

		/** Deep copy of object. */
		public EobAttach Copy() {
			EobAttach eobattach=new EobAttach();
			eobattach.EobAttachNum=this.EobAttachNum;
			eobattach.ClaimPaymentNum=this.ClaimPaymentNum;
			eobattach.DateTCreated=this.DateTCreated;
			eobattach.FileName=this.FileName;
			eobattach.RawBase64=this.RawBase64;
			return eobattach;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EobAttach>");
			sb.append("<EobAttachNum>").append(EobAttachNum).append("</EobAttachNum>");
			sb.append("<ClaimPaymentNum>").append(ClaimPaymentNum).append("</ClaimPaymentNum>");
			sb.append("<DateTCreated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTCreated>");
			sb.append("<FileName>").append(Serializing.EscapeForXml(FileName)).append("</FileName>");
			sb.append("<RawBase64>").append(Serializing.EscapeForXml(RawBase64)).append("</RawBase64>");
			sb.append("</EobAttach>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EobAttachNum=Integer.valueOf(doc.getElementsByTagName("EobAttachNum").item(0).getFirstChild().getNodeValue());
				ClaimPaymentNum=Integer.valueOf(doc.getElementsByTagName("ClaimPaymentNum").item(0).getFirstChild().getNodeValue());
				DateTCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTCreated").item(0).getFirstChild().getNodeValue());
				FileName=doc.getElementsByTagName("FileName").item(0).getFirstChild().getNodeValue();
				RawBase64=doc.getElementsByTagName("RawBase64").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
