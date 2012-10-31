using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ToothGridDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ToothGridDef toothgriddef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ToothGridDef>");
			sb.Append("<ToothGridDefNum>").Append(toothgriddef.ToothGridDefNum).Append("</ToothGridDefNum>");
			sb.Append("<NameInternal>").Append(SerializeStringEscapes.EscapeForXml(toothgriddef.NameInternal)).Append("</NameInternal>");
			sb.Append("<NameShowing>").Append(SerializeStringEscapes.EscapeForXml(toothgriddef.NameShowing)).Append("</NameShowing>");
			sb.Append("<CellType>").Append((int)toothgriddef.CellType).Append("</CellType>");
			sb.Append("<ItemOrder>").Append(toothgriddef.ItemOrder).Append("</ItemOrder>");
			sb.Append("<ColumnWidth>").Append(toothgriddef.ColumnWidth).Append("</ColumnWidth>");
			sb.Append("<CodeNum>").Append(toothgriddef.CodeNum).Append("</CodeNum>");
			sb.Append("<ProcStatus>").Append((int)toothgriddef.ProcStatus).Append("</ProcStatus>");
			sb.Append("</ToothGridDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ToothGridDef Deserialize(string xml) {
			OpenDentBusiness.ToothGridDef toothgriddef=new OpenDentBusiness.ToothGridDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ToothGridDefNum":
							toothgriddef.ToothGridDefNum=reader.ReadContentAsLong();
							break;
						case "NameInternal":
							toothgriddef.NameInternal=reader.ReadContentAsString();
							break;
						case "NameShowing":
							toothgriddef.NameShowing=reader.ReadContentAsString();
							break;
						case "CellType":
							toothgriddef.CellType=(OpenDentBusiness.ToothGridCellType)reader.ReadContentAsInt();
							break;
						case "ItemOrder":
							toothgriddef.ItemOrder=reader.ReadContentAsInt();
							break;
						case "ColumnWidth":
							toothgriddef.ColumnWidth=reader.ReadContentAsInt();
							break;
						case "CodeNum":
							toothgriddef.CodeNum=reader.ReadContentAsLong();
							break;
						case "ProcStatus":
							toothgriddef.ProcStatus=(OpenDentBusiness.ProcStat)reader.ReadContentAsInt();
							break;
					}
				}
			}
			return toothgriddef;
		}


	}
}