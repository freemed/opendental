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
			sb.Append("<DateTimeStart>").Append(phone.DateTimeStart.ToString("yyyyMMddHHmmss")).Append("</DateTimeStart>");
			sb.Append("<WebCamImage>").Append(SerializeStringEscapes.EscapeForXml(phone.WebCamImage)).Append("</WebCamImage>");
			sb.Append("<ScreenshotPath>").Append(SerializeStringEscapes.EscapeForXml(phone.ScreenshotPath)).Append("</ScreenshotPath>");
			sb.Append("<ScreenshotImage>").Append(SerializeStringEscapes.EscapeForXml(phone.ScreenshotImage)).Append("</ScreenshotImage>");
			sb.Append("<CustomerNumberRaw>").Append(SerializeStringEscapes.EscapeForXml(phone.CustomerNumberRaw)).Append("</CustomerNumberRaw>");
			sb.Append("<LastCallTimeStart>").Append(phone.LastCallTimeStart.ToString("yyyyMMddHHmmss")).Append("</LastCallTimeStart>");
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
							phone.PhoneNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Extension":
							phone.Extension=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "EmployeeName":
							phone.EmployeeName=reader.ReadContentAsString();
							break;
						case "ClockStatus":
							phone.ClockStatus=(OpenDentBusiness.ClockStatusEnum)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Description":
							phone.Description=reader.ReadContentAsString();
							break;
						case "ColorBar":
							phone.ColorBar=Color.FromArgb(System.Convert.ToInt32(reader.ReadContentAsString()));
							break;
						case "ColorText":
							phone.ColorText=Color.FromArgb(System.Convert.ToInt32(reader.ReadContentAsString()));
							break;
						case "EmployeeNum":
							phone.EmployeeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CustomerNumber":
							phone.CustomerNumber=reader.ReadContentAsString();
							break;
						case "InOrOut":
							phone.InOrOut=reader.ReadContentAsString();
							break;
						case "PatNum":
							phone.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTimeStart":
							phone.DateTimeStart=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
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
						case "LastCallTimeStart":
							phone.LastCallTimeStart=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return phone;
		}


	}
}