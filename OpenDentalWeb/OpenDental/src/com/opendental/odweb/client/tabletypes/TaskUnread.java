package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class TaskUnread {
		/** Primary key. */
		public int TaskUnreadNum;
		/** FK to task.TaskNum. */
		public int TaskNum;
		/** FK to userod.UserNum. */
		public int UserNum;

		/** Deep copy of object. */
		public TaskUnread Copy() {
			TaskUnread taskunread=new TaskUnread();
			taskunread.TaskUnreadNum=this.TaskUnreadNum;
			taskunread.TaskNum=this.TaskNum;
			taskunread.UserNum=this.UserNum;
			return taskunread;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TaskUnread>");
			sb.append("<TaskUnreadNum>").append(TaskUnreadNum).append("</TaskUnreadNum>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("</TaskUnread>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"TaskUnreadNum")!=null) {
					TaskUnreadNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskUnreadNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskNum")!=null) {
					TaskNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UserNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
