using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDentBusiness.UI;

namespace OpenDental{
	public class AppointmentL {
		///<summary>The date currently selected in the appointment module.</summary>
		public static DateTime DateSelected;

		///<summary>copy of function above for testing purposes.</summary>
		public static List<DateTime> GetSearchResults(long aptNum,DateTime afterDate,List<long> providerNums,int resultCount,TimeSpan beforeTime,TimeSpan afterTime) {
			if(beforeTime==TimeSpan.FromSeconds(0)) {//if they didn't set a before time, set it to a large timespan so that we can use the same logic for checking appointment times.
				beforeTime=TimeSpan.FromHours(25);//bigger than any time of day.
			}
			SearchBehaviorCriteria SearchType = (SearchBehaviorCriteria)PrefC.GetInt(PrefName.AppointmentSearchBehavior);
			List<DateTime> retVal= new List<DateTime>();
			DateTime dayEvaluating=afterDate.AddDays(1);
			Appointment appointmentToAdd=Appointments.GetOneApt(aptNum);
			List<DateTime> potentialProvAppointmentTime;
			List<DateTime> potentialOpAppointmentTime; 
			List<Operatory> opsListAll = OperatoryC.Listt;//all operatory Numbers
			List<Schedule> scheduleListAll = Schedules.GetTwoYearPeriod(dayEvaluating);// Schedules for the given day.
			List<Appointment> appointmentListAll = Appointments.GetForPeriodList(dayEvaluating,dayEvaluating.AddYears(2));
			List<ScheduleOp> schedOpListAll = ScheduleOps.GetForSchedList(scheduleListAll);
			List<ApptSearchProviderSchedule> provScheds = new List<ApptSearchProviderSchedule>();//Provider Bar, ProviderSched Bar, Date and Provider
			List<ApptSearchOperatorySchedule> operatrorySchedules = new List<ApptSearchOperatorySchedule>();//filtered based on SearchType
			List<long> operatoryNums = new List<long>();//more usefull than a list of operatories.
			for(int i=0;i<opsListAll.Count;i++) {
				operatoryNums.Add(opsListAll[i].OperatoryNum);
			}
			while(retVal.Count < resultCount && dayEvaluating < afterDate.AddYears(2)) {
				potentialOpAppointmentTime = new List<DateTime>();//clear or create
				//Providers-------------------------------------------------------------------------------------------------------------------------------------
				potentialProvAppointmentTime = new List<DateTime>();//clear or create
				provScheds = GetForProvidersAndDate(providerNums,dayEvaluating,scheduleListAll,appointmentListAll);
				for(int i=0;i<provScheds.Count;i++) {
					for(int j=0;j<288;j++) {//search every 5 minute increment per day
						if(j+appointmentToAdd.Pattern.Length>288) {
							break;
						}
						if(potentialProvAppointmentTime.Contains(dayEvaluating.AddMinutes(j*5))) {
							continue;
						}
						bool addDateTime=true;
						for(int k=0;k<appointmentToAdd.Pattern.Length;k++) {
							if((provScheds[i].ProvBar[j+k]==false && appointmentToAdd.Pattern[k]=='X') || provScheds[i].ProvSchedule[j+k]==false) {
								addDateTime=false;
								break;
							}
						}
						if(addDateTime) {
							potentialProvAppointmentTime.Add(dayEvaluating.AddMinutes(j*5));
						}
					}
				}
				if(SearchType==SearchBehaviorCriteria.ProviderTimeOperatory) {//Handle Operatories here----------------------------------------------------------------------------
					operatrorySchedules = GetAllForDate(dayEvaluating,scheduleListAll,appointmentListAll,schedOpListAll,operatoryNums,providerNums);
					potentialOpAppointmentTime = new List<DateTime>();//create or clear
					//for(int j=0;j<operatrorySchedules.Count;j++) {//for each operatory 
					for(int i=0;i<288;i++) {//search every 5 minute increment per day
						if(i+appointmentToAdd.Pattern.Length>288) {//skip if appointment would span across midnight
							break;
						}
						for(int j=0;j<operatrorySchedules.Count;j++) {//for each operatory 
							//if(potentialOpAppointmentTime.Contains(dayEvaluating.AddMinutes(i*5))) {//skip if we already have this dateTime
							//  break;
							//}
							bool addDateTime=true;
							for(int k=0;k<appointmentToAdd.Pattern.Length;k++) {//check appointment against operatories
								if(operatrorySchedules[j].OperatorySched[i+k]==false) {
									addDateTime=false;
									break;
								}
							}
							if(!addDateTime){
								break;
							}
							if(addDateTime){// && SearchType==SearchBehaviorCriteria.ProviderTimeOperatory) {//check appointment against providers available for the given operatory
								bool provAvail=false;
								for(int k=0;k<providerNums.Count;k++) {
									if(!operatrorySchedules[j].ProviderNums.Contains(providerNums[k])) {
										continue;
									}
									provAvail=true;
									for(int m=0;m<appointmentToAdd.Pattern.Length;m++) {
										if((provScheds[k].ProvBar[i+m]==false && appointmentToAdd.Pattern[m]=='X') || provScheds[k].ProvSchedule[i+m]==false) {//if provider bar time slot
											provAvail=false;
											break;
										}
									}
									if(provAvail) {//found a provider with an available operatory
										break;
									}
								}
								if(provAvail && addDateTime) {//operatory and provider are available
									potentialOpAppointmentTime.Add(dayEvaluating.AddMinutes(i*5));
								}
							}
							else {//not using SearchBehaviorCriteria.ProviderTimeOperatory
								if(addDateTime) {
									potentialOpAppointmentTime.Add(dayEvaluating.AddMinutes(i*5));
								}
							}
						}
					}
				}
				//At this point the potentialOpAppointmentTime is already filtered and only contains appointment times that match both provider time and operatory time. 
				switch(SearchType) {
					case SearchBehaviorCriteria.ProviderTime:
						//Add based on provider bars
						for(int i=0;i<potentialProvAppointmentTime.Count;i++) {
							if(potentialProvAppointmentTime[i].TimeOfDay>beforeTime || potentialProvAppointmentTime[i].TimeOfDay<afterTime) {
								continue;
							}
							retVal.Add(potentialProvAppointmentTime[i]);//add one for this day
							break;//stop looking through potential times for today.
						}
						break;
					case SearchBehaviorCriteria.ProviderTimeOperatory:
						//add based on provider bar and operatory bar
						for(int i=0;i<potentialOpAppointmentTime.Count;i++) {
							if(potentialOpAppointmentTime[i].TimeOfDay>beforeTime || potentialOpAppointmentTime[i].TimeOfDay<afterTime) {
								continue;
							}
							retVal.Add(potentialOpAppointmentTime[i]);//add one for this day
							break;//stop looking through potential times for today.
						}
						break;
				}
				dayEvaluating=dayEvaluating.AddDays(1);
			}
			return retVal;
		}

