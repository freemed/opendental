package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		/** Derived from Birthdate.  Not in the database table. */
		public int Age;
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
		public Patient deepCopy() {
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
			patient.Age=this.Age;
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Patient>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<LName>").append(Serializing.escapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.escapeForXml(FName)).append("</FName>");
			sb.append("<MiddleI>").append(Serializing.escapeForXml(MiddleI)).append("</MiddleI>");
			sb.append("<Preferred>").append(Serializing.escapeForXml(Preferred)).append("</Preferred>");
			sb.append("<PatStatus>").append(PatStatus.ordinal()).append("</PatStatus>");
			sb.append("<Gender>").append(Gender.ordinal()).append("</Gender>");
			sb.append("<Position>").append(Position.ordinal()).append("</Position>");
			sb.append("<Birthdate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(Birthdate)).append("</Birthdate>");
			sb.append("<SSN>").append(Serializing.escapeForXml(SSN)).append("</SSN>");
			sb.append("<Address>").append(Serializing.escapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.escapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.escapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.escapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.escapeForXml(Zip)).append("</Zip>");
			sb.append("<HmPhone>").append(Serializing.escapeForXml(HmPhone)).append("</HmPhone>");
			sb.append("<WkPhone>").append(Serializing.escapeForXml(WkPhone)).append("</WkPhone>");
			sb.append("<WirelessPhone>").append(Serializing.escapeForXml(WirelessPhone)).append("</WirelessPhone>");
			sb.append("<Guarantor>").append(Guarantor).append("</Guarantor>");
			sb.append("<Age>").append(Age).append("</Age>");
			sb.append("<CreditType>").append(Serializing.escapeForXml(CreditType)).append("</CreditType>");
			sb.append("<Email>").append(Serializing.escapeForXml(Email)).append("</Email>");
			sb.append("<Salutation>").append(Serializing.escapeForXml(Salutation)).append("</Salutation>");
			sb.append("<EstBalance>").append(EstBalance).append("</EstBalance>");
			sb.append("<PriProv>").append(PriProv).append("</PriProv>");
			sb.append("<SecProv>").append(SecProv).append("</SecProv>");
			sb.append("<FeeSched>").append(FeeSched).append("</FeeSched>");
			sb.append("<BillingType>").append(BillingType).append("</BillingType>");
			sb.append("<ImageFolder>").append(Serializing.escapeForXml(ImageFolder)).append("</ImageFolder>");
			sb.append("<AddrNote>").append(Serializing.escapeForXml(AddrNote)).append("</AddrNote>");
			sb.append("<FamFinUrgNote>").append(Serializing.escapeForXml(FamFinUrgNote)).append("</FamFinUrgNote>");
			sb.append("<MedUrgNote>").append(Serializing.escapeForXml(MedUrgNote)).append("</MedUrgNote>");
			sb.append("<ApptModNote>").append(Serializing.escapeForXml(ApptModNote)).append("</ApptModNote>");
			sb.append("<StudentStatus>").append(Serializing.escapeForXml(StudentStatus)).append("</StudentStatus>");
			sb.append("<SchoolName>").append(Serializing.escapeForXml(SchoolName)).append("</SchoolName>");
			sb.append("<ChartNumber>").append(Serializing.escapeForXml(ChartNumber)).append("</ChartNumber>");
			sb.append("<MedicaidID>").append(Serializing.escapeForXml(MedicaidID)).append("</MedicaidID>");
			sb.append("<Bal_0_30>").append(Bal_0_30).append("</Bal_0_30>");
			sb.append("<Bal_31_60>").append(Bal_31_60).append("</Bal_31_60>");
			sb.append("<Bal_61_90>").append(Bal_61_90).append("</Bal_61_90>");
			sb.append("<BalOver90>").append(BalOver90).append("</BalOver90>");
			sb.append("<InsEst>").append(InsEst).append("</InsEst>");
			sb.append("<BalTotal>").append(BalTotal).append("</BalTotal>");
			sb.append("<EmployerNum>").append(EmployerNum).append("</EmployerNum>");
			sb.append("<EmploymentNote>").append(Serializing.escapeForXml(EmploymentNote)).append("</EmploymentNote>");
			sb.append("<Race>").append(Race.ordinal()).append("</Race>");
			sb.append("<County>").append(Serializing.escapeForXml(County)).append("</County>");
			sb.append("<GradeLevel>").append(GradeLevel.ordinal()).append("</GradeLevel>");
			sb.append("<Urgency>").append(Urgency.ordinal()).append("</Urgency>");
			sb.append("<DateFirstVisit>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateFirstVisit)).append("</DateFirstVisit>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<HasIns>").append(Serializing.escapeForXml(HasIns)).append("</HasIns>");
			sb.append("<TrophyFolder>").append(Serializing.escapeForXml(TrophyFolder)).append("</TrophyFolder>");
			sb.append("<PlannedIsDone>").append((PlannedIsDone)?1:0).append("</PlannedIsDone>");
			sb.append("<Premed>").append((Premed)?1:0).append("</Premed>");
			sb.append("<Ward>").append(Serializing.escapeForXml(Ward)).append("</Ward>");
			sb.append("<PreferConfirmMethod>").append(PreferConfirmMethod.ordinal()).append("</PreferConfirmMethod>");
			sb.append("<PreferContactMethod>").append(PreferContactMethod.ordinal()).append("</PreferContactMethod>");
			sb.append("<PreferRecallMethod>").append(PreferRecallMethod.ordinal()).append("</PreferRecallMethod>");
			sb.append("<SchedBeforeTime>").append(Serializing.escapeForXml(SchedBeforeTime)).append("</SchedBeforeTime>");
			sb.append("<SchedAfterTime>").append(Serializing.escapeForXml(SchedAfterTime)).append("</SchedAfterTime>");
			sb.append("<SchedDayOfWeek>").append(SchedDayOfWeek).append("</SchedDayOfWeek>");
			sb.append("<Language>").append(Serializing.escapeForXml(Language)).append("</Language>");
			sb.append("<AdmitDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AdmitDate)).append("</AdmitDate>");
			sb.append("<Title>").append(Serializing.escapeForXml(Title)).append("</Title>");
			sb.append("<PayPlanDue>").append(PayPlanDue).append("</PayPlanDue>");
			sb.append("<SiteNum>").append(SiteNum).append("</SiteNum>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<ResponsParty>").append(ResponsParty).append("</ResponsParty>");
			sb.append("<CanadianEligibilityCode>").append(CanadianEligibilityCode).append("</CanadianEligibilityCode>");
			sb.append("<AskToArriveEarly>").append(AskToArriveEarly).append("</AskToArriveEarly>");
			sb.append("<OnlinePassword>").append(Serializing.escapeForXml(OnlinePassword)).append("</OnlinePassword>");
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
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.getXmlNodeValue(doc,"LName");
				}
				if(Serializing.getXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.getXmlNodeValue(doc,"FName");
				}
				if(Serializing.getXmlNodeValue(doc,"MiddleI")!=null) {
					MiddleI=Serializing.getXmlNodeValue(doc,"MiddleI");
				}
				if(Serializing.getXmlNodeValue(doc,"Preferred")!=null) {
					Preferred=Serializing.getXmlNodeValue(doc,"Preferred");
				}
				if(Serializing.getXmlNodeValue(doc,"PatStatus")!=null) {
					PatStatus=PatientStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Gender")!=null) {
					Gender=PatientGender.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Gender"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Position")!=null) {
					Position=PatientPosition.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Position"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Birthdate")!=null) {
					Birthdate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"Birthdate"));
				}
				if(Serializing.getXmlNodeValue(doc,"SSN")!=null) {
					SSN=Serializing.getXmlNodeValue(doc,"SSN");
				}
				if(Serializing.getXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.getXmlNodeValue(doc,"Address");
				}
				if(Serializing.getXmlNodeValue(doc,"Address2")!=null) {
					Address2=Serializing.getXmlNodeValue(doc,"Address2");
				}
				if(Serializing.getXmlNodeValue(doc,"City")!=null) {
					City=Serializing.getXmlNodeValue(doc,"City");
				}
				if(Serializing.getXmlNodeValue(doc,"State")!=null) {
					State=Serializing.getXmlNodeValue(doc,"State");
				}
				if(Serializing.getXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.getXmlNodeValue(doc,"Zip");
				}
				if(Serializing.getXmlNodeValue(doc,"HmPhone")!=null) {
					HmPhone=Serializing.getXmlNodeValue(doc,"HmPhone");
				}
				if(Serializing.getXmlNodeValue(doc,"WkPhone")!=null) {
					WkPhone=Serializing.getXmlNodeValue(doc,"WkPhone");
				}
				if(Serializing.getXmlNodeValue(doc,"WirelessPhone")!=null) {
					WirelessPhone=Serializing.getXmlNodeValue(doc,"WirelessPhone");
				}
				if(Serializing.getXmlNodeValue(doc,"Guarantor")!=null) {
					Guarantor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Guarantor"));
				}
				if(Serializing.getXmlNodeValue(doc,"Age")!=null) {
					Age=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Age"));
				}
				if(Serializing.getXmlNodeValue(doc,"CreditType")!=null) {
					CreditType=Serializing.getXmlNodeValue(doc,"CreditType");
				}
				if(Serializing.getXmlNodeValue(doc,"Email")!=null) {
					Email=Serializing.getXmlNodeValue(doc,"Email");
				}
				if(Serializing.getXmlNodeValue(doc,"Salutation")!=null) {
					Salutation=Serializing.getXmlNodeValue(doc,"Salutation");
				}
				if(Serializing.getXmlNodeValue(doc,"EstBalance")!=null) {
					EstBalance=Double.valueOf(Serializing.getXmlNodeValue(doc,"EstBalance"));
				}
				if(Serializing.getXmlNodeValue(doc,"PriProv")!=null) {
					PriProv=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PriProv"));
				}
				if(Serializing.getXmlNodeValue(doc,"SecProv")!=null) {
					SecProv=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SecProv"));
				}
				if(Serializing.getXmlNodeValue(doc,"FeeSched")!=null) {
					FeeSched=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FeeSched"));
				}
				if(Serializing.getXmlNodeValue(doc,"BillingType")!=null) {
					BillingType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"BillingType"));
				}
				if(Serializing.getXmlNodeValue(doc,"ImageFolder")!=null) {
					ImageFolder=Serializing.getXmlNodeValue(doc,"ImageFolder");
				}
				if(Serializing.getXmlNodeValue(doc,"AddrNote")!=null) {
					AddrNote=Serializing.getXmlNodeValue(doc,"AddrNote");
				}
				if(Serializing.getXmlNodeValue(doc,"FamFinUrgNote")!=null) {
					FamFinUrgNote=Serializing.getXmlNodeValue(doc,"FamFinUrgNote");
				}
				if(Serializing.getXmlNodeValue(doc,"MedUrgNote")!=null) {
					MedUrgNote=Serializing.getXmlNodeValue(doc,"MedUrgNote");
				}
				if(Serializing.getXmlNodeValue(doc,"ApptModNote")!=null) {
					ApptModNote=Serializing.getXmlNodeValue(doc,"ApptModNote");
				}
				if(Serializing.getXmlNodeValue(doc,"StudentStatus")!=null) {
					StudentStatus=Serializing.getXmlNodeValue(doc,"StudentStatus");
				}
				if(Serializing.getXmlNodeValue(doc,"SchoolName")!=null) {
					SchoolName=Serializing.getXmlNodeValue(doc,"SchoolName");
				}
				if(Serializing.getXmlNodeValue(doc,"ChartNumber")!=null) {
					ChartNumber=Serializing.getXmlNodeValue(doc,"ChartNumber");
				}
				if(Serializing.getXmlNodeValue(doc,"MedicaidID")!=null) {
					MedicaidID=Serializing.getXmlNodeValue(doc,"MedicaidID");
				}
				if(Serializing.getXmlNodeValue(doc,"Bal_0_30")!=null) {
					Bal_0_30=Double.valueOf(Serializing.getXmlNodeValue(doc,"Bal_0_30"));
				}
				if(Serializing.getXmlNodeValue(doc,"Bal_31_60")!=null) {
					Bal_31_60=Double.valueOf(Serializing.getXmlNodeValue(doc,"Bal_31_60"));
				}
				if(Serializing.getXmlNodeValue(doc,"Bal_61_90")!=null) {
					Bal_61_90=Double.valueOf(Serializing.getXmlNodeValue(doc,"Bal_61_90"));
				}
				if(Serializing.getXmlNodeValue(doc,"BalOver90")!=null) {
					BalOver90=Double.valueOf(Serializing.getXmlNodeValue(doc,"BalOver90"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsEst")!=null) {
					InsEst=Double.valueOf(Serializing.getXmlNodeValue(doc,"InsEst"));
				}
				if(Serializing.getXmlNodeValue(doc,"BalTotal")!=null) {
					BalTotal=Double.valueOf(Serializing.getXmlNodeValue(doc,"BalTotal"));
				}
				if(Serializing.getXmlNodeValue(doc,"EmployerNum")!=null) {
					EmployerNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmployerNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"EmploymentNote")!=null) {
					EmploymentNote=Serializing.getXmlNodeValue(doc,"EmploymentNote");
				}
				if(Serializing.getXmlNodeValue(doc,"Race")!=null) {
					Race=PatientRace.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Race"))];
				}
				if(Serializing.getXmlNodeValue(doc,"County")!=null) {
					County=Serializing.getXmlNodeValue(doc,"County");
				}
				if(Serializing.getXmlNodeValue(doc,"GradeLevel")!=null) {
					GradeLevel=PatientGrade.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"GradeLevel"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Urgency")!=null) {
					Urgency=TreatmentUrgency.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Urgency"))];
				}
				if(Serializing.getXmlNodeValue(doc,"DateFirstVisit")!=null) {
					DateFirstVisit=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateFirstVisit"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"HasIns")!=null) {
					HasIns=Serializing.getXmlNodeValue(doc,"HasIns");
				}
				if(Serializing.getXmlNodeValue(doc,"TrophyFolder")!=null) {
					TrophyFolder=Serializing.getXmlNodeValue(doc,"TrophyFolder");
				}
				if(Serializing.getXmlNodeValue(doc,"PlannedIsDone")!=null) {
					PlannedIsDone=(Serializing.getXmlNodeValue(doc,"PlannedIsDone")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Premed")!=null) {
					Premed=(Serializing.getXmlNodeValue(doc,"Premed")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Ward")!=null) {
					Ward=Serializing.getXmlNodeValue(doc,"Ward");
				}
				if(Serializing.getXmlNodeValue(doc,"PreferConfirmMethod")!=null) {
					PreferConfirmMethod=ContactMethod.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PreferConfirmMethod"))];
				}
				if(Serializing.getXmlNodeValue(doc,"PreferContactMethod")!=null) {
					PreferContactMethod=ContactMethod.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PreferContactMethod"))];
				}
				if(Serializing.getXmlNodeValue(doc,"PreferRecallMethod")!=null) {
					PreferRecallMethod=ContactMethod.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PreferRecallMethod"))];
				}
				if(Serializing.getXmlNodeValue(doc,"SchedBeforeTime")!=null) {
					SchedBeforeTime=Serializing.getXmlNodeValue(doc,"SchedBeforeTime");
				}
				if(Serializing.getXmlNodeValue(doc,"SchedAfterTime")!=null) {
					SchedAfterTime=Serializing.getXmlNodeValue(doc,"SchedAfterTime");
				}
				if(Serializing.getXmlNodeValue(doc,"SchedDayOfWeek")!=null) {
					SchedDayOfWeek=Byte.valueOf(Serializing.getXmlNodeValue(doc,"SchedDayOfWeek"));
				}
				if(Serializing.getXmlNodeValue(doc,"Language")!=null) {
					Language=Serializing.getXmlNodeValue(doc,"Language");
				}
				if(Serializing.getXmlNodeValue(doc,"AdmitDate")!=null) {
					AdmitDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"AdmitDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"Title")!=null) {
					Title=Serializing.getXmlNodeValue(doc,"Title");
				}
				if(Serializing.getXmlNodeValue(doc,"PayPlanDue")!=null) {
					PayPlanDue=Double.valueOf(Serializing.getXmlNodeValue(doc,"PayPlanDue"));
				}
				if(Serializing.getXmlNodeValue(doc,"SiteNum")!=null) {
					SiteNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SiteNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"ResponsParty")!=null) {
					ResponsParty=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ResponsParty"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianEligibilityCode")!=null) {
					CanadianEligibilityCode=Byte.valueOf(Serializing.getXmlNodeValue(doc,"CanadianEligibilityCode"));
				}
				if(Serializing.getXmlNodeValue(doc,"AskToArriveEarly")!=null) {
					AskToArriveEarly=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AskToArriveEarly"));
				}
				if(Serializing.getXmlNodeValue(doc,"OnlinePassword")!=null) {
					OnlinePassword=Serializing.getXmlNodeValue(doc,"OnlinePassword");
				}
				if(Serializing.getXmlNodeValue(doc,"SmokeStatus")!=null) {
					SmokeStatus=SmokingStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"SmokeStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"PreferContactConfidential")!=null) {
					PreferContactConfidential=ContactMethod.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PreferContactConfidential"))];
				}
				if(Serializing.getXmlNodeValue(doc,"SuperFamily")!=null) {
					SuperFamily=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SuperFamily"));
				}
				if(Serializing.getXmlNodeValue(doc,"TxtMsgOk")!=null) {
					TxtMsgOk=YN.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"TxtMsgOk"))];
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Patient: "+e.getMessage());
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
