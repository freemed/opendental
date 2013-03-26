using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class LabResult {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.LabResult labresult) {
			StringBuilder sb=new StringBuilder();
			if(labresult==null) {
				sb.Append("<null />");
				return sb.ToString();
			}
			sb.Append("<LabResult>");
			sb.Append("<LabResultNum>").Append(labresult.LabResultNum).Append("</LabResultNum>");
			sb.Append("<LabPanelNum>").Append(labresult.LabPanelNum).Append("</LabPanelNum>");
			sb.Append("<DateTimeTest>").Append(labresult.DateTimeTest.ToString("yyyyMMddHHmmss")).Append("</DateTimeTest>");
			sb.Append("<TestName>").Append(SerializeStringEscapes.EscapeForXml(labresult.TestName)).Append("</TestName>");
			sb.Append("<DateTStamp>").Append(labresult.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<TestID>").Append(SerializeStringEscapes.EscapeForXml(labresult.TestID)).Append("</TestID>");
			sb.Append("<ObsValue>").Append(SerializeStringEscapes.EscapeForXml(labresult.ObsValue)).Append("</ObsValue>");
			sb.Append("<ObsUnits>").Append(SerializeStringEscapes.EscapeForXml(labresult.ObsUnits)).Append("</ObsUnits>");
			sb.Append("<ObsRange>").Append(SerializeStringEscapes.EscapeForXml(labresult.ObsRange)).Append("</ObsRange>");
			sb.Append("<AbnormalFlag>").Append((int)labresult.AbnormalFlag).Append("</AbnormalFlag>");
			sb.Append("</LabResult>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.LabResult Deserialize(string xml) {
			OpenDentBusiness.LabResult labresult=new OpenDentBusiness.LabResult();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "LabResultNum":
							labresult.LabResultNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "LabPanelNum":
							labresult.LabPanelNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTimeTest":
							labresult.DateTimeTest=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "TestName":
							labresult.TestName=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							labresult.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "TestID":
							labresult.TestID=reader.ReadContentAsString();
							break;
						case "ObsValue":
							labresult.ObsValue=reader.ReadContentAsString();
							break;
						case "ObsUnits":
							labresult.ObsUnits=reader.ReadContentAsString();
							break;
						case "ObsRange":
							labresult.ObsRange=reader.ReadContentAsString();
							break;
						case "AbnormalFlag":
							labresult.AbnormalFlag=(OpenDentBusiness.LabAbnormalFlag)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return labresult;
		}


	}
}