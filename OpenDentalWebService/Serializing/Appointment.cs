using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Appointment {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Appointment appointment) {
			StringBuilder sb=new StringBuilder();
			if(appointment==null) {
				sb.Append("<null />");
				return sb.ToString();
			}
			sb.Append("<Appointment>");
			sb.Append("<AptNum>").Append(appointment.AptNum).Append("</AptNum>");
			sb.Append("<PatNum>").Append(appointment.PatNum).Append("</PatNum>");
			sb.Append("<AptStatus>").Append((int)appointment.AptStatus).Append("</AptStatus>");
			sb.Append("<Pattern>").Append(SerializeStringEscapes.EscapeForXml(appointment.Pattern)).Append("</Pattern>");
			sb.Append("<Confirmed>").Append(appointment.Confirmed).Append("</Confirmed>");
			sb.Append("<TimeLocked>").Append((appointment.TimeLocked)?1:0).Append("</TimeLocked>");
			sb.Append("<Op>").Append(appointment.Op).Append("</Op>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(appointment.Note)).Append("</Note>");
			sb.Append("<ProvNum>").Append(appointment.ProvNum).Append("</ProvNum>");
			sb.Append("<ProvHyg>").Append(appointment.ProvHyg).Append("</ProvHyg>");
			sb.Append("<AptDateTime>").Append(appointment.AptDateTime.ToString("yyyyMMddHHmmss")).Append("</AptDateTime>");
			sb.Append("<NextAptNum>").Append(appointment.NextAptNum).Append("</NextAptNum>");
			sb.Append("<UnschedStatus>").Append(appointment.UnschedStatus).Append("</UnschedStatus>");
			sb.Append("<IsNewPatient>").Append((appointment.IsNewPatient)?1:0).Append("</IsNewPatient>");
			sb.Append("<ProcDescript>").Append(SerializeStringEscapes.EscapeForXml(appointment.ProcDescript)).Append("</ProcDescript>");
			sb.Append("<Assistant>").Append(appointment.Assistant).Append("</Assistant>");
			sb.Append("<ClinicNum>").Append(appointment.ClinicNum).Append("</ClinicNum>");
			sb.Append("<IsHygiene>").Append((appointment.IsHygiene)?1:0).Append("</IsHygiene>");
			sb.Append("<DateTStamp>").Append(appointment.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<DateTimeArrived>").Append(appointment.DateTimeArrived.ToString("yyyyMMddHHmmss")).Append("</DateTimeArrived>");
			sb.Append("<DateTimeSeated>").Append(appointment.DateTimeSeated.ToString("yyyyMMddHHmmss")).Append("</DateTimeSeated>");
			sb.Append("<DateTimeDismissed>").Append(appointment.DateTimeDismissed.ToString("yyyyMMddHHmmss")).Append("</DateTimeDismissed>");
			sb.Append("<InsPlan1>").Append(appointment.InsPlan1).Append("</InsPlan1>");
			sb.Append("<InsPlan2>").Append(appointment.InsPlan2).Append("</InsPlan2>");
			sb.Append("<DateTimeAskedToArrive>").Append(appointment.DateTimeAskedToArrive.ToString("yyyyMMddHHmmss")).Append("</DateTimeAskedToArrive>");
			sb.Append("<ProcsColored>").Append(SerializeStringEscapes.EscapeForXml(appointment.ProcsColored)).Append("</ProcsColored>");
			sb.Append("<ColorOverride>").Append(appointment.ColorOverride.ToArgb()).Append("</ColorOverride>");
			sb.Append("</Appointment>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Appointment Deserialize(string xml) {
			OpenDentBusiness.Appointment appointment=new OpenDentBusiness.Appointment();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AptNum":
							appointment.AptNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							appointment.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AptStatus":
							appointment.AptStatus=(OpenDentBusiness.ApptStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Pattern":
							appointment.Pattern=reader.ReadContentAsString();
							break;
						case "Confirmed":
							appointment.Confirmed=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "TimeLocked":
							appointment.TimeLocked=reader.ReadContentAsString()!="0";
							break;
						case "Op":
							appointment.Op=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Note":
							appointment.Note=reader.ReadContentAsString();
							break;
						case "ProvNum":
							appointment.ProvNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProvHyg":
							appointment.ProvHyg=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AptDateTime":
							appointment.AptDateTime=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "NextAptNum":
							appointment.NextAptNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "UnschedStatus":
							appointment.UnschedStatus=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "IsNewPatient":
							appointment.IsNewPatient=reader.ReadContentAsString()!="0";
							break;
						case "ProcDescript":
							appointment.ProcDescript=reader.ReadContentAsString();
							break;
						case "Assistant":
							appointment.Assistant=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ClinicNum":
							appointment.ClinicNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "IsHygiene":
							appointment.IsHygiene=reader.ReadContentAsString()!="0";
							break;
						case "DateTStamp":
							appointment.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateTimeArrived":
							appointment.DateTimeArrived=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateTimeSeated":
							appointment.DateTimeSeated=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateTimeDismissed":
							appointment.DateTimeDismissed=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "InsPlan1":
							appointment.InsPlan1=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "InsPlan2":
							appointment.InsPlan2=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTimeAskedToArrive":
							appointment.DateTimeAskedToArrive=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ProcsColored":
							appointment.ProcsColored=reader.ReadContentAsString();
							break;
						case "ColorOverride":
							appointment.ColorOverride=Color.FromArgb(System.Convert.ToInt32(reader.ReadContentAsString()));
							break;
					}
				}
			}
			return appointment;
		}


	}
}