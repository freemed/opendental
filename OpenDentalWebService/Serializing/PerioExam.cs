using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PerioExam {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PerioExam perioexam) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PerioExam>");
			sb.Append("<PerioExamNum>").Append(perioexam.PerioExamNum).Append("</PerioExamNum>");
			sb.Append("<PatNum>").Append(perioexam.PatNum).Append("</PatNum>");
			sb.Append("<ExamDate>").Append(perioexam.ExamDate.ToLongDateString()).Append("</ExamDate>");
			sb.Append("<ProvNum>").Append(perioexam.ProvNum).Append("</ProvNum>");
			sb.Append("</PerioExam>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PerioExam Deserialize(string xml) {
			OpenDentBusiness.PerioExam perioexam=new OpenDentBusiness.PerioExam();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PerioExamNum":
							perioexam.PerioExamNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							perioexam.PatNum=reader.ReadContentAsLong();
							break;
						case "ExamDate":
							perioexam.ExamDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ProvNum":
							perioexam.ProvNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return perioexam;
		}


	}
}