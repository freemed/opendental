using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Patient {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Patient patient) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Patient>");
			sb.Append("<PatNum>").Append(patient.PatNum).Append("</PatNum>");
			sb.Append("<LName>").Append(SerializeStringEscapes.EscapeForXml(patient.LName)).Append("</LName>");
			sb.Append("<FName>").Append(SerializeStringEscapes.EscapeForXml(patient.FName)).Append("</FName>");
			sb.Append("<MiddleI>").Append(SerializeStringEscapes.EscapeForXml(patient.MiddleI)).Append("</MiddleI>");
			sb.Append("<Preferred>").Append(SerializeStringEscapes.EscapeForXml(patient.Preferred)).Append("</Preferred>");
			sb.Append("<PatStatus>").Append((int)patient.PatStatus).Append("</PatStatus>");
			sb.Append("<Gender>").Append((int)patient.Gender).Append("</Gender>");
			sb.Append("<Position>").Append((int)patient.Position).Append("</Position>");
			sb.Append("<Birthdate>").Append(patient.Birthdate.ToString("yyyyMMddHHmmss")).Append("</Birthdate>");
			sb.Append("<SSN>").Append(SerializeStringEscapes.EscapeForXml(patient.SSN)).Append("</SSN>");
			sb.Append("<Address>").Append(SerializeStringEscapes.EscapeForXml(patient.Address)).Append("</Address>");
			sb.Append("<Address2>").Append(SerializeStringEscapes.EscapeForXml(patient.Address2)).Append("</Address2>");
			sb.Append("<City>").Append(SerializeStringEscapes.EscapeForXml(patient.City)).Append("</City>");
			sb.Append("<State>").Append(SerializeStringEscapes.EscapeForXml(patient.State)).Append("</State>");
			sb.Append("<Zip>").Append(SerializeStringEscapes.EscapeForXml(patient.Zip)).Append("</Zip>");
			sb.Append("<HmPhone>").Append(SerializeStringEscapes.EscapeForXml(patient.HmPhone)).Append("</HmPhone>");
			sb.Append("<WkPhone>").Append(SerializeStringEscapes.EscapeForXml(patient.WkPhone)).Append("</WkPhone>");
			sb.Append("<WirelessPhone>").Append(SerializeStringEscapes.EscapeForXml(patient.WirelessPhone)).Append("</WirelessPhone>");
			sb.Append("<Guarantor>").Append(patient.Guarantor).Append("</Guarantor>");
			sb.Append("<Age>").Append(patient.Age).Append("</Age>");
			sb.Append("<CreditType>").Append(SerializeStringEscapes.EscapeForXml(patient.CreditType)).Append("</CreditType>");
			sb.Append("<Email>").Append(SerializeStringEscapes.EscapeForXml(patient.Email)).Append("</Email>");
			sb.Append("<Salutation>").Append(SerializeStringEscapes.EscapeForXml(patient.Salutation)).Append("</Salutation>");
			sb.Append("<EstBalance>").Append(patient.EstBalance).Append("</EstBalance>");
			sb.Append("<PriProv>").Append(patient.PriProv).Append("</PriProv>");
			sb.Append("<SecProv>").Append(patient.SecProv).Append("</SecProv>");
			sb.Append("<FeeSched>").Append(patient.FeeSched).Append("</FeeSched>");
			sb.Append("<BillingType>").Append(patient.BillingType).Append("</BillingType>");
			sb.Append("<ImageFolder>").Append(SerializeStringEscapes.EscapeForXml(patient.ImageFolder)).Append("</ImageFolder>");
			sb.Append("<AddrNote>").Append(SerializeStringEscapes.EscapeForXml(patient.AddrNote)).Append("</AddrNote>");
			sb.Append("<FamFinUrgNote>").Append(SerializeStringEscapes.EscapeForXml(patient.FamFinUrgNote)).Append("</FamFinUrgNote>");
			sb.Append("<MedUrgNote>").Append(SerializeStringEscapes.EscapeForXml(patient.MedUrgNote)).Append("</MedUrgNote>");
			sb.Append("<ApptModNote>").Append(SerializeStringEscapes.EscapeForXml(patient.ApptModNote)).Append("</ApptModNote>");
			sb.Append("<StudentStatus>").Append(SerializeStringEscapes.EscapeForXml(patient.StudentStatus)).Append("</StudentStatus>");
			sb.Append("<SchoolName>").Append(SerializeStringEscapes.EscapeForXml(patient.SchoolName)).Append("</SchoolName>");
			sb.Append("<ChartNumber>").Append(SerializeStringEscapes.EscapeForXml(patient.ChartNumber)).Append("</ChartNumber>");
			sb.Append("<MedicaidID>").Append(SerializeStringEscapes.EscapeForXml(patient.MedicaidID)).Append("</MedicaidID>");
			sb.Append("<Bal_0_30>").Append(patient.Bal_0_30).Append("</Bal_0_30>");
			sb.Append("<Bal_31_60>").Append(patient.Bal_31_60).Append("</Bal_31_60>");
			sb.Append("<Bal_61_90>").Append(patient.Bal_61_90).Append("</Bal_61_90>");
			sb.Append("<BalOver90>").Append(patient.BalOver90).Append("</BalOver90>");
			sb.Append("<InsEst>").Append(patient.InsEst).Append("</InsEst>");
			sb.Append("<BalTotal>").Append(patient.BalTotal).Append("</BalTotal>");
			sb.Append("<EmployerNum>").Append(patient.EmployerNum).Append("</EmployerNum>");
			sb.Append("<EmploymentNote>").Append(SerializeStringEscapes.EscapeForXml(patient.EmploymentNote)).Append("</EmploymentNote>");
			sb.Append("<Race>").Append((int)patient.Race).Append("</Race>");
			sb.Append("<County>").Append(SerializeStringEscapes.EscapeForXml(patient.County)).Append("</County>");
			sb.Append("<GradeLevel>").Append((int)patient.GradeLevel).Append("</GradeLevel>");
			sb.Append("<Urgency>").Append((int)patient.Urgency).Append("</Urgency>");
			sb.Append("<DateFirstVisit>").Append(patient.DateFirstVisit.ToString("yyyyMMddHHmmss")).Append("</DateFirstVisit>");
			sb.Append("<ClinicNum>").Append(patient.ClinicNum).Append("</ClinicNum>");
			sb.Append("<HasIns>").Append(SerializeStringEscapes.EscapeForXml(patient.HasIns)).Append("</HasIns>");
			sb.Append("<TrophyFolder>").Append(SerializeStringEscapes.EscapeForXml(patient.TrophyFolder)).Append("</TrophyFolder>");
			sb.Append("<PlannedIsDone>").Append((patient.PlannedIsDone)?1:0).Append("</PlannedIsDone>");
			sb.Append("<Premed>").Append((patient.Premed)?1:0).Append("</Premed>");
			sb.Append("<Ward>").Append(SerializeStringEscapes.EscapeForXml(patient.Ward)).Append("</Ward>");
			sb.Append("<PreferConfirmMethod>").Append((int)patient.PreferConfirmMethod).Append("</PreferConfirmMethod>");
			sb.Append("<PreferContactMethod>").Append((int)patient.PreferContactMethod).Append("</PreferContactMethod>");
			sb.Append("<PreferRecallMethod>").Append((int)patient.PreferRecallMethod).Append("</PreferRecallMethod>");
			sb.Append("<SchedBeforeTime>").Append(patient.SchedBeforeTime.ToString()).Append("</SchedBeforeTime>");
			sb.Append("<SchedAfterTime>").Append(patient.SchedAfterTime.ToString()).Append("</SchedAfterTime>");
			sb.Append("<SchedDayOfWeek>").Append(patient.SchedDayOfWeek).Append("</SchedDayOfWeek>");
			sb.Append("<Language>").Append(SerializeStringEscapes.EscapeForXml(patient.Language)).Append("</Language>");
			sb.Append("<AdmitDate>").Append(patient.AdmitDate.ToString("yyyyMMddHHmmss")).Append("</AdmitDate>");
			sb.Append("<Title>").Append(SerializeStringEscapes.EscapeForXml(patient.Title)).Append("</Title>");
			sb.Append("<PayPlanDue>").Append(patient.PayPlanDue).Append("</PayPlanDue>");
			sb.Append("<SiteNum>").Append(patient.SiteNum).Append("</SiteNum>");
			sb.Append("<DateTStamp>").Append(patient.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<ResponsParty>").Append(patient.ResponsParty).Append("</ResponsParty>");
			sb.Append("<CanadianEligibilityCode>").Append(patient.CanadianEligibilityCode).Append("</CanadianEligibilityCode>");
			sb.Append("<AskToArriveEarly>").Append(patient.AskToArriveEarly).Append("</AskToArriveEarly>");
			sb.Append("<OnlinePassword>").Append(SerializeStringEscapes.EscapeForXml(patient.OnlinePassword)).Append("</OnlinePassword>");
			sb.Append("<SmokeStatus>").Append((int)patient.SmokeStatus).Append("</SmokeStatus>");
			sb.Append("<PreferContactConfidential>").Append((int)patient.PreferContactConfidential).Append("</PreferContactConfidential>");
			sb.Append("<SuperFamily>").Append(patient.SuperFamily).Append("</SuperFamily>");
			sb.Append("<TxtMsgOk>").Append((int)patient.TxtMsgOk).Append("</TxtMsgOk>");
			sb.Append("</Patient>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Patient Deserialize(string xml) {
			OpenDentBusiness.Patient patient=new OpenDentBusiness.Patient();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PatNum":
							patient.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "LName":
							patient.LName=reader.ReadContentAsString();
							break;
						case "FName":
							patient.FName=reader.ReadContentAsString();
							break;
						case "MiddleI":
							patient.MiddleI=reader.ReadContentAsString();
							break;
						case "Preferred":
							patient.Preferred=reader.ReadContentAsString();
							break;
						case "PatStatus":
							patient.PatStatus=(OpenDentBusiness.PatientStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Gender":
							patient.Gender=(OpenDentBusiness.PatientGender)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Position":
							patient.Position=(OpenDentBusiness.PatientPosition)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Birthdate":
							patient.Birthdate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "SSN":
							patient.SSN=reader.ReadContentAsString();
							break;
						case "Address":
							patient.Address=reader.ReadContentAsString();
							break;
						case "Address2":
							patient.Address2=reader.ReadContentAsString();
							break;
						case "City":
							patient.City=reader.ReadContentAsString();
							break;
						case "State":
							patient.State=reader.ReadContentAsString();
							break;
						case "Zip":
							patient.Zip=reader.ReadContentAsString();
							break;
						case "HmPhone":
							patient.HmPhone=reader.ReadContentAsString();
							break;
						case "WkPhone":
							patient.WkPhone=reader.ReadContentAsString();
							break;
						case "WirelessPhone":
							patient.WirelessPhone=reader.ReadContentAsString();
							break;
						case "Guarantor":
							patient.Guarantor=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Age":
							patient.Age=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CreditType":
							patient.CreditType=reader.ReadContentAsString();
							break;
						case "Email":
							patient.Email=reader.ReadContentAsString();
							break;
						case "Salutation":
							patient.Salutation=reader.ReadContentAsString();
							break;
						case "EstBalance":
							patient.EstBalance=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "PriProv":
							patient.PriProv=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SecProv":
							patient.SecProv=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FeeSched":
							patient.FeeSched=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "BillingType":
							patient.BillingType=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ImageFolder":
							patient.ImageFolder=reader.ReadContentAsString();
							break;
						case "AddrNote":
							patient.AddrNote=reader.ReadContentAsString();
							break;
						case "FamFinUrgNote":
							patient.FamFinUrgNote=reader.ReadContentAsString();
							break;
						case "MedUrgNote":
							patient.MedUrgNote=reader.ReadContentAsString();
							break;
						case "ApptModNote":
							patient.ApptModNote=reader.ReadContentAsString();
							break;
						case "StudentStatus":
							patient.StudentStatus=reader.ReadContentAsString();
							break;
						case "SchoolName":
							patient.SchoolName=reader.ReadContentAsString();
							break;
						case "ChartNumber":
							patient.ChartNumber=reader.ReadContentAsString();
							break;
						case "MedicaidID":
							patient.MedicaidID=reader.ReadContentAsString();
							break;
						case "Bal_0_30":
							patient.Bal_0_30=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "Bal_31_60":
							patient.Bal_31_60=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "Bal_61_90":
							patient.Bal_61_90=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "BalOver90":
							patient.BalOver90=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "InsEst":
							patient.InsEst=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "BalTotal":
							patient.BalTotal=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "EmployerNum":
							patient.EmployerNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "EmploymentNote":
							patient.EmploymentNote=reader.ReadContentAsString();
							break;
						case "Race":
							patient.Race=(OpenDentBusiness.PatientRace)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "County":
							patient.County=reader.ReadContentAsString();
							break;
						case "GradeLevel":
							patient.GradeLevel=(OpenDentBusiness.PatientGrade)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Urgency":
							patient.Urgency=(OpenDentBusiness.TreatmentUrgency)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DateFirstVisit":
							patient.DateFirstVisit=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ClinicNum":
							patient.ClinicNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "HasIns":
							patient.HasIns=reader.ReadContentAsString();
							break;
						case "TrophyFolder":
							patient.TrophyFolder=reader.ReadContentAsString();
							break;
						case "PlannedIsDone":
							patient.PlannedIsDone=reader.ReadContentAsString()!="0";
							break;
						case "Premed":
							patient.Premed=reader.ReadContentAsString()!="0";
							break;
						case "Ward":
							patient.Ward=reader.ReadContentAsString();
							break;
						case "PreferConfirmMethod":
							patient.PreferConfirmMethod=(OpenDentBusiness.ContactMethod)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "PreferContactMethod":
							patient.PreferContactMethod=(OpenDentBusiness.ContactMethod)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "PreferRecallMethod":
							patient.PreferRecallMethod=(OpenDentBusiness.ContactMethod)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "SchedBeforeTime":
							patient.SchedBeforeTime=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "SchedAfterTime":
							patient.SchedAfterTime=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "SchedDayOfWeek":
							patient.SchedDayOfWeek=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "Language":
							patient.Language=reader.ReadContentAsString();
							break;
						case "AdmitDate":
							patient.AdmitDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "Title":
							patient.Title=reader.ReadContentAsString();
							break;
						case "PayPlanDue":
							patient.PayPlanDue=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "SiteNum":
							patient.SiteNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTStamp":
							patient.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ResponsParty":
							patient.ResponsParty=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CanadianEligibilityCode":
							patient.CanadianEligibilityCode=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "AskToArriveEarly":
							patient.AskToArriveEarly=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "OnlinePassword":
							patient.OnlinePassword=reader.ReadContentAsString();
							break;
						case "SmokeStatus":
							patient.SmokeStatus=(OpenDentBusiness.SmokingStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "PreferContactConfidential":
							patient.PreferContactConfidential=(OpenDentBusiness.ContactMethod)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "SuperFamily":
							patient.SuperFamily=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "TxtMsgOk":
							patient.TxtMsgOk=(OpenDentBusiness.YN)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return patient;
		}


	}
}