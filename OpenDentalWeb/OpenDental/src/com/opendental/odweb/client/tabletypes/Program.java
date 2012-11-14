package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ProgramNum=Integer.valueOf(doc.getElementsByTagName("ProgramNum").item(0).getFirstChild().getNodeValue());
				ProgName=doc.getElementsByTagName("ProgName").item(0).getFirstChild().getNodeValue();
				ProgDesc=doc.getElementsByTagName("ProgDesc").item(0).getFirstChild().getNodeValue();
				Enabled=(doc.getElementsByTagName("Enabled").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Path=doc.getElementsByTagName("Path").item(0).getFirstChild().getNodeValue();
				CommandLine=doc.getElementsByTagName("CommandLine").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				PluginDllName=doc.getElementsByTagName("PluginDllName").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
