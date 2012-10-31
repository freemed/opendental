using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ToothGridCell {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ToothGridCell toothgridcell) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ToothGridCell>");
			sb.Append("<ToothGridCellNum>").Append(toothgridcell.ToothGridCellNum).Append("</ToothGridCellNum>");
			sb.Append("<SheetFieldNum>").Append(toothgridcell.SheetFieldNum).Append("</SheetFieldNum>");
			sb.Append("<ToothGridColNum>").Append(toothgridcell.ToothGridColNum).Append("</ToothGridColNum>");
			sb.Append("<ValueEntered>").Append(SerializeStringEscapes.EscapeForXml(toothgridcell.ValueEntered)).Append("</ValueEntered>");
			sb.Append("<ToothNum>").Append(SerializeStringEscapes.EscapeForXml(toothgridcell.ToothNum)).Append("</ToothNum>");
			sb.Append("</ToothGridCell>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ToothGridCell Deserialize(string xml) {
			OpenDentBusiness.ToothGridCell toothgridcell=new OpenDentBusiness.ToothGridCell();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ToothGridCellNum":
							toothgridcell.ToothGridCellNum=reader.ReadContentAsLong();
							break;
						case "SheetFieldNum":
							toothgridcell.SheetFieldNum=reader.ReadContentAsLong();
							break;
						case "ToothGridColNum":
							toothgridcell.ToothGridColNum=reader.ReadContentAsLong();
							break;
						case "ValueEntered":
							toothgridcell.ValueEntered=reader.ReadContentAsString();
							break;
						case "ToothNum":
							toothgridcell.ToothNum=reader.ReadContentAsString();
							break;
					}
				}
			}
			return toothgridcell;
		}


	}
}