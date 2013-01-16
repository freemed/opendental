package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public ProgramProperty deepCopy() {
			ProgramProperty programproperty=new ProgramProperty();
			programproperty.ProgramPropertyNum=this.ProgramPropertyNum;
			programproperty.ProgramNum=this.ProgramNum;
			programproperty.PropertyDesc=this.PropertyDesc;
			programproperty.PropertyValue=this.PropertyValue;
			programproperty.ComputerName=this.ComputerName;
			return programproperty;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProgramProperty>");
			sb.append("<ProgramPropertyNum>").append(ProgramPropertyNum).append("</ProgramPropertyNum>");
			sb.append("<ProgramNum>").append(ProgramNum).append("</ProgramNum>");
			sb.append("<PropertyDesc>").append(Serializing.escapeForXml(PropertyDesc)).append("</PropertyDesc>");
			sb.append("<PropertyValue>").append(Serializing.escapeForXml(PropertyValue)).append("</PropertyValue>");
			sb.append("<ComputerName>").append(Serializing.escapeForXml(ComputerName)).append("</ComputerName>");
			sb.append("</ProgramProperty>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ProgramPropertyNum")!=null) {
					ProgramPropertyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProgramPropertyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProgramNum")!=null) {
					ProgramNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProgramNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PropertyDesc")!=null) {
					PropertyDesc=Serializing.getXmlNodeValue(doc,"PropertyDesc");
				}
				if(Serializing.getXmlNodeValue(doc,"PropertyValue")!=null) {
					PropertyValue=Serializing.getXmlNodeValue(doc,"PropertyValue");
				}
				if(Serializing.getXmlNodeValue(doc,"ComputerName")!=null) {
					ComputerName=Serializing.getXmlNodeValue(doc,"ComputerName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
