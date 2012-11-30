package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Patient {
		/** Primary key. */
		public int PatNum;
		/** Last name. */
		public String LName;
		/** First name. */
		public String FName;
		/** Middle initial or name. */
		public String MiddleI;
		/** Preferred name, aka nickname. */
		public String Preferred;
		/** Enum:PatientStatus */
		public PatientStatus PatStatus;
		/** Enum:PatientGender */
		public PatientGender Gender;
		/** Enum:PatientPosition Marital status would probably be a better name for this column. */
		public PatientPosition Position;
		/** Age is not stored in the database.  Age is always calculated as needed from birthdate. */
		public Date Birthdate;
		/** In the US, this is 9 digits, no dashes. For all other countries, any punctuation or format is allowed. */
		public String SSN;
		/** . */
		public String Address;
		/** Optional second address line. */
		public String Address2;
		/** . */
		public String City;
		/** 2 Char in USA */
		public String State;
		/** Postal code.  For Canadian claims, it must be ANANAN.  No validation gets done except there. */
		public String Zip;
		/** Home phone. Includes any punctuation */
		public String HmPhone;
		/** . */
		public String WkPhone;
		/** . */
		public String WirelessPhone;
		/** FK to patient.PatNum.  Head of household. */
		public int Guarantor;
		/** Single char. Shows at upper right corner of appointments.  Suggested use is A,B,or C to designate creditworthiness, but it can actually be used for any purpose. */
		public String CreditType;
		/** . */
		public String Email;
		/** Dear __.  This field does not include the "Dear" or a trailing comma.  If this field is blank, then the typical salutation is FName.  Or, if a Preferred name is present, that is used instead of FName. */
		public String Salutation;
		/** Current patient balance.(not family). Never subtracts insurance estimates. */
		public double EstBalance;
		/** FK to provider.ProvNum.  The patient's primary provider.  Required.  The database maintenance tool ensures that every patient always has this number set, so the program no longer has to handle 0. */
		public int PriProv;
		/** FK to provider.ProvNum.  Secondary provider (hygienist). Optional. */
		public int SecProv;
		/** FK to feesched.FeeSchedNum.  Fee schedule for this patient.  Usually not used.  If missing, the practice default fee schedule is used. If patient has insurance, then the fee schedule for the insplan is used. */
		public int FeeSched;
		/** FK to definition.DefNum.  Must have a value, or the patient will not show on some reports. */
		public int BillingType;
		/** Name of folder where images will be stored. Not editable for now. */
		public String ImageFolder;
		/** Address or phone note.  Unlimited length in order to handle data from other programs during a conversion. */
		public String AddrNote;
		/** Family financial urgent note.  Only stored with guarantor, and shared for family. */
		public String FamFinUrgNote;
		/** Individual patient note for Urgent medical. */
		public String MedUrgNote;
		/** Individual patient note for Appointment module note. */
		public String ApptModNote;
		/** Single char for Nonstudent, Parttime, or Fulltime.  Blank also=Nonstudent */
		public String StudentStatus;
		/** College name.  If Canadian, then this is field C10 and must be filled if C9 (patient.CanadianEligibilityCode) is 1 and patient is 18 or older. */
		public String SchoolName;
		/** Max 15 char.  Used for reference to previous programs. */
		public String ChartNumber;
		/** Optional. The Medicaid ID for this patient. */
		public String MedicaidID;
		/** Aged balance from 0 to 30 days old. Aging numbers are for entire family.  Only stored with guarantor. */
		public double Bal_0_30;
		/** Aged balance from 31 to 60 days old. Aging numbers are for entire family.  Only stored with guarantor. */
		public double Bal_31_60;
		/** Aged balance from 61 to 90 days old. Aging numbers are for entire family.  Only stored with guarantor. */
		public double Bal_61_90;
		/** Aged balance over 90 days old. Aging numbers are for entire family.  Only stored with guarantor. */
		public double BalOver90;
		/** Insurance Estimate for entire family. */
		public double InsEst;
		/** Total balance for entire family before insurance estimate.  Not the same as the sum of the 4 aging balances because this can be negative.  Only stored with guarantor. */
		public double BalTotal;
		/** FK to employer.EmployerNum. */
		public int EmployerNum;
		/** Not used since version 2.8. */
		public String EmploymentNote;
		/** Enum:PatientRace Race and ethnicity. */
		public PatientRace Race;
		/** FK to county.CountyName, although it will not crash if key absent. */
		public String County;
		/** Enum:PatientGrade Gradelevel. */
		public PatientGrade GradeLevel;
		/** Enum:TreatmentUrgency Used in public health screenings. */
		public TreatmentUrgency Urgency;
		/** The date that the patient first visited the office.  Automated. */
		public Date DateFirstVisit;
		/** FK to clinic.ClinicNum. Can be zero if not attached to a clinic or no clinics set up. */
		public int ClinicNum;
		/** For now, an 'I' indicates that the patient has insurance.  This is only used when displaying appointments.  It will later be expanded.  User can't edit. */
		public String HasIns;
		/** The Trophy bridge is inadequate.  This is an attempt to make it usable for offices that have already invested in Trophy hardware. */
		public String TrophyFolder;
		/** This simply indicates whether the 'done' box is checked in the chart module.  Used to be handled as a -1 in the NextAptNum field, but now that field is unsigned. */
		public boolean PlannedIsDone;
		/** Set to true if patient needs to be premedicated for appointments, includes PAC, halcion, etc. */
		public boolean Premed;
		/** Only used in hospitals. */
		public String Ward;
		/** Enum:ContactMethod */
		public ContactMethod PreferConfirmMethod;
		/** Enum:ContactMethod */
		public ContactMethod PreferContactMethod;
		/** Enum:ContactMethod */
		public ContactMethod PreferRecallMethod;
		/** . */
		public String SchedBeforeTime;
		/** . */
		public String SchedAfterTime;
		/** We do not use this, but some users do, so here it is. 0=none. Otherwise, 1-7 for day. */
		public byte SchedDayOfWeek;
		/** The primary language of the patient.  Typically en, fr, es, or similar.  If it's a custom language, then it might look like Tahitian. */
		public String Language;
		/** Used in hospitals.  It can be before the first visit date.  It typically gets set automatically by the hospital system. */
		public Date AdmitDate;
		/** Includes any punctuation.  For example, Mr., Mrs., Miss, Dr., etc.  There is no selection mechanism yet for user; they must simply type it in. */
		public String Title;
		/** . */
		public double PayPlanDue;
		/** FK to site.SiteNum. Can be zero. Replaces the old GradeSchool field with a proper foreign key. */
		public int SiteNum;
		/** The last date and time this row was altered.  Not user editable. */
		public Date DateTStamp;
		/** FK to patient.PatNum. Can be zero.  Person responsible for medical decisions rather than finances.  Guarantor is still responsible for finances.  This is useful for nursing home residents.  Part of public health. */
		public int ResponsParty;
		/** C09.  Eligibility Exception Code.  A single digit 1-4.  0 is not acceptable for e-claims. 1=FT student, 2=disabled, 3=disabled student, 4=code not applicable.  Warning.  4 is a 0 if using CDAnet version 02. */
		public byte CanadianEligibilityCode;
		/** Number of minutes patient is asked to come early to appointments. */
		public int AskToArriveEarly;
		/** If this is blank, then the chart info for this patient will not be uploaded.  If this has a value, then this is the password that a patient must use to access their info online. */
		public String OnlinePassword;
		/** Enum:SmokingStatus  0=UnknownIfEver,1=SmokerUnkownCurrent,2=NeverSmoked,3=FormerSmoker,4=CurrentSomeDay,5=CurrentEveryDay */
		public SmokingStatus SmokeStatus;
		/** Enum:ContactMethod  Used for EHR. */
		public ContactMethod PreferContactConfidential;
		/** FK to patient.PatNum.  If this is the same as PatNum, then this is a SuperHead.  If zero, then not part of a superfamily.  Synched for entire family.  If family is part of a superfamily, then the guarantor for this family will show in the superfamily list in the Family module for anyone else who is in the superfamily.  Only a guarantor can be a superfamily head. */
		public int SuperFamily;
		/** Enum:YN */
		public YN TxtMsgOk;

		/** Deep copy of object. */
		public Patient Copy() {
			Patient patient=new Patient();
			patient.PatNum=this.PatNum;
			patient.LName=this.LName;
			patient.FName=this.FName;
			patient.MiddleI=this.MiddleI;
			patient.Preferred=this.Preferred;
			patient.PatStatus=this.PatStatus;
			patient.Gender=this.Gender;
			patient.Position=this.Position;
			patient.Birthdate=this.Birthdate;
			patient.SSN=this.SSN;
			patient.Address=this.Address;
			patient.Address2=this.Address2;
			patient.City=this.City;
			patient.State=this.State;
			patient.Zip=this.Zip;
			patient.HmPhone=this.HmPhone;
			patient.WkPhone=this.WkPhone;
			patient.WirelessPhone=this.WirelessPhone;
			patient.Guarantor=this.Guarantor;
			patient.CreditType=this.CreditType;
			patient.Email=this.Email;
			patient.Salutation=this.Salutation;
			patient.EstBalance=this.EstBalance;
			patient.PriProv=this.PriProv;
			patient.SecProv=this.SecProv;
			patient.FeeSched=this.FeeSched;
			patient.BillingType=this.BillingType;
			patient.ImageFolder=this.ImageFolder;
			patient.AddrNote=this.AddrNote;
			patient.FamFinUrgNote=this.FamFinUrgNote;
			patient.MedUrgNote=this.MedUrgNote;
			patient.ApptModNote=this.ApptModNote;
			patient.StudentStatus=this.StudentStatus;
			patient.SchoolName=this.SchoolName;
			patient.ChartNumber=this.ChartNumber;
			patient.MedicaidID=this.MedicaidID;
			patient.Bal_0_30=this.Bal_0_30;
			patient.Bal_31_60=this.Bal_31_60;
			patient.Bal_61_90=this.Bal_61_90;
			patient.BalOver90=this.BalOver90;
			patient.InsEst=this.InsEst;
			patient.BalTotal=this.BalTotal;
			patient.EmployerNum=this.EmployerNum;
			patient.EmploymentNote=this.EmploymentNote;
			patient.Race=this.Race;
			patient.County=this.County;
			patient.GradeLevel=this.GradeLevel;
			patient.Urgency=this.Urgency;
			patient.DateFirstVisit=this.DateFirstVisit;
			patient.ClinicNum=this.ClinicNum;
			patient.HasIns=this.HasIns;
			patient.TrophyFolder=this.TrophyFolder;
			patient.PlannedIsDone=this.PlannedIsDone;
			patient.Premed=this.Premed;
			patient.Ward=this.Ward;
			patient.PreferConfirmMethod=this.PreferConfirmMethod;
			patient.PreferContactMethod=this.PreferContactMethod;
			patient.PreferRecallMethod=this.PreferRecallMethod;
			patient.SchedBeforeTime=this.SchedBeforeTime;
			patient.SchedAfterTime=this.SchedAfterTime;
			patient.SchedDayOfWeek=this.SchedDayOfWeek;
			patient.Language=this.Language;
			patient.AdmitDate=this.AdmitDate;
			patient.Title=this.Title;
			patient.PayPlanDue=this.PayPlanDue;
			patient.SiteNum=this.SiteNum;
			patient.DateTStamp=this.DateTStamp;
			patient.ResponsParty=this.ResponsParty;
			patient.CanadianEligibilityCode=this.CanadianEligibilityCode;
			patient.AskToArriveEarly=this.AskToArriveEarly;
			patient.OnlinePassword=this.OnlinePassword;
			patient.SmokeStatus=this.SmokeStatus;
			patient.PreferContactConfidential=this.PreferContactConfidential;
			patient.SuperFamily=this.SuperFamily;
			patient.TxtMsgOk=this.TxtMsgOk;
			return patient;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Patient>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<LName>").append(Serializing.EscapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.EscapeForXml(FName)).append("</FName>");
			sb.append("<MiddleI>").append(Serializing.EscapeForXml(MiddleI)).append("</MiddleI>");
			sb.append("<Preferred>").append(Serializing.EscapeForXml(Preferred)).append("</Preferred>");
			sb.append("<PatStatus>").append(PatStatus.ordinal()).append("</PatStatus>");
			sb.append("<Gender>").append(Gender.ordinal()).append("</Gender>");
			sb.append("<Position>").append(Position.ordinal()).append("</Position>");
			sb.append("<Birthdate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(Birthdate)).append("</Birthdate>");
			sb.append("<SSN>").append(Serializing.EscapeForXml(SSN)).append("</SSN>");
			sb.append("<Address>").append(Serializing.EscapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.EscapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.EscapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.EscapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.EscapeForXml(Zip)).append("</Zip>");
			sb.append("<HmPhone>").append(Serializing.EscapeForXml(HmPhone)).append("</HmPhone>");
			sb.append("<WkPhone>").append(Serializing.EscapeForXml(WkPhone)).append("</WkPhone>");
			sb.append("<WirelessPhone>").append(Serializing.EscapeForXml(WirelessPhone)).append("</WirelessPhone>");
			sb.append("<Guarantor>").append(Guarantor).append("</Guarantor>");
			sb.append("<CreditType>").append(Serializing.EscapeForXml(CreditType)).append("</CreditType>");
			sb.append("<Email>").append(Serializing.EscapeForXml(Email)).append("</Email>");
			sb.append("<Salutation>").append(Serializing.EscapeForXml(Salutation)).append("</Salutation>");
			sb.append("<EstBalance>").append(EstBalance).append("</EstBalance>");
			sb.append("<PriProv>").append(PriProv).append("</PriProv>");
			sb.append("<SecProv>").append(SecProv).append("</SecProv>");
			sb.append("<FeeSched>").append(FeeSched).append("</FeeSched>");
			sb.append("<BillingType>").append(BillingType).append("</BillingType>");
			sb.append("<ImageFolder>").append(Serializing.EscapeForXml(ImageFolder)).append("</ImageFolder>");
			sb.append("<AddrNote>").append(Serializing.EscapeForXml(AddrNote)).append("</AddrNote>");
			sb.append("<FamFinUrgNote>").append(Serializing.EscapeForXml(FamFinUrgNote)).append("</FamFinUrgNote>");
			sb.append("<MedUrgNote>").append(Serializing.EscapeForXml(MedUrgNote)).append("</MedUrgNote>");
			sb.append("<ApptModNote>").append(Serializing.EscapeForXml(ApptModNote)).append("</ApptModNote>");
			sb.append("<StudentStatus>").append(Serializing.EscapeForXml(StudentStatus)).append("</StudentStatus>");
			sb.append("<SchoolName>").append(Serializing.EscapeForXml(SchoolName)).append("</SchoolName>");
			sb.append("<ChartNumber>").append(Serializing.EscapeForXml(ChartNumber)).append("</ChartNumber>");
			sb.append("<MedicaidID>").append(Serializing.EscapeForXml(MedicaidID)).append("</MedicaidID>");
			sb.append("<Bal_0_30>").append(Bal_0_30).append("</Bal_0_30>");
			sb.append("<Bal_31_60>").append(Bal_31_60).append("</Bal_31_60>");
			sb.append("<Bal_61_90>").append(Bal_61_90).append("</Bal_61_90>");
			sb.append("<BalOver90>").append(BalOver90).append("</BalOver90>");
			sb.append("<InsEst>").append(InsEst).append("</InsEst>");
			sb.append("<BalTotal>").append(BalTotal).append("</BalTotal>");
			sb.append("<EmployerNum>").append(EmployerNum).append("</EmployerNum>");
			sb.append("<EmploymentNote>").append(Serializing.EscapeForXml(EmploymentNote)).append("</EmploymentNote>");
			sb.append("<Race>").append(Race.ordinal()).append("</Race>");
			sb.append("<County>").append(Serializing.EscapeForXml(County)).append("</County>");
			sb.append("<GradeLevel>").append(GradeLevel.ordinal()).append("</GradeLevel>");
			sb.append("<Urgency>").append(Urgency.ordinal()).append("</Urgency>");
			sb.append("<DateFirstVisit>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateFirstVisit)).append("</DateFirstVisit>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<HasIns>").append(Serializing.EscapeForXml(HasIns)).append("</HasIns>");
			sb.append("<TrophyFolder>").append(Serializing.EscapeForXml(TrophyFolder)).append("</TrophyFolder>");
			sb.append("<PlannedIsDone>").append((PlannedIsDone)?1:0).append("</PlannedIsDone>");
			sb.append("<Premed>").append((Premed)?1:0).append("</Premed>");
			sb.append("<Ward>").append(Serializing.EscapeForXml(Ward)).append("</Ward>");
			sb.append("<PreferConfirmMethod>").append(PreferConfirmMethod.ordinal()).append("</PreferConfirmMethod>");
			sb.append("<PreferContactMethod>").append(PreferContactMethod.ordinal()).append("</PreferContactMethod>");
			sb.append("<PreferRecallMethod>").append(PreferRecallMethod.ordinal()).append("</PreferRecallMethod>");
			sb.append("<SchedBeforeTime>").append(Serializing.EscapeForXml(SchedBeforeTime)).append("</SchedBeforeTime>");
			sb.append("<SchedAfterTime>").append(Serializing.EscapeForXml(SchedAfterTime)).append("</SchedAfterTime>");
			sb.append("<SchedDayOfWeek>").append(SchedDayOfWeek).append("</SchedDayOfWeek>");
			sb.append("<Language>").append(Serializing.EscapeForXml(Language)).append("</Language>");
			sb.append("<AdmitDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AdmitDate)).append("</AdmitDate>");
			sb.append("<Title>").append(Serializing.EscapeForXml(Title)).append("</Title>");
			sb.append("<PayPlanDue>").append(PayPlanDue).append("</PayPlanDue>");
			sb.append("<SiteNum>").append(SiteNum).append("</SiteNum>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<ResponsParty>").append(ResponsParty).append("</ResponsParty>");
			sb.append("<CanadianEligibilityCode>").append(CanadianEligibilityCode).append("</CanadianEligibilityCode>");
			sb.append("<AskToArriveEarly>").append(AskToArriveEarly).append("</AskToArriveEarly>");
			sb.append("<OnlinePassword>").append(Serializing.EscapeForXml(OnlinePassword)).append("</OnlinePassword>");
			sb.append("<SmokeStatus>").append(SmokeStatus.ordinal()).append("</SmokeStatus>");
			sb.append("<PreferContactConfidential>").append(PreferContactConfidential.ordinal()).append("</PreferContactConfidential>");
			sb.append("<SuperFamily>").append(SuperFamily).append("</SuperFamily>");
			sb.append("<TxtMsgOk>").append(TxtMsgOk.ordinal()).append("</TxtMsgOk>");
			sb.append("</Patient>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.GetXmlNodeValue(doc,"LName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.GetXmlNodeValue(doc,"FName");
				}
				if(Serializing.GetXmlNodeValue(doc,"MiddleI")!=null) {
					MiddleI=Serializing.GetXmlNodeValue(doc,"MiddleI");
				}
				if(Serializing.GetXmlNodeValue(doc,"Preferred")!=null) {
					Preferred=Serializing.GetXmlNodeValue(doc,"Preferred");
				}
				if(Serializing.GetXmlNodeValue(doc,"PatStatus")!=null) {
					PatStatus=PatientStatus.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatStatus"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Gender")!=null) {
					Gender=PatientGender.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Gender"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Position")!=null) {
					Position=PatientPosition.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Position"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Birthdate")!=null) {
					Birthdate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"Birthdate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SSN")!=null) {
					SSN=Serializing.GetXmlNodeValue(doc,"SSN");
				}
				if(Serializing.GetXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.GetXmlNodeValue(doc,"Address");
				}
				if(Serializing.GetXmlNodeValue(doc,"Address2")!=null) {
					Address2=Serializing.GetXmlNodeValue(doc,"Address2");
				}
				if(Serializing.GetXmlNodeValue(doc,"City")!=null) {
					City=Serializing.GetXmlNodeValue(doc,"City");
				}
				if(Serializing.GetXmlNodeValue(doc,"State")!=null) {
					State=Serializing.GetXmlNodeValue(doc,"State");
				}
				if(Serializing.GetXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.GetXmlNodeValue(doc,"Zip");
				}
				if(Serializing.GetXmlNodeValue(doc,"HmPhone")!=null) {
					HmPhone=Serializing.GetXmlNodeValue(doc,"HmPhone");
				}
				if(Serializing.GetXmlNodeValue(doc,"WkPhone")!=null) {
					WkPhone=Serializing.GetXmlNodeValue(doc,"WkPhone");
				}
				if(Serializing.GetXmlNodeValue(doc,"WirelessPhone")!=null) {
					WirelessPhone=Serializing.GetXmlNodeValue(doc,"WirelessPhone");
				}
				if(Serializing.GetXmlNodeValue(doc,"Guarantor")!=null) {
					Guarantor=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Guarantor"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CreditType")!=null) {
					CreditType=Serializing.GetXmlNodeValue(doc,"CreditType");
				}
				if(Serializing.GetXmlNodeValue(doc,"Email")!=null) {
					Email=Serializing.GetXmlNodeValue(doc,"Email");
				}
				if(Serializing.GetXmlNodeValue(doc,"Salutation")!=null) {
					Salutation=Serializing.GetXmlNodeValue(doc,"Salutation");
				}
				if(Serializing.GetXmlNodeValue(doc,"EstBalance")!=null) {
					EstBalance=Double.valueOf(Serializing.GetXmlNodeValue(doc,"EstBalance"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PriProv")!=null) {
					PriProv=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PriProv"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SecProv")!=null) {
					SecProv=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SecProv"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FeeSched")!=null) {
					FeeSched=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FeeSched"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BillingType")!=null) {
					BillingType=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"BillingType"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ImageFolder")!=null) {
					ImageFolder=Serializing.GetXmlNodeValue(doc,"ImageFolder");
				}
				if(Serializing.GetXmlNodeValue(doc,"AddrNote")!=null) {
					AddrNote=Serializing.GetXmlNodeValue(doc,"AddrNote");
				}
				if(Serializing.GetXmlNodeValue(doc,"FamFinUrgNote")!=null) {
					FamFinUrgNote=Serializing.GetXmlNodeValue(doc,"FamFinUrgNote");
				}
				if(Serializing.GetXmlNodeValue(doc,"MedUrgNote")!=null) {
					MedUrgNote=Serializing.GetXmlNodeValue(doc,"MedUrgNote");
				}
				if(Serializing.GetXmlNodeValue(doc,"ApptModNote")!=null) {
					ApptModNote=Serializing.GetXmlNodeValue(doc,"ApptModNote");
				}
				if(Serializing.GetXmlNodeValue(doc,"StudentStatus")!=null) {
					StudentStatus=Serializing.GetXmlNodeValue(doc,"StudentStatus");
				}
				if(Serializing.GetXmlNodeValue(doc,"SchoolName")!=null) {
					SchoolName=Serializing.GetXmlNodeValue(doc,"SchoolName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ChartNumber")!=null) {
					ChartNumber=Serializing.GetXmlNodeValue(doc,"ChartNumber");
				}
				if(Serializing.GetXmlNodeValue(doc,"MedicaidID")!=null) {
					MedicaidID=Serializing.GetXmlNodeValue(doc,"MedicaidID");
				}
				if(Serializing.GetXmlNodeValue(doc,"Bal_0_30")!=null) {
					Bal_0_30=Double.valueOf(Serializing.GetXmlNodeValue(doc,"Bal_0_30"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Bal_31_60")!=null) {
					Bal_31_60=Double.valueOf(Serializing.GetXmlNodeValue(doc,"Bal_31_60"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Bal_61_90")!=null) {
					Bal_61_90=Double.valueOf(Serializing.GetXmlNodeValue(doc,"Bal_61_90"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BalOver90")!=null) {
					BalOver90=Double.valueOf(Serializing.GetXmlNodeValue(doc,"BalOver90"));
				}
				if(Serializing.GetXmlNodeValue(doc,"InsEst")!=null) {
					InsEst=Double.valueOf(Serializing.GetXmlNodeValue(doc,"InsEst"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BalTotal")!=null) {
					BalTotal=Double.valueOf(Serializing.GetXmlNodeValue(doc,"BalTotal"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EmployerNum")!=null) {
					EmployerNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployerNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EmploymentNote")!=null) {
					EmploymentNote=Serializing.GetXmlNodeValue(doc,"EmploymentNote");
				}
				if(Serializing.GetXmlNodeValue(doc,"Race")!=null) {
					Race=PatientRace.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Race"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"County")!=null) {
					County=Serializing.GetXmlNodeValue(doc,"County");
				}
				if(Serializing.GetXmlNodeValue(doc,"GradeLevel")!=null) {
					GradeLevel=PatientGrade.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"GradeLevel"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Urgency")!=null) {
					Urgency=TreatmentUrgency.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Urgency"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"DateFirstVisit")!=null) {
					DateFirstVisit=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateFirstVisit"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"HasIns")!=null) {
					HasIns=Serializing.GetXmlNodeValue(doc,"HasIns");
				}
				if(Serializing.GetXmlNodeValue(doc,"TrophyFolder")!=null) {
					TrophyFolder=Serializing.GetXmlNodeValue(doc,"TrophyFolder");
				}
				if(Serializing.GetXmlNodeValue(doc,"PlannedIsDone")!=null) {
					PlannedIsDone=(Serializing.GetXmlNodeValue(doc,"PlannedIsDone")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"Premed")!=null) {
					Premed=(Serializing.GetXmlNodeValue(doc,"Premed")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"Ward")!=null) {
					Ward=Serializing.GetXmlNodeValue(doc,"Ward");
				}
				if(Serializing.GetXmlNodeValue(doc,"PreferConfirmMethod")!=null) {
					PreferConfirmMethod=ContactMethod.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PreferConfirmMethod"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"PreferContactMethod")!=null) {
					PreferContactMethod=ContactMethod.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PreferContactMethod"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"PreferRecallMethod")!=null) {
					PreferRecallMethod=ContactMethod.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PreferRecallMethod"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"SchedBeforeTime")!=null) {
					SchedBeforeTime=Serializing.GetXmlNodeValue(doc,"SchedBeforeTime");
				}
				if(Serializing.GetXmlNodeValue(doc,"SchedAfterTime")!=null) {
					SchedAfterTime=Serializing.GetXmlNodeValue(doc,"SchedAfterTime");
				}
				if(Serializing.GetXmlNodeValue(doc,"SchedDayOfWeek")!=null) {
					SchedDayOfWeek=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"SchedDayOfWeek"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Language")!=null) {
					Language=Serializing.GetXmlNodeValue(doc,"Language");
				}
				if(Serializing.GetXmlNodeValue(doc,"AdmitDate")!=null) {
					AdmitDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"AdmitDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Title")!=null) {
					Title=Serializing.GetXmlNodeValue(doc,"Title");
				}
				if(Serializing.GetXmlNodeValue(doc,"PayPlanDue")!=null) {
					PayPlanDue=Double.valueOf(Serializing.GetXmlNodeValue(doc,"PayPlanDue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SiteNum")!=null) {
					SiteNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SiteNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ResponsParty")!=null) {
					ResponsParty=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ResponsParty"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianEligibilityCode")!=null) {
					CanadianEligibilityCode=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"CanadianEligibilityCode"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AskToArriveEarly")!=null) {
					AskToArriveEarly=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AskToArriveEarly"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OnlinePassword")!=null) {
					OnlinePassword=Serializing.GetXmlNodeValue(doc,"OnlinePassword");
				}
				if(Serializing.GetXmlNodeValue(doc,"SmokeStatus")!=null) {
					SmokeStatus=SmokingStatus.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SmokeStatus"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"PreferContactConfidential")!=null) {
					PreferContactConfidential=ContactMethod.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PreferContactConfidential"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"SuperFamily")!=null) {
					SuperFamily=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SuperFamily"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TxtMsgOk")!=null) {
					TxtMsgOk=YN.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TxtMsgOk"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum PatientStatus {
			/** 0 */
			Patient,
			/** 1 */
			NonPatient,
			/** 2 */
			Inactive,
			/** 3 */
			Archived,
			/** 4 */
			Deleted,
			/** 5 */
			Deceased,
			/** 6- Not an actual patient yet. */
			Prospective
		}

		/**  */
		public enum PatientGender {
			/** 0 */
			Male,
			/** 1 */
			Female,
			/** 2- This is not a joke. Required by HIPAA for privacy.  Required by ehr to track missing entries. */
			Unknown
		}

		/**  */
		public enum PatientPosition {
			/** 0 */
			Single,
			/** 1 */
			Married,
			/** 2 */
			Child,
			/** 3 */
			Widowed,
			/** 4 */
			Divorced
		}

		/** Race and ethnicity for patient. Used by public health.  The problem is that everyone seems to want different choices.  If we give these choices their own table, then we also need to include mapping functions.  These are currently used in ArizonaReports, HL7 w ECW, and EHR.  Foreign users would like their own mappings. */
		public enum PatientRace {
			/** 0 */
			Unknown,
			/** 1 */
			Multiracial,
			/** 2 */
			HispanicLatino,
			/** 3 */
			AfricanAmerican,
			/** 4 */
			White,
			/** 5 */
			HawaiiOrPacIsland,
			/** 6 */
			AmericanIndian,
			/** 7 */
			Asian,
			/** 8 */
			Other,
			/** 9 */
			Aboriginal,
			/** 10 - Required by EHR, even though it's stupid. */
			BlackHispanic
		}

		/** Grade level used in public health. */
		public enum PatientGrade {
			/** 0 */
			Unknown,
			/** 1 */
			First,
			/** 2 */
			Second,
			/** 3 */
			Third,
			/** 4 */
			Fourth,
			/** 5 */
			Fifth,
			/** 6 */
			Sixth,
			/** 7 */
			Seventh,
			/** 8 */
			Eighth,
			/** 9 */
			Ninth,
			/** 10 */
			Tenth,
			/** 11 */
			Eleventh,
			/** 12 */
			Twelfth,
			/** 13 */
			PrenatalWIC,
			/** 14 */
			PreK,
			/** 15 */
			Kindergarten,
			/** 16 */
			Other
		}

		/** For public health.  Unknown, NoProblems, NeedsCarE, or Urgent. */
		public enum TreatmentUrgency {
			/**  */
			Unknown,
			/**  */
			NoProblems,
			/**  */
			NeedsCare,
			/**  */
			Urgent
		}

		/**  */
		public enum ContactMethod {
			/** 0 */
			None,
			/** 1 */
			DoNotCall,
			/** 2 */
			HmPhone,
			/** 3 */
			WkPhone,
			/** 4 */
			WirelessPh,
			/** 5 */
			Email,
			/** 6 */
			SeeNotes,
			/** 7 */
			Mail,
			/** 8 */
			TextMessage
		}

		/** 0=UnknownIfEver,1=SmokerUnkownCurrent,2=NeverSmoked,3=FormerSmoker,4=CurrentSomeDay,5=CurrentEveryDay */
		public enum SmokingStatus {
			/** 0 */
			UnknownIfEver_Recode9,
			/** 1 */
			SmokerUnknownCurrent_Recode5,
			/** 2 */
			NeverSmoked_Recode4,
			/** 3 */
			FormerSmoker_Recode3,
			/** 4 */
			CurrentSomeDay_Recode2,
			/** 5 */
			CurrentEveryDay_Recode1
		}

		/** Unknown,Yes, or No. */
		public enum YN {
			/** 0 */
			Unknown,
			/** 1 */
			Yes,
			/** 2 */
			No
		}


}
