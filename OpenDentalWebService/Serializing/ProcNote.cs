using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProcNote {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProcNote procnote) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProcNote>");
			sb.Append("<ProcNoteNum>").Append(procnote.ProcNoteNum).Append("</ProcNoteNum>");
			sb.Append("<PatNum>").Append(procnote.PatNum).Append("</PatNum>");
			sb.Append("<ProcNum>").Append(procnote.ProcNum).Append("</ProcNum>");
			sb.Append("<EntryDateTime>").Append(procnote.EntryDateTime.ToString("yyyyMMddHHmmss")).Append("</EntryDateTime>");
			sb.Append("<UserNum>").Append(procnote.UserNum).Append("</UserNum>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(procnote.Note)).Append("</Note>");
			sb.Append("<SigIsTopaz>").Append((procnote.SigIsTopaz)?1:0).Append("</SigIsTopaz>");
			sb.Append("<Signature>").Append(SerializeStringEscapes.EscapeForXml(procnote.Signature)).Append("</Signature>");
			sb.Append("</ProcNote>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProcNote Deserialize(string xml) {
			OpenDentBusiness.ProcNote procnote=new OpenDentBusiness.ProcNote();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProcNoteNum":
							procnote.ProcNoteNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							procnote.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProcNum":
							procnote.ProcNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "EntryDateTime":
							procnote.EntryDateTime=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "UserNum":
							procnote.UserNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Note":
							procnote.Note=reader.ReadContentAsString();
							break;
						case "SigIsTopaz":
							procnote.SigIsTopaz=reader.ReadContentAsString()!="0";
							break;
						case "Signature":
							procnote.Signature=reader.ReadContentAsString();
							break;
					}
				}
			}
			return procnote;
		}


	}
}