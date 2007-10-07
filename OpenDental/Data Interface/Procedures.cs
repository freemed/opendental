using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Procedures{
		//private static bool OpenDentalBusinessIsLocal=false;

		///<summary></summary>
		public static void Insert(Procedure proc){
			try{
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ProcedureB.Insert(proc);
				}
				else {
					DtoProcedureInsert dto=new DtoProcedureInsert();
					dto.Proc=proc;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e){
				MessageBox.Show(e.Message);
			}
		}

		///<summary>Updates all fields in the supplied procedure.</summary>
		public static void Update(Procedure proc,Procedure procOld){
			try{
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ProcedureB.Update(proc,procOld);
				}
				else {
					DtoProcedureUpdate dto=new DtoProcedureUpdate();
					dto.Proc=proc;
					dto.OldProc=procOld;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		///<summary>Also deletes any claimProcs. Must test to make sure claimProcs are not part of a payment first.  Returns false if unsuccessful.</summary>
		public static bool Delete(int procNum) {
			try{
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ProcedureB.Delete(procNum);
				}
				else {
					DtoProcedureDelete dto=new DtoProcedureDelete();
					dto.ProcNum=procNum;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return false;
			}
			return true;
		}


		public static void UpdateAptNum(int procNum,int newAptNum){
			try{
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ProcedureB.UpdateAptNum(procNum,newAptNum);
				}
				else {
					DtoProcedureUpdateAptNum dto=new DtoProcedureUpdateAptNum();
					dto.ProcNum=procNum;
					dto.NewAptNum=newAptNum;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		public static void UpdatePlannedAptNum(int procNum,int newPlannedAptNum){
			try{
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ProcedureB.UpdatePlannedAptNum(procNum,newPlannedAptNum);
				}
				else {
					DtoProcedureUpdatePlannedAptNum dto=new DtoProcedureUpdatePlannedAptNum();
					dto.ProcNum=procNum;
					dto.NewPlannedAptNum=newPlannedAptNum;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		public static void UpdatePriority(int procNum,int newPriority){
			try{
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ProcedureB.UpdatePriority(procNum,newPriority);
				}
				else {
					DtoProcedureUpdatePriority dto=new DtoProcedureUpdatePriority();
					dto.ProcNum=procNum;
					dto.NewPriority=newPriority;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		public static void UpdateFee(int procNum,double newFee){
			try{
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ProcedureB.UpdateFee(procNum,newFee);
				}
				else {
					DtoProcedureUpdateFee dto=new DtoProcedureUpdateFee();
					dto.ProcNum=procNum;
					dto.NewFee=newFee;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		///<summary>Gets all procedures for a single patient, without notes.  Does not include deleted procedures.</summary>
		public static Procedure[] Refresh(int patNum){
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=ProcedureB.Refresh(patNum);
				}
				else {
					DtoProcedureRefresh dto=new DtoProcedureRefresh();
					dto.PatNum=patNum;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return new Procedure[0];
			}
			Procedure[] procList=ConvertToList(ds.Tables[0]);
			return procList;
		}

		///<summary>Gets one procedure directly from the db.  Option to include the note.</summary>
		public static Procedure GetOneProc(int procNum,bool includeNote){
			string command=
				"SELECT * FROM procedurelog "
				+"WHERE ProcNum="+procNum.ToString();
			Procedure[] List=RefreshAndFill(command);
			if(List.Length==0){
				MessageBox.Show(Lan.g("Procedures","Error. Procedure not found")+": "+procNum.ToString());
				return new Procedure();
			}
			Procedure proc=List[0];
			if(!includeNote){
				return proc;
			}
			command="SELECT * FROM procnote WHERE ProcNum="+POut.PInt(procNum)+" ORDER BY EntryDateTime DESC ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assume MySQL
				command+="LIMIT 1";
			}
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return proc;
			}
			proc.UserNum   =PIn.PInt   (table.Rows[0]["UserNum"].ToString());
			proc.Note      =PIn.PString(table.Rows[0]["Note"].ToString());
			proc.SigIsTopaz=PIn.PBool  (table.Rows[0]["SigIsTopaz"].ToString());
			proc.Signature =PIn.PString(table.Rows[0]["Signature"].ToString());
			return proc;
		}

		private static Procedure[] RefreshAndFill(string command){
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return new Procedure[0];
			}
 			DataTable table=ds.Tables[0];
			return ConvertToList(table);
		}

		private static Procedure[] ConvertToList(DataTable table){
			Procedure[] List=new Procedure[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Procedure();
				List[i].ProcNum					= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum					= PIn.PInt   (table.Rows[i][1].ToString());
				List[i].AptNum					= PIn.PInt   (table.Rows[i][2].ToString());
				List[i].OldCode					= PIn.PString(table.Rows[i][3].ToString());
				List[i].ProcDate				= PIn.PDate  (table.Rows[i][4].ToString());
				List[i].ProcFee					= PIn.PDouble(table.Rows[i][5].ToString());
				List[i].Surf						= PIn.PString(table.Rows[i][6].ToString());
				List[i].ToothNum				= PIn.PString(table.Rows[i][7].ToString());
				List[i].ToothRange			= PIn.PString(table.Rows[i][8].ToString());
				List[i].Priority				= PIn.PInt   (table.Rows[i][9].ToString());
				List[i].ProcStatus			= (ProcStat)PIn.PInt   (table.Rows[i][10].ToString());
				List[i].ProvNum					= PIn.PInt   (table.Rows[i][11].ToString());
				List[i].Dx							= PIn.PInt   (table.Rows[i][12].ToString());
				List[i].PlannedAptNum		= PIn.PInt   (table.Rows[i][13].ToString());
				List[i].PlaceService		= (PlaceOfService)PIn.PInt(table.Rows[i][14].ToString());
				List[i].Prosthesis		  = PIn.PString(table.Rows[i][15].ToString());
				List[i].DateOriginalProsth= PIn.PDate(table.Rows[i][16].ToString());
				List[i].ClaimNote		    = PIn.PString(table.Rows[i][17].ToString());
				List[i].DateEntryC      = PIn.PDate  (table.Rows[i][18].ToString());
				List[i].ClinicNum       = PIn.PInt   (table.Rows[i][19].ToString());
				List[i].MedicalCode     = PIn.PString(table.Rows[i][20].ToString());
				List[i].DiagnosticCode  = PIn.PString(table.Rows[i][21].ToString());
				List[i].IsPrincDiag     = PIn.PBool  (table.Rows[i][22].ToString());
				List[i].ProcNumLab      = PIn.PInt   (table.Rows[i][23].ToString());	
				List[i].BillingTypeOne  = PIn.PInt   (table.Rows[i][24].ToString());
				List[i].BillingTypeTwo  = PIn.PInt   (table.Rows[i][25].ToString());
				List[i].CodeNum         = PIn.PInt   (table.Rows[i][26].ToString());
				List[i].CodeMod1        = PIn.PString(table.Rows[i][27].ToString());
				List[i].CodeMod2        = PIn.PString(table.Rows[i][28].ToString());
				List[i].CodeMod3        = PIn.PString(table.Rows[i][29].ToString());
				List[i].CodeMod4        = PIn.PString(table.Rows[i][30].ToString());
				List[i].RevCode         = PIn.PString(table.Rows[i][31].ToString());
				List[i].UnitCode         = PIn.PString(table.Rows[i][32].ToString());
				List[i].UnitQty        = PIn.PInt(table.Rows[i][33].ToString());
				List[i].BaseUnits       = PIn.PInt(table.Rows[i][34].ToString());
				List[i].StartTime       = PIn.PInt(table.Rows[i][35].ToString());
				List[i].StopTime        = PIn.PInt(table.Rows[i][36].ToString());
				//only used sometimes:
				/*if(table.Columns.Count>24){
					List[i].UserNum       = PIn.PInt   (table.Rows[i][24].ToString());
					List[i].Note          = PIn.PString(table.Rows[i][25].ToString());
					List[i].SigIsTopaz    = PIn.PBool  (table.Rows[i][26].ToString());
					List[i].Signature     = PIn.PString(table.Rows[i][27].ToString());
				}*/
			}
			return List;
		}

		///<summary>Gets Procedures for a single appointment directly from the database</summary>
		public static Procedure[] GetProcsForSingle(int aptNum, bool isPlanned){
			string command;
			if(isPlanned){
				command = "SELECT * from procedurelog WHERE PlannedAptNum = '"+POut.PInt(aptNum)+"'";
			}
			else{
				command = "SELECT * from procedurelog WHERE AptNum = '"+POut.PInt(aptNum)+"'";
			}
			return RefreshAndFill(command);
		}

		/// <summary>Used by GetProcsForSingle and GetProcsMultApts to generate a short string description of a procedure.</summary>
		public static string ConvertProcToString(int codeNum,string surf,string toothNum){
			string strLine="";
			ProcedureCode code=ProcedureCodes.GetProcCode(codeNum);
			switch (code.TreatArea){
				case TreatmentArea.Surf :
					strLine+="#"+Tooth.ToInternat(toothNum)+"-"+surf+"-";//""#12-MOD-"
					break;
				case TreatmentArea.Tooth :
					strLine+="#"+Tooth.ToInternat(toothNum)+"-";//"#12-"
					break;
				default ://area 3 or 0 (mouth)
					break;
				case TreatmentArea.Quad :
					strLine+=surf+"-";//"UL-"
					break;
				case TreatmentArea.Sextant :
					strLine+="S"+surf+"-";//"S2-"
					break;
				case TreatmentArea.Arch :
					strLine+=surf+"-";//"U-"
					break;
				case TreatmentArea.ToothRange :
					//strLine+=table.Rows[j][13].ToString()+" ";//don't show range
					break;
			}//end switch
			strLine+=code.AbbrDesc;
			return strLine;
		}

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum, string[], and production) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list.  This process requires only one call to the database. "myAptNums" is the list of appointments to get procedures for.</summary>
		public static Procedure[] GetProcsMultApts(int[] myAptNums){
			return GetProcsMultApts(myAptNums,false);
		}

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum, string[], and production) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list or GetProductionOneApt.  This process requires only one call to the database.  "myAptNums" is the list of appointments to get procedures for.  isForNext gets procedures for a list of next appointments rather than regular appointments.</summary>
		public static Procedure[] GetProcsMultApts(int[] myAptNums,bool isForPlanned){
			if(myAptNums.Length==0){
				return new Procedure[0];
			}
			string strAptNums="";
			for(int i=0;i<myAptNums.Length;i++){
				if(i>0){
					strAptNums+=" OR";
				}
				if(isForPlanned){
					strAptNums+=" PlannedAptNum='"+POut.PInt(myAptNums[i])+"'";
				}
				else{
					strAptNums+=" AptNum='"+POut.PInt(myAptNums[i])+"'";
				}
			}
			string command = "SELECT * FROM procedurelog WHERE"+strAptNums;
			return RefreshAndFill(command);
		}

		///<summary>Used do display procedure descriptions on appointments. The returned string also includes surf and toothNum.</summary>
		public static string GetDescription(Procedure proc){
			return ConvertProcToString(proc.CodeNum,proc.Surf,proc.ToothNum);
		}

		///<summary>Gets procedures for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static Procedure[] GetProcsOneApt(int myAptNum,Procedure[] procsMultApts){
			ArrayList AL=new ArrayList();
			for(int i=0;i<procsMultApts.Length;i++){
				if(procsMultApts[i].AptNum==myAptNum){
					AL.Add(procsMultApts[i].Copy());
				}
			}
			Procedure[] retVal=new Procedure[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Gets the production for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static double GetProductionOneApt(int myAptNum,Procedure[] procsMultApts,bool isPlanned){
			double retVal=0;
			for(int i=0;i<procsMultApts.Length;i++){
				if(isPlanned && procsMultApts[i].PlannedAptNum==myAptNum) {
					retVal+=procsMultApts[i].ProcFee;
				}
				if(!isPlanned && procsMultApts[i].AptNum==myAptNum) {
					retVal+=procsMultApts[i].ProcFee;
				}
			}
			return retVal;
		}

		///<summary>Used in FormClaimEdit,FormClaimPrint,FormClaimPayTotal, etc to get description of procedure. Procedure list needs to include the procedure we are looking for.</summary>
		public static Procedure GetProc(Procedure[] list, int procNum){
			for(int i=0;i<list.Length;i++){
				if(procNum==list[i].ProcNum){
					return list[i];
				}
			}
			MessageBox.Show("Error. Procedure not found");
			return new Procedure();
		}

		///<summary>Loops through each proc. Does not add notes to a procedure that already has notes. Used twice, security checked in both places before calling this.  Also sets provider for each proc.</summary>
		public static void SetCompleteInAppt(Appointment apt,InsPlan[] PlanList,PatPlan[] patPlans){
			Procedure[] ProcList=Procedures.Refresh(apt.PatNum);
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(apt.PatNum);
			Benefit[] benefitList=Benefits.Refresh(patPlans);
			//this query could be improved slightly to only get notes of interest.
			string command="SELECT * FROM procnote WHERE PatNum="+POut.PInt(apt.PatNum)+" ORDER BY EntryDateTime";
			DataTable rawNotes=General.GetTable(command);
			//CovPats.Refresh(PlanList,patPlans);
			//bool doResetRecallStatus=false;
			ProcedureCode procCode;
			Procedure oldProc;
			for(int i=0;i<ProcList.Length;i++){
				if(ProcList[i].AptNum!=apt.AptNum){
					continue;
				}
				//attach the note, if it exists.
				for(int n=rawNotes.Rows.Count-1;n>=0;n--) {//loop through each note, backwards.
					if(ProcList[i].ProcNum.ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
						continue;
					}
					ProcList[i].UserNum   =PIn.PInt(rawNotes.Rows[n]["UserNum"].ToString());
					ProcList[i].Note      =PIn.PString(rawNotes.Rows[n]["Note"].ToString());
					ProcList[i].SigIsTopaz=PIn.PBool(rawNotes.Rows[n]["SigIsTopaz"].ToString());
					ProcList[i].Signature =PIn.PString(rawNotes.Rows[n]["Signature"].ToString());
					break;//out of note loop.
				}
				oldProc=ProcList[i].Copy();
				procCode=ProcedureCodes.GetProcCode(ProcList[i].CodeNum);
				if(procCode.PaintType==ToothPaintingType.Extraction){//if an extraction, then mark previous procs hidden
					//SetHideGraphical(ProcList[i]);//might not matter anymore
					ToothInitials.SetValue(apt.PatNum,ProcList[i].ToothNum,ToothInitialType.Missing);
				}
				ProcList[i].ProcStatus=ProcStat.C;
				ProcList[i].ProcDate=apt.AptDateTime.Date;
				if(oldProc.ProcStatus!=ProcStat.C){
					ProcList[i].DateEntryC=DateTime.Now;//this triggers it to set to server time NOW().
				}
				ProcList[i].PlaceService=(PlaceOfService)PrefB.GetInt("DefaultProcedurePlaceService");
				ProcList[i].ClinicNum=apt.ClinicNum;
				ProcList[i].PlaceService=Clinics.GetPlaceService(apt.ClinicNum);
				if(apt.ProvHyg!=0){//if the appointment has a hygiene provider
					if(procCode.IsHygiene){//hyg proc
						ProcList[i].ProvNum=apt.ProvHyg;
					}
					else{//regular proc
						ProcList[i].ProvNum=apt.ProvNum;
					}
				}
				else{//same provider for every procedure
					ProcList[i].ProvNum=apt.ProvNum;
				}
				//if procedure was already complete, then don't add more notes.
				if(oldProc.ProcStatus!=ProcStat.C){
					ProcList[i].Note+=ProcCodeNotes.GetNote(ProcList[i].ProvNum,ProcList[i].CodeNum);
				}
				Update(ProcList[i],oldProc);
				ComputeEstimates(ProcList[i],apt.PatNum,ClaimProcList,false,PlanList,patPlans,benefitList);
			}
			//if(doResetRecallStatus){
			//	Recalls.Reset(apt.PatNum);//this also synchs recall
			//}
			Recalls.Synch(apt.PatNum);
		}

		///<Summary>Supply the list of procedures attached to the appointment.  It will loop through each and assign the correct provider.  Also sets clinic.</Summary>
		public static void SetProvidersInAppointment(Appointment apt,Procedure[] procList){
			ProcedureCode procCode;
			Procedure changedProc;
			for(int i=0;i<procList.Length;i++){
				changedProc=procList[i].Copy();
				if(apt.ProvHyg!=0) {//if the appointment has a hygiene provider
					procCode=ProcedureCodes.GetProcCode(procList[i].CodeNum);
					if(procCode.IsHygiene) {//hygiene proc
						changedProc.ProvNum=apt.ProvHyg;
					}
					else{//dentist proc
						changedProc.ProvNum=apt.ProvNum;
					}
				}
				else {//same provider for every procedure
					changedProc.ProvNum=apt.ProvNum;
				}
				changedProc.ClinicNum=apt.ClinicNum;
				Procedures.Update(changedProc,procList[i]);//won't go to db unless a field has changed.
			}
		}

		///<summary>Does not make any calls to db.</summary>
		public static double ComputeBal(Procedure[] List){
			double retVal=0;
			double procFee=0;
			double qty=0;
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcStatus==ProcStat.C) {//complete
					procFee=List[i].ProcFee;
					qty=PIn.PInt(List[i].UnitQty.ToString()) + PIn.PInt(List[i].BaseUnits.ToString());//handles 0 and blank
					if(qty > 0) {
						procFee*=qty;
					}
					retVal+=procFee;//List[i].ProcFee;
				}
			}
			return retVal;
		}

		///<summary>Sets the patient.DateFirstVisit if necessary. A visitDate is required to be passed in because it may not be today's date. This is triggered by:
		///1. When any procedure is inserted regardless of status. From Chart or appointment. If no C procs and date blank, changes date.
		///2. When updating a procedure to status C. If no C procs, update visit date. Ask user first?
		///  #2 was recently changed to only happen if date is blank or less than 7 days old.
		///3. When an appointment is deleted. If no C procs, clear visit date.
		///  #3 was recently changed to not occur at all unless appt is of type IsNewPatient
		///4. Changing an appt date of type IsNewPatient. If no C procs, change visit date.
		///Old: when setting a procedure complete in the Chart module or the ProcEdit window.  Also when saving an appointment that is marked IsNewPat.</summary>
		public static void SetDateFirstVisit(DateTime visitDate, int situation,Patient pat){
			if(situation==1){
				if(pat.DateFirstVisit.Year>1880){
					return;//a date has already been set.
				}
			}
			if(situation==2) {
				if(pat.DateFirstVisit.Year>1880 && pat.DateFirstVisit<DateTime.Now.AddDays(-7)) {
					return;//a date has already been set.
				}
			}
			string command="SELECT Count(*) from procedurelog WHERE "
				+"PatNum = '"+POut.PInt(pat.PatNum)+"' "
				+"AND ProcStatus = '2'";
 			DataTable table=General.GetTable(command);
			if(PIn.PInt(table.Rows[0][0].ToString())>0){
				return;//there are already completed procs (for all situations)
			}
			if(situation==2){
				//ask user first?
			}
			if(situation==3){
				command="UPDATE patient SET DateFirstVisit ='0001-01-01'"
					+" WHERE PatNum ='"
					+POut.PInt(pat.PatNum)+"'";
			}
			else{
				command="UPDATE patient SET DateFirstVisit ="
					+POut.PDate(visitDate)+" WHERE PatNum ='"
					+POut.PInt(pat.PatNum)+"'";
			}
			//MessageBox.Show(cmd.CommandText);
			//dcon.NonQ(command);
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					GeneralB.NonQ(command);
				}
				else {
					DtoGeneralNonQ dto=new DtoGeneralNonQ();
					dto.Command=command;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		///<summary>Used in FormClaimProc to get the codeNum for a procedure. Do not use this if accessing FormClaimProc from the ProcEdit window, because proc might not be updated to db yet.</summary>
		public static int GetCodeNum(int procNum){
			string command="SELECT CodeNum FROM procedurelog WHERE ProcNum='"+procNum.ToString()+"'";
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.PInt(table.Rows[0][0].ToString());
		}

		///<summary>Used in FormClaimProc to get the fee for a procedure directly from the db.  Do not use this if accessing FormClaimProc from the ProcEdit window, because proc might not be updated to db yet.</summary>
		public static double GetProcFee(int procNum){
			string command="SELECT ProcFee FROM procedurelog WHERE ProcNum='"+procNum.ToString()+"'";
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.PDouble(table.Rows[0][0].ToString());
		}

		///<summary>Used once in FormClaimProc.</summary>
		public static string GetToothNum(int procNum){
			string command="SELECT ToothNum FROM procedurelog WHERE ProcNum="+POut.PInt(procNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0) {
				return "";
			}
			return PIn.PString(table.Rows[0][0].ToString());
		}

		///<summary>After changing important coverage plan info, this is called to recompute estimates for all procedures for this patient.</summary>
		public static void ComputeEstimatesForAll(int patNum,ClaimProc[] claimProcs,Procedure[] procs,InsPlan[] PlanList,PatPlan[] patPlans,Benefit[] benefitList)
		{
			for(int i=0;i<procs.Length;i++){
				ComputeEstimates(procs[i],patNum,claimProcs,false,PlanList,patPlans,benefitList);
			}
		}

		///<summary>Called from FormApptsOther when creating a new appointment.  Returns true if there are any procedures marked complete for this patient.  The result is that the NewPt box on the appointment won't be checked.</summary>
		public static bool AreAnyComplete(int patNum){
			string command="SELECT COUNT(*) FROM procedurelog "
				+"WHERE PatNum="+patNum.ToString()
				+" AND ProcStatus=2";
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
			if(table.Rows[0][0].ToString()=="0"){
				return false;
			}
			else return true;
		}

		///<summary>Called from AutoCodeItems.  Makes a call to the database to determine whether the specified tooth has been extracted or will be extracted. This could then trigger a pontic code.</summary>
		public static bool WillBeMissing(string toothNum,int patNum){
			//first, check for missing teeth
			string command="SELECT COUNT(*) FROM toothinitial "
				+"WHERE ToothNum='"+toothNum+"' "
				+"AND PatNum="+POut.PInt(patNum)
				+" AND InitialType=0";//missing
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
			if(table.Rows[0][0].ToString()!="0"){
				return true;
			}
			//then, check for a planned extraction
			command="SELECT COUNT(*) FROM procedurelog,procedurecode "
				+"WHERE procedurelog.CodeNum=procedurecode.CodeNum "
				+"AND procedurelog.ToothNum='"+toothNum+"' "
				+"AND procedurelog.PatNum="+patNum.ToString()
				+" AND procedurecode.PaintType=1";//extraction
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			table=ds.Tables[0];
			if(table.Rows[0][0].ToString()!="0"){
				return true;
			}
			return false;
		}

		///<summary>Used from TP to get a list of all TP procs, ordered by priority, toothnum.</summary>
		public static Procedure[] GetListTP(Procedure[] procList){
			ArrayList AL=new ArrayList();
			for(int i=0;i<procList.Length;i++){
				if(procList[i].ProcStatus==ProcStat.TP){
					AL.Add(procList[i]);
				}
			}
			IComparer myComparer=new ProcedureComparer();
			AL.Sort(myComparer);
			Procedure[] retVal=new Procedure[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Gets a list of procedures representing extracted teeth.  Status of C,EC,orEO. Includes procs with toothNum "1"-"32".  Will not include procs with unreasonable dates.  Used for Canadian e-claims instead of the usual ToothInitials.GetMissingOrHiddenTeeth, because Canada requires dates on the extracted teeth.  Supply all procedures for the patient.</summary>
		public static List<Procedure> GetExtractedTeeth(Procedure[] procList) {
			List<Procedure> extracted=new List<Procedure>();
			ProcedureCode procCode;
			for(int i=0;i<procList.Length;i++) {
				if(procList[i].ProcStatus!=ProcStat.C && procList[i].ProcStatus!=ProcStat.EC && procList[i].ProcStatus!=ProcStat.EO){
					continue;
				}
				if(!Tooth.IsValidDB(procList[i].ToothNum)){
					continue;
				}
				if(Tooth.IsSuperNum(procList[i].ToothNum)){
					continue;
				}
				if(Tooth.IsPrimary(procList[i].ToothNum)){
					continue;
				}
				if(procList[i].ProcDate.Year<1880 || procList[i].ProcDate>DateTime.Today){
					continue;
				}
				procCode=ProcedureCodes.GetProcCode(procList[i].CodeNum);
				if(procCode.TreatArea!=TreatmentArea.Tooth){
					continue;
				}
				if(procCode.PaintType!=ToothPaintingType.Extraction){
					continue;
				}
				extracted.Add(procList[i].Copy());
			}
			return extracted;
		}




		//--------------------Taken from Procedure class--------------------------------------------------




		
		///<summary>Base estimate or override is retrieved from supplied claimprocs. Does not take into consideration annual max or deductible.  If limitToTotal set to true, then it does limit total of pri+sec to not be more than total fee.  The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public static double GetEst(Procedure proc,ClaimProc[] claimProcs,PriSecTot pst,PatPlan[] patPlans,bool limitToTotal) {
			double priBaseEst=0;
			double secBaseEst=0;
			double priOverride=-1;
			double secOverride=-1;
			for(int i=0;i<claimProcs.Length;i++) {
				//adjustments automatically ignored since no ProcNum
				if(claimProcs[i].Status==ClaimProcStatus.CapClaim
					|| claimProcs[i].Status==ClaimProcStatus.Preauth
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental) {
					continue;
				}
				if(claimProcs[i].ProcNum==proc.ProcNum) {
					if(PatPlans.GetPlanNum(patPlans,1)==claimProcs[i].PlanNum) {
						//if this is a Cap, then this will still work. Est comes out 0.
						priBaseEst=claimProcs[i].BaseEst;
						priOverride=claimProcs[i].OverrideInsEst;
					}
					else if(PatPlans.GetPlanNum(patPlans,2)==claimProcs[i].PlanNum) {
						secBaseEst=claimProcs[i].BaseEst;
						secOverride=claimProcs[i].OverrideInsEst;
					}
				}
			}
			if(priOverride!=-1) {
				priBaseEst=priOverride;
			}
			if(secOverride!=-1) {
				secBaseEst=secOverride;
			}
			if(limitToTotal && proc.ProcFee-priBaseEst-secBaseEst < 0) {
				secBaseEst=proc.ProcFee-priBaseEst;
			}
			switch(pst) {
				case PriSecTot.Pri:
					return priBaseEst;
				case PriSecTot.Sec:
					return secBaseEst;
				case PriSecTot.Tot:
					return priBaseEst+secBaseEst;
			}
			return 0;
		}

		/*
		///<summary>Gets allowedOverride for this procedure based on supplied claimprocs. Includes all claimproc types.  Only used in main TP module when calculating PPOs. The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public static double GetAllowedOverride(Procedure proc,ClaimProc[] claimProcs,int priPlanNum) {
			//double retVal=0;
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum==proc.ProcNum && claimProcs[i].PlanNum==priPlanNum) {
					return claimProcs[i].AllowedOverride;
					//retVal+=claimProcs[i].WriteOff;
				}
			}
			return 0;//retVal;
		}*/

		///<summary>Gets total writeoff for this procedure based on supplied claimprocs. Includes all claimproc types.  Only used in main TP module. The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public static double GetWriteOff(Procedure proc,ClaimProc[] claimProcs) {
			double retVal=0;
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum==proc.ProcNum) {
					retVal+=claimProcs[i].WriteOff;
				}
			}
			return retVal;
		}

		///<summary>WriteOff'Complete'. Only used in main Account module. Gets writeoff for this procedure based on supplied claimprocs. Only includes claimprocs with status of CapComplete,CapClaim,NotReceived,Received,or Supplemental. Used to ONLY include Writeoffs not attached to claims, because those would display on the claim line, but now they show on each procedure instead.  /*In practice, this means only writeoffs with CapComplete status get returned because they are to be subtracted from the patient portion on the proc line*/. The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public static double GetWriteOffC(Procedure proc,ClaimProc[] claimProcs) {
			double retVal=0;
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				//if(claimProcs[i].ClaimNum>0) {
				//	continue;
				//}
				if(
					//adj skipped
					claimProcs[i].Status==ClaimProcStatus.CapClaim
					|| claimProcs[i].Status==ClaimProcStatus.CapComplete
					//capEstimate would never happen because procedure is C.
					//estimate means not attached to claim, so don't count
					|| claimProcs[i].Status==ClaimProcStatus.NotReceived
					//preAuth -no
					|| claimProcs[i].Status==ClaimProcStatus.Received
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental
					) {
					retVal+=claimProcs[i].WriteOff;
				}
			}
			return retVal;
		}

		///<summary>Used whenever a procedure changes or a plan changes.  All estimates for a given procedure must be updated. This frequently includes adding claimprocs, but can also just edit the appropriate existing claimprocs. Skips status=Adjustment,CapClaim,Preauth,Supplemental.  Also fixes date,status,and provnum if appropriate.  The claimProc array can be all claimProcs for the patient, but must at least include all claimprocs for this proc.  Only set IsInitialEntry true from Chart module; this is for cap procs.</summary>
		public static void ComputeEstimates(Procedure proc,int patNum,ClaimProc[] claimProcs,bool IsInitialEntry,InsPlan[] PlanList,PatPlan[] patPlans,Benefit[] benefitList) {
			bool doCreate=true;
			if(proc.ProcDate<DateTime.Today && proc.ProcStatus==ProcStat.C) {
				//don't automatically create an estimate for completed procedures
				//especially if they are older than today
				//Very important after a conversion from another software.
				//This may need to be relaxed a little for offices that enter treatment a few days after it's done.
				doCreate=false;
			}
			//first test to see if each estimate matches an existing patPlan (current coverage),
			//delete any other estimates
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				if(claimProcs[i].PlanNum==0) {
					continue;
				}
				if(claimProcs[i].Status==ClaimProcStatus.CapClaim
					|| claimProcs[i].Status==ClaimProcStatus.Preauth
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental) {
					continue;
					//ignored: adjustment
					//included: capComplete,CapEstimate,Estimate,NotReceived,Received
				}
				if(claimProcs[i].Status!=ClaimProcStatus.Estimate && claimProcs[i].Status!=ClaimProcStatus.CapEstimate) {
					continue;
				}
				bool planIsCurrent=false;
				for(int p=0;p<patPlans.Length;p++) {
					if(patPlans[p].PlanNum==claimProcs[i].PlanNum) {
						planIsCurrent=true;
						break;
					}
				}
				//If claimProc estimate is for a plan that is not current, delete it
				if(!planIsCurrent) {
					ClaimProcs.Delete(claimProcs[i]);
				}
			}
			InsPlan PlanCur;
			bool estExists;
			bool cpAdded=false;
			//loop through all patPlans (current coverage), and add any missing estimates
			for(int p=0;p<patPlans.Length;p++) {//typically, loop will only have length of 1 or 2
				if(!doCreate) {
					break;
				}
				//test to see if estimate exists
				estExists=false;
				for(int i=0;i<claimProcs.Length;i++) {
					if(claimProcs[i].ProcNum!=proc.ProcNum) {
						continue;
					}
					if(claimProcs[i].PlanNum==0) {
						continue;
					}
					if(claimProcs[i].Status==ClaimProcStatus.CapClaim
						|| claimProcs[i].Status==ClaimProcStatus.Preauth
						|| claimProcs[i].Status==ClaimProcStatus.Supplemental) {
						continue;
						//ignored: adjustment
						//included: capComplete,CapEstimate,Estimate,NotReceived,Received
					}
					if(patPlans[p].PlanNum!=claimProcs[i].PlanNum) {
						continue;
					}
					estExists=true;
					break;
				}
				if(estExists) {
					continue;
				}
				//estimate is missing, so add it.
				ClaimProc cp=new ClaimProc();
				cp.ProcNum=proc.ProcNum;
				cp.PatNum=patNum;
				cp.ProvNum=proc.ProvNum;
				PlanCur=InsPlans.GetPlan(patPlans[p].PlanNum,PlanList);
				if(PlanCur==null) {
					continue;//??
				}
				if(PlanCur.PlanType=="c") {
					if(proc.ProcStatus==ProcStat.C) {
						cp.Status=ClaimProcStatus.CapComplete;
					}
					else {
						cp.Status=ClaimProcStatus.CapEstimate;//this may be changed below
					}
				}
				else {
					cp.Status=ClaimProcStatus.Estimate;
				}
				cp.PlanNum=PlanCur.PlanNum;
				cp.DateCP=proc.ProcDate;
				cp.AllowedOverride=-1;
				cp.PercentOverride=-1;
				cp.OverrideInsEst=-1;
				cp.NoBillIns=ProcedureCodes.GetProcCode(proc.CodeNum).NoBillIns;
				cp.OverAnnualMax=-1;
				cp.PaidOtherIns=-1;
				cp.CopayOverride=-1;
				cp.ProcDate=proc.ProcDate;
				//ComputeBaseEst will fill AllowedOverride,Percentage,CopayAmt,BaseEst
				ClaimProcs.Insert(cp);
				cpAdded=true;
			}
			//if any were added, refresh the list
			if(cpAdded) {
				claimProcs=ClaimProcs.Refresh(patNum);
			}
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				claimProcs[i].DateCP=proc.ProcDate;//dates MUST match, but I can't remember why. Claims?
				claimProcs[i].ProcDate=proc.ProcDate;
				//capitation estimates are always forced to follow the status of the procedure
				PlanCur=InsPlans.GetPlan(claimProcs[i].PlanNum,PlanList);
				if(PlanCur!=null
					&& PlanCur.PlanType=="c"
					&& (claimProcs[i].Status==ClaimProcStatus.CapComplete
					|| claimProcs[i].Status==ClaimProcStatus.CapEstimate)) {
					if(IsInitialEntry) {
						//this will be switched to CapComplete further down if applicable.
						//This makes ComputeBaseEst work properly on new cap procs w status Complete
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
					else if(proc.ProcStatus==ProcStat.C) {
						claimProcs[i].Status=ClaimProcStatus.CapComplete;
					}
					else {
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
				}
				//ignored: adjustment
				//ComputeBaseEst automatically skips: capComplete,Preauth,capClaim,Supplemental
				//does recalc est on: CapEstimate,Estimate,NotReceived,Received
				if(claimProcs[i].PlanNum>0 && PatPlans.GetPlanNum(patPlans,1)==claimProcs[i].PlanNum) {
					ClaimProcs.ComputeBaseEst(claimProcs[i],proc,PriSecTot.Pri,PlanList,patPlans,benefitList);
				}
				if(claimProcs[i].PlanNum>0 && PatPlans.GetPlanNum(patPlans,2)==claimProcs[i].PlanNum) {
					ClaimProcs.ComputeBaseEst(claimProcs[i],proc,PriSecTot.Sec,PlanList,patPlans,benefitList);
				}
				if(IsInitialEntry
					&& claimProcs[i].Status==ClaimProcStatus.CapEstimate
					&& proc.ProcStatus==ProcStat.C) {
					claimProcs[i].Status=ClaimProcStatus.CapComplete;
				}
				//prov only updated if still an estimate
				if(claimProcs[i].Status==ClaimProcStatus.Estimate
					|| claimProcs[i].Status==ClaimProcStatus.CapEstimate) {
					claimProcs[i].ProvNum=proc.ProvNum;
				}
				ClaimProcs.Update(claimProcs[i]);
			}
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs at all are attached to this procedure.</summary>
		public static bool IsCoveredIns(Procedure proc,ClaimProc[] List) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum) {
					return true;
				}
			}
			return false;
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are set NoBillIns.</summary>
		public static bool NoBillIns(Procedure proc,ClaimProc[] List) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum
					&& List[i].NoBillIns) {
					return true;
				}
			}
			return false;
		}

		///<summary>Used in ContrAccount.CreateClaim when validating selected procedures. Returns true if there is any claimproc for this procedure and plan which is marked NoBillIns.  The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are set NoBillIns.</summary>
		public static bool NoBillIns(Procedure proc,ClaimProc[] List,int planNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum
					&& List[i].PlanNum==planNum
					&& List[i].NoBillIns) {
					return true;
				}
			}
			return false;
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are status estimate, which means they haven't been attached to a claim because their status would have been changed to NotReceived.  And if the patient doesn't have ins, then the estimates would have been deleted.</summary>
		public static bool IsUnsent(Procedure proc,ClaimProc[] List) {
			//unsent if no claimprocs with claimNums
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum
					&& List[i].Status==ClaimProcStatus.Estimate
					//&& List[i].ClaimNum>0
					//&& List[i].Status!=ClaimProcStatus.Preauth
					) {
					return true;
				}
			}
			return false;
		}

		///<summary>Only called from FormProcEdit to signal when to disable much of the editing in that form. If the procedure is 'AttachedToClaim' then user should not change it very much.  The claimProcList can be all claimProcs for the patient or only those attached to this proc.</summary>
		public static bool IsAttachedToClaim(Procedure proc,ClaimProc[] List) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum
					&& List[i].ClaimNum>0
					&& (List[i].Status==ClaimProcStatus.CapClaim
					|| List[i].Status==ClaimProcStatus.NotReceived
					|| List[i].Status==ClaimProcStatus.Preauth
					|| List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental
					)) {
					return true;
				}
			}
			return false;
		}

		///<summary>Used in ContrAccount.CreateClaim to validate that procedure is not already attached to a claim for this specific insPlan.  The claimProcList can be all claimProcs for the patient or only those attached to this proc.</summary>
		public static bool IsAlreadyAttachedToClaim(Procedure proc,ClaimProc[] List,int planNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum
					&& List[i].PlanNum==planNum
					&& List[i].ClaimNum>0
					&& List[i].Status!=ClaimProcStatus.Preauth) {
					return true;
				}
			}
			return false;
		}

		///<summary>Only used in ContrAccount.OnInsClick to automate selection of procedures.  Returns true if this procedure should be selected.  This happens if there is at least one claimproc attached for this plan that is an estimate, and it is not set to NoBillIns.  The list can be all ClaimProcs for patient, or just those for this procedure. The plan is the primary plan.</summary>
		public static bool NeedsSent(Procedure proc,ClaimProc[] List,int planNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum
					&& !List[i].NoBillIns
					&& List[i].PlanNum==planNum
					&& List[i].Status==ClaimProcStatus.Estimate) {
					return true;
				}
			}
			return false;
		}

		///<summary>Only used in ContrAccount.CreateClaim to decide whether a given procedure has an estimate that can be used to attach to a claim for the specified plan.  Returns a valid claimProc if this procedure has an estimate attached that is not set to NoBillIns.  The list can be all ClaimProcs for patient, or just those for this procedure. Returns null if there are no claimprocs that would work.</summary>
		public static ClaimProc GetClaimProcEstimate(Procedure proc,ClaimProc[] List,InsPlan plan) {
			//bool matchOfWrongType=false;
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum
					&& !List[i].NoBillIns
					&& List[i].PlanNum==plan.PlanNum) {
					if(plan.PlanType=="c") {
						if(List[i].Status==ClaimProcStatus.CapComplete)
							return List[i];
					}
					else {//any type except capitation
						if(List[i].Status==ClaimProcStatus.Estimate)
							return List[i];
					}
				}
			}
			return null;
		}

		///<summary>Only fees, not estimates.  Returns number of fees changed.</summary>
		public static int GlobalUpdateFees(){
			string command=@"SELECT procedurecode.CodeNum,ProcNum,patient.PatNum,procedurelog.PatNum,
				insplan.FeeSched AS PlanFeeSched,patient.FeeSched AS PatFeeSched,patient.PriProv,
				procedurelog.ProcFee
				FROM procedurelog
				LEFT JOIN patient ON patient.PatNum=procedurelog.PatNum
				LEFT JOIN patplan ON patplan.PatNum=procedurelog.PatNum
				AND patplan.Ordinal=1
				LEFT JOIN procedurecode ON procedurecode.CodeNum=procedurelog.CodeNum
				LEFT JOIN insplan ON insplan.PlanNum=patplan.PlanNum
				WHERE procedurelog.ProcStatus=1";
/*@"SELECT procedurelog.ProcCode,insplan.FeeSched AS PlanFeeSched,patient.FeeSched AS PatFeeSched,
				patient.PriProv,ProcNum
				FROM procedurelog,patient
				LEFT JOIN patplan ON patplan.PatNum=procedurelog.PatNum
				AND patplan.Ordinal=1
				LEFT JOIN insplan ON insplan.PlanNum=patplan.PlanNum
				WHERE procedurelog.ProcStatus=1
				AND patient.PatNum=procedurelog.PatNum
			";*/
			DataTable table=General.GetTable(command);
			int priPlanFeeSched;
			int feeSchedNum;
			int patFeeSched;
			int patProv;
			double newFee;
			double oldFee;
			int rowsChanged=0;
			for(int i=0;i<table.Rows.Count;i++){
				priPlanFeeSched=PIn.PInt(table.Rows[i]["PlanFeeSched"].ToString());
				patFeeSched=PIn.PInt(table.Rows[i]["PatFeeSched"].ToString());
				patProv=PIn.PInt(table.Rows[i]["PriProv"].ToString());
				feeSchedNum=Fees.GetFeeSched(priPlanFeeSched,patFeeSched,patProv);
				newFee=Fees.GetAmount0(PIn.PInt(table.Rows[i]["CodeNum"].ToString()),feeSchedNum);
				oldFee=PIn.PDouble(table.Rows[i]["ProcFee"].ToString());
				if(newFee==oldFee){
					continue;
				}
				command="UPDATE procedurelog SET ProcFee='"+POut.PDouble(newFee)+"' "
					+"WHERE ProcNum="+table.Rows[i]["ProcNum"].ToString();
				rowsChanged+=General.NonQ(command);
			}
			return rowsChanged;
		}

		


	}

	/*================================================================================================================
	=========================================== class ProcedureComparer =============================================*/

	///<summary>This sorts procedures based on priority, then tooth number, then code (but if Canadian lab code, uses proc code here instead of lab code).  Finally, if comparing a proc and its Canadian lab code, it puts the lab code after the proc.  It does not care about dates or status.  Currently used in TP module and Chart module sorting.</summary>
	public class ProcedureComparer:IComparer {
		///<summary>This sorts procedures based on priority, then tooth number.  It does not care about dates or status.  Currently used in TP module and Chart module sorting.</summary>
		int IComparer.Compare(Object objx,Object objy) {
			Procedure x=(Procedure)objx;
			Procedure y=(Procedure)objy;
			//first, by priority
			if(x.Priority!=y.Priority) {//if priorities are different
				if(x.Priority==0){
					return 1;//x is greater than y. Priorities always come first.
				}
				if(y.Priority==0){
					return -1;//x is less than y. Priorities always come first.
				}
				return DefB.GetOrder(DefCat.TxPriorities,x.Priority).CompareTo(DefB.GetOrder(DefCat.TxPriorities,y.Priority));
			}
			//priorities are the same, so sort by toothrange
			if(x.ToothRange != y.ToothRange){
				//empty toothranges come before filled toothrange values
				return x.ToothRange.CompareTo(y.ToothRange);
			}
			//toothranges are the same (usually empty), so compare toothnumbers
			if(x.ToothNum != y.ToothNum){
				//this also puts invalid or empty toothnumbers before the others.
				return Tooth.ToInt(x.ToothNum).CompareTo(Tooth.ToInt(y.ToothNum));
			}
			//priority and toothnums are the same, so sort by code.
			/*string adaX=x.Code;
			if(x.ProcNumLab !=0){//if x is a Canadian lab proc
				//then use the Code of the procedure instead of the lab code
				adaX=Procedures.GetOneProc(
			}
			string adaY=y.Code;*/
			return ProcedureCodes.GetStringProcCode(x.CodeNum).CompareTo(ProcedureCodes.GetStringProcCode(y.CodeNum));
			//return x.Code.CompareTo(y.Code);
			//return 0;//priority, tooth number, and code are all the same
		}

	}

	

	


}










