using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class CovSpan {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.CovSpan covspan) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<CovSpan>");
			sb.Append("<CovSpanNum>").Append(covspan.CovSpanNum).Append("</CovSpanNum>");
			sb.Append("<CovCatNum>").Append(covspan.CovCatNum).Append("</CovCatNum>");
			sb.Append("<FromCode>").Append(SerializeStringEscapes.EscapeForXml(covspan.FromCode)).Append("</FromCode>");
			sb.Append("<ToCode>").Append(SerializeStringEscapes.EscapeForXml(covspan.ToCode)).Append("</ToCode>");
			sb.Append("</CovSpan>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.CovSpan Deserialize(string xml) {
			OpenDentBusiness.CovSpan covspan=new OpenDentBusiness.CovSpan();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CovSpanNum":
							covspan.CovSpanNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CovCatNum":
							covspan.CovCatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FromCode":
							covspan.FromCode=reader.ReadContentAsString();
							break;
						case "ToCode":
							covspan.ToCode=reader.ReadContentAsString();
							break;
					}
				}
			}
			return covspan;
		}


	}
}