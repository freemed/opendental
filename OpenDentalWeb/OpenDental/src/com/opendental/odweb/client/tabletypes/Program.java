package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class Program {
		/** Primary key. */
		public int ProgramNum;
		/** Unique name for built-in program bridges. Not user-editable. enum ProgramName */
		public String ProgName;
		/** Description that shows. */
		public String ProgDesc;
		/** True if enabled. */
		public boolean Enabled;
		/** The path of the executable to run or file to open. */
		public String Path;
		/** Some programs will accept command line arguments. */
		public String CommandLine;
		/** Notes about this program link. Peculiarities, etc. */
		public String Note;
		/** If this is a Plugin, then this is the filename of the dll.  The dll must be located in the application directory. */
		public String PluginDllName;

		/** Deep copy of object. */
		public Program deepCopy() {
			Program program=new Program();
			program.ProgramNum=this.ProgramNum;
			program.ProgName=this.ProgName;
			program.ProgDesc=this.ProgDesc;
			program.Enabled=this.Enabled;
			program.Path=this.Path;
			program.CommandLine=this.CommandLine;
			program.Note=this.Note;
			program.PluginDllName=this.PluginDllName;
			return program;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Program>");
			sb.append("<ProgramNum>").append(ProgramNum).append("</ProgramNum>");
			sb.append("<ProgName>").append(Serializing.escapeForXml(ProgName)).append("</ProgName>");
			sb.append("<ProgDesc>").append(Serializing.escapeForXml(ProgDesc)).append("</ProgDesc>");
			sb.append("<Enabled>").append((Enabled)?1:0).append("</Enabled>");
			sb.append("<Path>").append(Serializing.escapeForXml(Path)).append("</Path>");
			sb.append("<CommandLine>").append(Serializing.escapeForXml(CommandLine)).append("</CommandLine>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<PluginDllName>").append(Serializing.escapeForXml(PluginDllName)).append("</PluginDllName>");
			sb.append("</Program>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ProgramNum")!=null) {
					ProgramNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProgramNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProgName")!=null) {
					ProgName=Serializing.getXmlNodeValue(doc,"ProgName");
				}
				if(Serializing.getXmlNodeValue(doc,"ProgDesc")!=null) {
					ProgDesc=Serializing.getXmlNodeValue(doc,"ProgDesc");
				}
				if(Serializing.getXmlNodeValue(doc,"Enabled")!=null) {
					Enabled=(Serializing.getXmlNodeValue(doc,"Enabled")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Path")!=null) {
					Path=Serializing.getXmlNodeValue(doc,"Path");
				}
				if(Serializing.getXmlNodeValue(doc,"CommandLine")!=null) {
					CommandLine=Serializing.getXmlNodeValue(doc,"CommandLine");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"PluginDllName")!=null) {
					PluginDllName=Serializing.getXmlNodeValue(doc,"PluginDllName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
