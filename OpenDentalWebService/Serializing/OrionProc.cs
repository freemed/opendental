using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class OrionProc {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.OrionProc orionproc) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<OrionProc>");
			sb.Append("<OrionProcNum>").Append(orionproc.OrionProcNum).Append("</OrionProcNum>");
			sb.Append("<ProcNum>").Append(orionproc.ProcNum).Append("</ProcNum>");
			sb.Append("<DPC>").Append((int)orionproc.DPC).Append("</DPC>");
			sb.Append("<DPCpost>").Append((int)orionproc.DPCpost).Append("</DPCpost>");
			sb.Append("<DateScheduleBy>").Append(orionproc.DateScheduleBy.ToString("yyyyMMddHHmmss")).Append("</DateScheduleBy>");
			sb.Append("<DateStopClock>").Append(orionproc.DateStopClock.ToString("yyyyMMddHHmmss")).Append("</DateStopClock>");
			sb.Append("<Status2>").Append((int)orionproc.Status2).Append("</Status2>");
			sb.Append("<IsOnCall>").Append((orionproc.IsOnCall)?1:0).Append("</IsOnCall>");
			sb.Append("<IsEffectiveComm>").Append((orionproc.IsEffectiveComm)?1:0).Append("</IsEffectiveComm>");
			sb.Append("<IsRepair>").Append((orionproc.IsRepair)?1:0).Append("</IsRepair>");
			sb.Append("</OrionProc>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.OrionProc Deserialize(string xml) {
			OpenDentBusiness.OrionProc orionproc=new OpenDentBusiness.OrionProc();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "OrionProcNum":
							orionproc.OrionProcNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProcNum":
							orionproc.ProcNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DPC":
							orionproc.DPC=(OpenDentBusiness.OrionDPC)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DPCpost":
							orionproc.DPCpost=(OpenDentBusiness.OrionDPC)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DateScheduleBy":
							orionproc.DateScheduleBy=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateStopClock":
							orionproc.DateStopClock=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "Status2":
							orionproc.Status2=(OpenDentBusiness.OrionStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IsOnCall":
							orionproc.IsOnCall=reader.ReadContentAsString()!="0";
							break;
						case "IsEffectiveComm":
							orionproc.IsEffectiveComm=reader.ReadContentAsString()!="0";
							break;
						case "IsRepair":
							orionproc.IsRepair=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return orionproc;
		}


	}
}