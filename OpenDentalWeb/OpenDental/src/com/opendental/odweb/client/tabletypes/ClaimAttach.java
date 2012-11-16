package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ClaimAttach {
		/** Primary key. */
		public int ClaimAttachNum;
		/** FK to claim.ClaimNum */
		public int ClaimNum;
		/** The name of the file that shows on the claim.  For example: tooth2.jpg. */
		public String DisplayedFileName;
		/** The actual file is stored in the A-Z folder in EmailAttachments.  (yes, even though it's not actually an email attachment)  The files are named automatically based on Date/time along with a random number.  This ensures that they will be sequential as well as unique. */
		public String ActualFileName;

		/** Deep copy of object. */
		public ClaimAttach Copy() {
			ClaimAttach claimattach=new ClaimAttach();
			claimattach.ClaimAttachNum=this.ClaimAttachNum;
			claimattach.ClaimNum=this.ClaimNum;
			claimattach.DisplayedFileName=this.DisplayedFileName;
			claimattach.ActualFileName=this.ActualFileName;
			return claimattach;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimAttach>");
			sb.append("<ClaimAttachNum>").append(ClaimAttachNum).append("</ClaimAttachNum>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<DisplayedFileName>").append(Serializing.EscapeForXml(DisplayedFileName)).append("</DisplayedFileName>");
			sb.append("<ActualFileName>").append(Serializing.EscapeForXml(ActualFileName)).append("</ActualFileName>");
			sb.append("</ClaimAttach>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ClaimAttachNum")!=null) {
					ClaimAttachNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimAttachNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimNum")!=null) {
					ClaimNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DisplayedFileName")!=null) {
					DisplayedFileName=Serializing.GetXmlNodeValue(doc,"DisplayedFileName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ActualFileName")!=null) {
					ActualFileName=Serializing.GetXmlNodeValue(doc,"ActualFileName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
