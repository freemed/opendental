using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DashboardAR {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.DashboardAR dashboardar) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<DashboardAR>");
			sb.Append("<DashboardARNum>").Append(dashboardar.DashboardARNum).Append("</DashboardARNum>");
			sb.Append("<DateCalc>").Append(dashboardar.DateCalc.ToString("yyyyMMddHHmmss")).Append("</DateCalc>");
			sb.Append("<BalTotal>").Append(dashboardar.BalTotal).Append("</BalTotal>");
			sb.Append("<InsEst>").Append(dashboardar.InsEst).Append("</InsEst>");
			sb.Append("</DashboardAR>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.DashboardAR Deserialize(string xml) {
			OpenDentBusiness.DashboardAR dashboardar=new OpenDentBusiness.DashboardAR();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DashboardARNum":
							dashboardar.DashboardARNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateCalc":
							dashboardar.DateCalc=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "BalTotal":
							dashboardar.BalTotal=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "InsEst":
							dashboardar.InsEst=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
					}
				}
			}
			return dashboardar;
		}


	}
}