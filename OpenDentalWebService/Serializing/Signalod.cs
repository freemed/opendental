using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Signalod {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Signalod signalod) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Signalod>");
			sb.Append("<SignalNum>").Append(signalod.SignalNum).Append("</SignalNum>");
			sb.Append("<FromUser>").Append(SerializeStringEscapes.EscapeForXml(signalod.FromUser)).Append("</FromUser>");
			sb.Append("<ITypes>").Append(SerializeStringEscapes.EscapeForXml(signalod.ITypes)).Append("</ITypes>");
			sb.Append("<DateViewing>").Append(signalod.DateViewing.ToString("yyyyMMddHHmmss")).Append("</DateViewing>");
			sb.Append("<SigType>").Append((int)signalod.SigType).Append("</SigType>");
			sb.Append("<SigText>").Append(SerializeStringEscapes.EscapeForXml(signalod.SigText)).Append("</SigText>");
			sb.Append("<SigDateTime>").Append(signalod.SigDateTime.ToString("yyyyMMddHHmmss")).Append("</SigDateTime>");
			sb.Append("<ToUser>").Append(SerializeStringEscapes.EscapeForXml(signalod.ToUser)).Append("</ToUser>");
			sb.Append("<AckTime>").Append(signalod.AckTime.ToString("yyyyMMddHHmmss")).Append("</AckTime>");
			sb.Append("<TaskNum>").Append(signalod.TaskNum).Append("</TaskNum>");
			sb.Append("</Signalod>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Signalod Deserialize(string xml) {
			OpenDentBusiness.Signalod signalod=new OpenDentBusiness.Signalod();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SignalNum":
							signalod.SignalNum=reader.ReadContentAsLong();
							break;
						case "FromUser":
							signalod.FromUser=reader.ReadContentAsString();
							break;
						case "ITypes":
							signalod.ITypes=reader.ReadContentAsString();
							break;
						case "DateViewing":
							signalod.DateViewing=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "SigType":
							signalod.SigType=(OpenDentBusiness.SignalType)reader.ReadContentAsInt();
							break;
						case "SigText":
							signalod.SigText=reader.ReadContentAsString();
							break;
						case "SigDateTime":
							signalod.SigDateTime=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ToUser":
							signalod.ToUser=reader.ReadContentAsString();
							break;
						case "AckTime":
							signalod.AckTime=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "TaskNum":
							signalod.TaskNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return signalod;
		}


	}
}