using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PayPeriod {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PayPeriod payperiod) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PayPeriod>");
			sb.Append("<PayPeriodNum>").Append(payperiod.PayPeriodNum).Append("</PayPeriodNum>");
			sb.Append("<DateStart>").Append(payperiod.DateStart.ToString("yyyyMMddHHmmss")).Append("</DateStart>");
			sb.Append("<DateStop>").Append(payperiod.DateStop.ToString("yyyyMMddHHmmss")).Append("</DateStop>");
			sb.Append("<DatePaycheck>").Append(payperiod.DatePaycheck.ToString("yyyyMMddHHmmss")).Append("</DatePaycheck>");
			sb.Append("</PayPeriod>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PayPeriod Deserialize(string xml) {
			OpenDentBusiness.PayPeriod payperiod=new OpenDentBusiness.PayPeriod();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PayPeriodNum":
							payperiod.PayPeriodNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateStart":
							payperiod.DateStart=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateStop":
							payperiod.DateStop=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DatePaycheck":
							payperiod.DatePaycheck=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return payperiod;
		}


	}
}