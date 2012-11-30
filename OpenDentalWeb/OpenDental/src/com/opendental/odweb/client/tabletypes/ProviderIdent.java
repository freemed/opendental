package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ProviderIdentNum")!=null) {
					ProviderIdentNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProviderIdentNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PayorID")!=null) {
					PayorID=Serializing.GetXmlNodeValue(doc,"PayorID");
				}
				if(Serializing.GetXmlNodeValue(doc,"SuppIDType")!=null) {
					SuppIDType=ProviderSupplementalID.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SuppIDType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"IDNumber")!=null) {
					IDNumber=Serializing.GetXmlNodeValue(doc,"IDNumber");
				}
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
