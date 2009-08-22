using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	
	///<summary></summary>
	public class Recalls{

		///<summary>Gets all recalls for the supplied patients, usually a family or single pat.  Result might have a length of zero.  Each recall will also have the DateScheduled filled by pulling that info from other tables.</summary>
		public static List<Recall> GetList(List<int> patNums){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Recall>>(MethodBase.GetCurrentMethod(),patNums);
			} 
			string wherePats="";
			for(int i=0;i<patNums.Count;i++){
				if(i!=0){
					wherePats+=" OR ";
				}
				wherePats+="PatNum="+patNums[i].ToString();
			}
			string command=
				"SELECT recall.*, "
				//MIN prevents multiple rows from being returned in the subquery.
				+"(SELECT MIN(appointment.AptDateTime) FROM appointment,procedurelog,recalltrigger "
				+"WHERE appointment.AptNum=procedurelog.AptNum "
				+"AND appointment.AptDateTime > NOW() "
				+"AND procedurelog.CodeNum=recalltrigger.CodeNum "
				+"AND recall.PatNum=procedurelog.PatNum "
				+"AND recalltrigger.RecallTypeNum=recall.RecallTypeNum "
				+"AND (appointment.AptStatus=1 "//Scheduled
				+"OR appointment.AptStatus=4))"//ASAP
				+"FROM recall "
				+"WHERE "+wherePats;
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table);
		}

		public static List<Recall> GetList(int patNum){
			//No need to check RemotingRole; no call to db.
			List<int> patNums=new List<int>();
			patNums.Add(patNum);
			return GetList(patNums);
		}

		/// <summary></summary>
		public static List<Recall> GetList(List<Patient> patients){
			//No need to check RemotingRole; no call to db.
			List<int> patNums=new List<int>();
			for(int i=0;i<patients.Count;i++){
				patNums.Add(patients[i].PatNum);
			}
			return GetList(patNums);
		}

		public static Recall GetRecall(int recallNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Recall>(MethodBase.GetCurrentMethod(),recallNum);
			} 
			string command="SELECT * FROM recall WHERE RecallNum="+POut.PInt(recallNum);
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table)[0];
		}

		///<summary>Will return a recall or null.</summary>
		public static Recall GetRecallProphyOrPerio(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Recall>(MethodBase.GetCurrentMethod(),patNum);
			} 
			string command="SELECT * FROM recall WHERE PatNum="+POut.PInt(patNum)
				+" AND (RecallTypeNum="+RecallTypes.ProphyType+" OR RecallTypeNum="+RecallTypes.PerioType+")";
			DataTable table=Db.GetTable(command);
			List<Recall> recallList=RefreshAndFill(table);
			if(recallList.Count==0){
				return null;
			}
			return recallList[0];
		}

		private static List<Recall> RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<Recall> list=new List<Recall>();
			Recall recall;
			for(int i=0;i<table.Rows.Count;i++){
				recall=new Recall();
				recall.RecallNum      = PIn.PInt   (table.Rows[i][0].ToString());
				recall.PatNum         = PIn.PInt   (table.Rows[i][1].ToString());
				recall.DateDueCalc    = PIn.PDate  (table.Rows[i][2].ToString());
				recall.DateDue        = PIn.PDate  (table.Rows[i][3].ToString());
				recall.DatePrevious   = PIn.PDate  (table.Rows[i][4].ToString());
				recall.RecallInterval = new Interval(PIn.PInt(table.Rows[i][5].ToString()));
				recall.RecallStatus   = PIn.PInt   (table.Rows[i][6].ToString());
				recall.Note           = PIn.PString(table.Rows[i][7].ToString());
				recall.IsDisabled     = PIn.PBool  (table.Rows[i][8].ToString());
				//DateTStamp
				recall.RecallTypeNum  = PIn.PInt   (table.Rows[i][10].ToString());
				if(table.Columns.Count>11){
					recall.DateScheduled= PIn.PDate  (table.Rows[i][11].ToString());
				}
				list.Add(recall);
			}
			return list;
		}

		public static List<Recall> GetUAppoint(DateTime changedSince){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Recall>>(MethodBase.GetCurrentMethod(),changedSince);
			} 
			string command="SELECT * FROM recall WHERE DateTStamp > "+POut.PDateT(changedSince);
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table);
		}

		///<summary>Only used in FormRecallList to get a list of patients with recall.  Supply a date range, using min and max values if user left blank.  If provNum=0, then it will get all provnums.  It looks for both provider match in either PriProv or SecProv.  If sortAlph is false, it will sort by dueDate.</summary>
		public static DataTable GetRecallList(DateTime fromDate,DateTime toDate,bool groupByFamilies,int provNum,int clinicNum,
			int siteNum,bool sortAlph)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),fromDate,toDate,groupByFamilies,provNum,clinicNum,siteNum,sortAlph);
			}
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("age");
			table.Columns.Add("contactMethod");//text representation for display
			table.Columns.Add("dateLastReminder");
			table.Columns.Add("DateDue",typeof(DateTime));
			table.Columns.Add("dueDate");//blank if minVal
			table.Columns.Add("Email");
			table.Columns.Add("FName");
			table.Columns.Add("Guarantor");
			table.Columns.Add("guarFName");
			table.Columns.Add("guarLName");
			table.Columns.Add("LName");
			table.Columns.Add("maxDateDue",typeof(DateTime));
			table.Columns.Add("Note");
			table.Columns.Add("numberOfReminders");
			table.Columns.Add("patientName");
			table.Columns.Add("PatNum");
			table.Columns.Add("PreferRecallMethod");
			table.Columns.Add("recallInterval");
			table.Columns.Add("RecallNum");
			table.Columns.Add("recallType");
			table.Columns.Add("status");
			List<DataRow> rows=new List<DataRow>();
			//get maxDateDue for each family.
			string command=@"SELECT MAX(recall.DateDue) maxDateDue,patient.Guarantor 
				FROM patient
				LEFT JOIN recall ON patient.PatNum=recall.PatNum
				AND recall.DateDue >= "+POut.PDate(fromDate)+" "
				+"AND recall.DateDue <= "+POut.PDate(toDate)+" "
				+"AND recall.IsDisabled = 0 "
				+"AND NOT EXISTS("//test for scheduled appt.
				+"SELECT * FROM appointment,procedurelog,recalltrigger "
				+"WHERE appointment.AptNum=procedurelog.AptNum "
				+"AND procedurelog.CodeNum=recalltrigger.CodeNum "
				+"AND recall.PatNum=procedurelog.PatNum "
				+"AND recalltrigger.RecallTypeNum=recall.RecallTypeNum "
				+"AND (appointment.AptStatus=1 "//Scheduled
				+"OR appointment.AptStatus=4)) "//ASAP,    end of NOT EXISTS
				+"GROUP BY patient.Guarantor";
			DataTable maxTable=Db.GetTable(command);
			//key=guarantor, value=maxDateDue
			Dictionary<int,DateTime> dictMaxDateDue=new Dictionary<int,DateTime>();
			for(int i=0;i<maxTable.Rows.Count;i++) {
				dictMaxDateDue.Add(PIn.PInt(maxTable.Rows[i]["Guarantor"].ToString()),PIn.PDate(maxTable.Rows[i]["maxDateDue"].ToString()));
			}
			command=
				@"SELECT patient.Birthdate,recall.DateDue,MAX(CommDateTime) _dateLastReminder,
				patient.Email,patguar.Email _guarEmail,patguar.FName _guarFName,
				patguar.LName _guarLName,patient.FName,
				patient.Guarantor,patient.HmPhone,patient.LName,recall.Note,
				COUNT(commlog.CommlogNum) _numberOfReminders,
				recall.PatNum,patient.PreferRecallMethod,patient.Preferred,
				recall.RecallInterval,recall.RecallNum,recall.RecallStatus,
				recalltype.Description _recalltype,patient.WirelessPhone,patient.WkPhone
				FROM recall
				LEFT JOIN patient ON recall.PatNum=patient.PatNum
				LEFT JOIN patient patguar ON patient.Guarantor=patguar.PatNum
				LEFT JOIN recalltype ON recall.RecallTypeNum=recalltype.RecallTypeNum
				LEFT JOIN commlog ON commlog.PatNum=recall.PatNum
				AND CommType="+POut.PInt(Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL))+" "
				//+"AND SentOrReceived = "+POut.PInt((int)CommSentOrReceived.Sent)+" "
				+"AND CommDateTime > recall.DatePrevious "
				+"WHERE patient.patstatus=0 ";
			if(provNum>0){
				command+="AND (patient.PriProv="+POut.PInt(provNum)+" "
					+"OR patient.SecProv="+POut.PInt(provNum)+") ";
			}
			if(clinicNum>0) {
				command+="AND patient.ClinicNum="+POut.PInt(clinicNum)+" ";
			}
			if(siteNum>0) {
				command+="AND patient.SiteNum="+POut.PInt(siteNum)+" ";
			}
			command+=
				"AND NOT EXISTS("//test for scheduled appt.
				+"SELECT * FROM appointment,procedurelog,recalltrigger "
				+"WHERE appointment.AptNum=procedurelog.AptNum "
				+"AND appointment.PatNum=recall.PatNum "
				+"AND procedurelog.CodeNum=recalltrigger.CodeNum "
				+"AND recall.PatNum=procedurelog.PatNum "
				+"AND recalltrigger.RecallTypeNum=recall.RecallTypeNum "
				+"AND (appointment.AptStatus=1 "//Scheduled
				+"OR appointment.AptStatus=4) "//ASAP
				+"AND appointment.AptDateTime > CURDATE() "//early this morning
				+") "//end of NOT EXISTS
				+"AND recall.DateDue >= "+POut.PDate(fromDate)+" "
				+"AND recall.DateDue <= "+POut.PDate(toDate)+" "
				+"AND recall.IsDisabled = 0 ";
			List<int> recalltypes=new List<int>();
			string[] typearray=PrefC.GetString("RecallTypesShowingInList").Split(',');
			if(typearray.Length>0){
				for(int i=0;i<typearray.Length;i++){
					recalltypes.Add(PIn.PInt(typearray[i]));
				}
			}
			if(recalltypes.Count>0){
				command+="AND (";
				for(int i=0;i<recalltypes.Count;i++){
					if(i>0){
						command+=" OR ";
					}
					command+="recall.RecallTypeNum="+POut.PInt(recalltypes[i]);
				}
				command+=") ";
			}
			command+="GROUP BY recall.PatNum ";
 			DataTable rawtable=Db.GetTable(command);
			DateTime dateDue;
			DateTime dateRemind;
			Interval interv;
			Patient pat;
			ContactMethod contmeth;
			int guarNum;
			string numberOfReminders;
			for(int i=0;i<rawtable.Rows.Count;i++){
				dateDue=PIn.PDate(rawtable.Rows[i]["DateDue"].ToString());
				dateRemind=PIn.PDate(rawtable.Rows[i]["_dateLastReminder"].ToString());
				numberOfReminders=rawtable.Rows[i]["_numberOfReminders"].ToString();
				if(numberOfReminders=="0" || numberOfReminders=="") {
					//always show
				}
				else if(numberOfReminders=="1") {
					if(PrefC.GetInt("RecallShowIfDaysFirstReminder")==-1) {
						continue;
					}
					if(dateRemind.AddDays(PrefC.GetInt("RecallShowIfDaysFirstReminder")) > DateTime.Today) {
						continue;
					}
				}
				else{//2 or more reminders
					if(PrefC.GetInt("RecallShowIfDaysSecondReminder")==-1) {
						continue;
					}
					if(dateRemind.AddDays(PrefC.GetInt("RecallShowIfDaysSecondReminder")) > DateTime.Today) {
						continue;
					}
				}
				row=table.NewRow();
				row["age"]=Patients.DateToAge(PIn.PDate(rawtable.Rows[i]["Birthdate"].ToString())).ToString();//we don't care about m/y.
				contmeth=(ContactMethod)PIn.PInt(rawtable.Rows[i]["PreferRecallMethod"].ToString());
				if(contmeth==ContactMethod.None){
					if(groupByFamilies){
						if(rawtable.Rows[i]["_guarEmail"].ToString() != "") {//since there is an email,
							row["contactMethod"]=rawtable.Rows[i]["_guarEmail"].ToString();
						}
						else{
							row["contactMethod"]=Lans.g("FormRecallList","Hm:")+rawtable.Rows[i]["HmPhone"].ToString();
						}
					}
					else{
						if(rawtable.Rows[i]["Email"].ToString() != "") {//since there is an email,
							row["contactMethod"]=rawtable.Rows[i]["Email"].ToString();
						}
						else{
							row["contactMethod"]=Lans.g("FormRecallList","Hm:")+rawtable.Rows[i]["HmPhone"].ToString();
						}
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
					if(groupByFamilies) {
						//always use guarantor email
						row["contactMethod"]=rawtable.Rows[i]["_guarEmail"].ToString();
					}
					else {
						row["contactMethod"]=rawtable.Rows[i]["Email"].ToString();
					}
				}
				if(contmeth==ContactMethod.Mail) {
					row["contactMethod"]=Lans.g("FormRecallList","Mail");
				}
				if(contmeth==ContactMethod.DoNotCall || contmeth==ContactMethod.SeeNotes) {
					row["contactMethod"]=Lans.g("enumContactMethod",contmeth.ToString());
				}
				if(dateRemind.Year<1880) {
					row["dateLastReminder"]="";
				}
				else {
					row["dateLastReminder"]=dateRemind.ToShortDateString();
				}
				row["DateDue"]=dateDue;
				if(dateDue.Year<1880) {
					row["dueDate"]="";
				}
				else {
					row["dueDate"]=dateDue.ToShortDateString();
				}
				if(groupByFamilies) {
					row["Email"]=rawtable.Rows[i]["_guarEmail"].ToString();
				}
				else {
					row["Email"]=rawtable.Rows[i]["Email"].ToString();
				}
				row["FName"]=rawtable.Rows[i]["FName"].ToString();
				row["Guarantor"]=rawtable.Rows[i]["Guarantor"].ToString();
				row["guarFName"]=rawtable.Rows[i]["_guarFName"].ToString();
				row["guarLName"]=rawtable.Rows[i]["_guarLName"].ToString();
				row["LName"]=rawtable.Rows[i]["LName"].ToString();
				guarNum=PIn.PInt(rawtable.Rows[i]["Guarantor"].ToString());
				if(dictMaxDateDue.ContainsKey(guarNum)) {
					row["maxDateDue"]=dictMaxDateDue[guarNum];
				}
				else {
					row["maxDateDue"]=DateTime.MinValue;
				}
				row["Note"]=rawtable.Rows[i]["Note"].ToString();
				if(numberOfReminders=="0") {
					row["numberOfReminders"]="";
				}
				else {
					row["numberOfReminders"]=numberOfReminders;
				}
				pat=new Patient();
				pat.LName=rawtable.Rows[i]["LName"].ToString();
				pat.FName=rawtable.Rows[i]["FName"].ToString();
				pat.Preferred=rawtable.Rows[i]["Preferred"].ToString();
				row["patientName"]=pat.GetNameLF();
				row["PatNum"]=rawtable.Rows[i]["PatNum"].ToString();
				if(contmeth==ContactMethod.None){
					if(groupByFamilies) {
						if(rawtable.Rows[i]["_guarEmail"].ToString() != "") {//since there is an email,
							row["PreferRecallMethod"]=((int)ContactMethod.Email).ToString();
						}
						else {
							row["PreferRecallMethod"]=((int)ContactMethod.None).ToString();
						}
					}
					else {
						if(rawtable.Rows[i]["Email"].ToString() != "") {//since there is an email,
							row["PreferRecallMethod"]=((int)ContactMethod.Email).ToString();
						}
						else {
							row["PreferRecallMethod"]=((int)ContactMethod.None).ToString();
						}
					}
				}
				else{
					row["PreferRecallMethod"]=rawtable.Rows[i]["PreferRecallMethod"].ToString();
				}
				interv=new Interval(PIn.PInt(rawtable.Rows[i]["RecallInterval"].ToString()));
				row["recallInterval"]=interv.ToString();
				row["RecallNum"]=rawtable.Rows[i]["RecallNum"].ToString();
				row["recallType"]=rawtable.Rows[i]["_recalltype"].ToString();
				row["status"]=DefC.GetName(DefCat.RecallUnschedStatus,PIn.PInt(rawtable.Rows[i]["RecallStatus"].ToString()));
				rows.Add(row);
			}
			RecallComparer comparer=new RecallComparer();
			comparer.GroupByFamilies=groupByFamilies;
			comparer.SortAlph=sortAlph;
			rows.Sort(comparer);
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary></summary>
		public static int Insert(Recall recall) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				recall.RecallNum=Meth.GetInt(MethodBase.GetCurrentMethod(),recall);
				return recall.RecallNum;
			}
			if(PrefC.RandomKeys) {
				recall.RecallNum=MiscData.GetKey("recall","RecallNum");
			}
			string command= "INSERT INTO recall (";
			if(PrefC.RandomKeys) {
				command+="RecallNum,";
			}
			command+="PatNum,DateDueCalc,DateDue,DatePrevious,"
				+"RecallInterval,RecallStatus,Note,IsDisabled,"//DateTStamp
				+"RecallTypeNum"
				+") VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(recall.RecallNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (recall.PatNum)+"', "
				    +POut.PDate  (recall.DateDueCalc)+", "
				    +POut.PDate  (recall.DateDue)+", "
				    +POut.PDate  (recall.DatePrevious)+", "
				+"'"+POut.PInt   (recall.RecallInterval.ToInt())+"', "
				+"'"+POut.PInt   (recall.RecallStatus)+"', "
				+"'"+POut.PString(recall.Note)+"', "
				+"'"+POut.PBool  (recall.IsDisabled)+"', "
				//DateTStamp
				+"'"+POut.PInt   (recall.RecallTypeNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				recall.RecallNum=Db.NonQ(command,true);
			}
			return recall.RecallNum;
		}

		///<summary></summary>
		public static void Update(Recall recall) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),recall);
				return;
			}
			string command= "UPDATE recall SET "
				+"PatNum = '"          +POut.PInt   (recall.PatNum)+"'"
				+",DateDueCalc = "     +POut.PDate  (recall.DateDueCalc)+" "
				+",DateDue = "         +POut.PDate  (recall.DateDue)+" "
				+",DatePrevious = "    +POut.PDate  (recall.DatePrevious)+" "
				+",RecallInterval = '" +POut.PInt   (recall.RecallInterval.ToInt())+"' "
				+",RecallStatus= '"    +POut.PInt   (recall.RecallStatus)+"' "
				+",Note = '"           +POut.PString(recall.Note)+"' "
				+",IsDisabled = '"     +POut.PBool  (recall.IsDisabled)+"' "
				//DateTStamp
				+",RecallTypeNum = '"  +POut.PInt   (recall.RecallTypeNum)+"' "
				+" WHERE RecallNum = '"+POut.PInt   (recall.RecallNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Recall recall) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),recall);
				return;
			}
			string command= "DELETE from recall WHERE RecallNum = "+POut.PInt(recall.RecallNum);
			Db.NonQ(command);
			DeletedObjects.SetDeleted(DeletedObjectType.RecallPatNum,recall.PatNum);
		}

		/*//<summary>Will only return true if not disabled, date previous is empty, DateDue is same as DateDueCalc, etc.</summary>
		public static bool IsAllDefault(Recall recall) {
			if(recall.IsDisabled
				|| recall.DatePrevious.Year>1880
				|| recall.DateDue != recall.DateDueCalc
				|| recall.RecallInterval!=RecallTypes.GetInterval(recall.RecallTypeNum)//new Interval(0,0,6,0)
				|| recall.RecallStatus!=0
				|| recall.Note!="") 
			{
				return false;
			}
			return true;
		}*/

		///<summary>Synchronizes all recalls for one patient. If datePrevious has changed, then it completely deletes the old status and note information and sets a new DatePrevious and dateDueCalc.  Also updates dateDue to match dateDueCalc if not disabled.  Creates any recalls as necessary.  Recalls will never get automatically deleted except when all triggers are removed.  Otherwise, the dateDueCalc just gets cleared.</summary>
		public static void Synch(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			List<RecallType> typeListActive=RecallTypes.GetActive();
			List<RecallType> typeList=new List<RecallType>(typeListActive);
			string command="SELECT * FROM recall WHERE PatNum="+POut.PInt(patNum);
			DataTable table=Db.GetTable(command);
			List<Recall> recallList=RefreshAndFill(table);
			//determine if this patient is a perio patient.
			bool isPerio=false;
			for(int i=0;i<recallList.Count;i++){
				if(PrefC.GetInt("RecallTypeSpecialPerio")==recallList[i].RecallTypeNum){
					isPerio=true;
					break;
				}
			}
			//remove types from the list which do not apply to this patient.
			for(int i=0;i<typeList.Count;i++){
				if(isPerio){
					if(PrefC.GetInt("RecallTypeSpecialProphy")==typeList[i].RecallTypeNum){
						typeList.RemoveAt(i);
						break;
					}
				}
				else{
					if(PrefC.GetInt("RecallTypeSpecialPerio")==typeList[i].RecallTypeNum){
						typeList.RemoveAt(i);
						break;
					}
				}
			}
			//get previous dates for all types at once
			command="SELECT RecallTypeNum,MAX(ProcDate) _procDate "
				+"FROM procedurelog,recalltrigger "
				+"WHERE PatNum="+POut.PInt(patNum)
				+" AND procedurelog.CodeNum=recalltrigger.CodeNum "
				+"AND (";
			if(typeListActive.Count>0) {
				for(int i=0;i<typeListActive.Count;i++) {
					if(i>0) {
						command+=" OR";
					}
					command+=" RecallTypeNum="+POut.PInt(typeListActive[i].RecallTypeNum);
				}
			} 
			else {
				command+=" RecallTypeNum=0";//Effectively forces an empty result set, without changing the returned table structure.
			}
			command+=") AND (ProcStatus = "+POut.PInt((int)ProcStat.C)+" "
				+"OR ProcStatus = "+POut.PInt((int)ProcStat.EC)+" "
				+"OR ProcStatus = "+POut.PInt((int)ProcStat.EO)+") "
				+"GROUP BY RecallTypeNum";
			DataTable tableDates=Db.GetTable(command);
			//Go through the type list and either update recalls, or create new recalls.
			//Recalls that are no longer active because their type has no triggers will be ignored.
			//It is assumed that there are no duplicate recall types for a patient.
			DateTime prevDate;
			Recall matchingRecall;
			Recall recallNew;
			DateTime prevDateProphy=DateTime.MinValue;
			DateTime dateProphyTesting;
			for(int i=0;i<typeListActive.Count;i++) {
				if(PrefC.GetInt("RecallTypeSpecialProphy")!=typeListActive[i].RecallTypeNum
					&& PrefC.GetInt("RecallTypeSpecialPerio")!=typeListActive[i].RecallTypeNum) 
				{
					continue;
				}
				for(int d=0;d<tableDates.Rows.Count;d++) {//procs for patient
					if(tableDates.Rows[d]["RecallTypeNum"].ToString()==typeListActive[i].RecallTypeNum.ToString()) {
						dateProphyTesting=PIn.PDate(tableDates.Rows[d]["_procDate"].ToString());
						//but patient could have both perio and prophy.
						//So must test to see if the date is newer
						if(dateProphyTesting>prevDateProphy) {
							prevDateProphy=dateProphyTesting;
						}
						break;
					}
				}
			}
			for(int i=0;i<typeList.Count;i++){
				if(PrefC.GetInt("RecallTypeSpecialProphy")==typeList[i].RecallTypeNum
					|| PrefC.GetInt("RecallTypeSpecialPerio")==typeList[i].RecallTypeNum) 
				{
					prevDate=prevDateProphy;
				}
				else {
					prevDate=DateTime.MinValue;
					for(int d=0;d<tableDates.Rows.Count;d++) {//procs for patient
						if(tableDates.Rows[d]["RecallTypeNum"].ToString()==typeList[i].RecallTypeNum.ToString()) {
							prevDate=PIn.PDate(tableDates.Rows[d]["_procDate"].ToString());
							break;
						}
					}
				}
				matchingRecall=null;
				for(int r=0;r<recallList.Count;r++){//recalls for patient
					if(recallList[r].RecallTypeNum==typeList[i].RecallTypeNum){
						matchingRecall=recallList[r];
					}
				}
				if(matchingRecall==null){//if there is no existing recall,
					if(PrefC.GetInt("RecallTypeSpecialProphy")==typeList[i].RecallTypeNum
						|| PrefC.GetInt("RecallTypeSpecialPerio")==typeList[i].RecallTypeNum
						|| prevDate.Year>1880)//for other types, if date is not minVal, then add a recall
					{
						//add a recall
						recallNew=new Recall();
						recallNew.RecallTypeNum=typeList[i].RecallTypeNum;
						recallNew.PatNum=patNum;
						recallNew.DatePrevious=prevDate;//will be min val for prophy/perio with no previous procs
						recallNew.RecallInterval=typeList[i].DefaultInterval;
						if(prevDate.Year<1880) {
							recallNew.DateDueCalc=DateTime.MinValue;
						}
						else {
							recallNew.DateDueCalc=prevDate+recallNew.RecallInterval;
						}
						recallNew.DateDue=recallNew.DateDueCalc;
						Recalls.Insert(recallNew);
					}
				}
				else{//alter the existing recall
					if(!matchingRecall.IsDisabled
						&& prevDate.Year>1880//this protects recalls that were manually added as part of a conversion
						&& prevDate != matchingRecall.DatePrevious) 
					{//if datePrevious has changed, reset
						matchingRecall.RecallStatus=0;
						matchingRecall.Note="";
						matchingRecall.DateDue=matchingRecall.DateDueCalc;//now it is allowed to be changed in the steps below
					}
					if(prevDate.Year<1880){//if no previous date
						matchingRecall.DatePrevious=DateTime.MinValue;
						if(matchingRecall.DateDue==matchingRecall.DateDueCalc){//user did not enter a DateDue
							matchingRecall.DateDue=DateTime.MinValue;
						}
						matchingRecall.DateDueCalc=DateTime.MinValue;
						Recalls.Update(matchingRecall);
					}
					else{//if previous date is a valid date
						matchingRecall.DatePrevious=prevDate;
						if(matchingRecall.IsDisabled){//if the existing recall is disabled 
							matchingRecall.DateDue=DateTime.MinValue;//DateDue is always blank
						}
						else{//but if not disabled
							if(matchingRecall.DateDue==matchingRecall.DateDueCalc//if user did not enter a DateDue
								|| matchingRecall.DateDue.Year<1880)//or DateDue was blank
							{
								matchingRecall.DateDue=matchingRecall.DatePrevious+matchingRecall.RecallInterval;//set same as DateDueCalc
							}
						}
						matchingRecall.DateDueCalc=matchingRecall.DatePrevious+matchingRecall.RecallInterval;
						Recalls.Update(matchingRecall);
					}
				}
			}
			//now, we need to loop through all the inactive recall types and clear the DateDueCalc
			List<RecallType> typeListInactive=RecallTypes.GetInactive();
			for(int i=0;i<typeListInactive.Count;i++){
				matchingRecall=null;
				for(int r=0;r<recallList.Count;r++){
					if(recallList[r].RecallTypeNum==typeListInactive[i].RecallTypeNum){
						matchingRecall=recallList[r];
					}
				}
				if(matchingRecall==null){//if there is no existing recall,
					continue;
				}
				Recalls.Delete(matchingRecall);//we'll just delete it
				/*
				//There is an existing recall, so alter it if certain conditions are met
				matchingRecall.DatePrevious=DateTime.MinValue;
				if(matchingRecall.DateDue==matchingRecall.DateDueCalc){//if user did not enter a DateDue
					//we can safely alter the DateDue
					matchingRecall.DateDue=DateTime.MinValue;
				}
				matchingRecall.DateDueCalc=DateTime.MinValue;
				Recalls.Update(matchingRecall);*/
			}
		}

		///<summary></summary>
		public static void SynchAllPatients(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			//get all active patients
			string command="SELECT PatNum "
				+"FROM patient "
				+"WHERE PatStatus=0";
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				Synch(PIn.PInt(table.Rows[i][0].ToString()));
			}
		}

		/// <summary></summary>
		public static DataTable GetAddrTable(List<int> recallNums,bool groupByFamily,bool sortAlph){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),recallNums,groupByFamily,sortAlph);
			}
			//get maxDateDue for each family.
			string command=@"DROP TABLE IF EXISTS temprecallmaxdate;
				CREATE table temprecallmaxdate(
					Guarantor int NOT NULL,
					MaxDateDue date NOT NULL,
					PRIMARY KEY (Guarantor)
				);
				INSERT INTO temprecallmaxdate 
				SELECT patient.Guarantor,MAX(recall.DateDue) maxDateDue
				FROM patient
				LEFT JOIN recall ON patient.PatNum=recall.PatNum
				AND (";
			for(int i=0;i<recallNums.Count;i++){
				if(i>0){
					command+=" OR ";
				}
				command+="recall.RecallNum="+POut.PInt(recallNums[i]);
			}
			command+=") GROUP BY patient.Guarantor";
			Db.NonQ(command);
			command=@"SELECT patient.Address,patguar.Address guarAddress,
				patient.Address2,patguar.Address2 guarAddress2,
				patient.City,patguar.City guarCity,recall.DateDue,patient.Email,patguar.Email guarEmail,
				patient.FName,patguar.FName guarFName,patient.Guarantor,
				patient.LName,patguar.LName guarLName,temprecallmaxdate.MaxDateDue maxDateDue,
				patient.MiddleI,
				COUNT(commlog.CommlogNum) numberOfReminders,
				patient.PatNum,patient.Preferred,recall.RecallNum,
				patient.State,patguar.State guarState,patient.Zip,patguar.Zip guarZip
				FROM recall 
				LEFT JOIN patient ON patient.PatNum=recall.PatNum 
				LEFT JOIN patient patguar ON patient.Guarantor=patguar.PatNum
				LEFT JOIN commlog ON commlog.PatNum=recall.PatNum
				AND CommType="+POut.PInt(Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL))+" "
				+"AND SentOrReceived = "+POut.PInt((int)CommSentOrReceived.Sent)+" "
				+"AND CommDateTime > recall.DatePrevious "
				+"LEFT JOIN temprecallmaxdate ON temprecallmaxdate.Guarantor=patient.Guarantor "
				+"WHERE ";
      for(int i=0;i<recallNums.Count;i++){
        if(i>0){
					command+=" OR ";
				}
        command+="recall.RecallNum="+POut.PInt(recallNums[i]);
      }
			command+=" GROUP BY recall.RecallNum";
			DataTable rawTable=Db.GetTable(command);
			command="DROP TABLE IF EXISTS temprecallmaxdate";
			Db.NonQ(command);
			List<DataRow> rawRows=new List<DataRow>();
			for(int i=0;i<rawTable.Rows.Count;i++){
				rawRows.Add(rawTable.Rows[i]);
			}
			RecallComparer comparer=new RecallComparer();
			comparer.GroupByFamilies=groupByFamily;
			comparer.SortAlph=sortAlph;
			rawRows.Sort(comparer);
			DataTable table=new DataTable();
			table.Columns.Add("address");//includes address2. Can be guar.
			table.Columns.Add("cityStZip");//Can be guar.
			table.Columns.Add("dateDue");
			table.Columns.Add("email");//Will be guar if grouped by family
			table.Columns.Add("emailPatNum");//Will be guar if grouped by family
			table.Columns.Add("famList");
			table.Columns.Add("guarLName");
			table.Columns.Add("numberOfReminders");//for a family, this will be the max for the family
			table.Columns.Add("patientNameF");//Only used when single email
			table.Columns.Add("patientNameFL");
			table.Columns.Add("patNums");//Comma delimited.  Used in email.
			table.Columns.Add("recallNums");//Comma delimited.  Used during e-mail
			string familyAptList="";
			string recallNumStr="";
			string patNumStr="";
			DataRow row;
			List<DataRow> rows=new List<DataRow>();
			int maxNumReminders=0;
			int maxRemindersThisPat;
			for(int i=0;i<rawRows.Count;i++) {
				if(!groupByFamily) {
					row=table.NewRow();
					row["address"]=rawRows[i]["Address"].ToString();
					if(rawRows[i]["Address2"].ToString()!="") {
						row["address"]+="\r\n"+rawRows[i]["Address2"].ToString();
					}
					row["cityStZip"]=rawRows[i]["City"].ToString()+",  "
						+rawRows[i]["State"].ToString()+"  "
						+rawRows[i]["Zip"].ToString();
					row["dateDue"]=PIn.PDate(rawRows[i]["DateDue"].ToString()).ToShortDateString();
					//since not grouping by family, this is always just the patient email
					row["email"]=rawRows[i]["Email"].ToString();
					row["emailPatNum"]=rawRows[i]["PatNum"].ToString();
					row["famList"]="";
					row["guarLName"]=rawRows[i]["guarLName"].ToString();//even though we won't use it.
					row["numberOfReminders"]=PIn.PInt(rawRows[i]["numberOfReminders"].ToString()).ToString();
					if(rawRows[i]["Preferred"].ToString()=="") {
						row["patientNameF"]=rawRows[i]["FName"].ToString();
					}
					else {
						row["patientNameF"]=rawRows[i]["Preferred"].ToString();
					}
					row["patientNameFL"]=rawRows[i]["FName"].ToString()+" "
						+rawRows[i]["MiddleI"].ToString()+" "
						+rawRows[i]["LName"].ToString();
					row["patNums"]=rawRows[i]["PatNum"].ToString();
					row["recallNums"]=rawRows[i]["RecallNum"].ToString();
					rows.Add(row);
					continue;
				}
				//groupByFamily----------------------------------------------------------------------
				if(familyAptList==""){//if this is the first patient in the family
					maxNumReminders=0;
					//loop through the whole family, and determine the maximum number of reminders
					for(int f=i;f<rawRows.Count;f++) {
						maxRemindersThisPat=PIn.PInt(rawRows[f]["numberOfReminders"].ToString());
						if(maxRemindersThisPat>maxNumReminders) {
							maxNumReminders=maxRemindersThisPat;
						}
						if(f==rawRows.Count-1//if this is the last row
							|| rawRows[i]["Guarantor"].ToString()!=rawRows[f+1]["Guarantor"].ToString())//or if the guarantor on next line is different
						{
							break;
						}
					}
					//now we know the max number of reminders for the family
					if(i==rawRows.Count-1//if this is the last row
						|| rawRows[i]["Guarantor"].ToString()!=rawRows[i+1]["Guarantor"].ToString())//or if the guarantor on next line is different
					{
						//then this is a single patient, and there are no other family members in the list.
						row=table.NewRow();
						row["address"]=rawRows[i]["Address"].ToString();
						if(rawRows[i]["Address2"].ToString()!="") {
							row["address"]+="\r\n"+rawRows[i]["Address2"].ToString();
						}
						row["cityStZip"]=rawRows[i]["City"].ToString()+",  "
							+rawRows[i]["State"].ToString()+"  "
							+rawRows[i]["Zip"].ToString();
						row["dateDue"]=PIn.PDate(rawRows[i]["DateDue"].ToString()).ToShortDateString();
						//this will always be the guarantor email
						row["email"]=rawRows[i]["guarEmail"].ToString();
						row["emailPatNum"]=rawRows[i]["Guarantor"].ToString();
						row["famList"]="";
						row["guarLName"]=rawRows[i]["guarLName"].ToString();//even though we won't use it.
						row["numberOfReminders"]=maxNumReminders.ToString();
						if(rawRows[i]["Preferred"].ToString()=="") {
							row["patientNameF"]=rawRows[i]["FName"].ToString();
						}
						else {
							row["patientNameF"]=rawRows[i]["Preferred"].ToString();
						}
						row["patientNameFL"]=rawRows[i]["FName"].ToString()+" "
							+rawRows[i]["MiddleI"].ToString()+" "
							+rawRows[i]["LName"].ToString();
						row["patNums"]=rawRows[i]["PatNum"].ToString();
						row["recallNums"]=rawRows[i]["RecallNum"].ToString();
						rows.Add(row);
						continue;
					}
					else{//this is the first patient of a family with multiple family members
						familyAptList=rawRows[i]["FName"].ToString()+":  "
							+PIn.PDate(rawRows[i]["DateDue"].ToString()).ToShortDateString();
						patNumStr=rawRows[i]["PatNum"].ToString();
						recallNumStr=rawRows[i]["RecallNum"].ToString();
						continue;
					}
				}
				else{//not the first patient
					familyAptList+="\r\n"+rawRows[i]["FName"].ToString()+":  "
						+PIn.PDate(rawRows[i]["DateDue"].ToString()).ToShortDateString();
					patNumStr+=","+rawRows[i]["PatNum"].ToString();
					recallNumStr+=","+rawRows[i]["RecallNum"].ToString();
				}
				if(i==rawRows.Count-1//if this is the last row
					|| rawRows[i]["Guarantor"].ToString()!=rawRows[i+1]["Guarantor"].ToString())//or if the guarantor on next line is different
				{
					//This part only happens for the last family member of a grouped family
					row=table.NewRow();
					row["address"]=rawRows[i]["guarAddress"].ToString();
					if(rawRows[i]["guarAddress2"].ToString()!="") {
						row["address"]+="\r\n"+rawRows[i]["guarAddress2"].ToString();
					}
					row["cityStZip"]=rawRows[i]["guarCity"].ToString()+",  "
						+rawRows[i]["guarState"].ToString()+"  "
						+rawRows[i]["guarZip"].ToString();
					row["dateDue"]=PIn.PDate(rawRows[i]["DateDue"].ToString()).ToShortDateString();
					row["email"]=rawRows[i]["guarEmail"].ToString();
					row["emailPatNum"]=rawRows[i]["Guarantor"].ToString();
					row["famList"]=familyAptList;
					row["guarLName"]=rawRows[i]["guarLName"].ToString();
					row["numberOfReminders"]=maxNumReminders.ToString();
					row["patientNameF"]="";//not used here
					row["patientNameFL"]="";//we won't use this
					row["patNums"]=patNumStr;
					row["recallNums"]=recallNumStr;
					rows.Add(row);
					familyAptList="";
				}	
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		/// <summary></summary>
		public static void UpdateStatus(int recallNum,int newStatus){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),recallNum,newStatus);
				return;
			}
			string command="UPDATE recall SET RecallStatus="+newStatus.ToString()
				+" WHERE RecallNum="+recallNum.ToString();
			Db.NonQ(command);
		}


	}

	///<summary>The supplied DataRows must include the following columns: Guarantor, PatNum, guarLName, guarFName, LName, FName, DateDue, maxDateDue.  maxDateDue is the most recent DateDue for all family members in the list and needs to be the same for all family members.  This date will be used for better grouping.</summary>
	class RecallComparer:IComparer<DataRow> {
		public bool GroupByFamilies;
		///<summary>rather than by the ordinary DueDate.</summary>
		public bool SortAlph;

		///<summary></summary>
		public int Compare(DataRow x,DataRow y) {
			//NOTE: Even if grouping by families, each family is not necessarily going to have a guarantor.
			if(GroupByFamilies) {
				if(SortAlph) {
					//if guarantors are different, sort by guarantor name
					if(x["Guarantor"].ToString() != y["Guarantor"].ToString()) {
						if(x["guarLName"].ToString() != y["guarLName"].ToString()) {
							return x["guarLName"].ToString().CompareTo(y["guarLName"].ToString());
						}
						return x["guarFName"].ToString().CompareTo(y["guarFName"].ToString());
					}
					return 0;//order within family does not matter
				}
				else {//sort by maxDateDue
					DateTime xD=PIn.PDate(x["maxDateDue"].ToString());
					DateTime yD=PIn.PDate(y["maxDateDue"].ToString());
					if(xD != yD) {
						return (xD.CompareTo(yD));
					}
					//if dates are same, sort by guarantor
					if(x["Guarantor"].ToString() != y["Guarantor"].ToString()) {
						return (x["Guarantor"].ToString().CompareTo(y["Guarantor"].ToString()));
					}
					//within the same family, sort by actual DueDate
					xD=PIn.PDate(x["DateDue"].ToString());
					yD=PIn.PDate(y["DateDue"].ToString());
					return (xD.CompareTo(yD));
				}
			}
			else {//individual patients
				if(SortAlph) {
					if(x["LName"].ToString() != y["LName"].ToString()) {
						return x["LName"].ToString().CompareTo(y["LName"].ToString());
					}
					return x["FName"].ToString().CompareTo(y["FName"].ToString());
				}
				else {//sort by DueDate
					if((DateTime)x["DateDue"] != (DateTime)y["DateDue"]) {
						return ((DateTime)x["DateDue"]).CompareTo(((DateTime)y["DateDue"]));
					}
					//if duedates are the same, sort by LName
					return x["LName"].ToString().CompareTo(y["LName"].ToString());
				}
			}
			//return 0;
		}
	}

	
	

}









