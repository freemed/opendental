using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Phone {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Phone phone) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Phone>");
			sb.Append("<PhoneNum>").Append(phone.PhoneNum).Append("</PhoneNum>");
			sb.Append("<Extension>").Append(phone.Extension).Append("</Extension>");
			sb.Append("<EmployeeName>").Append(SerializeStringEscapes.EscapeForXml(phone.EmployeeName)).Append("</EmployeeName>");
			sb.Append("<ClockStatus>").Append((int)phone.ClockStatus).Append("</ClockStatus>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(phone.Description)).Append("</Description>");
			sb.Append("<ColorBar>").Append(phone.ColorBar.ToArgb()).Append("</ColorBar>");
			sb.Append("<ColorText>").Append(phone.ColorText.ToArgb()).Append("</ColorText>");
			sb.Append("<EmployeeNum>").Append(phone.EmployeeNum).Append("</EmployeeNum>");
			sb.Append("<CustomerNumber>").Append(SerializeStringEscapes.EscapeForXml(phone.CustomerNumber)).Append("</CustomerNumber>");
			sb.Append("<InOrOut>").Append(SerializeStringEscapes.EscapeForXml(phone.InOrOut)).Append("</InOrOut>");
			sb.Append("<PatNum>").Append(phone.PatNum).Append("</PatNum>");
			sb.Append("<DateTimeStart>").Append(phone.DateTimeStart.ToString()).Append("</DateTimeStart>");
			sb.Append("<WebCamImage>").Append(SerializeStringEscapes.EscapeForXml(phone.WebCamImage)).Append("</WebCamImage>");
			sb.Append("<ScreenshotPath>").Append(SerializeStringEscapes.EscapeForXml(phone.ScreenshotPath)).Append("</ScreenshotPath>");
			sb.Append("<ScreenshotImage>").Append(SerializeStringEscapes.EscapeForXml(phone.ScreenshotImage)).Append("</ScreenshotImage>");
			sb.Append("<CustomerNumberRaw>").Append(SerializeStringEscapes.EscapeForXml(phone.CustomerNumberRaw)).Append("</CustomerNumberRaw>");
			sb.Append("</Phone>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Phone Deserialize(string xml) {
			OpenDentBusiness.Phone phone=new OpenDentBusiness.Phone();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PhoneNum":
							phone.PhoneNum=reader.ReadContentAsLong();
							break;
						case "Extension":
							phone.Extension=reader.ReadContentAsInt();
							break;
						case "EmployeeName":
							phone.EmployeeName=reader.ReadContentAsString();
							break;
						case "ClockStatus":
							phone.ClockStatus=(OpenDentBusiness.ClockStatusEnum)reader.ReadContentAsInt();
							break;
						case "Description":
							phone.Description=reader.ReadContentAsString();
							break;
						case "ColorBar":
							phone.ColorBar=Color.FromArgb(reader.ReadContentAsInt());
							break;
						case "ColorText":
							phone.ColorText=Color.FromArgb(reader.ReadContentAsInt());
							break;
						case "EmployeeNum":
							phone.EmployeeNum=reader.ReadContentAsLong();
							break;
						case "CustomerNumber":
							phone.CustomerNumber=reader.ReadContentAsString();
							break;
						case "InOrOut":
							phone.InOrOut=reader.ReadContentAsString();
							break;
						case "PatNum":
							phone.PatNum=reader.ReadContentAsLong();
							break;
						case "DateTimeStart":
							phone.DateTimeStart=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "WebCamImage":
							phone.WebCamImage=reader.ReadContentAsString();
							break;
						case "ScreenshotPath":
							phone.ScreenshotPath=reader.ReadContentAsString();
							break;
						case "ScreenshotImage":
							phone.ScreenshotImage=reader.ReadContentAsString();
							break;
						case "CustomerNumberRaw":
							phone.CustomerNumberRaw=reader.ReadContentAsString();
							break;
					}
				}
			}
			return phone;
		}


	}
}