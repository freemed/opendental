package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
		public CustRefEntry Copy() {
			CustRefEntry custrefentry=new CustRefEntry();
			custrefentry.CustRefEntryNum=this.CustRefEntryNum;
			custrefentry.PatNumCust=this.PatNumCust;
			custrefentry.PatNumRef=this.PatNumRef;
			custrefentry.DateEntry=this.DateEntry;
			custrefentry.Note=this.Note;
			return custrefentry;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CustRefEntry>");
			sb.append("<CustRefEntryNum>").append(CustRefEntryNum).append("</CustRefEntryNum>");
			sb.append("<PatNumCust>").append(PatNumCust).append("</PatNumCust>");
			sb.append("<PatNumRef>").append(PatNumRef).append("</PatNumRef>");
			sb.append("<DateEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEntry)).append("</DateEntry>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("</CustRefEntry>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"CustRefEntryNum")!=null) {
					CustRefEntryNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CustRefEntryNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNumCust")!=null) {
					PatNumCust=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNumCust"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNumRef")!=null) {
					PatNumRef=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNumRef"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateEntry")!=null) {
					DateEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateEntry"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
