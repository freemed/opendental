using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Adjustment {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Adjustment adjustment) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Adjustment>");
			sb.Append("<AdjNum>").Append(adjustment.AdjNum).Append("</AdjNum>");
			sb.Append("<AdjDate>").Append(adjustment.AdjDate.ToString("yyyyMMddHHmmss")).Append("</AdjDate>");
			sb.Append("<AdjAmt>").Append(adjustment.AdjAmt).Append("</AdjAmt>");
			sb.Append("<PatNum>").Append(adjustment.PatNum).Append("</PatNum>");
			sb.Append("<AdjType>").Append(adjustment.AdjType).Append("</AdjType>");
			sb.Append("<ProvNum>").Append(adjustment.ProvNum).Append("</ProvNum>");
			sb.Append("<AdjNote>").Append(SerializeStringEscapes.EscapeForXml(adjustment.AdjNote)).Append("</AdjNote>");
			sb.Append("<ProcDate>").Append(adjustment.ProcDate.ToString("yyyyMMddHHmmss")).Append("</ProcDate>");
			sb.Append("<ProcNum>").Append(adjustment.ProcNum).Append("</ProcNum>");
			sb.Append("<DateEntry>").Append(adjustment.DateEntry.ToString("yyyyMMddHHmmss")).Append("</DateEntry>");
			sb.Append("<ClinicNum>").Append(adjustment.ClinicNum).Append("</ClinicNum>");
			sb.Append("<StatementNum>").Append(adjustment.StatementNum).Append("</StatementNum>");
			sb.Append("</Adjustment>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Adjustment Deserialize(string xml) {
			OpenDentBusiness.Adjustment adjustment=new OpenDentBusiness.Adjustment();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AdjNum":
							adjustment.AdjNum=reader.ReadContentAsLong();
							break;
						case "AdjDate":
							adjustment.AdjDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "AdjAmt":
							adjustment.AdjAmt=reader.ReadContentAsDouble();
							break;
						case "PatNum":
							adjustment.PatNum=reader.ReadContentAsLong();
							break;
						case "AdjType":
							adjustment.AdjType=reader.ReadContentAsLong();
							break;
						case "ProvNum":
							adjustment.ProvNum=reader.ReadContentAsLong();
							break;
						case "AdjNote":
							adjustment.AdjNote=reader.ReadContentAsString();
							break;
						case "ProcDate":
							adjustment.ProcDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ProcNum":
							adjustment.ProcNum=reader.ReadContentAsLong();
							break;
						case "DateEntry":
							adjustment.DateEntry=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ClinicNum":
							adjustment.ClinicNum=reader.ReadContentAsLong();
							break;
						case "StatementNum":
							adjustment.StatementNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return adjustment;
		}


	}
}