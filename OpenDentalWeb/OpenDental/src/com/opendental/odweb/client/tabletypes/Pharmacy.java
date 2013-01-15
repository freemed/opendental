package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class Pharmacy {
		/** Primary key. */
		public int PharmacyNum;
		/** NCPDPID assigned by NCPDP.  Not used yet. */
		public String PharmID;
		/** For now, it can just be a common description.  Later, it might have to be an official designation. */
		public String StoreName;
		/** Includes all punctuation. */
		public String Phone;
		/** Includes all punctuation. */
		public String Fax;
		/** . */
		public String Address;
		/** Optional. */
		public String Address2;
		/** . */
		public String City;
		/** Two char, uppercase. */
		public String State;
		/** . */
		public String Zip;
		/** A freeform note for any info that is needed about the pharmacy, such as hours. */
		public String Note;
		/** The last date and time this row was altered.  Not user editable. */
		public Date DateTStamp;

		/** Deep copy of object. */
		public Pharmacy deepCopy() {
			Pharmacy pharmacy=new Pharmacy();
			pharmacy.PharmacyNum=this.PharmacyNum;
			pharmacy.PharmID=this.PharmID;
			pharmacy.StoreName=this.StoreName;
			pharmacy.Phone=this.Phone;
			pharmacy.Fax=this.Fax;
			pharmacy.Address=this.Address;
			pharmacy.Address2=this.Address2;
			pharmacy.City=this.City;
			pharmacy.State=this.State;
			pharmacy.Zip=this.Zip;
			pharmacy.Note=this.Note;
			pharmacy.DateTStamp=this.DateTStamp;
			return pharmacy;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Pharmacy>");
			sb.append("<PharmacyNum>").append(PharmacyNum).append("</PharmacyNum>");
			sb.append("<PharmID>").append(Serializing.escapeForXml(PharmID)).append("</PharmID>");
			sb.append("<StoreName>").append(Serializing.escapeForXml(StoreName)).append("</StoreName>");
			sb.append("<Phone>").append(Serializing.escapeForXml(Phone)).append("</Phone>");
			sb.append("<Fax>").append(Serializing.escapeForXml(Fax)).append("</Fax>");
			sb.append("<Address>").append(Serializing.escapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.escapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.escapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.escapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.escapeForXml(Zip)).append("</Zip>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("</Pharmacy>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PharmacyNum")!=null) {
					PharmacyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PharmacyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PharmID")!=null) {
					PharmID=Serializing.getXmlNodeValue(doc,"PharmID");
				}
				if(Serializing.getXmlNodeValue(doc,"StoreName")!=null) {
					StoreName=Serializing.getXmlNodeValue(doc,"StoreName");
				}
				if(Serializing.getXmlNodeValue(doc,"Phone")!=null) {
					Phone=Serializing.getXmlNodeValue(doc,"Phone");
				}
				if(Serializing.getXmlNodeValue(doc,"Fax")!=null) {
					Fax=Serializing.getXmlNodeValue(doc,"Fax");
				}
				if(Serializing.getXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.getXmlNodeValue(doc,"Address");
				}
				if(Serializing.getXmlNodeValue(doc,"Address2")!=null) {
					Address2=Serializing.getXmlNodeValue(doc,"Address2");
				}
				if(Serializing.getXmlNodeValue(doc,"City")!=null) {
					City=Serializing.getXmlNodeValue(doc,"City");
				}
				if(Serializing.getXmlNodeValue(doc,"State")!=null) {
					State=Serializing.getXmlNodeValue(doc,"State");
				}
				if(Serializing.getXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.getXmlNodeValue(doc,"Zip");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
