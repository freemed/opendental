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
			sb.Append("<DateStart>").Append(payperiod.DateStart.ToString()).Append("</DateStart>");
			sb.Append("<DateStop>").Append(payperiod.DateStop.ToString()).Append("</DateStop>");
			sb.Append("<DatePaycheck>").Append(payperiod.DatePaycheck.ToString()).Append("</DatePaycheck>");
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
							payperiod.PayPeriodNum=reader.ReadContentAsLong();
							break;
						case "DateStart":
							payperiod.DateStart=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateStop":
							payperiod.DateStop=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DatePaycheck":
							payperiod.DatePaycheck=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return payperiod;
		}


	}
}