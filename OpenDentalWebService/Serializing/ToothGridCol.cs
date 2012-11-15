using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ToothGridCol {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ToothGridCol toothgridcol) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ToothGridCol>");
			sb.Append("<ToothGridColNum>").Append(toothgridcol.ToothGridColNum).Append("</ToothGridColNum>");
			sb.Append("<SheetFieldNum>").Append(toothgridcol.SheetFieldNum).Append("</SheetFieldNum>");
			sb.Append("<NameItem>").Append(SerializeStringEscapes.EscapeForXml(toothgridcol.NameItem)).Append("</NameItem>");
			sb.Append("<CellType>").Append((int)toothgridcol.CellType).Append("</CellType>");
			sb.Append("<ItemOrder>").Append(toothgridcol.ItemOrder).Append("</ItemOrder>");
			sb.Append("<ColumnWidth>").Append(toothgridcol.ColumnWidth).Append("</ColumnWidth>");
			sb.Append("<CodeNum>").Append(toothgridcol.CodeNum).Append("</CodeNum>");
			sb.Append("<ProcStatus>").Append((int)toothgridcol.ProcStatus).Append("</ProcStatus>");
			sb.Append("</ToothGridCol>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ToothGridCol Deserialize(string xml) {
			OpenDentBusiness.ToothGridCol toothgridcol=new OpenDentBusiness.ToothGridCol();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ToothGridColNum":
							toothgridcol.ToothGridColNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SheetFieldNum":
							toothgridcol.SheetFieldNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "NameItem":
							toothgridcol.NameItem=reader.ReadContentAsString();
							break;
						case "CellType":
							toothgridcol.CellType=(OpenDentBusiness.ToothGridCellType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ItemOrder":
							toothgridcol.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ColumnWidth":
							toothgridcol.ColumnWidth=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CodeNum":
							toothgridcol.CodeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProcStatus":
							toothgridcol.ProcStatus=(OpenDentBusiness.ProcStat)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return toothgridcol;
		}


	}
}