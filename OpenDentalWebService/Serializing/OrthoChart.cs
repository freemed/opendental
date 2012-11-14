using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class OrthoChart {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.OrthoChart orthochart) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<OrthoChart>");
			sb.Append("<OrthoChartNum>").Append(orthochart.OrthoChartNum).Append("</OrthoChartNum>");
			sb.Append("<PatNum>").Append(orthochart.PatNum).Append("</PatNum>");
			sb.Append("<DateService>").Append(orthochart.DateService.ToString()).Append("</DateService>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(orthochart.FieldName)).Append("</FieldName>");
			sb.Append("<FieldValue>").Append(SerializeStringEscapes.EscapeForXml(orthochart.FieldValue)).Append("</FieldValue>");
			sb.Append("</OrthoChart>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.OrthoChart Deserialize(string xml) {
			OpenDentBusiness.OrthoChart orthochart=new OpenDentBusiness.OrthoChart();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "OrthoChartNum":
							orthochart.OrthoChartNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							orthochart.PatNum=reader.ReadContentAsLong();
							break;
						case "DateService":
							orthochart.DateService=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "FieldName":
							orthochart.FieldName=reader.ReadContentAsString();
							break;
						case "FieldValue":
							orthochart.FieldValue=reader.ReadContentAsString();
							break;
					}
				}
			}
			return orthochart;
		}


	}
}