using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness.HL7;
using OpenDentBusiness;
using OpenDental;

namespace UnitTests {
	public class HL7Tests {

		public static string HL7TestAll(HL7TestInterface hl7TestInterface) {
			string retval="";
			//Switch statement to enable the correct HL7Def
			switch(hl7TestInterface) {
				case HL7TestInterface.EcwOldFull:
					return retval+="No tests yet for EcwOldFull.\r\n";
				case HL7TestInterface.HL7DefFull:
					return retval+="No tests yet for HL7DefFull.\r\n";
				case HL7TestInterface.HL7DefTight:
					//Make sure the correct HL7Def is enabled for message processing
					List<HL7Def> defList=HL7Defs.GetDeepInternalList();
					foreach(HL7Def hl7Def in defList) {
						if(hl7Def.InternalType!="eCWTight" && HL7Defs.GetInternalFromDb(hl7Def.InternalType)!=null) {//If the def is in the db and not tight so disable it
							hl7Def.IsEnabled=false;
							HL7Defs.Update(hl7Def);
						}
						if(hl7Def.InternalType=="eCWTight") {//enable the def and insert or update in database
							hl7Def.IsEnabled=true;
							if(HL7Defs.GetInternalFromDb(hl7Def.InternalType)==null) {//eCWTight is not in the db so insert it
								HL7Defs.Insert(hl7Def);
							}
							else {//eCWTight is in the db so just make sure it is enabled
								HL7Defs.Update(hl7Def);
							}
						}
					}
					HL7Defs.RefreshCache();
					break;
				case HL7TestInterface.HL7DefStandalone:
					//Make sure the correct HL7Def is enabled for message processing
					defList=HL7Defs.GetDeepInternalList();
					foreach(HL7Def hl7Def in defList) {
						if(hl7Def.InternalType!="eCWStandalone" && HL7Defs.GetInternalFromDb(hl7Def.InternalType)!=null) {//If the def is in the db and not standalone so disable it
							hl7Def.IsEnabled=false;
							HL7Defs.Update(hl7Def);
						}
						if(hl7Def.InternalType=="eCWStandalone") {//enable the def and insert or update in database
							hl7Def.IsEnabled=true;
							if(HL7Defs.GetInternalFromDb(hl7Def.InternalType)==null) {//eCWStandalone is not in the db so insert it
								HL7Defs.Insert(hl7Def);
							}
							else {//eCWStandalone is in the db so just make sure it is enabled
								HL7Defs.Update(hl7Def);
							}
						}
					}
					HL7Defs.RefreshCache();
					break;
				default:
					break;
			}
			retval+=Test1(hl7TestInterface);
			return retval;
		}

