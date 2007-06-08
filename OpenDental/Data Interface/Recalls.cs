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
		public static Recall[] GetList(int[] patNums){
			string wherePats="";
			for(int i=0;i<patNums.Length;i++){
				if(i!=0){
					wherePats+=" OR ";
				}
				wherePats+="PatNum="+patNums[i].ToString();
			}
			string command=
				"SELECT * from recall "
				+"WHERE "+wherePats;
 			DataTable table=General.GetTable(command);
			Recall[] List=new Recall[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Recall();
				List[i].RecallNum      = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DateDueCalc    = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DateDue        = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].DatePrevious   = PIn.PDate  (table.Rows[i][4].ToString());
				List[i].RecallInterval = new Interval(PIn.PInt(table.Rows[i][5].ToString()));
				List[i].RecallStatus   = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				List[i].IsDisabled     = PIn.PBool  (table.Rows[i][8].ToString());
			}
			return List;
		}

		/// <summary></summary>
		public static Recall[] GetList(Patient[] patients){
			int[] patNums=new int[patients.Length];
			for(int i=0;i<patients.Length;i++){
				patNums[i]=patients[i].PatNum;
			}
			return GetList(patNums);
		}

		///<summary>Only used in FormRecallList to get a list of patients with recall.  Supply a date range, using min(-1 day) and max values if user left blank.</summary>
		public static DataTable GetRecallList(DateTime fromDate,DateTime toDate,bool groupByFamilies){
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("age");
			table.Columns.Add("contactMethod");
			table.Columns.Add("dueDate");
			table.Columns.Add("Guarantor");
			table.Columns.Add("Note");
			table.Columns.Add("patientName");
			table.Columns.Add("PatNum");
			table.Columns.Add("PreferRecallMethod");
			table.Columns.Add("recallInterval");
			table.Columns.Add("RecallNum");
			table.Columns.Add("status");
			List<DataRow> rows=new List<DataRow>();
			string command=
				"SELECT recall.RecallNum,recall.PatNum,recall.DateDue,"
				+"recall.RecallInterval,recall.RecallStatus,recall.Note,"
				+"patient.LName,patient.FName,patient.Preferred,patient.Birthdate, "
				+"patient.HmPhone,patient.WkPhone,patient.WirelessPhone,patient.Email, "
				+"patient.Guarantor, patient.PreferRecallMethod "
				+"FROM recall,patient "
				+"WHERE recall.PatNum=patient.PatNum "
				+"AND NOT EXISTS("//test for future appt.
				+"SELECT * FROM appointment,procedurelog,procedurecode "
				+"WHERE procedurelog.PatNum = recall.PatNum "
				+"AND appointment.PatNum = recall.PatNum "
				+"AND procedurelog.CodeNum = procedurecode.CodeNum "
				+"AND procedurelog.AptNum = appointment.AptNum "
				+"AND appointment.AptDateTime >= ";//'"+DateTime.Today.ToString("yyyy-MM-dd")+"' "
				if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
					command+=POut.PDate(MiscData.GetNowDateTime());
				}else{//Assume MySQL
					command+="CURDATE()";
				}
				command+=" AND procedurecode.SetRecall = '1') "//end of NOT EXISTS
				+"AND recall.DateDue >= "+POut.PDate(fromDate)+" "
				+"AND recall.DateDue <= "+POut.PDate(toDate)+" "
				+"AND patient.patstatus=0 "
				+"ORDER BY ";
			if(groupByFamilies){
				command+="patient.Guarantor, ";
			}
			command+="recall.DateDue";
 			DataTable rawtable=General.GetTable(command);
			DateTime date;
			Interval interv;
			Patient pat;
			ContactMethod contmeth;
			for(int i=0;i<rawtable.Rows.Count;i++){
				row=table.NewRow();
				row["age"]=Shared.DateToAge(PIn.PDate(rawtable.Rows[i]["Birthdate"].ToString())).ToString();//we don't care about m/y.
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
				if(contmeth==ContactMethod.DoNotCall || contmeth==ContactMethod.SeeNotes) {
					row["contactMethod"]=Lan.g("enumContactMethod",contmeth.ToString());
				}
				date=PIn.PDate(rawtable.Rows[i]["DateDue"].ToString());
				row["dueDate"]=date.ToShortDateString();
				row["Guarantor"]=rawtable.Rows[i]["Guarantor"].ToString();
				row["Note"]=rawtable.Rows[i]["Note"].ToString();
				pat=new Patient();
				pat.LName=rawtable.Rows[i]["LName"].ToString();
				pat.FName=rawtable.Rows[i]["FName"].ToString();
				pat.Preferred=rawtable.Rows[i]["Preferred"].ToString();
				row["patientName"]=pat.GetNameLF();
				row["PatNum"]=rawtable.Rows[i]["PatNum"].ToString();
				row["PreferRecallMethod"]=rawtable.Rows[i]["PreferRecallMethod"].ToString();//not used yet, but might be.
				interv=new Interval(PIn.PInt(rawtable.Rows[i]["RecallInterval"].ToString()));
				row["recallInterval"]=interv.ToString();
				row["RecallNum"]=rawtable.Rows[i]["RecallNum"].ToString();
				row["status"]=DefB.GetName(DefCat.RecallUnschedStatus,PIn.PInt(rawtable.Rows[i]["RecallStatus"].ToString()));
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
			if(PrefB.RandomKeys) {
				recall.RecallNum=MiscData.GetKey("recall","RecallNum");
			}
			string command= "INSERT INTO recall (";
			if(PrefB.RandomKeys) {
				command+="RecallNum,";
			}
			command+="PatNum,DateDueCalc,DateDue,DatePrevious,"
				+"RecallInterval,RecallStatus,Note,IsDisabled"
				+") VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(recall.RecallNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(recall.PatNum)+"', "
				+POut.PDate(recall.DateDueCalc)+", "
				+POut.PDate(recall.DateDue)+", "
				+POut.PDate(recall.DatePrevious)+", "
				+"'"+POut.PInt(recall.RecallInterval.ToInt())+"', "
				+"'"+POut.PInt(recall.RecallStatus)+"', "
				+"'"+POut.PString(recall.Note)+"', "
				+"'"+POut.PBool(recall.IsDisabled)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				recall.RecallNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Recall recall) {
			string command= "UPDATE recall SET "
				+"PatNum = '"          +POut.PInt(recall.PatNum)+"'"
				+",DateDueCalc = "    +POut.PDate(recall.DateDueCalc)+" "
				+",DateDue = "        +POut.PDate(recall.DateDue)+" "
				+",DatePrevious = "   +POut.PDate(recall.DatePrevious)+" "
				+",RecallInterval = '" +POut.PInt(recall.RecallInterval.ToInt())+"' "
				+",RecallStatus= '"    +POut.PInt(recall.RecallStatus)+"' "
				+",Note = '"           +POut.PString(recall.Note)+"' "
				+",IsDisabled = '"     +POut.PBool(recall.IsDisabled)+"' "
				+" WHERE RecallNum = '"+POut.PInt(recall.RecallNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Recall recall) {
			string command= "DELETE from recall WHERE RecallNum = '"+POut.PInt(recall.RecallNum)+"'";
			General.NonQ(command);
		}

		///<summary>Will only return true if not disabled, date previous is empty, DateDue is same as DateDueCalc, etc.</summary>
		public static bool IsAllDefault(Recall recall) {
			if(recall.IsDisabled
				|| recall.DatePrevious.Year>1880
				|| recall.DateDue != recall.DateDueCalc
				|| recall.RecallInterval!=new Interval(0,0,6,0)
				|| recall.RecallStatus!=0
				|| recall.Note!="") {
				return false;
			}
			return true;
		}

		///<summary>Synchronizes all recall for one patient. If datePrevious has changed, then it completely deletes the old recall information and sets a new dateDueCalc and DatePrevious.  Also updates dateDue to match dateDueCalc if not disabled.  The supplied recall can be null if patient has no existing recall. Deletes or creates any recalls as necessary.</summary>
		public static void Synch(int patNum,Recall recall){
			DateTime previousDate=GetPreviousDate(patNum);
			if(recall!=null 
				&& !recall.IsDisabled
				&& previousDate.Year>1880//this protects recalls that were manually added as part of a conversion
				&& previousDate != recall.DatePrevious) {//if datePrevious has changed, reset
				recall.RecallStatus=0;
				recall.Note="";
				recall.DateDue=recall.DateDueCalc;//now it is allowed to be changed in the steps below
			}
			if(previousDate.Year<1880){//if no previous date
				if(recall==null){//no recall present
					//do nothing.
				}
				else{
					recall.DatePrevious=DateTime.MinValue;
					if(recall.DateDue==recall.DateDueCalc){//user did not enter a DateDue
						recall.DateDue=DateTime.MinValue;
					}
					recall.DateDueCalc=DateTime.MinValue;
					Recalls.Update(recall);
					if(Recalls.IsAllDefault(recall)){//no useful info
						Recalls.Delete(recall);
						recall=null;
					}
				}
			}
			else{//if previous date is a valid date
				if(recall==null){//no recall present
					recall=new Recall();
					recall.PatNum=patNum;
					recall.DatePrevious=previousDate;
					recall.RecallInterval=new Interval(0,0,6,0);
					recall.DateDueCalc=previousDate+recall.RecallInterval;
					recall.DateDue=recall.DateDueCalc;
					Recalls.Insert(recall);
					return;
				}
				else{
					recall.DatePrevious=previousDate;
					if(recall.IsDisabled){//if the existing recall is disabled 
						recall.DateDue=DateTime.MinValue;//DateDue is always blank
					}
					else{//but if not disabled
						if(recall.DateDue==recall.DateDueCalc//if user did not enter a DateDue
							|| recall.DateDue.Year<1880) {//or DateDue was blank
							recall.DateDue=recall.DatePrevious+recall.RecallInterval;//set same as DateDueCalc
						}
					}
					recall.DateDueCalc=recall.DatePrevious+recall.RecallInterval;
					Recalls.Update(recall);
				}
			}
		}

		///<summary>Synchronizes all recall for one patient. Sets dateDueCalc and DatePrevious.  Also updates dateDue to match dateDueCalc if not disabled.  The supplied recall can be null if patient has no existing recall. Deletes or creates any recalls as necessary.</summary>
		public static void Synch(int patNum){
			Recall[] recalls=GetList(new int[] {patNum});
			Recall recall=null;
			if(recalls.Length>0){
				recall=recalls[0];
			}
			Synch(patNum,recall);
		}

		private static DateTime GetPreviousDate(int patNum){
			string command= 
				"SELECT MAX(procedurelog.procdate) "
				+"FROM procedurelog,procedurecode "
				+"WHERE procedurelog.PatNum="+patNum.ToString()
				+" AND procedurecode.CodeNum = procedurelog.CodeNum "
				+"AND procedurecode.SetRecall = 1 "
				+"AND (procedurelog.ProcStatus = 2 "
				+"OR procedurelog.ProcStatus = 3 "
				+"OR procedurelog.ProcStatus = 4) "
				+"GROUP BY procedurelog.PatNum";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return DateTime.MinValue;
			}
			return PIn.PDate(table.Rows[0][0].ToString());
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
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
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
				if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
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









