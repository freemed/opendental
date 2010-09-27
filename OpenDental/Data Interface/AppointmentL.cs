using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	public class AppointmentL {
		///<summary>The date currently selected in the appointment module.</summary>
		public static DateTime DateSelected;
		
		///<summary>Used by appt search function.  Returns the next available time for the appointment.  Starts searching on lastSlot, which can be tonight at midnight for the first search.  Then, each subsequent search will start at the time of the previous search plus the length of the appointment.  Provider array cannot be length 0.  Might return array of 0 if it goes more than 2 years into the future.</summary>
		public static DateTime[] GetSearchResults(long aptNum,DateTime afterDate,long[] providers,int resultCount,
			TimeSpan beforeTime,TimeSpan afterTime)
		{
			Appointment apt=Appointments.GetOneApt(aptNum);
			DateTime dayEvaluating=afterDate.AddDays(1);
			Appointment[] aptList;//list of appointments for one day
			ArrayList ALresults=new ArrayList();//result Date/Times
			TimeSpan timeFound;
			int hourFound;
			int[][] provBar=new int[providers.Length][];//dim 1 is for each provider.  Dim 2is the 10min increment
			bool[][] provBarSched=new bool[providers.Length][];//keeps track of the schedule of each provider. True means open, false is closed.
			long aptProv;
			string pattern;
			int startIndex;
			int provIndex;//the index of a provider within providers
			List<Schedule> schedDay;//all schedule items for a given day.
			bool aptIsMatch=false;
			while(ALresults.Count<resultCount//stops when the specified number of results are retrieved
				&& dayEvaluating<afterDate.AddYears(2))
			{
				for(int i=0;i<providers.Length;i++){
					provBar[i]=new int[24*ContrApptSheet.RowsPerHr];//[144]; or 24*6
					provBarSched[i]=new bool[24*ContrApptSheet.RowsPerHr];
				}
				//get appointments for one day
				aptList=Appointments.GetForPeriod(dayEvaluating,dayEvaluating);
				//fill provBar
				for(int i=0;i<aptList.Length;i++){
					if(aptList[i].IsHygiene){
						aptProv=aptList[i].ProvHyg;
					}
					else{
						aptProv=aptList[i].ProvNum;
					}
					provIndex=-1;
					for(int p=0;p<providers.Length;p++){
						if(providers[p]==aptProv){
							provIndex=p;
							break;
						}
					}
					if(provIndex==-1){
						continue;
					}
					pattern=ContrApptSingle.GetPatternShowing(aptList[i].Pattern);
					startIndex=(int)(((double)aptList[i].AptDateTime.Hour*(double)60/ContrApptSheet.MinPerRow
						+(double)aptList[i].AptDateTime.Minute/ContrApptSheet.MinPerRow)
						*(double)ContrApptSheet.Lh)/ContrApptSheet.Lh;//rounds down
					for(int k=0;k<pattern.Length;k++){
						if(pattern.Substring(k,1)=="X"){
							provBar[provIndex][startIndex+k]++;
						}
					}
				}
				//handle all schedules by setting element of provBarSched to true if provider schedule shows open.
				schedDay=Schedules.GetDayList(dayEvaluating);
				for(int p=0;p<providers.Length;p++){
					for(int i=0;i<schedDay.Count;i++){
						if(schedDay[i].SchedType!=ScheduleType.Provider){
							continue;
						}
						if(providers[p]!=schedDay[i].ProvNum){
							continue;
						}
						SetProvBarSched(ref provBarSched[p],schedDay[i].StartTime,schedDay[i].StopTime);
					}
				}
				//step through day, one increment at a time, looking for a slot
				pattern=ContrApptSingle.GetPatternShowing(apt.Pattern);
				timeFound=new TimeSpan(0);
				//It's done this way for a plugin that wants to pull all matches for a given day.
				List<bool> findMoreMatchesToday=new List<bool>(); 
				findMoreMatchesToday.Add(true);
				for(int i=0;findMoreMatchesToday[0] && i<provBar[0].Length;i++) {//144 if using 10 minute increments
					for(int p=0;findMoreMatchesToday[0] && p<providers.Length;p++) {
						//assume apt will be placed here
						aptIsMatch=true;
						//test all apt increments for prov closed. If any found, continue
						for(int a=0;a<pattern.Length;a++){
							if(provBarSched[p].Length<i+a+1 || !provBarSched[p][i+a]){
								aptIsMatch=false;
								break;
							}
						}
						if(!aptIsMatch){
							continue;
						}
						//test all apt increments with an X for not scheduled. If scheduled, continue.
						for(int a=0;a<pattern.Length;a++){
							if(pattern.Substring(a,1)=="X" && (provBar[p].Length<i+a+1 || provBar[p][i+a]>0)){
								aptIsMatch=false;
								break;
							}
						}
						if(!aptIsMatch){
							continue;
						}
						//convert to valid time
						hourFound=(int)((double)(i)/(float)60*ContrApptSheet.MinPerRow);//8am=48/60*10
						timeFound=new TimeSpan(
							hourFound,
							//minutes. eg. (13-(2*60/10))*10
							(int)((i-((double)hourFound*(float)60/ContrApptSheet.MinPerRow))*ContrApptSheet.MinPerRow),
							0);
						//make sure it's after the time restricted
						//Debug.WriteLine(timeFound.ToString()+"  "+afterTime.ToString());
							//apt.AptDateTime.TimeOfDay+"  "+afterTime.ToString());
						if(afterTime!=TimeSpan.Zero && timeFound<afterTime){
							aptIsMatch=false;
							continue;
						}
						if(beforeTime!=TimeSpan.Zero && timeFound>beforeTime) {
							aptIsMatch=false;
							continue;
						}
						//match found
						ALresults.Add(dayEvaluating+timeFound);
						findMoreMatchesToday[0]=false;
						Plugins.HookAddCode(null,"AppointmentL.GetSearchResults_postfilter",ALresults,providers[p],apt,findMoreMatchesToday);
					}//for p
				}
				dayEvaluating=dayEvaluating.AddDays(1);//move to the next day
			}
			DateTime[] retVal=new DateTime[ALresults.Count];
			ALresults.CopyTo(retVal);
			return retVal;
		}

		///<summary>Only used in GetSearchResults.  All times between start and stop get set to true in provBarSched.</summary>
		private static void SetProvBarSched(ref bool[] provBarSched,DateTime timeStart,DateTime timeStop){
			int startI=GetProvBarIndex(timeStart);
			int stopI=GetProvBarIndex(timeStop);
			for(int i=startI;i<=stopI;i++){
				provBarSched[i]=true;
			}
		}

		private static int GetProvBarIndex(DateTime time){
			return (int)(((double)time.Hour*(double)60/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)//aptTimeIncr=minutesPerIncr
				+(double)time.Minute/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement))
				*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr)
				/ContrApptSheet.Lh;//rounds down
		}

		///<summary>Used by UI when it needs a recall appointment placed on the pinboard ready to schedule.  This method creates the appointment and attaches all appropriate procedures.  It's up to the calling class to then place the appointment on the pinboard.  If the appointment doesn't get scheduled, it's important to delete it.  If a recallNum is not 0 or -1, then it will create an appt of that recalltype.</summary>
		public static Appointment CreateRecallApt(Patient patCur,List<Procedure> procList,List<InsPlan> planList,long recallNum){
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
			if(recallCur==null || recallCur.DateDue.Year<1880){
				throw new ApplicationException(Lan.g("AppointmentL","No recall is due."));
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
				&& patCur.Birthdate.AddYears(12) > recallCur.DateDue) //if pt's 12th birthday falls after recall date. ie younger than 12.
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
			Appointments.Insert(AptCur);	
			Procedure ProcCur;
			List <PatPlan> patPlanList=PatPlans.Refresh(patCur.PatNum);
			List <Benefit> benefitList=Benefits.Refresh(patPlanList);
			InsPlan priplan=null;
			if(patPlanList.Count>0) {
				priplan=InsPlans.GetPlan(patPlanList[0].PlanNum,planList);
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
				insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(patCur,planList,patPlanList));
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
				ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
				ProcCur.BaseUnits = ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
				Procedures.Insert(ProcCur);//no recall synch required
				Procedures.ComputeEstimates(ProcCur,patCur.PatNum,new List<ClaimProc>(),false,planList,patPlanList,benefitList,patCur.Age);
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
						//not needed because always TP
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
				)*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr);
			int startIndex=convertToY/ContrApptSheet.Lh;//rounds down
			string pattern=ContrApptSingle.GetPatternShowing(apt.Pattern);
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
				if(ContrApptSheet.IsWeeklyView && aptDateTime.Date==apt.AptDateTime.Date){
					continue;
				}
				//calculate starting row
				//this math is copied from another section of the program, so it's sloppy. Safer than trying to rewrite it:
				convertToY=(int)(((double)aptDateTime.Hour*(double)60
					/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)
					+(double)aptDateTime.Minute
					/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)
					)*(double)ContrApptSheet.Lh*ContrApptSheet.RowsPerIncr);
				startIndex=convertToY/ContrApptSheet.Lh;//rounds down
				pattern=ContrApptSingle.GetPatternShowing(dayTable.Rows[i]["Pattern"].ToString());
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
}
