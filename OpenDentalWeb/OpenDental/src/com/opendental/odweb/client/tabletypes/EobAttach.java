package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public EobAttach deepCopy() {
			EobAttach eobattach=new EobAttach();
			eobattach.EobAttachNum=this.EobAttachNum;
			eobattach.ClaimPaymentNum=this.ClaimPaymentNum;
			eobattach.DateTCreated=this.DateTCreated;
			eobattach.FileName=this.FileName;
			eobattach.RawBase64=this.RawBase64;
			return eobattach;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EobAttach>");
			sb.append("<EobAttachNum>").append(EobAttachNum).append("</EobAttachNum>");
			sb.append("<ClaimPaymentNum>").append(ClaimPaymentNum).append("</ClaimPaymentNum>");
			sb.append("<DateTCreated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTCreated)).append("</DateTCreated>");
			sb.append("<FileName>").append(Serializing.escapeForXml(FileName)).append("</FileName>");
			sb.append("<RawBase64>").append(Serializing.escapeForXml(RawBase64)).append("</RawBase64>");
			sb.append("</EobAttach>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EobAttachNum")!=null) {
					EobAttachNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EobAttachNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimPaymentNum")!=null) {
					ClaimPaymentNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimPaymentNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTCreated")!=null) {
					DateTCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTCreated"));
				}
				if(Serializing.getXmlNodeValue(doc,"FileName")!=null) {
					FileName=Serializing.getXmlNodeValue(doc,"FileName");
				}
				if(Serializing.getXmlNodeValue(doc,"RawBase64")!=null) {
					RawBase64=Serializing.getXmlNodeValue(doc,"RawBase64");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
