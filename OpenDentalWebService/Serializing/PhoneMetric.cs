using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PhoneMetric {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PhoneMetric phonemetric) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PhoneMetric>");
			sb.Append("<PhoneMetricNum>").Append(phonemetric.PhoneMetricNum).Append("</PhoneMetricNum>");
			sb.Append("<DateTimeEntry>").Append(phonemetric.DateTimeEntry.ToString("yyyyMMddHHmmss")).Append("</DateTimeEntry>");
			sb.Append("<VoiceMails>").Append(phonemetric.VoiceMails).Append("</VoiceMails>");
			sb.Append("<Triages>").Append(phonemetric.Triages).Append("</Triages>");
			sb.Append("<MinutesBehind>").Append(phonemetric.MinutesBehind).Append("</MinutesBehind>");
			sb.Append("</PhoneMetric>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PhoneMetric Deserialize(string xml) {
			OpenDentBusiness.PhoneMetric phonemetric=new OpenDentBusiness.PhoneMetric();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PhoneMetricNum":
							phonemetric.PhoneMetricNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTimeEntry":
							phonemetric.DateTimeEntry=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "VoiceMails":
							phonemetric.VoiceMails=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Triages":
							phonemetric.Triages=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "MinutesBehind":
							phonemetric.MinutesBehind=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return phonemetric;
		}


	}
}