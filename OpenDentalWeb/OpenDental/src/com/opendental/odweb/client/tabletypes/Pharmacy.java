package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public Pharmacy Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Pharmacy>");
			sb.append("<PharmacyNum>").append(PharmacyNum).append("</PharmacyNum>");
			sb.append("<PharmID>").append(Serializing.EscapeForXml(PharmID)).append("</PharmID>");
			sb.append("<StoreName>").append(Serializing.EscapeForXml(StoreName)).append("</StoreName>");
			sb.append("<Phone>").append(Serializing.EscapeForXml(Phone)).append("</Phone>");
			sb.append("<Fax>").append(Serializing.EscapeForXml(Fax)).append("</Fax>");
			sb.append("<Address>").append(Serializing.EscapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.EscapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.EscapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.EscapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.EscapeForXml(Zip)).append("</Zip>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("</Pharmacy>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"PharmacyNum")!=null) {
					PharmacyNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PharmacyNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PharmID")!=null) {
					PharmID=Serializing.GetXmlNodeValue(doc,"PharmID");
				}
				if(Serializing.GetXmlNodeValue(doc,"StoreName")!=null) {
					StoreName=Serializing.GetXmlNodeValue(doc,"StoreName");
				}
				if(Serializing.GetXmlNodeValue(doc,"Phone")!=null) {
					Phone=Serializing.GetXmlNodeValue(doc,"Phone");
				}
				if(Serializing.GetXmlNodeValue(doc,"Fax")!=null) {
					Fax=Serializing.GetXmlNodeValue(doc,"Fax");
				}
				if(Serializing.GetXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.GetXmlNodeValue(doc,"Address");
				}
				if(Serializing.GetXmlNodeValue(doc,"Address2")!=null) {
					Address2=Serializing.GetXmlNodeValue(doc,"Address2");
				}
				if(Serializing.GetXmlNodeValue(doc,"City")!=null) {
					City=Serializing.GetXmlNodeValue(doc,"City");
				}
				if(Serializing.GetXmlNodeValue(doc,"State")!=null) {
					State=Serializing.GetXmlNodeValue(doc,"State");
				}
				if(Serializing.GetXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.GetXmlNodeValue(doc,"Zip");
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
