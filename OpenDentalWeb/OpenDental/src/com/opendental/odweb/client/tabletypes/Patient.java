package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public String Birthdate;
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
		public String DateFirstVisit;
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
		public String AdmitDate;
		/** Includes any punctuation.  For example, Mr., Mrs., Miss, Dr., etc.  There is no selection mechanism yet for user; they must simply type it in. */
		public String Title;
		/** . */
		public double PayPlanDue;
		/** FK to site.SiteNum. Can be zero. Replaces the old GradeSchool field with a proper foreign key. */
		public int SiteNum;
		/** The last date and time this row was altered.  Not user editable. */
		public String DateTStamp;
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
			sb.append("<Birthdate>").append(Serializing.EscapeForXml(Birthdate)).append("</Birthdate>");
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
			sb.append("<DateFirstVisit>").append(Serializing.EscapeForXml(DateFirstVisit)).append("</DateFirstVisit>");
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
			sb.append("<AdmitDate>").append(Serializing.EscapeForXml(AdmitDate)).append("</AdmitDate>");
			sb.append("<Title>").append(Serializing.EscapeForXml(Title)).append("</Title>");
			sb.append("<PayPlanDue>").append(PayPlanDue).append("</PayPlanDue>");
			sb.append("<SiteNum>").append(SiteNum).append("</SiteNum>");
			sb.append("<DateTStamp>").append(Serializing.EscapeForXml(DateTStamp)).append("</DateTStamp>");
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				LName=doc.getElementsByTagName("LName").item(0).getFirstChild().getNodeValue();
				FName=doc.getElementsByTagName("FName").item(0).getFirstChild().getNodeValue();
				MiddleI=doc.getElementsByTagName("MiddleI").item(0).getFirstChild().getNodeValue();
				Preferred=doc.getElementsByTagName("Preferred").item(0).getFirstChild().getNodeValue();
				PatStatus=PatientStatus.values()[Integer.valueOf(doc.getElementsByTagName("PatStatus").item(0).getFirstChild().getNodeValue())];
				Gender=PatientGender.values()[Integer.valueOf(doc.getElementsByTagName("Gender").item(0).getFirstChild().getNodeValue())];
				Position=PatientPosition.values()[Integer.valueOf(doc.getElementsByTagName("Position").item(0).getFirstChild().getNodeValue())];
				Birthdate=doc.getElementsByTagName("Birthdate").item(0).getFirstChild().getNodeValue();
				SSN=doc.getElementsByTagName("SSN").item(0).getFirstChild().getNodeValue();
				Address=doc.getElementsByTagName("Address").item(0).getFirstChild().getNodeValue();
				Address2=doc.getElementsByTagName("Address2").item(0).getFirstChild().getNodeValue();
				City=doc.getElementsByTagName("City").item(0).getFirstChild().getNodeValue();
				State=doc.getElementsByTagName("State").item(0).getFirstChild().getNodeValue();
				Zip=doc.getElementsByTagName("Zip").item(0).getFirstChild().getNodeValue();
				HmPhone=doc.getElementsByTagName("HmPhone").item(0).getFirstChild().getNodeValue();
				WkPhone=doc.getElementsByTagName("WkPhone").item(0).getFirstChild().getNodeValue();
				WirelessPhone=doc.getElementsByTagName("WirelessPhone").item(0).getFirstChild().getNodeValue();
				Guarantor=Integer.valueOf(doc.getElementsByTagName("Guarantor").item(0).getFirstChild().getNodeValue());
				CreditType=doc.getElementsByTagName("CreditType").item(0).getFirstChild().getNodeValue();
				Email=doc.getElementsByTagName("Email").item(0).getFirstChild().getNodeValue();
				Salutation=doc.getElementsByTagName("Salutation").item(0).getFirstChild().getNodeValue();
				EstBalance=Double.valueOf(doc.getElementsByTagName("EstBalance").item(0).getFirstChild().getNodeValue());
				PriProv=Integer.valueOf(doc.getElementsByTagName("PriProv").item(0).getFirstChild().getNodeValue());
				SecProv=Integer.valueOf(doc.getElementsByTagName("SecProv").item(0).getFirstChild().getNodeValue());
				FeeSched=Integer.valueOf(doc.getElementsByTagName("FeeSched").item(0).getFirstChild().getNodeValue());
				BillingType=Integer.valueOf(doc.getElementsByTagName("BillingType").item(0).getFirstChild().getNodeValue());
				ImageFolder=doc.getElementsByTagName("ImageFolder").item(0).getFirstChild().getNodeValue();
				AddrNote=doc.getElementsByTagName("AddrNote").item(0).getFirstChild().getNodeValue();
				FamFinUrgNote=doc.getElementsByTagName("FamFinUrgNote").item(0).getFirstChild().getNodeValue();
				MedUrgNote=doc.getElementsByTagName("MedUrgNote").item(0).getFirstChild().getNodeValue();
				ApptModNote=doc.getElementsByTagName("ApptModNote").item(0).getFirstChild().getNodeValue();
				StudentStatus=doc.getElementsByTagName("StudentStatus").item(0).getFirstChild().getNodeValue();
				SchoolName=doc.getElementsByTagName("SchoolName").item(0).getFirstChild().getNodeValue();
				ChartNumber=doc.getElementsByTagName("ChartNumber").item(0).getFirstChild().getNodeValue();
				MedicaidID=doc.getElementsByTagName("MedicaidID").item(0).getFirstChild().getNodeValue();
				Bal_0_30=Double.valueOf(doc.getElementsByTagName("Bal_0_30").item(0).getFirstChild().getNodeValue());
				Bal_31_60=Double.valueOf(doc.getElementsByTagName("Bal_31_60").item(0).getFirstChild().getNodeValue());
				Bal_61_90=Double.valueOf(doc.getElementsByTagName("Bal_61_90").item(0).getFirstChild().getNodeValue());
				BalOver90=Double.valueOf(doc.getElementsByTagName("BalOver90").item(0).getFirstChild().getNodeValue());
				InsEst=Double.valueOf(doc.getElementsByTagName("InsEst").item(0).getFirstChild().getNodeValue());
				BalTotal=Double.valueOf(doc.getElementsByTagName("BalTotal").item(0).getFirstChild().getNodeValue());
				EmployerNum=Integer.valueOf(doc.getElementsByTagName("EmployerNum").item(0).getFirstChild().getNodeValue());
				EmploymentNote=doc.getElementsByTagName("EmploymentNote").item(0).getFirstChild().getNodeValue();
				Race=PatientRace.values()[Integer.valueOf(doc.getElementsByTagName("Race").item(0).getFirstChild().getNodeValue())];
				County=doc.getElementsByTagName("County").item(0).getFirstChild().getNodeValue();
				GradeLevel=PatientGrade.values()[Integer.valueOf(doc.getElementsByTagName("GradeLevel").item(0).getFirstChild().getNodeValue())];
				Urgency=TreatmentUrgency.values()[Integer.valueOf(doc.getElementsByTagName("Urgency").item(0).getFirstChild().getNodeValue())];
				DateFirstVisit=doc.getElementsByTagName("DateFirstVisit").item(0).getFirstChild().getNodeValue();
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
				HasIns=doc.getElementsByTagName("HasIns").item(0).getFirstChild().getNodeValue();
				TrophyFolder=doc.getElementsByTagName("TrophyFolder").item(0).getFirstChild().getNodeValue();
				PlannedIsDone=(doc.getElementsByTagName("PlannedIsDone").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Premed=(doc.getElementsByTagName("Premed").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Ward=doc.getElementsByTagName("Ward").item(0).getFirstChild().getNodeValue();
				PreferConfirmMethod=ContactMethod.values()[Integer.valueOf(doc.getElementsByTagName("PreferConfirmMethod").item(0).getFirstChild().getNodeValue())];
				PreferContactMethod=ContactMethod.values()[Integer.valueOf(doc.getElementsByTagName("PreferContactMethod").item(0).getFirstChild().getNodeValue())];
				PreferRecallMethod=ContactMethod.values()[Integer.valueOf(doc.getElementsByTagName("PreferRecallMethod").item(0).getFirstChild().getNodeValue())];
				SchedBeforeTime=doc.getElementsByTagName("SchedBeforeTime").item(0).getFirstChild().getNodeValue();
				SchedAfterTime=doc.getElementsByTagName("SchedAfterTime").item(0).getFirstChild().getNodeValue();
				SchedDayOfWeek=Byte.valueOf(doc.getElementsByTagName("SchedDayOfWeek").item(0).getFirstChild().getNodeValue());
				Language=doc.getElementsByTagName("Language").item(0).getFirstChild().getNodeValue();
				AdmitDate=doc.getElementsByTagName("AdmitDate").item(0).getFirstChild().getNodeValue();
				Title=doc.getElementsByTagName("Title").item(0).getFirstChild().getNodeValue();
				PayPlanDue=Double.valueOf(doc.getElementsByTagName("PayPlanDue").item(0).getFirstChild().getNodeValue());
				SiteNum=Integer.valueOf(doc.getElementsByTagName("SiteNum").item(0).getFirstChild().getNodeValue());
				DateTStamp=doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue();
				ResponsParty=Integer.valueOf(doc.getElementsByTagName("ResponsParty").item(0).getFirstChild().getNodeValue());
				CanadianEligibilityCode=Byte.valueOf(doc.getElementsByTagName("CanadianEligibilityCode").item(0).getFirstChild().getNodeValue());
				AskToArriveEarly=Integer.valueOf(doc.getElementsByTagName("AskToArriveEarly").item(0).getFirstChild().getNodeValue());
				OnlinePassword=doc.getElementsByTagName("OnlinePassword").item(0).getFirstChild().getNodeValue();
				SmokeStatus=SmokingStatus.values()[Integer.valueOf(doc.getElementsByTagName("SmokeStatus").item(0).getFirstChild().getNodeValue())];
				PreferContactConfidential=ContactMethod.values()[Integer.valueOf(doc.getElementsByTagName("PreferContactConfidential").item(0).getFirstChild().getNodeValue())];
				SuperFamily=Integer.valueOf(doc.getElementsByTagName("SuperFamily").item(0).getFirstChild().getNodeValue());
				TxtMsgOk=YN.values()[Integer.valueOf(doc.getElementsByTagName("TxtMsgOk").item(0).getFirstChild().getNodeValue())];
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
