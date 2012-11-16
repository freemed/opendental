package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class AutoNote {
		/** Primary key */
		public int AutoNoteNum;
		/** Name of AutoNote */
		public String AutoNoteName;
		/** Was 'ControlsToInc' in previous versions. */
		public String MainText;

		/** Deep copy of object. */
		public AutoNote Copy() {
			AutoNote autonote=new AutoNote();
			autonote.AutoNoteNum=this.AutoNoteNum;
			autonote.AutoNoteName=this.AutoNoteName;
			autonote.MainText=this.MainText;
			return autonote;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutoNote>");
			sb.append("<AutoNoteNum>").append(AutoNoteNum).append("</AutoNoteNum>");
			sb.append("<AutoNoteName>").append(Serializing.EscapeForXml(AutoNoteName)).append("</AutoNoteName>");
			sb.append("<MainText>").append(Serializing.EscapeForXml(MainText)).append("</MainText>");
			sb.append("</AutoNote>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"AutoNoteNum")!=null) {
					AutoNoteNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AutoNoteNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AutoNoteName")!=null) {
					AutoNoteName=Serializing.GetXmlNodeValue(doc,"AutoNoteName");
				}
				if(Serializing.GetXmlNodeValue(doc,"MainText")!=null) {
					MainText=Serializing.GetXmlNodeValue(doc,"MainText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
