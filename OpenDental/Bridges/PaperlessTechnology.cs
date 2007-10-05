using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary>PT Dental.  www.gopaperlessnow.com.  This bridge only works on Windows and in English, so some shortcuts were taken.</summary>
	public class PaperlessTechnology{
		private static string exportAddCsv="addpatient_OD.csv";
		private static string exportUpdateCsv="updatepatient_OD.csv";
		private static string importCsv="patientinfo_PT.csv";
		private static string importMedCsv="patientmedalerts.csv";
		private static string exportAddExe="addpatient_OD.exe";
		private static string exportUpdateExe="updatepatient_OD.exe";
		private static string dir=@"C:\PT\USI";
		private static FileSystemWatcher watcher;

		/// <summary></summary>
		public PaperlessTechnology(){
			
		}

		///<Summary>There might be incoming files that we have to watch for.  They will get processed and deleted.  There is no user interface for this function.  This method is called when OD first starts up.</Summary>
		public static void InitializeFileWatcher(){
			if(!Directory.Exists(dir)){
				if(watcher!=null){
					watcher.Dispose();
				}
				return;
			}
			watcher = new FileSystemWatcher();
			watcher.Path =dir;
			//watcher.NotifyFilter = NotifyFilters.CreationTime;
			string importFileName=importCsv;
			watcher.Filter=importFileName;
			watcher.Created+=new FileSystemEventHandler(OnCreated);
			watcher.EnableRaisingEvents = true;
		}

		private static void OnCreated(object source,FileSystemEventArgs e) {
			MessageBox.Show("File created.  It will now be deleted.");
			File.Delete(e.FullPath);
		}

		///<summary>Sends data for Patient.Cur to an export file and then launches an exe to notify PT.  If patient exists, this simply opens the patient.  If patient does not exist, then this triggers creation of the patient in PT Dental.  If isUpdate is true, then the export file and exe will have different names. In PT, update is a separate programlink with a separate button.</summary>
		public static void SendData(Program ProgramCur, Patient pat,bool isUpdate){
			//ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			//ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "InfoFile path");
			//string infoFile=PPCur.PropertyValue;
			if(!Directory.Exists(dir)){
				MessageBox.Show(dir+" does not exist.  PT Dental doesn't seem to be properly installed on this computer.");
				return;
			}
			if(pat==null){
				MessageBox.Show("No patient is selected.");
				return;
			}
			string filename=dir+"\\"+exportAddCsv;
			if(isUpdate){
				filename=dir+"\\"+exportUpdateCsv;
			}
			using(StreamWriter sw=new StreamWriter(filename,false)){//overwrites if it already exists.
				sw.WriteLine("PAT_PK,PAT_LOGFK,PAT_LANFK,PAT_TITLE,PAT_FNAME,PAT_MI,PAT_LNAME,PAT_CALLED,PAT_ADDR1,PAT_ADDR2,PAT_CITY,PAT_ST,PAT_ZIP,PAT_HPHN,PAT_WPHN,PAT_EXT,PAT_FAX,PAT_PAGER,PAT_CELL,PAT_EMAIL,PAT_SEX,PAT_EDOCS,PAT_STATUS,PAT_TYPE,PAT_BIRTH,PAT_SSN,PAT_NOCALL,PAT_NOCORR,PAT_DISRES,PAT_LSTUPD,PAT_INSNM,PAT_INSGPL,PAT_INSAD1,PAT_INSAD2,PAT_INSCIT,PAT_INSST,PAT_INSZIP,PAT_INSPHN,PAT_INSEXT,PAT_INSCON,PAT_INSGNO,PAT_EMPNM,PAT_EMPAD1,PAT_EMPAD2,PAT_EMPCIT,PAT_EMPST,PAT_EMPZIP,PAT_EMPPHN,PAT_REFLNM,PAT_REFFNM,PAT_REFMI,PAT_REFPHN,PAT_REFEML,PAT_REFSPE,PAT_NOTES,PAT_FPSCAN,PAT_PREMED,PAT_MEDS,PAT_FTSTUD,PAT_PTSTUD,PAT_COLLEG,PAT_CHRTNO,PAT_OTHID,PAT_RESPRT,PAT_POLHLD,PAT_CUSCD,PAT_PMPID");
				sw.Write(",");//PAT_PK  Primary key. Long alphanumeric. We do not use.
				sw.Write(",");//PAT_LOGFK Internal PT logical, it can be ignored.
				sw.Write(",");//PAT_LANFK Internal PT logical, it can be ignored.
				sw.Write(",");//PAT_TITLE We do not have this field yet
				sw.Write(Tidy(pat.FName)+",");//PAT_FNAME
				sw.Write(Tidy(pat.MiddleI)+",");//PAT_MI
				sw.Write(Tidy(pat.LName)+",");//PAT_LNAME
				sw.Write(Tidy(pat.Preferred)+",");//PAT_CALLED Nickname
				sw.Write(Tidy(pat.Address)+",");//PAT_ADDR1
				sw.Write(Tidy(pat.Address2)+",");//PAT_ADDR2
				sw.Write(Tidy(pat.City)+",");//PAT_CITY
				sw.Write(Tidy(pat.State)+",");//PAT_ST
				sw.Write(Tidy(pat.Zip)+",");//PAT_ZIP
				sw.Write(TidyNumber(pat.HmPhone)+",");//PAT_HPHN No punct
				sw.Write(TidyNumber(pat.WkPhone)+",");//PAT_WPHN
				sw.Write(",");//PAT_EXT
				sw.Write(",");//PAT_FAX
				sw.Write(",");//PAT_PAGER
				sw.Write(TidyNumber(pat.WirelessPhone)+",");//PAT_CELL
				sw.Write(Tidy(pat.Email)+",");//PAT_EMAIL
				if(pat.Gender==PatientGender.Female){
					sw.Write("F");
				}
				else if(pat.Gender==PatientGender.Male) {
					sw.Write("M");
				}
				sw.Write(",");//PAT_SEX might be blank if unknown
				sw.Write(",");//PAT_EDOCS Internal PT logical, it can be ignored.
				sw.Write(pat.PatStatus.ToString()+",");//PAT_STATUS Any text allowed
				sw.Write(pat.Position.ToString()+",");//PAT_TYPE Any text allowed
				if(pat.Birthdate.Year>1880){
					sw.Write(pat.Birthdate.ToString("yyyyMMdd"));//PAT_BIRTH yyyyMMdd
				}
				sw.Write(",");
				sw.Write(Tidy(pat.SSN)+",");//PAT_SSN No punct
				if(pat.PreferContactMethod==ContactMethod.DoNotCall
					|| pat.PreferConfirmMethod==ContactMethod.DoNotCall
					|| pat.PreferRecallMethod==ContactMethod.DoNotCall)
				{
					sw.Write("T");
				}
				sw.Write(",");//PAT_NOCALL T if no call
				sw.Write(",");//PAT_NOCORR No correspondence HIPAA
				sw.Write(",");//PAT_DISRES Internal PT logical, it can be ignored.
				sw.Write(",");//PAT_LSTUPD Internal PT logical, it can be ignored.
				PatPlan[] patPlanList=PatPlans.Refresh(pat.PatNum);
				Family fam=Patients.GetFamily(pat.PatNum);
				InsPlan[] planList=InsPlans.Refresh(fam);
				PatPlan patplan=null;
				InsPlan plan=null;
				Carrier carrier=null;
				Employer emp=null;
				if(patPlanList.Length>0){
					patplan=patPlanList[0];
					plan=InsPlans.GetPlan(patplan.PlanNum,planList);
					carrier=Carriers.GetCarrier(plan.CarrierNum);
					if(plan.EmployerNum!=0){
						emp=Employers.GetEmployer(plan.EmployerNum);
					}
				}
				if(plan==null){
					sw.Write(",");//PAT_INSNM
					sw.Write(",");//PAT_INSGPL Ins group plan name
					sw.Write(",");//PAT_INSAD1
					sw.Write(",");//PAT_INSAD2
					sw.Write(",");//PAT_INSCIT
					sw.Write(",");//PAT_INSST
					sw.Write(",");//PAT_INSZIP
					sw.Write(",");//PAT_INSPHN
				}
				else{
					sw.Write(Tidy(carrier.CarrierName)+",");//PAT_INSNM
					sw.Write(Tidy(plan.GroupName)+",");//PAT_INSGPL Ins group plan name
					sw.Write(Tidy(carrier.Address)+",");//PAT_INSAD1
					sw.Write(Tidy(carrier.Address2)+",");//PAT_INSAD2
					sw.Write(Tidy(carrier.City)+",");//PAT_INSCIT
					sw.Write(Tidy(carrier.State)+",");//PAT_INSST
					sw.Write(Tidy(carrier.Zip)+",");//PAT_INSZIP
					sw.Write(TidyNumber(carrier.Phone)+",");//PAT_INSPHN
				}
				sw.Write(",");//PAT_INSEXT
				sw.Write(",");//PAT_INSCON Ins contact person
				sw.Write(",");//PAT_INSGNO Ins group number
				if(emp==null){
					sw.Write(",");//PAT_EMPNM
					sw.Write(",");//PAT_EMPAD1
					sw.Write(",");//PAT_EMPAD2
					sw.Write(",");//PAT_EMPCIT
					sw.Write(",");//PAT_EMPST
					sw.Write(",");//PAT_EMPZIP
					sw.Write(",");//PAT_EMPPHN
				}
				else{
					sw.Write(Tidy(emp.EmpName)+",");//PAT_EMPNM
					sw.Write(Tidy(emp.Address)+",");//PAT_EMPAD1
					sw.Write(Tidy(emp.Address2)+",");//PAT_EMPAD2
					sw.Write(Tidy(emp.City)+",");//PAT_EMPCIT
					sw.Write(Tidy(emp.State)+",");//PAT_EMPST
					sw.Write(Tidy(emp.State)+",");//PAT_EMPZIP
					sw.Write(TidyNumber(emp.Phone)+",");//PAT_EMPPHN
				}
				Referral referral=Referrals.GetReferralForPat(pat.PatNum);//could be null
				if(referral==null){
					sw.Write(",");//PAT_REFLNM
					sw.Write(",");//PAT_REFFNM
					sw.Write(",");//PAT_REFMI
					sw.Write(",");//PAT_REFPHN
					sw.Write(",");//PAT_REFEML Referral source email
					sw.Write(",");//PAT_REFSPE Referral specialty. Customizable, so any allowed
				}
				else{
					sw.Write(Tidy(referral.LName)+",");//PAT_REFLNM
					sw.Write(Tidy(referral.FName)+",");//PAT_REFFNM
					sw.Write(Tidy(referral.MName)+",");//PAT_REFMI
					sw.Write(referral.Telephone+",");//PAT_REFPHN
					sw.Write(Tidy(referral.EMail)+",");//PAT_REFEML Referral source email
					if(referral.PatNum==0 && !referral.NotPerson){//not a patient, and is a person
						sw.Write(referral.Specialty.ToString());
					}
					sw.Write(",");//PAT_REFSPE Referral specialty. Customizable, so any allowed
				}
				sw.Write(",");//PAT_NOTES No limits.  We won't use this right now for exports.
				//sw.Write(",");//PAT_NOTE1-PAT_NOTE10 skipped
				sw.Write(",");//PAT_FPSCAN Internal PT logical, it can be ignored.
				if(pat.Premed){
					sw.Write("T");
				}
				else{
					sw.Write("F");
				}
				sw.Write(",");//PAT_PREMED F or T
				sw.Write(Tidy(pat.MedUrgNote)+",");//PAT_MEDS The meds that they must premedicate with.
				if(pat.StudentStatus=="F"){//fulltime
					sw.Write("T");
				}
				else{
					sw.Write("F");
				}
				sw.Write(",");//PAT_FTSTUD T/F
				if(pat.StudentStatus=="P") {//parttime
					sw.Write("T");
				}
				else {
					sw.Write("F");
				}
				sw.Write(",");//PAT_PTSTUD
				sw.Write(Tidy(pat.SchoolName)+",");//PAT_COLLEG Name of college
				sw.Write(Tidy(pat.ChartNumber)+",");//PAT_CHRTNO
				sw.Write(pat.PatNum.ToString()+",");//PAT_OTHID The primary key in Open Dental ************IMPORTANT***************
				if(pat.PatNum==pat.Guarantor){
					sw.Write("T");
				}
				else {
					sw.Write("F");
				}
				sw.Write(",");//PAT_RESPRT Responsible party checkbox T/F
				if(plan!=null && pat.PatNum==plan.Subscriber) {//if current patient is the subscriber on their primary plan
					sw.Write("T");
				}
				else {
					sw.Write("F");
				}
				sw.Write(",");//PAT_POLHLD Policy holder checkbox T/F
				sw.Write(",");//PAT_CUSCD Web sync folder, used internally this can be ignored.
				sw.Write(",");//PAT_PMPID Practice Management Program ID. Can be ignored
				sw.WriteLine();
			}
			try{
				//Process.Start(ProgramCur.Path,"@"+infoFile);
				MessageBox.Show("done");
			}
			catch{
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
		}

		///<summary>removes commas.</summary>
		private static string Tidy(string str){
			return str.Replace(",","");
		}

		///<summary>removes commas, dashes, parentheses, and spaces.  It would be better to use regex to strip all non-numbers.</summary>
		private static string TidyNumber(string str) {
			//Regex.
			str=str.Replace(",","");
			str=str.Replace("-","");
			str=str.Replace("(","");
			str=str.Replace(")","");
			str=str.Replace(" ","");
			return str;
		}

		//<Summary>In PT, update is a separate programlink with a separate button.</Summary>
		//public static void SendUpdate(Program ProgramCur, Patient pat){

		//}



	}
}










