using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class TaskList {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.TaskList tasklist) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<TaskList>");
			sb.Append("<TaskListNum>").Append(tasklist.TaskListNum).Append("</TaskListNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(tasklist.Descript)).Append("</Descript>");
			sb.Append("<Parent>").Append(tasklist.Parent).Append("</Parent>");
			sb.Append("<DateTL>").Append(tasklist.DateTL.ToString("yyyyMMddHHmmss")).Append("</DateTL>");
			sb.Append("<IsRepeating>").Append((tasklist.IsRepeating)?1:0).Append("</IsRepeating>");
			sb.Append("<DateType>").Append((int)tasklist.DateType).Append("</DateType>");
			sb.Append("<FromNum>").Append(tasklist.FromNum).Append("</FromNum>");
			sb.Append("<ObjectType>").Append((int)tasklist.ObjectType).Append("</ObjectType>");
			sb.Append("<DateTimeEntry>").Append(tasklist.DateTimeEntry.ToString("yyyyMMddHHmmss")).Append("</DateTimeEntry>");
			sb.Append("</TaskList>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.TaskList Deserialize(string xml) {
			OpenDentBusiness.TaskList tasklist=new OpenDentBusiness.TaskList();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TaskListNum":
							tasklist.TaskListNum=reader.ReadContentAsLong();
							break;
						case "Descript":
							tasklist.Descript=reader.ReadContentAsString();
							break;
						case "Parent":
							tasklist.Parent=reader.ReadContentAsLong();
							break;
						case "DateTL":
							tasklist.DateTL=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "IsRepeating":
							tasklist.IsRepeating=reader.ReadContentAsString()!="0";
							break;
						case "DateType":
							tasklist.DateType=(OpenDentBusiness.TaskDateType)reader.ReadContentAsInt();
							break;
						case "FromNum":
							tasklist.FromNum=reader.ReadContentAsLong();
							break;
						case "ObjectType":
							tasklist.ObjectType=(OpenDentBusiness.TaskObjectType)reader.ReadContentAsInt();
							break;
						case "DateTimeEntry":
							tasklist.DateTimeEntry=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return tasklist;
		}


	}
}