package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
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
