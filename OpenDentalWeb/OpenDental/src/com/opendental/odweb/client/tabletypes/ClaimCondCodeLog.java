package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public ClaimCondCodeLog Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimCondCodeLog>");
			sb.append("<ClaimCondCodeLogNum>").append(ClaimCondCodeLogNum).append("</ClaimCondCodeLogNum>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<Code0>").append(Serializing.EscapeForXml(Code0)).append("</Code0>");
			sb.append("<Code1>").append(Serializing.EscapeForXml(Code1)).append("</Code1>");
			sb.append("<Code2>").append(Serializing.EscapeForXml(Code2)).append("</Code2>");
			sb.append("<Code3>").append(Serializing.EscapeForXml(Code3)).append("</Code3>");
			sb.append("<Code4>").append(Serializing.EscapeForXml(Code4)).append("</Code4>");
			sb.append("<Code5>").append(Serializing.EscapeForXml(Code5)).append("</Code5>");
			sb.append("<Code6>").append(Serializing.EscapeForXml(Code6)).append("</Code6>");
			sb.append("<Code7>").append(Serializing.EscapeForXml(Code7)).append("</Code7>");
			sb.append("<Code8>").append(Serializing.EscapeForXml(Code8)).append("</Code8>");
			sb.append("<Code9>").append(Serializing.EscapeForXml(Code9)).append("</Code9>");
			sb.append("<Code10>").append(Serializing.EscapeForXml(Code10)).append("</Code10>");
			sb.append("</ClaimCondCodeLog>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ClaimCondCodeLogNum=Integer.valueOf(doc.getElementsByTagName("ClaimCondCodeLogNum").item(0).getFirstChild().getNodeValue());
				ClaimNum=Integer.valueOf(doc.getElementsByTagName("ClaimNum").item(0).getFirstChild().getNodeValue());
				Code0=doc.getElementsByTagName("Code0").item(0).getFirstChild().getNodeValue();
				Code1=doc.getElementsByTagName("Code1").item(0).getFirstChild().getNodeValue();
				Code2=doc.getElementsByTagName("Code2").item(0).getFirstChild().getNodeValue();
				Code3=doc.getElementsByTagName("Code3").item(0).getFirstChild().getNodeValue();
				Code4=doc.getElementsByTagName("Code4").item(0).getFirstChild().getNodeValue();
				Code5=doc.getElementsByTagName("Code5").item(0).getFirstChild().getNodeValue();
				Code6=doc.getElementsByTagName("Code6").item(0).getFirstChild().getNodeValue();
				Code7=doc.getElementsByTagName("Code7").item(0).getFirstChild().getNodeValue();
				Code8=doc.getElementsByTagName("Code8").item(0).getFirstChild().getNodeValue();
				Code9=doc.getElementsByTagName("Code9").item(0).getFirstChild().getNodeValue();
				Code10=doc.getElementsByTagName("Code10").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
