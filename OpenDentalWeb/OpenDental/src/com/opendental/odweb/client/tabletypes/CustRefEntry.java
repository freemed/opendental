package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class CustRefEntry {
		/** Primary key. */
		public int CustRefEntryNum;
		/** FK to patient.PatNum.  The customer seeking a reference. */
		public int PatNumCust;
		/** FK to patient.PatNum.  The chosen reference.  This is the customer who was given as a reference to the new customer. */
		public int PatNumRef;
		/** Date the reference was chosen. */
		public Date DateEntry;
		/** Notes specific to this particular reference entry, mostly for a special reference situation. */
		public String Note;

		/** Deep copy of object. */
		public CustRefEntry deepCopy() {
			CustRefEntry custrefentry=new CustRefEntry();
			custrefentry.CustRefEntryNum=this.CustRefEntryNum;
			custrefentry.PatNumCust=this.PatNumCust;
			custrefentry.PatNumRef=this.PatNumRef;
			custrefentry.DateEntry=this.DateEntry;
			custrefentry.Note=this.Note;
			return custrefentry;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CustRefEntry>");
			sb.append("<CustRefEntryNum>").append(CustRefEntryNum).append("</CustRefEntryNum>");
			sb.append("<PatNumCust>").append(PatNumCust).append("</PatNumCust>");
			sb.append("<PatNumRef>").append(PatNumRef).append("</PatNumRef>");
			sb.append("<DateEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEntry)).append("</DateEntry>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("</CustRefEntry>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CustRefEntryNum")!=null) {
					CustRefEntryNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CustRefEntryNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNumCust")!=null) {
					PatNumCust=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNumCust"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNumRef")!=null) {
					PatNumRef=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNumRef"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateEntry")!=null) {
					DateEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateEntry"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
