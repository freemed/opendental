using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class TaskSubscription {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.TaskSubscription tasksubscription) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<TaskSubscription>");
			sb.Append("<TaskSubscriptionNum>").Append(tasksubscription.TaskSubscriptionNum).Append("</TaskSubscriptionNum>");
			sb.Append("<UserNum>").Append(tasksubscription.UserNum).Append("</UserNum>");
			sb.Append("<TaskListNum>").Append(tasksubscription.TaskListNum).Append("</TaskListNum>");
			sb.Append("</TaskSubscription>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.TaskSubscription Deserialize(string xml) {
			OpenDentBusiness.TaskSubscription tasksubscription=new OpenDentBusiness.TaskSubscription();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TaskSubscriptionNum":
							tasksubscription.TaskSubscriptionNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "UserNum":
							tasksubscription.UserNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "TaskListNum":
							tasksubscription.TaskListNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return tasksubscription;
		}


	}
}