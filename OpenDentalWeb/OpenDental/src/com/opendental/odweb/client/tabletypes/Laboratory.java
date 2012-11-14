package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public Laboratory Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Laboratory>");
			sb.append("<LaboratoryNum>").append(LaboratoryNum).append("</LaboratoryNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<Phone>").append(Serializing.EscapeForXml(Phone)).append("</Phone>");
			sb.append("<Notes>").append(Serializing.EscapeForXml(Notes)).append("</Notes>");
			sb.append("<Slip>").append(Slip).append("</Slip>");
			sb.append("<Address>").append(Serializing.EscapeForXml(Address)).append("</Address>");
			sb.append("<City>").append(Serializing.EscapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.EscapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.EscapeForXml(Zip)).append("</Zip>");
			sb.append("<Email>").append(Serializing.EscapeForXml(Email)).append("</Email>");
			sb.append("<WirelessPhone>").append(Serializing.EscapeForXml(WirelessPhone)).append("</WirelessPhone>");
			sb.append("</Laboratory>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				LaboratoryNum=Integer.valueOf(doc.getElementsByTagName("LaboratoryNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				Phone=doc.getElementsByTagName("Phone").item(0).getFirstChild().getNodeValue();
				Notes=doc.getElementsByTagName("Notes").item(0).getFirstChild().getNodeValue();
				Slip=Integer.valueOf(doc.getElementsByTagName("Slip").item(0).getFirstChild().getNodeValue());
				Address=doc.getElementsByTagName("Address").item(0).getFirstChild().getNodeValue();
				City=doc.getElementsByTagName("City").item(0).getFirstChild().getNodeValue();
				State=doc.getElementsByTagName("State").item(0).getFirstChild().getNodeValue();
				Zip=doc.getElementsByTagName("Zip").item(0).getFirstChild().getNodeValue();
				Email=doc.getElementsByTagName("Email").item(0).getFirstChild().getNodeValue();
				WirelessPhone=doc.getElementsByTagName("WirelessPhone").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
