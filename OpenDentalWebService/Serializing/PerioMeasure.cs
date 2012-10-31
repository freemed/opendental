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
							periomeasure.PerioMeasureNum=reader.ReadContentAsLong();
							break;
						case "PerioExamNum":
							periomeasure.PerioExamNum=reader.ReadContentAsLong();
							break;
						case "SequenceType":
							periomeasure.SequenceType=(OpenDentBusiness.PerioSequenceType)reader.ReadContentAsInt();
							break;
						case "IntTooth":
							periomeasure.IntTooth=reader.ReadContentAsInt();
							break;
						case "ToothValue":
							periomeasure.ToothValue=reader.ReadContentAsInt();
							break;
						case "MBvalue":
							periomeasure.MBvalue=reader.ReadContentAsInt();
							break;
						case "Bvalue":
							periomeasure.Bvalue=reader.ReadContentAsInt();
							break;
						case "DBvalue":
							periomeasure.DBvalue=reader.ReadContentAsInt();
							break;
						case "MLvalue":
							periomeasure.MLvalue=reader.ReadContentAsInt();
							break;
						case "Lvalue":
							periomeasure.Lvalue=reader.ReadContentAsInt();
							break;
						case "DLvalue":
							periomeasure.DLvalue=reader.ReadContentAsInt();
							break;
					}
				}
			}
			return periomeasure;
		}


	}
}