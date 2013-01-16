package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ClaimCondCodeLog {
		/** Primary key. */
		public int ClaimCondCodeLogNum;
		/** FK to claim.ClaimNum. */
		public int ClaimNum;
		/** Corresponds with condition code 18 on the UB04. */
		public String Code0;
		/** Corresponds with condition code 19 on the UB04. */
		public String Code1;
		/** Corresponds with condition code 20 on the UB04. */
		public String Code2;
		/** Corresponds with condition code 21 on the UB04. */
		public String Code3;
		/** Corresponds with condition code 22 on the UB04. */
		public String Code4;
		/** Corresponds with condition code 23 on the UB04. */
		public String Code5;
		/** Corresponds with condition code 24 on the UB04. */
		public String Code6;
		/** Corresponds with condition code 25 on the UB04. */
		public String Code7;
		/** Corresponds with condition code 26 on the UB04. */
		public String Code8;
		/** Corresponds with condition code 27 on the UB04. */
		public String Code9;
		/** Corresponds with condition code 28 on the UB04. */
		public String Code10;

		/** Deep copy of object. */
		public ClaimCondCodeLog deepCopy() {
			ClaimCondCodeLog claimcondcodelog=new ClaimCondCodeLog();
			claimcondcodelog.ClaimCondCodeLogNum=this.ClaimCondCodeLogNum;
			claimcondcodelog.ClaimNum=this.ClaimNum;
			claimcondcodelog.Code0=this.Code0;
			claimcondcodelog.Code1=this.Code1;
			claimcondcodelog.Code2=this.Code2;
			claimcondcodelog.Code3=this.Code3;
			claimcondcodelog.Code4=this.Code4;
			claimcondcodelog.Code5=this.Code5;
			claimcondcodelog.Code6=this.Code6;
			claimcondcodelog.Code7=this.Code7;
			claimcondcodelog.Code8=this.Code8;
			claimcondcodelog.Code9=this.Code9;
			claimcondcodelog.Code10=this.Code10;
			return claimcondcodelog;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimCondCodeLog>");
			sb.append("<ClaimCondCodeLogNum>").append(ClaimCondCodeLogNum).append("</ClaimCondCodeLogNum>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<Code0>").append(Serializing.escapeForXml(Code0)).append("</Code0>");
			sb.append("<Code1>").append(Serializing.escapeForXml(Code1)).append("</Code1>");
			sb.append("<Code2>").append(Serializing.escapeForXml(Code2)).append("</Code2>");
			sb.append("<Code3>").append(Serializing.escapeForXml(Code3)).append("</Code3>");
			sb.append("<Code4>").append(Serializing.escapeForXml(Code4)).append("</Code4>");
			sb.append("<Code5>").append(Serializing.escapeForXml(Code5)).append("</Code5>");
			sb.append("<Code6>").append(Serializing.escapeForXml(Code6)).append("</Code6>");
			sb.append("<Code7>").append(Serializing.escapeForXml(Code7)).append("</Code7>");
			sb.append("<Code8>").append(Serializing.escapeForXml(Code8)).append("</Code8>");
			sb.append("<Code9>").append(Serializing.escapeForXml(Code9)).append("</Code9>");
			sb.append("<Code10>").append(Serializing.escapeForXml(Code10)).append("</Code10>");
			sb.append("</ClaimCondCodeLog>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ClaimCondCodeLogNum")!=null) {
					ClaimCondCodeLogNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimCondCodeLogNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimNum")!=null) {
					ClaimNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Code0")!=null) {
					Code0=Serializing.getXmlNodeValue(doc,"Code0");
				}
				if(Serializing.getXmlNodeValue(doc,"Code1")!=null) {
					Code1=Serializing.getXmlNodeValue(doc,"Code1");
				}
				if(Serializing.getXmlNodeValue(doc,"Code2")!=null) {
					Code2=Serializing.getXmlNodeValue(doc,"Code2");
				}
				if(Serializing.getXmlNodeValue(doc,"Code3")!=null) {
					Code3=Serializing.getXmlNodeValue(doc,"Code3");
				}
				if(Serializing.getXmlNodeValue(doc,"Code4")!=null) {
					Code4=Serializing.getXmlNodeValue(doc,"Code4");
				}
				if(Serializing.getXmlNodeValue(doc,"Code5")!=null) {
					Code5=Serializing.getXmlNodeValue(doc,"Code5");
				}
				if(Serializing.getXmlNodeValue(doc,"Code6")!=null) {
					Code6=Serializing.getXmlNodeValue(doc,"Code6");
				}
				if(Serializing.getXmlNodeValue(doc,"Code7")!=null) {
					Code7=Serializing.getXmlNodeValue(doc,"Code7");
				}
				if(Serializing.getXmlNodeValue(doc,"Code8")!=null) {
					Code8=Serializing.getXmlNodeValue(doc,"Code8");
				}
				if(Serializing.getXmlNodeValue(doc,"Code9")!=null) {
					Code9=Serializing.getXmlNodeValue(doc,"Code9");
				}
				if(Serializing.getXmlNodeValue(doc,"Code10")!=null) {
					Code10=Serializing.getXmlNodeValue(doc,"Code10");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
