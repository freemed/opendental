package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class EtransMessageText {
		/** Primary key. */
		public int EtransMessageTextNum;
		/** The entire message text, including carriage returns. */
		public String MessageText;

		/** Deep copy of object. */
		public EtransMessageText Copy() {
			EtransMessageText etransmessagetext=new EtransMessageText();
			etransmessagetext.EtransMessageTextNum=this.EtransMessageTextNum;
			etransmessagetext.MessageText=this.MessageText;
			return etransmessagetext;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EtransMessageText>");
			sb.append("<EtransMessageTextNum>").append(EtransMessageTextNum).append("</EtransMessageTextNum>");
			sb.append("<MessageText>").append(Serializing.EscapeForXml(MessageText)).append("</MessageText>");
			sb.append("</EtransMessageText>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EtransMessageTextNum=Integer.valueOf(doc.getElementsByTagName("EtransMessageTextNum").item(0).getFirstChild().getNodeValue());
				MessageText=doc.getElementsByTagName("MessageText").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
