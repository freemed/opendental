package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class TaskNote {
		/** Primary key. */
		public int TaskNoteNum;
		/** FK to task. The task this tasknote is attached to. */
		public int TaskNum;
		/** FK to user. The user who created this tasknote. */
		public int UserNum;
		/** Date and time the note was created (editable). */
		public Date DateTimeNote;
		/** Note. Text that the user wishes to show on the task. */
		public String Note;

		/** Deep copy of object. */
		public TaskNote deepCopy() {
			TaskNote tasknote=new TaskNote();
			tasknote.TaskNoteNum=this.TaskNoteNum;
			tasknote.TaskNum=this.TaskNum;
			tasknote.UserNum=this.UserNum;
			tasknote.DateTimeNote=this.DateTimeNote;
			tasknote.Note=this.Note;
			return tasknote;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TaskNote>");
			sb.append("<TaskNoteNum>").append(TaskNoteNum).append("</TaskNoteNum>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<DateTimeNote>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeNote)).append("</DateTimeNote>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("</TaskNote>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"TaskNoteNum")!=null) {
					TaskNoteNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskNoteNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"TaskNum")!=null) {
					TaskNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeNote")!=null) {
					DateTimeNote=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeNote"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing TaskNote: "+e.getMessage());
			}
		}


}