		///<summary>Uses the input parameters to construct a List&lt;ProviderSchedule&gt;. It is written to reduce the number of queries to the database.</summary>
		/// <param name="ProviderNums">PrimaryKeys to Provider.</param>
		/// <param name="ScheduleDate">The date to construct the schedule for.</param>
		/// <param name="ScheduleList">A List of Schedules containing all of the schedules for the given day, or possibly more. 
		/// Intended to be all schedules between search start date and search start date plus 2 years. This is to reduce queries to DB.</param>
		/// <param name="AppointmentList">A List of Appointments containing all of the schedules for the given day, or possibly more. 
		/// Intended to be all Appointments between search start date and search start date plus 2 years. This is to reduce queries to DB.</param>
		/// <param name="ScheduleOpList">A List of all ScheduleOps . 
		/// Intended to be all Appointments between search start date and search start date plus 2 years. This is to reduce queries to DB.</param>
		/// <param name="OperatoryList">A List of all operatories. This is to reduce queries to DB.</param> 
		private static List<ApptSearchProviderSchedule> GetForProvidersAndDate(List<long> ProviderNums,DateTime ScheduleDate,List<Schedule> ScheduleList,List<Appointment> AppointmentList) {//Not working properly when scheduled but no ops are set.
			List<ApptSearchProviderSchedule> retVal=new List<ApptSearchProviderSchedule>();
			ScheduleDate=ScheduleDate.Date;
			for(int i=0;i<ProviderNums.Count;i++) {
				retVal.Add(new ApptSearchProviderSchedule());
				retVal[i].ProviderNum=ProviderNums[i];
				retVal[i].SchedDate=ScheduleDate;
			}
			for(int s=0;s<ScheduleList.Count;s++) {
				if(ScheduleList[s].SchedDate.Date!=ScheduleDate) {//ignore schedules for different dates.
					continue;
				}
				if(ProviderNums.Contains(ScheduleList[s].ProvNum)) {//schedule applies to one of the selected providers
					int indexOfProvider = ProviderNums.IndexOf(ScheduleList[s].ProvNum);//cache the provider index
					int scheduleStartBlock = (int)ScheduleList[s].StartTime.TotalMinutes/5;//cache the start time of the schedule
					int scheduleLengthInBlocks = (int)(ScheduleList[s].StopTime-ScheduleList[s].StartTime).TotalMinutes/5;//cache the length of the schedule
					for(int i=0;i<scheduleLengthInBlocks;i++) {
						retVal[indexOfProvider].ProvBar[scheduleStartBlock+i]=true;//provider may have an appointment here
						retVal[indexOfProvider].ProvSchedule[scheduleStartBlock+i]=true;//provider is scheduled today
					}
				}
			}
			for(int a=0;a<AppointmentList.Count;a++) {
				if(AppointmentList[a].AptDateTime.Date!=ScheduleDate) {
					continue;
				}
				if(!AppointmentList[a].IsHygiene && ProviderNums.Contains(AppointmentList[a].ProvNum)) {//Not hygiene Modify provider bar based on ProvNum
					int indexOfProvider = ProviderNums.IndexOf(AppointmentList[a].ProvNum);
					int appointmentCurStartBlock = (int)AppointmentList[a].AptDateTime.TimeOfDay.TotalMinutes/5;
					for(int i=0;i<AppointmentList[a].Pattern.Length;i++) {
						if(AppointmentList[a].Pattern[i]=='X') {
							retVal[indexOfProvider].ProvBar[appointmentCurStartBlock+i]=false;
						}
					}
				}
				else if(AppointmentList[a].IsHygiene && ProviderNums.Contains(AppointmentList[a].ProvHyg)) {//Modify provider bar based on ProvHyg
					int indexOfProvider = ProviderNums.IndexOf(AppointmentList[a].ProvHyg);
					int appointmentStartBlock = (int)AppointmentList[a].AptDateTime.TimeOfDay.TotalMinutes/5;
					for(int i=0;i<AppointmentList[a].Pattern.Length;i++) {
						if(AppointmentList[a].Pattern[i]=='X') {
							retVal[indexOfProvider].ProvBar[appointmentStartBlock+i]=false;
						}
					}
				}
			}
			return retVal;
		}

