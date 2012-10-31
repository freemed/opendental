using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Question {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Question question) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Question>");
			sb.Append("<QuestionNum>").Append(question.QuestionNum).Append("</QuestionNum>");
			sb.Append("<PatNum>").Append(question.PatNum).Append("</PatNum>");
			sb.Append("<ItemOrder>").Append(question.ItemOrder).Append("</ItemOrder>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(question.Description)).Append("</Description>");
			sb.Append("<Answer>").Append(SerializeStringEscapes.EscapeForXml(question.Answer)).Append("</Answer>");
			sb.Append("<FormPatNum>").Append(question.FormPatNum).Append("</FormPatNum>");
			sb.Append("</Question>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Question Deserialize(string xml) {
			OpenDentBusiness.Question question=new OpenDentBusiness.Question();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "QuestionNum":
							question.QuestionNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							question.PatNum=reader.ReadContentAsLong();
							break;
						case "ItemOrder":
							question.ItemOrder=reader.ReadContentAsInt();
							break;
						case "Description":
							question.Description=reader.ReadContentAsString();
							break;
						case "Answer":
							question.Answer=reader.ReadContentAsString();
							break;
						case "FormPatNum":
							question.FormPatNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return question;
		}


	}
}