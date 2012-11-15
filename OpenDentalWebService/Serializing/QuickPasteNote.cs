using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class QuickPasteNote {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.QuickPasteNote quickpastenote) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<QuickPasteNote>");
			sb.Append("<QuickPasteNoteNum>").Append(quickpastenote.QuickPasteNoteNum).Append("</QuickPasteNoteNum>");
			sb.Append("<QuickPasteCatNum>").Append(quickpastenote.QuickPasteCatNum).Append("</QuickPasteCatNum>");
			sb.Append("<ItemOrder>").Append(quickpastenote.ItemOrder).Append("</ItemOrder>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(quickpastenote.Note)).Append("</Note>");
			sb.Append("<Abbreviation>").Append(SerializeStringEscapes.EscapeForXml(quickpastenote.Abbreviation)).Append("</Abbreviation>");
			sb.Append("</QuickPasteNote>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.QuickPasteNote Deserialize(string xml) {
			OpenDentBusiness.QuickPasteNote quickpastenote=new OpenDentBusiness.QuickPasteNote();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "QuickPasteNoteNum":
							quickpastenote.QuickPasteNoteNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "QuickPasteCatNum":
							quickpastenote.QuickPasteCatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ItemOrder":
							quickpastenote.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Note":
							quickpastenote.Note=reader.ReadContentAsString();
							break;
						case "Abbreviation":
							quickpastenote.Abbreviation=reader.ReadContentAsString();
							break;
					}
				}
			}
			return quickpastenote;
		}


	}
}