package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
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
