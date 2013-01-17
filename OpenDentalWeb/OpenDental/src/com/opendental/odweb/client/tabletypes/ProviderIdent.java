package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public ProviderIdent deepCopy() {
			ProviderIdent providerident=new ProviderIdent();
			providerident.ProviderIdentNum=this.ProviderIdentNum;
			providerident.ProvNum=this.ProvNum;
			providerident.PayorID=this.PayorID;
			providerident.SuppIDType=this.SuppIDType;
			providerident.IDNumber=this.IDNumber;
			return providerident;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProviderIdent>");
			sb.append("<ProviderIdentNum>").append(ProviderIdentNum).append("</ProviderIdentNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<PayorID>").append(Serializing.escapeForXml(PayorID)).append("</PayorID>");
			sb.append("<SuppIDType>").append(SuppIDType.ordinal()).append("</SuppIDType>");
			sb.append("<IDNumber>").append(Serializing.escapeForXml(IDNumber)).append("</IDNumber>");
			sb.append("</ProviderIdent>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ProviderIdentNum")!=null) {
					ProviderIdentNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProviderIdentNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PayorID")!=null) {
					PayorID=Serializing.getXmlNodeValue(doc,"PayorID");
				}
				if(Serializing.getXmlNodeValue(doc,"SuppIDType")!=null) {
					SuppIDType=ProviderSupplementalID.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"SuppIDType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"IDNumber")!=null) {
					IDNumber=Serializing.getXmlNodeValue(doc,"IDNumber");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ProviderIdent: "+e.getMessage());
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
