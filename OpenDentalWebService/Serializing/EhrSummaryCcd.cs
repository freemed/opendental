using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EhrSummaryCcd {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EhrSummaryCcd ehrsummaryccd) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EhrSummaryCcd>");
			sb.Append("<EhrSummaryCcdNum>").Append(ehrsummaryccd.EhrSummaryCcdNum).Append("</EhrSummaryCcdNum>");
			sb.Append("<PatNum>").Append(ehrsummaryccd.PatNum).Append("</PatNum>");
			sb.Append("<DateSummary>").Append(ehrsummaryccd.DateSummary.ToString("yyyyMMddHHmmss")).Append("</DateSummary>");
			sb.Append("<ContentSummary>").Append(SerializeStringEscapes.EscapeForXml(ehrsummaryccd.ContentSummary)).Append("</ContentSummary>");
			sb.Append("</EhrSummaryCcd>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EhrSummaryCcd Deserialize(string xml) {
			OpenDentBusiness.EhrSummaryCcd ehrsummaryccd=new OpenDentBusiness.EhrSummaryCcd();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EhrSummaryCcdNum":
							ehrsummaryccd.EhrSummaryCcdNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							ehrsummaryccd.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateSummary":
							ehrsummaryccd.DateSummary=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ContentSummary":
							ehrsummaryccd.ContentSummary=reader.ReadContentAsString();
							break;
					}
				}
			}
			return ehrsummaryccd;
		}


	}
}