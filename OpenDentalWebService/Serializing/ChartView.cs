using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ChartView {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ChartView chartview) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ChartView>");
			sb.Append("<ChartViewNum>").Append(chartview.ChartViewNum).Append("</ChartViewNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(chartview.Description)).Append("</Description>");
			sb.Append("<ItemOrder>").Append(chartview.ItemOrder).Append("</ItemOrder>");
			sb.Append("<ProcStatuses>").Append((int)chartview.ProcStatuses).Append("</ProcStatuses>");
			sb.Append("<ObjectTypes>").Append((int)chartview.ObjectTypes).Append("</ObjectTypes>");
			sb.Append("<ShowProcNotes>").Append((chartview.ShowProcNotes)?1:0).Append("</ShowProcNotes>");
			sb.Append("<IsAudit>").Append((chartview.IsAudit)?1:0).Append("</IsAudit>");
			sb.Append("<SelectedTeethOnly>").Append((chartview.SelectedTeethOnly)?1:0).Append("</SelectedTeethOnly>");
			sb.Append("<OrionStatusFlags>").Append((int)chartview.OrionStatusFlags).Append("</OrionStatusFlags>");
			sb.Append("<DatesShowing>").Append((int)chartview.DatesShowing).Append("</DatesShowing>");
			sb.Append("</ChartView>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ChartView Deserialize(string xml) {
			OpenDentBusiness.ChartView chartview=new OpenDentBusiness.ChartView();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ChartViewNum":
							chartview.ChartViewNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							chartview.Description=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							chartview.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ProcStatuses":
							chartview.ProcStatuses=(OpenDentBusiness.ChartViewProcStat)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ObjectTypes":
							chartview.ObjectTypes=(OpenDentBusiness.ChartViewObjs)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ShowProcNotes":
							chartview.ShowProcNotes=reader.ReadContentAsString()!="0";
							break;
						case "IsAudit":
							chartview.IsAudit=reader.ReadContentAsString()!="0";
							break;
						case "SelectedTeethOnly":
							chartview.SelectedTeethOnly=reader.ReadContentAsString()!="0";
							break;
						case "OrionStatusFlags":
							chartview.OrionStatusFlags=(OpenDentBusiness.OrionStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DatesShowing":
							chartview.DatesShowing=(OpenDentBusiness.ChartViewDates)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return chartview;
		}


	}
}