package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class AutoNoteControl {
		/** Primary key */
		public int AutoNoteControlNum;
		/** The description of the prompt as it will be referred to from other windows. */
		public String Descript;
		/** 'Text', 'OneResponse', or 'MultiResponse'.  More types to be added later. */
		public String ControlType;
		/** The prompt text. */
		public String ControlLabel;
		/** For TextBox, this is the default text.  For a ComboBox, this is the list of possible responses, one per line. */
		public String ControlOptions;

		/** Deep copy of object. */
		public AutoNoteControl Copy() {
			AutoNoteControl autonotecontrol=new AutoNoteControl();
			autonotecontrol.AutoNoteControlNum=this.AutoNoteControlNum;
			autonotecontrol.Descript=this.Descript;
			autonotecontrol.ControlType=this.ControlType;
			autonotecontrol.ControlLabel=this.ControlLabel;
			autonotecontrol.ControlOptions=this.ControlOptions;
			return autonotecontrol;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutoNoteControl>");
			sb.append("<AutoNoteControlNum>").append(AutoNoteControlNum).append("</AutoNoteControlNum>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<ControlType>").append(Serializing.EscapeForXml(ControlType)).append("</ControlType>");
			sb.append("<ControlLabel>").append(Serializing.EscapeForXml(ControlLabel)).append("</ControlLabel>");
			sb.append("<ControlOptions>").append(Serializing.EscapeForXml(ControlOptions)).append("</ControlOptions>");
			sb.append("</AutoNoteControl>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"AutoNoteControlNum")!=null) {
					AutoNoteControlNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AutoNoteControlNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.GetXmlNodeValue(doc,"Descript");
				}
				if(Serializing.GetXmlNodeValue(doc,"ControlType")!=null) {
					ControlType=Serializing.GetXmlNodeValue(doc,"ControlType");
				}
				if(Serializing.GetXmlNodeValue(doc,"ControlLabel")!=null) {
					ControlLabel=Serializing.GetXmlNodeValue(doc,"ControlLabel");
				}
				if(Serializing.GetXmlNodeValue(doc,"ControlOptions")!=null) {
					ControlOptions=Serializing.GetXmlNodeValue(doc,"ControlOptions");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
