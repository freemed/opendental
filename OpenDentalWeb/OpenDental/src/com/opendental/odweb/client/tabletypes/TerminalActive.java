package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public TerminalActive Copy() {
			TerminalActive terminalactive=new TerminalActive();
			terminalactive.TerminalActiveNum=this.TerminalActiveNum;
			terminalactive.ComputerName=this.ComputerName;
			terminalactive.TerminalStatus=this.TerminalStatus;
			terminalactive.PatNum=this.PatNum;
			return terminalactive;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TerminalActive>");
			sb.append("<TerminalActiveNum>").append(TerminalActiveNum).append("</TerminalActiveNum>");
			sb.append("<ComputerName>").append(Serializing.EscapeForXml(ComputerName)).append("</ComputerName>");
			sb.append("<TerminalStatus>").append(TerminalStatus.ordinal()).append("</TerminalStatus>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("</TerminalActive>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"TerminalActiveNum")!=null) {
					TerminalActiveNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TerminalActiveNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ComputerName")!=null) {
					ComputerName=Serializing.GetXmlNodeValue(doc,"ComputerName");
				}
				if(Serializing.GetXmlNodeValue(doc,"TerminalStatus")!=null) {
					TerminalStatus=TerminalStatusEnum.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TerminalStatus"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
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
