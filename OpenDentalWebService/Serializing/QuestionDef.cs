using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class QuestionDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.QuestionDef questiondef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<QuestionDef>");
			sb.Append("<QuestionDefNum>").Append(questiondef.QuestionDefNum).Append("</QuestionDefNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(questiondef.Description)).Append("</Description>");
			sb.Append("<ItemOrder>").Append(questiondef.ItemOrder).Append("</ItemOrder>");
			sb.Append("<QuestType>").Append((int)questiondef.QuestType).Append("</QuestType>");
			sb.Append("</QuestionDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.QuestionDef Deserialize(string xml) {
			OpenDentBusiness.QuestionDef questiondef=new OpenDentBusiness.QuestionDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "QuestionDefNum":
							questiondef.QuestionDefNum=reader.ReadContentAsLong();
							break;
						case "Description":
							questiondef.Description=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							questiondef.ItemOrder=reader.ReadContentAsInt();
							break;
						case "QuestType":
							questiondef.QuestType=(OpenDentBusiness.QuestionType)reader.ReadContentAsInt();
							break;
					}
				}
			}
			return questiondef;
		}


	}
}