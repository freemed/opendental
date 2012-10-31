using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SchoolCourse {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SchoolCourse schoolcourse) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SchoolCourse>");
			sb.Append("<SchoolCourseNum>").Append(schoolcourse.SchoolCourseNum).Append("</SchoolCourseNum>");
			sb.Append("<CourseID>").Append(SerializeStringEscapes.EscapeForXml(schoolcourse.CourseID)).Append("</CourseID>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(schoolcourse.Descript)).Append("</Descript>");
			sb.Append("</SchoolCourse>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SchoolCourse Deserialize(string xml) {
			OpenDentBusiness.SchoolCourse schoolcourse=new OpenDentBusiness.SchoolCourse();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SchoolCourseNum":
							schoolcourse.SchoolCourseNum=reader.ReadContentAsLong();
							break;
						case "CourseID":
							schoolcourse.CourseID=reader.ReadContentAsString();
							break;
						case "Descript":
							schoolcourse.Descript=reader.ReadContentAsString();
							break;
					}
				}
			}
			return schoolcourse;
		}


	}
}