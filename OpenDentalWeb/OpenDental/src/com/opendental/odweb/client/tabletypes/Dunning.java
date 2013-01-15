package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class Dunning {
		/** Primary key. */
		public int DunningNum;
		/** The actual dunning message that will go on the patient bill. */
		public String DunMessage;
		/** FK to definition.DefNum. */
		public int BillingType;
		/** Program forces only 0,30,60,or 90. */
		public byte AgeAccount;
		/** Enum:YN Set Y to only show if insurance is pending. */
		public YN InsIsPending;
		/** A message that will be copied to the NoteBold field of the Statement. */
		public String MessageBold;

		/** Deep copy of object. */
		public Dunning deepCopy() {
			Dunning dunning=new Dunning();
			dunning.DunningNum=this.DunningNum;
			dunning.DunMessage=this.DunMessage;
			dunning.BillingType=this.BillingType;
			dunning.AgeAccount=this.AgeAccount;
			dunning.InsIsPending=this.InsIsPending;
			dunning.MessageBold=this.MessageBold;
			return dunning;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Dunning>");
			sb.append("<DunningNum>").append(DunningNum).append("</DunningNum>");
			sb.append("<DunMessage>").append(Serializing.escapeForXml(DunMessage)).append("</DunMessage>");
			sb.append("<BillingType>").append(BillingType).append("</BillingType>");
			sb.append("<AgeAccount>").append(AgeAccount).append("</AgeAccount>");
			sb.append("<InsIsPending>").append(InsIsPending.ordinal()).append("</InsIsPending>");
			sb.append("<MessageBold>").append(Serializing.escapeForXml(MessageBold)).append("</MessageBold>");
			sb.append("</Dunning>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DunningNum")!=null) {
					DunningNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DunningNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DunMessage")!=null) {
					DunMessage=Serializing.getXmlNodeValue(doc,"DunMessage");
				}
				if(Serializing.getXmlNodeValue(doc,"BillingType")!=null) {
					BillingType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"BillingType"));
				}
				if(Serializing.getXmlNodeValue(doc,"AgeAccount")!=null) {
					AgeAccount=Byte.valueOf(Serializing.getXmlNodeValue(doc,"AgeAccount"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsIsPending")!=null) {
					InsIsPending=YN.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsIsPending"))];
				}
				if(Serializing.getXmlNodeValue(doc,"MessageBold")!=null) {
					MessageBold=Serializing.getXmlNodeValue(doc,"MessageBold");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Unknown,Yes, or No. */
		public enum YN {
			/** 0 */
			Unknown,
			/** 1 */
			Yes,
			/** 2 */
			No
		}


}
