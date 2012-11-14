package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class CanadianNetwork {
		/** Primary key. */
		public int CanadianNetworkNum;
		/** This will also be the folder name */
		public String Abbrev;
		/** . */
		public String Descript;
		/** A01.  Up to 12 char. */
		public String CanadianTransactionPrefix;
		/** Set to true if this network is in charge of handling all Request for Payment Reconciliation (RPR) transactions for all carriers within this network, as opposed to the individual carriers wihtin the network processing the RPR transactions themselves. */
		public boolean CanadianIsRprHandler;

		/** Deep copy of object. */
		public CanadianNetwork Copy() {
			CanadianNetwork canadiannetwork=new CanadianNetwork();
			canadiannetwork.CanadianNetworkNum=this.CanadianNetworkNum;
			canadiannetwork.Abbrev=this.Abbrev;
			canadiannetwork.Descript=this.Descript;
			canadiannetwork.CanadianTransactionPrefix=this.CanadianTransactionPrefix;
			canadiannetwork.CanadianIsRprHandler=this.CanadianIsRprHandler;
			return canadiannetwork;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CanadianNetwork>");
			sb.append("<CanadianNetworkNum>").append(CanadianNetworkNum).append("</CanadianNetworkNum>");
			sb.append("<Abbrev>").append(Serializing.EscapeForXml(Abbrev)).append("</Abbrev>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<CanadianTransactionPrefix>").append(Serializing.EscapeForXml(CanadianTransactionPrefix)).append("</CanadianTransactionPrefix>");
			sb.append("<CanadianIsRprHandler>").append((CanadianIsRprHandler)?1:0).append("</CanadianIsRprHandler>");
			sb.append("</CanadianNetwork>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				CanadianNetworkNum=Integer.valueOf(doc.getElementsByTagName("CanadianNetworkNum").item(0).getFirstChild().getNodeValue());
				Abbrev=doc.getElementsByTagName("Abbrev").item(0).getFirstChild().getNodeValue();
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				CanadianTransactionPrefix=doc.getElementsByTagName("CanadianTransactionPrefix").item(0).getFirstChild().getNodeValue();
				CanadianIsRprHandler=(doc.getElementsByTagName("CanadianIsRprHandler").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}


}
