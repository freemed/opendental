package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public ClaimValCodeLog deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimValCodeLog>");
			sb.append("<ClaimValCodeLogNum>").append(ClaimValCodeLogNum).append("</ClaimValCodeLogNum>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<ClaimField>").append(Serializing.escapeForXml(ClaimField)).append("</ClaimField>");
			sb.append("<ValCode>").append(Serializing.escapeForXml(ValCode)).append("</ValCode>");
			sb.append("<ValAmount>").append(ValAmount).append("</ValAmount>");
			sb.append("<Ordinal>").append(Ordinal).append("</Ordinal>");
			sb.append("</ClaimValCodeLog>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ClaimValCodeLogNum")!=null) {
					ClaimValCodeLogNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimValCodeLogNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimNum")!=null) {
					ClaimNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimField")!=null) {
					ClaimField=Serializing.getXmlNodeValue(doc,"ClaimField");
				}
				if(Serializing.getXmlNodeValue(doc,"ValCode")!=null) {
					ValCode=Serializing.getXmlNodeValue(doc,"ValCode");
				}
				if(Serializing.getXmlNodeValue(doc,"ValAmount")!=null) {
					ValAmount=Double.valueOf(Serializing.getXmlNodeValue(doc,"ValAmount"));
				}
				if(Serializing.getXmlNodeValue(doc,"Ordinal")!=null) {
					Ordinal=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Ordinal"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
