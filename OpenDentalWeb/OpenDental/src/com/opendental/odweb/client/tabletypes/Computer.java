package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Computer {
		/** Primary key. */
		public int ComputerNum;
		/** Name of the computer. */
		public String CompName;
		/** Allows use to tell which computers are running.  All workstations record a heartbeat here at an interval of 3 minutes.  And when they shut down, they set this value to min.  So if the heartbeat is fairly fresh, then that's an accurate indicator of whether Open Dental is running on that computer. */
		public Date LastHeartBeat;

		/** Deep copy of object. */
		public Computer deepCopy() {
			Computer computer=new Computer();
			computer.ComputerNum=this.ComputerNum;
			computer.CompName=this.CompName;
			computer.LastHeartBeat=this.LastHeartBeat;
			return computer;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Computer>");
			sb.append("<ComputerNum>").append(ComputerNum).append("</ComputerNum>");
			sb.append("<CompName>").append(Serializing.escapeForXml(CompName)).append("</CompName>");
			sb.append("<LastHeartBeat>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(LastHeartBeat)).append("</LastHeartBeat>");
			sb.append("</Computer>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ComputerNum")!=null) {
					ComputerNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ComputerNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CompName")!=null) {
					CompName=Serializing.getXmlNodeValue(doc,"CompName");
				}
				if(Serializing.getXmlNodeValue(doc,"LastHeartBeat")!=null) {
					LastHeartBeat=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"LastHeartBeat"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Computer: "+e.getMessage());
			}
		}


}
