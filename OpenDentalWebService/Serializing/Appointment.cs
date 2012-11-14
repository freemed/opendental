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
			sb.Append("<AptDateTime>").Append(appointment.AptDateTime.ToString()).Append("</AptDateTime>");
			sb.Append("<NextAptNum>").Append(appointment.NextAptNum).Append("</NextAptNum>");
			sb.Append("<UnschedStatus>").Append(appointment.UnschedStatus).Append("</UnschedStatus>");
			sb.Append("<IsNewPatient>").Append((appointment.IsNewPatient)?1:0).Append("</IsNewPatient>");
			sb.Append("<ProcDescript>").Append(SerializeStringEscapes.EscapeForXml(appointment.ProcDescript)).Append("</ProcDescript>");
			sb.Append("<Assistant>").Append(appointment.Assistant).Append("</Assistant>");
			sb.Append("<ClinicNum>").Append(appointment.ClinicNum).Append("</ClinicNum>");
			sb.Append("<IsHygiene>").Append((appointment.IsHygiene)?1:0).Append("</IsHygiene>");
			sb.Append("<DateTStamp>").Append(appointment.DateTStamp.ToString()).Append("</DateTStamp>");
			sb.Append("<DateTimeArrived>").Append(appointment.DateTimeArrived.ToString()).Append("</DateTimeArrived>");
			sb.Append("<DateTimeSeated>").Append(appointment.DateTimeSeated.ToString()).Append("</DateTimeSeated>");
			sb.Append("<DateTimeDismissed>").Append(appointment.DateTimeDismissed.ToString()).Append("</DateTimeDismissed>");
			sb.Append("<InsPlan1>").Append(appointment.InsPlan1).Append("</InsPlan1>");
			sb.Append("<InsPlan2>").Append(appointment.InsPlan2).Append("</InsPlan2>");
			sb.Append("<DateTimeAskedToArrive>").Append(appointment.DateTimeAskedToArrive.ToString()).Append("</DateTimeAskedToArrive>");
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
							appointment.AptNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							appointment.PatNum=reader.ReadContentAsLong();
							break;
						case "AptStatus":
							appointment.AptStatus=(OpenDentBusiness.ApptStatus)reader.ReadContentAsInt();
							break;
						case "Pattern":
							appointment.Pattern=reader.ReadContentAsString();
							break;
						case "Confirmed":
							appointment.Confirmed=reader.ReadContentAsLong();
							break;
						case "TimeLocked":
							appointment.TimeLocked=reader.ReadContentAsString()!="0";
							break;
						case "Op":
							appointment.Op=reader.ReadContentAsLong();
							break;
						case "Note":
							appointment.Note=reader.ReadContentAsString();
							break;
						case "ProvNum":
							appointment.ProvNum=reader.ReadContentAsLong();
							break;
						case "ProvHyg":
							appointment.ProvHyg=reader.ReadContentAsLong();
							break;
						case "AptDateTime":
							appointment.AptDateTime=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "NextAptNum":
							appointment.NextAptNum=reader.ReadContentAsLong();
							break;
						case "UnschedStatus":
							appointment.UnschedStatus=reader.ReadContentAsLong();
							break;
						case "IsNewPatient":
							appointment.IsNewPatient=reader.ReadContentAsString()!="0";
							break;
						case "ProcDescript":
							appointment.ProcDescript=reader.ReadContentAsString();
							break;
						case "Assistant":
							appointment.Assistant=reader.ReadContentAsLong();
							break;
						case "ClinicNum":
							appointment.ClinicNum=reader.ReadContentAsLong();
							break;
						case "IsHygiene":
							appointment.IsHygiene=reader.ReadContentAsString()!="0";
							break;
						case "DateTStamp":
							appointment.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateTimeArrived":
							appointment.DateTimeArrived=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateTimeSeated":
							appointment.DateTimeSeated=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateTimeDismissed":
							appointment.DateTimeDismissed=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "InsPlan1":
							appointment.InsPlan1=reader.ReadContentAsLong();
							break;
						case "InsPlan2":
							appointment.InsPlan2=reader.ReadContentAsLong();
							break;
						case "DateTimeAskedToArrive":
							appointment.DateTimeAskedToArrive=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ProcsColored":
							appointment.ProcsColored=reader.ReadContentAsString();
							break;
						case "ColorOverride":
							appointment.ColorOverride=Color.FromArgb(reader.ReadContentAsInt());
							break;
					}
				}
			}
			return appointment;
		}


	}
}