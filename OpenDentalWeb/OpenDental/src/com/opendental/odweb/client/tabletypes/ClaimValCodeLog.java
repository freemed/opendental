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
				if(Serializing.GetXmlNodeValue(doc,"ClaimValCodeLogNum")!=null) {
					ClaimValCodeLogNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimValCodeLogNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimNum")!=null) {
					ClaimNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimField")!=null) {
					ClaimField=Serializing.GetXmlNodeValue(doc,"ClaimField");
				}
				if(Serializing.GetXmlNodeValue(doc,"ValCode")!=null) {
					ValCode=Serializing.GetXmlNodeValue(doc,"ValCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"ValAmount")!=null) {
					ValAmount=Double.valueOf(Serializing.GetXmlNodeValue(doc,"ValAmount"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Ordinal")!=null) {
					Ordinal=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Ordinal"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