		///<summary>Test 1: Pat in PID segment and guar in GT1 segment are new pats since db cleared so insert new patients into db.  Set pat.guar=guar.PatNum and guar.guar=guar.PatNum.  Set pat.PriProv and guar.PriProv=PracticeDefaultProvider and pat.BillingType and guar.BillingType to PracticeDefaultBillType.  pat.ChartNumber=A10,pat.PatNum=10,guar.PatNum=11.</summary>
		public static string Test1(HL7TestInterface hl7TestInterface) {
			string msgtext=@"MSH|^~\&|||||20120901100000||ADT^A04||P|2.3
				EVN|A04|20120901100000||
				PID|1|10||A10|Testt^Pat^F||19760210|F||Hispanic|420 Test Ave^Apt 7^Salem^MA^97330||5305554045|5305554234||Single|||534997812|||Standard
				GT1|1|11|Testt^Guar^F||420 Test Ave^Apt 7^Salem^MA^97330|5305554743|5303635432|19770730|M|||544071829";
			MessageHL7 msg=new MessageHL7(msgtext);
			try {
				if(hl7TestInterface==HL7TestInterface.EcwOldTight || hl7TestInterface==HL7TestInterface.EcwOldFull) {
					EcwADT.ProcessMessage(msg,false,false);
				}
				else if(hl7TestInterface==HL7TestInterface.EcwOldStandalone) {
					EcwADT.ProcessMessage(msg,true,false);
				}
				else {
					MessageParser.Process(msg,false);
				}
			}
			catch(Exception ex) {
				return "Test 1: Message processing error: "+ex+"\r\n";
			}
			Patient correctPat=new Patient();
			Patient correctGuar=new Patient();
			switch(hl7TestInterface) {
				case HL7TestInterface.EcwOldStandalone:
					correctPat.ChartNumber="10";
					correctGuar.ChartNumber="11";
					break;
				case HL7TestInterface.HL7DefStandalone:
					correctPat.ChartNumber="10";
					correctGuar.ChartNumber="11";
					break;
				default:
					correctPat.ChartNumber="A10";
					correctPat.PatNum=10;
					correctGuar.ChartNumber="A11";
					correctGuar.PatNum=11;
					break;
			}
			//Set up the rest of pat information
			correctPat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
			correctPat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			correctPat.FName="Pat";
			correctPat.MiddleI="F";
			correctPat.LName="Testt";
			correctPat.Birthdate=new DateTime(1976,02,10);
			correctPat.Gender=PatientGender.Female;
			correctPat.Race=PatientRace.HispanicLatino;
			correctPat.Address="420 Test Ave";
			correctPat.Address2="Apt 7";
			correctPat.City="Salem";
			correctPat.State="MA";
			correctPat.Zip="97330";
			correctPat.HmPhone="(530)555-4045";
			correctPat.WkPhone="(530)555-4234";
			correctPat.Position=PatientPosition.Single;
			correctPat.SSN="534997812";
			correctPat.FeeSched=FeeScheds.GetByExactName("Standard").FeeSchedNum;
			//Set up the rest of guar information
			correctGuar.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
			correctGuar.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			correctGuar.FName="Guar";
			correctGuar.MiddleI="F";
			correctGuar.LName="Testt";
			correctGuar.Birthdate=new DateTime(1977,07,30);
			correctGuar.Gender=PatientGender.Male;
			correctGuar.Address="420 Test Ave";
			correctGuar.Address2="Apt 7";
			correctGuar.City="Salem";
			correctGuar.State="MA";
			correctGuar.Zip="97330";
			correctGuar.HmPhone="(530)555-4743";
			correctGuar.WkPhone="(530)363-5432";
			correctGuar.SSN="544071829";
			Patient pat=Patients.GetPat(correctPat.PatNum);
			Patient guar=Patients.GetPat(correctGuar.PatNum);
			if(pat==null) {
				return "Test 1: Couldn't locate patient.\r\n";
			}
			if(guar==null) {
				return "Test 1: Couldn't locate guarantor.\r\n";
			}
			string retval=CompareGuarAndPat(pat,correctPat,guar,correctGuar,hl7TestInterface);
			if(retval.Length>0) {
				return retval;
			}
			return "Test 1: Passed.\r\n";
		}

		///<summary>Locate existing patient with patient.PatNum and update demographic information.  patient.ChartNumber should be changed from A10 to A11.  All pat fields should be updated from test 1.</summary>
		public static string Test2(HL7TestInterface hl7TestInterface) {
			string msgtext=@"MSH|^~\&|||||20120901100000||ADT^A04||P|2.3
				EVN|A04|20120901100000||
				PID|1|10||A11|Test^Patient^N||19760205|Male||White|420 Test St^Apt 17^Dallas^OR^97338||5035554045|5035554234||Married|||543997812|||Standard
				GT1|1|11|Test^Guarantor^L||420 Test St^Apt 17^Dallas^OR^97338|5035554743|5033635432|19770704|Female|||544071289";
			MessageHL7 msg=new MessageHL7(msgtext);
			try {
				EcwADT.ProcessMessage(msg,false,false);
			}
			catch(Exception ex) {
				return "Test 2: Message processing error: "+ex+"\r\n";
			}
			Patient correctPat=new Patient();
			correctPat.ChartNumber="A11";
			correctPat.PatNum=10;
			correctPat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
			correctPat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			correctPat.FName="Patient";
			correctPat.MiddleI="N";
			correctPat.LName="Test";
			correctPat.Birthdate=new DateTime(1976,02,05);
			correctPat.Gender=PatientGender.Male;
			correctPat.Race=PatientRace.White;
			correctPat.Address="420 Test St";
			correctPat.Address2="Apt 17";
			correctPat.City="Dallas";
			correctPat.State="OR";
			correctPat.Zip="97338";
			correctPat.HmPhone="(503)555-4045";
			correctPat.WkPhone="(503)555-4234";
			correctPat.Position=PatientPosition.Married;
			correctPat.SSN="543997812";
			correctPat.FeeSched=FeeScheds.GetByExactName("Standard").FeeSchedNum;
			Patient correctGuar=new Patient();
			correctGuar.ChartNumber="";
			correctGuar.PatNum=11;
			correctGuar.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
			correctGuar.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			correctGuar.FName="Guarantor";
			correctGuar.MiddleI="L";
			correctGuar.LName="Test";
			correctGuar.Birthdate=new DateTime(1977,07,04);
			correctGuar.Gender=PatientGender.Female;
			correctGuar.Address="420 Test St";
			correctGuar.Address2="Apt 17";
			correctGuar.City="Dallas";
			correctGuar.State="OR";
			correctGuar.Zip="97338";
			correctGuar.HmPhone="(503)555-4743";
			correctGuar.WkPhone="(503)363-5432";
			correctGuar.SSN="544071289";
			Patient pat=Patients.GetPat(correctPat.PatNum);
			Patient guar=Patients.GetPat(correctGuar.PatNum);
			if(pat==null) {
				return "Test 2: Couldn't locate the patient we just updated.\r\n";
			}
			if(guar==null) {
				return "Test 2: Couldn't locate the guarantor we just updated.\r\n";
			}
			string retval=CompareGuarAndPat(pat,correctPat,guar,correctGuar,hl7TestInterface);
			if(retval.Length>0) {
				return retval;
			}
			return "Test 2: Passed.\r\n";
		}

