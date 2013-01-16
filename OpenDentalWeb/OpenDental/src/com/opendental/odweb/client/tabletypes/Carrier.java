package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Carrier {
		/** Primary key. */
		public int CarrierNum;
		/** Name of the carrier. */
		public String CarrierName;
		/** . */
		public String Address;
		/** Second line of address. */
		public String Address2;
		/** . */
		public String City;
		/** 2 char in the US. */
		public String State;
		/** Postal code. */
		public String Zip;
		/** Includes any punctuation. */
		public String Phone;
		/** E-claims electronic payer id.  5 char in USA.  6 digits in Canada.  I've seen an ID this long before: "LA-DHH-MEDICAID".  The user interface currently limits length to 20, although db limits length to 255.  X12 requires length between 2 and 80. */
		public String ElectID;
		/** Do not send electronically.  It's just a default; you can still send electronically. */
		public boolean NoSendElect;
		/** Canada: True if a CDAnet carrier.  This has significant implications:  1. It can be filtered for in the list of carriers.  2. An ElectID is required.  3. The ElectID can never be used by another carrier.  4. If the carrier is attached to any etrans, then the ElectID cannot be changed (and, of course, the carrier cannot be deleted or combined). */
		public boolean IsCDA;
		/** The version of CDAnet supported.  Either 02 or 04. */
		public String CDAnetVersion;
		/** FK to canadiannetwork.CanadianNetworkNum.  Only used in Canada.  Right now, there is no UI to the canadiannetwork table in our db. */
		public int CanadianNetworkNum;
		/** . */
		public boolean IsHidden;
		/** 1=No Encryption, 2=CDAnet standard #1, 3=CDAnet standard #2.  Field A10. */
		public byte CanadianEncryptionMethod;
		/** Bit flags. */
		public CanSupTransTypes CanadianSupportedTypes;

		/** Deep copy of object. */
		public Carrier deepCopy() {
			Carrier carrier=new Carrier();
			carrier.CarrierNum=this.CarrierNum;
			carrier.CarrierName=this.CarrierName;
			carrier.Address=this.Address;
			carrier.Address2=this.Address2;
			carrier.City=this.City;
			carrier.State=this.State;
			carrier.Zip=this.Zip;
			carrier.Phone=this.Phone;
			carrier.ElectID=this.ElectID;
			carrier.NoSendElect=this.NoSendElect;
			carrier.IsCDA=this.IsCDA;
			carrier.CDAnetVersion=this.CDAnetVersion;
			carrier.CanadianNetworkNum=this.CanadianNetworkNum;
			carrier.IsHidden=this.IsHidden;
			carrier.CanadianEncryptionMethod=this.CanadianEncryptionMethod;
			carrier.CanadianSupportedTypes=this.CanadianSupportedTypes;
			return carrier;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Carrier>");
			sb.append("<CarrierNum>").append(CarrierNum).append("</CarrierNum>");
			sb.append("<CarrierName>").append(Serializing.escapeForXml(CarrierName)).append("</CarrierName>");
			sb.append("<Address>").append(Serializing.escapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.escapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.escapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.escapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.escapeForXml(Zip)).append("</Zip>");
			sb.append("<Phone>").append(Serializing.escapeForXml(Phone)).append("</Phone>");
			sb.append("<ElectID>").append(Serializing.escapeForXml(ElectID)).append("</ElectID>");
			sb.append("<NoSendElect>").append((NoSendElect)?1:0).append("</NoSendElect>");
			sb.append("<IsCDA>").append((IsCDA)?1:0).append("</IsCDA>");
			sb.append("<CDAnetVersion>").append(Serializing.escapeForXml(CDAnetVersion)).append("</CDAnetVersion>");
			sb.append("<CanadianNetworkNum>").append(CanadianNetworkNum).append("</CanadianNetworkNum>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<CanadianEncryptionMethod>").append(CanadianEncryptionMethod).append("</CanadianEncryptionMethod>");
			sb.append("<CanadianSupportedTypes>").append(CanadianSupportedTypes.ordinal()).append("</CanadianSupportedTypes>");
			sb.append("</Carrier>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CarrierNum")!=null) {
					CarrierNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CarrierNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CarrierName")!=null) {
					CarrierName=Serializing.getXmlNodeValue(doc,"CarrierName");
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
				if(Serializing.getXmlNodeValue(doc,"Phone")!=null) {
					Phone=Serializing.getXmlNodeValue(doc,"Phone");
				}
				if(Serializing.getXmlNodeValue(doc,"ElectID")!=null) {
					ElectID=Serializing.getXmlNodeValue(doc,"ElectID");
				}
				if(Serializing.getXmlNodeValue(doc,"NoSendElect")!=null) {
					NoSendElect=(Serializing.getXmlNodeValue(doc,"NoSendElect")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"IsCDA")!=null) {
					IsCDA=(Serializing.getXmlNodeValue(doc,"IsCDA")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"CDAnetVersion")!=null) {
					CDAnetVersion=Serializing.getXmlNodeValue(doc,"CDAnetVersion");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianNetworkNum")!=null) {
					CanadianNetworkNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CanadianNetworkNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianEncryptionMethod")!=null) {
					CanadianEncryptionMethod=Byte.valueOf(Serializing.getXmlNodeValue(doc,"CanadianEncryptionMethod"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianSupportedTypes")!=null) {
					CanadianSupportedTypes=CanSupTransTypes.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"CanadianSupportedTypes"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Type 23, Predetermination EOB (regular and embedded) are not included because they are not part of the testing scripts.  The three required types are not included: ClaimTransaction_01, ClaimAcknowledgement_11, and ClaimEOB_21.  Can't find specs for PredeterminationEobEmbedded. */
		public enum CanSupTransTypes {
			/**  */
			None,
			/**  */
			EligibilityTransaction_08,
			/**  */
			EligibilityResponse_18,
			/**  */
			CobClaimTransaction_07,
			/** ClaimAck_11 is not here because it's required by all carriers. */
			ClaimAckEmbedded_11e,
			/** ClaimEob_21 is not here because it's required by all carriers. */
			ClaimEobEmbedded_21e,
			/**  */
			ClaimReversal_02,
			/**  */
			ClaimReversalResponse_12,
			/**  */
			PredeterminationSinglePage_03,
			/**  */
			PredeterminationMultiPage_03,
			/**  */
			PredeterminationAck_13,
			/**  */
			PredeterminationAckEmbedded_13e,
			/**  */
			RequestForOutstandingTrans_04,
			/**  */
			OutstandingTransAck_14,
			/** Response */
			EmailTransaction_24,
			/**  */
			RequestForSummaryReconciliation_05,
			/**  */
			SummaryReconciliation_15,
			/**  */
			RequestForPaymentReconciliation_06,
			/**  */
			PaymentReconciliation_16
		}


}
