package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ProviderIdent {
		/** Primary key. */
		public int ProviderIdentNum;
		/** FK to provider.ProvNum.  An ID only applies to one provider. */
		public int ProvNum;
		/** FK to carrier.ElectID  aka Electronic ID. An ID only applies to one insurance carrier. */
		public String PayorID;
		/** Enum:ProviderSupplementalID */
		public ProviderSupplementalID SuppIDType;
		/** The number assigned by the ins carrier. */
		public String IDNumber;

		/** Deep copy of object. */
		public ProviderIdent Copy() {
			ProviderIdent providerident=new ProviderIdent();
			providerident.ProviderIdentNum=this.ProviderIdentNum;
			providerident.ProvNum=this.ProvNum;
			providerident.PayorID=this.PayorID;
			providerident.SuppIDType=this.SuppIDType;
			providerident.IDNumber=this.IDNumber;
			return providerident;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProviderIdent>");
			sb.append("<ProviderIdentNum>").append(ProviderIdentNum).append("</ProviderIdentNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<PayorID>").append(Serializing.EscapeForXml(PayorID)).append("</PayorID>");
			sb.append("<SuppIDType>").append(SuppIDType.ordinal()).append("</SuppIDType>");
			sb.append("<IDNumber>").append(Serializing.EscapeForXml(IDNumber)).append("</IDNumber>");
			sb.append("</ProviderIdent>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ProviderIdentNum=Integer.valueOf(doc.getElementsByTagName("ProviderIdentNum").item(0).getFirstChild().getNodeValue());
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				PayorID=doc.getElementsByTagName("PayorID").item(0).getFirstChild().getNodeValue();
				SuppIDType=ProviderSupplementalID.values()[Integer.valueOf(doc.getElementsByTagName("SuppIDType").item(0).getFirstChild().getNodeValue())];
				IDNumber=doc.getElementsByTagName("IDNumber").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Used when submitting e-claims to some carriers who require extra provider identifiers.  Usage varies by company.  Only used as needed. */
		public enum ProviderSupplementalID {
			/** 0 */
			BlueCross,
			/** 1 */
			BlueShield,
			/** 2 */
			SiteNumber,
			/** 3 */
			CommercialNumber
		}


}
