package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class Laboratory {
		/** Primary key. */
		public int LaboratoryNum;
		/** Description of lab. */
		public String Description;
		/** Freeform text includes punctuation. */
		public String Phone;
		/** Any notes.  No practical limit to amount of text. */
		public String Notes;
		/** FK to sheetdef.SheetDefNum.  Lab slips can be set for individual laboratories.  If zero, then the default internal lab slip will be used instead of a custom lab slip. */
		public int Slip;
		/** . */
		public String Address;
		/** . */
		public String City;
		/** . */
		public String State;
		/** . */
		public String Zip;
		/** . */
		public String Email;
		/** . */
		public String WirelessPhone;

		/** Deep copy of object. */
		public Laboratory deepCopy() {
			Laboratory laboratory=new Laboratory();
			laboratory.LaboratoryNum=this.LaboratoryNum;
			laboratory.Description=this.Description;
			laboratory.Phone=this.Phone;
			laboratory.Notes=this.Notes;
			laboratory.Slip=this.Slip;
			laboratory.Address=this.Address;
			laboratory.City=this.City;
			laboratory.State=this.State;
			laboratory.Zip=this.Zip;
			laboratory.Email=this.Email;
			laboratory.WirelessPhone=this.WirelessPhone;
			return laboratory;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Laboratory>");
			sb.append("<LaboratoryNum>").append(LaboratoryNum).append("</LaboratoryNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<Phone>").append(Serializing.escapeForXml(Phone)).append("</Phone>");
			sb.append("<Notes>").append(Serializing.escapeForXml(Notes)).append("</Notes>");
			sb.append("<Slip>").append(Slip).append("</Slip>");
			sb.append("<Address>").append(Serializing.escapeForXml(Address)).append("</Address>");
			sb.append("<City>").append(Serializing.escapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.escapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.escapeForXml(Zip)).append("</Zip>");
			sb.append("<Email>").append(Serializing.escapeForXml(Email)).append("</Email>");
			sb.append("<WirelessPhone>").append(Serializing.escapeForXml(WirelessPhone)).append("</WirelessPhone>");
			sb.append("</Laboratory>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"LaboratoryNum")!=null) {
					LaboratoryNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LaboratoryNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"Phone")!=null) {
					Phone=Serializing.getXmlNodeValue(doc,"Phone");
				}
				if(Serializing.getXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.getXmlNodeValue(doc,"Notes");
				}
				if(Serializing.getXmlNodeValue(doc,"Slip")!=null) {
					Slip=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Slip"));
				}
				if(Serializing.getXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.getXmlNodeValue(doc,"Address");
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
				if(Serializing.getXmlNodeValue(doc,"Email")!=null) {
					Email=Serializing.getXmlNodeValue(doc,"Email");
				}
				if(Serializing.getXmlNodeValue(doc,"WirelessPhone")!=null) {
					WirelessPhone=Serializing.getXmlNodeValue(doc,"WirelessPhone");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
