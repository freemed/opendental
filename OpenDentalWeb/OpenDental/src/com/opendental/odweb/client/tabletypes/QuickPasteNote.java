package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class QuickPasteNote {
		/** Primary key. */
		public int QuickPasteNoteNum;
		/** FK to quickpastecat.QuickPasteCatNum.  Keeps track of which category this note is in. */
		public int QuickPasteCatNum;
		/** The order of this note within it's category. 0-based. */
		public int ItemOrder;
		/** The actual note. Can be multiple lines and possibly very long. */
		public String Note;
		/** The abbreviation which will automatically substitute when preceded by a ?. */
		public String Abbreviation;

		/** Deep copy of object. */
		public QuickPasteNote Copy() {
			QuickPasteNote quickpastenote=new QuickPasteNote();
			quickpastenote.QuickPasteNoteNum=this.QuickPasteNoteNum;
			quickpastenote.QuickPasteCatNum=this.QuickPasteCatNum;
			quickpastenote.ItemOrder=this.ItemOrder;
			quickpastenote.Note=this.Note;
			quickpastenote.Abbreviation=this.Abbreviation;
			return quickpastenote;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<QuickPasteNote>");
			sb.append("<QuickPasteNoteNum>").append(QuickPasteNoteNum).append("</QuickPasteNoteNum>");
			sb.append("<QuickPasteCatNum>").append(QuickPasteCatNum).append("</QuickPasteCatNum>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<Abbreviation>").append(Serializing.EscapeForXml(Abbreviation)).append("</Abbreviation>");
			sb.append("</QuickPasteNote>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"QuickPasteNoteNum")!=null) {
					QuickPasteNoteNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"QuickPasteNoteNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"QuickPasteCatNum")!=null) {
					QuickPasteCatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"QuickPasteCatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"Abbreviation")!=null) {
					Abbreviation=Serializing.GetXmlNodeValue(doc,"Abbreviation");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
