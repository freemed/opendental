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
				PharmacyNum=Integer.valueOf(doc.getElementsByTagName("PharmacyNum").item(0).getFirstChild().getNodeValue());
				PharmID=doc.getElementsByTagName("PharmID").item(0).getFirstChild().getNodeValue();
				StoreName=doc.getElementsByTagName("StoreName").item(0).getFirstChild().getNodeValue();
				Phone=doc.getElementsByTagName("Phone").item(0).getFirstChild().getNodeValue();
				Fax=doc.getElementsByTagName("Fax").item(0).getFirstChild().getNodeValue();
				Address=doc.getElementsByTagName("Address").item(0).getFirstChild().getNodeValue();
				Address2=doc.getElementsByTagName("Address2").item(0).getFirstChild().getNodeValue();
				City=doc.getElementsByTagName("City").item(0).getFirstChild().getNodeValue();
				State=doc.getElementsByTagName("State").item(0).getFirstChild().getNodeValue();
				Zip=doc.getElementsByTagName("Zip").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