		/// <summary>Uses Inputs to construct a List&lt;OperatorySchedule&gt;. It is written to reduce the number of queries to the database.</summary>
		private static List<ApptSearchOperatorySchedule> GetAllForDate(DateTime ScheduleDate,List<Schedule> ScheduleList,List<Appointment> AppointmentList,List<ScheduleOp> ScheduleOpList,List<long> OperatoryNums,List<long> ProviderNums) {
			List<ApptSearchOperatorySchedule> retVal = new List<ApptSearchOperatorySchedule>();
			List<ApptSearchOperatorySchedule> opSchedListAll = new List<ApptSearchOperatorySchedule>();
			List<Operatory> opsListAll = OperatoryC.Listt;
			opsListAll.Sort(compareOpsByOpNum);//sort by Operatory Num Ascending
			OperatoryNums.Sort();//Sort by operatory Num Ascending to match
			List<List<long>> opsProvPerSchedules = new List<List<long>>();//opsProvPerSchedules[<opIndex>][ProviderNums] based solely on schedules, lists of providers 'allowed' to work in the given operatory
			List<List<long>> opsProvPerOperatories = new List<List<long>>();//opsProvPerSchedules[<opIndex>][ProviderNums] based solely on operatories, lists of providers 'allowed' to work in the given operatory
			List<List<long>> opsProvIntersect = new List<List<long>>();////opsProvPerSchedules[<opIndex>][ProviderNums] based on the intersection of the two data sets above.
			ScheduleDate=ScheduleDate.Date;//remove time component
			for(int i=0;i<OperatoryNums.Count;i++) {
				opSchedListAll.Add(new ApptSearchOperatorySchedule());
				opSchedListAll[i].SchedDate=ScheduleDate;
				opSchedListAll[i].ProviderNums=new List<long>();
				opSchedListAll[i].OperatoryNum=OperatoryNums[i];
				opSchedListAll[i].OperatorySched=new bool[288];
				for(int j=0;j<288;j++) {
					opSchedListAll[i].OperatorySched[j]=true;//Set entire operatory schedule to true. True=available.
				}
				opsProvPerSchedules.Add(new List<long>());
				opsProvPerOperatories.Add(new List<long>());
				opsProvIntersect.Add(new List<long>());
			}
			#region fillOpSchedListAll.ProviderNums
			for(int i=0;i<ScheduleList.Count;i++) {//use this loop to fill opsProvPerSchedules
				if(ScheduleList[i].SchedDate.Date!=ScheduleDate) {//only schedules for the applicable day.
					continue;
				}
				int schedopsforschedule=0;
				for(int j=0;j<ScheduleOpList.Count;j++) {
					if(ScheduleOpList[j].ScheduleNum!=ScheduleList[i].ScheduleNum) {//ScheduleOp does not apply to this schedule
						continue;
					}
					schedopsforschedule++;
					int indexofop = OperatoryNums.IndexOf(ScheduleOpList[j].OperatoryNum);//cache to increase speed
					if(opsProvPerSchedules[indexofop].Contains(ScheduleList[i].ProvNum)) {//only add ones that have not been added.
						continue;
					}
					opsProvPerSchedules[indexofop].Add(ScheduleList[i].ProvNum);
				}
				if(schedopsforschedule==0) {//Provider is scheduled to work, but not limited to any specific operatory so add provider num to all operatories in opsProvPerSchedules
					for(int k=0;k<opsProvPerSchedules.Count;k++) {
						if(opsProvPerSchedules[k].Contains(ScheduleList[i].ProvNum)) {
							continue;
						}
						opsProvPerSchedules[k].Add(ScheduleList[i].ProvNum);
					}
				}
			}
			for(int i=0;i<opsListAll.Count;i++) {//use this loop to fill opsProvPerOperatories
				opsProvPerOperatories[i].Add(opsListAll[i].ProvDentist);
				opsProvPerOperatories[i].Add(opsListAll[i].ProvHygienist);
			}
			for(int i=0;i<opsProvPerSchedules.Count;i++) {//Use this loop to fill opsProvIntersect by finding matching pairs in opsProvPerSchedules and opsProvPerOperatories
				for(int j=0;j<opsProvPerSchedules[i].Count;j++) {
					if(opsProvPerOperatories[i][0]==0 && opsProvPerOperatories[i][1]==0) {//There are no providers set for this operatory, use all the provider nums from the schedules.
						opsProvIntersect[i].Add(opsProvPerSchedules[i][j]);
						opSchedListAll[i].ProviderNums.Add(opsProvPerSchedules[i][j]);
						continue;
					}
					if(opsProvPerSchedules[i][j]==0) {
						continue;//just in case a non valid prov num got through.
					}
					if(opsProvPerOperatories[i].Contains(opsProvPerSchedules[i][j])) {//if a provider was assigned and matches
						opsProvIntersect[i].Add(opsProvPerSchedules[i][j]);
						opSchedListAll[i].ProviderNums.Add(opsProvPerSchedules[i][j]);
					}
				}
			}
			#endregion fillOpSchedListAll.ProviderNums
			for(int i=0;i<AppointmentList.Count;i++) {//use this loop to set all operatory schedules.
				if(AppointmentList[i].AptDateTime.Date!=ScheduleDate) {//skip appointments that do not apply to this date
					continue;
				}
				int indexofop = OperatoryNums.IndexOf(AppointmentList[i].Op);
				int aptstartindex= (int)AppointmentList[i].AptDateTime.TimeOfDay.TotalMinutes/5;
				for(int j=0;j<AppointmentList[i].Pattern.Length;j++) {//make unavailable all blocks of time during this appointment.
					opSchedListAll[indexofop].OperatorySched[aptstartindex+j]=false;//Set time block to false, meaning something is scheduled here.
				}
			}
			for(int i=0;i<opSchedListAll.Count;i++) {//Filter out operatory schedules for ops that our selected providers don't work in.
				if(retVal.Contains(opSchedListAll[i])) {
					continue;
				}
				for(int j=0;j<opSchedListAll[i].ProviderNums.Count;j++) {
					if(ProviderNums.Contains(opSchedListAll[i].ProviderNums[j])) {
						retVal.Add(opSchedListAll[i]);
						break;
					}
				}
			}
			//For Future Use When adding third search behavior:
			//if((SearchBehaviorCriteria)PrefC.GetInt(PrefName.AppointmentSearchBehavior)==SearchBehaviorCriteria.OperatoryOnly) {
			//  return opSchedListAll;
			//}
			return retVal;
		}

