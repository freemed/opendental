using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	
	///<summary></summary>
	public class Patients{
		///<summary>A list of all patient names. Key=patNum, value=formatted name.  Fill with GetHList.  Used in FormQuery, FormTrackNext, and FormUnsched.</summary>
		public static Hashtable HList;
		///<summary>Collection of Patient Names. The last five patients. Gets displayed on dropdown button.</summary>
		private static ArrayList buttonLastFiveNames;
		///<summary>Collection of PatNums. The last five patients. Used when clicking on dropdown button.</summary>
		private static ArrayList buttonLastFivePatNums;

		///<summary>Returns a Family object for the supplied patNum.  Use Family.GetPatient to extract the desired patient from the family.</summary>
		public static Family GetFamily(int patNum){
			string command= 
				"SELECT guarantor FROM patient "
				+"WHERE patnum = '"+POut.PInt(patNum)+"'";
 			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return null;
			}
			if(FormChooseDatabase.DBtype==DatabaseType.MySql){
				command= 
					"SELECT patient.* "
					+"FROM patient "
					+"WHERE Guarantor = '"+table.Rows[0][0].ToString()+"'"
					+" ORDER BY Guarantor!=PatNum,Birthdate";
			}
			else if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command= 
					"SELECT patient.*,CASE WHEN PatNum=Guarantor THEN 0 ELSE 1 END AS isguarantor "
					+"FROM patient "
					+"WHERE Guarantor = '"+table.Rows[0][0].ToString()+"'"
					+" ORDER BY 68,Birthdate";//just asking for bugs
			}
			Family fam=new Family();
			fam.List=SubmitAndFill(command);
			return fam;
		}

		///<summary>This is a way to get a single patient from the database if you don't already have a family object to use.</summary>
		public static Patient GetPat(int patNum){
			if(patNum==0) {
				return null;
			}
			string command="SELECT * FROM patient WHERE PatNum="+POut.PInt(patNum);
			return SubmitAndFill(command)[0];
		}

		private static Patient[] SubmitAndFill(string command){
 			DataTable table=General.GetTable(command);
			Patient[] retVal=new Patient[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retVal[i]=new Patient();
				retVal[i].PatNum       = PIn.PInt   (table.Rows[i][0].ToString());
				retVal[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				retVal[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				retVal[i].MiddleI      = PIn.PString(table.Rows[i][3].ToString());
				retVal[i].Preferred    = PIn.PString(table.Rows[i][4].ToString());
				retVal[i].PatStatus    = (PatientStatus)PIn.PInt   (table.Rows[i][5].ToString());
				retVal[i].Gender       = (PatientGender)PIn.PInt   (table.Rows[i][6].ToString());
				retVal[i].Position     = (PatientPosition)PIn.PInt   (table.Rows[i][7].ToString());
				retVal[i].Birthdate    = PIn.PDate  (table.Rows[i][8].ToString());
				retVal[i].Age=Shared.DateToAge(retVal[i].Birthdate);
				//Debug.WriteLine("*"+retVal[i].Age+"*");
				retVal[i].SSN          = PIn.PString(table.Rows[i][9].ToString());
				retVal[i].Address      = PIn.PString(table.Rows[i][10].ToString());
				retVal[i].Address2     = PIn.PString(table.Rows[i][11].ToString());
				retVal[i].City         = PIn.PString(table.Rows[i][12].ToString());
				retVal[i].State        = PIn.PString(table.Rows[i][13].ToString());
				retVal[i].Zip          = PIn.PString(table.Rows[i][14].ToString());
				retVal[i].HmPhone      = PIn.PString(table.Rows[i][15].ToString());
				retVal[i].WkPhone      = PIn.PString(table.Rows[i][16].ToString());
				retVal[i].WirelessPhone= PIn.PString(table.Rows[i][17].ToString());
				retVal[i].Guarantor    = PIn.PInt   (table.Rows[i][18].ToString());
				retVal[i].CreditType   = PIn.PString(table.Rows[i][19].ToString());
				retVal[i].Email        = PIn.PString(table.Rows[i][20].ToString());
				retVal[i].Salutation   = PIn.PString(table.Rows[i][21].ToString());
				retVal[i].EstBalance   = PIn.PDouble(table.Rows[i][22].ToString());
				retVal[i].NextAptNum   = PIn.PInt   (table.Rows[i][23].ToString());
				retVal[i].PriProv      = PIn.PInt   (table.Rows[i][24].ToString());
				retVal[i].SecProv      = PIn.PInt   (table.Rows[i][25].ToString());
				retVal[i].FeeSched     = PIn.PInt   (table.Rows[i][26].ToString());
				retVal[i].BillingType  = PIn.PInt   (table.Rows[i][27].ToString());
				retVal[i].ImageFolder  = PIn.PString(table.Rows[i][28].ToString());
				retVal[i].AddrNote     = PIn.PString(table.Rows[i][29].ToString());
				retVal[i].FamFinUrgNote= PIn.PString(table.Rows[i][30].ToString());
				retVal[i].MedUrgNote   = PIn.PString(table.Rows[i][31].ToString());
				retVal[i].ApptModNote  = PIn.PString(table.Rows[i][32].ToString());
				retVal[i].StudentStatus= PIn.PString(table.Rows[i][33].ToString());
				retVal[i].SchoolName   = PIn.PString(table.Rows[i][34].ToString());
				retVal[i].ChartNumber  = PIn.PString(table.Rows[i][35].ToString());
				retVal[i].MedicaidID   = PIn.PString(table.Rows[i][36].ToString());
				retVal[i].Bal_0_30     = PIn.PDouble(table.Rows[i][37].ToString());
				retVal[i].Bal_31_60    = PIn.PDouble(table.Rows[i][38].ToString());
				retVal[i].Bal_61_90    = PIn.PDouble(table.Rows[i][39].ToString());
				retVal[i].BalOver90    = PIn.PDouble(table.Rows[i][40].ToString());
				retVal[i].InsEst       = PIn.PDouble(table.Rows[i][41].ToString());
				//retVal[i].PrimaryTeeth = PIn.PString(table.Rows[i][42].ToString());
				retVal[i].BalTotal     = PIn.PDouble(table.Rows[i][43].ToString());
				retVal[i].EmployerNum  = PIn.PInt   (table.Rows[i][44].ToString());
				retVal[i].EmploymentNote=PIn.PString(table.Rows[i][45].ToString());
				retVal[i].Race         = (PatientRace)PIn.PInt(table.Rows[i][46].ToString());
				retVal[i].County       = PIn.PString(table.Rows[i][47].ToString());
				retVal[i].GradeSchool  = PIn.PString(table.Rows[i][48].ToString());
				retVal[i].GradeLevel   = (PatientGrade)PIn.PInt(table.Rows[i][49].ToString());
				retVal[i].Urgency      = (TreatmentUrgency)PIn.PInt(table.Rows[i][50].ToString());
				retVal[i].DateFirstVisit=PIn.PDate  (table.Rows[i][51].ToString());
				retVal[i].ClinicNum    = PIn.PInt   (table.Rows[i][52].ToString());
				retVal[i].HasIns       = PIn.PString(table.Rows[i][53].ToString());
				retVal[i].TrophyFolder = PIn.PString(table.Rows[i][54].ToString());
				retVal[i].PlannedIsDone= PIn.PBool  (table.Rows[i][55].ToString());
				retVal[i].Premed       = PIn.PBool  (table.Rows[i][56].ToString());
				retVal[i].Ward         = PIn.PString(table.Rows[i][57].ToString());
				retVal[i].PreferConfirmMethod=(ContactMethod)PIn.PInt(table.Rows[i][58].ToString());
				retVal[i].PreferContactMethod=(ContactMethod)PIn.PInt(table.Rows[i][59].ToString());
				retVal[i].PreferRecallMethod=(ContactMethod)PIn.PInt(table.Rows[i][60].ToString());
				retVal[i].SchedBeforeTime= PIn.PDateT(table.Rows[i][61].ToString());
				retVal[i].SchedAfterTime= PIn.PDateT(table.Rows[i][62].ToString());
				retVal[i].SchedDayOfWeek= PIn.PInt(table.Rows[i][63].ToString());
				retVal[i].Language     = PIn.PString(table.Rows[i][64].ToString());
				retVal[i].AdmitDate    = PIn.PDate  (table.Rows[i][65].ToString());
				retVal[i].Title        = PIn.PString(table.Rows[i][66].ToString());
				//WARNING.  If you add any rows, you MUST change the number in GetFamily(). Always last num above +2.
			}
			return retVal;
		}

		///<summary>ONLY for new patients. Set includePatNum to true for use the patnum from the import function.  Otherwise, uses InsertID to fill PatNum.</summary>
		public static void Insert(Patient pat, bool includePatNum) {
			if(!includePatNum && PrefB.RandomKeys) {
				pat.PatNum=MiscData.GetKey("patient","PatNum");
			}
			string command= "INSERT INTO patient (";
			if(includePatNum || PrefB.RandomKeys) {
				command+="PatNum,";
			}
			command+="lname,fname,middlei,preferred,patstatus,gender,"
				+"position,birthdate,ssn,address,address2,city,state,zip,hmphone,wkphone,wirelessphone,"
				+"guarantor,credittype,email,salutation,"
				+"estbalance,nextaptnum,priprov,secprov,feesched,billingtype,"
				+"imagefolder,addrnote,famfinurgnote,medurgnote,apptmodnote,"
				+"studentstatus,schoolname,chartnumber,medicaidid"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,insest,BalTotal"
				+",EmployerNum,EmploymentNote,Race,County,GradeSchool,GradeLevel,Urgency,DateFirstVisit"
				+",ClinicNum,HasIns,TrophyFolder,PlannedIsDone,Premed,Ward,PreferConfirmMethod,PreferContactMethod,PreferRecallMethod"
				+",SchedBeforeTime,SchedAfterTime"
				+",SchedDayOfWeek,Language,AdmitDate,Title) VALUES(";
			if(includePatNum || PrefB.RandomKeys) {
				command+="'"+POut.PInt(pat.PatNum)+"', ";
			}
			command+="'"+POut.PString(pat.LName)+"', "
				+"'"+POut.PString(pat.FName)+"', "
				+"'"+POut.PString(pat.MiddleI)+"', "
				+"'"+POut.PString(pat.Preferred)+"', "
				+"'"+POut.PInt((int)pat.PatStatus)+"', "
				+"'"+POut.PInt((int)pat.Gender)+"', "
				+"'"+POut.PInt((int)pat.Position)+"', "
				+POut.PDate(pat.Birthdate)+", "
				+"'"+POut.PString(pat.SSN)+"', "
				+"'"+POut.PString(pat.Address)+"', "
				+"'"+POut.PString(pat.Address2)+"', "
				+"'"+POut.PString(pat.City)+"', "
				+"'"+POut.PString(pat.State)+"', "
				+"'"+POut.PString(pat.Zip)+"', "
				+"'"+POut.PString(pat.HmPhone)+"', "
				+"'"+POut.PString(pat.WkPhone)+"', "
				+"'"+POut.PString(pat.WirelessPhone)+"', "
				+"'"+POut.PInt(pat.Guarantor)+"', "
				+"'"+POut.PString(pat.CreditType)+"', "
				+"'"+POut.PString(pat.Email)+"', "
				+"'"+POut.PString(pat.Salutation)+"', "
				+"'"+POut.PDouble(pat.EstBalance)+"', "
				+"'"+POut.PInt(pat.NextAptNum)+"', "
				+"'"+POut.PInt(pat.PriProv)+"', "
				+"'"+POut.PInt(pat.SecProv)+"', "
				+"'"+POut.PInt(pat.FeeSched)+"', "
				+"'"+POut.PInt(pat.BillingType)+"', "
				+"'"+POut.PString(pat.ImageFolder)+"', "
				+"'"+POut.PString(pat.AddrNote)+"', "
				+"'"+POut.PString(pat.FamFinUrgNote)+"', "
				+"'"+POut.PString(pat.MedUrgNote)+"', "
				+"'"+POut.PString(pat.ApptModNote)+"', "
				+"'"+POut.PString(pat.StudentStatus)+"', "
				+"'"+POut.PString(pat.SchoolName)+"', "
				+"'"+POut.PString(pat.ChartNumber)+"', "
				+"'"+POut.PString(pat.MedicaidID)+"', "
				+"'"+POut.PDouble(pat.Bal_0_30)+"', "
				+"'"+POut.PDouble(pat.Bal_31_60)+"', "
				+"'"+POut.PDouble(pat.Bal_61_90)+"', "
				+"'"+POut.PDouble(pat.BalOver90)+"', "
				+"'"+POut.PDouble(pat.InsEst)+"', "
				+"'"+POut.PDouble(pat.BalTotal)+"', "
				+"'"+POut.PInt(pat.EmployerNum)+"', "
				+"'"+POut.PString(pat.EmploymentNote)+"', "
				+"'"+POut.PInt((int)pat.Race)+"', "
				+"'"+POut.PString(pat.County)+"', "
				+"'"+POut.PString(pat.GradeSchool)+"', "
				+"'"+POut.PInt((int)pat.GradeLevel)+"', "
				+"'"+POut.PInt((int)pat.Urgency)+"', "
				+POut.PDate(pat.DateFirstVisit)+", "
				+"'"+POut.PInt(pat.ClinicNum)+"', "
				+"'"+POut.PString(pat.HasIns)+"', "
				+"'"+POut.PString(pat.TrophyFolder)+"', "
				+"'"+POut.PBool(pat.PlannedIsDone)+"', "
				+"'"+POut.PBool(pat.Premed)+"', "
				+"'"+POut.PString(pat.Ward)+"', "
				+"'"+POut.PInt((int)pat.PreferConfirmMethod)+"', "
				+"'"+POut.PInt((int)pat.PreferContactMethod)+"', "
				+"'"+POut.PInt((int)pat.PreferRecallMethod)+"', "
				+POut.PDateT(pat.SchedBeforeTime)+", "
				+POut.PDateT(pat.SchedAfterTime)+", "
				+"'"+POut.PInt(pat.SchedDayOfWeek)+"', "
				+"'"+POut.PString(pat.Language)+"', "
				+POut.PDate(pat.AdmitDate)+", "
				+"'"+POut.PString(pat.Title)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				pat.PatNum=General.NonQ(command,true);
			}
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply the old Patient object to compare for changes.</summary>
		public static int Update(Patient pat, Patient CurOld) {
			bool comma=false;
			string c = "UPDATE patient SET ";
			if(pat.LName!=CurOld.LName) {
				c+="LName = '"     +POut.PString(pat.LName)+"'";
				comma=true;
			}
			if(pat.FName!=CurOld.FName) {
				if(comma)
					c+=",";
				c+="FName = '"     +POut.PString(pat.FName)+"'";
				comma=true;
			}
			if(pat.MiddleI!=CurOld.MiddleI) {
				if(comma)
					c+=",";
				c+="MiddleI = '"   +POut.PString(pat.MiddleI)+"'";
				comma=true;
			}
			if(pat.Preferred!=CurOld.Preferred) {
				if(comma)
					c+=",";
				c+="Preferred = '" +POut.PString(pat.Preferred)+"'";
				comma=true;
			}
			if(pat.PatStatus!=CurOld.PatStatus) {
				if(comma)
					c+=",";
				c+="PatStatus = '" +POut.PInt((int)pat.PatStatus)+"'";
				comma=true;
			}
			if(pat.Gender!=CurOld.Gender) {
				if(comma)
					c+=",";
				c+="Gender = '"    +POut.PInt((int)pat.Gender)+"'";
				comma=true;
			}
			if(pat.Position!=CurOld.Position) {
				if(comma)
					c+=",";
				c+="Position = '"  +POut.PInt((int)pat.Position)+"'";
				comma=true;
			}
			if(pat.Birthdate!=CurOld.Birthdate) {
				if(comma)
					c+=",";
				c+="Birthdate = " +POut.PDate(pat.Birthdate);
				comma=true;
			}
			if(pat.SSN!=CurOld.SSN) {
				if(comma)
					c+=",";
				c+="SSN = '"       +POut.PString(pat.SSN)+"'";
				comma=true;
			}
			if(pat.Address!=CurOld.Address) {
				if(comma)
					c+=",";
				c+="Address = '"   +POut.PString(pat.Address)+"'";
				comma=true;
			}
			if(pat.Address2!=CurOld.Address2) {
				if(comma)
					c+=",";
				c+="Address2 = '"  +POut.PString(pat.Address2)+"'";
				comma=true;
			}
			if(pat.City!=CurOld.City) {
				if(comma)
					c+=",";
				c+="City = '"      +POut.PString(pat.City)+"'";
				comma=true;
			}
			if(pat.State!=CurOld.State) {
				if(comma)
					c+=",";
				c+="State = '"     +POut.PString(pat.State)+"'";
				comma=true;
			}
			if(pat.Zip!=CurOld.Zip) {
				if(comma)
					c+=",";
				c+="Zip = '"       +POut.PString(pat.Zip)+"'";
				comma=true;
			}
			if(pat.HmPhone!=CurOld.HmPhone) {
				if(comma)
					c+=",";
				c+="HmPhone = '"   +POut.PString(pat.HmPhone)+"'";
				comma=true;
			}
			if(pat.WkPhone!=CurOld.WkPhone) {
				if(comma)
					c+=",";
				c+="WkPhone = '"   +POut.PString(pat.WkPhone)+"'";
				comma=true;
			}
			if(pat.WirelessPhone!=CurOld.WirelessPhone) {
				if(comma)
					c+=",";
				c+="WirelessPhone='"    +POut.PString(pat.WirelessPhone)+"'";
				comma=true;
			}
			if(pat.Guarantor!=CurOld.Guarantor) {
				if(comma)
					c+=",";
				c+="Guarantor = '"      +POut.PInt(pat.Guarantor)+"'";
				comma=true;
			}
			if(pat.CreditType!=CurOld.CreditType) {
				if(comma)
					c+=",";
				c+="CreditType = '"     +POut.PString(pat.CreditType)+"'";
				comma=true;
			}
			if(pat.Email!=CurOld.Email) {
				if(comma)
					c+=",";
				c+="Email = '"          +POut.PString(pat.Email)+"'";
				comma=true;
			}
			if(pat.Salutation!=CurOld.Salutation) {
				if(comma)
					c+=",";
				c+="Salutation = '"     +POut.PString(pat.Salutation)+"'";
				comma=true;
			}
			if(pat.EstBalance!=CurOld.EstBalance) {
				if(comma)
					c+=",";
				c+="EstBalance = '"     +POut.PDouble(pat.EstBalance)+"'";
				comma=true;
			}
			if(pat.NextAptNum!=CurOld.NextAptNum) {
				if(comma)
					c+=",";
				c+="NextAptNum = '"     +POut.PInt(pat.NextAptNum)+"'";
				comma=true;
			}
			if(pat.PriProv!=CurOld.PriProv) {
				if(comma)
					c+=",";
				c+="PriProv = '"        +POut.PInt(pat.PriProv)+"'";
				comma=true;
			}
			if(pat.SecProv!=CurOld.SecProv) {
				if(comma)
					c+=",";
				c+="SecProv = '"        +POut.PInt(pat.SecProv)+"'";
				comma=true;
			}
			if(pat.FeeSched!=CurOld.FeeSched) {
				if(comma)
					c+=",";
				c+="FeeSched = '"       +POut.PInt(pat.FeeSched)+"'";
				comma=true;
			}
			if(pat.BillingType!=CurOld.BillingType) {
				if(comma)
					c+=",";
				c+="BillingType = '"    +POut.PInt(pat.BillingType)+"'";
				comma=true;
			}
			if(pat.ImageFolder!=CurOld.ImageFolder) {
				if(comma)
					c+=",";
				c+="ImageFolder = '"    +POut.PString(pat.ImageFolder)+"'";
				comma=true;
			}
			if(pat.AddrNote!=CurOld.AddrNote) {
				if(comma)
					c+=",";
				c+="AddrNote = '"       +POut.PString(pat.AddrNote)+"'";
				comma=true;
			}
			if(pat.FamFinUrgNote!=CurOld.FamFinUrgNote) {
				if(comma)
					c+=",";
				c+="FamFinUrgNote = '"  +POut.PString(pat.FamFinUrgNote)+"'";
				comma=true;
			}
			if(pat.MedUrgNote!=CurOld.MedUrgNote) {
				if(comma)
					c+=",";
				c+="MedUrgNote = '"     +POut.PString(pat.MedUrgNote)+"'";
				comma=true;
			}
			if(pat.ApptModNote!=CurOld.ApptModNote) {
				if(comma)
					c+=",";
				c+="ApptModNote = '"    +POut.PString(pat.ApptModNote)+"'";
				comma=true;
			}
			if(pat.StudentStatus!=CurOld.StudentStatus) {
				if(comma)
					c+=",";
				c+="StudentStatus = '"  +POut.PString(pat.StudentStatus)+"'";
				comma=true;
			}
			if(pat.SchoolName!=CurOld.SchoolName) {
				if(comma)
					c+=",";
				c+="SchoolName = '"     +POut.PString(pat.SchoolName)+"'";
				comma=true;
			}
			if(pat.ChartNumber!=CurOld.ChartNumber) {
				if(comma)
					c+=",";
				c+="ChartNumber = '"    +POut.PString(pat.ChartNumber)+"'";
				comma=true;
			}
			if(pat.MedicaidID!=CurOld.MedicaidID) {
				if(comma)
					c+=",";
				c+="MedicaidID = '"     +POut.PString(pat.MedicaidID)+"'";
				comma=true;
			}
			if(pat.Bal_0_30!=CurOld.Bal_0_30) {
				if(comma)
					c+=",";
				c+="Bal_0_30 = '"       +POut.PDouble(pat.Bal_0_30)+"'";
				comma=true;
			}
			if(pat.Bal_31_60!=CurOld.Bal_31_60) {
				if(comma)
					c+=",";
				c+="Bal_31_60 = '"      +POut.PDouble(pat.Bal_31_60)+"'";
				comma=true;
			}
			if(pat.Bal_61_90!=CurOld.Bal_61_90) {
				if(comma)
					c+=",";
				c+="Bal_61_90 = '"      +POut.PDouble(pat.Bal_61_90)+"'";
				comma=true;
			}
			if(pat.BalOver90!=CurOld.BalOver90) {
				if(comma)
					c+=",";
				c+="BalOver90 = '"      +POut.PDouble(pat.BalOver90)+"'";
				comma=true;
			}
			if(pat.InsEst!=CurOld.InsEst) {
				if(comma)
					c+=",";
				c+="InsEst    = '"      +POut.PDouble(pat.InsEst)+"'";
				comma=true;
			}
			if(pat.BalTotal!=CurOld.BalTotal) {
				if(comma)
					c+=",";
				c+="BalTotal = '"       +POut.PDouble(pat.BalTotal)+"'";
				comma=true;
			}
			if(pat.EmployerNum!=CurOld.EmployerNum) {
				if(comma)
					c+=",";
				c+="EmployerNum = '"    +POut.PInt(pat.EmployerNum)+"'";
				comma=true;
			}
			if(pat.EmploymentNote!=CurOld.EmploymentNote) {
				if(comma)
					c+=",";
				c+="EmploymentNote = '" +POut.PString(pat.EmploymentNote)+"'";
				comma=true;
			}
			if(pat.Race!=CurOld.Race) {
				if(comma)
					c+=",";
				c+="Race = '"           +POut.PInt((int)pat.Race)+"'";
				comma=true;
			}
			if(pat.County!=CurOld.County) {
				if(comma)
					c+=",";
				c+="County = '"         +POut.PString(pat.County)+"'";
				comma=true;
			}
			if(pat.GradeSchool!=CurOld.GradeSchool) {
				if(comma)
					c+=",";
				c+="GradeSchool = '"    +POut.PString(pat.GradeSchool)+"'";
				comma=true;
			}
			if(pat.GradeLevel!=CurOld.GradeLevel) {
				if(comma)
					c+=",";
				c+="GradeLevel = '"     +POut.PInt((int)pat.GradeLevel)+"'";
				comma=true;
			}
			if(pat.Urgency!=CurOld.Urgency) {
				if(comma)
					c+=",";
				c+="Urgency = '"        +POut.PInt((int)pat.Urgency)+"'";
				comma=true;
			}
			if(pat.DateFirstVisit!=CurOld.DateFirstVisit) {
				if(comma)
					c+=",";
				c+="DateFirstVisit = " +POut.PDate(pat.DateFirstVisit);
				comma=true;
			}
			if(pat.ClinicNum!=CurOld.ClinicNum) {
				if(comma)
					c+=",";
				c+="ClinicNum = '"     +POut.PInt(pat.ClinicNum)+"'";
				comma=true;
			}
			if(pat.HasIns!=CurOld.HasIns) {
				if(comma)
					c+=",";
				c+="HasIns = '"     +POut.PString(pat.HasIns)+"'";
				comma=true;
			}
			if(pat.TrophyFolder!=CurOld.TrophyFolder) {
				if(comma)
					c+=",";
				c+="TrophyFolder = '"     +POut.PString(pat.TrophyFolder)+"'";
				comma=true;
			}
			if(pat.PlannedIsDone!=CurOld.PlannedIsDone) {
				if(comma)
					c+=",";
				c+="PlannedIsDone = '"     +POut.PBool(pat.PlannedIsDone)+"'";
				comma=true;
			}
			if(pat.Premed!=CurOld.Premed) {
				if(comma)
					c+=",";
				c+="Premed = '"     +POut.PBool(pat.Premed)+"'";
				comma=true;
			}
			if(pat.Ward!=CurOld.Ward) {
				if(comma)
					c+=",";
				c+="Ward = '"     +POut.PString(pat.Ward)+"'";
				comma=true;
			}
			if(pat.PreferConfirmMethod!=CurOld.PreferConfirmMethod) {
				if(comma)
					c+=",";
				c+="PreferConfirmMethod = '"     +POut.PInt((int)pat.PreferConfirmMethod)+"'";
				comma=true;
			}
			if(pat.PreferContactMethod!=CurOld.PreferContactMethod) {
				if(comma)
					c+=",";
				c+="PreferContactMethod = '"     +POut.PInt((int)pat.PreferContactMethod)+"'";
				comma=true;
			}
			if(pat.PreferRecallMethod!=CurOld.PreferRecallMethod) {
				if(comma)
					c+=",";
				c+="PreferRecallMethod = '"     +POut.PInt((int)pat.PreferRecallMethod)+"'";
				comma=true;
			}
			if(pat.SchedBeforeTime!=CurOld.SchedBeforeTime) {
				if(comma)
					c+=",";
				c+="SchedBeforeTime = "     +POut.PDateT(pat.SchedBeforeTime);
				comma=true;
			}
			if(pat.SchedAfterTime!=CurOld.SchedAfterTime) {
				if(comma)
					c+=",";
				c+="SchedAfterTime = "     +POut.PDateT(pat.SchedAfterTime);
				comma=true;
			}
			if(pat.SchedDayOfWeek!=CurOld.SchedDayOfWeek) {
				if(comma)
					c+=",";
				c+="SchedDayOfWeek = '"     +POut.PInt(pat.SchedDayOfWeek)+"'";
				comma=true;
			}
			if(pat.Language!=CurOld.Language) {
				if(comma)
					c+=",";
				c+="Language = '"     +POut.PString(pat.Language)+"'";
				comma=true;
			}
			if(pat.AdmitDate!=CurOld.AdmitDate) {
				if(comma)
					c+=",";
				c+="AdmitDate = "     +POut.PDate(pat.AdmitDate);
				comma=true;
			}
			if(pat.Title!=CurOld.Title) {
				if(comma)
					c+=",";
				c+="Title = '"     +POut.PString(pat.Title)+"'";
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE PatNum = '"   +POut.PInt(pat.PatNum)+"'";
			return General.NonQ(c);
		}//end UpdatePatient

		///<summary>Only used when entering a new patient and user clicks cancel. To delete an existing patient, the PatStatus is simply changed to 4.</summary>
		public static void Delete(Patient pat) {
			string command="DELETE FROM patient WHERE PatNum ="+pat.PatNum.ToString();
			General.NonQ(command);
		}

 		///<summary>Only used for the Select Patient dialog</summary>
		public static DataTable GetPtDataTable(bool limit,string lname,string fname,string phone,//string wkphone,
			string address,bool hideInactive,string city,string state,string ssn,string patnum,string chartnumber,
			int[] billingtypes,bool guarOnly,bool showArchived,int clinicNum)
		{
			//bool retval=false;
			string billingsnippet="";
			for(int i=0;i<billingtypes.Length;i++){
				if(i==0){
					billingsnippet+="AND (";
				}
				else{
					billingsnippet+="OR ";
				}
				billingsnippet+="BillingType ='"+billingtypes[i].ToString()+"' ";
				if(i==billingtypes.Length-1){//if there is only one row, this will also be triggered.
					billingsnippet+=") ";
				}
			}
			string phonedigits="";
			for(int i=0;i<phone.Length;i++){
				if(Regex.IsMatch(phone[i].ToString(),"[0-9]")){
					phonedigits=phonedigits+phone[i];
				}
			}
			string regexp="";
			for(int i=0;i<phonedigits.Length;i++){
				if(i!=0){
					regexp+="[^0-9]*";//zero or more intervening digits that are not numbers
				}
				regexp+=phonedigits[i];
			}
			string command= 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,WkPhone,Address,PatStatus"
				+",BillingType,ChartNumber,City,State,PriProv "
				+"FROM patient "
				+"WHERE PatStatus != '4' "//not status 'deleted'
				+"AND LName LIKE '"      +POut.PString(lname)+"%' "
				+"AND FName LIKE '"      +POut.PString(fname)+"%' ";
			if(regexp!=""){
				command+="AND (HmPhone REGEXP '"+POut.PString(regexp)+"' "
					+"OR WkPhone REGEXP '"+POut.PString(regexp)+"' "
					+"OR WirelessPhone REGEXP '"+POut.PString(regexp)+"') ";
			}
			command+=
				"AND Address LIKE '"    +POut.PString(address)+"%' "
				+"AND City LIKE '"       +POut.PString(city)+"%' "
				+"AND State LIKE '"      +POut.PString(state)+"%' "
				+"AND SSN LIKE '"        +POut.PString(ssn)+"%' "
				+"AND PatNum LIKE '"     +POut.PString(patnum)+"%' "
				+"AND ChartNumber LIKE '"+POut.PString(chartnumber)+"%' "
				+billingsnippet;
			if(hideInactive){
				command+="AND PatStatus != '2' ";
			}
			if(!showArchived) {
				command+="AND PatStatus != '3' AND PatStatus != '5' ";
			}
			if(guarOnly){
				command+="AND PatNum = Guarantor ";
			}
			if(clinicNum!=0){
				command+="AND ClinicNum="+POut.PInt(clinicNum)+" ";
			}
			command+="ORDER BY LName,FName ";
			if(limit){
				if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
					command="SELECT * FROM ("+command+") WHERE ROWNUM<=";
				}else{//Assume MySQL
					command+="LIMIT ";
				}
				command+="36";//only need 35, but the extra will help indicate more rows
			}
			//MessageBox.Show(command);
 			DataTable table=General.GetTable(command);
			DataTable PtDataTable=table.Clone();//does not copy any data
			for(int i=0;i<PtDataTable.Columns.Count;i++){
				PtDataTable.Columns[i].DataType=typeof(string);
			}
			//if(limit && table.Rows.Count==36){
			//	retval=true;
			//}
			DataRow r;
			for(int i=0;i<table.Rows.Count;i++){//table.Rows.Count && i<44;i++){
				r=PtDataTable.NewRow();
				//PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,WkPhone,Address,PatStatus"
				//+",BillingType,ChartNumber,City,State
				r[0]=table.Rows[i][0].ToString();//PatNum
				r[1]=table.Rows[i][1].ToString();//LName
				r[2]=table.Rows[i][2].ToString();//FName
				r[3]=table.Rows[i][3].ToString();//MiddleI
				r[4]=table.Rows[i][4].ToString();//Preferred
				r[5]=Shared.DateToAge(PIn.PDate(table.Rows[i][5].ToString()));//Birthdate
				r[6]=table.Rows[i][6].ToString();//SSN
				r[7]=table.Rows[i][7].ToString();//HmPhone
				r[8]=table.Rows[i][8].ToString();//WkPhone
				r[9]=table.Rows[i][9].ToString();//Address
				r[10]=((PatientStatus)PIn.PInt(table.Rows[i][10].ToString())).ToString();//PatStatus
				r[11]=DefB.GetName(DefCat.BillingTypes,PIn.PInt(table.Rows[i][11].ToString()));//BillingType
				r[12]=table.Rows[i][12].ToString();//ChartNumber
				r[13]=table.Rows[i][13].ToString();//City
				r[14]=table.Rows[i][14].ToString();//State
				r[15]=Providers.GetNameLF(PIn.PInt(table.Rows[i][15].ToString()));//PriProv
				PtDataTable.Rows.Add(r);
			}
			return PtDataTable;//retval;//if true, there are more rows.
		}

		///<summary>Used when filling appointments for an entire day. Gets a list of Pats, multPats, of all the specified patients.  Then, use GetOnePat to pull one patient from this list.  This process requires only one call to the database.</summary>
		public static Patient[] GetMultPats(int[] patNums){
			//MessageBox.Show(patNums.Length.ToString());
			string strPatNums="";
			DataTable table;
			if(patNums.Length>0){
				for(int i=0;i<patNums.Length;i++){
					if(i>0){
						strPatNums+="OR ";
					}
					strPatNums+="PatNum='"+patNums[i].ToString()+"' ";
				}
				string command="SELECT * from patient WHERE "+strPatNums;
				//MessageBox.Show(string command);
 				table=General.GetTable(command);
			}
			else{
				table=new DataTable();
			}
			Patient[] multPats=new Patient[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				multPats[i]=new Patient();
				multPats[i].PatNum       = PIn.PInt   (table.Rows[i][0].ToString());
				multPats[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				multPats[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				multPats[i].MiddleI      = PIn.PString(table.Rows[i][3].ToString());
				multPats[i].Preferred    = PIn.PString(table.Rows[i][4].ToString());
				multPats[i].PatStatus    = (PatientStatus)PIn.PInt   (table.Rows[i][5].ToString());
				multPats[i].Gender       = (PatientGender)PIn.PInt   (table.Rows[i][6].ToString());
				multPats[i].Position     = (PatientPosition)PIn.PInt   (table.Rows[i][7].ToString());
				multPats[i].Birthdate    = PIn.PDate  (table.Rows[i][8].ToString());
				multPats[i].Age=Shared.DateToAge(multPats[i].Birthdate);
				multPats[i].SSN          = PIn.PString(table.Rows[i][9].ToString());
				multPats[i].Address      = PIn.PString(table.Rows[i][10].ToString());
				multPats[i].Address2     = PIn.PString(table.Rows[i][11].ToString());
				multPats[i].City         = PIn.PString(table.Rows[i][12].ToString());
				multPats[i].State        = PIn.PString(table.Rows[i][13].ToString());
				multPats[i].Zip          = PIn.PString(table.Rows[i][14].ToString());
				multPats[i].HmPhone      = PIn.PString(table.Rows[i][15].ToString());
				multPats[i].WkPhone      = PIn.PString(table.Rows[i][16].ToString());
				multPats[i].WirelessPhone= PIn.PString(table.Rows[i][17].ToString());
				multPats[i].Guarantor    = PIn.PInt   (table.Rows[i][18].ToString());
				multPats[i].CreditType   = PIn.PString(table.Rows[i][19].ToString());
				multPats[i].Email        = PIn.PString(table.Rows[i][20].ToString());
				multPats[i].Salutation   = PIn.PString(table.Rows[i][21].ToString());
				multPats[i].EstBalance   = PIn.PDouble(table.Rows[i][22].ToString());
				multPats[i].NextAptNum   = PIn.PInt   (table.Rows[i][23].ToString());
				multPats[i].PriProv      = PIn.PInt   (table.Rows[i][24].ToString());
				multPats[i].SecProv      = PIn.PInt   (table.Rows[i][25].ToString());
				multPats[i].FeeSched     = PIn.PInt   (table.Rows[i][26].ToString());
				multPats[i].BillingType  = PIn.PInt   (table.Rows[i][27].ToString());
				multPats[i].ImageFolder  = PIn.PString(table.Rows[i][28].ToString());
				multPats[i].AddrNote     = PIn.PString(table.Rows[i][29].ToString());
				multPats[i].FamFinUrgNote= PIn.PString(table.Rows[i][30].ToString());
				multPats[i].MedUrgNote   = PIn.PString(table.Rows[i][31].ToString());
				multPats[i].ApptModNote  = PIn.PString(table.Rows[i][32].ToString());
				multPats[i].StudentStatus= PIn.PString(table.Rows[i][33].ToString());
				multPats[i].SchoolName   = PIn.PString(table.Rows[i][34].ToString());
				multPats[i].ChartNumber  = PIn.PString(table.Rows[i][35].ToString());
				multPats[i].MedicaidID   = PIn.PString(table.Rows[i][36].ToString());
				multPats[i].Bal_0_30     = PIn.PDouble(table.Rows[i][37].ToString());
				multPats[i].Bal_31_60    = PIn.PDouble(table.Rows[i][38].ToString());
				multPats[i].Bal_61_90    = PIn.PDouble(table.Rows[i][39].ToString());
				multPats[i].BalOver90    = PIn.PDouble(table.Rows[i][40].ToString());
				multPats[i].InsEst       = PIn.PDouble(table.Rows[i][41].ToString());
				//multPats[i].PrimaryTeeth = PIn.PString(table.Rows[i][42].ToString());
				multPats[i].BalTotal     = PIn.PDouble(table.Rows[i][43].ToString());
				multPats[i].EmployerNum  = PIn.PInt   (table.Rows[i][44].ToString());
				multPats[i].EmploymentNote=PIn.PString(table.Rows[i][45].ToString());
				multPats[i].Race         = (PatientRace)PIn.PInt(table.Rows[i][46].ToString());
				multPats[i].County       = PIn.PString(table.Rows[i][47].ToString());
				multPats[i].GradeSchool  = PIn.PString(table.Rows[i][48].ToString());
				multPats[i].GradeLevel   = (PatientGrade)PIn.PInt(table.Rows[i][49].ToString());
				multPats[i].Urgency      = (TreatmentUrgency)PIn.PInt(table.Rows[i][50].ToString());
				multPats[i].DateFirstVisit=PIn.PDate  (table.Rows[i][51].ToString());
				multPats[i].ClinicNum    = PIn.PInt   (table.Rows[i][52].ToString());
				multPats[i].HasIns       = PIn.PString(table.Rows[i][53].ToString());
				multPats[i].TrophyFolder = PIn.PString(table.Rows[i][54].ToString());
				multPats[i].PlannedIsDone= PIn.PBool  (table.Rows[i][55].ToString());
				multPats[i].Premed       = PIn.PBool  (table.Rows[i][56].ToString());
				multPats[i].Ward         = PIn.PString(table.Rows[i][57].ToString());
				multPats[i].PreferConfirmMethod=(ContactMethod)PIn.PInt(table.Rows[i][58].ToString());
				multPats[i].PreferContactMethod=(ContactMethod)PIn.PInt(table.Rows[i][59].ToString());
				multPats[i].PreferRecallMethod=(ContactMethod)PIn.PInt(table.Rows[i][60].ToString());
				multPats[i].SchedBeforeTime= PIn.PDateT(table.Rows[i][61].ToString());
				multPats[i].SchedAfterTime= PIn.PDateT(table.Rows[i][62].ToString());
				multPats[i].SchedDayOfWeek= PIn.PInt(table.Rows[i][63].ToString());
				multPats[i].Language     = PIn.PString(table.Rows[i][64].ToString());
				multPats[i].AdmitDate    = PIn.PDate(table.Rows[i][65].ToString());
				multPats[i].Title        = PIn.PString(table.Rows[i][66].ToString());
			}
			return multPats;
		}

		///<summary>First call GetMultPats to fill the list of multPats. Then, use this to return one patient from that list.</summary>
		public static Patient GetOnePat(Patient[] multPats, int patNum){
			for(int i=0;i<multPats.Length;i++){
				if(multPats[i].PatNum==patNum){
					return multPats[i];
				}
			}
			return new Patient();
		}

		/// <summary>Gets nine of the most useful fields from the db for the given patnum.</summary>
		public static Patient GetLim(int patNum){
			if(patNum==0){
				return new Patient();
			}
			string command= 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,CreditType,Guarantor,HasIns,SSN " 
				+"FROM patient "
				+"WHERE PatNum = '"+patNum.ToString()+"'";
 			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return new Patient();
			}
			Patient Lim=new Patient();
			Lim.PatNum     = PIn.PInt   (table.Rows[0][0].ToString());
			Lim.LName      = PIn.PString(table.Rows[0][1].ToString());
			Lim.FName      = PIn.PString(table.Rows[0][2].ToString());
			Lim.MiddleI    = PIn.PString(table.Rows[0][3].ToString());
			Lim.Preferred  = PIn.PString(table.Rows[0][4].ToString());
			Lim.CreditType = PIn.PString(table.Rows[0][5].ToString());
			Lim.Guarantor  = PIn.PInt   (table.Rows[0][6].ToString());
			Lim.HasIns     = PIn.PString(table.Rows[0][7].ToString());
			Lim.SSN        = PIn.PString(table.Rows[0][8].ToString());
			return Lim;
		}

		///<summary>Gets the patient and provider balances for all patients in the family.  Used from the payment window to help visualize and automate the family splits.</summary>
		public static DataTable GetPaymentStartingBalances(int guarNum,int excludePayNum){
			/*command=@"SELECT (SELECT EstBalance FROM patient WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum) EstBalance, "
				+"IFNULL((SELECT SUM(ProcFee) FROM procedurelog WHERE PatNum="+POut.PInt(patNum)+" AND ProcStatus=2 GROUP BY PatNum),0)"//complete
				+"+IFNULL((SELECT SUM(InsPayAmt) FROM claimproc WHERE PatNum="+POut.PInt(patNum)
				+" AND (Status=1 OR Status=4 OR Status=5) GROUP BY PatNum),0) "//received,supplemental,capclaim"
				+"+IFNULL((SELECT SUM(AdjAmt) FROM adjustment WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) "
				+"-IFNULL((SELECT SUM(SplitAmt) FROM paysplit WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) CalcBalance, "
				+"IFNULL((SELECT SUM(InsPayEst) FROM claimproc WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) Estimate ";
			DataTable table=General.GetTable(command);
				+" GROUP BY PatNum,"
				+" ORDER BY Guarantor!=PatNum,Birthdate,";
			*/
			string command="SET @GuarNum="+POut.PInt(guarNum)+";"
				+"SET @ExcludePayNum="+POut.PInt(excludePayNum)+";";
			command+=@"
				DROP TABLE IF EXISTS tempfambal;
				CREATE TABLE tempfambal( 
					FamBalNum int NOT NULL auto_increment,
					PatNum int NOT NULL,
					ProvNum int NOT NULL,
					AmtBal double NOT NULL,
					PRIMARY KEY (FamBalNum));
				
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,procedurelog.ProvNum,SUM(ProcFee)
				FROM procedurelog,patient
				WHERE patient.PatNum=procedurelog.PatNum
				AND ProcStatus=2
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;
			
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,claimproc.ProvNum,-SUM(InsPayAmt)-SUM(Writeoff)
				FROM claimproc,patient
				WHERE patient.PatNum=claimproc.PatNum
				AND (Status=1 OR Status=4 OR Status=5)/*received,supplemental,capclaim*/
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,adjustment.ProvNum,SUM(AdjAmt)
				FROM adjustment,patient
				WHERE patient.PatNum=adjustment.PatNum
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,paysplit.ProvNum,-SUM(SplitAmt)
				FROM paysplit,patient
				WHERE patient.PatNum=paysplit.PatNum
				AND paysplit.PayNum!=@ExcludePayNum
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				/*payplan amount due*/
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,payplancharge.ProvNum,SUM(Principal+Interest)
				FROM payplancharge,patient
				WHERE patient.PatNum=payplancharge.Guarantor
				AND ChargeDate <= NOW()
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				/*payplan princ reduction*/
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,payplancharge.ProvNum,-SUM(Principal)
				FROM payplancharge,patient
				WHERE patient.PatNum=payplancharge.PatNum
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				SELECT tempfambal.PatNum,tempfambal.ProvNum,SUM(AmtBal) StartBal,FName,Preferred,'0' EndBal
				FROM tempfambal,patient
				WHERE tempfambal.PatNum=patient.PatNum
				GROUP BY PatNum,ProvNum
				ORDER BY Guarantor!=patient.PatNum,Birthdate,ProvNum";
			return General.GetTable(command);
			command="DROP TABLE IF EXISTS tempfambal";
			//General.NonQ(command);
		}

		///<summary></summary>
		public static void ChangeGuarantorToCur(Family Fam,Patient Pat){
			//Move famfinurgnote to current patient:
			string command= "UPDATE patient SET "
				+"FamFinUrgNote = '"+POut.PString(Fam.List[0].FamFinUrgNote)+"' "
				+"WHERE PatNum = "+POut.PInt(Pat.PatNum);
 			General.NonQ(command);
			command= "UPDATE patient SET FamFinUrgNote = '' "
				+"WHERE PatNum = '"+Pat.Guarantor.ToString()+"'";
			General.NonQ(command);
			//Move family financial note to current patient:
			command="SELECT FamFinancial FROM patientnote "
				+"WHERE PatNum = "+POut.PInt(Pat.Guarantor);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==1){
				command= "UPDATE patientnote SET "
					+"FamFinancial = '"+POut.PString(table.Rows[0][0].ToString())+"' "
					+"WHERE PatNum = "+POut.PInt(Pat.PatNum);
				General.NonQ(command);
			}
			command= "UPDATE patientnote SET FamFinancial = '' "
				+"WHERE PatNum = "+POut.PInt(Pat.Guarantor);
			General.NonQ(command);
			//change guarantor of all family members:
			command= "UPDATE patient SET "
				+"Guarantor = "+POut.PInt(Pat.PatNum)
				+" WHERE Guarantor = "+POut.PInt(Pat.Guarantor);
			General.NonQ(command);
		}
		
		///<summary></summary>
		public static void CombineGuarantors(Family Fam,Patient Pat){
			//concat cur notes with guarantor notes
			string command= 
				"UPDATE patient SET "
				//+"addrnote = '"+POut.PString(FamilyList[GuarIndex].FamAddrNote)
				//									+POut.PString(cur.FamAddrNote)+"', "
				+"famfinurgnote = '"+POut.PString(Fam.List[0].FamFinUrgNote)
				+POut.PString(Pat.FamFinUrgNote)+"' "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
 			General.NonQ(command);
			//delete cur notes
			command= 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+Pat.PatNum+"'";
			General.NonQ(command);
			//concat family financial notes
			PatientNote PatientNoteCur=PatientNotes.Refresh(Pat.PatNum,Pat.Guarantor);
			//patientnote table must have been refreshed for this to work.
			//Makes sure there are entries for patient and for guarantor.
			//Also, PatientNotes.cur.FamFinancial will now have the guar info in it.
			string strGuar=PatientNoteCur.FamFinancial;
			command= 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(Pat.PatNum)+"'";
			//MessageBox.Show(string command);
			DataTable table=General.GetTable(command);
			string strCur=PIn.PString(table.Rows[0][0].ToString());
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = '"+POut.PString(strGuar+strCur)+"' "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
			General.NonQ(command);
			//delete cur financial notes
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Pat.PatNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary>Gets names for all patients.  Used mostly to show paysplit info.  Also used for reports, FormTrackNext, and FormUnsched.</summary>
		public static void GetHList(){
			string command="SELECT patnum,lname,fname,middlei,preferred "
				+"FROM patient";
			DataTable table=General.GetTable(command);
			HList=new Hashtable(table.Rows.Count);
			int patnum;
			string lname,fname,middlei,preferred;
			for(int i=0;i<table.Rows.Count;i++){
				patnum=PIn.PInt(table.Rows[i][0].ToString());
				lname=PIn.PString(table.Rows[i][1].ToString());
				fname=PIn.PString(table.Rows[i][2].ToString());
				middlei=PIn.PString(table.Rows[i][3].ToString());
				preferred=PIn.PString(table.Rows[i][4].ToString());
				if(preferred==""){
					HList.Add(patnum,lname+", "+fname+" "+middlei);
				}
				else{
					HList.Add(patnum,lname+", '"+preferred+"' "+fname+" "+middlei);
				}
			}
		}

		///<summary></summary>
		public static void UpdateAddressForFam(Patient pat){
			string command= "UPDATE patient SET " 
				+"Address = '"    +POut.PString(pat.Address)+"'"
				+",Address2 = '"   +POut.PString(pat.Address2)+"'"
				+",City = '"       +POut.PString(pat.City)+"'"
				+",State = '"      +POut.PString(pat.State)+"'"
				+",Zip = '"        +POut.PString(pat.Zip)+"'"
				+",HmPhone = '"    +POut.PString(pat.HmPhone)+"'"
				+",credittype = '" +POut.PString(pat.CreditType)+"'"
				+",priprov = '"    +POut.PInt   (pat.PriProv)+"'"
				+",secprov = '"    +POut.PInt   (pat.SecProv)+"'"
				+",feesched = '"   +POut.PInt   (pat.FeeSched)+"'"
				+",billingtype = '"+POut.PInt   (pat.BillingType)+"'"
				+" WHERE guarantor = '"+POut.PDouble(pat.Guarantor)+"'";
			General.NonQ(command);
		}

		///<summary>Used in new patient terminal.  Synchs less fields than the normal synch.</summary>
		public static void UpdateAddressForFamTerminal(Patient pat) {
			string command= "UPDATE patient SET " 
				+"Address = '"    +POut.PString(pat.Address)+"'"
				+",Address2 = '"   +POut.PString(pat.Address2)+"'"
				+",City = '"       +POut.PString(pat.City)+"'"
				+",State = '"      +POut.PString(pat.State)+"'"
				+",Zip = '"        +POut.PString(pat.Zip)+"'"
				+",HmPhone = '"    +POut.PString(pat.HmPhone)+"'"
				+" WHERE guarantor = '"+POut.PDouble(pat.Guarantor)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void UpdateNotesForFam(Patient pat){
			string command= "UPDATE patient SET " 
				+"addrnote = '"   +POut.PString(pat.AddrNote)+"'"
				+" WHERE guarantor = '"+POut.PDouble(pat.Guarantor)+"'";
			DataTable table=General.GetTable(command);
		}

		///<summary>Only used from FormRecallListEdit.  Updates two fields for family if they are already the same for the entire family.  If they start out different for different family members, then it only changes the two fields for the single patient.</summary>
		public static void UpdatePhoneAndNoteIfNeeded(string newphone, string newnote, int patNum){
			string command="SELECT Guarantor,HmPhone,AddrNote FROM patient WHERE Guarantor="
				+"(SELECT Guarantor FROM patient WHERE PatNum="+POut.PInt(patNum)+")";
			DataTable table=General.GetTable(command);
			bool phoneIsSame=true;
			bool noteIsSame=true;
			int guar=PIn.PInt(table.Rows[0]["Guarantor"].ToString());
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["HmPhone"].ToString()!=table.Rows[0]["HmPhone"].ToString()){
					phoneIsSame=false;
				}
				if(table.Rows[i]["AddrNote"].ToString()!=table.Rows[0]["AddrNote"].ToString()) {
					noteIsSame=false;
				}
			}
			command="UPDATE patient SET HmPhone='"+POut.PString(newphone)+"' WHERE ";
			if(phoneIsSame){
				command+="Guarantor="+POut.PInt(guar);
			}
			else{
				command+="PatNum="+POut.PInt(patNum);
			}
			General.NonQ(command);
			command="UPDATE patient SET AddrNote='"+POut.PString(newnote)+"' WHERE ";
			if(noteIsSame) {
				command+="Guarantor="+POut.PInt(guar);
			}
			else {
				command+="PatNum="+POut.PInt(patNum);
			}
			General.NonQ(command);
		}

		///<summary>This is only used in the Billing dialog</summary>
		public static PatAging[] GetAgingList(string age,DateTime lastStatement,int[] billingIndices,bool excludeAddr
			,bool excludeNeg,double excludeLessThan,bool excludeInactive,bool includeChanged)
		{
			string command="";
			if(includeChanged){
				command+=@"DROP TABLE IF EXISTS templastproc;
					CREATE TABLE templastproc(
					Guarantor mediumint unsigned NOT NULL,
					LastProc date NOT NULL,
					PRIMARY KEY (Guarantor));
					INSERT INTO templastproc
					SELECT patient.Guarantor,MAX(ProcDate)
					FROM procedurelog,patient
					WHERE patient.PatNum=procedurelog.PatNum
					AND procedurelog.ProcStatus=2
					AND procedurelog.ProcFee>0
					GROUP BY patient.Guarantor;
					
					DROP TABLE IF EXISTS templastpay;
					CREATE TABLE templastpay(
					Guarantor mediumint unsigned NOT NULL,
					LastPay date NOT NULL,
					PRIMARY KEY (Guarantor));
					INSERT INTO templastpay
					SELECT patient.Guarantor,MAX(DateCP)
					FROM claimproc,patient
					WHERE claimproc.PatNum=patient.PatNum
					AND claimproc.InsPayAmt>0
					GROUP BY patient.Guarantor;";
			}
			command+="SELECT patient.PatNum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI, "
				+"IFNULL(MAX(commlog.CommDateTime),'0001-01-01') AS LastStatement ";
			if(includeChanged){
				command+=",IFNULL(templastproc.LastProc,'0001-01-01') AS LastChange,"
					+"IFNULL(templastpay.LastPay,'0001-01-01') AS LastPayment ";
			}
			command+=
				"FROM patient "//actually only gets guarantors since others are 0.
				+"LEFT JOIN commlog ON patient.PatNum=commlog.PatNum "
				+"AND IsStatementSent=1 ";
			if(includeChanged){
				command+="LEFT JOIN templastproc ON patient.PatNum=templastproc.Guarantor "
					+"LEFT JOIN templastpay ON patient.PatNum=templastpay.Guarantor ";
			}
			command+="WHERE ";
			if(excludeInactive){
				command+="(patstatus != '2') AND ";
			}
			command+="(BalTotal - InsEst > '"+excludeLessThan.ToString()+"'";
			if(!excludeNeg){
				command+=" OR BalTotal - InsEst < '0')";
			}
			else{
				command+=")";
			}
			switch(age){
				//where is age 0. Is it missing because no restriction
				case "30":
					command+=" AND (Bal_31_60 > '0' OR Bal_61_90 > '0' OR BalOver90 > '0')";
					break;
				case "60":
					command+=" AND (Bal_61_90 > '0' OR BalOver90 > '0')";
					break;
				case "90":
					command+=" AND (BalOver90 > '0')";
					break;
			}
			for(int i=0;i<billingIndices.Length;i++){
				if(i==0){
					command+=" AND (billingtype = '";
				}
				else{
					command+=" OR billingtype = '";
				}
				command+=
					DefB.Short[(int)DefCat.BillingTypes][billingIndices[i]].DefNum.ToString()+"'";
			}
			command+=")";
			if(excludeAddr){
				command+=" AND (zip !='')";
			}	
			command+=" GROUP BY patient.PatNum ";
			if(includeChanged){
				command+="HAVING LastStatement < "+POut.PDate(lastStatement.AddDays(1))+" "//<midnight of lastStatement date
					+"OR LastChange > LastStatement "//eg '2005-10-25' > '2005-10-24 15:00:00'
					+"OR LastPayment > LastStatement ";
			}
			else{
				command+="HAVING LastStatement < "+POut.PDate(lastStatement.AddDays(1))+" ";//<midnight of lastStatement date
			}
			command+="ORDER BY LName,FName";
			//Debug.WriteLine(command);
			DataTable table=General.GetTable(command);
			PatAging[] retVal=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retVal[i]=new PatAging();
				retVal[i].PatNum   = PIn.PInt   (table.Rows[i][0].ToString());
				retVal[i].Bal_0_30 = PIn.PDouble(table.Rows[i][1].ToString());
				retVal[i].Bal_31_60= PIn.PDouble(table.Rows[i][2].ToString());
				retVal[i].Bal_61_90= PIn.PDouble(table.Rows[i][3].ToString());
				retVal[i].BalOver90= PIn.PDouble(table.Rows[i][4].ToString());
				retVal[i].BalTotal = PIn.PDouble(table.Rows[i][5].ToString());
				retVal[i].InsEst   = PIn.PDouble(table.Rows[i][6].ToString());
				retVal[i].PatName=PIn.PString(table.Rows[i][7].ToString())
					+", "+PIn.PString(table.Rows[i][8].ToString())
					+" "+PIn.PString(table.Rows[i][9].ToString());
				retVal[i].AmountDue=retVal[i].BalTotal-retVal[i].InsEst;
				retVal[i].DateLastStatement=PIn.PDate(table.Rows[i][10].ToString());
			}
			if(includeChanged){
				command="DROP TABLE IF EXISTS templastproc";
				General.NonQ(command);
				command="DROP TABLE IF EXISTS templastpay";
				General.NonQ(command);
			}
			return retVal;
		}

		///<summary>Used only to run finance charges, so it ignores negative balances.</summary>
		public static PatAging[] GetAgingList(){
			string command =
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI,priprov "
				+"FROM patient "//actually only gets guarantors since others are 0.
				+" WHERE Bal_0_30 + Bal_31_60 + Bal_61_90 + BalOver90 - InsEst > '0.005'"//more that 1/2 cent
				+" ORDER BY LName,FName";
			DataTable table=General.GetTable(command);
			PatAging[] AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AgingList[i]=new PatAging();
				AgingList[i].PatNum   = PIn.PInt   (table.Rows[i][0].ToString());
				AgingList[i].Bal_0_30 = PIn.PDouble(table.Rows[i][1].ToString());
				AgingList[i].Bal_31_60= PIn.PDouble(table.Rows[i][2].ToString());
				AgingList[i].Bal_61_90= PIn.PDouble(table.Rows[i][3].ToString());
				AgingList[i].BalOver90= PIn.PDouble(table.Rows[i][4].ToString());
				AgingList[i].BalTotal = PIn.PDouble(table.Rows[i][5].ToString());
				AgingList[i].InsEst   = PIn.PDouble(table.Rows[i][6].ToString());
				AgingList[i].PatName=PIn.PString(table.Rows[i][7].ToString())
					+", "+PIn.PString(table.Rows[i][8].ToString())
					+" "+PIn.PString(table.Rows[i][9].ToString());;
				//AgingList[i].Balance=AgingList[i].Bal_0_30+AgingList[i].Bal_31_60
				//	+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				AgingList[i].AmountDue=AgingList[i].BalTotal-AgingList[i].InsEst;
				AgingList[i].PriProv=PIn.PInt(table.Rows[i][10].ToString());
			}
			return AgingList;
		}

		///<summary>For entire database.  Need to zero everything out first because the update aging only inserts non-zero values.</summary>
		public static void ResetAging(){
			string command="UPDATE patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void ResetAging(int guarantor){
			string command="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'"
			  +" WHERE guarantor = '"+POut.PInt(guarantor)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void UpdateAging(int patnum,double Bal0,double Bal31
			,double Bal61,double Bal91,double InsEst,double BalTotal){
			string command="Update patient SET "
				+"Bal_0_30        = '" +POut.PDouble(Bal0)+"'"
				+",Bal_31_60      = '" +POut.PDouble(Bal31)+"'"
				+",Bal_61_90      = '" +POut.PDouble(Bal61)+"'"
				+",BalOver90      = '" +POut.PDouble(Bal91)+"'"
				+",InsEst         = '" +POut.PDouble(InsEst)+"'"
				+",BalTotal       = '" +POut.PDouble(BalTotal)+"'"
				+" WHERE patnum   = '" +POut.PInt   (patnum)+"'";
			//MessageBox.Show(string command);
			General.NonQ(command);
		}

		///<summary>Gets the next available integer chart number.  Will later add a where clause based on preferred format.</summary>
		public static string GetNextChartNum(){
			string command="SELECT ChartNumber from patient WHERE"
				+" ChartNumber REGEXP '^[0-9]+$'"//matches any number of digits
				+" ORDER BY (chartnumber+0) DESC ";//1/13/05 by Keyush Shaw-added 0.
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assume MySQL
				command+="LIMIT 1";
			}
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){//no existing chart numbers
				return "1";
			}
			string lastChartNum=PIn.PString(table.Rows[0][0].ToString());
			//or could add more match conditions
			//if(Regex.IsMatch(lastChartNum,@"^\d+$")){//if is an integer
			return(PIn.PInt(lastChartNum)+1).ToString();
			//}
			//return "1";//if there are no integer chartnumbers yet
		}

		///<summary>Returns the name(only one) of the patient using this chartnumber.</summary>
		public static string ChartNumUsedBy(string chartNum,int excludePatNum){
			string command="SELECT LName,FName from patient WHERE "
				+"ChartNumber = '"+chartNum
				+"' AND PatNum != '"+excludePatNum.ToString()+"'";
			DataTable table=General.GetTable(command);
			string retVal="";
			if(table.Rows.Count!=0){//found duplicate chart number
				retVal=PIn.PString(table.Rows[0][1].ToString())+" "+PIn.PString(table.Rows[0][0].ToString());
			}
			return retVal;
		}

		///<summary>Used in the patient select window to determine if a trial version user is over their limit.</summary>
		public static int GetNumberPatients(){
			string command="SELECT Count(*) FROM patient";
			DataTable table=General.GetTable(command);
			return PIn.PInt(table.Rows[0][0].ToString());
		}

		///<summary>Adds the current patient to the button. Can handle null values for pat and fam. Also resets the family list on the button appropriately. Need to supply the menu to fill as well as the EventHandler to set for each item (all the same).</summary>
		public static void AddPatsToMenu(ContextMenu menu,EventHandler onClick,Patient pat,Family fam){
			//add current patient
			if(buttonLastFivePatNums==null){
				buttonLastFivePatNums=new ArrayList();
			}
			if(buttonLastFiveNames==null) {
				buttonLastFiveNames=new ArrayList();
			}
			if(pat!=null){
				if(buttonLastFivePatNums.Count==0	|| pat.PatNum!=(int)buttonLastFivePatNums[0]){//different patient selected
					buttonLastFivePatNums.Insert(0,pat.PatNum);
					buttonLastFiveNames.Insert(0,pat.GetNameLF());
					if(buttonLastFivePatNums.Count>5){
						buttonLastFivePatNums.RemoveAt(5);
						buttonLastFiveNames.RemoveAt(5);
					}
				}
			}
			//fill menu
			menu.MenuItems.Clear();
			for(int i=0;i<buttonLastFiveNames.Count;i++){
				menu.MenuItems.Add(buttonLastFiveNames[i].ToString(),onClick);
			}
			menu.MenuItems.Add("-");
			menu.MenuItems.Add("FAMILY");
			if(pat!=null){
				for(int i=0;i<fam.List.Length;i++){
					menu.MenuItems.Add(fam.List[i].GetNameLF(),onClick);
				}
			}
		}

		///<summary>A newer alternative which requires fewer calls to the database.  Does not handle null values. Use zero.</summary>
		public static void AddPatsToMenu(ContextMenu menu,EventHandler onClick,string nameLF,int patNum) {
			//add current patient
			if(buttonLastFivePatNums==null) {
				buttonLastFivePatNums=new ArrayList();
			}
			if(buttonLastFiveNames==null) {
				buttonLastFiveNames=new ArrayList();
			}
			if(patNum!=0) {
				if(buttonLastFivePatNums.Count==0	|| patNum!=(int)buttonLastFivePatNums[0]) {//different patient selected
					buttonLastFivePatNums.Insert(0,patNum);
					buttonLastFiveNames.Insert(0,nameLF);
					if(buttonLastFivePatNums.Count>5) {
						buttonLastFivePatNums.RemoveAt(5);
						buttonLastFiveNames.RemoveAt(5);
					}
				}
			}
			//fill menu
			menu.MenuItems.Clear();
			for(int i=0;i<buttonLastFiveNames.Count;i++) {
				menu.MenuItems.Add(buttonLastFiveNames[i].ToString(),onClick);
			}
		}

		///<summary>Determines which menu Item was selected from the Patient dropdown list and returns the patNum for that patient. This will not be activated when click on 'FAMILY' or on separator, because they do not have events attached.  Calling class then does a ModuleSelected.</summary>
		public static int ButtonSelect(ContextMenu menu,object sender,Family fam){
			int index=menu.MenuItems.IndexOf((MenuItem)sender);
			//Patients.PatIsLoaded=true;
			if(index<buttonLastFivePatNums.Count){
				return (int)buttonLastFivePatNums[index];
			}
			if(fam==null){
				return 0;//will never happen
			}
			return fam.List[index-buttonLastFivePatNums.Count-2].PatNum;
		}

		///<summary>Makes a call to the db to figure out if the current HasIns status is correct.  If not, then it changes it.</summary>
		public static void SetHasIns(int patNum){
			string command="SELECT patient.HasIns,COUNT(patplan.PatNum) FROM patient "
				+"LEFT JOIN patplan ON patplan.PatNum=patient.PatNum"
				+" WHERE patient.PatNum="+POut.PInt(patNum)
				+" GROUP BY patplan.PatNum,patient.HasIns";
			DataTable table=General.GetTable(command);
			string newVal="";
			if(table.Rows[0][1].ToString()!="0"){
				newVal="I";
			}
			if(newVal!=table.Rows[0][0].ToString()){
				command="UPDATE patient SET HasIns='"+POut.PString(newVal)
					+"' WHERE PatNum="+POut.PInt(patNum);
				General.NonQ(command);
			}
		}

		///<summary></summary>
		public static DataTable GetBirthdayList(DateTime dateFrom,DateTime dateTo){
			string command="SELECT LName,FName,Preferred,Address,Address2,City,State,Zip,Birthdate "
				+"FROM patient " 
				+"WHERE SUBSTRING(Birthdate,6,5) >= '"+dateFrom.ToString("MM-dd")+"' "
				+"AND SUBSTRING(Birthdate,6,5) <= '"+dateTo.ToString("MM-dd")+"' "
				+"AND Birthdate > '1880-01-01' "
				+"AND PatStatus=0	ORDER BY DATE_FORMAT(Birthdate,'%m/%d/%Y')";
			DataTable table=General.GetTable(command);
			table.Columns.Add("Age");
			for(int i=0;i<table.Rows.Count;i++){
				table.Rows[i]["Age"]=Shared.DateToAge(PIn.PDate(table.Rows[i]["Birthdate"].ToString()),dateTo.AddDays(1)).ToString();
			}
			return table;
		}

		///<summary>It is entirely acceptable to pass in a null value for PatCur.  In that case, no patient name will show.</summary>
		public static string GetMainTitle(Patient PatCur){
			string retVal=PrefB.GetString("MainWindowTitle");
			if(Security.CurUser!=null) {
				retVal+=" {"+Security.CurUser.UserName+"}";
			}
			if(PatCur==null){
				return retVal;
			}
			retVal+=" - "+PatCur.GetNameLF();
			//if(PrefB.GetInt("ShowIDinTitleBar")==0){//no action
			if(PrefB.GetInt("ShowIDinTitleBar")==1){
				retVal+=" - "+PatCur.PatNum.ToString();
			}
			else if(PrefB.GetInt("ShowIDinTitleBar")==2) {
				retVal+=" - "+PatCur.ChartNumber;
			}
			return retVal;
		}

		///<summary>A simpler version which does not require as much data.</summary>
		public static string GetMainTitle(string nameLF,int patNum,string chartNumber) {
			string retVal=PrefB.GetString("MainWindowTitle");
			if(Security.CurUser!=null){
				retVal+=" {"+Security.CurUser.UserName+"}";
			}
			if(patNum==0 || patNum==-1){
				return retVal;
			}
			retVal+=" - "+nameLF;
			if(PrefB.GetInt("ShowIDinTitleBar")==1) {
				retVal+=" - "+patNum.ToString();
			}
			else if(PrefB.GetInt("ShowIDinTitleBar")==2) {
				retVal+=" - "+chartNumber;
			}
			return retVal;
		}

		///<summary>Gets the provider for this patient.  If provNum==0, then it gets the practice default prov.</summary>
		public static int GetProvNum(Patient pat) {
			if(pat.PriProv!=0)
				return pat.PriProv;
			if(PrefB.GetInt("PracticeDefaultProv")==0) {
				MessageBox.Show(Lan.g("Patients","Please set a default provider in the practice setup window."));
				return Providers.List[0].ProvNum;
			}
			return PrefB.GetInt("PracticeDefaultProv");
		}

		///<summary>Gets the list of all valid patient primary keys. Used when checking for missing ADA procedure codes after a user has begun entering them manually. This function is necessary because not all patient numbers are necessarily consecutive (say if the database was created due to a conversion from another program and the customer wanted to keep their old patient ids after the conversion).</summary>
		public static int[] GetAllPatNums(){
			string command="SELECT PatNum From patient";
			DataTable dt=General.GetTableEx(command);
			int[] patnums=new int[dt.Rows.Count];
			for(int i=0;i<patnums.Length;i++){
				patnums[i]=PIn.PInt(dt.Rows[i]["PatNum"].ToString());
			}
			return patnums;
		}
		


	}

	

	///<summary>Not a database table.  Just used for running reports.</summary>
	public class PatAging{
		///<summary></summary>
		public int PatNum;
		///<summary></summary>
		public double Bal_0_30;
		///<summary></summary>
		public double Bal_31_60;
		///<summary></summary>
		public double Bal_61_90;
		///<summary></summary>
		public double BalOver90;
		///<summary></summary>
		public double InsEst;
		///<summary></summary>
		public string PatName;
		///<summary></summary>
		public double BalTotal;
		///<summary></summary>
		public double AmountDue;
		///<summary>The patient priprov to assign the finance charge to.</summary>
		public int PriProv;
		///<summary>The date of the last statement.</summary>
		public DateTime DateLastStatement;
	}

	///<summary></summary>
	public class PatientSelectedEventArgs{
		private int myPatNum;

		///<summary></summary>
		public PatientSelectedEventArgs(int patNum){
			myPatNum=patNum;
		}

		///<summary></summary>
		public int PatNum{
			get{ 
				return myPatNum;
			}
		}

	}

	///<summary></summary>
	public delegate void PatientSelectedEventHandler(object sender,PatientSelectedEventArgs e);

	


}