		///<summary>If first or last name is blank, don't update any information.  Message would update birthdate of patient and guarantor if processed, but patient.FName is blank and guarantor.LName is blank so should not change anything</summary>
		public static string Test3(HL7TestInterface hl7TestInterface) {
			Patient correctPat=Patients.GetPat(10);
			Patient correctGuar=Patients.GetPat(11);
			if(correctPat==null) {
				return "Test 3: Couldn't locate comparison patient.\r\n";
			}
			if(correctGuar==null) {
				return "Test 3: Couldn't locate comparison guarantor.\r\n";
			}
			string msgtext=@"MSH|^~\&|||||20120901100000||ADT^A04||P|2.3
				EVN|A04|20120901100000||
				PID|1|10||A11|Test^^N||19760215|Male||White|420 Test St^Apt 17^Dallas^OR^97338||5035554045|5035554234||Married|||543997812|||Standard
				GT1|1|11|^Guarantor^L||420 Test St^Apt 17^Dallas^OR^97338|5035554743|5033635432|19770713|Female|||544071289";
			MessageHL7 msg=new MessageHL7(msgtext);
			try {
				EcwADT.ProcessMessage(msg,false,false);
			}
			catch(Exception ex) {
				return "Test 3: Message processing error: "+ex+"\r\n";
			}
			Patient pat=Patients.GetPat(10);
			Patient guar=Patients.GetPat(11);
			if(pat==null) {
				return "Test 3: Couldn't locate patient.\r\n";
			}
			if(guar==null) {
				return "EcwTightOld 3: Couldn't locate guarantor.\r\n";
			}
			string retval=CompareGuarAndPat(pat,correctPat,guar,correctGuar,hl7TestInterface);
			if(retval.Length>0) {
				return retval;
			}
			return "Test 3: Passed.\r\n";
		}

		/// <summary>Insert two new patients, patient from PID and guarantor from GT1.  If date is less than 8 digits, set to MinValue of 0001-01-01.  Otherwise set to the correct precision with yyyyMMddHHmmss format and HHmmss all optional.  patient.Birthdate=0001-01-01; guarantor.Birthdate=1977-07-03 7:11 AM but inserted as date 1977-07-03.</summary>
		public static string Test4(HL7TestInterface hl7TestInterface) {
			string msgtext=@"MSH|^~\&|||||20120901100000||ADT^A04||P|2.3
				EVN|A04|20120901100000||
				PID|1|20||A100|Test^Patient^N||197602|Male||White|420 Test St^Apt 17^Dallas^OR^97338||5035554045|5035554234||Married|||543997812|||Standard
				GT1|1|21|Test^Guarantor^L||420 Test St^Apt 17^Dallas^OR^97338|5035554743|5033635432|197707030711|Female|||544071289";
			MessageHL7 msg=new MessageHL7(msgtext);
			try {
				EcwADT.ProcessMessage(msg,false,false);
			}
			catch(Exception ex) {
				return "Test 4: Message processing error: "+ex+"\r\n";
			}
			Patient pat=Patients.GetPat(20);
			Patient guar=Patients.GetPat(21);
			if(pat==null) {
				return "Test 4: Couldn't locate patient.\r\n";
			}
			if(guar==null) {
				return "Test 4: Couldn't locate guarantor.\r\n";
			}
			if(pat.Birthdate!=DateTime.MinValue) {
				return "Test 4: Patient Birthdate should be 0001-01-01.\r\n";
			}
			if(guar.Birthdate!=new DateTime(1977,07,03,0,0,0)) {
				return "Test 4: Guarantor Birthdate should be 1977-07-03.\r\n";
			}
			return "Test 4: Passed.\r\n";
		}

