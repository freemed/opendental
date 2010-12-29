using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeBase;

namespace OpenDentBusiness{
	
	///<summary></summary>
	public class Patients{
		
		///<summary>Returns a Family object for the supplied patNum.  Use Family.GetPatient to extract the desired patient from the family.</summary>
		public static Family GetFamily(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Family>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=//GetFamilySelectCommand(patNum);
				"SELECT patient.* FROM patient WHERE Guarantor = ("
				+"SELECT Guarantor FROM patient WHERE PatNum="+POut.Long(patNum)+") "
				+"ORDER BY CASE WHEN Guarantor=PatNum THEN 0 ELSE 1 END,Birthdate";//Guarantor!=PatNum,Birthdate";
			Family fam=new Family();
			List<Patient> patients=Crud.PatientCrud.SelectMany(command);
			foreach(Patient patient in patients) {
				patient.Age = DateToAge(patient.Birthdate);
			}
			fam.ListPats=new Patient[patients.Count];
			patients.CopyTo(fam.ListPats,0);
			return fam;
		}

		/*
		public static string GetFamilySelectCommand(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),patNum);
			}
			string command= 
				"SELECT guarantor FROM patient "
				+"WHERE patnum = '"+POut.Long(patNum)+"'";
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return null;
			}
			command= 
				"SELECT patient.* "
				+"FROM patient "
				+"WHERE Guarantor = '"+table.Rows[0][0].ToString()+"'"
				+" ORDER BY Guarantor!=PatNum,Birthdate";
			return command;
		}*/

