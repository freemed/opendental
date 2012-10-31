using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProcApptColor {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProcApptColor procapptcolor) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProcApptColor>");
			sb.Append("<ProcApptColorNum>").Append(procapptcolor.ProcApptColorNum).Append("</ProcApptColorNum>");
			sb.Append("<CodeRange>").Append(SerializeStringEscapes.EscapeForXml(procapptcolor.CodeRange)).Append("</CodeRange>");
			sb.Append("<ShowPreviousDate>").Append((procapptcolor.ShowPreviousDate)?1:0).Append("</ShowPreviousDate>");
			sb.Append("<ColorText>").Append(procapptcolor.ColorText.ToArgb()).Append("</ColorText>");
			sb.Append("</ProcApptColor>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProcApptColor Deserialize(string xml) {
			OpenDentBusiness.ProcApptColor procapptcolor=new OpenDentBusiness.ProcApptColor();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProcApptColorNum":
							procapptcolor.ProcApptColorNum=reader.ReadContentAsLong();
							break;
						case "CodeRange":
							procapptcolor.CodeRange=reader.ReadContentAsString();
							break;
						case "ShowPreviousDate":
							procapptcolor.ShowPreviousDate=reader.ReadContentAsString()!="0";
							break;
						case "ColorText":
							procapptcolor.ColorText=Color.FromArgb(reader.ReadContentAsInt());
							break;
					}
				}
			}
			return procapptcolor;
		}


	}
}