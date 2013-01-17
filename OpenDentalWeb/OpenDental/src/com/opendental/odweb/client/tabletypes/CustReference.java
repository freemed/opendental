package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class CustReference {
		/** Primary key. */
		public int CustReferenceNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Most recent date the reference was used, loosely kept updated. */
		public Date DateMostRecent;
		/** Notes specific to this customer as a reference. */
		public String Note;
		/** Set to true if this customer was a bad reference. */
		public boolean IsBadRef;

		/** Deep copy of object. */
		public CustReference deepCopy() {
			CustReference custreference=new CustReference();
			custreference.CustReferenceNum=this.CustReferenceNum;
			custreference.PatNum=this.PatNum;
			custreference.DateMostRecent=this.DateMostRecent;
			custreference.Note=this.Note;
			custreference.IsBadRef=this.IsBadRef;
			return custreference;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CustReference>");
			sb.append("<CustReferenceNum>").append(CustReferenceNum).append("</CustReferenceNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateMostRecent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateMostRecent)).append("</DateMostRecent>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<IsBadRef>").append((IsBadRef)?1:0).append("</IsBadRef>");
			sb.append("</CustReference>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CustReferenceNum")!=null) {
					CustReferenceNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CustReferenceNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateMostRecent")!=null) {
					DateMostRecent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateMostRecent"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"IsBadRef")!=null) {
					IsBadRef=(Serializing.getXmlNodeValue(doc,"IsBadRef")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing CustReference: "+e.getMessage());
			}
		}


}
