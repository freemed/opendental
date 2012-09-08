using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness.HL7;
using OpenDentBusiness;

namespace UnitTests {
	public class HL7Tests_old {
		///<summary></summary>
//    public static string HL7TestAll(HL7TestInterface hl7TestInterface) {
//      string retval="";
//      if(!isEcwOld) {//Make sure the correct HL7Def is enabled for message processing
//        List<HL7Def> defList=HL7Defs.GetDeepInternalList();
//        HL7Def def;
//        if(isStandalone) {
//          def=defList[0];
//          if(HL7Defs.GetInternalFromDb(def.InternalType)!=null) {//ecwTight is in the db so disable
//            def.IsEnabled=false;
//            HL7Defs.Update(def);
//          }
//          def=defList[1];
//          def.IsEnabled=true;
//          if(HL7Defs.GetInternalFromDb(def.InternalType)==null) {//ecwStandalone is not in the db so insert it and enable it
//            HL7Defs.Insert(def);
//          }
//          else {//ecwStandalone is in the db so just make sure it is enabled
//            HL7Defs.Update(def);
//          }
//        }
//        else {//right now if not standalone then tight (handle full later)
//          def=defList[0];
//          def.IsEnabled=true;
//          if(HL7Defs.GetInternalFromDb(def.InternalType)==null) {//ecwTight is not in the db so insert it and enable it
//            HL7Defs.Insert(def);
//          }
//          else {//ecwTight is in the db so just make sure it is enabled
//            HL7Defs.Update(def);
//          }
//          def=defList[1];
//          if(HL7Defs.GetInternalFromDb(def.InternalType)!=null) {//ecwStandalone is in the db so disable it
//            def.IsEnabled=false;
//            HL7Defs.Update(def);
//          }
//        }
//        HL7Defs.RefreshCache();
//      }
//      try {
//        retval+=ADTTest1(isEcwOld,isStandalone);
//      }
//      catch(Exception ex) {
//        retval+=ex.ToString()+"\r\n";
//      }
//      try {
//        retval+=ADTTest2(isEcwOld,isStandalone);
//      }
//      catch(Exception ex) {
//        retval+=ex.ToString()+"\r\n";
//      }
//      try {
//        retval+=ADTTest3(isEcwOld,isStandalone);
//      }
//      catch(Exception ex) {
//        retval+=ex.ToString()+"\r\n";
//      }
//      try {
//        retval+=ADTTest4(isEcwOld,isStandalone);
//      }
//      catch(Exception ex) {
//        retval+=ex.ToString()+"\r\n";
//      }
//      try {
//        retval+=ADTTest5(isEcwOld,isStandalone);
//      }
//      catch(Exception ex) {
//        retval+=ex.ToString()+"\r\n";
//      }
//      return retval;
//    }

//    ///<summary>Test 1: Pat in PID segment and guar in GT1 segment are new pats since db cleared so insert new patients into db.  Set pat.guar=guar.PatNum.  Set pat.PriProv=PracticeDefaultProvider and pat.BillingType to practice default billing type.  pat.ChartNumber=10 in standalone mode, A10 in tight or full.  pat.PatNum will be 10 in tight and full and next auto-incremented value in standalone.</summary>
//    public static string ADTTest1(bool isEcwOld,bool isStandalone) {
//      string retval="";
//      string msgtext=@"MSH|^~\&|ECW|110011||LAB|200605111453||ADT^A04|200605111453|P|2.3
//				EVN|A04|20061018103459||01
//				PID|1|10||A10|Testt^Pat^F||19760210|F||Hispanic|420 Test Ave^Apt 7^Salem^MA^97330||5305554045|5305554234||Single|||534997812|||Standard
//				GT1|1|11|Testt^Guar^F||420 Test Ave^Apt 7^Salem^MA^97330|5305554743|5303635432|19770730|M|||544071829";
//      MessageHL7 msg=new MessageHL7(msgtext);
//      try {
//        if(isEcwOld) {
//          EcwADT.ProcessMessage(msg,isStandalone,false);
//        }
//        else {
//          MessageParser.Process(msg,false);
//        }
//      }
//      catch(Exception ex) {
//        throw new Exception("Test 1 failed due to message processing error: "+ex+"\r\n");
//      }
//      Patient correctPat=new Patient();
//      if(isStandalone) {
//        correctPat.ChartNumber="10";
//      }
//      else {
//        correctPat.ChartNumber="A10";
//        correctPat.PatNum=10;
//      }
//      correctPat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctPat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctPat.FName="Pat";
//      correctPat.MiddleI="F";
//      correctPat.LName="Testt";
//      correctPat.Birthdate=new DateTime(1976,02,10);
//      correctPat.Gender=PatientGender.Female;
//      correctPat.Race=PatientRace.HispanicLatino;
//      correctPat.Address="420 Test Ave";
//      correctPat.Address2="Apt 7";
//      correctPat.City="Salem";
//      correctPat.State="MA";
//      correctPat.Zip="97330";
//      correctPat.HmPhone="(530)555-4045";
//      correctPat.WkPhone="(530)555-4234";
//      correctPat.Position=PatientPosition.Single;
//      correctPat.SSN="534997812";
//      correctPat.FeeSched=FeeScheds.GetByExactName("Standard").FeeSchedNum;
//      Patient correctGuar=new Patient();
//      if(isStandalone) {
//        correctGuar.ChartNumber="11";
//      }
//      else {
//        correctGuar.ChartNumber="";
//        correctGuar.PatNum=11;
//      }
//      correctGuar.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctGuar.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctGuar.FName="Guar";
//      correctGuar.MiddleI="F";
//      correctGuar.LName="Testt";
//      correctGuar.Birthdate=new DateTime(1977,07,30);
//      correctGuar.Gender=PatientGender.Male;
//      correctGuar.Address="420 Test Ave";
//      correctGuar.Address2="Apt 7";
//      correctGuar.City="Salem";
//      correctGuar.State="MA";
//      correctGuar.Zip="97330";
//      correctGuar.HmPhone="(530)555-4743";
//      correctGuar.WkPhone="(530)363-5432";
//      correctGuar.SSN="544071829";
//      Patient pat=null;
//      Patient guar=null;
//      if(isStandalone) {//Get Patient and Guarantor and compare fields in db to expected results
//        pat=Patients.GetPatByChartNumber(correctPat.ChartNumber);
//        guar=Patients.GetPatByChartNumber(correctGuar.ChartNumber);
//      }
//      else {
//        pat=Patients.GetPat(correctPat.PatNum);
//        guar=Patients.GetPat(correctGuar.PatNum);
//      }
//      if(pat==null) {
//        return retval+="Couldn't locate patient we just inserted.\r\n";
//      }
//      if(guar==null) {
//        return retval+="Couldn't locate the guarantor we just inserted.\r\n";
//      }
//      retval+=CompareGuarAndPat(pat,correctPat,guar,correctGuar,isStandalone);
//      if(retval=="") {
//        retval+="ADT Test 1 passed.\r\n";
//      }
//      return retval;
//    }

//    ///<summary>Locate existing patient with patient.PatNum in tight/full, patient.ChartNumber in standalone and update demographic information.  In tight/full, patient.ChartNumber should be changed from A10 to A11.  All pat fields should be updated from test 1.</summary>
//    public static string ADTTest2(bool isEcwOld,bool isStandalone) {
//      string retval="";
//      string msgtext=@"MSH|^~\&|ECW|110011||LAB|200605111453||ADT^A04|200605111453|P|2.3
//				EVN|A04|20061018103459||01
//				PID|1|10||A11|Test^Patient^N||19760205|Male||White|420 Test St^Apt 17^Dallas^OR^97338||5035554045|5035554234||Married|||543997812|||Standard
//				GT1|1|11|Test^Guarantor^L||420 Test St^Apt 17^Dallas^OR^97338|5035554743|5033635432|19770704|Female|||544071289";
//      MessageHL7 msg=new MessageHL7(msgtext);
//      try {
//        if(isEcwOld) {
//          EcwADT.ProcessMessage(msg,isStandalone,false);
//        }
//        else {
//          MessageParser.Process(msg,false);
//        }
//      }
//      catch(Exception ex) {
//        throw new Exception("Test 2 failed due to message processing error: "+ex+"\r\n");
//      }
//      Patient correctPat=new Patient();
//      if(isStandalone) {
//        correctPat.ChartNumber="10";
//      }
//      else {
//        correctPat.ChartNumber="A11";
//        correctPat.PatNum=10;
//      }
//      correctPat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctPat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctPat.FName="Patient";
//      correctPat.MiddleI="N";
//      correctPat.LName="Test";
//      correctPat.Birthdate=new DateTime(1976,02,05);
//      correctPat.Gender=PatientGender.Male;
//      correctPat.Race=PatientRace.White;
//      correctPat.Address="420 Test St";
//      correctPat.Address2="Apt 17";
//      correctPat.City="Dallas";
//      correctPat.State="OR";
//      correctPat.Zip="97338";
//      correctPat.HmPhone="(503)555-4045";
//      correctPat.WkPhone="(503)555-4234";
//      correctPat.Position=PatientPosition.Married;
//      correctPat.SSN="543997812";
//      correctPat.FeeSched=FeeScheds.GetByExactName("Standard").FeeSchedNum;
//      Patient correctGuar=new Patient();
//      if(isStandalone) {
//        correctGuar.ChartNumber="11";
//      }
//      else {
//        correctGuar.ChartNumber="";
//        correctGuar.PatNum=11;
//      }
//      correctGuar.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctGuar.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctGuar.FName="Guarantor";
//      correctGuar.MiddleI="L";
//      correctGuar.LName="Test";
//      correctGuar.Birthdate=new DateTime(1977,07,04);
//      correctGuar.Gender=PatientGender.Female;
//      correctGuar.Address="420 Test St";
//      correctGuar.Address2="Apt 17";
//      correctGuar.City="Dallas";
//      correctGuar.State="OR";
//      correctGuar.Zip="97338";
//      correctGuar.HmPhone="(503)555-4743";
//      correctGuar.WkPhone="(503)363-5432";
//      correctGuar.SSN="544071289";
//      Patient pat=null;
//      Patient guar=null;
//      if(isStandalone) {//Get Patient and Guarantor and compare fields in db to expected results
//        pat=Patients.GetPatByChartNumber(correctPat.ChartNumber);
//        guar=Patients.GetPatByChartNumber(correctGuar.ChartNumber);
//      }
//      else {
//        pat=Patients.GetPat(correctPat.PatNum);
//        guar=Patients.GetPat(correctGuar.PatNum);
//      }
//      if(pat==null) {
//        return retval+="Couldn't locate patient we just updated.\r\n";
//      }
//      if(guar==null) {
//        return retval+="Couldn't locate the guarantor we just updated.\r\n";
//      }
//      retval+=CompareGuarAndPat(pat,correctPat,guar,correctGuar,isStandalone);
//      if(retval=="") {
//        retval+="ADT Test 2 passed.\r\n";
//      }
//      return retval;
//    }

//    ///<summary>In standalone, fail to locate both the patient and guarantor by ChartNumber but locate by name and birthdate.  This should then update the ChartNumber to the value of PID.2 for patient.ChartNumber and GT1.2 for guarantor.ChartNumber.  In tight or full the patient and guarantor will not be located so new patients will be added with the PatNums sent in PID.2 and GT1.2.</summary>
//    public static string ADTTest3(bool isEcwOld,bool isStandalone) {
//      string retval="";
//      string msgtext=@"MSH|^~\&|ECW|110011||LAB|200605111453||ADT^A04|200605111453|P|2.3
//				EVN|A04|20061018103459||01
//				PID|1|20||A100|Test^Patient^N||19760205|Male||White|420 Test St^Apt 17^Dallas^OR^97338||5035554045|5035554234||Married|||543997812|||Standard
//				GT1|1|21|Test^Guarantor^L||420 Test St^Apt 17^Dallas^OR^97338|5035554743|5033635432|19770704|Female|||544071289";
//      MessageHL7 msg=new MessageHL7(msgtext);
//      try {
//        if(isEcwOld) {
//          EcwADT.ProcessMessage(msg,isStandalone,false);
//        }
//        else {
//          MessageParser.Process(msg,false);
//        }
//      }
//      catch(Exception ex) {
//        throw new Exception("Test 3 failed due to message processing error: "+ex+"\r\n");
//      }
//      Patient correctPat=new Patient();
//      if(isStandalone) {
//        correctPat.ChartNumber="20";
//      }
//      else {
//        correctPat.ChartNumber="A100";
//        correctPat.PatNum=20;
//      }
//      correctPat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctPat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctPat.FName="Patient";
//      correctPat.MiddleI="N";
//      correctPat.LName="Test";
//      correctPat.Birthdate=new DateTime(1976,02,05);
//      correctPat.Gender=PatientGender.Male;
//      correctPat.Race=PatientRace.White;
//      correctPat.Address="420 Test St";
//      correctPat.Address2="Apt 17";
//      correctPat.City="Dallas";
//      correctPat.State="OR";
//      correctPat.Zip="97338";
//      correctPat.HmPhone="(503)555-4045";
//      correctPat.WkPhone="(503)555-4234";
//      correctPat.Position=PatientPosition.Married;
//      correctPat.SSN="543997812";
//      correctPat.FeeSched=FeeScheds.GetByExactName("Standard").FeeSchedNum;
//      Patient correctGuar=new Patient();
//      if(isStandalone) {
//        correctGuar.ChartNumber="21";
//      }
//      else {
//        correctGuar.ChartNumber="";
//        correctGuar.PatNum=21;
//      }
//      correctGuar.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctGuar.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctGuar.FName="Guarantor";
//      correctGuar.MiddleI="L";
//      correctGuar.LName="Test";
//      correctGuar.Birthdate=new DateTime(1977,07,04);
//      correctGuar.Gender=PatientGender.Female;
//      correctGuar.Address="420 Test St";
//      correctGuar.Address2="Apt 17";
//      correctGuar.City="Dallas";
//      correctGuar.State="OR";
//      correctGuar.Zip="97338";
//      correctGuar.HmPhone="(503)555-4743";
//      correctGuar.WkPhone="(503)363-5432";
//      correctGuar.SSN="544071289";
//      Patient pat=null;
//      Patient guar=null;
//      if(isStandalone) {//Get Patient and Guarantor and compare fields in db to expected results
//        pat=Patients.GetPatByChartNumber(correctPat.ChartNumber);
//        guar=Patients.GetPatByChartNumber(correctGuar.ChartNumber);
//      }
//      else {
//        pat=Patients.GetPat(correctPat.PatNum);
//        guar=Patients.GetPat(correctGuar.PatNum);
//      }
//      if(pat==null) {
//        return retval+="Couldn't locate patient we just updated.\r\n";
//      }
//      if(guar==null) {
//        return retval+="Couldn't locate the guarantor we just updated.\r\n";
//      }
//      retval+=CompareGuarAndPat(pat,correctPat,guar,correctGuar,isStandalone);
//      if(retval=="") {
//        retval+="ADT Test 3 passed.\r\n";
//      }
//      return retval;
//    }

//    ///<summary>If first or last name is blank, don't update any information</summary>
//    public static string ADTTest4(bool isEcwOld,bool isStandalone) {
//      string retval="";
//      string msgtext=@"MSH|^~\&|ECW|110011||LAB|200605111453||ADT^A04|200605111453|P|2.3
//				EVN|A04|20061018103459||01
//				PID|1|20||A100|Test^^N||19760215|F||Asian|421 Test St^Apt 170^Dalas^WA^97308||4035554045|4035554234||Divorced|||443997812
//				GT1|1|21|^Guarantor^L||421 Test St^Apt 170^Dalas^WA^97308|4035554743|4033635432|19770713|M|||444071289";
//      MessageHL7 msg=new MessageHL7(msgtext);
//      try {
//        if(isEcwOld) {
//          EcwADT.ProcessMessage(msg,isStandalone,false);
//        }
//        else {
//          MessageParser.Process(msg,false);
//        }
//      }
//      catch(Exception ex) {
//        throw new Exception("Test 4 failed due to message processing error: "+ex+"\r\n");
//      }
//      Patient correctPat=new Patient();
//      if(isStandalone) {
//        correctPat.ChartNumber="20";
//      }
//      else {
//        correctPat.ChartNumber="A100";
//        correctPat.PatNum=20;
//      }
//      correctPat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctPat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctPat.FName="Patient";
//      correctPat.MiddleI="N";
//      correctPat.LName="Test";
//      correctPat.Birthdate=new DateTime(1976,02,05);
//      correctPat.Gender=PatientGender.Male;
//      correctPat.Race=PatientRace.White;
//      correctPat.Address="420 Test St";
//      correctPat.Address2="Apt 17";
//      correctPat.City="Dallas";
//      correctPat.State="OR";
//      correctPat.Zip="97338";
//      correctPat.HmPhone="(503)555-4045";
//      correctPat.WkPhone="(503)555-4234";
//      correctPat.Position=PatientPosition.Married;
//      correctPat.SSN="543997812";
//      correctPat.FeeSched=FeeScheds.GetByExactName("Standard").FeeSchedNum;
//      Patient correctGuar=new Patient();
//      if(isStandalone) {
//        correctGuar.ChartNumber="21";
//      }
//      else {
//        correctGuar.ChartNumber="";
//        correctGuar.PatNum=21;
//      }
//      correctGuar.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctGuar.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctGuar.FName="Guarantor";
//      correctGuar.MiddleI="L";
//      correctGuar.LName="Test";
//      correctGuar.Birthdate=new DateTime(1977,07,04);
//      correctGuar.Gender=PatientGender.Female;
//      correctGuar.Address="420 Test St";
//      correctGuar.Address2="Apt 17";
//      correctGuar.City="Dallas";
//      correctGuar.State="OR";
//      correctGuar.Zip="97338";
//      correctGuar.HmPhone="(503)555-4743";
//      correctGuar.WkPhone="(503)363-5432";
//      correctGuar.SSN="544071289";
//      Patient pat=null;
//      Patient guar=null;
//      if(isStandalone) {//Get Patient and Guarantor and compare fields in db to expected results
//        pat=Patients.GetPatByChartNumber(correctPat.ChartNumber);
//        guar=Patients.GetPatByChartNumber(correctGuar.ChartNumber);
//      }
//      else {
//        pat=Patients.GetPat(correctPat.PatNum);
//        guar=Patients.GetPat(correctGuar.PatNum);
//      }
//      if(pat==null) {
//        return retval+="Couldn't locate patient we just updated.\r\n";
//      }
//      if(guar==null) {
//        return retval+="Couldn't locate the guarantor we just updated.\r\n";
//      }
//      retval+=CompareGuarAndPat(pat,correctPat,guar,correctGuar,isStandalone);
//      if(retval=="") {
//        retval+="ADT Test 4 passed.\r\n";
//      }
//      return retval;
//    }

//    ///<summary>Test correct date parsing.  If date is less than 8 digits, set to MinValue.  Otherwise set to the correct precision with yyyyMMddHHmmss format and HHmmss all optional.  Patient.Birthdate should be changed to MinValue and guarantor.Birthdate should be 1977-07-03 070300 but inserted into database as just 1977-07-03</summary>
//    public static string ADTTest5(bool isEcwOld,bool isStandalone) {
//      string retval="";
//      string msgtext=@"MSH|^~\&|ECW|110011||LAB|200605111453||ADT^A04|200605111453|P|2.3
//				EVN|A04|20061018103459||01
//				PID|1|20||A100|Test^Patient^N||197602|Male||White|420 Test St^Apt 17^Dallas^OR^97338||5035554045|5035554234||Married|||543997812|||Standard
//				GT1|1|21|Test^Guarantor^L||420 Test St^Apt 17^Dallas^OR^97338|5035554743|5033635432|197707030711|Female|||544071289";
//      MessageHL7 msg=new MessageHL7(msgtext);
//      try {
//        if(isEcwOld) {
//          EcwADT.ProcessMessage(msg,isStandalone,false);
//        }
//        else {
//          MessageParser.Process(msg,false);
//        }
//      }
//      catch(Exception ex) {
//        throw new Exception("Test 5 failed due to message processing error: "+ex+"\r\n");
//      }
//      Patient correctPat=new Patient();
//      if(isStandalone) {
//        correctPat.ChartNumber="20";
//      }
//      else {
//        correctPat.ChartNumber="A100";
//        correctPat.PatNum=20;
//      }
//      correctPat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctPat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctPat.FName="Patient";
//      correctPat.MiddleI="N";
//      correctPat.LName="Test";
//      correctPat.Birthdate=new DateTime(0001,01,01);
//      correctPat.Gender=PatientGender.Male;
//      correctPat.Race=PatientRace.White;
//      correctPat.Address="420 Test St";
//      correctPat.Address2="Apt 17";
//      correctPat.City="Dallas";
//      correctPat.State="OR";
//      correctPat.Zip="97338";
//      correctPat.HmPhone="(503)555-4045";
//      correctPat.WkPhone="(503)555-4234";
//      correctPat.Position=PatientPosition.Married;
//      correctPat.SSN="543997812";
//      correctPat.FeeSched=FeeScheds.GetByExactName("Standard").FeeSchedNum;
//      Patient correctGuar=new Patient();
//      if(isStandalone) {
//        correctGuar.ChartNumber="21";
//      }
//      else {
//        correctGuar.ChartNumber="";
//        correctGuar.PatNum=21;
//      }
//      correctGuar.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
//      correctGuar.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
//      correctGuar.FName="Guarantor";
//      correctGuar.MiddleI="L";
//      correctGuar.LName="Test";
//      correctGuar.Birthdate=new DateTime(1977,07,03);
//      correctGuar.Gender=PatientGender.Female;
//      correctGuar.Address="420 Test St";
//      correctGuar.Address2="Apt 17";
//      correctGuar.City="Dallas";
//      correctGuar.State="OR";
//      correctGuar.Zip="97338";
//      correctGuar.HmPhone="(503)555-4743";
//      correctGuar.WkPhone="(503)363-5432";
//      correctGuar.SSN="544071289";
//      Patient pat=null;
//      Patient guar=null;
//      if(isStandalone) {//Get Patient and Guarantor and compare fields in db to expected results
//        pat=Patients.GetPatByChartNumber(correctPat.ChartNumber);
//        guar=Patients.GetPatByChartNumber(correctGuar.ChartNumber);
//      }
//      else {
//        pat=Patients.GetPat(correctPat.PatNum);
//        guar=Patients.GetPat(correctGuar.PatNum);
//      }
//      if(pat==null) {
//        return retval+="Couldn't locate patient we just updated.\r\n";
//      }
//      if(guar==null) {
//        return retval+="Couldn't locate the guarantor we just updated.\r\n";
//      }
//      retval+=CompareGuarAndPat(pat,correctPat,guar,correctGuar,isStandalone);
//      if(retval=="") {
//        retval+="ADT Test 5 passed.\r\n";
//      }
//      return retval;
//    }

//    ///<summary>Compares the pat and guar to correctPat and correctGuar to make sure every field matches.</summary>
//    public static string CompareGuarAndPat(Patient pat,Patient correctPat,Patient guar,Patient correctGuar,bool isStandalone) {
//      string retval="";
//      if(pat.Guarantor!=guar.PatNum) {
//        retval+="Patient inserted isn't assigned to the guarantor specified in the GT1 segment.\r\n";
//      }
//      if(pat.PriProv!=correctPat.PriProv) {
//        retval+="Patient PriProv should be "+Providers.GetAbbr(correctPat.PriProv)+" and is "+Providers.GetAbbr(correctPat.PriProv)+".\r\n";
//        retval+="Guarantor PriProv should be "+Providers.GetAbbr(correctGuar.PriProv)+" and is "+Providers.GetAbbr(guar.PriProv)+".\r\n";
//      }
//      if(pat.BillingType!=correctPat.BillingType || guar.BillingType!=correctGuar.BillingType) {
//        retval+="Patient BillingType should be "+DefC.GetName(DefCat.BillingTypes,correctPat.BillingType)+" and is "+DefC.GetName(DefCat.BillingTypes,pat.BillingType)+".\r\n";
//        retval+="Guarantor BillingType should be "+DefC.GetName(DefCat.BillingTypes,correctGuar.BillingType)+" and is "+DefC.GetName(DefCat.BillingTypes,guar.BillingType)+".\r\n";
//      }
//      if(pat.ChartNumber!=correctPat.ChartNumber || guar.ChartNumber!=correctGuar.ChartNumber) {
//        retval+="Patient ChartNumber should be "+correctPat.ChartNumber+" and is "+pat.ChartNumber+".\r\n";
//        retval+="Guarantor ChartNumber should be "+correctGuar.ChartNumber+" and is "+guar.ChartNumber+".\r\n";
//      }
//      if(!isStandalone && (pat.PatNum!=correctPat.PatNum || guar.PatNum!=correctGuar.PatNum)) {
//        retval+="Patient PatNum should be "+correctPat.PatNum.ToString()+" and is "+pat.PatNum.ToString()+".\r\n";
//        retval+="Guarantor PatNum should be "+correctGuar.PatNum.ToString()+" and is "+correctGuar.PatNum.ToString()+".\r\n";
//      }
//      if(pat.LName!=correctPat.LName || guar.LName!=correctPat.LName) {
//        retval+="Patient LName should be "+correctPat.LName+" and is "+pat.LName+".\r\n";
//        retval+="Guarantor LName should be "+correctGuar.LName+" and is "+guar.LName+".\r\n";
//      }
//      if(pat.FName!=correctPat.FName || guar.FName!=correctGuar.FName) {
//        retval+="Patient FName should be "+correctPat.FName+" and is "+pat.FName+".\r\n";
//        retval+="Guarantor FName should be "+correctGuar.FName+" and is "+guar.FName+".\r\n";
//      }
//      if(pat.MiddleI!=correctPat.MiddleI || guar.MiddleI!=correctGuar.MiddleI) {
//        retval+="Patient MiddleI should be "+correctPat.MiddleI+" and is "+pat.MiddleI+".\r\n";
//        retval+="Guarantor MiddleI should be "+correctGuar.MiddleI+" and is "+guar.MiddleI+".\r\n";
//      }
//      if(pat.Birthdate!=correctPat.Birthdate || guar.Birthdate!=correctGuar.Birthdate) {
//        retval+="Patient Birthdate should be "+correctPat.Birthdate.ToString()+" and is "+pat.Birthdate.ToString()+".\r\n";
//        retval+="Guarantor Birthdate should be "+correctGuar.Birthdate.ToString()+" and is "+guar.Birthdate.ToString()+".\r\n";
//      }
//      if(pat.Gender!=correctPat.Gender || guar.Gender!=correctGuar.Gender) {
//        retval+="Patient Gender should be "+correctPat.Gender.ToString()+" and is "+pat.Gender.ToString()+".\r\n";
//        retval+="Guarantor Gender should be "+correctGuar.Gender.ToString()+" and is "+guar.Gender.ToString()+".\r\n";
//      }
//      if(pat.Race!=correctPat.Race) {
//        retval+="Patient Race should be "+correctPat.Race.ToString()+" and is "+pat.Race.ToString()+".\r\n";
//      }
//      if(pat.Address!=correctPat.Address || pat.Address2!=correctPat.Address2 || pat.City!=correctPat.City || pat.State!=correctPat.State || pat.Zip!=correctPat.Zip) {
//        retval+="Patient Address should be "+correctPat.Address+" "+correctPat.Address2+" "+correctPat.City+", "+correctPat.State+" "+correctPat.Zip;
//        retval+=" and is "+pat.Address+" "+pat.Address2+" "+pat.City+", "+pat.State+" "+pat.Zip+".\r\n";
//      }
//      if(guar.Address!=correctGuar.Address || guar.Address2!=correctGuar.Address2 || guar.City!=correctGuar.City || guar.State!=correctGuar.State || guar.Zip!=correctGuar.Zip) {
//        retval+="Guarantor Address should be "+correctGuar.Address+" "+correctGuar.Address2+" "+correctGuar.City+", "+correctGuar.State+" "+correctGuar.Zip;
//        retval+=" and is "+guar.Address+" "+guar.Address2+" "+guar.City+", "+guar.State+" "+guar.Zip+".\r\n";
//      }
//      if(pat.HmPhone!=correctPat.HmPhone || guar.HmPhone!=correctGuar.HmPhone) {
//        retval+="Patient HmPhone should be "+correctPat.HmPhone+" and is "+pat.HmPhone+".\r\n";
//        retval+="Guarantor HmPhone should be "+correctGuar.HmPhone+" and is "+guar.HmPhone+".\r\n";
//      }
//      if(pat.WkPhone!=correctPat.WkPhone || guar.WkPhone!=correctGuar.WkPhone) {
//        retval+="Patient WkPhone should be "+correctPat.WkPhone+" and is "+pat.WkPhone+".\r\n";
//        retval+="Guarantor WkPhone should be "+correctGuar.WkPhone+" and is "+guar.WkPhone+".\r\n";
//      }
//      if(pat.SSN!=correctPat.SSN || guar.SSN!=correctGuar.SSN) {
//        retval+="Patient SSN should be "+correctPat.SSN+" and is "+pat.SSN+".\r\n";
//        retval+="Guarantor SSN should be "+correctGuar.SSN+" and is "+guar.SSN+".\r\n";
//      }
//      if(pat.FeeSched!=correctPat.FeeSched) {
//        retval+="Patient FeeSched should be "+FeeScheds.GetDescription(correctPat.FeeSched)+" and is "+FeeScheds.GetDescription(pat.FeeSched)+".\r\n";
//      }
//      return retval;
//    }
	}
}
