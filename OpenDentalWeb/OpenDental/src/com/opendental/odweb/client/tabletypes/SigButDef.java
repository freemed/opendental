package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		/** Not a database field.  The sounds and lights attached to the button. */
		public SigButDefElement[] ElementList;

		/** Deep copy of object. */
		public SigButDef deepCopy() {
			SigButDef sigbutdef=new SigButDef();
			sigbutdef.SigButDefNum=this.SigButDefNum;
			sigbutdef.ButtonText=this.ButtonText;
			sigbutdef.ButtonIndex=this.ButtonIndex;
			sigbutdef.SynchIcon=this.SynchIcon;
			sigbutdef.ComputerName=this.ComputerName;
			sigbutdef.ElementList=this.ElementList;
			return sigbutdef;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SigButDef>");
			sb.append("<SigButDefNum>").append(SigButDefNum).append("</SigButDefNum>");
			sb.append("<ButtonText>").append(Serializing.escapeForXml(ButtonText)).append("</ButtonText>");
			sb.append("<ButtonIndex>").append(ButtonIndex).append("</ButtonIndex>");
			sb.append("<SynchIcon>").append(SynchIcon).append("</SynchIcon>");
			sb.append("<ComputerName>").append(Serializing.escapeForXml(ComputerName)).append("</ComputerName>");
			sb.append("</SigButDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SigButDefNum")!=null) {
					SigButDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SigButDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ButtonText")!=null) {
					ButtonText=Serializing.getXmlNodeValue(doc,"ButtonText");
				}
				if(Serializing.getXmlNodeValue(doc,"ButtonIndex")!=null) {
					ButtonIndex=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ButtonIndex"));
				}
				if(Serializing.getXmlNodeValue(doc,"SynchIcon")!=null) {
					SynchIcon=Byte.valueOf(Serializing.getXmlNodeValue(doc,"SynchIcon"));
				}
				if(Serializing.getXmlNodeValue(doc,"ComputerName")!=null) {
					ComputerName=Serializing.getXmlNodeValue(doc,"ComputerName");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing SigButDef: "+e.getMessage());
			}
		}


}
