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
			sb.Append("<DateTask>").Append(task.DateTask.ToString("yyyyMMddHHmmss")).Append("</DateTask>");
			sb.Append("<KeyNum>").Append(task.KeyNum).Append("</KeyNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(task.Descript)).Append("</Descript>");
			sb.Append("<TaskStatus>").Append((int)task.TaskStatus).Append("</TaskStatus>");
			sb.Append("<IsRepeating>").Append((task.IsRepeating)?1:0).Append("</IsRepeating>");
			sb.Append("<DateType>").Append((int)task.DateType).Append("</DateType>");
			sb.Append("<FromNum>").Append(task.FromNum).Append("</FromNum>");
			sb.Append("<ObjectType>").Append((int)task.ObjectType).Append("</ObjectType>");
			sb.Append("<DateTimeEntry>").Append(task.DateTimeEntry.ToString("yyyyMMddHHmmss")).Append("</DateTimeEntry>");
			sb.Append("<UserNum>").Append(task.UserNum).Append("</UserNum>");
			sb.Append("<DateTimeFinished>").Append(task.DateTimeFinished.ToString("yyyyMMddHHmmss")).Append("</DateTimeFinished>");
			sb.Append("<IsUnread>").Append((task.IsUnread)?1:0).Append("</IsUnread>");
			sb.Append("<ParentDesc>").Append(SerializeStringEscapes.EscapeForXml(task.ParentDesc)).Append("</ParentDesc>");
			sb.Append("<PatientName>").Append(SerializeStringEscapes.EscapeForXml(task.PatientName)).Append("</PatientName>");
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
							task.TaskNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "TaskListNum":
							task.TaskListNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTask":
							task.DateTask=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "KeyNum":
							task.KeyNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Descript":
							task.Descript=reader.ReadContentAsString();
							break;
						case "TaskStatus":
							task.TaskStatus=(OpenDentBusiness.TaskStatusEnum)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IsRepeating":
							task.IsRepeating=reader.ReadContentAsString()!="0";
							break;
						case "DateType":
							task.DateType=(OpenDentBusiness.TaskDateType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "FromNum":
							task.FromNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ObjectType":
							task.ObjectType=(OpenDentBusiness.TaskObjectType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DateTimeEntry":
							task.DateTimeEntry=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "UserNum":
							task.UserNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTimeFinished":
							task.DateTimeFinished=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "IsUnread":
							task.IsUnread=reader.ReadContentAsString()!="0";
							break;
						case "ParentDesc":
							task.ParentDesc=reader.ReadContentAsString();
							break;
						case "PatientName":
							task.PatientName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return task;
		}


	}
}