using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PerioMeasure {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PerioMeasure periomeasure) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PerioMeasure>");
			sb.Append("<PerioMeasureNum>").Append(periomeasure.PerioMeasureNum).Append("</PerioMeasureNum>");
			sb.Append("<PerioExamNum>").Append(periomeasure.PerioExamNum).Append("</PerioExamNum>");
			sb.Append("<SequenceType>").Append((int)periomeasure.SequenceType).Append("</SequenceType>");
			sb.Append("<IntTooth>").Append(periomeasure.IntTooth).Append("</IntTooth>");
			sb.Append("<ToothValue>").Append(periomeasure.ToothValue).Append("</ToothValue>");
			sb.Append("<MBvalue>").Append(periomeasure.MBvalue).Append("</MBvalue>");
			sb.Append("<Bvalue>").Append(periomeasure.Bvalue).Append("</Bvalue>");
			sb.Append("<DBvalue>").Append(periomeasure.DBvalue).Append("</DBvalue>");
			sb.Append("<MLvalue>").Append(periomeasure.MLvalue).Append("</MLvalue>");
			sb.Append("<Lvalue>").Append(periomeasure.Lvalue).Append("</Lvalue>");
			sb.Append("<DLvalue>").Append(periomeasure.DLvalue).Append("</DLvalue>");
			sb.Append("</PerioMeasure>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PerioMeasure Deserialize(string xml) {
			OpenDentBusiness.PerioMeasure periomeasure=new OpenDentBusiness.PerioMeasure();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PerioMeasureNum":
							periomeasure.PerioMeasureNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PerioExamNum":
							periomeasure.PerioExamNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SequenceType":
							periomeasure.SequenceType=(OpenDentBusiness.PerioSequenceType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IntTooth":
							periomeasure.IntTooth=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ToothValue":
							periomeasure.ToothValue=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "MBvalue":
							periomeasure.MBvalue=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Bvalue":
							periomeasure.Bvalue=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DBvalue":
							periomeasure.DBvalue=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "MLvalue":
							periomeasure.MLvalue=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Lvalue":
							periomeasure.Lvalue=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DLvalue":
							periomeasure.DLvalue=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return periomeasure;
		}


	}
}