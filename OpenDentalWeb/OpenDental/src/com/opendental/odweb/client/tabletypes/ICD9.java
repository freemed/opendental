package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ICD9 {
		/** Primary key. */
		public int ICD9Num;
		/** Not allowed to edit this column once saved in the database. */
		public String ICD9Code;
		/** Description. */
		public String Description;
		/** The last date and time this row was altered.  Not user editable. */
		public Date DateTStamp;

		/** Deep copy of object. */
		public ICD9 deepCopy() {
			ICD9 icd9=new ICD9();
			icd9.ICD9Num=this.ICD9Num;
			icd9.ICD9Code=this.ICD9Code;
			icd9.Description=this.Description;
			icd9.DateTStamp=this.DateTStamp;
			return icd9;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ICD9>");
			sb.append("<ICD9Num>").append(ICD9Num).append("</ICD9Num>");
			sb.append("<ICD9Code>").append(Serializing.escapeForXml(ICD9Code)).append("</ICD9Code>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("</ICD9>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ICD9Num")!=null) {
					ICD9Num=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ICD9Num"));
				}
				if(Serializing.getXmlNodeValue(doc,"ICD9Code")!=null) {
					ICD9Code=Serializing.getXmlNodeValue(doc,"ICD9Code");
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ICD9: "+e.getMessage());
			}
		}


}
