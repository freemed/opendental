package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class ProcCodeNote {
		/** Primary Key. */
		public int ProcCodeNoteNum;
		/** FK to procedurecode.CodeNum. */
		public int CodeNum;
		/** FK to provider.ProvNum. */
		public int ProvNum;
		/** The note. */
		public String Note;
		/** X's and /'s describe Dr's time and assistant's time in the same increments as the user has set. */
		public String ProcTime;

		/** Deep copy of object. */
		public ProcCodeNote Copy() {
			ProcCodeNote proccodenote=new ProcCodeNote();
			proccodenote.ProcCodeNoteNum=this.ProcCodeNoteNum;
			proccodenote.CodeNum=this.CodeNum;
			proccodenote.ProvNum=this.ProvNum;
			proccodenote.Note=this.Note;
			proccodenote.ProcTime=this.ProcTime;
			return proccodenote;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcCodeNote>");
			sb.append("<ProcCodeNoteNum>").append(ProcCodeNoteNum).append("</ProcCodeNoteNum>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<ProcTime>").append(Serializing.EscapeForXml(ProcTime)).append("</ProcTime>");
			sb.append("</ProcCodeNote>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ProcCodeNoteNum")!=null) {
					ProcCodeNoteNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcCodeNoteNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CodeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcTime")!=null) {
					ProcTime=Serializing.GetXmlNodeValue(doc,"ProcTime");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
