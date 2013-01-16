package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class TaskAncestor {
		/** Primary key. */
		public int TaskAncestorNum;
		/** FK to task.TaskNum */
		public int TaskNum;
		/** FK to tasklist.TaskListNum */
		public int TaskListNum;

		/** Deep copy of object. */
		public TaskAncestor deepCopy() {
			TaskAncestor taskancestor=new TaskAncestor();
			taskancestor.TaskAncestorNum=this.TaskAncestorNum;
			taskancestor.TaskNum=this.TaskNum;
			taskancestor.TaskListNum=this.TaskListNum;
			return taskancestor;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TaskAncestor>");
			sb.append("<TaskAncestorNum>").append(TaskAncestorNum).append("</TaskAncestorNum>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("<TaskListNum>").append(TaskListNum).append("</TaskListNum>");
			sb.append("</TaskAncestor>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"TaskAncestorNum")!=null) {
					TaskAncestorNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskAncestorNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"TaskNum")!=null) {
					TaskNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"TaskListNum")!=null) {
					TaskListNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskListNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