		///<summary>This is a way to get a single patient from the database if you don't already have a family object to use.  Will return null if not found.</summary>
		public static Patient GetPat(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),patNum);
			}
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),patNum);
			} 
			if(patNum==0) {
				return null;
			}
			string command="SELECT * FROM patient WHERE PatNum="+POut.Long(patNum);
			Patient pat=null;
			try {
				pat=Crud.PatientCrud.SelectOne(patNum);
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
			string command="SELECT * FROM patient WHERE ChartNumber='"+POut.String(chartNumber)+"'";
			Patient pat=null;
			try {
				pat=Crud.PatientCrud.SelectOne(command);
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
			string command="SELECT * FROM patient WHERE SSN='"+POut.String(ssn)+"'";
			Patient pat=null;
			try {
				pat=Crud.PatientCrud.SelectOne(command);
			}
			catch { }
			if(pat==null) {
				return null;
			}
			pat.Age = DateToAge(pat.Birthdate);
			return pat;
		}

		public static List<Patient> GetChangedSince(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Patient>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM patient WHERE DateTStamp > "+POut.DateT(changedSince);
			//command+=" "+DbHelper.LimitAnd(1000);
			return Crud.PatientCrud.SelectMany(command);
		}

		///<summary>Used if the number of records are very large, in which case using GetChangedSince(DateTime changedSince) is not the preffered route due to memory problems caused by large recordsets. </summary>
		public static long[] GetChangedSincePatNums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<long[]>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT PatNum From patient WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			long[] patnums=new long[dt.Rows.Count];
			for(int i=0;i<patnums.Length;i++) {
				patnums[i]=PIn.Long(dt.Rows[i]["PatNum"].ToString());
			}
			return patnums;
		}

		///<summary>ONLY for new patients. Set includePatNum to true for use the patnum from the import function.  Used in HL7.  Otherwise, uses InsertID to fill PatNum.</summary>
		public static long Insert(Patient pat,bool useExistingPK) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pat.PatNum=Meth.GetLong(MethodBase.GetCurrentMethod(),pat,useExistingPK);
				return pat.PatNum;
			}
			return Crud.PatientCrud.Insert(pat,useExistingPK);
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply the old Patient object to compare for changes.</summary>
		public static void Update(Patient patient,Patient oldPatient) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patient,oldPatient);
				return;
			}
			Crud.PatientCrud.Update(patient,oldPatient);
		}

		//This can never be used anymore, or it will mess up 
		///<summary>This is only used when entering a new patient and user clicks cancel.  It used to actually delete the patient, but that will mess up UAppoint synch function.  DateTStamp needs to track deleted patients. So now, the PatStatus is simply changed to 4.</summary>
		public static void Delete(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command="UPDATE patient SET PatStatus="+POut.Long((int)PatientStatus.Deleted)+", "
				+"Guarantor=PatNum "
				+"WHERE PatNum ="+pat.PatNum.ToString();
			Db.NonQ(command);
		}

 		///<summary>Only used for the Select Patient dialog.  Pass in a billing type of 0 for all billing types.</summary>
		public static DataTable GetPtDataTable(bool limit,string lname,string fname,string phone,
			string address,bool hideInactive,string city,string state,string ssn,string patnum,string chartnumber,
			long billingtype,bool guarOnly,bool showArchived,bool showProspectiveOnly,long clinicNum,DateTime birthdate,
			long siteNum,string subscriberId)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),limit,lname,fname,phone,address,hideInactive,city,state,ssn,patnum,chartnumber,billingtype,guarOnly,showArchived,showProspectiveOnly,clinicNum,birthdate,siteNum,subscriberId);
			}
			string billingsnippet=" ";
			if(billingtype!=0){
				billingsnippet+="AND BillingType="+POut.Long(billingtype)+" ";
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
				"SELECT patient.PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,WkPhone,Address,PatStatus"
				+",BillingType,ChartNumber,City,State,PriProv,SiteNum ";
			if(subscriberId!=""){
				command+=",inssub.SubscriberId ";
			}
			command+="FROM patient ";
			if(subscriberId!=""){
				command+="LEFT JOIN patplan ON patplan.PatNum=patient.PatNum "
					+"LEFT JOIN insplan ON patplan.PlanNum=insplan.PlanNum "
					+"LEFT JOIN inssub ON patplan.InsSubNum=inssub.InsSubNum ";
			}
			command+="WHERE PatStatus != '4' ";//not status 'deleted'
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command+=(lname.Length>0?"AND LName LIKE '%"+POut.String(lname)+"%' ":"")//LIKE is case insensitive in mysql.
					+(fname.Length>0?"AND FName LIKE '%"+POut.String(fname)+"%' ":"");//LIKE is case insensitive in mysql.
			}
			else {//oracle
				command+=(lname.Length>0?"AND LOWER(LName) LIKE '%"+POut.String(lname).ToLower()+"%' ":"") //case matters in a like statement in oracle.
					+(fname.Length>0?"AND LOWER(FName) LIKE '%"+POut.String(fname).ToLower()+"%' ":"");//case matters in a like statement in oracle.
			}
			if(regexp!="") {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command+="AND (HmPhone REGEXP '"+POut.String(regexp)+"' "
						+"OR WkPhone REGEXP '"+POut.String(regexp)+"' "
						+"OR WirelessPhone REGEXP '"+POut.String(regexp)+"') ";
				} 
				else {//oracle
					command+="AND ((SELECT REGEXP_INSTR(p.HmPhone,'"+POut.String(regexp)+"') FROM dual)<>0"
						+"OR (SELECT REGEXP_INSTR(p.WkPhone,'"+POut.String(regexp)+"') FROM dual)<>0 "
						+"OR (SELECT REGEXP_INSTR(p.WirelessPhone,'"+POut.String(regexp)+"') FROM dual)<>0) ";
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command+=
					(address.Length>0?"AND Address LIKE '%"+POut.String(address)+"%' ":"")//LIKE is case insensitive in mysql.
					+(city.Length>0?"AND City LIKE '"+POut.String(city)+"%' ":"")//LIKE is case insensitive in mysql.
					+(state.Length>0?"AND State LIKE '"+POut.String(state)+"%' ":"")//LIKE is case insensitive in mysql.
					+(ssn.Length>0?"AND SSN LIKE '"+POut.String(ssn)+"%' ":"")//LIKE is case insensitive in mysql.
					+(patnum.Length>0?"AND PatNum LIKE '"+POut.String(patnum)+"%' ":"")//LIKE is case insensitive in mysql.
					+(chartnumber.Length>0?"AND ChartNumber LIKE '"+POut.String(chartnumber)+"%' ":"");//LIKE is case insensitive in mysql.
			}
			else {//oracle
				command+=
					(address.Length>0?"AND LOWER(Address) LIKE '%"+POut.String(address).ToLower()+"%' ":"")//case matters in a like statement in oracle.
					+(city.Length>0?"AND LOWER(City) LIKE '"+POut.String(city).ToLower()+"%' ":"")//case matters in a like statement in oracle.
					+(state.Length>0?"AND LOWER(State) LIKE '"+POut.String(state).ToLower()+"%' ":"")//case matters in a like statement in oracle.
					+(ssn.Length>0?"AND LOWER(SSN) LIKE '"+POut.String(ssn).ToLower()+"%' ":"")//In case an office uses this field for something else.
					+(patnum.Length>0?"AND PatNum LIKE '"+POut.String(patnum)+"%' ":"")//case matters in a like statement in oracle.
					+(chartnumber.Length>0?"AND LOWER(ChartNumber) LIKE '"+POut.String(chartnumber).ToLower()+"%' ":"");//case matters in a like statement in oracle.
			}
			if(birthdate.Year>1880 && birthdate.Year<2100){
				command+="AND Birthdate ="+POut.Date(birthdate)+" ";
			}
			command+=billingsnippet;
			if(hideInactive){
				command+="AND PatStatus != '2' ";
			}
			if(!showArchived) {
				command+="AND PatStatus != '3' AND PatStatus != '5' ";
			}
			if(showProspectiveOnly) {
				command+="AND PatStatus = "+POut.Int((int)PatientStatus.Prospective)+" ";
			}
			if(!showProspectiveOnly) {
				command+="AND PatStatus != "+POut.Int((int)PatientStatus.Prospective)+" ";
			}
			if(guarOnly){
				command+="AND PatNum = Guarantor ";
			}
			if(clinicNum!=0){
				command+="AND ClinicNum="+POut.Long(clinicNum)+" ";
			}
			if(siteNum>0) {
				command+="AND SiteNum="+POut.Long(siteNum)+" ";
			}
			if(subscriberId!=""){
				command+="AND inssub.SubscriberId LIKE '%"+POut.String(subscriberId)+"%' ";
			}
			command+="ORDER BY LName,FName ";
			if(limit){
				command=DbHelper.LimitOrderBy(command,40);
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
				date=PIn.Date(table.Rows[i]["Birthdate"].ToString());
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
				r["PatStatus"]=((PatientStatus)PIn.Long(table.Rows[i]["PatStatus"].ToString())).ToString();
				r["BillingType"]=DefC.GetName(DefCat.BillingTypes,PIn.Long(table.Rows[i]["BillingType"].ToString()));
				r["ChartNumber"]=table.Rows[i]["ChartNumber"].ToString();
				r["City"]=table.Rows[i]["City"].ToString();
				r["State"]=table.Rows[i]["State"].ToString();
				r["PriProv"]=Providers.GetAbbr(PIn.Long(table.Rows[i]["PriProv"].ToString()));
				r["site"]=Sites.GetDescription(PIn.Long(table.Rows[i]["SiteNum"].ToString()));
				PtDataTable.Rows.Add(r);
			}
			return PtDataTable;
		}

		///<summary>Used when filling appointments for an entire day. Gets a list of Pats, multPats, of all the specified patients.  Then, use GetOnePat to pull one patient from this list.  This process requires only one call to the database.</summary>
		public static Patient[] GetMultPats(List<long> patNums) {
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
			Patient[] multPats=Crud.PatientCrud.TableToList(table).ToArray();
			return multPats;
		}

		///<summary>First call GetMultPats to fill the list of multPats. Then, use this to return one patient from that list.</summary>
		public static Patient GetOnePat(Patient[] multPats,long patNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<multPats.Length;i++){
				if(multPats[i].PatNum==patNum){
					return multPats[i];
				}
			}
			return new Patient();
		}

		/// <summary>Gets nine of the most useful fields from the db for the given patnum.</summary>
		public static Patient GetLim(long patNum) {
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
			Lim.PatNum     = PIn.Long   (table.Rows[0][0].ToString());
			Lim.LName      = PIn.String(table.Rows[0][1].ToString());
			Lim.FName      = PIn.String(table.Rows[0][2].ToString());
			Lim.MiddleI    = PIn.String(table.Rows[0][3].ToString());
			Lim.Preferred  = PIn.String(table.Rows[0][4].ToString());
			Lim.CreditType = PIn.String(table.Rows[0][5].ToString());
			Lim.Guarantor  = PIn.Long   (table.Rows[0][6].ToString());
			Lim.HasIns     = PIn.String(table.Rows[0][7].ToString());
			Lim.SSN        = PIn.String(table.Rows[0][8].ToString());
			return Lim;
		}

		///<summary>Gets the patient and provider balances for all patients in the family.  Used from the payment window to help visualize and automate the family splits.</summary>
		public static DataTable GetPaymentStartingBalances(long guarNum,long excludePayNum) {
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
			string command="SET @GuarNum="+POut.Long(guarNum)+";"
				+"SET @ExcludePayNum="+POut.Long(excludePayNum)+";";
			command+=@"
				DROP TABLE IF EXISTS tempfambal;
				CREATE TABLE tempfambal( 
					FamBalNum int NOT NULL auto_increment,
					PatNum bigint NOT NULL,
					ProvNum bigint NOT NULL,
					ClinicNum bigint NOT NULL,
					AmtBal double NOT NULL,
					InsEst double NOT NULL,
					PRIMARY KEY (FamBalNum));
				
				/*Completed procedures*/
				INSERT INTO tempfambal (PatNum,ProvNum,ClinicNum,AmtBal)
				SELECT patient.PatNum,procedurelog.ProvNum,procedurelog.ClinicNum,SUM(ProcFee)
				FROM procedurelog,patient
				WHERE patient.PatNum=procedurelog.PatNum
				AND ProcStatus=2
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum,ClinicNum;
			
				/*Received insurance payments*/
				INSERT INTO tempfambal (PatNum,ProvNum,ClinicNum,AmtBal)
				SELECT patient.PatNum,claimproc.ProvNum,claimproc.ClinicNum,-SUM(InsPayAmt)-SUM(Writeoff)
				FROM claimproc,patient
				WHERE patient.PatNum=claimproc.PatNum
				AND (Status=1 OR Status=4 OR Status=5)/*received,supplemental,capclaim. (7-capcomplete writeoff)*/
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum,ClinicNum;

				/*Insurance estimates*/
				INSERT INTO tempfambal (PatNum,ProvNum,ClinicNum,InsEst)
				SELECT patient.PatNum,claimproc.ProvNum,claimproc.ClinicNum,SUM(InsPayEst)+SUM(Writeoff)
				FROM claimproc,patient
				WHERE patient.PatNum=claimproc.PatNum
				AND Status=0 /*NotReceived*/
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum,ClinicNum;

				/*Adjustments*/
				INSERT INTO tempfambal (PatNum,ProvNum,ClinicNum,AmtBal)
				SELECT patient.PatNum,adjustment.ProvNum,adjustment.ClinicNum,SUM(AdjAmt)
				FROM adjustment,patient
				WHERE patient.PatNum=adjustment.PatNum
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum,ClinicNum;

				/*Patient payments*/
				INSERT INTO tempfambal (PatNum,ProvNum,ClinicNum,AmtBal)
				SELECT patient.PatNum,paysplit.ProvNum,paysplit.ClinicNum,-SUM(SplitAmt)
				FROM paysplit,patient
				WHERE patient.PatNum=paysplit.PatNum
				AND paysplit.PayNum!=@ExcludePayNum
				AND patient.Guarantor=@GuarNum
				AND paysplit.PayPlanNum=0
				GROUP BY patient.PatNum,ProvNum,ClinicNum;

				/*payplan princ reduction*/
				INSERT INTO tempfambal (PatNum,ProvNum,ClinicNum,AmtBal)
				SELECT patient.PatNum,payplancharge.ProvNum,payplancharge.ClinicNum,-SUM(Principal)
				FROM payplancharge,patient
				WHERE patient.PatNum=payplancharge.PatNum
				AND patient.Guarantor=@GuarNum
				GROUP BY patient.PatNum,ProvNum,ClinicNum;

				SELECT tempfambal.PatNum,tempfambal.ProvNum,tempfambal.ClinicNum,SUM(AmtBal) StartBal,SUM(AmtBal-tempfambal.InsEst) AfterIns,FName,Preferred,'0' EndBal
				FROM tempfambal,patient
				WHERE tempfambal.PatNum=patient.PatNum
				GROUP BY PatNum,ProvNum,ClinicNum
				ORDER BY CASE WHEN Guarantor!=patient.PatNum THEN 0 ELSE 1 END,Birthdate,ProvNum,FName,Preferred;

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
				+"FamFinUrgNote = '"+POut.String(Fam.ListPats[0].FamFinUrgNote)+"' "
				+"WHERE PatNum = "+POut.Long(Pat.PatNum);
 			Db.NonQ(command);
			command= "UPDATE patient SET FamFinUrgNote = '' "
				+"WHERE PatNum = '"+Pat.Guarantor.ToString()+"'";
			Db.NonQ(command);
			//Move family financial note to current patient:
			command="SELECT FamFinancial FROM patientnote "
				+"WHERE PatNum = "+POut.Long(Pat.Guarantor);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==1){
				command= "UPDATE patientnote SET "
					+"FamFinancial = '"+POut.String(table.Rows[0][0].ToString())+"' "
					+"WHERE PatNum = "+POut.Long(Pat.PatNum);
				Db.NonQ(command);
			}
			command= "UPDATE patientnote SET FamFinancial = '' "
				+"WHERE PatNum = "+POut.Long(Pat.Guarantor);
			Db.NonQ(command);
			//change guarantor of all family members:
			command= "UPDATE patient SET "
				+"Guarantor = "+POut.Long(Pat.PatNum)
				+" WHERE Guarantor = "+POut.Long(Pat.Guarantor);
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
				+"famfinurgnote = '"+POut.String(Fam.ListPats[0].FamFinUrgNote)
				+POut.String(Pat.FamFinUrgNote)+"' "
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
				+"FROM patientnote WHERE patnum ='"+POut.Long(Pat.PatNum)+"'";
			//MessageBox.Show(string command);
			DataTable table=Db.GetTable(command);
			string strCur=PIn.String(table.Rows[0][0].ToString());
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = '"+POut.String(strGuar+strCur)+"' "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
			Db.NonQ(command);
			//delete cur financial notes
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Pat.PatNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>Key=patNum, value=formatted name.  Used for reports, FormASAP, FormTrackNext, and FormUnsched.</summary>
		public static Dictionary<long,string> GetAllPatientNames() {
			//No need to check RemotingRole; no call to db.
			DataTable table=GetAllPatientNamesTable();
			Dictionary<long,string> dict=new Dictionary<long,string>();
			long patnum;
			string lname,fname,middlei,preferred;
			for(int i=0;i<table.Rows.Count;i++) {
				patnum=PIn.Long(table.Rows[i][0].ToString());
				lname=PIn.String(table.Rows[i][1].ToString());
				fname=PIn.String(table.Rows[i][2].ToString());
				middlei=PIn.String(table.Rows[i][3].ToString());
				preferred=PIn.String(table.Rows[i][4].ToString());
				if(preferred=="") {
					dict.Add(patnum,lname+", "+fname+" "+middlei);
				}
				else {
					dict.Add(patnum,lname+", '"+preferred+"' "+fname+" "+middlei);
				}
			}
			return dict;
		}

		public static DataTable GetAllPatientNamesTable() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command="SELECT patnum,lname,fname,middlei,preferred "
				+"FROM patient";
			DataTable table=Db.GetTable(command);
			return table;
		}

		///<summary></summary>
		public static void UpdateAddressForFam(Patient pat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command= "UPDATE patient SET " 
				+"Address = '"    +POut.String(pat.Address)+"'"
				+",Address2 = '"   +POut.String(pat.Address2)+"'"
				+",City = '"       +POut.String(pat.City)+"'"
				+",State = '"      +POut.String(pat.State)+"'"
				+",Zip = '"        +POut.String(pat.Zip)+"'"
				+",HmPhone = '"    +POut.String(pat.HmPhone)+"'"
				+",credittype = '" +POut.String(pat.CreditType)+"'"
				+",priprov = '"    +POut.Long   (pat.PriProv)+"'"
				+",secprov = '"    +POut.Long   (pat.SecProv)+"'"
				+",feesched = '"   +POut.Long   (pat.FeeSched)+"'"
				+",billingtype = '"+POut.Long   (pat.BillingType)+"'"
				+" WHERE guarantor = '"+POut.Double(pat.Guarantor)+"'";
			Db.NonQ(command);
		}

		///<summary>Used in patient terminal, aka sheet import.  Synchs less fields than the normal synch.</summary>
		public static void UpdateAddressForFamTerminal(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command= "UPDATE patient SET " 
				+"Address = '"    +POut.String(pat.Address)+"'"
				+",Address2 = '"   +POut.String(pat.Address2)+"'"
				+",City = '"       +POut.String(pat.City)+"'"
				+",State = '"      +POut.String(pat.State)+"'"
				+",Zip = '"        +POut.String(pat.Zip)+"'"
				+",HmPhone = '"    +POut.String(pat.HmPhone)+"'"
				+" WHERE guarantor = '"+POut.Double(pat.Guarantor)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void UpdateArriveEarlyForFam(Patient pat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command= "UPDATE patient SET " 
				+"AskToArriveEarly = '"   +POut.Int(pat.AskToArriveEarly)+"'"
				+" WHERE guarantor = '"+POut.Double(pat.Guarantor)+"'";
			DataTable table=Db.GetTable(command);
		}

		///<summary></summary>
		public static void UpdateNotesForFam(Patient pat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command= "UPDATE patient SET " 
				+"addrnote = '"   +POut.String(pat.AddrNote)+"'"
				+" WHERE guarantor = '"+POut.Double(pat.Guarantor)+"'";
			Db.NonQ(command);
		}

		///<summary>Only used from FormRecallListEdit.  Updates two fields for family if they are already the same for the entire family.  If they start out different for different family members, then it only changes the two fields for the single patient.</summary>
		public static void UpdatePhoneAndNoteIfNeeded(string newphone,string newnote,long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),newphone,newnote,patNum);
				return;
			}
			string command="SELECT Guarantor,HmPhone,AddrNote FROM patient WHERE Guarantor="
				+"(SELECT Guarantor FROM patient WHERE PatNum="+POut.Long(patNum)+")";
			DataTable table=Db.GetTable(command);
			bool phoneIsSame=true;
			bool noteIsSame=true;
			long guar=PIn.Long(table.Rows[0]["Guarantor"].ToString());
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["HmPhone"].ToString()!=table.Rows[0]["HmPhone"].ToString()){
					phoneIsSame=false;
				}
				if(table.Rows[i]["AddrNote"].ToString()!=table.Rows[0]["AddrNote"].ToString()) {
					noteIsSame=false;
				}
			}
			command="UPDATE patient SET HmPhone='"+POut.String(newphone)+"' WHERE ";
			if(phoneIsSame){
				command+="Guarantor="+POut.Long(guar);
			}
			else{
				command+="PatNum="+POut.Long(patNum);
			}
			Db.NonQ(command);
			command="UPDATE patient SET AddrNote='"+POut.String(newnote)+"' WHERE ";
			if(noteIsSame) {
				command+="Guarantor="+POut.Long(guar);
			}
			else {
				command+="PatNum="+POut.Long(patNum);
			}
			Db.NonQ(command);
		}

		///<summary>This is only used in the Billing dialog</summary>
		public static List<PatAging> GetAgingList(string age,DateTime lastStatement,List<long> billingNums,bool excludeAddr,
			bool excludeNeg,double excludeLessThan,bool excludeInactive,bool includeChanged,bool excludeInsPending,
			bool excludeIfUnsentProcs,bool ignoreInPerson)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PatAging>>(MethodBase.GetCurrentMethod(),age,lastStatement,billingNums,excludeAddr,excludeNeg,excludeLessThan,excludeInactive,includeChanged,excludeInsPending,excludeIfUnsentProcs,ignoreInPerson);
			}
			string command="";
			if(includeChanged){
				command+=@"DROP TABLE IF EXISTS templastproc;
					CREATE TABLE templastproc(
					Guarantor bigint unsigned NOT NULL,
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
					Guarantor bigint unsigned NOT NULL,
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
					Guarantor bigint unsigned NOT NULL,
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
			if(excludeIfUnsentProcs) {
				command+=@"DROP TABLE IF EXISTS tempunsentprocs;
					CREATE TABLE tempunsentprocs(
					Guarantor bigint unsigned NOT NULL,
					UnsentProcCount int NOT NULL,
					PRIMARY KEY (Guarantor));
					INSERT INTO tempunsentprocs
					SELECT patient.Guarantor,COUNT(*)
					FROM patient,procedurecode,procedurelog,claimproc 
					WHERE claimproc.procnum=procedurelog.procnum
					AND patient.PatNum=procedurelog.PatNum
					AND procedurelog.CodeNum=procedurecode.CodeNum
					AND claimproc.NoBillIns=0
					AND procedurelog.ProcFee>0
					AND claimproc.Status=6
					AND procedurelog.procstatus=2
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
			if(excludeIfUnsentProcs) {
				command+=",IFNULL(tempunsentprocs.UnsentProcCount,'0') AS _unsentProcCount ";
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
			if(excludeIfUnsentProcs) {
				command+="LEFT JOIN tempunsentprocs ON patient.PatNum=tempunsentprocs.Guarantor ";
			}
			command+="WHERE ";
			if(excludeInactive){
				command+="(patstatus != '2') AND ";
			}
			if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
				command+="(BalTotal";
			}
			else {
				command+="(BalTotal - InsEst";
			}
			command+=" > '"+POut.Double(excludeLessThan+.005)+"'"//add half a penny for rounding error
				+" OR PayPlanDue > 0";
			if(!excludeNeg){
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
					command+=" OR BalTotal < '-.005')";
				}
				else {
					command+=" OR BalTotal - InsEst < '-.005')";
				}
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
				command+=POut.Long(billingNums[i])+"'";
					//DefC.Short[(int)DefCat.BillingTypes][billingIndices[i]].DefNum.ToString()+"'";
				if(i==billingNums.Count-1){
					command+=")";
				}
			}
			if(excludeAddr){
				command+=" AND (zip !='')";
			}
			command+=" GROUP BY patient.PatNum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,BillingType,"
				+"InsEst,LName,FName,MiddleI,PayPlanDue,Preferred "
				+"HAVING (LastStatement < "+POut.Date(lastStatement.AddDays(1))+" ";//<midnight of lastStatement date
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
			if(excludeIfUnsentProcs) {
				command+="AND _unsentProcCount=0 ";
			}
			command+="ORDER BY LName,FName";
			//Debug.WriteLine(command);
			DataTable table=Db.GetTable(command);
			List<PatAging> agingList=new List<PatAging>();
			PatAging patage;
			Patient pat;
			for(int i=0;i<table.Rows.Count;i++){
				patage=new PatAging();
				patage.PatNum   = PIn.Long   (table.Rows[i]["PatNum"].ToString());
				patage.Bal_0_30 = PIn.Double(table.Rows[i]["Bal_0_30"].ToString());
				patage.Bal_31_60= PIn.Double(table.Rows[i]["Bal_31_60"].ToString());
				patage.Bal_61_90= PIn.Double(table.Rows[i]["Bal_61_90"].ToString());
				patage.BalOver90= PIn.Double(table.Rows[i]["BalOver90"].ToString());
				patage.BalTotal = PIn.Double(table.Rows[i]["BalTotal"].ToString());
				patage.InsEst   = PIn.Double(table.Rows[i]["InsEst"].ToString());
				pat=new Patient();
				pat.LName=PIn.String(table.Rows[i]["LName"].ToString());
				pat.FName=PIn.String(table.Rows[i]["FName"].ToString());
				pat.MiddleI=PIn.String(table.Rows[i]["MiddleI"].ToString());
				pat.Preferred=PIn.String(table.Rows[i]["Preferred"].ToString());
				patage.PatName=pat.GetNameLF();
				patage.AmountDue=patage.BalTotal-patage.InsEst;
				patage.DateLastStatement=PIn.Date(table.Rows[i]["LastStatement"].ToString());
				patage.BillingType=PIn.Long(table.Rows[i]["BillingType"].ToString());
				patage.PayPlanDue =PIn.Double(table.Rows[i]["PayPlanDue"].ToString());
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
			if(excludeIfUnsentProcs){
				command="DROP TABLE IF EXISTS tempunsentprocs";
				Db.NonQ(command);
			}
			return agingList;
		}

		///<summary>Used only to run finance charges, so it ignores negative balances.</summary>
		public static PatAging[] GetAgingListArray(){
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
				AgingList[i].PatNum   = PIn.Long   (table.Rows[i][0].ToString());
				AgingList[i].Bal_0_30 = PIn.Double(table.Rows[i][1].ToString());
				AgingList[i].Bal_31_60= PIn.Double(table.Rows[i][2].ToString());
				AgingList[i].Bal_61_90= PIn.Double(table.Rows[i][3].ToString());
				AgingList[i].BalOver90= PIn.Double(table.Rows[i][4].ToString());
				AgingList[i].BalTotal = PIn.Double(table.Rows[i][5].ToString());
				AgingList[i].InsEst   = PIn.Double(table.Rows[i][6].ToString());
				AgingList[i].PatName=PIn.String(table.Rows[i][7].ToString())
					+", "+PIn.String(table.Rows[i][8].ToString())
					+" "+PIn.String(table.Rows[i][9].ToString());;
				//AgingList[i].Balance=AgingList[i].Bal_0_30+AgingList[i].Bal_31_60
				//	+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				AgingList[i].AmountDue=AgingList[i].BalTotal-AgingList[i].InsEst;
				AgingList[i].PriProv=PIn.Long(table.Rows[i][10].ToString());
				AgingList[i].BillingType=PIn.Long(table.Rows[i][11].ToString());
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
				+" ORDER BY (chartnumber+0) DESC";//1/13/05 by Keyush Shaw-added 0.
			command=DbHelper.LimitOrderBy(command,1);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){//no existing chart numbers
				return "1";
			}
			string lastChartNum=PIn.String(table.Rows[0][0].ToString());
			//or could add more match conditions
			try {
				return (Convert.ToInt64(lastChartNum)+1).ToString();
			}
			catch {
				throw new ApplicationException(lastChartNum+" is an existing ChartNumber.  It's too big to convert to a long int, so it's not possible to add one to automatically increment.");
			}
		}

		///<summary>Returns the name(only one) of the patient using this chartnumber.</summary>
		public static string ChartNumUsedBy(string chartNum,long excludePatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),chartNum,excludePatNum);
			}
			string command="SELECT LName,FName from patient WHERE "
				+"ChartNumber = '"+chartNum
				+"' AND PatNum != '"+excludePatNum.ToString()+"'";
			DataTable table=Db.GetTable(command);
			string retVal="";
			if(table.Rows.Count!=0){//found duplicate chart number
				retVal=PIn.String(table.Rows[0][1].ToString())+" "+PIn.String(table.Rows[0][0].ToString());
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
			return PIn.Int(table.Rows[0][0].ToString());
		}

		///<summary>Makes a call to the db to figure out if the current HasIns status is correct.  If not, then it changes it.</summary>
		public static void SetHasIns(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="SELECT patient.HasIns,COUNT(patplan.PatNum) FROM patient "
				+"LEFT JOIN patplan ON patplan.PatNum=patient.PatNum"
				+" WHERE patient.PatNum="+POut.Long(patNum)
				+" GROUP BY patplan.PatNum,patient.HasIns";
			DataTable table=Db.GetTable(command);
			string newVal="";
			if(table.Rows[0][1].ToString()!="0"){
				newVal="I";
			}
			if(newVal!=table.Rows[0][0].ToString()){
				command="UPDATE patient SET HasIns='"+POut.String(newVal)
					+"' WHERE PatNum="+POut.Long(patNum);
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
				+"AND PatStatus=0	"
				+"ORDER BY "+DbHelper.DateFormatColumn("Birthdate","%m/%d/%Y");
			DataTable table=Db.GetTable(command);
			table.Columns.Add("Age");
			for(int i=0;i<table.Rows.Count;i++){
				table.Rows[i]["Age"]=DateToAge(PIn.Date(table.Rows[i]["Birthdate"].ToString()),dateTo.AddDays(1)).ToString();
			}
			return table;
		}

		///<summary>Gets the provider for this patient.  If provNum==0, then it gets the practice default prov.</summary>
		public static long GetProvNum(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),pat);
			}
			if(pat.PriProv!=0)
				return pat.PriProv;
			if(PrefC.GetLong(PrefName.PracticeDefaultProv)==0) {
				MessageBox.Show(Lans.g("Patients","Please set a default provider in the practice setup window."));
				return ProviderC.List[0].ProvNum;
			}
			return PrefC.GetLong(PrefName.PracticeDefaultProv);
		}

		///<summary>Gets the list of all valid patient primary keys. Used when checking for missing ADA procedure codes after a user has begun entering them manually. This function is necessary because not all patient numbers are necessarily consecutive (say if the database was created due to a conversion from another program and the customer wanted to keep their old patient ids after the conversion).</summary>
		public static long[] GetAllPatNums() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<long[]>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT PatNum From patient";
			DataTable dt=Db.GetTable(command);
			long[] patnums=new long[dt.Rows.Count];
			for(int i=0;i<patnums.Length;i++){
				patnums[i]=PIn.Long(dt.Rows[i]["PatNum"].ToString());
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

		///<summary>If zero, returns empty string.  Otherwise returns simple year.  Also see PatientLogic.DateToAgeString().</summary>
		public static string AgeToString(int age){
			//No need to check RemotingRole; no call to db.
			if(age==0) {
				return "";
			}
			else {
				return age.ToString();
			}
		}

		public static void ReformatAllPhoneNumbers() {
			string oldTel;
			string newTel;
			string idNum;
			string command="select * from patient";
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				idNum=PIn.String(table.Rows[i][0].ToString());
				//home
				oldTel=PIn.String(table.Rows[i][15].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel) {
					command="UPDATE patient SET hmphone = '"
						+POut.String(newTel)+"' WHERE patNum = '"+idNum+"'";
					Db.NonQ(command);
				}
				//wk:
				oldTel=PIn.String(table.Rows[i][16].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel) {
					command="UPDATE patient SET wkphone = '"
						+POut.String(newTel)+"' WHERE patNum = '"+idNum+"'";
					Db.NonQ(command);
				}
				//wireless
				oldTel=PIn.String(table.Rows[i][17].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel) {// Keyush Shah 04/21/04 Bug, was overwriting wireless with work phone here
					command="UPDATE patient SET wirelessphone = '"
						+POut.String(newTel)+"' WHERE patNum = '"+idNum+"'";
					Db.NonQ(command);
				}
			}
			command="select * from carrier";
			Db.NonQ(command);
			for(int i=0;i<table.Rows.Count;i++) {
				idNum=PIn.String(table.Rows[i][0].ToString());
				//ph
				oldTel=PIn.String(table.Rows[i][7].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel) {
					command="UPDATE carrier SET Phone = '"
						+POut.String(newTel)+"' WHERE CarrierNum = '"+idNum+"'";
					Db.NonQ(command);
				}
			}
		}

		public static DataTable GetGuarantorInfo(long PatientID) {
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

		///<summary>Will return 0 if can't find exact matching pat.</summary>
		public static long GetPatNumByNameAndBirthday(string lName,string fName,DateTime birthdate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),lName,fName,birthdate);
			}
			string command="SELECT PatNum FROM patient WHERE "
				+"LName='"+POut.String(lName)+"' "
				+"AND FName='"+POut.String(fName)+"' "
				+"AND Birthdate="+POut.Date(birthdate)+" "
				+"AND PatStatus!=4";//not deleted
			return PIn.Long(Db.GetScalar(command));
		}

		///<summary>Returns a list of patients that match last and first name.</summary>
		public static List<Patient> GetListByName(string lName,string fName,long PatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Patient>>(MethodBase.GetCurrentMethod(),lName,fName);
			}
			string command="SELECT * FROM patient WHERE "
				+"LOWER(LName)=LOWER('"+POut.String(lName)+"') "
				+"AND LOWER(FName)=LOWER('"+POut.String(fName)+"') "
				+"AND PatNum!="+POut.Long(PatNum)
				+" AND PatStatus!=4";//not deleted
			return Crud.PatientCrud.SelectMany(command);
		}

		public static void UpdateFamilyBillingType(long billingType,long Guarantor) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),billingType,Guarantor);
				return;
			}
			string command="UPDATE patient SET BillingType="+POut.Long(billingType)+
				" WHERE Guarantor="+POut.Long(Guarantor);
			Db.NonQ(command);
		}

		public static DataTable GetPartialPatientData(long PatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command="SELECT FName,LName,"+DbHelper.DateFormatColumn("birthdate","%m/%d/%Y")+" BirthDate,Gender "
				+"FROM patient WHERE patient.PatNum="+PatNum;
			return Db.GetTable(command);
		}

		public static DataTable GetPartialPatientData2(long PatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command=@"SELECT FName,LName,"+DbHelper.DateFormatColumn("birthdate","%m/%d/%Y")+" BirthDate,Gender "
				+"FROM patient WHERE PatNum In (SELECT Guarantor FROM PATIENT WHERE patnum = "+PatNum+")";
			return Db.GetTable(command);
		}

		public static string GetEligibilityDisplayName(long patId) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),patId);
			}
			string command = @"SELECT FName,LName,"+DbHelper.DateFormatColumn("birthdate","%m/%d/%Y")+" BirthDate,Gender "
				+"FROM patient WHERE patient.PatNum=" + POut.Long(patId);
			DataTable table = Db.GetTable(command);
			if(table.Rows.Count == 0) {
				return "Patient(???) is Eligible";
			}
			return PIn.String(table.Rows[0][1].ToString()) + ", "+ PIn.String(table.Rows[0][0].ToString()) + " is Eligible";
		}

		///<summary>Gets the DataTable to display for treatment finder report</summary>
		public static DataTable GetTreatmentFinderList(bool noIns,int monthStart,DateTime dateSince,double aboveAmount,ArrayList providerFilter,
			ArrayList billingFilter,string code1,string code2) 
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),noIns);
			}
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("PatNum");
			table.Columns.Add("LName");
			table.Columns.Add("FName");
			table.Columns.Add("contactMethod");
			table.Columns.Add("address");
			table.Columns.Add("cityStZip");
			table.Columns.Add("annualMax");
			table.Columns.Add("amountUsed");
			table.Columns.Add("amountRemaining");
			table.Columns.Add("treatmentPlan");
			List<DataRow> rows=new List<DataRow>();
			string command=@"
				DROP TABLE IF EXISTS tempused;
				DROP TABLE IF EXISTS tempplanned;
				DROP TABLE IF EXISTS tempannualmax;

				CREATE TABLE tempused(
				PatPlanNum bigint unsigned NOT NULL,
				AmtUsed double NOT NULL,
				PRIMARY KEY (PatPlanNum));

				CREATE TABLE tempplanned(
				PatNum bigint unsigned NOT NULL,
				AmtPlanned double NOT NULL,
				PRIMARY KEY (PatNum));

				CREATE TABLE tempannualmax(
				PlanNum bigint unsigned NOT NULL,
				AnnualMax double NOT NULL,
				PRIMARY KEY (PlanNum));";
			Db.NonQ(command);
			command=@"INSERT INTO tempused
