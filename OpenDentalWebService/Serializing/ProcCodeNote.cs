using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProcCodeNote {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProcCodeNote proccodenote) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProcCodeNote>");
			sb.Append("<ProcCodeNoteNum>").Append(proccodenote.ProcCodeNoteNum).Append("</ProcCodeNoteNum>");
			sb.Append("<CodeNum>").Append(proccodenote.CodeNum).Append("</CodeNum>");
			sb.Append("<ProvNum>").Append(proccodenote.ProvNum).Append("</ProvNum>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(proccodenote.Note)).Append("</Note>");
			sb.Append("<ProcTime>").Append(SerializeStringEscapes.EscapeForXml(proccodenote.ProcTime)).Append("</ProcTime>");
			sb.Append("</ProcCodeNote>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProcCodeNote Deserialize(string xml) {
			OpenDentBusiness.ProcCodeNote proccodenote=new OpenDentBusiness.ProcCodeNote();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProcCodeNoteNum":
							proccodenote.ProcCodeNoteNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CodeNum":
							proccodenote.CodeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProvNum":
							proccodenote.ProvNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Note":
							proccodenote.Note=reader.ReadContentAsString();
							break;
						case "ProcTime":
							proccodenote.ProcTime=reader.ReadContentAsString();
							break;
					}
				}
			}
			return proccodenote;
		}


	}
}