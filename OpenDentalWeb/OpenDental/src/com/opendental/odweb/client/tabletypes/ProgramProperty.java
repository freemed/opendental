package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ProgramProperty {
		/** Primary key. */
		public int ProgramPropertyNum;
		/** FK to program.ProgramNum */
		public int ProgramNum;
		/** The description or prompt for this property.  Blank for workstation overrides of program path. */
		public String PropertyDesc;
		/** The value.   */
		public String PropertyValue;
		/** The human-readable name of the computer on the network (not the IP address).  Only used when overriding program path.  Blank for typical Program Properties. */
		public String ComputerName;

		/** Deep copy of object. */
		public ProgramProperty Copy() {
			ProgramProperty programproperty=new ProgramProperty();
			programproperty.ProgramPropertyNum=this.ProgramPropertyNum;
			programproperty.ProgramNum=this.ProgramNum;
			programproperty.PropertyDesc=this.PropertyDesc;
			programproperty.PropertyValue=this.PropertyValue;
			programproperty.ComputerName=this.ComputerName;
			return programproperty;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProgramProperty>");
			sb.append("<ProgramPropertyNum>").append(ProgramPropertyNum).append("</ProgramPropertyNum>");
			sb.append("<ProgramNum>").append(ProgramNum).append("</ProgramNum>");
			sb.append("<PropertyDesc>").append(Serializing.EscapeForXml(PropertyDesc)).append("</PropertyDesc>");
			sb.append("<PropertyValue>").append(Serializing.EscapeForXml(PropertyValue)).append("</PropertyValue>");
			sb.append("<ComputerName>").append(Serializing.EscapeForXml(ComputerName)).append("</ComputerName>");
			sb.append("</ProgramProperty>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ProgramPropertyNum")!=null) {
					ProgramPropertyNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProgramPropertyNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProgramNum")!=null) {
					ProgramNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProgramNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PropertyDesc")!=null) {
					PropertyDesc=Serializing.GetXmlNodeValue(doc,"PropertyDesc");
				}
				if(Serializing.GetXmlNodeValue(doc,"PropertyValue")!=null) {
					PropertyValue=Serializing.GetXmlNodeValue(doc,"PropertyValue");
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
