using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Recall {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Recall recall) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Recall>");
			sb.Append("<RecallNum>").Append(recall.RecallNum).Append("</RecallNum>");
			sb.Append("<PatNum>").Append(recall.PatNum).Append("</PatNum>");
			sb.Append("<DateDueCalc>").Append(recall.DateDueCalc.ToString("yyyyMMddHHmmss")).Append("</DateDueCalc>");
			sb.Append("<DateDue>").Append(recall.DateDue.ToString("yyyyMMddHHmmss")).Append("</DateDue>");
			sb.Append("<DatePrevious>").Append(recall.DatePrevious.ToString("yyyyMMddHHmmss")).Append("</DatePrevious>");
			sb.Append("<RecallInterval>").Append(recall.RecallInterval).Append("</RecallInterval>");
			sb.Append("<RecallStatus>").Append(recall.RecallStatus).Append("</RecallStatus>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(recall.Note)).Append("</Note>");
			sb.Append("<IsDisabled>").Append((recall.IsDisabled)?1:0).Append("</IsDisabled>");
			sb.Append("<DateTStamp>").Append(recall.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<RecallTypeNum>").Append(recall.RecallTypeNum).Append("</RecallTypeNum>");
			sb.Append("<DisableUntilBalance>").Append(recall.DisableUntilBalance).Append("</DisableUntilBalance>");
			sb.Append("<DisableUntilDate>").Append(recall.DisableUntilDate.ToString("yyyyMMddHHmmss")).Append("</DisableUntilDate>");
			sb.Append("<DateScheduled>").Append(recall.DateScheduled.ToString("yyyyMMddHHmmss")).Append("</DateScheduled>");
			sb.Append("</Recall>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Recall Deserialize(string xml) {
			OpenDentBusiness.Recall recall=new OpenDentBusiness.Recall();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RecallNum":
							recall.RecallNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							recall.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateDueCalc":
							recall.DateDueCalc=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateDue":
							recall.DateDue=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DatePrevious":
							recall.DatePrevious=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "RecallInterval":
							recall.RecallInterval=new OpenDentBusiness.Interval(System.Convert.ToInt32(reader.ReadContentAsString()));
							break;
						case "RecallStatus":
							recall.RecallStatus=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Note":
							recall.Note=reader.ReadContentAsString();
							break;
						case "IsDisabled":
							recall.IsDisabled=reader.ReadContentAsString()!="0";
							break;
						case "DateTStamp":
							recall.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "RecallTypeNum":
							recall.RecallTypeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DisableUntilBalance":
							recall.DisableUntilBalance=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "DisableUntilDate":
							recall.DisableUntilDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateScheduled":
							recall.DateScheduled=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return recall;
		}


	}
}