package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Computer {
		/** Primary key. */
		public int ComputerNum;
		/** Name of the computer. */
		public String CompName;
		/** Allows use to tell which computers are running.  All workstations record a heartbeat here at an interval of 3 minutes.  And when they shut down, they set this value to min.  So if the heartbeat is fairly fresh, then that's an accurate indicator of whether Open Dental is running on that computer. */
		public Date LastHeartBeat;

		/** Deep copy of object. */
		public Computer Copy() {
			Computer computer=new Computer();
			computer.ComputerNum=this.ComputerNum;
			computer.CompName=this.CompName;
			computer.LastHeartBeat=this.LastHeartBeat;
			return computer;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Computer>");
			sb.append("<ComputerNum>").append(ComputerNum).append("</ComputerNum>");
			sb.append("<CompName>").append(Serializing.EscapeForXml(CompName)).append("</CompName>");
			sb.append("<LastHeartBeat>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</LastHeartBeat>");
			sb.append("</Computer>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ComputerNum=Integer.valueOf(doc.getElementsByTagName("ComputerNum").item(0).getFirstChild().getNodeValue());
				CompName=doc.getElementsByTagName("CompName").item(0).getFirstChild().getNodeValue();
				LastHeartBeat=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("LastHeartBeat").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
