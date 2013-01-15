package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public ElectID deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ElectID>");
			sb.append("<ElectIDNum>").append(ElectIDNum).append("</ElectIDNum>");
			sb.append("<PayorID>").append(Serializing.escapeForXml(PayorID)).append("</PayorID>");
			sb.append("<CarrierName>").append(Serializing.escapeForXml(CarrierName)).append("</CarrierName>");
			sb.append("<IsMedicaid>").append((IsMedicaid)?1:0).append("</IsMedicaid>");
			sb.append("<ProviderTypes>").append(Serializing.escapeForXml(ProviderTypes)).append("</ProviderTypes>");
			sb.append("<Comments>").append(Serializing.escapeForXml(Comments)).append("</Comments>");
			sb.append("</ElectID>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ElectIDNum")!=null) {
					ElectIDNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ElectIDNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PayorID")!=null) {
					PayorID=Serializing.getXmlNodeValue(doc,"PayorID");
				}
				if(Serializing.getXmlNodeValue(doc,"CarrierName")!=null) {
					CarrierName=Serializing.getXmlNodeValue(doc,"CarrierName");
				}
				if(Serializing.getXmlNodeValue(doc,"IsMedicaid")!=null) {
					IsMedicaid=(Serializing.getXmlNodeValue(doc,"IsMedicaid")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ProviderTypes")!=null) {
					ProviderTypes=Serializing.getXmlNodeValue(doc,"ProviderTypes");
				}
				if(Serializing.getXmlNodeValue(doc,"Comments")!=null) {
					Comments=Serializing.getXmlNodeValue(doc,"Comments");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
