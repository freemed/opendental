using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class TaskUnread {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.TaskUnread taskunread) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<TaskUnread>");
			sb.Append("<TaskUnreadNum>").Append(taskunread.TaskUnreadNum).Append("</TaskUnreadNum>");
			sb.Append("<TaskNum>").Append(taskunread.TaskNum).Append("</TaskNum>");
			sb.Append("<UserNum>").Append(taskunread.UserNum).Append("</UserNum>");
			sb.Append("</TaskUnread>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.TaskUnread Deserialize(string xml) {
			OpenDentBusiness.TaskUnread taskunread=new OpenDentBusiness.TaskUnread();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TaskUnreadNum":
							taskunread.TaskUnreadNum=reader.ReadContentAsLong();
							break;
						case "TaskNum":
							taskunread.TaskNum=reader.ReadContentAsLong();
							break;
						case "UserNum":
							taskunread.UserNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return taskunread;
		}


	}
}