package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class SigButDef {
		/** Primary key. */
		public int SigButDefNum;
		/** The text on the button */
		public String ButtonText;
		/** 0-based index defines the order of the buttons. */
		public int ButtonIndex;
		/** 0=none, or 1-9. The cell in the 3x3 tic-tac-toe main program icon that is to be synched with this button.  It will light up or clear whenever this button lights or clears. */
		public byte SynchIcon;
		/** Blank for the default buttons.  Or contains the computer name for the buttons that override the defaults. */
		public String ComputerName;

		/** Deep copy of object. */
		public SigButDef Copy() {
			SigButDef sigbutdef=new SigButDef();
			sigbutdef.SigButDefNum=this.SigButDefNum;
			sigbutdef.ButtonText=this.ButtonText;
			sigbutdef.ButtonIndex=this.ButtonIndex;
			sigbutdef.SynchIcon=this.SynchIcon;
			sigbutdef.ComputerName=this.ComputerName;
			return sigbutdef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SigButDef>");
			sb.append("<SigButDefNum>").append(SigButDefNum).append("</SigButDefNum>");
			sb.append("<ButtonText>").append(Serializing.EscapeForXml(ButtonText)).append("</ButtonText>");
			sb.append("<ButtonIndex>").append(ButtonIndex).append("</ButtonIndex>");
			sb.append("<SynchIcon>").append(SynchIcon).append("</SynchIcon>");
			sb.append("<ComputerName>").append(Serializing.EscapeForXml(ComputerName)).append("</ComputerName>");
			sb.append("</SigButDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"SigButDefNum")!=null) {
					SigButDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SigButDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ButtonText")!=null) {
					ButtonText=Serializing.GetXmlNodeValue(doc,"ButtonText");
				}
				if(Serializing.GetXmlNodeValue(doc,"ButtonIndex")!=null) {
					ButtonIndex=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ButtonIndex"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SynchIcon")!=null) {
					SynchIcon=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"SynchIcon"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ComputerName")!=null) {
					ComputerName=Serializing.GetXmlNodeValue(doc,"ComputerName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
