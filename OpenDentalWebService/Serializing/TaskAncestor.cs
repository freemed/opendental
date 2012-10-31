using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class TaskAncestor {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.TaskAncestor taskancestor) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<TaskAncestor>");
			sb.Append("<TaskAncestorNum>").Append(taskancestor.TaskAncestorNum).Append("</TaskAncestorNum>");
			sb.Append("<TaskNum>").Append(taskancestor.TaskNum).Append("</TaskNum>");
			sb.Append("<TaskListNum>").Append(taskancestor.TaskListNum).Append("</TaskListNum>");
			sb.Append("</TaskAncestor>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.TaskAncestor Deserialize(string xml) {
			OpenDentBusiness.TaskAncestor taskancestor=new OpenDentBusiness.TaskAncestor();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TaskAncestorNum":
							taskancestor.TaskAncestorNum=reader.ReadContentAsLong();
							break;
						case "TaskNum":
							taskancestor.TaskNum=reader.ReadContentAsLong();
							break;
						case "TaskListNum":
							taskancestor.TaskListNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return taskancestor;
		}


	}
}