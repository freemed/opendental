package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ClaimValCodeLog {
		/** Primary key. */
		public int ClaimValCodeLogNum;
		/** FK to claim.ClaimNum. */
		public int ClaimNum;
		/** Descriptive abbreviation to help place field on form (Ex: "FL55" for field 55). */
		public String ClaimField;
		/** Value Code. 2 char. */
		public String ValCode;
		/** Value Code Amount. */
		public double ValAmount;
		/** Order of Value Code */
		public int Ordinal;

		/** Deep copy of object. */
		public ClaimValCodeLog Copy() {
			ClaimValCodeLog claimvalcodelog=new ClaimValCodeLog();
			claimvalcodelog.ClaimValCodeLogNum=this.ClaimValCodeLogNum;
			claimvalcodelog.ClaimNum=this.ClaimNum;
			claimvalcodelog.ClaimField=this.ClaimField;
			claimvalcodelog.ValCode=this.ValCode;
			claimvalcodelog.ValAmount=this.ValAmount;
			claimvalcodelog.Ordinal=this.Ordinal;
			return claimvalcodelog;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimValCodeLog>");
			sb.append("<ClaimValCodeLogNum>").append(ClaimValCodeLogNum).append("</ClaimValCodeLogNum>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<ClaimField>").append(Serializing.EscapeForXml(ClaimField)).append("</ClaimField>");
			sb.append("<ValCode>").append(Serializing.EscapeForXml(ValCode)).append("</ValCode>");
			sb.append("<ValAmount>").append(ValAmount).append("</ValAmount>");
			sb.append("<Ordinal>").append(Ordinal).append("</Ordinal>");
			sb.append("</ClaimValCodeLog>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ClaimValCodeLogNum=Integer.valueOf(doc.getElementsByTagName("ClaimValCodeLogNum").item(0).getFirstChild().getNodeValue());
				ClaimNum=Integer.valueOf(doc.getElementsByTagName("ClaimNum").item(0).getFirstChild().getNodeValue());
				ClaimField=doc.getElementsByTagName("ClaimField").item(0).getFirstChild().getNodeValue();
				ValCode=doc.getElementsByTagName("ValCode").item(0).getFirstChild().getNodeValue();
				ValAmount=Double.valueOf(doc.getElementsByTagName("ValAmount").item(0).getFirstChild().getNodeValue());
				Ordinal=Integer.valueOf(doc.getElementsByTagName("Ordinal").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