SELECT patplan.PatPlanNum,
SUM(IFNULL(claimproc.InsPayAmt,0))
FROM claimproc
LEFT JOIN patplan ON patplan.PatNum = claimproc.PatNum
AND patplan.PlanNum = claimproc.PlanNum
WHERE claimproc.Status IN (1, 3, 4)
AND claimproc.ProcDate BETWEEN makedate(year(curdate()), 1)
AND makedate(year(curdate())+1, 1) /*current calendar year*/
GROUP BY patplan.PatPlanNum";
			Db.NonQ(command);
			command=@"INSERT INTO tempplanned
SELECT PatNum, SUM(ProcFee)
FROM procedurelog
LEFT JOIN procedurecode ON procedurecode.CodeNum = procedurelog.CodeNum
WHERE ProcStatus = 1 /*treatment planned*/";
			if(code1!="") {
				command+=@" AND (((SELECT STRCMP('"+POut.String(code1)+@"', ProcCode))=0) OR ((SELECT STRCMP('"+POut.String(code1)+@"', ProcCode))=-1))
				AND (((SELECT STRCMP('"+POut.String(code2)+@"', ProcCode))=0) OR ((SELECT STRCMP('"+POut.String(code2)+@"', ProcCode))=1))";
			}
			command+="AND procedurelog.ProcDate>"+POut.DateT(dateSince)+" "
				+"GROUP BY PatNum";
			Db.NonQ(command);
			command=@"INSERT INTO tempannualmax
