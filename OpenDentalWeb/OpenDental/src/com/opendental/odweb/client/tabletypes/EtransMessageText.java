package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"EtransMessageTextNum")!=null) {
					EtransMessageTextNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EtransMessageTextNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MessageText")!=null) {
					MessageText=Serializing.GetXmlNodeValue(doc,"MessageText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