		/// <summary>Fail to locate patient so insert new patient.  No PV1 or AIG segment.  New appointment so insert appointment into database.</summary>
		public static string Test5(HL7TestInterface hl7TestInterface) {
			string msgtext=@"MSH|^~\&|||||20120901100000||SIU^S12||P|2.3
				EVN|S12|20120901100000||
				SCH|100|100|||||Checkup||||^^1200^20120901100000^20120901102000||||||||||||||
				PID|1|30||A30|Testt2^Patientt2^L||19760210|Female||Hispanic|420 Test Ave^Apt 7^Salem^MA^97330||5305554045|5305554234||Single|||111224444|||Standard";
			MessageHL7 msg=new MessageHL7(msgtext);
			try {
				EcwSIU.ProcessMessage(msg,false,false);
			}
			catch(Exception ex) {
				return "Test 5: Message processing error: "+ex+"\r\n";
			}
			Patient pat=Patients.GetPat(30);
			Appointment apt=Appointments.GetOneApt(100);
			if(pat==null) {
				return "Test 5: Couldn't locate patient.\r\n";
			}
			if(apt==null) {
				return "Test 5: Couldn't locate appointment.\r\n";
			}
			if(apt.PatNum!=pat.PatNum) {
				return "Test 5: Appointment.PatNum does not match patient.PatNum.\r\n";
			}
			if(apt.AptStatus!=ApptStatus.Scheduled) {
				return "Test 5: Appointment.AptStatus is not 'Scheduled'.\r\n";
			}
			if(apt.Note!="Checkup") {
				return "Test 5: Appointment.Note is not 'Checkup'.\r\n";
			}
			if(apt.AptDateTime!=new DateTime(2012,09,01,10,0,0)) {
				return "Test 5: Appointment.AptDateTime is not correct.\r\n";
			}
			return "Test 5: Passed.\r\n";
		}

