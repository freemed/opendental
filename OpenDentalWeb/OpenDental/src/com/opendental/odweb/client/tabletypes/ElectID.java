package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ElectID {
		/** Primary key. */
		public int ElectIDNum;
		/** aka Electronic ID.  A simple string. */
		public String PayorID;
		/** Used when doing a search. */
		public String CarrierName;
		/** True if medicaid. Then, the billing and treating providers will have their Medicaid ID's attached. */
		public boolean IsMedicaid;
		/** Integers separated by commas. Each long represents a ProviderSupplementalID type that is required by this insurance. Usually only used for BCBS or other carriers that require supplemental provider id's.  Even if we don't put the supplemental types in here, the user can still add them.  This just helps by doing an additional check for known required types. */
		public String ProviderTypes;
		/** Any comments. Usually includes enrollment requirements and descriptions of how to use the provider id's supplied by the carrier because they might call them by different names. */
		public String Comments;

		/** Deep copy of object. */
		public ElectID Copy() {
			ElectID electid=new ElectID();
			electid.ElectIDNum=this.ElectIDNum;
			electid.PayorID=this.PayorID;
			electid.CarrierName=this.CarrierName;
			electid.IsMedicaid=this.IsMedicaid;
			electid.ProviderTypes=this.ProviderTypes;
			electid.Comments=this.Comments;
			return electid;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ElectID>");
			sb.append("<ElectIDNum>").append(ElectIDNum).append("</ElectIDNum>");
			sb.append("<PayorID>").append(Serializing.EscapeForXml(PayorID)).append("</PayorID>");
			sb.append("<CarrierName>").append(Serializing.EscapeForXml(CarrierName)).append("</CarrierName>");
			sb.append("<IsMedicaid>").append((IsMedicaid)?1:0).append("</IsMedicaid>");
			sb.append("<ProviderTypes>").append(Serializing.EscapeForXml(ProviderTypes)).append("</ProviderTypes>");
			sb.append("<Comments>").append(Serializing.EscapeForXml(Comments)).append("</Comments>");
			sb.append("</ElectID>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ElectIDNum=Integer.valueOf(doc.getElementsByTagName("ElectIDNum").item(0).getFirstChild().getNodeValue());
				PayorID=doc.getElementsByTagName("PayorID").item(0).getFirstChild().getNodeValue();
				CarrierName=doc.getElementsByTagName("CarrierName").item(0).getFirstChild().getNodeValue();
				IsMedicaid=(doc.getElementsByTagName("IsMedicaid").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ProviderTypes=doc.getElementsByTagName("ProviderTypes").item(0).getFirstChild().getNodeValue();
				Comments=doc.getElementsByTagName("Comments").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
