package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class TaskSubscription {
		/** Primary key. */
		public int TaskSubscriptionNum;
		/** FK to userod.UserNum */
		public int UserNum;
		/** FK to tasklist.TaskListNum */
		public int TaskListNum;

		/** Deep copy of object. */
		public TaskSubscription deepCopy() {
			TaskSubscription tasksubscription=new TaskSubscription();
			tasksubscription.TaskSubscriptionNum=this.TaskSubscriptionNum;
			tasksubscription.UserNum=this.UserNum;
			tasksubscription.TaskListNum=this.TaskListNum;
			return tasksubscription;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TaskSubscription>");
			sb.append("<TaskSubscriptionNum>").append(TaskSubscriptionNum).append("</TaskSubscriptionNum>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<TaskListNum>").append(TaskListNum).append("</TaskListNum>");
			sb.append("</TaskSubscription>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"TaskSubscriptionNum")!=null) {
					TaskSubscriptionNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskSubscriptionNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"TaskListNum")!=null) {
					TaskListNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskListNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing TaskSubscription: "+e.getMessage());
			}
		}


}
