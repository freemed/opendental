package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class TerminalActive {
		/** Primary key. */
		public int TerminalActiveNum;
		/** The name of the computer where the terminal is active. */
		public String ComputerName;
		/** Enum:TerminalStatusEnum  No longer used.  Instead, the PatNum field is used.  Used to indicates at what point the patient was in the sequence. 0=standby, 1=PatientInfo, 2=Medical, 3=UpdateOnly.  If status is 1, then nobody else on the network could open the patient edit window for that patient. */
		public TerminalStatusEnum TerminalStatus;
		/** FK to patient.PatNum.  The patient currently showing in the terminal.  If 0, then terminal is in standby mode. */
		public int PatNum;

		/** Deep copy of object. */
		public TerminalActive deepCopy() {
			TerminalActive terminalactive=new TerminalActive();
			terminalactive.TerminalActiveNum=this.TerminalActiveNum;
			terminalactive.ComputerName=this.ComputerName;
			terminalactive.TerminalStatus=this.TerminalStatus;
			terminalactive.PatNum=this.PatNum;
			return terminalactive;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TerminalActive>");
			sb.append("<TerminalActiveNum>").append(TerminalActiveNum).append("</TerminalActiveNum>");
			sb.append("<ComputerName>").append(Serializing.escapeForXml(ComputerName)).append("</ComputerName>");
			sb.append("<TerminalStatus>").append(TerminalStatus.ordinal()).append("</TerminalStatus>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("</TerminalActive>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"TerminalActiveNum")!=null) {
					TerminalActiveNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TerminalActiveNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ComputerName")!=null) {
					ComputerName=Serializing.getXmlNodeValue(doc,"ComputerName");
				}
				if(Serializing.getXmlNodeValue(doc,"TerminalStatus")!=null) {
					TerminalStatus=TerminalStatusEnum.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"TerminalStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Indicates at what point the patient is in the sequence. 0=standby, 1=PatientInfo, 2=Medical, 3=UpdateOnly. */
		public enum TerminalStatusEnum {
			/** 0 */
			Standby,
			/** 1 */
			PatientInfo,
			/** 2 */
			Medical,
			/** 3. Only the patient info tab will be visible.  This is just to let patient up date their address and phone number. */
			UpdateOnly
		}


}