		///<summary>Compares the pat and guar to correctPat and correctGuar to make sure every field matches.</summary>
		public static string CompareGuarAndPat(Patient pat,Patient correctPat,Patient guar,Patient correctGuar,HL7TestInterface hl7TestInterface) {
			string retval="";
			if(pat.Guarantor!=guar.PatNum) {
				retval+="Patient inserted isn't assigned to the guarantor specified in the GT1 segment.\r\n";
			}
			if(guar.Guarantor!=guar.PatNum) {
				retval+="Guarantor inserted should have self for guarantor.\r\n";
			}
			if(pat.PriProv!=correctPat.PriProv) {
				retval+="Patient PriProv should be "+Providers.GetAbbr(correctPat.PriProv)+" and is "+Providers.GetAbbr(correctPat.PriProv)+".\r\n";
				retval+="Guarantor PriProv should be "+Providers.GetAbbr(correctGuar.PriProv)+" and is "+Providers.GetAbbr(guar.PriProv)+".\r\n";
			}
			if(pat.BillingType!=correctPat.BillingType || guar.BillingType!=correctGuar.BillingType) {
				retval+="Patient BillingType should be "+DefC.GetName(DefCat.BillingTypes,correctPat.BillingType)+" and is "+DefC.GetName(DefCat.BillingTypes,pat.BillingType)+".\r\n";
				retval+="Guarantor BillingType should be "+DefC.GetName(DefCat.BillingTypes,correctGuar.BillingType)+" and is "+DefC.GetName(DefCat.BillingTypes,guar.BillingType)+".\r\n";
			}
			if(pat.ChartNumber!=correctPat.ChartNumber || guar.ChartNumber!=correctGuar.ChartNumber) {
				retval+="Patient ChartNumber should be "+correctPat.ChartNumber+" and is "+pat.ChartNumber+".\r\n";
				retval+="Guarantor ChartNumber should be "+correctGuar.ChartNumber+" and is "+guar.ChartNumber+".\r\n";
			}
			if(hl7TestInterface!=HL7TestInterface.EcwOldStandalone
				&& hl7TestInterface!=HL7TestInterface.HL7DefStandalone
				&& (pat.PatNum!=correctPat.PatNum || guar.PatNum!=correctGuar.PatNum))
			{
				retval+="Patient PatNum should be "+correctPat.PatNum.ToString()+" and is "+pat.PatNum.ToString()+".\r\n";
				retval+="Guarantor PatNum should be "+correctGuar.PatNum.ToString()+" and is "+correctGuar.PatNum.ToString()+".\r\n";
			}
			if(pat.LName!=correctPat.LName || guar.LName!=correctPat.LName) {
				retval+="Patient LName should be "+correctPat.LName+" and is "+pat.LName+".\r\n";
				retval+="Guarantor LName should be "+correctGuar.LName+" and is "+guar.LName+".\r\n";
			}
			if(pat.FName!=correctPat.FName || guar.FName!=correctGuar.FName) {
				retval+="Patient FName should be "+correctPat.FName+" and is "+pat.FName+".\r\n";
				retval+="Guarantor FName should be "+correctGuar.FName+" and is "+guar.FName+".\r\n";
			}
			if(pat.MiddleI!=correctPat.MiddleI || guar.MiddleI!=correctGuar.MiddleI) {
				retval+="Patient MiddleI should be "+correctPat.MiddleI+" and is "+pat.MiddleI+".\r\n";
				retval+="Guarantor MiddleI should be "+correctGuar.MiddleI+" and is "+guar.MiddleI+".\r\n";
			}
			if(pat.Birthdate!=correctPat.Birthdate || guar.Birthdate!=correctGuar.Birthdate) {
				retval+="Patient Birthdate should be "+correctPat.Birthdate.ToString()+" and is "+pat.Birthdate.ToString()+".\r\n";
				retval+="Guarantor Birthdate should be "+correctGuar.Birthdate.ToString()+" and is "+guar.Birthdate.ToString()+".\r\n";
			}
			if(pat.Gender!=correctPat.Gender || guar.Gender!=correctGuar.Gender) {
				retval+="Patient Gender should be "+correctPat.Gender.ToString()+" and is "+pat.Gender.ToString()+".\r\n";
				retval+="Guarantor Gender should be "+correctGuar.Gender.ToString()+" and is "+guar.Gender.ToString()+".\r\n";
			}
			if(pat.Race!=correctPat.Race) {
				retval+="Patient Race should be "+correctPat.Race.ToString()+" and is "+pat.Race.ToString()+".\r\n";
			}
			if(pat.Address!=correctPat.Address || pat.Address2!=correctPat.Address2 || pat.City!=correctPat.City || pat.State!=correctPat.State || pat.Zip!=correctPat.Zip) {
				retval+="Patient Address should be "+correctPat.Address+" "+correctPat.Address2+" "+correctPat.City+", "+correctPat.State+" "+correctPat.Zip;
				retval+=" and is "+pat.Address+" "+pat.Address2+" "+pat.City+", "+pat.State+" "+pat.Zip+".\r\n";
			}
			if(guar.Address!=correctGuar.Address || guar.Address2!=correctGuar.Address2 || guar.City!=correctGuar.City || guar.State!=correctGuar.State || guar.Zip!=correctGuar.Zip) {
				retval+="Guarantor Address should be "+correctGuar.Address+" "+correctGuar.Address2+" "+correctGuar.City+", "+correctGuar.State+" "+correctGuar.Zip;
				retval+=" and is "+guar.Address+" "+guar.Address2+" "+guar.City+", "+guar.State+" "+guar.Zip+".\r\n";
			}
			if(pat.HmPhone!=correctPat.HmPhone || guar.HmPhone!=correctGuar.HmPhone) {
				retval+="Patient HmPhone should be "+correctPat.HmPhone+" and is "+pat.HmPhone+".\r\n";
				retval+="Guarantor HmPhone should be "+correctGuar.HmPhone+" and is "+guar.HmPhone+".\r\n";
			}
			if(pat.WkPhone!=correctPat.WkPhone || guar.WkPhone!=correctGuar.WkPhone) {
				retval+="Patient WkPhone should be "+correctPat.WkPhone+" and is "+pat.WkPhone+".\r\n";
				retval+="Guarantor WkPhone should be "+correctGuar.WkPhone+" and is "+guar.WkPhone+".\r\n";
			}
			if(pat.SSN!=correctPat.SSN || guar.SSN!=correctGuar.SSN) {
				retval+="Patient SSN should be "+correctPat.SSN+" and is "+pat.SSN+".\r\n";
				retval+="Guarantor SSN should be "+correctGuar.SSN+" and is "+guar.SSN+".\r\n";
			}
			if(pat.FeeSched!=correctPat.FeeSched) {
				retval+="Patient FeeSched should be "+FeeScheds.GetDescription(correctPat.FeeSched)+" and is "+FeeScheds.GetDescription(pat.FeeSched)+".\r\n";
			}
			return retval;
		}
	}
}
