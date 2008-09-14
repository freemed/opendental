using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	
	///<summary></summary>
	public class Recalls{

		///<summary>Gets all recalls for the supplied patients, usually a family or single pat.  Result might have a length of zero.</summary>
		public static List<Recall> GetList(List<int> patNums){
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
				+"AND procedurelog.CodeNum=recalltrigger.CodeNum "
				+"AND recall.PatNum=procedurelog.PatNum "
				+"AND recalltrigger.RecallTypeNum=recall.RecallTypeNum "
				+"AND (appointment.AptStatus=1 "//Scheduled
				+"OR appointment.AptStatus=4))"//ASAP
				+"FROM recall "
				+"WHERE "+wherePats;
			return RefreshAndFill(command);
		}

		public static List<Recall> GetList(int patNum){
			List<int> patNums=new List<int>();
			patNums.Add(patNum);
			return GetList(patNums);
		}

		/// <summary></summary>
		public static List<Recall> GetList(Patient[] patients){
			List<int> patNums=new List<int>();
			for(int i=0;i<patients.Length;i++){
				patNums.Add(patients[i].PatNum);
			}
			return GetList(patNums);
		}

		public static Recall GetRecall(int recallNum){
			string command="SELECT * FROM recall WHERE RecallNum="+POut.PInt(recallNum);
			return RefreshAndFill(command)[0];
		}

		///<summary>Will return a recall or null.</summary>
		public static Recall GetRecallProphyOrPerio(int patNum){
			string command="SELECT * FROM recall WHERE PatNum="+POut.PInt(patNum)
				+" AND (RecallTypeNum="+RecallTypes.ProphyType+" OR RecallTypeNum="+RecallTypes.PerioType+")";
			List<Recall> recallList=RefreshAndFill(command);
			if(recallList.Count==0){
				return null;
			}
			return recallList[0];
		}

		private static List<Recall> RefreshAndFill(string command){
			DataTable table=General.GetTable(command);
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
			string command="SELECT * FROM recall WHERE DateTStamp > "+POut.PDateT(changedSince);
			return RefreshAndFill(command);
		}

		///<summary>Only used in FormRecallList to get a list of patients with recall.  Supply a date range, using min(-1 day) and max values if user left blank.  If provNum=0, then it will get all provnums.  It looks for both provider match in either PriProv or SecProv.</summary>
		public static DataTable GetRecallList(DateTime fromDate,DateTime toDate,bool groupByFamilies,int provNum,int clinicNum,
			int siteNum)
		{
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("age");
			table.Columns.Add("contactMethod");
			table.Columns.Add("dueDate");
			table.Columns.Add("Email");
			table.Columns.Add("Guarantor");
			table.Columns.Add("Note");
			table.Columns.Add("patientName");
			table.Columns.Add("PatNum");
			table.Columns.Add("PreferRecallMethod");
			table.Columns.Add("recallInterval");
			table.Columns.Add("RecallNum");
			table.Columns.Add("recallType");
			table.Columns.Add("status");
			List<DataRow> rows=new List<DataRow>();
			string command=
				"SELECT recall.RecallNum,recall.PatNum,recall.DateDue,"
				+"recall.RecallInterval,recall.RecallStatus,recall.Note,"
				+"patient.LName,patient.FName,patient.Preferred,patient.Birthdate, "
				+"patient.HmPhone,patient.WkPhone,patient.WirelessPhone,patient.Email, "
				+"patient.Guarantor, patient.PreferRecallMethod,recalltype.Description _recalltype "
				+"FROM recall,patient,recalltype "
				+"WHERE recall.PatNum=patient.PatNum "
				+"AND recall.RecallTypeNum=recalltype.RecallTypeNum ";
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
				+"AND procedurelog.CodeNum=recalltrigger.CodeNum "
				+"AND recall.PatNum=procedurelog.PatNum "
				+"AND recalltrigger.RecallTypeNum=recall.RecallTypeNum "
				+"AND (appointment.AptStatus=1 "//Scheduled
				+"OR appointment.AptStatus=4)) "//ASAP,    end of NOT EXISTS
				+"AND recall.DateDue >= "+POut.PDate(fromDate)+" "
				+"AND recall.DateDue <= "+POut.PDate(toDate)+" "
				+"AND patient.patstatus=0 ";
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
				//+"AND (recall.RecallTypeNum="+RecallTypes.ProphyType+" "
				//+"OR recall.RecallTypeNum="+RecallTypes.PerioType+") "
			}
			command+="ORDER BY ";// ";
			if(groupByFamilies){
				command+="patient.Guarantor,";
			}
			command+="recall.RecallTypeNum,recall.DateDue";
 			DataTable rawtable=General.GetTable(command);
			DateTime date;
			Interval interv;
			Patient pat;
			ContactMethod contmeth;
			for(int i=0;i<rawtable.Rows.Count;i++){
				row=table.NewRow();
				row["age"]=Patients.DateToAge(PIn.PDate(rawtable.Rows[i]["Birthdate"].ToString())).ToString();//we don't care about m/y.
				contmeth=(ContactMethod)PIn.PInt(rawtable.Rows[i]["PreferRecallMethod"].ToString());
				if(contmeth==ContactMethod.None || contmeth==ContactMethod.HmPhone){
					row["contactMethod"]=Lan.g("FormRecallList","Hm:")+rawtable.Rows[i]["HmPhone"].ToString();
				}
				if(contmeth==ContactMethod.WkPhone) {
					row["contactMethod"]=Lan.g("FormRecallList","Wk:")+rawtable.Rows[i]["WkPhone"].ToString();
				}
				if(contmeth==ContactMethod.WirelessPh) {
					row["contactMethod"]=Lan.g("FormRecallList","Cell:")+rawtable.Rows[i]["WirelessPhone"].ToString();
				}
				if(contmeth==ContactMethod.Email) {
					row["contactMethod"]=rawtable.Rows[i]["Email"].ToString();
				}
				if(contmeth==ContactMethod.Mail) {
					row["contactMethod"]=Lan.g("FormRecallList","Mail");
				}
				if(contmeth==ContactMethod.DoNotCall || contmeth==ContactMethod.SeeNotes) {
					row["contactMethod"]=Lan.g("enumContactMethod",contmeth.ToString());
				}
				date=PIn.PDate(rawtable.Rows[i]["DateDue"].ToString());
				row["dueDate"]=date.ToShortDateString();
				row["Email"]=rawtable.Rows[i]["Email"].ToString();
				row["Guarantor"]=rawtable.Rows[i]["Guarantor"].ToString();
				row["Note"]=rawtable.Rows[i]["Note"].ToString();
				pat=new Patient();
				pat.LName=rawtable.Rows[i]["LName"].ToString();
				pat.FName=rawtable.Rows[i]["FName"].ToString();
				pat.Preferred=rawtable.Rows[i]["Preferred"].ToString();
				row["patientName"]=pat.GetNameLF();
				row["PatNum"]=rawtable.Rows[i]["PatNum"].ToString();
				row["PreferRecallMethod"]=rawtable.Rows[i]["PreferRecallMethod"].ToString();
				interv=new Interval(PIn.PInt(rawtable.Rows[i]["RecallInterval"].ToString()));
				row["recallInterval"]=interv.ToString();
				row["RecallNum"]=rawtable.Rows[i]["RecallNum"].ToString();
				row["recallType"]=rawtable.Rows[i]["_recalltype"].ToString();
				row["status"]=DefC.GetName(DefCat.RecallUnschedStatus,PIn.PInt(rawtable.Rows[i]["RecallStatus"].ToString()));
				rows.Add(row);
			}
			//Array.Sort(orderDate,RecallList);
			//return RecallList;
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary></summary>
		public static void Insert(Recall recall) {
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
				General.NonQ(command);
			}
			else {
				recall.RecallNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Recall recall) {
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
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Recall recall) {
			string command= "DELETE from recall WHERE RecallNum = '"+POut.PInt(recall.RecallNum)+"'";
			General.NonQ(command);
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

		///<summary>Synchronizes all recalls for one patient. If datePrevious has changed, then it completely deletes the old status and note information and sets a new DatePrevious and dateDueCalc.  Also updates dateDue to match dateDueCalc if not disabled.  Creates any recalls as necessary.  Recalls will never get automatically deleted.  Instead, the dateDueCalc just gets cleared.</summary>
		public static void Synch(int patNum){
			List<RecallType> typeList=RecallTypes.GetActive();
			string command="SELECT * FROM recall WHERE PatNum="+POut.PInt(patNum);
			List<Recall> recallList=RefreshAndFill(command);
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
			for(int i=0;i<typeList.Count;i++){
				if(i>0){
					command+=" OR";
				}
				command+=" RecallTypeNum="+POut.PInt(typeList[i].RecallTypeNum);
			}
			command+=") AND (ProcStatus = 2 "
				+"OR ProcStatus = 3 "
				+"OR ProcStatus = 4) "
				+"GROUP BY RecallTypeNum";
			DataTable tableDates=General.GetTable(command);
			//Go through the type list and either update recalls, or create new recalls.
			//Recalls that are no longer active because their type has no triggers will be ignored.
			//It is assumed that there are no duplicate recall types for a patient.
			DateTime prevDate;
			Recall matchingRecall;
			Recall recallNew;
			for(int i=0;i<typeList.Count;i++){
				prevDate=DateTime.MinValue;
				for(int d=0;d<tableDates.Rows.Count;d++){
					if(tableDates.Rows[d]["RecallTypeNum"].ToString()==typeList[i].RecallTypeNum.ToString()){
						prevDate=PIn.PDate(tableDates.Rows[d]["_procDate"].ToString());
						break;
					}
				}
				matchingRecall=null;
				for(int r=0;r<recallList.Count;r++){
					if(recallList[r].RecallTypeNum==typeList[i].RecallTypeNum){
						matchingRecall=recallList[r];
					}
				}
				if(matchingRecall==null){//if there is no existing recall,
					if(prevDate.Year>1880){//if date is not minVal, then add a recall
						//add a recall
						recallNew=new Recall();
						recallNew.RecallTypeNum=typeList[i].RecallTypeNum;
						recallNew.PatNum=patNum;
						recallNew.DatePrevious=prevDate;
						recallNew.RecallInterval=typeList[i].DefaultInterval;
						recallNew.DateDueCalc=prevDate+recallNew.RecallInterval;
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
		}

		///<summary>Only called when editing certain procedurecodes, but only very rarely as needed. For power users, this is a good little trick to use to synch recall for all patients.</summary>
		public static void SynchAllPatients(){
			//get all active patients
			string command="SELECT PatNum "
				+"FROM patient "
				+"WHERE PatStatus=0";
			DataTable table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				Synch(PIn.PInt(table.Rows[i][0].ToString()));
			}
		}

		/// <summary></summary>
		public static DataTable GetAddrTable(int[] patNums,bool groupByFamily){
			string command="SELECT patient.LName,patient.FName,patient.MiddleI,patient.Preferred,"//0-3
				+"patient.Address,patient.Address2,patient.City,patient.State,patient.Zip,recall.DateDue, "//4-9
				+"patient.Guarantor,"//10
				+"'' FamList ";//placeholder column: 11 for patient names and dates. If empty, then only single patient will print
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command+=",CASE WHEN patient.PatNum=patient.Guarantor THEN 1 ELSE 0 END AS isguarantor ";
			}
			command+="FROM patient,recall "
				+"WHERE patient.PatNum=recall.PatNum "
				+"AND (";
      for(int i=0;i<patNums.Length;i++){
        if(i>0){
					command+=" OR ";
				}
        command+="patient.PatNum="+patNums[i].ToString();
      }
			command+=") ";
			if(groupByFamily){
				command+="ORDER BY patient.Guarantor,";
				if(DataConnection.DBtype==DatabaseType.Oracle){
					command+="13";//isguarantor column
				}
				else{
					command+="patient.PatNum = patient.Guarantor";//guarantor needs to be last //FIXME:ORDER-BY. probably fixed??
				}
			}
			else{
				command+="ORDER BY patient.LName,patient.FName";
			}
			DataTable table=General.GetTable(command);
			if(!groupByFamily){
				return table;
			}
			DataTable newTable=table.Clone();
			string familyAptList="";
			DataRow row;
			for(int i=0;i<table.Rows.Count;i++){
				if(familyAptList==""){//if this is the first patient in the family
					if(i==table.Rows.Count-1//if this is the last row
						|| table.Rows[i][10].ToString()!=table.Rows[i+1][10].ToString())//or if the guarantor on next line is different
					{
						//then this is a single patient, and there are no other family members in the list.
						row=newTable.NewRow();
						row[0]=table.Rows[i][0].ToString();//LName
						row[1]=table.Rows[i][1].ToString();//FName
						row[2]=table.Rows[i][2].ToString();//MiddleI
						row[3]=table.Rows[i][3].ToString();//Preferred
						row[4]=table.Rows[i][4].ToString();//Address
						row[5]=table.Rows[i][5].ToString();//Address2
						row[6]=table.Rows[i][6].ToString();//City
						row[7]=table.Rows[i][7].ToString();//State
						row[8]=table.Rows[i][8].ToString();//Zip
						row[9]=table.Rows[i][9].ToString();//DateDue
						//we don't care about the guarantor for printing
						//row[]=table.Rows[i][].ToString();//
						newTable.Rows.Add(row);
						continue;
					}
					else{//this is the first patient of a family with multiple family members
						familyAptList=table.Rows[i][1].ToString()+":  "//FName
							+PIn.PDate(table.Rows[i][9].ToString()).ToShortDateString();//due date
						continue;
					}
				}
				else{//not the first patient
					familyAptList+="\r\n"+table.Rows[i][1].ToString()+":  "//FName
						+PIn.PDate(table.Rows[i][9].ToString()).ToShortDateString();//due date
				}
				if(i==table.Rows.Count-1//if this is the last row
					|| table.Rows[i][10].ToString()!=table.Rows[i+1][10].ToString())//or if the guarantor on next line is different
				{
					row=newTable.NewRow();
					//so for the query above, the guarantor should be last to show here.
					row[0]=table.Rows[i][0].ToString();//LName
					row[4]=table.Rows[i][4].ToString();//Address
					row[5]=table.Rows[i][5].ToString();//Address2
					row[6]=table.Rows[i][6].ToString();//City
					row[7]=table.Rows[i][7].ToString();//State
					row[8]=table.Rows[i][8].ToString();//Zip
					row[11]=familyAptList;
					//we don't really care about the other fields for printing
					//row[]=table.Rows[i][].ToString();//
					newTable.Rows.Add(row);
					familyAptList="";
				}	
			}
			return newTable;
		}

		/// <summary></summary>
		public static void UpdateStatus(int recallNum,int newStatus){
			string command="UPDATE recall SET RecallStatus="+newStatus.ToString()
				+" WHERE RecallNum="+recallNum.ToString();
			General.NonQ(command);
		}


	}

	
	

}









