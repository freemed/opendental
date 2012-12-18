package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class AutoNote {
		/** Primary key */
		public int AutoNoteNum;
		/** Name of AutoNote */
		public String AutoNoteName;
		/** Was 'ControlsToInc' in previous versions. */
		public String MainText;

		/** Deep copy of object. */
		public AutoNote deepCopy() {
			AutoNote autonote=new AutoNote();
			autonote.AutoNoteNum=this.AutoNoteNum;
			autonote.AutoNoteName=this.AutoNoteName;
			autonote.MainText=this.MainText;
			return autonote;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutoNote>");
			sb.append("<AutoNoteNum>").append(AutoNoteNum).append("</AutoNoteNum>");
			sb.append("<AutoNoteName>").append(Serializing.escapeForXml(AutoNoteName)).append("</AutoNoteName>");
			sb.append("<MainText>").append(Serializing.escapeForXml(MainText)).append("</MainText>");
			sb.append("</AutoNote>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AutoNoteNum")!=null) {
					AutoNoteNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutoNoteNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AutoNoteName")!=null) {
					AutoNoteName=Serializing.getXmlNodeValue(doc,"AutoNoteName");
				}
				if(Serializing.getXmlNodeValue(doc,"MainText")!=null) {
					MainText=Serializing.getXmlNodeValue(doc,"MainText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
