using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Task {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Task task) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Task>");
			sb.Append("<TaskNum>").Append(task.TaskNum).Append("</TaskNum>");
			sb.Append("<TaskListNum>").Append(task.TaskListNum).Append("</TaskListNum>");
			sb.Append("<DateTask>").Append(task.DateTask.ToLongDateString()).Append("</DateTask>");
			sb.Append("<KeyNum>").Append(task.KeyNum).Append("</KeyNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(task.Descript)).Append("</Descript>");
			sb.Append("<TaskStatus>").Append((int)task.TaskStatus).Append("</TaskStatus>");
			sb.Append("<IsRepeating>").Append((task.IsRepeating)?1:0).Append("</IsRepeating>");
			sb.Append("<DateType>").Append((int)task.DateType).Append("</DateType>");
			sb.Append("<FromNum>").Append(task.FromNum).Append("</FromNum>");
			sb.Append("<ObjectType>").Append((int)task.ObjectType).Append("</ObjectType>");
			sb.Append("<DateTimeEntry>").Append(task.DateTimeEntry.ToLongDateString()).Append("</DateTimeEntry>");
			sb.Append("<UserNum>").Append(task.UserNum).Append("</UserNum>");
			sb.Append("<DateTimeFinished>").Append(task.DateTimeFinished.ToLongDateString()).Append("</DateTimeFinished>");
			sb.Append("</Task>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Task Deserialize(string xml) {
			OpenDentBusiness.Task task=new OpenDentBusiness.Task();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TaskNum":
							task.TaskNum=reader.ReadContentAsLong();
							break;
						case "TaskListNum":
							task.TaskListNum=reader.ReadContentAsLong();
							break;
						case "DateTask":
							task.DateTask=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "KeyNum":
							task.KeyNum=reader.ReadContentAsLong();
							break;
						case "Descript":
							task.Descript=reader.ReadContentAsString();
							break;
						case "TaskStatus":
							task.TaskStatus=(OpenDentBusiness.TaskStatusEnum)reader.ReadContentAsInt();
							break;
						case "IsRepeating":
							task.IsRepeating=reader.ReadContentAsString()!="0";
							break;
						case "DateType":
							task.DateType=(OpenDentBusiness.TaskDateType)reader.ReadContentAsInt();
							break;
						case "FromNum":
							task.FromNum=reader.ReadContentAsLong();
							break;
						case "ObjectType":
							task.ObjectType=(OpenDentBusiness.TaskObjectType)reader.ReadContentAsInt();
							break;
						case "DateTimeEntry":
							task.DateTimeEntry=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "UserNum":
							task.UserNum=reader.ReadContentAsLong();
							break;
						case "DateTimeFinished":
							task.DateTimeFinished=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return task;
		}


	}
}