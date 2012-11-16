package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class TaskSubscription {
		/** Primary key. */
		public int TaskSubscriptionNum;
		/** FK to userod.UserNum */
		public int UserNum;
		/** FK to tasklist.TaskListNum */
		public int TaskListNum;

		/** Deep copy of object. */
		public TaskSubscription Copy() {
			TaskSubscription tasksubscription=new TaskSubscription();
			tasksubscription.TaskSubscriptionNum=this.TaskSubscriptionNum;
			tasksubscription.UserNum=this.UserNum;
			tasksubscription.TaskListNum=this.TaskListNum;
			return tasksubscription;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TaskSubscription>");
			sb.append("<TaskSubscriptionNum>").append(TaskSubscriptionNum).append("</TaskSubscriptionNum>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<TaskListNum>").append(TaskListNum).append("</TaskListNum>");
			sb.append("</TaskSubscription>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"TaskSubscriptionNum")!=null) {
					TaskSubscriptionNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskSubscriptionNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UserNum"));
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
