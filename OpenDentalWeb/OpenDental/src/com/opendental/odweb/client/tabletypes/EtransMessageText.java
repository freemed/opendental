package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class EtransMessageText {
		/** Primary key. */
		public int EtransMessageTextNum;
		/** The entire message text, including carriage returns. */
		public String MessageText;

		/** Deep copy of object. */
		public EtransMessageText deepCopy() {
			EtransMessageText etransmessagetext=new EtransMessageText();
			etransmessagetext.EtransMessageTextNum=this.EtransMessageTextNum;
			etransmessagetext.MessageText=this.MessageText;
			return etransmessagetext;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EtransMessageText>");
			sb.append("<EtransMessageTextNum>").append(EtransMessageTextNum).append("</EtransMessageTextNum>");
			sb.append("<MessageText>").append(Serializing.escapeForXml(MessageText)).append("</MessageText>");
			sb.append("</EtransMessageText>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EtransMessageTextNum")!=null) {
					EtransMessageTextNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EtransMessageTextNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MessageText")!=null) {
					MessageText=Serializing.getXmlNodeValue(doc,"MessageText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
