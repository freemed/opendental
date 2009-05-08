using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	
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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Family>(MethodBase.GetCurrentMethod(),patNum);
			} 
			string command=GetFamilySelectCommand(patNum);
			Family fam=new Family();
			Collection<Patient> patients = DataObjectFactory<Patient>.CreateObjects(command);
			foreach(Patient patient in patients) {
				patient.Age = DateToAge(patient.Birthdate);
			}
			fam.ListPats=new Patient[patients.Count];
			patients.CopyTo(fam.ListPats,0);
			return fam;
		}

		public static string GetFamilySelectCommand(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),patNum);
			}
			string command= 
				"SELECT guarantor FROM patient "
				+"WHERE patnum = '"+POut.PInt(patNum)+"'";
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return null;
			}
			if(DataConnection.DBtype==DatabaseType.MySql){
				command= 
					"SELECT patient.* "
					+"FROM patient "
					+"WHERE Guarantor = '"+table.Rows[0][0].ToString()+"'"
					+" ORDER BY Guarantor!=PatNum,Birthdate";
			}
			else if(DataConnection.DBtype==DatabaseType.Oracle){
				command= 
					"SELECT patient.*,CASE WHEN PatNum=Guarantor THEN 0 ELSE 1 END AS isguarantor "
					+"FROM patient "
					+"WHERE Guarantor = '"+table.Rows[0][0].ToString()+"'"
					+" ORDER BY 69,Birthdate";//just asking for bugs. Must be one more than the count of fields,
				//which is two more than the last number in the [] of TableToList()
			}
			return command;
		}

		///<summary>This is a way to get a single patient from the database if you don't already have a family object to use.  Will return null if not found.</summary>
		public static Patient GetPat(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),patNum);
			}
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),patNum);
			} 
			if(patNum==0) {
				return null;
			}
			string command="SELECT * FROM patient WHERE PatNum="+POut.PInt(patNum);
			Patient pat=null;
			try {
				pat= DataObjectFactory<Patient>.CreateObject(command);
			}
			catch { }
			if(pat==null) {
				return null;//used in eCW bridge
			}
			pat.Age = DateToAge(pat.Birthdate);
			return pat;
		}

		///<summary>Will return null if not found.</summary>
		public static Patient GetPatByChartNumber(string chartNumber) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),chartNumber);
			}
			if(chartNumber=="") {
				return null;
			}
			string command="SELECT * FROM patient WHERE ChartNumber="+POut.PString(chartNumber);
			Patient pat=null;
			try {
				pat= DataObjectFactory<Patient>.CreateObject(command);
			}
			catch { }
			if(pat==null) {
				return null;
			}
			pat.Age = DateToAge(pat.Birthdate);
			return pat;
		}

		///<summary>Will return null if not found.</summary>
		public static Patient GetPatBySSN(string ssn) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),ssn);
			}
			if(ssn=="") {
				return null;
			}
			string command="SELECT * FROM patient WHERE SSN="+POut.PString(ssn);
			Patient pat=null;
			try {
				pat= DataObjectFactory<Patient>.CreateObject(command);
			}
			catch { }
			if(pat==null) {
				return null;
			}
			pat.Age = DateToAge(pat.Birthdate);
			return pat;
		}

		public static List<Patient> GetUAppoint(DateTime changedSince){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Patient>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM patient WHERE DateTStamp > "+POut.PDateT(changedSince);
				//+" LIMIT 1000";
			DataTable table=Db.GetTable(command);
			return TableToList(table);
			//List<Patient> retVal=new List<Patient>(DataObjectFactory<Patient>.CreateObjects(command));
			//return retVal;
		}

		///<summary>ONLY for new patients. Set includePatNum to true for use the patnum from the import function.  Otherwise, uses InsertID to fill PatNum.</summary>
		public static void Insert(Patient pat, bool includePatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat,includePatNum);
				return;
			}
			if(!includePatNum && PrefC.RandomKeys) {
				pat.PatNum=MiscData.GetKey("patient","PatNum");
			}
			string command= "INSERT INTO patient (";
			if(includePatNum || PrefC.RandomKeys) {
				command+="PatNum,";
			}
			command+="lname,fname,middlei,preferred,patstatus,gender,"
				+"position,birthdate,ssn,address,address2,city,state,zip,hmphone,wkphone,wirelessphone,"
				+"guarantor,credittype,email,salutation,"
				+"estbalance,priprov,secprov,feesched,billingtype,"
				+"imagefolder,addrnote,famfinurgnote,medurgnote,apptmodnote,"
				+"studentstatus,schoolname,chartnumber,medicaidid"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,insest,BalTotal"
				+",EmployerNum,EmploymentNote,Race,County,GradeLevel,Urgency,DateFirstVisit"
				+",ClinicNum,HasIns,TrophyFolder,PlannedIsDone,Premed,Ward,PreferConfirmMethod,PreferContactMethod,PreferRecallMethod"
				+",SchedBeforeTime,SchedAfterTime"
				+",SchedDayOfWeek,Language,AdmitDate,Title,PayPlanDue,SiteNum"//DateTStamp
				+",ResponsParty) VALUES (";
			if(includePatNum || PrefC.RandomKeys) {
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
				+POut.PTimeSpan(pat.SchedBeforeTime)+", "
				+POut.PTimeSpan(pat.SchedAfterTime)+", "
				+"'"+POut.PInt(pat.SchedDayOfWeek)+"', "
				+"'"+POut.PString(pat.Language)+"', "
				+POut.PDate(pat.AdmitDate)+", "
				+"'"+POut.PString(pat.Title)+"', "
				+"'"+POut.PDouble(pat.PayPlanDue)+"', "
				+"'"+POut.PInt(pat.SiteNum)+"', "
				//DateTStamp won't show here.
				+"'"+POut.PInt(pat.ResponsParty)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				pat.PatNum=Db.NonQ(command,true);
			}
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply the old Patient object to compare for changes.</summary>
		public static int Update(Patient pat, Patient CurOld) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),pat,CurOld);
			}
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
				c+="SchedBeforeTime = "     +POut.PTimeSpan(pat.SchedBeforeTime);
				comma=true;
			}
			if(pat.SchedAfterTime!=CurOld.SchedAfterTime) {
				if(comma)
					c+=",";
				c+="SchedAfterTime = "     +POut.PTimeSpan(pat.SchedAfterTime);
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
			if(pat.PayPlanDue!=CurOld.PayPlanDue) {
				if(comma)
					c+=",";
				c+="PayPlanDue = '"     +POut.PDouble(pat.PayPlanDue)+"'";
				comma=true;
			}
			if(pat.SiteNum!=CurOld.SiteNum) {
				if(comma)
					c+=",";
				c+="SiteNum = '"    +POut.PInt(pat.SiteNum)+"'";
				comma=true;
			}
			//DateTStamp
			if(pat.ResponsParty!=CurOld.ResponsParty) {
				if(comma)
					c+=",";
				c+="ResponsParty = '"    +POut.PInt(pat.ResponsParty)+"'";
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE PatNum = '"   +POut.PInt(pat.PatNum)+"'";
			return Db.NonQ(c);
		}//end UpdatePatient

		//This can never be used anymore, or it will mess up 
		///<summary>This is only used when entering a new patient and user clicks cancel.  It used to actually delete the patient, but that will mess up UAppoint synch function.  DateTStamp needs to track deleted patients. So now, the PatStatus is simply changed to 4.</summary>
		public static void Delete(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command="UPDATE patient SET PatStatus="+POut.PInt((int)PatientStatus.Deleted)+", "
				+"Guarantor=PatNum "
				+"WHERE PatNum ="+pat.PatNum.ToString();
			Db.NonQ(command);
		}

 		///<summary>Only used for the Select Patient dialog.  Pass in a billing type of 0 for all billing types.</summary>
		public static DataTable GetPtDataTable(bool limit,string lname,string fname,string phone,
			string address,bool hideInactive,string city,string state,string ssn,string patnum,string chartnumber,
			int billingtype,bool guarOnly,bool showArchived,int clinicNum,DateTime birthdate,int siteNum)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),limit,lname,fname,phone,address,hideInactive,city,state,ssn,patnum,chartnumber,billingtype,guarOnly,showArchived,clinicNum,birthdate,siteNum);
			}
			string billingsnippet=" ";
			if(billingtype!=0){
				billingsnippet+="AND BillingType="+POut.PInt(billingtype)+" ";
			}
			/*for(int i=0;i<billingtypes.Length;i++){//if length==0, it will get all billing types
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
			}*/
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
				+",BillingType,ChartNumber,City,State,PriProv,SiteNum "
				+"FROM patient "
				+"WHERE PatStatus != '4' "//not status 'deleted'
				+(lname.Length>0?"AND LOWER(LName) LIKE '"+POut.PString(lname).ToLower()+"%' ":"") //case matters in a like statement in oracle.
				+(fname.Length>0?"AND LOWER(FName) LIKE '"+POut.PString(fname).ToLower()+"%' ":"");//case matters in a like statement in oracle.
			if(regexp!="") {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command+="AND (HmPhone REGEXP '"+POut.PString(regexp)+"' "
						+"OR WkPhone REGEXP '"+POut.PString(regexp)+"' "
						+"OR WirelessPhone REGEXP '"+POut.PString(regexp)+"') ";
				} else {//oracle
					command+="AND ((SELECT REGEXP_INSTR(p.HmPhone,'"+POut.PString(regexp)+"') FROM dual)<>0"
						+"OR (SELECT REGEXP_INSTR(p.WkPhone,'"+POut.PString(regexp)+"') FROM dual)<>0 "
						+"OR (SELECT REGEXP_INSTR(p.WirelessPhone,'"+POut.PString(regexp)+"') FROM dual)<>0) ";
				}
			}
			command+=
				(address.Length>0?"AND LOWER(Address) LIKE '"+POut.PString(address).ToLower()+"%' ":"")//case matters in a like statement in oracle.
				+(city.Length>0?"AND LOWER(City) LIKE '"+POut.PString(city).ToLower()+"%' ":"")//case matters in a like statement in oracle.
				+(state.Length>0?"AND LOWER(State) LIKE '"+POut.PString(state).ToLower()+"%' ":"")//case matters in a like statement in oracle.
				+(ssn.Length>0?"AND LOWER(SSN) LIKE '"+POut.PString(ssn).ToLower()+"%' ":"")//In case an office uses this field for something else.
				+(patnum.Length>0?"AND PatNum LIKE '"+POut.PString(patnum)+"%' ":"")//case matters in a like statement in oracle.
				+(chartnumber.Length>0?"AND LOWER(ChartNumber) LIKE '"+POut.PString(chartnumber).ToLower()+"%' ":"");//case matters in a like statement in oracle.
			if(birthdate.Year>1880 && birthdate.Year<2100){
				command+="AND Birthdate ="+POut.PDate(birthdate)+" ";
			}
			command+=billingsnippet;
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
			if(siteNum>0) {
				command+="AND SiteNum="+POut.PInt(siteNum)+" ";
			}
			command+="ORDER BY LName,FName ";
			if(limit){
				if(DataConnection.DBtype==DatabaseType.Oracle){
					command="SELECT * FROM ("+command+") WHERE ROWNUM<=";
				}else{//Assume MySQL
					command+="LIMIT ";
				}
				command+="40";
			}
			//MessageBox.Show(command);
 			DataTable table=Db.GetTable(command);
			DataTable PtDataTable=table.Clone();//does not copy any data
			PtDataTable.TableName="table";
			PtDataTable.Columns.Add("age");
			PtDataTable.Columns.Add("site");
			for(int i=0;i<PtDataTable.Columns.Count;i++){
				PtDataTable.Columns[i].DataType=typeof(string);
			}
			//if(limit && table.Rows.Count==36){
			//	retval=true;
			//}
			DataRow r;
			DateTime date;
			for(int i=0;i<table.Rows.Count;i++){//table.Rows.Count && i<44;i++){
				r=PtDataTable.NewRow();
				//PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,WkPhone,Address,PatStatus"
				//+",BillingType,ChartNumber,City,State
				r["PatNum"]=table.Rows[i]["PatNum"].ToString();
				r["LName"]=table.Rows[i]["LName"].ToString();
				r["FName"]=table.Rows[i]["FName"].ToString();
				r["MiddleI"]=table.Rows[i]["MiddleI"].ToString();
				r["Preferred"]=table.Rows[i]["Preferred"].ToString();
				date=PIn.PDate(table.Rows[i]["Birthdate"].ToString());
				if(date.Year>1880){
					r["age"]=DateToAge(date);
					r["Birthdate"]=date.ToShortDateString();
				}
				else{
					r["age"]="";
					r["Birthdate"]="";
				}				
				r["SSN"]=table.Rows[i]["SSN"].ToString();
				r["HmPhone"]=table.Rows[i]["HmPhone"].ToString();
				r["WkPhone"]=table.Rows[i]["WkPhone"].ToString();
				r["Address"]=table.Rows[i]["Address"].ToString();
				r["PatStatus"]=((PatientStatus)PIn.PInt(table.Rows[i]["PatStatus"].ToString())).ToString();
				r["BillingType"]=DefC.GetName(DefCat.BillingTypes,PIn.PInt(table.Rows[i]["BillingType"].ToString()));
				r["ChartNumber"]=table.Rows[i]["ChartNumber"].ToString();
				r["City"]=table.Rows[i]["City"].ToString();
				r["State"]=table.Rows[i]["State"].ToString();
				r["PriProv"]=Providers.GetAbbr(PIn.PInt(table.Rows[i]["PriProv"].ToString()));
				r["site"]=Sites.GetDescription(PIn.PInt(table.Rows[i]["SiteNum"].ToString()));
				PtDataTable.Rows.Add(r);
			}
			return PtDataTable;
		}

		///<summary>Used when filling appointments for an entire day. Gets a list of Pats, multPats, of all the specified patients.  Then, use GetOnePat to pull one patient from this list.  This process requires only one call to the database.</summary>
		public static Patient[] GetMultPats(List <int> patNums){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient[]>(MethodBase.GetCurrentMethod(),patNums);
			}
			//MessageBox.Show(patNums.Length.ToString());
			string strPatNums="";
			DataTable table;
			if(patNums.Count>0){
				for(int i=0;i<patNums.Count;i++){
					if(i>0){
						strPatNums+="OR ";
					}
					strPatNums+="PatNum='"+patNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM patient WHERE "+strPatNums;
				//MessageBox.Show(string command);
 				table=Db.GetTable(command);
			}
			else{
				table=new DataTable();
			}
			Patient[] multPats=TableToList(table).ToArray();
			return multPats;
		}

		public static List<Patient> TableToList(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<Patient> patList=new List<Patient>();
			Patient pat;
			for(int i=0;i<table.Rows.Count;i++){
				pat=new Patient();
				pat.PatNum       = PIn.PInt   (table.Rows[i][0].ToString());
				pat.LName        = PIn.PString(table.Rows[i][1].ToString());
				pat.FName        = PIn.PString(table.Rows[i][2].ToString());
				pat.MiddleI      = PIn.PString(table.Rows[i][3].ToString());
				pat.Preferred    = PIn.PString(table.Rows[i][4].ToString());
				pat.PatStatus    = (PatientStatus)PIn.PInt   (table.Rows[i][5].ToString());
				pat.Gender       = (PatientGender)PIn.PInt   (table.Rows[i][6].ToString());
				pat.Position     = (PatientPosition)PIn.PInt   (table.Rows[i][7].ToString());
				pat.Birthdate    = PIn.PDate  (table.Rows[i][8].ToString());
				pat.Age=DateToAge(pat.Birthdate);
				pat.SSN          = PIn.PString(table.Rows[i][9].ToString());
				pat.Address      = PIn.PString(table.Rows[i][10].ToString());
				pat.Address2     = PIn.PString(table.Rows[i][11].ToString());
				pat.City         = PIn.PString(table.Rows[i][12].ToString());
				pat.State        = PIn.PString(table.Rows[i][13].ToString());
				pat.Zip          = PIn.PString(table.Rows[i][14].ToString());
				pat.HmPhone      = PIn.PString(table.Rows[i][15].ToString());
				pat.WkPhone      = PIn.PString(table.Rows[i][16].ToString());
				pat.WirelessPhone= PIn.PString(table.Rows[i][17].ToString());
				pat.Guarantor    = PIn.PInt   (table.Rows[i][18].ToString());
				pat.CreditType   = PIn.PString(table.Rows[i][19].ToString());
				pat.Email        = PIn.PString(table.Rows[i][20].ToString());
				pat.Salutation   = PIn.PString(table.Rows[i][21].ToString());
				pat.EstBalance   = PIn.PDouble(table.Rows[i][22].ToString());
				pat.PriProv      = PIn.PInt   (table.Rows[i][23].ToString());
				pat.SecProv      = PIn.PInt   (table.Rows[i][24].ToString());
				pat.FeeSched     = PIn.PInt   (table.Rows[i][25].ToString());
				pat.BillingType  = PIn.PInt   (table.Rows[i][26].ToString());
				pat.ImageFolder  = PIn.PString(table.Rows[i][27].ToString());
				pat.AddrNote     = PIn.PString(table.Rows[i][28].ToString());
				pat.FamFinUrgNote= PIn.PString(table.Rows[i][29].ToString());
				pat.MedUrgNote   = PIn.PString(table.Rows[i][30].ToString());
				pat.ApptModNote  = PIn.PString(table.Rows[i][31].ToString());
				pat.StudentStatus= PIn.PString(table.Rows[i][32].ToString());
				pat.SchoolName   = PIn.PString(table.Rows[i][33].ToString());
				pat.ChartNumber  = PIn.PString(table.Rows[i][34].ToString());
				pat.MedicaidID   = PIn.PString(table.Rows[i][35].ToString());
				pat.Bal_0_30     = PIn.PDouble(table.Rows[i][36].ToString());
				pat.Bal_31_60    = PIn.PDouble(table.Rows[i][37].ToString());
				pat.Bal_61_90    = PIn.PDouble(table.Rows[i][38].ToString());
				pat.BalOver90    = PIn.PDouble(table.Rows[i][39].ToString());
				pat.InsEst       = PIn.PDouble(table.Rows[i][40].ToString());
				pat.BalTotal     = PIn.PDouble(table.Rows[i][41].ToString());
				pat.EmployerNum  = PIn.PInt   (table.Rows[i][42].ToString());
				pat.EmploymentNote=PIn.PString(table.Rows[i][43].ToString());
				pat.Race         = (PatientRace)PIn.PInt(table.Rows[i][44].ToString());
				pat.County       = PIn.PString(table.Rows[i][45].ToString());
				pat.GradeLevel   = (PatientGrade)PIn.PInt(table.Rows[i][46].ToString());
				pat.Urgency      = (TreatmentUrgency)PIn.PInt(table.Rows[i][47].ToString());
				pat.DateFirstVisit=PIn.PDate  (table.Rows[i][48].ToString());
				pat.ClinicNum    = PIn.PInt   (table.Rows[i][49].ToString());
				pat.HasIns       = PIn.PString(table.Rows[i][50].ToString());
				pat.TrophyFolder = PIn.PString(table.Rows[i][51].ToString());
				pat.PlannedIsDone= PIn.PBool  (table.Rows[i][52].ToString());
				pat.Premed       = PIn.PBool  (table.Rows[i][53].ToString());
				pat.Ward         = PIn.PString(table.Rows[i][54].ToString());
				pat.PreferConfirmMethod=(ContactMethod)PIn.PInt(table.Rows[i][55].ToString());
				pat.PreferContactMethod=(ContactMethod)PIn.PInt(table.Rows[i][56].ToString());
				pat.PreferRecallMethod=(ContactMethod)PIn.PInt(table.Rows[i][57].ToString());
				pat.SchedBeforeTime= PIn.PTimeSpan(table.Rows[i][58].ToString());
				pat.SchedAfterTime= PIn.PTimeSpan(table.Rows[i][59].ToString());
				pat.SchedDayOfWeek=PIn.PByte  (table.Rows[i][60].ToString());
				pat.Language     = PIn.PString(table.Rows[i][61].ToString());
				pat.AdmitDate    = PIn.PDate  (table.Rows[i][62].ToString());
				pat.Title        = PIn.PString(table.Rows[i][63].ToString());
				pat.PayPlanDue   = PIn.PDouble(table.Rows[i][64].ToString());
				pat.SiteNum      = PIn.PInt   (table.Rows[i][65].ToString());
				//DateTStamp 66
				pat.ResponsParty = PIn.PInt   (table.Rows[i][67].ToString());
				patList.Add(pat);
			}
			return patList;
		}

		///<summary>First call GetMultPats to fill the list of multPats. Then, use this to return one patient from that list.</summary>
		public static Patient GetOnePat(Patient[] multPats, int patNum){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<multPats.Length;i++){
				if(multPats[i].PatNum==patNum){
					return multPats[i];
				}
			}
			return new Patient();
		}

		/// <summary>Gets nine of the most useful fields from the db for the given patnum.</summary>
		public static Patient GetLim(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),patNum);
			}
			if(patNum==0){
				return new Patient();
			}
			string command= 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,CreditType,Guarantor,HasIns,SSN " 
				+"FROM patient "
				+"WHERE PatNum = '"+patNum.ToString()+"'";
 			DataTable table=Db.GetTable(command);
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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),guarNum,excludePayNum);
			}
			/*command=@"SELECT (SELECT EstBalance FROM patient WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum) EstBalance, "
				+"IFNULL((SELECT SUM(ProcFee) FROM procedurelog WHERE PatNum="+POut.PInt(patNum)+" AND ProcStatus=2 GROUP BY PatNum),0)"//complete
				+"+IFNULL((SELECT SUM(InsPayAmt) FROM claimproc WHERE PatNum="+POut.PInt(patNum)
				+" AND (Status=1 OR Status=4 OR Status=5) GROUP BY PatNum),0) "//received,supplemental,capclaim"
				+"+IFNULL((SELECT SUM(AdjAmt) FROM adjustment WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) "
				+"-IFNULL((SELECT SUM(SplitAmt) FROM paysplit WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) CalcBalance, "
				+"IFNULL((SELECT SUM(InsPayEst) FROM claimproc WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) Estimate ";
			DataTable table=Db.GetTable(command);
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
					InsEst double NOT NULL,
					PRIMARY KEY (FamBalNum));
				
				/*Completed procedures*/
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,procedurelog.ProvNum,SUM(ProcFee)
				FROM procedurelog,patient
				WHERE patient.PatNum=procedurelog.PatNum
				AND ProcStatus=2
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;
			
				/*Received insurance payments*/
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,claimproc.ProvNum,-SUM(InsPayAmt)-SUM(Writeoff)
				FROM claimproc,patient
				WHERE patient.PatNum=claimproc.PatNum
				AND (Status=1 OR Status=4 OR Status=5)/*received,supplemental,capclaim. (7-capcomplete writeoff)*/
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				/*Insurance estimates*/
				INSERT INTO tempfambal (PatNum,ProvNum,InsEst)
				SELECT patient.PatNum,claimproc.ProvNum,SUM(InsPayEst)+SUM(Writeoff)
				FROM claimproc,patient
				WHERE patient.PatNum=claimproc.PatNum
				AND Status=0 /*NotReceived*/
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				/*Adjustments*/
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,adjustment.ProvNum,SUM(AdjAmt)
				FROM adjustment,patient
				WHERE patient.PatNum=adjustment.PatNum
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				/*Patient payments*/
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,paysplit.ProvNum,-SUM(SplitAmt)
				FROM paysplit,patient
				WHERE patient.PatNum=paysplit.PatNum
				AND paysplit.PayNum!=@ExcludePayNum
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				/*payplan princ reduction*/
				INSERT INTO tempfambal (PatNum,ProvNum,AmtBal)
				SELECT patient.PatNum,payplancharge.ProvNum,-SUM(Principal)
				FROM payplancharge,patient
				WHERE patient.PatNum=payplancharge.PatNum
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum;

				SELECT tempfambal.PatNum,tempfambal.ProvNum,SUM(AmtBal) StartBal,SUM(AmtBal-tempfambal.InsEst) AfterIns,FName,Preferred,'0' EndBal
				FROM tempfambal,patient
				WHERE tempfambal.PatNum=patient.PatNum
				GROUP BY PatNum,ProvNum
				ORDER BY Guarantor!=patient.PatNum,Birthdate,ProvNum;

				DROP TABLE IF EXISTS tempfambal";
			return Db.GetTable(command);
		}

		///<summary></summary>
		public static void ChangeGuarantorToCur(Family Fam,Patient Pat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Fam,Pat);
				return;
			}
			//Move famfinurgnote to current patient:
			string command= "UPDATE patient SET "
				+"FamFinUrgNote = '"+POut.PString(Fam.ListPats[0].FamFinUrgNote)+"' "
				+"WHERE PatNum = "+POut.PInt(Pat.PatNum);
 			Db.NonQ(command);
			command= "UPDATE patient SET FamFinUrgNote = '' "
				+"WHERE PatNum = '"+Pat.Guarantor.ToString()+"'";
			Db.NonQ(command);
			//Move family financial note to current patient:
			command="SELECT FamFinancial FROM patientnote "
				+"WHERE PatNum = "+POut.PInt(Pat.Guarantor);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==1){
				command= "UPDATE patientnote SET "
					+"FamFinancial = '"+POut.PString(table.Rows[0][0].ToString())+"' "
					+"WHERE PatNum = "+POut.PInt(Pat.PatNum);
				Db.NonQ(command);
			}
			command= "UPDATE patientnote SET FamFinancial = '' "
				+"WHERE PatNum = "+POut.PInt(Pat.Guarantor);
			Db.NonQ(command);
			//change guarantor of all family members:
			command= "UPDATE patient SET "
				+"Guarantor = "+POut.PInt(Pat.PatNum)
				+" WHERE Guarantor = "+POut.PInt(Pat.Guarantor);
			Db.NonQ(command);
		}
		
		///<summary></summary>
		public static void CombineGuarantors(Family Fam,Patient Pat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Fam,Pat);
				return;
			}
			//concat cur notes with guarantor notes
			string command= 
				"UPDATE patient SET "
				//+"addrnote = '"+POut.PString(FamilyList[GuarIndex].FamAddrNote)
				//									+POut.PString(cur.FamAddrNote)+"', "
				+"famfinurgnote = '"+POut.PString(Fam.ListPats[0].FamFinUrgNote)
				+POut.PString(Pat.FamFinUrgNote)+"' "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
 			Db.NonQ(command);
			//delete cur notes
			command= 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+Pat.PatNum+"'";
			Db.NonQ(command);
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
			DataTable table=Db.GetTable(command);
			string strCur=PIn.PString(table.Rows[0][0].ToString());
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = '"+POut.PString(strGuar+strCur)+"' "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
			Db.NonQ(command);
			//delete cur financial notes
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Pat.PatNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>Gets names for all patients.  Used mostly to show paysplit info.  Also used for reports, FormTrackNext, and FormUnsched.</summary>
		public static void GetHList(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="SELECT patnum,lname,fname,middlei,preferred "
				+"FROM patient";
			DataTable table=Db.GetTable(command);
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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
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
			Db.NonQ(command);
		}

		///<summary>Used in new patient terminal.  Synchs less fields than the normal synch.</summary>
		public static void UpdateAddressForFamTerminal(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command= "UPDATE patient SET " 
				+"Address = '"    +POut.PString(pat.Address)+"'"
				+",Address2 = '"   +POut.PString(pat.Address2)+"'"
				+",City = '"       +POut.PString(pat.City)+"'"
				+",State = '"      +POut.PString(pat.State)+"'"
				+",Zip = '"        +POut.PString(pat.Zip)+"'"
				+",HmPhone = '"    +POut.PString(pat.HmPhone)+"'"
				+" WHERE guarantor = '"+POut.PDouble(pat.Guarantor)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void UpdateNotesForFam(Patient pat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command= "UPDATE patient SET " 
				+"addrnote = '"   +POut.PString(pat.AddrNote)+"'"
				+" WHERE guarantor = '"+POut.PDouble(pat.Guarantor)+"'";
			DataTable table=Db.GetTable(command);
		}

		///<summary>Only used from FormRecallListEdit.  Updates two fields for family if they are already the same for the entire family.  If they start out different for different family members, then it only changes the two fields for the single patient.</summary>
		public static void UpdatePhoneAndNoteIfNeeded(string newphone, string newnote, int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),newphone,newnote,patNum);
				return;
			}
			string command="SELECT Guarantor,HmPhone,AddrNote FROM patient WHERE Guarantor="
				+"(SELECT Guarantor FROM patient WHERE PatNum="+POut.PInt(patNum)+")";
			DataTable table=Db.GetTable(command);
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
			Db.NonQ(command);
			command="UPDATE patient SET AddrNote='"+POut.PString(newnote)+"' WHERE ";
			if(noteIsSame) {
				command+="Guarantor="+POut.PInt(guar);
			}
			else {
				command+="PatNum="+POut.PInt(patNum);
			}
			Db.NonQ(command);
		}

		///<summary>This is only used in the Billing dialog</summary>
		public static List<PatAging> GetAgingList(string age,DateTime lastStatement,List<int> billingNums,bool excludeAddr
			,bool excludeNeg,double excludeLessThan,bool excludeInactive,bool includeChanged,bool excludeInsPending,bool ignoreInPerson)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PatAging>>(MethodBase.GetCurrentMethod(),age,lastStatement,billingNums,excludeAddr,excludeNeg,excludeLessThan,excludeInactive,includeChanged,excludeInsPending,ignoreInPerson);
			}
			string command="";
			if(includeChanged){
				command+=@"DROP TABLE IF EXISTS templastproc;
					CREATE TABLE templastproc(
					Guarantor int unsigned NOT NULL,
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
					Guarantor int unsigned NOT NULL,
					LastPay date NOT NULL,
					PRIMARY KEY (Guarantor));
					INSERT INTO templastpay
					SELECT patient.Guarantor,MAX(DateCP)
					FROM claimproc,patient
					WHERE claimproc.PatNum=patient.PatNum
					AND claimproc.InsPayAmt>0
					GROUP BY patient.Guarantor;";					
			}
			if(excludeInsPending) {
				command+=@"DROP TABLE IF EXISTS tempclaimspending;
					CREATE TABLE tempclaimspending(
					Guarantor int unsigned NOT NULL,
					PendingClaimCount int NOT NULL,
					PRIMARY KEY (Guarantor));
					INSERT INTO tempclaimspending
					SELECT patient.Guarantor,COUNT(*)
					FROM claim,patient
					WHERE claim.PatNum=patient.PatNum
					AND (ClaimStatus='U' OR ClaimStatus='H' OR ClaimStatus='W' OR ClaimStatus='S')
					AND (ClaimType='P' OR ClaimType='S' OR ClaimType='Other')
					GROUP BY patient.Guarantor;";
			}
			command+="SELECT patient.PatNum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,BillingType,"
				+"InsEst,LName,FName,MiddleI,PayPlanDue,Preferred, "
				+"IFNULL(MAX(statement.DateSent),'0001-01-01') AS LastStatement ";
			if(includeChanged){
				command+=",IFNULL(templastproc.LastProc,'0001-01-01') AS LastChange,"
					+"IFNULL(templastpay.LastPay,'0001-01-01') AS LastPayment ";
			}
			if(excludeInsPending){
				command+=",IFNULL(tempclaimspending.PendingClaimCount,'0') AS ClaimCount ";
			}
			command+=
				"FROM patient "//actually only gets guarantors since others are 0.
				+"LEFT JOIN statement ON patient.PatNum=statement.PatNum ";
			if(ignoreInPerson) {
				command+="AND statement.Mode_ != 1 ";
			}
			if(includeChanged){
				command+="LEFT JOIN templastproc ON patient.PatNum=templastproc.Guarantor "
					+"LEFT JOIN templastpay ON patient.PatNum=templastpay.Guarantor ";
			}
			if(excludeInsPending){
				command+="LEFT JOIN tempclaimspending ON patient.PatNum=tempclaimspending.Guarantor ";
			}
			command+="WHERE ";
			if(excludeInactive){
				command+="(patstatus != '2') AND ";
			}
			command+="(BalTotal - InsEst > '"+excludeLessThan.ToString()+"'"
				+" OR PayPlanDue > 0";
			if(!excludeNeg){
				command+=" OR BalTotal - InsEst < '0')";
			}
			else{
				command+=")";
			}
			switch(age){
				//where is age 0. Is it missing because no restriction
				case "30":
					command+=" AND (Bal_31_60 > '0' OR Bal_61_90 > '0' OR BalOver90 > '0' OR PayPlanDue > 0)";
					break;
				case "60":
					command+=" AND (Bal_61_90 > '0' OR BalOver90 > '0' OR PayPlanDue > 0)";
					break;
				case "90":
					command+=" AND (BalOver90 > '0' OR PayPlanDue > 0)";
					break;
			}
			//if billingNums.Count==0, then we'll include all billing types
			for(int i=0;i<billingNums.Count;i++){
				if(i==0){
					command+=" AND (billingtype = '";
				}
				else{
					command+=" OR billingtype = '";
				}
				command+=POut.PInt(billingNums[i])+"'";
					//DefC.Short[(int)DefCat.BillingTypes][billingIndices[i]].DefNum.ToString()+"'";
				if(i==billingNums.Count-1){
					command+=")";
				}
			}
			if(excludeAddr){
				command+=" AND (zip !='')";
			}	
			command+=" GROUP BY patient.PatNum "
				+"HAVING (LastStatement < "+POut.PDate(lastStatement.AddDays(1))+" ";//<midnight of lastStatement date
				//+"OR PayPlanDue>0 ";we don't have a great way to trigger due to a payplancharge yet
			if(includeChanged){
				command+=
					 "OR LastChange > LastStatement "//eg '2005-10-25' > '2005-10-24 15:00:00'
					+"OR LastPayment > LastStatement) ";
			}
			else{
				command+=") ";
			}
			if(excludeInsPending){
				command+="AND ClaimCount=0 ";
			}
			command+="ORDER BY LName,FName";
			//Debug.WriteLine(command);
			DataTable table=Db.GetTable(command);
			List<PatAging> agingList=new List<PatAging>();
			PatAging patage;
			Patient pat;
			for(int i=0;i<table.Rows.Count;i++){
				patage=new PatAging();
				patage.PatNum   = PIn.PInt   (table.Rows[i]["PatNum"].ToString());
				patage.Bal_0_30 = PIn.PDouble(table.Rows[i]["Bal_0_30"].ToString());
				patage.Bal_31_60= PIn.PDouble(table.Rows[i]["Bal_31_60"].ToString());
				patage.Bal_61_90= PIn.PDouble(table.Rows[i]["Bal_61_90"].ToString());
				patage.BalOver90= PIn.PDouble(table.Rows[i]["BalOver90"].ToString());
				patage.BalTotal = PIn.PDouble(table.Rows[i]["BalTotal"].ToString());
				patage.InsEst   = PIn.PDouble(table.Rows[i]["InsEst"].ToString());
				pat=new Patient();
				pat.LName=PIn.PString(table.Rows[i]["LName"].ToString());
				pat.FName=PIn.PString(table.Rows[i]["FName"].ToString());
				pat.MiddleI=PIn.PString(table.Rows[i]["MiddleI"].ToString());
				pat.Preferred=PIn.PString(table.Rows[i]["Preferred"].ToString());
				patage.PatName=pat.GetNameLF();
				patage.AmountDue=patage.BalTotal-patage.InsEst;
				patage.DateLastStatement=PIn.PDate(table.Rows[i]["LastStatement"].ToString());
				patage.BillingType=PIn.PInt(table.Rows[i]["BillingType"].ToString());
				patage.PayPlanDue =PIn.PDouble(table.Rows[i]["PayPlanDue"].ToString());
				//if(excludeInsPending && patage.InsEst>0){
					//don't add
				//}
				//else{
				agingList.Add(patage);
				//}
			}
			//PatAging[] retVal=new PatAging[agingList.Count];
			//for(int i=0;i<retVal.Length;i++){
			//	retVal[i]=agingList[i];
			//}
			if(includeChanged){
				command="DROP TABLE IF EXISTS templastproc";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS templastpay";
				Db.NonQ(command);
			}
			if(excludeInsPending){
				command="DROP TABLE IF EXISTS tempclaimspending";
				Db.NonQ(command);
			}
			return agingList;
		}

		///<summary>Used only to run finance charges, so it ignores negative balances.</summary>
		public static PatAging[] GetAgingList(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PatAging[]>(MethodBase.GetCurrentMethod());
			}
			string command =
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI,PriProv,BillingType "
				+"FROM patient "//actually only gets guarantors since others are 0.
				+" WHERE Bal_0_30 + Bal_31_60 + Bal_61_90 + BalOver90 - InsEst > '0.005'"//more that 1/2 cent
				+" ORDER BY LName,FName";
			DataTable table=Db.GetTable(command);
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
				AgingList[i].BillingType=PIn.PInt(table.Rows[i][11].ToString());
			}
			return AgingList;
		}

		///<summary>Gets the next available integer chart number.  Will later add a where clause based on preferred format.</summary>
		public static string GetNextChartNum(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string command="SELECT ChartNumber from patient WHERE"
				+" ChartNumber REGEXP '^[0-9]+$'"//matches any number of digits
				+" ORDER BY (chartnumber+0) DESC ";//1/13/05 by Keyush Shaw-added 0.
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assume MySQL
				command+="LIMIT 1";
			}
			DataTable table=Db.GetTable(command);
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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),chartNum,excludePatNum);
			}
			string command="SELECT LName,FName from patient WHERE "
				+"ChartNumber = '"+chartNum
				+"' AND PatNum != '"+excludePatNum.ToString()+"'";
			DataTable table=Db.GetTable(command);
			string retVal="";
			if(table.Rows.Count!=0){//found duplicate chart number
				retVal=PIn.PString(table.Rows[0][1].ToString())+" "+PIn.PString(table.Rows[0][0].ToString());
			}
			return retVal;
		}

		///<summary>Used in the patient select window to determine if a trial version user is over their limit.</summary>
		public static int GetNumberPatients(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command="SELECT Count(*) FROM patient";
			DataTable table=Db.GetTable(command);
			return PIn.PInt(table.Rows[0][0].ToString());
		}

		///<summary>The current patient will already be on the button.  This adds the family members when user clicks dropdown arrow. Can handle null values for pat and fam.  Need to supply the menu to fill as well as the EventHandler to set for each item (all the same).</summary>
		public static void AddFamilyToMenu(ContextMenu menu,EventHandler onClick,int patNum,Family fam){
			//No need to check RemotingRole; no call to db.
			//fill menu
			menu.MenuItems.Clear();
			for(int i=0;i<buttonLastFiveNames.Count;i++){
				menu.MenuItems.Add(buttonLastFiveNames[i].ToString(),onClick);
			}
			menu.MenuItems.Add("-");
			menu.MenuItems.Add("FAMILY");
			if(patNum!=0 && fam!=null){
				for(int i=0;i<fam.ListPats.Length;i++){
					menu.MenuItems.Add(fam.ListPats[i].GetNameLF(),onClick);
				}
			}
		}

		///<summary>Does not handle null values. Use zero.  Does not handle adding family members.</summary>
		public static void AddPatsToMenu(ContextMenu menu,EventHandler onClick,string nameLF,int patNum) {
			//No need to check RemotingRole; no call to db.
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
			//menu.MenuItems.Clear();
			//for(int i=0;i<buttonLastFiveNames.Count;i++) {
			//	menu.MenuItems.Add(buttonLastFiveNames[i].ToString(),onClick);
			//}
		}

		///<summary>Determines which menu Item was selected from the Patient dropdown list and returns the patNum for that patient. This will not be activated when click on 'FAMILY' or on separator, because they do not have events attached.  Calling class then does a ModuleSelected.</summary>
		public static int ButtonSelect(ContextMenu menu,object sender,Family fam){
			//No need to check RemotingRole; no call to db.
			int index=menu.MenuItems.IndexOf((MenuItem)sender);
			//Patients.PatIsLoaded=true;
			if(index<buttonLastFivePatNums.Count){
				return (int)buttonLastFivePatNums[index];
			}
			if(fam==null){
				return 0;//will never happen
			}
			return fam.ListPats[index-buttonLastFivePatNums.Count-2].PatNum;
		}

		///<summary>Makes a call to the db to figure out if the current HasIns status is correct.  If not, then it changes it.</summary>
		public static void SetHasIns(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="SELECT patient.HasIns,COUNT(patplan.PatNum) FROM patient "
				+"LEFT JOIN patplan ON patplan.PatNum=patient.PatNum"
				+" WHERE patient.PatNum="+POut.PInt(patNum)
				+" GROUP BY patplan.PatNum,patient.HasIns";
			DataTable table=Db.GetTable(command);
			string newVal="";
			if(table.Rows[0][1].ToString()!="0"){
				newVal="I";
			}
			if(newVal!=table.Rows[0][0].ToString()){
				command="UPDATE patient SET HasIns='"+POut.PString(newVal)
					+"' WHERE PatNum="+POut.PInt(patNum);
				Db.NonQ(command);
			}
		}

		///<summary></summary>
		public static DataTable GetBirthdayList(DateTime dateFrom,DateTime dateTo){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateFrom,dateTo);
			}
			string command="SELECT LName,FName,Preferred,Address,Address2,City,State,Zip,Birthdate "
				+"FROM patient " 
				+"WHERE SUBSTRING(Birthdate,6,5) >= '"+dateFrom.ToString("MM-dd")+"' "
				+"AND SUBSTRING(Birthdate,6,5) <= '"+dateTo.ToString("MM-dd")+"' "
				+"AND Birthdate > '1880-01-01' "
				+"AND PatStatus=0	ORDER BY DATE_FORMAT(Birthdate,'%m/%d/%Y')";
			DataTable table=Db.GetTable(command);
			table.Columns.Add("Age");
			for(int i=0;i<table.Rows.Count;i++){
				table.Rows[i]["Age"]=DateToAge(PIn.PDate(table.Rows[i]["Birthdate"].ToString()),dateTo.AddDays(1)).ToString();
			}
			return table;
		}

		///<summary>Gets the provider for this patient.  If provNum==0, then it gets the practice default prov.</summary>
		public static int GetProvNum(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),pat);
			}
			if(pat.PriProv!=0)
				return pat.PriProv;
			if(PrefC.GetInt("PracticeDefaultProv")==0) {
				MessageBox.Show(Lans.g("Patients","Please set a default provider in the practice setup window."));
				return ProviderC.List[0].ProvNum;
			}
			return PrefC.GetInt("PracticeDefaultProv");
		}

		///<summary>Gets the list of all valid patient primary keys. Used when checking for missing ADA procedure codes after a user has begun entering them manually. This function is necessary because not all patient numbers are necessarily consecutive (say if the database was created due to a conversion from another program and the customer wanted to keep their old patient ids after the conversion).</summary>
		public static int[] GetAllPatNums(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<int[]>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT PatNum From patient";
			DataTable dt=Db.GetTable(command);
			int[] patnums=new int[dt.Rows.Count];
			for(int i=0;i<patnums.Length;i++){
				patnums[i]=PIn.PInt(dt.Rows[i]["PatNum"].ToString());
			}
			return patnums;
		}

		///<summary>Converts a date to an age. If age is over 115, then returns 0.</summary>
		public static int DateToAge(DateTime date){
			//No need to check RemotingRole; no call to db.
			if(date.Year<1880)
				return 0;
			if(date.Month < DateTime.Now.Month){//birthday in previous month
				return DateTime.Now.Year-date.Year;
			}
			if(date.Month == DateTime.Now.Month && date.Day <= DateTime.Now.Day){//birthday in this month
				return DateTime.Now.Year-date.Year;
			}
			return DateTime.Now.Year-date.Year-1;
		}

		///<summary>Converts a date to an age. If age is over 115, then returns 0.</summary>
		public static int DateToAge(DateTime birthdate,DateTime asofDate) {
			//No need to check RemotingRole; no call to db.
			if(birthdate.Year<1880)
				return 0;
			if(birthdate.Month < asofDate.Month) {//birthday in previous month
				return asofDate.Year-birthdate.Year;
			}
			if(birthdate.Month == asofDate.Month && birthdate.Day <= asofDate.Day) {//birthday in this month
				return asofDate.Year-birthdate.Year;
			}
			return asofDate.Year-birthdate.Year-1;
		}

		///<summary></summary>
		public static string AgeToString(int age){
			//No need to check RemotingRole; no call to db.
			if(age==0)
				return "";
			else
				return age.ToString();
		}

		public static void ReformatAllPhoneNumbers() {
			string oldTel;
			string newTel;
			string idNum;
			string command="select * from patient";
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				idNum=PIn.PString(table.Rows[i][0].ToString());
				//home
				oldTel=PIn.PString(table.Rows[i][15].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel) {
					command="UPDATE patient SET hmphone = '"
						+POut.PString(newTel)+"' WHERE patNum = '"+idNum+"'";
					Db.NonQ(command);
				}
				//wk:
				oldTel=PIn.PString(table.Rows[i][16].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel) {
					command="UPDATE patient SET wkphone = '"
						+POut.PString(newTel)+"' WHERE patNum = '"+idNum+"'";
					Db.NonQ(command);
				}
				//wireless
				oldTel=PIn.PString(table.Rows[i][17].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel) {// Keyush Shah 04/21/04 Bug, was overwriting wireless with work phone here
					command="UPDATE patient SET wirelessphone = '"
						+POut.PString(newTel)+"' WHERE patNum = '"+idNum+"'";
					Db.NonQ(command);
				}
			}
			command="select * from carrier";
			Db.NonQ(command);
			for(int i=0;i<table.Rows.Count;i++) {
				idNum=PIn.PString(table.Rows[i][0].ToString());
				//ph
				oldTel=PIn.PString(table.Rows[i][7].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel) {
					command="UPDATE carrier SET Phone = '"
						+POut.PString(newTel)+"' WHERE CarrierNum = '"+idNum+"'";
					Db.NonQ(command);
				}
			}
		}

		public static DataTable GetGuarantorInfo(int PatientID) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatientID);
			}
			string command=@"SELECT FName,MiddleI,LName,Guarantor,Address,
								Address2,City,State,Zip,Email,EstBalance,
								BalTotal,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90
						FROM Patient Where Patnum="+PatientID+
				" AND patnum=guarantor";
			return Db.GetTable(command);
		}

		public static DataTable GetPatientByNameAndBirthday(Patient pat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),pat);
			}
			string command="SELECT PatNum FROM patient WHERE "
				+"LName='"+POut.PString(pat.LName)+"' "
				+"AND FName='"+POut.PString(pat.FName)+"' "
				+"AND Birthdate="+POut.PDate(pat.Birthdate)+" "
				+"AND PatStatus!=4";//not deleted
			return Db.GetTable(command);
		}

		public static void UpdateFamilyBillingType(int billingType,int Guarantor){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),billingType,Guarantor);
				return;
			}
			string command="UPDATE patient SET BillingType="+POut.PInt(billingType)+
				" WHERE Guarantor="+POut.PInt(Guarantor);
			Db.NonQ(command);
		}

		public static DataTable GetPartialPatientData(int PatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command=@"SELECT FName,LName,date_format(birthdate,'%m/%d/%Y') as BirthDate,Gender
				FROM patient WHERE patient.PatNum="+PatNum;
			return Db.GetTable(command);
		}

		public static DataTable GetPartialPatientData2(int PatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command=@"SELECT FName,LName,date_format(birthdate,'%m/%d/%Y') as BirthDate,Gender
				        FROM patient WHERE PatNum In (SELECT Guarantor FROM 
                            PATIENT WHERE patnum = "+PatNum+")";
			return Db.GetTable(command);
		}

	}

	///<summary>Not a database table.  Just used in billing and finance charges.</summary>
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
		///<summary>FK to defNum.</summary>
		public int BillingType;
		///<summary></summary>
		public double PayPlanDue;
	}

}