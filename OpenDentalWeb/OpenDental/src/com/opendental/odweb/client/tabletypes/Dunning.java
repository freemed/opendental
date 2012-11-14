package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public Dunning Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Dunning>");
			sb.append("<DunningNum>").append(DunningNum).append("</DunningNum>");
			sb.append("<DunMessage>").append(Serializing.EscapeForXml(DunMessage)).append("</DunMessage>");
			sb.append("<BillingType>").append(BillingType).append("</BillingType>");
			sb.append("<AgeAccount>").append(AgeAccount).append("</AgeAccount>");
			sb.append("<InsIsPending>").append(InsIsPending.ordinal()).append("</InsIsPending>");
			sb.append("<MessageBold>").append(Serializing.EscapeForXml(MessageBold)).append("</MessageBold>");
			sb.append("</Dunning>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				DunningNum=Integer.valueOf(doc.getElementsByTagName("DunningNum").item(0).getFirstChild().getNodeValue());
				DunMessage=doc.getElementsByTagName("DunMessage").item(0).getFirstChild().getNodeValue();
				BillingType=Integer.valueOf(doc.getElementsByTagName("BillingType").item(0).getFirstChild().getNodeValue());
				AgeAccount=Byte.valueOf(doc.getElementsByTagName("AgeAccount").item(0).getFirstChild().getNodeValue());
				InsIsPending=YN.values()[Integer.valueOf(doc.getElementsByTagName("InsIsPending").item(0).getFirstChild().getNodeValue())];
				MessageBold=doc.getElementsByTagName("MessageBold").item(0).getFirstChild().getNodeValue();
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
