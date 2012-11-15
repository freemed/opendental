package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class ProcNote {
		/** Primary key. */
		public int ProcNoteNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** FK to procedurelog.ProcNum */
		public int ProcNum;
		/** The server time that this note was entered. Essentially a timestamp. */
		public Date EntryDateTime;
		/** FK to userod.UserNum. */
		public int UserNum;
		/** The actual note. */
		public String Note;
		/** There are two kinds of signatures.  Topaz signatures use hardware manufactured by that company, and the signature is created by their library.  OD signatures work exactly the same way, but are only for on-screen signing. */
		public boolean SigIsTopaz;
		/** The encrypted signature.  A signature starts as a collection of vectors.  The Topaz .sig file format is proprietary.  The OD signature format looks like this: 45,68;48,70;49,72;0,0;55,88;etc.  It's simply a sequence of points, separated by semicolons.  0,0 represents pen up.  Then, a hash is created from the Note, concatenated directly with the userNum.  For example, "This is a note3" gets turned into a hash of 2849283940385391 (16 bytes).  The hash is used to encrypt the signature data string using symmetric encryption.  Therefore, the actual signature cannot be retrieved from the database by ordinary means.  Also, the signature info cannot even be retrieved by Open Dental at all unless it supplies the same hash as before, proving that the data has not changed since signed.  If OD supplies the correct hash, then it will be able to extract the sequence of vectors which it will then use to display the signature.  The OD sigs are not compressed, and the Topaz sigs are.  But there is very little difference in their sizes.  It would be very rare for a signature to be larger than 1000 bytes. */
		public String Signature;

		/** Deep copy of object. */
		public ProcNote Copy() {
			ProcNote procnote=new ProcNote();
			procnote.ProcNoteNum=this.ProcNoteNum;
			procnote.PatNum=this.PatNum;
			procnote.ProcNum=this.ProcNum;
			procnote.EntryDateTime=this.EntryDateTime;
			procnote.UserNum=this.UserNum;
			procnote.Note=this.Note;
			procnote.SigIsTopaz=this.SigIsTopaz;
			procnote.Signature=this.Signature;
			return procnote;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcNote>");
			sb.append("<ProcNoteNum>").append(ProcNoteNum).append("</ProcNoteNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<EntryDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</EntryDateTime>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<SigIsTopaz>").append((SigIsTopaz)?1:0).append("</SigIsTopaz>");
			sb.append("<Signature>").append(Serializing.EscapeForXml(Signature)).append("</Signature>");
			sb.append("</ProcNote>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ProcNoteNum=Integer.valueOf(doc.getElementsByTagName("ProcNoteNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ProcNum=Integer.valueOf(doc.getElementsByTagName("ProcNum").item(0).getFirstChild().getNodeValue());
				EntryDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("EntryDateTime").item(0).getFirstChild().getNodeValue());
				UserNum=Integer.valueOf(doc.getElementsByTagName("UserNum").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				SigIsTopaz=(doc.getElementsByTagName("SigIsTopaz").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Signature=doc.getElementsByTagName("Signature").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
