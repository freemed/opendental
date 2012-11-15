using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ClockEvent {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ClockEvent clockevent) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ClockEvent>");
			sb.Append("<ClockEventNum>").Append(clockevent.ClockEventNum).Append("</ClockEventNum>");
			sb.Append("<EmployeeNum>").Append(clockevent.EmployeeNum).Append("</EmployeeNum>");
			sb.Append("<TimeEntered1>").Append(clockevent.TimeEntered1.ToString("yyyyMMddHHmmss")).Append("</TimeEntered1>");
			sb.Append("<TimeDisplayed1>").Append(clockevent.TimeDisplayed1.ToString("yyyyMMddHHmmss")).Append("</TimeDisplayed1>");
			sb.Append("<ClockStatus>").Append((int)clockevent.ClockStatus).Append("</ClockStatus>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(clockevent.Note)).Append("</Note>");
			sb.Append("<TimeEntered2>").Append(clockevent.TimeEntered2.ToString("yyyyMMddHHmmss")).Append("</TimeEntered2>");
			sb.Append("<TimeDisplayed2>").Append(clockevent.TimeDisplayed2.ToString("yyyyMMddHHmmss")).Append("</TimeDisplayed2>");
			sb.Append("<OTimeHours>").Append(clockevent.OTimeHours.ToString()).Append("</OTimeHours>");
			sb.Append("<OTimeAuto>").Append(clockevent.OTimeAuto.ToString()).Append("</OTimeAuto>");
			sb.Append("<Adjust>").Append(clockevent.Adjust.ToString()).Append("</Adjust>");
			sb.Append("<AdjustAuto>").Append(clockevent.AdjustAuto.ToString()).Append("</AdjustAuto>");
			sb.Append("<AdjustIsOverridden>").Append((clockevent.AdjustIsOverridden)?1:0).Append("</AdjustIsOverridden>");
			sb.Append("<AmountBonus>").Append(clockevent.AmountBonus).Append("</AmountBonus>");
			sb.Append("<AmountBonusAuto>").Append(clockevent.AmountBonusAuto).Append("</AmountBonusAuto>");
			sb.Append("</ClockEvent>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ClockEvent Deserialize(string xml) {
			OpenDentBusiness.ClockEvent clockevent=new OpenDentBusiness.ClockEvent();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClockEventNum":
							clockevent.ClockEventNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "EmployeeNum":
							clockevent.EmployeeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "TimeEntered1":
							clockevent.TimeEntered1=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "TimeDisplayed1":
							clockevent.TimeDisplayed1=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ClockStatus":
							clockevent.ClockStatus=(OpenDentBusiness.TimeClockStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Note":
							clockevent.Note=reader.ReadContentAsString();
							break;
						case "TimeEntered2":
							clockevent.TimeEntered2=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "TimeDisplayed2":
							clockevent.TimeDisplayed2=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "OTimeHours":
							clockevent.OTimeHours=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "OTimeAuto":
							clockevent.OTimeAuto=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "Adjust":
							clockevent.Adjust=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "AdjustAuto":
							clockevent.AdjustAuto=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "AdjustIsOverridden":
							clockevent.AdjustIsOverridden=reader.ReadContentAsString()!="0";
							break;
						case "AmountBonus":
							clockevent.AmountBonus=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "AmountBonusAuto":
							clockevent.AmountBonusAuto=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
					}
				}
			}
			return clockevent;
		}


	}
}