		private static int compareOpsByOpNum(Operatory op1,Operatory op2) {
			return (int)op1.OperatoryNum-(int)op2.OperatoryNum;
		}

		/*
		///<summary>Only used in GetSearchResults.  All times between start and stop get set to true in provBarSched.</summary>
		private static void SetProvBarSched(ref bool[] provBarSched,TimeSpan timeStart,TimeSpan timeStop){
			int startI=GetProvBarIndex(timeStart);
			int stopI=GetProvBarIndex(timeStop);
			for(int i=startI;i<=stopI;i++){
				provBarSched[i]=true;
			}
		}

		private static int GetProvBarIndex(TimeSpan time) {
			return (int)(((double)time.Hours*(double)60/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)//aptTimeIncr=minutesPerIncr
				+(double)time.Minutes/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement))
				*(double)ApptDrawing.LineH*ApptDrawing.RowsPerIncr)
				/ApptDrawing.LineH;//rounds down
		}*/

		///<summary>Used by UI when it needs a recall appointment placed on the pinboard ready to schedule.  This method creates the appointment and attaches all appropriate procedures.  It's up to the calling class to then place the appointment on the pinboard.  If the appointment doesn't get scheduled, it's important to delete it.  If a recallNum is not 0 or -1, then it will create an appt of that recalltype.</summary>
		public static Appointment CreateRecallApt(Patient patCur,List<Procedure> procList,List<InsPlan> planList,long recallNum,List<InsSub> subList){
			List<Recall> recallList=Recalls.GetList(patCur.PatNum);
			Recall recallCur=null;
			if(recallNum>0) {
				recallCur=Recalls.GetRecall(recallNum);
			}
			else{
				for(int i=0;i<recallList.Count;i++){
					if(recallList[i].RecallTypeNum==RecallTypes.PerioType || recallList[i].RecallTypeNum==RecallTypes.ProphyType){
						if(!recallList[i].IsDisabled){
							recallCur=recallList[i];
						}
						break;
					}
				}
			}
			if(recallCur==null){// || recallCur.DateDue.Year<1880){
				throw new ApplicationException(Lan.g("AppointmentL","No recall is due."));//should never happen because everyone has a recall.
			}
			if(recallCur.DateScheduled.Date>=DateTime.Now.Date) {
				throw new ApplicationException(Lan.g("AppointmentL","Recall has already been scheduled for ")+recallCur.DateScheduled.ToShortDateString());
			}
			Appointment AptCur=new Appointment();
			AptCur.PatNum=patCur.PatNum;
			AptCur.AptStatus=ApptStatus.UnschedList;//In all places where this is used, the unsched status with no aptDateTime will cause the appt to be deleted when the pinboard is cleared.
			if(patCur.PriProv==0){
				AptCur.ProvNum=PrefC.GetLong(PrefName.PracticeDefaultProv);
			}
			else{
				AptCur.ProvNum=patCur.PriProv;
			}
			AptCur.ProvHyg=patCur.SecProv;
			if(AptCur.ProvHyg!=0){
				AptCur.IsHygiene=true;
			}
			AptCur.ClinicNum=patCur.ClinicNum;
			//whether perio or prophy:
			List<string> procs=RecallTypes.GetProcs(recallCur.RecallTypeNum);
			string recallPattern=RecallTypes.GetTimePattern(recallCur.RecallTypeNum);
			if(RecallTypes.IsSpecialRecallType(recallCur.RecallTypeNum)
				&& patCur.Birthdate.AddYears(PrefC.GetInt(PrefName.RecallAgeAdult)) > ((recallCur.DateDue>DateTime.Today)?recallCur.DateDue:DateTime.Today)) //For example, if pt's 12th birthday falls after recall date.
			{
				for(int i=0;i<RecallTypeC.Listt.Count;i++) {
					if(RecallTypeC.Listt[i].RecallTypeNum==RecallTypes.ChildProphyType) {
						List<string> childprocs=RecallTypes.GetProcs(RecallTypeC.Listt[i].RecallTypeNum);
						if(childprocs.Count>0) {
							procs=childprocs;//overrides adult procs.
						}
						string childpattern=RecallTypes.GetTimePattern(RecallTypeC.Listt[i].RecallTypeNum);
						if(childpattern!="") {
							recallPattern=childpattern;//overrides adult pattern.
						}
					}
				}
			}
			//convert time pattern to 5 minute increment
			StringBuilder savePattern=new StringBuilder();
			for(int i=0;i<recallPattern.Length;i++){
				savePattern.Append(recallPattern.Substring(i,1));
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==10) {
					savePattern.Append(recallPattern.Substring(i,1));
				}
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15){
					savePattern.Append(recallPattern.Substring(i,1));
					savePattern.Append(recallPattern.Substring(i,1));
				}
			}
			if(savePattern.ToString()==""){
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15){
					savePattern.Append("///XXX///");
				}
				else{
					savePattern.Append("//XX//");
				}
			}
			AptCur.Pattern=savePattern.ToString();
			//Add films------------------------------------------------------------------------------------------------------
			if(RecallTypes.IsSpecialRecallType(recallCur.RecallTypeNum)){//if this is a prophy or perio
				for(int i=0;i<recallList.Count;i++){
					if(recallCur.RecallNum==recallList[i].RecallNum){
						continue;//already handled.
					}
					if(recallList[i].IsDisabled){
						continue;
					}
					if(recallList[i].DateDue.Year<1880){
						continue;
					}
					if(recallList[i].DateDue>recallCur.DateDue//if film due date is after prophy due date
						&& recallList[i].DateDue>DateTime.Today)//and not overdue
					{
						continue;
					}
					//incomplete: exclude manual recall types
					procs.AddRange(RecallTypes.GetProcs(recallList[i].RecallTypeNum));
				}
			}
			AptCur.ProcDescript="";
			for(int i=0;i<procs.Count;i++) {
				if(i>0){
					AptCur.ProcDescript+=", ";
				}
				AptCur.ProcDescript+=ProcedureCodes.GetProcCode(procs[i]).AbbrDesc;
			}
			AptCur.TimeLocked=PrefC.GetBool(PrefName.AppointmentTimeIsLocked);
			Appointments.Insert(AptCur);
			Procedure ProcCur;
			List <PatPlan> patPlanList=PatPlans.Refresh(patCur.PatNum);
			List <Benefit> benefitList=Benefits.Refresh(patPlanList,subList);
			InsPlan priplan=null;
			InsSub prisub=null;
			if(patPlanList.Count>0) {
				prisub=InsSubs.GetSub(patPlanList[0].InsSubNum,subList);
				priplan=InsPlans.GetPlan(prisub.PlanNum,planList);
			}
			double insfee;
			double standardfee;
			for(int i=0;i<procs.Count;i++){
				ProcCur=new Procedure();//this will be an insert
				//procnum
				ProcCur.PatNum=patCur.PatNum;
				ProcCur.AptNum=AptCur.AptNum;
				ProcCur.CodeNum=ProcedureCodes.GetCodeNum(procs[i]);
				ProcCur.ProcDate=DateTime.Now;
				ProcCur.DateTP=DateTime.Now;
				//Check if it's a medical procedure.
				bool isMed = false;
				ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
				if(ProcCur.MedicalCode != null && ProcCur.MedicalCode != "") {
					isMed = true;
				}
				//Get fee schedule for medical or dental.
				long feeSch;
				if(isMed) {
					feeSch=Fees.GetMedFeeSched(patCur,planList,patPlanList,subList);
				}
				else {
					feeSch=Fees.GetFeeSched(patCur,planList,patPlanList,subList);
				}
				//Get the fee amount for medical or dental.
				if(PrefC.GetBool(PrefName.MedicalFeeUsedForNewProcs) && isMed) {
					insfee=Fees.GetAmount0(ProcedureCodes.GetProcCode(ProcCur.MedicalCode).CodeNum,feeSch);
				}
				else {
					insfee=Fees.GetAmount0(ProcCur.CodeNum,feeSch);
				}
				if(priplan!=null && priplan.PlanType=="p") {//PPO
					standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(patCur)).FeeSched);
					if(standardfee>insfee) {
						ProcCur.ProcFee=standardfee;
					}
					else {
						ProcCur.ProcFee=insfee;
					}
				}
				else {
					ProcCur.ProcFee=insfee;
				}
				//surf
				//toothnum
				//Procedures.Cur.ToothRange="";
				//ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.CodeNum).NoBillIns;
				//priority
				ProcCur.ProcStatus=ProcStat.TP;
				ProcCur.Note="";
				//Procedures.Cur.PriEstim=
				//Procedures.Cur.SecEstim=
				//claimnum
				ProcCur.ProvNum=patCur.PriProv;
				//Procedures.Cur.Dx=
				ProcCur.ClinicNum=patCur.ClinicNum;
				//nextaptnum
				ProcCur.BaseUnits = ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
				ProcCur.DiagnosticCode=PrefC.GetString(PrefName.ICD9DefaultForNewProcs);
				Procedures.Insert(ProcCur);//no recall synch required
				Procedures.ComputeEstimates(ProcCur,patCur.PatNum,new List<ClaimProc>(),false,planList,patPlanList,benefitList,patCur.Age,subList);
				if(Programs.UsingOrion){
					FormProcEdit FormP=new FormProcEdit(ProcCur,patCur.Copy(),Patients.GetFamily(patCur.PatNum));
					FormP.IsNew=true;
					FormP.ShowDialog();
					if(FormP.DialogResult==DialogResult.Cancel){
						//any created claimprocs are automatically deleted from within procEdit window.
						try{
							Procedures.Delete(ProcCur.ProcNum);//also deletes the claimprocs
						}
						catch(Exception ex){
							MessageBox.Show(ex.Message);
						}
					}
					else{
						//Do not synch. Recalls based on ScheduleByDate reports in Orion mode.
						//Recalls.Synch(PatCur.PatNum);
					}
				}
			}
			return AptCur;
		}

		///<summary>Tests to see if this appointment will create a double booking. Returns arrayList with no items in it if no double bookings for this appt.  But if double booking, then it returns an arrayList of codes which would be double booked.  You must supply the appointment being scheduled as well as a list of all appointments for that day.  The list can include the appointment being tested if user is moving it to a different time on the same day.  The ProcsForOne list of procedures needs to contain the procedures for the apt becauese procsMultApts won't necessarily, especially if it's a planned appt on the pinboard.</summary>
		public static ArrayList GetDoubleBookedCodes(Appointment apt,DataTable dayTable,List<Procedure> procsMultApts,Procedure[] procsForOne) {
			ArrayList retVal=new ArrayList();//codes
			//figure out which provider we are testing for
			long provNum;
			if(apt.IsHygiene){
				provNum=apt.ProvHyg;
			}
			else{
				provNum=apt.ProvNum;
			}
			//compute the starting row of this appt
			int convertToY=(int)(((double)apt.AptDateTime.Hour*(double)60
				/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)
				+(double)apt.AptDateTime.Minute
				/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)
				)*(double)ApptDrawing.LineH*ApptDrawing.RowsPerIncr);
			int startIndex=convertToY/ApptDrawing.LineH;//rounds down
			string pattern=ApptSingleDrawing.GetPatternShowing(apt.Pattern);
			//keep track of which rows in the entire day would be occupied by provider time for this appt
			ArrayList aptProvTime=new ArrayList();
			for(int k=0;k<pattern.Length;k++){
				if(pattern.Substring(k,1)=="X"){
					aptProvTime.Add(startIndex+k);//even if it extends past midnight, we don't care
				}
			}
			//Now, loop through all the other appointments for the day, and see if any would overlap this one
			bool overlaps;
			Procedure[] procs;
			bool doubleBooked=false;//applies to all appts, not just one at a time.
			DateTime aptDateTime;
			for(int i=0;i<dayTable.Rows.Count;i++){
				if(dayTable.Rows[i]["AptNum"].ToString()==apt.AptNum.ToString()){//ignore current apt in its old location
					continue;
				}
				//ignore other providers
				if(dayTable.Rows[i]["IsHygiene"].ToString()=="1" && dayTable.Rows[i]["ProvHyg"].ToString()!=provNum.ToString()){
					continue;
				}
				if(dayTable.Rows[i]["IsHygiene"].ToString()=="0" && dayTable.Rows[i]["ProvNum"].ToString()!=provNum.ToString()){
					continue;
				}
				if(dayTable.Rows[i]["AptStatus"].ToString()==((int)ApptStatus.Broken).ToString()){//ignore broken appts
					continue;
				}
				aptDateTime=PIn.DateT(dayTable.Rows[i]["AptDateTime"].ToString());
				if(ApptDrawing.IsWeeklyView && aptDateTime.Date==apt.AptDateTime.Date){
					continue;
				}
				//calculate starting row
				//this math is copied from another section of the program, so it's sloppy. Safer than trying to rewrite it:
				convertToY=(int)(((double)aptDateTime.Hour*(double)60
					/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)
					+(double)aptDateTime.Minute
					/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)
					)*(double)ApptDrawing.LineH*ApptDrawing.RowsPerIncr);
				startIndex=convertToY/ApptDrawing.LineH;//rounds down
				pattern=ApptSingleDrawing.GetPatternShowing(dayTable.Rows[i]["Pattern"].ToString());
				//now compare it to apt
				overlaps=false;
				for(int k=0;k<pattern.Length;k++){
					if(pattern.Substring(k,1)=="X"){
						if(aptProvTime.Contains(startIndex+k)){
							overlaps=true;
							doubleBooked=true;
						}
					}
				}
				if(overlaps){
					//we need to add all codes for this appt to retVal
					procs=Procedures.GetProcsOneApt(PIn.Long(dayTable.Rows[i]["AptNum"].ToString()),procsMultApts);
					for(int j=0;j<procs.Length;j++){
						retVal.Add(ProcedureCodes.GetStringProcCode(procs[j].CodeNum));
					}
				}
			}
			//now, retVal contains all double booked procs except for this appt
			//need to all procs for this appt.
			if(doubleBooked){
				for(int j=0;j<procsForOne.Length;j++) {
					retVal.Add(ProcedureCodes.GetStringProcCode(procsForOne[j].CodeNum));
				}
			}
			return retVal;
		}
	}

	///<summary>Holds information about a provider's Schedule. Not actual database table.</summary>
	public class ApptSearchProviderSchedule {
		///<summary>FK to Provider</summary>
		public long ProviderNum;
		///<summary>Date of the ProviderSchedule.</summary>
		public DateTime SchedDate;
		///<summary>This contains a bool for each 5 minute block throughout the day. True means provider is scheduled to work, False means provider is not scheduled to work.</summary>
		public bool[] ProvSchedule;
		///<summary>This contains a bool for each 5 minute block throughout the day. True means available, False means something is scheduled there or the provider is not scheduled to work.</summary>
		public bool[] ProvBar;

		///<summary>Constructor.</summary>
		public ApptSearchProviderSchedule() {
			ProvSchedule=new bool[288];
			ProvBar=new bool[288];
		}
	}

	///<summary>Holds information about a operatory's Schedule. Not actual database table.</summary>
	public class ApptSearchOperatorySchedule {
		///<summary>FK to Operatory</summary>
		public long OperatoryNum;
		///<summary>Date of the OperatorySchedule.</summary>
		public DateTime SchedDate;
		///<summary>This contains a bool for each 5 minute block throughout the day. True means operatory is open, False means operatory is in use.</summary>
		public bool[] OperatorySched;
		///<summary>List of providers 'allowed' to work in this operatory.</summary>
		public List<long> ProviderNums;

	}


}
