using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RepeatCharge {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RepeatCharge repeatcharge) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RepeatCharge>");
			sb.Append("<RepeatChargeNum>").Append(repeatcharge.RepeatChargeNum).Append("</RepeatChargeNum>");
			sb.Append("<PatNum>").Append(repeatcharge.PatNum).Append("</PatNum>");
			sb.Append("<ProcCode>").Append(SerializeStringEscapes.EscapeForXml(repeatcharge.ProcCode)).Append("</ProcCode>");
			sb.Append("<ChargeAmt>").Append(repeatcharge.ChargeAmt).Append("</ChargeAmt>");
			sb.Append("<DateStart>").Append(repeatcharge.DateStart.ToString("yyyyMMddHHmmss")).Append("</DateStart>");
			sb.Append("<DateStop>").Append(repeatcharge.DateStop.ToString("yyyyMMddHHmmss")).Append("</DateStop>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(repeatcharge.Note)).Append("</Note>");
			sb.Append("</RepeatCharge>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RepeatCharge Deserialize(string xml) {
			OpenDentBusiness.RepeatCharge repeatcharge=new OpenDentBusiness.RepeatCharge();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RepeatChargeNum":
							repeatcharge.RepeatChargeNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							repeatcharge.PatNum=reader.ReadContentAsLong();
							break;
						case "ProcCode":
							repeatcharge.ProcCode=reader.ReadContentAsString();
							break;
						case "ChargeAmt":
							repeatcharge.ChargeAmt=reader.ReadContentAsDouble();
							break;
						case "DateStart":
							repeatcharge.DateStart=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateStop":
							repeatcharge.DateStop=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "Note":
							repeatcharge.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return repeatcharge;
		}


	}
}