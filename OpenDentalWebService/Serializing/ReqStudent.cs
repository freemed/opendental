using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ReqStudent {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ReqStudent reqstudent) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ReqStudent>");
			sb.Append("<ReqStudentNum>").Append(reqstudent.ReqStudentNum).Append("</ReqStudentNum>");
			sb.Append("<ReqNeededNum>").Append(reqstudent.ReqNeededNum).Append("</ReqNeededNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(reqstudent.Descript)).Append("</Descript>");
			sb.Append("<SchoolCourseNum>").Append(reqstudent.SchoolCourseNum).Append("</SchoolCourseNum>");
			sb.Append("<ProvNum>").Append(reqstudent.ProvNum).Append("</ProvNum>");
			sb.Append("<AptNum>").Append(reqstudent.AptNum).Append("</AptNum>");
			sb.Append("<PatNum>").Append(reqstudent.PatNum).Append("</PatNum>");
			sb.Append("<InstructorNum>").Append(reqstudent.InstructorNum).Append("</InstructorNum>");
			sb.Append("<DateCompleted>").Append(reqstudent.DateCompleted.ToString("yyyyMMddHHmmss")).Append("</DateCompleted>");
			sb.Append("</ReqStudent>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ReqStudent Deserialize(string xml) {
			OpenDentBusiness.ReqStudent reqstudent=new OpenDentBusiness.ReqStudent();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ReqStudentNum":
							reqstudent.ReqStudentNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ReqNeededNum":
							reqstudent.ReqNeededNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Descript":
							reqstudent.Descript=reader.ReadContentAsString();
							break;
						case "SchoolCourseNum":
							reqstudent.SchoolCourseNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProvNum":
							reqstudent.ProvNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AptNum":
							reqstudent.AptNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							reqstudent.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "InstructorNum":
							reqstudent.InstructorNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateCompleted":
							reqstudent.DateCompleted=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return reqstudent;
		}


	}
}