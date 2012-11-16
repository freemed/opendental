package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class TaskAncestor {
		/** Primary key. */
		public int TaskAncestorNum;
		/** FK to task.TaskNum */
		public int TaskNum;
		/** FK to tasklist.TaskListNum */
		public int TaskListNum;

		/** Deep copy of object. */
		public TaskAncestor Copy() {
			TaskAncestor taskancestor=new TaskAncestor();
			taskancestor.TaskAncestorNum=this.TaskAncestorNum;
			taskancestor.TaskNum=this.TaskNum;
			taskancestor.TaskListNum=this.TaskListNum;
			return taskancestor;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TaskAncestor>");
			sb.append("<TaskAncestorNum>").append(TaskAncestorNum).append("</TaskAncestorNum>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("<TaskListNum>").append(TaskListNum).append("</TaskListNum>");
			sb.append("</TaskAncestor>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"TaskAncestorNum")!=null) {
					TaskAncestorNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskAncestorNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskNum")!=null) {
					TaskNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskListNum")!=null) {
					TaskListNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskListNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
