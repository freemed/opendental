package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ProcCodeNoteNum=Integer.valueOf(doc.getElementsByTagName("ProcCodeNoteNum").item(0).getFirstChild().getNodeValue());
				CodeNum=Integer.valueOf(doc.getElementsByTagName("CodeNum").item(0).getFirstChild().getNodeValue());
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				ProcTime=doc.getElementsByTagName("ProcTime").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