SELECT benefit.PlanNum, MAX(benefit.MonetaryAmt) /*for oracle in case there's more than one*/
FROM benefit, covcat, insplan
WHERE benefit.CovCatNum = covcat.CovCatNum
AND benefit.PlanNum=insplan.PlanNum 
AND benefit.BenefitType = 5 /* limitation */
AND (covcat.EbenefitCat=1 OR ISNULL(covcat.EbenefitCat))
AND benefit.MonetaryAmt > 0
AND benefit.QuantityQualifier=0 ";
			if(monthStart!=13) {
				command+="AND insplan.MonthRenew='"+POut.Int(monthStart)+"' ";
			}
			command+="GROUP BY benefit.PlanNum ORDER BY benefit.PlanNum";
			Db.NonQ(command);
			command=@"SELECT patient.PatNum, patient.LName, patient.FName,
				patient.Email, patient.HmPhone, patient.PreferRecallMethod,
				patient.WirelessPhone, patient.WkPhone, patient.Address,
				patient.Address2, patient.City, patient.State, patient.Zip,
				patient.PriProv, patient.BillingType, 
				tempannualmax.AnnualMax $AnnualMax,
				tempused.AmtUsed $AmountUsed,
				tempannualmax.AnnualMax-IFNULL(tempused.AmtUsed,0) $AmtRemaining,
				tempplanned.AmtPlanned $TreatmentPlan
				FROM patient
				LEFT JOIN tempplanned ON tempplanned.PatNum=patient.PatNum
				LEFT JOIN patplan ON patient.PatNum=patplan.PatNum
				LEFT JOIN tempused ON tempused.PatPlanNum=patplan.PatPlanNum
				LEFT JOIN tempannualmax ON tempannualmax.PlanNum=patplan.PlanNum
					AND tempannualmax.AnnualMax>0
					/*AND tempannualmax.AnnualMax-tempused.AmtUsed>0*/
				WHERE tempplanned.AmtPlanned>0 ";
			if(!noIns) {//if we don't want patients without insurance
				command+="AND AnnualMax > 0 ";
			}
			if(!(aboveAmount==0 && noIns)){
				command+="AND tempannualmax.AnnualMax-IFNULL(tempused.AmtUsed,0)>"+POut.Double(aboveAmount)+" ";
			}
			for(int i=0;i<providerFilter.Count;i++) {
				if(i==0) {
					command+=" AND (patient.PriProv=";
				}
				else {
					command+=" OR patient.PriProv=";
				}
				command+=POut.Long(ProviderC.List[(int)providerFilter[i]-1].ProvNum);
				if(i==providerFilter.Count-1) {
					command+=") ";
				}
			}
			for(int i=0;i<billingFilter.Count;i++) {
				if(i==0) {
					command+=" AND (patient.BillingType=";
				}
				else {
					command+=" OR patient.BillingType=";
				}
				command+=POut.Long(DefC.Short[(int)DefCat.BillingTypes][(int)billingFilter[i]-1].DefNum);
				if(i==billingFilter.Count-1) {
					command+=") ";
				}
			}
			command+=@"
				AND patient.PatStatus =0
				ORDER BY tempplanned.AmtPlanned DESC";
			DataTable rawtable=Db.GetTable(command);
			command=@"DROP TABLE tempused;
				DROP TABLE tempplanned;
				DROP TABLE tempannualmax;";
			Db.NonQ(command);
			ContactMethod contmeth;
			for(int i=0;i<rawtable.Rows.Count;i++) {
				row=table.NewRow();
				row["PatNum"]=PIn.Long(rawtable.Rows[i]["PatNum"].ToString());
				row["LName"]=rawtable.Rows[i]["LName"].ToString();
				row["FName"]=rawtable.Rows[i]["FName"].ToString();
				contmeth=(ContactMethod)PIn.Long(rawtable.Rows[i]["PreferRecallMethod"].ToString());
				if(contmeth==ContactMethod.None){
					if(PrefC.GetBool(PrefName.RecallUseEmailIfHasEmailAddress)){//if user only wants to use email if contact method is email
						if(rawtable.Rows[i]["Email"].ToString() != "") {
							row["contactMethod"]=rawtable.Rows[i]["Email"].ToString();
						}
						else{
							row["contactMethod"]=Lans.g("FormRecallList","Hm:")+rawtable.Rows[i]["HmPhone"].ToString();
						}
					}
					else{
						row["contactMethod"]=Lans.g("FormRecallList","Hm:")+rawtable.Rows[i]["HmPhone"].ToString();
					}
				}
				if(contmeth==ContactMethod.HmPhone){
					row["contactMethod"]=Lans.g("FormRecallList","Hm:")+rawtable.Rows[i]["HmPhone"].ToString();
				}
				if(contmeth==ContactMethod.WkPhone) {
					row["contactMethod"]=Lans.g("FormRecallList","Wk:")+rawtable.Rows[i]["WkPhone"].ToString();
				}
				if(contmeth==ContactMethod.WirelessPh) {
					row["contactMethod"]=Lans.g("FormRecallList","Cell:")+rawtable.Rows[i]["WirelessPhone"].ToString();
				}
				if(contmeth==ContactMethod.Email) {
					row["contactMethod"]=rawtable.Rows[i]["Email"].ToString();
				}
				if(contmeth==ContactMethod.Mail) {
					row["contactMethod"]=Lans.g("FormRecallList","Mail");
				}
				if(contmeth==ContactMethod.DoNotCall || contmeth==ContactMethod.SeeNotes) {
					row["contactMethod"]=Lans.g("enumContactMethod",contmeth.ToString());
				}
				row["address"]=rawtable.Rows[i]["Address"].ToString();
					if(rawtable.Rows[i]["Address2"].ToString()!="") {
						row["address"]+="\r\n"+rawtable.Rows[i]["Address2"].ToString();
					}
					row["cityStZip"]=rawtable.Rows[i]["City"].ToString()+",  "
						+rawtable.Rows[i]["State"].ToString()+"  "
						+rawtable.Rows[i]["Zip"].ToString();
				row["annualMax"]=(PIn.Double(rawtable.Rows[i]["$AnnualMax"].ToString())).ToString("N");
				row["amountUsed"]=(PIn.Double(rawtable.Rows[i]["$AmountUsed"].ToString())).ToString("N");
				row["amountRemaining"]=(PIn.Double(rawtable.Rows[i]["$AmtRemaining"].ToString())).ToString("N");
				row["treatmentPlan"]=(PIn.Double(rawtable.Rows[i]["$TreatmentPlan"].ToString())).ToString("N");
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>Only a partial folderName will be sent in.  Not the .rvg part.</summary>
		public static bool IsTrophyFolderInUse(string folderName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),folderName);
			}
			string command ="SELECT COUNT(*) FROM patient WHERE TrophyFolder LIKE '%"+POut.String(folderName)+"%'";
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}

		///<summary>To prevent orphaned patients, if patFrom is a guarantor then all family members of patFrom 
		///are moved into the family patTo belongs to, and then the merge of the two specified accounts is performed.</summary>
		public static void MergeTwoPatients(long patTo,long patFrom){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patTo,patFrom);
				return;
			}
			if(patTo==patFrom) {
				//Do not merge the same patient onto itself.
				return;
			}
			string[] patNumForeignKeys=new string[]{
				//This list is up to date as of 11/19/2010 up to version 7.6.0
				"adjustment.PatNum",
				"anestheticrecord.PatNum",
				"anesthvsdata.PatNum",
				"appointment.PatNum",
				"claim.PatNum",
				"claimproc.PatNum",
				"commlog.PatNum",
				"disease.PatNum",
				"document.PatNum",
				"emailmessage.PatNum",
				"etrans.PatNum",
				"formpat.PatNum",
				"inssub.Subscriber",
				"labcase.PatNum",
				"medicationpat.PatNum",
				"mount.PatNum",
				"patfield.PatNum",				
				"patient.ResponsParty",
				//The patientnote table is ignored because only one record can exist for each patient. 
				//The record in 'patFrom' remains so it can be accessed again if needed.
				//"patientnote.PatNum"				
				"patplan.PatNum",
				"payment.PatNum",
				"payplan.Guarantor",//Treated as a patnum, because it is actually a guarantor for the payment plan, and not a patient guarantor.
				"payplan.PatNum",				
				"payplancharge.Guarantor",//Treated as a patnum, because it is actually a guarantor for the payment plan, and not a patient guarantor.
				"payplancharge.PatNum",
				"paysplit.PatNum",
				"perioexam.PatNum",
				"phonenumber.PatNum",
				"plannedappt.PatNum",
				"popup.PatNum",
				"procedurelog.PatNum",
				"procnote.PatNum",
				"proctp.PatNum",
				"question.PatNum",
				"recall.PatNum",
				"refattach.PatNum",
				"referral.PatNum",
				"registrationkey.PatNum",
				"repeatcharge.PatNum",
				"reqstudent.PatNum",
				"rxpat.PatNum",
				"securitylog.PatNum",
				"sheet.PatNum",
				"statement.PatNum",
				//task.KeyNum,//Taken care of in a seperate step, because it is not always a patnum.
				"terminalactive.PatNum",
				"toothinitial.PatNum",
				"treatplan.PatNum",
				"treatplan.ResponsParty",
			};
			string command="";
			Patient patientFrom=Patients.GetPat(patFrom);
			Patient patientTo=Patients.GetPat(patTo);
			string atozFrom=ImageStore.GetPatientFolder(patientFrom);
			string atozTo=ImageStore.GetPatientFolder(patientTo);
			//Move the patient documents within the 'patFrom' A to Z folder to the 'patTo' A to Z folder.
			//We have to be careful here of documents with the same name. We have to rename such documents
			//so that no documents are overwritten/lost.
			string[] fromFiles=Directory.GetFiles(atozFrom);
			for(int i=0;i<fromFiles.Length;i++) {
				string fileName=Path.GetFileName(fromFiles[i]);
				string destFilePath=ODFileUtils.CombinePaths(atozTo,fileName);
				if(File.Exists(destFilePath)) {
					//The file being copied has the same name as a possibly different file within the destination a to z folder.
					//We need to copy the file under a unique file name and then make sure to update the document table to reflect
					//the change.
					destFilePath=ODFileUtils.CombinePaths(atozTo,patientFrom.PatNum.ToString()+"_"+fileName);
					while(File.Exists(destFilePath)){
						destFilePath=ODFileUtils.CombinePaths(atozTo,patientFrom.PatNum.ToString()+"_"+DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")+"_"+fileName);
					}
					command="UPDATE document "
						+"SET FileName='"+POut.String(Path.GetFileName(destFilePath))+"' "
						+"WHERE FileName='"+POut.String(fileName)+"' AND PatNum="+POut.Long(patFrom)+" "+DbHelper.LimitAnd(1);
					Db.NonQ(command);					
				}
				File.Copy(fromFiles[i],destFilePath);//Will throw exception if file already exists.
				try {
					File.Delete(fromFiles[i]);
				} catch {
					//If we were unable to delete the file then it is probably because someone has the document open currently.
					//Just skip deleting the file. This means that occasionally there will be an extra file in their backup
					//which is just clutter but at least the merge is guaranteed this way.
				}
			}
			//If the 'patFrom' had any ties to guardians, they should be deleted to prevent duplicate entries.
			command="DELETE FROM guardian"
				+" WHERE PatNumChild="+POut.Long(patFrom)
				+" OR PatNumGuardian="+POut.Long(patFrom);
			Db.NonQ(command);
			//Update all guarantor foreign keys to change them from 'patFrom' to 
			//the guarantor of 'patTo'. This will effectively move all 'patFrom' family members 
			//to the family defined by 'patTo' in the case that 'patFrom' is a guarantor. If
			//'patFrom' is not a guarantor, then this command will have no effect and is
			//thus safe to always be run.
			command="UPDATE patient "
				+"SET Guarantor="+POut.Long(patientTo.Guarantor)+" "
				+"WHERE Guarantor="+POut.Long(patFrom);
			Db.NonQ(command);
			//At this point, the 'patFrom' is a regular patient and is absoloutely not a guarantor.
			//Now modify all PatNum foreign keys from 'patFrom' to 'patTo' to complete the majority of the
			//merge of the records between the two accounts.			
			for(int i=0;i<patNumForeignKeys.Length;i++) {
				string[] tableAndKeyName=patNumForeignKeys[i].Split(new char[] {'.'});
				command="UPDATE "+tableAndKeyName[0]
					+" SET "+tableAndKeyName[1]+"="+POut.Long(patTo)
					+" WHERE "+tableAndKeyName[1]+"="+POut.Long(patFrom);
				Db.NonQ(command);
			}
			//We have to move over the tasks belonging to the 'patFrom' patient in a seperate step because
			//the KeyNum field of the task table might be a foreign key to something other than a patnum,
			//including possibly an appointment number.
			command="UPDATE task "
				+"SET KeyNum="+POut.Long(patTo)+" "
				+"WHERE KeyNum="+POut.Long(patFrom)+" AND ObjectType="+((int)TaskObjectType.Patient);
			Db.NonQ(command);
			//Mark the patient where data was pulled from as archived unless the patient is already marked as deceased.
			//We need to have the patient marked either archived or deceased so that it is hidden by default, and
			//we also need the customer to be able to access the account again in case a particular table gets missed
			//in the merge tool after an update to Open Dental. This will allow our customers to remerge the missing
			//data after a bug fix is released. 
			command="UPDATE patient "
				+"SET PatStatus="+((int)PatientStatus.Archived)+" "
				+"WHERE PatNum="+POut.Long(patFrom)+" "
				+"AND PatStatus<>"+((int)PatientStatus.Deceased)+" "
				+DbHelper.LimitAnd(1);
			Db.NonQ(command);
		}

		///<summary>LName, 'Preferred' FName M</summary>
		public static string GetNameLF(string LName,string FName,string Preferred,string MiddleI) {
			//No need to check RemotingRole; no call to db.
			string retVal="";
			//if(Title!=""){
			//	retVal+=Title+" ";
			//}
			retVal+=LName+", ";
			if(Preferred!=""){
				retVal+="'"+Preferred+"' ";
			}
			retVal+=FName;
			if(MiddleI!=""){
				retVal+=" "+MiddleI;
			}
			return retVal;
		}

		///<summary>FName 'Preferred' M LName</summary>
		public static string GetNameFL(string LName,string FName,string Preferred,string MiddleI) {
			//No need to check RemotingRole; no call to db.
			string retVal="";
			//if(Title!="") {
				//retVal+=Title+" ";
			//}
			retVal+=FName+" ";
			if(Preferred!="") {
				retVal+="'"+Preferred+"' ";
			}
			if(MiddleI!="") {
				retVal+=MiddleI+" ";
			}
			retVal+=LName;
			return retVal;
		}

		///<summary>FName M LName</summary>
		public static string GetNameFLnoPref(string LName,string FName,string MiddleI) {
			//No need to check RemotingRole; no call to db.
			string retVal="";
			retVal+=FName+" ";
			if(MiddleI!="") {
				retVal+=MiddleI+" ";
			}
			retVal+=LName;
			return retVal;
		}

		///<summary>FName/Preferred LName</summary>
		public static string GetNameFirstOrPrefL(string LName,string FName,string Preferred) {
			//No need to check RemotingRole; no call to db.
			string retVal="";
			if(Preferred=="") {
				retVal+=FName+" ";
			}
			else {
				retVal+=Preferred+" ";
			}
			retVal+=LName;
			return retVal;
		}

		///<summary>FName/Preferred M. LName</summary>
		public static string GetNameFirstOrPrefML(string LName,string FName,string Preferred,string MiddleI) {
			//No need to check RemotingRole; no call to db.
			string retVal="";
			if(Preferred=="") {
				retVal+=FName+" ";
			}
			else {
				retVal+=Preferred+" ";
			}
			if(MiddleI!="") {
				retVal+=MiddleI+". ";
			}
			retVal+=LName;
			return retVal;
		}

		///<summary>Title FName M LName</summary>
		public static string GetNameFLFormal(string LName,string FName,string MiddleI,string Title) {
			//No need to check RemotingRole; no call to db.
			string retVal="";
			if(Title!="") {
				retVal+=Title+" ";
			}
			retVal+=FName+" "+MiddleI+" "+LName;
			return retVal;
		}

		///<summary>Includes preferred.</summary>
		public static string GetNameFirst(string FName,string Preferred) {
			//No need to check RemotingRole; no call to db.
			string retVal=FName;
			if(Preferred!="") {
				retVal+=" '"+Preferred+"'";
			}
			return retVal;
		}

		///<summary></summary>
		public static string GetNameFirstOrPreferred(string FName,string Preferred) {
			//No need to check RemotingRole; no call to db.
			if(Preferred!="") {
				return Preferred;
			}
			return FName;
		}

		///<summary>Dear __.  Does not include the "Dear" or the comma.</summary>
		public static string GetSalutation(string Salutation,string Preferred,string FName) {
			//No need to check RemotingRole; no call to db.
			if(Salutation!="") {
				return Salutation;
			}
			if(Preferred!="") {
				return Preferred;
			}
			return FName;
		}

		/// <summary>Result will be multiline.</summary>
		public static string GetAddressFull(string address,string address2,string city,string state,string zip) {
			string retVal=address;
			if(address2!="") {
				retVal+="\r\n"+address2;
			}
			retVal+="\r\n"+city+", "+state+" "+zip;
			return retVal;
		}



		



	}

	///<summary>Not a database table.  Just used in billing and finance charges.</summary>
	public class PatAging{
		///<summary></summary>
		public long PatNum;
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
		public long PriProv;
		///<summary>The date of the last statement.</summary>
		public DateTime DateLastStatement;
		///<summary>FK to defNum.</summary>
		public long BillingType;
		///<summary></summary>
		public double PayPlanDue;
	}

}