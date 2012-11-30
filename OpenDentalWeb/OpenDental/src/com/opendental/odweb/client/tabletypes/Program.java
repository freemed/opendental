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
		public Program Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Program>");
			sb.append("<ProgramNum>").append(ProgramNum).append("</ProgramNum>");
			sb.append("<ProgName>").append(Serializing.EscapeForXml(ProgName)).append("</ProgName>");
			sb.append("<ProgDesc>").append(Serializing.EscapeForXml(ProgDesc)).append("</ProgDesc>");
			sb.append("<Enabled>").append((Enabled)?1:0).append("</Enabled>");
			sb.append("<Path>").append(Serializing.EscapeForXml(Path)).append("</Path>");
			sb.append("<CommandLine>").append(Serializing.EscapeForXml(CommandLine)).append("</CommandLine>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<PluginDllName>").append(Serializing.EscapeForXml(PluginDllName)).append("</PluginDllName>");
			sb.append("</Program>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ProgramNum")!=null) {
					ProgramNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProgramNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProgName")!=null) {
					ProgName=Serializing.GetXmlNodeValue(doc,"ProgName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProgDesc")!=null) {
					ProgDesc=Serializing.GetXmlNodeValue(doc,"ProgDesc");
				}
				if(Serializing.GetXmlNodeValue(doc,"Enabled")!=null) {
					Enabled=(Serializing.GetXmlNodeValue(doc,"Enabled")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"Path")!=null) {
					Path=Serializing.GetXmlNodeValue(doc,"Path");
				}
				if(Serializing.GetXmlNodeValue(doc,"CommandLine")!=null) {
					CommandLine=Serializing.GetXmlNodeValue(doc,"CommandLine");
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"PluginDllName")!=null) {
					PluginDllName=Serializing.GetXmlNodeValue(doc,"PluginDllName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
