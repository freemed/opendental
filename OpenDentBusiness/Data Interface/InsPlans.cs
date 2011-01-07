using CodeBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class InsPlans {
		///<summary>Also fills PlanNum from db.</summary>
		public static long Insert(InsPlan plan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				plan.PlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),plan);
				return plan.PlanNum;
			}
			return Crud.InsPlanCrud.Insert(plan);
		}

		///<summary></summary>
		public static void Update(InsPlan plan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),plan);
				return;
			}
			Crud.InsPlanCrud.Update(plan);
		}

		///<summary>It's fastest if you supply a plan list that contains the plan, but it also works just fine if it can't initally locate the plan in the list.  You can supply an list of length 0 or null.  If still not found, returns null.</summary>
		public static InsPlan GetPlan(long planNum,List<InsPlan> planList) {
			//No need to check RemotingRole; no call to db.
			InsPlan retPlan=new InsPlan();
			if(planNum==0) {
				return null;
			}
			if(planList==null) {
				planList=new List<InsPlan>();
			}
			bool found=false;
			for(int i=0;i<planList.Count;i++) {
				if(planList[i].PlanNum==planNum) {
					found=true;
					retPlan=planList[i];
				}
			}
			if(!found) {
				retPlan=RefreshOne(planNum);//retPlan will now be null if not found
			}
			if(retPlan==null) {
				//MessageBox.Show(Lans.g("InsPlans","Database is inconsistent.  Please run the database maintenance tool."));
				return new InsPlan();
			}
			if(retPlan==null) {
				return null;
			}
			return retPlan;
		}

		/*
		///<summary>Will return null if no active plan for that ordinal.  Ordinal means primary, secondary, etc.</summary>
		public static InsPlan GetPlanByOrdinal(int patNum,int ordinal) {
			string command="SELECT * FROM insplan WHERE EXISTS "
				+"(SELECT * FROM patplan WHERE insplan.PlanNum=patplan.PlanNum "
				+"AND patplan.PatNum="+POut.PInt(patNum)
				+" AND patplan.Ordinal="+POut.PInt(ordinal);
			//num = '"+planNum+"'";
		}*/

		public static InsPlan[] GetByTrojanID(string trojanID) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<InsPlan[]>(MethodBase.GetCurrentMethod(),trojanID);
			} 
			string command="SELECT * FROM insplan WHERE TrojanID = '"+POut.String(trojanID)+"'";
			return Crud.InsPlanCrud.SelectMany(command).ToArray();
		}

		///<summary>Only loads one plan from db. Can return null.</summary>
		public static InsPlan RefreshOne(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<InsPlan>(MethodBase.GetCurrentMethod(),planNum);
			} 
			if(planNum==0){
				return null;
			}
			string command="SELECT * FROM insplan WHERE plannum = '"+planNum+"'";
			return Crud.InsPlanCrud.SelectOne(command);
		}

		//<summary>Deprecated.  Instead, use RefreshForSubList.</summary>
		//public static List<InsPlan> RefreshForFam(){//Family Fam) {
		//	return null;
		//}

		///<summary>Gets List of plans based on the subList.  The list won't be in the same order.</summary>
		public static List<InsPlan> RefreshForSubList(List<InsSub> subList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InsPlan>>(MethodBase.GetCurrentMethod(),subList);
			}
			if(subList.Count==0) {
				return new List<InsPlan>();
			}
			string command="SELECT * FROM insplan WHERE";
			for(int i=0;i<subList.Count;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" PlanNum="+POut.Long(subList[i].PlanNum);
			}
			return Crud.InsPlanCrud.SelectMany(command);
		}

		///<summary>Tests all fields for equality.</summary>
		public static bool AreEqualValue(InsPlan plan1,InsPlan plan2) {
			if(plan1.PlanNum==plan2.PlanNum
				&& plan1.GroupName==plan2.GroupName
				&& plan1.GroupNum==plan2.GroupNum
				&& plan1.PlanNote==plan2.PlanNote
				&& plan1.FeeSched==plan2.FeeSched
				&& plan1.PlanType==plan2.PlanType
				&& plan1.ClaimFormNum==plan2.ClaimFormNum
				&& plan1.UseAltCode==plan2.UseAltCode
				&& plan1.ClaimsUseUCR==plan2.ClaimsUseUCR
				&& plan1.CopayFeeSched==plan2.CopayFeeSched
				&& plan1.EmployerNum==plan2.EmployerNum
				&& plan1.CarrierNum==plan2.CarrierNum
				&& plan1.AllowedFeeSched==plan2.AllowedFeeSched
				&& plan1.TrojanID==plan2.TrojanID
				&& plan1.DivisionNo==plan2.DivisionNo
				&& plan1.IsMedical==plan2.IsMedical
				&& plan1.FilingCode==plan2.FilingCode
				&& plan1.DentaideCardSequence==plan2.DentaideCardSequence
				&& plan1.ShowBaseUnits==plan2.ShowBaseUnits
				&& plan1.CodeSubstNone==plan2.CodeSubstNone
				&& plan1.IsHidden==plan2.IsHidden
				&& plan1.MonthRenew==plan2.MonthRenew
				&& plan1.FilingCodeSubtype==plan2.FilingCodeSubtype
				&& plan1.CanadianPlanFlag==plan2.CanadianPlanFlag) 
			{
				return true;
			}
			return false;
		}

		/*
		///<summary>Called from FormInsPlan when applying changes to all identical insurance plans. This updates the synchronized fields for all plans like the specified insPlan.  Current InsPlan must be set to the new values that we want.  BenefitNotes and SubscNote are specific to subscriber and are not changed.  PlanNotes are handled separately in a different function after this one is complete.</summary>
		public static void UpdateForLike(InsPlan like, InsPlan plan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),like,plan);
				return;
			}
			string command= "UPDATE insplan SET "
				+"EmployerNum = '"     +POut.Long   (plan.EmployerNum)+"'"
				+",GroupName = '"      +POut.String(plan.GroupName)+"'"
				+",GroupNum = '"       +POut.String(plan.GroupNum)+"'"
				+",DivisionNo = '"     +POut.String(plan.DivisionNo)+"'"
				+",CarrierNum = '"     +POut.Long   (plan.CarrierNum)+"'"
				+",PlanType = '"       +POut.String(plan.PlanType)+"'"
				+",UseAltCode = '"     +POut.Bool  (plan.UseAltCode)+"'"
				+",IsMedical = '"      +POut.Bool  (plan.IsMedical)+"'"
				+",ClaimsUseUCR = '"   +POut.Bool  (plan.ClaimsUseUCR)+"'"
				+",FeeSched = '"       +POut.Long   (plan.FeeSched)+"'"
				+",CopayFeeSched = '"  +POut.Long   (plan.CopayFeeSched)+"'"
				+",ClaimFormNum = '"   +POut.Long   (plan.ClaimFormNum)+"'"
				+",AllowedFeeSched= '" +POut.Long   (plan.AllowedFeeSched)+"'"
				+",TrojanID = '"       +POut.String(plan.TrojanID)+"'"
				+",FilingCode = '"     +POut.Long   (plan.FilingCode)+"'"
				+",FilingCodeSubtype = '"+POut.Long(plan.FilingCodeSubtype)+"'"
				+",ShowBaseUnits = '"  +POut.Bool  (plan.ShowBaseUnits)+"'"
				//+",DedBeforePerc = '"  +POut.PBool  (plan.DedBeforePerc)+"'"
				+",CodeSubstNone='"    +POut.Bool  (plan.CodeSubstNone)+"'"
				+",IsHidden='"         +POut.Bool  (plan.IsHidden)+"'"
				+",MonthRenew='"       +POut.Int   (plan.MonthRenew)+"'"
				//It is most likely that MonthRenew would be the same for everyone on the same plan.  If we get complaints, we might have to add an option.
				+" WHERE "
				+"EmployerNum = '"        +POut.Long   (like.EmployerNum)+"' "
				+"AND GroupName = '"      +POut.String(like.GroupName)+"' "
				+"AND GroupNum = '"       +POut.String(like.GroupNum)+"' "
				+"AND DivisionNo = '"     +POut.String(like.DivisionNo)+"'"
				+"AND CarrierNum = '"     +POut.Long   (like.CarrierNum)+"' "
				+"AND IsMedical = '"      +POut.Bool  (like.IsMedical)+"'";
			Db.NonQ(command);
		}*/

		///<summary>Gets a description of the specified plan, including carrier name and subscriber. It's fastest if you supply a plan list that contains the plan, but it also works just fine if it can't initally locate the plan in the list.  You can supply an array of length 0 for both family and planlist.</summary>
		public static string GetDescript(long planNum,Family family,List<InsPlan> planList,long insSubNum,List<InsSub> subList) {
			//No need to check RemotingRole; no call to db.
			if(planNum==0) {
				return "";
			}
			InsPlan plan=GetPlan(planNum,planList);
			if(plan==null || plan.PlanNum==0) {
				return "";
			}
			InsSub sub=InsSubs.GetSub(insSubNum,subList);
			if(sub==null || sub.InsSubNum==0) {
				return "";
			}
			string subscriber=family.GetNameInFamFL(sub.Subscriber);
			if(subscriber=="") {//subscriber from another family
				subscriber=Patients.GetLim(sub.Subscriber).GetNameLF();
			}
			string retStr="";
			//loop just to get the index of the plan in the family list
			bool otherFam=true;
			for(int i=0;i<planList.Count;i++) {
				if(planList[i].PlanNum==planNum) {
					otherFam=false;
					//retStr += (i+1).ToString()+": ";
				}
			}
			if(otherFam) {//retStr=="")
				retStr="(other fam):";
			}
			Carrier carrier=Carriers.GetCarrier(plan.CarrierNum);
			string carrierName=carrier.CarrierName;
			if(carrierName.Length>20) {
				carrierName=carrierName.Substring(0,20)+"...";
			}
			retStr+=carrierName;
			retStr+=" ("+subscriber+")";
			return retStr;
		}

		///<summary>Used in Ins lines in Account module and in Family module.</summary>
		public static string GetCarrierName(long planNum,List<InsPlan> planList) {
			//No need to check RemotingRole; no call to db.
			InsPlan plan=GetPlan(planNum,planList);
			if(plan==null) {
				return "";
			}
			Carrier carrier=Carriers.GetCarrier(plan.CarrierNum);
			if(carrier.CarrierNum==0) {//if corrupted
				return "";
			}
			return carrier.CarrierName;
		}

		/// <summary>Only used once in Claims.cs.  Gets insurance benefits remaining for one benefit year.  Returns actual remaining insurance based on ClaimProc data, taking into account inspaid and ins pending. Must supply all claimprocs for the patient.  Date used to determine which benefit year to calc.  Usually today's date.  The insplan.PlanNum is the plan to get value for.  ExcludeClaim is the ClaimNum to exclude, or enter -1 to include all.  This does not yet handle calculations where ortho max is different from regular max.  Just takes the most general annual max, and subtracts all benefits used from all categories.</summary>
		public static double GetInsRem(List<ClaimProcHist> histList,DateTime asofDate,long planNum,long patPlanNum,long excludeClaim,List<InsPlan> planList,List<Benefit> benList,long patNum) {
			//No need to check RemotingRole; no call to db.
			double insUsed=GetInsUsedDisplay(histList,asofDate,planNum,patPlanNum,excludeClaim,planList,benList,patNum);
			InsPlan plan=InsPlans.GetPlan(planNum,planList);
			double insPending=GetPendingDisplay(histList,asofDate,plan,patPlanNum,excludeClaim,patNum);
			double annualMaxFam=Benefits.GetAnnualMaxDisplay(benList,planNum,patPlanNum,true);
			double annualMaxInd=Benefits.GetAnnualMaxDisplay(benList,planNum,patPlanNum,false);
			double annualMax=annualMaxInd;
			if(annualMaxFam>annualMaxInd){
				annualMax=annualMaxFam;
			}
			if(annualMax<0) {
				return 999999;
			}
			if(annualMax-insUsed-insPending<0) {
				return 0;
			}
			return annualMax-insUsed-insPending;
		}

		///<summary>Only for display purposes rather than for calculations.  Get pending insurance for a given plan for one benefit year. Include a history list for the patient/family.  asofDate used to determine which benefit year to calc.  Usually the date of service for a claim.  The planNum is the plan to get value for.</summary>
		public static double GetPendingDisplay(List<ClaimProcHist> histList,DateTime asofDate,InsPlan curPlan,long patPlanNum,long excludeClaim,long patNum) {
			//No need to check RemotingRole; no call to db.
			//InsPlan curPlan=GetPlan(planNum,PlanList);
			if(curPlan==null) {
				return 0;
			}
			//get the most recent renew date, possibly including today:
			DateTime renewDate=BenefitLogic.ComputeRenewDate(asofDate,curPlan.MonthRenew);
			DateTime stopDate=renewDate.AddYears(1);
			double retVal=0;
			CovCat generalCat=CovCats.GetForEbenCat(EbenefitCategory.General);
			CovSpan[] covSpanArray=null;
			if(generalCat!=null) {
				covSpanArray=CovSpans.GetForCat(generalCat.CovCatNum);
			}
			for(int i=0;i<histList.Count;i++) {
				if(generalCat!=null) {//If there is a general category, then we only consider codes within it.  This is how we exclude ortho.
					if(!CovSpans.IsCodeInSpans(histList[i].StrProcCode,covSpanArray)) {//for example, ortho
						continue;
					}
				}
				if(histList[i].PlanNum==curPlan.PlanNum
					&& histList[i].ClaimNum != excludeClaim
					&& histList[i].ProcDate < stopDate
					&& histList[i].ProcDate >= renewDate
					//enum ClaimProcStatus{NotReceived,Received,Preauth,Adjustment,Supplemental}
					&& histList[i].Status==ClaimProcStatus.NotReceived
					&& histList[i].PatNum==patNum)
				//Status Adjustment has no insPayEst, so can ignore it here.
				{
					retVal+=histList[i].Amount;
				}
			}
			return retVal;
		}

		/// <summary>Only for display purposes rather than for calculations.  Get insurance benefits used for one benefit year.  Must supply all relevant hist for the patient.  asofDate is used to determine which benefit year to calc.  Usually date of service for a claim.  The insplan.PlanNum is the plan to get value for.  ExcludeClaim is the ClaimNum to exclude, or enter -1 to include all.  The behavior of this changed in 7.1.  It now only includes values that apply towards annual max.  So if there is a limitation override for a category like ortho or preventive, then completed procedures in those categories will be excluded.  The benefitList passed in might very well have benefits from other insurance plans included.</summary>
		public static double GetInsUsedDisplay(List<ClaimProcHist> histList,DateTime asofDate,long planNum,long patPlanNum,long excludeClaim,List<InsPlan> planList,List<Benefit> benefitList,long patNum) {
			//No need to check RemotingRole; no call to db.
			InsPlan curPlan=GetPlan(planNum,planList);
			if(curPlan==null) {
				return 0;
			}
			//get the most recent renew date, possibly including today:
			DateTime renewDate=BenefitLogic.ComputeRenewDate(asofDate,curPlan.MonthRenew);
			DateTime stopDate=renewDate.AddYears(1);
			double retVal=0;
			CovCat generalCat=CovCats.GetForEbenCat(EbenefitCategory.General);
			CovSpan[] covSpanArray=null;
			if(generalCat!=null) {
				covSpanArray=CovSpans.GetForCat(generalCat.CovCatNum);
			}
			for(int i=0;i<histList.Count;i++) {
				if(histList[i].PlanNum!=planNum
					|| histList[i].ClaimNum == excludeClaim
					|| histList[i].ProcDate.Date >= stopDate
					|| histList[i].ProcDate.Date < renewDate
					|| histList[i].PatNum != patNum)
				{
					continue;
				}
				if(Benefits.LimitationExistsNotGeneral(benefitList,planNum,patPlanNum,histList[i].StrProcCode)) {
					continue;
				}
				//if(generalCat!=null){//If there is a general category, then we only consider codes within it.  This is how we exclude ortho.
				//	if(histList[i].StrProcCode!="" && !CovSpans.IsCodeInSpans(histList[i].StrProcCode,covSpanArray)){//for example, ortho
				//		continue;
				//	}
				//}
				//enum ClaimProcStatus{NotReceived,Received,Preauth,Adjustment,Supplemental}
				if(histList[i].Status==ClaimProcStatus.Received 
					|| histList[i].Status==ClaimProcStatus.Adjustment
					|| histList[i].Status==ClaimProcStatus.Supplemental) 
				{
					retVal+=histList[i].Amount;
				}
			}
			return retVal;
		}

		///<summary>Only for display purposes rather than for calculations.  Get insurance deductible used for one benefit year.  Must supply a history list for the patient/family.  asofDate is used to determine which benefit year to calc.  Usually date of service for a claim.  The planNum is the plan to get value for.  ExcludeClaim is the ClaimNum to exclude, or enter -1 to include all.  It includes pending deductibles in the result.</summary>
		public static double GetDedUsedDisplay(List<ClaimProcHist> histList,DateTime asofDate,long planNum,long patPlanNum,long excludeClaim,List<InsPlan> planList,BenefitCoverageLevel coverageLevel,long patNum) {
			//No need to check RemotingRole; no call to db.
			InsPlan curPlan=GetPlan(planNum,planList);
			if(curPlan==null) {
				return 0;
			}
			//get the most recent renew date, possibly including today. Date based on annual max.
			DateTime renewDate=BenefitLogic.ComputeRenewDate(asofDate,curPlan.MonthRenew);
			DateTime stopDate=renewDate.AddYears(1);
			double retVal=0;
			for(int i=0;i<histList.Count;i++) {
				if(histList[i].PlanNum!=planNum
					|| histList[i].ClaimNum == excludeClaim
					|| histList[i].ProcDate >= stopDate
					|| histList[i].ProcDate < renewDate
					//no need to check status, because only the following statuses will be part of histlist:
					//Adjustment,NotReceived,Received,Supplemental
					)
				{
					continue;
				}
				if(coverageLevel!=BenefitCoverageLevel.Family && histList[i].PatNum != patNum) {
					continue;//to exclude histList items from other family members
				}
				retVal+=histList[i].Deduct;
			}
			return retVal;
		}

		/*
		///<summary>Used once from Claims and also in ContrTreat.  Gets insurance deductible remaining for one benefit year which includes the given date.  Must supply all claimprocs for the patient.  Must supply all benefits for patient so that we know if it's a service year or a calendar year.  Date used to determine which benefit year to calc.  Usually today's date.  The insplan.PlanNum is the plan to get value for.  ExcludeClaim is the ClaimNum to exclude, or enter -1 to include all.  The supplied procCode is needed because some deductibles, for instance, do not apply to preventive.</summary>
		public static double GetDedRem(List<ClaimProc> claimProcList,DateTime date,int planNum,int patPlanNum,int excludeClaim,List<InsPlan> PlanList,List<Benefit> benList,string procCode) {
			//No need to check RemotingRole; no call to db.
			double dedTot=Benefits.GetDeductibleByCode(benList,planNum,patPlanNum,procCode);
			double dedUsed=GetDedUsed(claimProcList,date,planNum,patPlanNum,excludeClaim,PlanList,benList);
			if(dedTot-dedUsed<0){
				return 0;
			}
			return dedTot-dedUsed;
		}*/

		/*
		///<Summary>Only used in TP to calculate discount for PPO procedure.  Will return -1 if no fee found.</Summary>
		public static double GetPPOAllowed(int codeNum,InsPlan plan){
			//plan has already been tested to not be null and to be a PPO plan.
			double fee=Fees.GetAmount(codeNum,plan.FeeSched);//could be -1
		}*/

		///<summary>This is used in FormQuery.SubmitQuery to allow display of carrier names.</summary>
		public static Hashtable GetHListAll(){
			//No need to check RemotingRole; no call to db.
			DataTable table=GetCarrierTable();
			Hashtable HListAll=new Hashtable(table.Rows.Count);
			long plannum;
			string carrierName;
			for(int i=0;i<table.Rows.Count;i++){
				plannum=PIn.Long(table.Rows[i][0].ToString());
				carrierName=PIn.String(table.Rows[i][1].ToString());
				HListAll.Add(plannum,carrierName);
			}
			return HListAll;
		}

		public static DataTable GetCarrierTable() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command="SELECT insplan.PlanNum,carrier.CarrierName "
				+"FROM insplan,carrier "
				+"WHERE insplan.CarrierNum=carrier.CarrierNum";
			return Db.GetTable(command);
		}
		/*
		///<summary>Used by Trojan.  Gets all distinct notes for the planNums supplied.  Includes blank notes.</summary>
		public static string[] GetNotesForPlans(List<long> planNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<string[]>(MethodBase.GetCurrentMethod(),planNums,excludePlanNum);
			}
			if(planNums.Count==0) {//this should never happen, but just in case...
				return new string[0];
			}
			if(planNums.Count==1 && planNums[0]==excludePlanNum){
				return new string[0];
			}
			string s="";
			for(int i=0;i<planNums.Count;i++) {
				if(planNums[i]==excludePlanNum){
					continue;
				}
				if(s!="") {
					s+=" OR";
				}
				s+=" PlanNum="+POut.Long(planNums[i]);
			}
			string command="SELECT DISTINCT PlanNote FROM insplan WHERE"+s;
			DataTable table=Db.GetTable(command);
			string[] retVal=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				retVal[i]=PIn.String(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		///<summary>Used by Trojan.  Sets the PlanNote for multiple plans at once.</summary>
		public static void UpdateNoteForPlans(List<long> planNums,string newNote) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),planNums,newNote);
				return;
			}
			if(planNums.Count==0){
				return;
			}
			string s="";
			for(int i=0;i<planNums.Count;i++){
				if(i>0){
					s+=" OR";
				}
				s+=" PlanNum="+POut.Long(planNums[i]);
			}
			string command="UPDATE insplan SET PlanNote='"+POut.String(newNote)+"' "
				+"WHERE"+s;
			Db.NonQ(command);
		}*/

		/*
		///<summary>Called from FormInsPlan when user wants to view a benefit note for similar plans.  Should never include the current plan that the user is editing.  This function will get one note from the database, not including blank notes.  If no note can be found, then it returns empty string.</summary>
		public static string GetBenefitNotes(List<long> planNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),planNums);
			}
			if(planNums.Count==0){
				return "";
			}
			string s="";
			for(int i=0;i<planNums.Count;i++) {
				if(i>0) {
					s+=" OR";
				}
				s+=" PlanNum="+POut.Long(planNums[i]);
			}
			string command="SELECT BenefitNotes FROM insplan WHERE BenefitNotes != '' AND ("+s+") "+DbHelper.LimitAnd(1);
			DataTable table=Db.GetTable(command);
			//string[] retVal=new string[];
			if(table.Rows.Count==0){
				return "";
			}
			return PIn.String(table.Rows[0][0].ToString());
		}*/

		/*
		///<summary>Gets a list of PlanNums from the database of plans that have identical info as this one. Used to perform updates to benefits, etc.  Note that you have the option to include the current plan in the list.</summary>
		public static List<long> GetPlanNumsOfSamePlans(string employerName,string groupName,string groupNum,
				string divisionNo,string carrierName,bool isMedical,long planNum,bool includePlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),employerName,groupName,groupNum,divisionNo,carrierName,isMedical,planNum,includePlanNum);
			}
			string command="SELECT PlanNum FROM insplan "
				+"LEFT JOIN carrier ON carrier.CarrierNum = insplan.CarrierNum "
				+"LEFT JOIN employer ON employer.EmployerNum = insplan.EmployerNum ";
			if(employerName==""){
				command+="WHERE employer.EmpName IS NULL ";
			}
			else{
				command+="WHERE employer.EmpName = '"+POut.String(employerName)+"' ";
			}
			command+="AND insplan.GroupName = '"  +POut.String(groupName)+"' "
				+"AND insplan.GroupNum = '"   +POut.String(groupNum)+"' "
				+"AND insplan.DivisionNo = '" +POut.String(divisionNo)+"' "
				+"AND carrier.CarrierName = '"+POut.String(carrierName)+"' "
				+"AND insplan.IsMedical = '"  +POut.Bool  (isMedical)+"'"
				+"AND insplan.PlanNum != "+POut.Long(planNum);
			DataTable table=Db.GetTable(command);
			List<long> retVal=new List<long>();
			//if(includePlanNum){
			//	retVal=new int[table.Rows.Count+1];
			//}
			//else{
			//	retVal=new int[table.Rows.Count];
			//}
			for(int i=0;i<table.Rows.Count;i++) {
				retVal.Add(PIn.Long(table.Rows[i][0].ToString()));
			}
			if(includePlanNum){
				retVal.Add(planNum);
			}
			return retVal;
		}*/

		///<summary>Used from FormInsPlans to get a big list of many plans, organized by carrier name or by employer.</summary>
		public static DataTable GetBigList(bool byEmployer,string empName,string carrierName,string groupName,string groupNum,
			string trojanID,bool showHidden)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),byEmployer,empName,carrierName,groupName,groupNum,trojanID,showHidden);
			}
			DataTable table=new DataTable();
			DataRow row;
			table.Columns.Add("Address");
			table.Columns.Add("City");
			table.Columns.Add("CarrierName");
			table.Columns.Add("ElectID");
			table.Columns.Add("EmpName");
			table.Columns.Add("GroupName");
			table.Columns.Add("GroupNum");
			table.Columns.Add("noSendElect");
			table.Columns.Add("Phone");
			table.Columns.Add("PlanNum");
			table.Columns.Add("State");
			table.Columns.Add("subscribers");
			table.Columns.Add("trojanID");
			table.Columns.Add("Zip");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT carrier.Address,carrier.City,CarrierName,ElectID,EmpName,GroupName,GroupNum,NoSendElect,"
				+"carrier.Phone,PlanNum,"
				+"(SELECT COUNT(*) FROM inssub WHERE insplan.PlanNum=inssub.PlanNum) subscribers,"//for Oracle
				+"carrier.State,TrojanID,carrier.Zip, "
				//+"(SELECT COUNT(*) FROM employer WHERE insplan.EmployerNum=employer.EmployerNum) haveName "//for Oracle. Could be higher than 1?
				+"CASE WHEN (EmpName IS NULL) THEN 1 ELSE 0 END as haveName "//for Oracle
				+"FROM insplan "
				+"LEFT JOIN employer ON employer.EmployerNum = insplan.EmployerNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum = insplan.CarrierNum "
				+"WHERE CarrierName LIKE '%"+POut.String(carrierName)+"%' ";
			if(empName!="") {
				command+="AND EmpName LIKE '%"+POut.String(empName)+"%' ";
			}
			if(groupName!="") {
				command+="AND GroupName LIKE '%"+POut.String(groupName)+"%' ";
			}
			if(groupNum!="") {
				command+="AND GroupNum LIKE '%"+POut.String(groupNum)+"%' ";
			}
			if(trojanID!=""){
				command+="AND TrojanID LIKE '%"+POut.String(trojanID)+"%' ";
			}
			if(!showHidden){
				command+="AND insplan.IsHidden=0 ";
			}
			if(byEmployer) {
				command+="ORDER BY haveName,EmpName,CarrierName";
			}
			else {//not by employer
				command+="ORDER BY CarrierName";
			}
			DataTable rawT=Db.GetTable(command);
			for(int i=0;i<rawT.Rows.Count;i++) {
				row=table.NewRow();
				row["Address"]=rawT.Rows[i]["Address"].ToString();
				row["City"]=rawT.Rows[i]["City"].ToString();
				row["CarrierName"]=rawT.Rows[i]["CarrierName"].ToString();
				row["ElectID"]=rawT.Rows[i]["ElectID"].ToString();
				row["EmpName"]=rawT.Rows[i]["EmpName"].ToString();
				row["GroupName"]=rawT.Rows[i]["GroupName"].ToString();
				row["GroupNum"]=rawT.Rows[i]["GroupNum"].ToString();
				if(rawT.Rows[i]["NoSendElect"].ToString()=="0"){
					row["noSendElect"]="";
				}	
				else{
					row["noSendElect"]="X";
				}
				row["Phone"]=rawT.Rows[i]["Phone"].ToString();
				row["PlanNum"]=rawT.Rows[i]["PlanNum"].ToString();
				row["State"]=rawT.Rows[i]["State"].ToString();
				row["subscribers"]=rawT.Rows[i]["subscribers"].ToString();
				row["TrojanID"]=rawT.Rows[i]["TrojanID"].ToString();
				row["Zip"]=rawT.Rows[i]["Zip"].ToString();
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>Used in FormFeesForIns</summary>
		public static DataTable GetListFeeCheck(string carrierName,string carrierNameNot,long feeSchedWithout,long feeSchedWith,
			FeeScheduleType feeSchedType)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),carrierName,carrierNameNot,feeSchedWithout,feeSchedWith,feeSchedType);
			}
			string pFeeSched="FeeSched";
			if(feeSchedType==FeeScheduleType.Allowed){
				pFeeSched="AllowedFeeSched";
			}
			if(feeSchedType==FeeScheduleType.CoPay){
				pFeeSched="CopayFeeSched";
			}
			string command=
				"SELECT insplan.GroupName,insplan.GroupNum,COUNT(*) AS Plans,employer.EmpName,carrier.CarrierName,"
				+"insplan.EmployerNum,insplan.CarrierNum,feesched.Description AS FeeSchedName,insplan.PlanType,"
				+"insplan."+pFeeSched+" feeSched "
				+"FROM insplan "
				+"LEFT JOIN employer ON employer.EmployerNum = insplan.EmployerNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum = insplan.CarrierNum "
				+"LEFT JOIN feesched ON feesched.FeeSchedNum = insplan."+pFeeSched+" "
				+"WHERE carrier.CarrierName LIKE '%"+POut.String(carrierName)+"%' ";
			if(carrierNameNot!=""){
				command+="AND carrier.CarrierName NOT LIKE '%"+POut.String(carrierNameNot)+"%' ";
			}
			if(feeSchedWithout!=0){
				command+="AND insplan."+pFeeSched+" !="+POut.Long(feeSchedWithout)+" ";
			}
			if(feeSchedWith!=0) {
				command+="AND insplan."+pFeeSched+" ="+POut.Long(feeSchedWith)+" ";
			}
			command+="insplan.GroupName,employer.EmpName,carrier.CarrierName,"
				+"insplan.EmployerNum,insplan.CarrierNum,feesched.Description,insplan.PlanType,"
				+"insplan."+pFeeSched+" "
				+"ORDER BY carrier.CarrierName,employer.EmpName,insplan.GroupNum";
			return Db.GetTable(command);
		}

		///<summary>Based on the four supplied parameters, it updates all similar plans.  Used in a specific tool: FormFeesForIns.</summary>
		public static long SetFeeSched(long employerNum,string carrierName,string groupNum,string groupName,long feeSchedNum,
			FeeScheduleType feeSchedType)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),employerNum,carrierName,groupNum,groupName,feeSchedNum,feeSchedType);
			}
			//FIXME:UPDATE-MULTIPLE-TABLES
			/*string command="UPDATE insplan,carrier SET insplan.FeeSched="+POut.PInt(feeSchedNum)
				+" WHERE carrier.CarrierNum = insplan.CarrierNum "//employer.EmployerNum = insplan.EmployerNum "
				+"AND insplan.EmployerNum='"+POut.PInt(employerNum)+"' "
				+"AND carrier.CarrierName='"+POut.PString(carrierName)+"' "
				+"AND insplan.GroupNum='"+POut.PString(groupNum)+"' "
				+"AND insplan.GroupName='"+POut.PString(groupName)+"'";
			 return Db.NonQ(command);
			 */
			//Code rewritten so that it is not only MySQL compatible, but Oracle compatible as well.
			string command="SELECT insplan.PlanNum FROM insplan,carrier "
				+"WHERE carrier.CarrierNum = insplan.CarrierNum "//employer.EmployerNum = insplan.EmployerNum "
				+"AND insplan.EmployerNum='"+POut.Long(employerNum)+"' "
				+"AND carrier.CarrierName='"+POut.String(carrierName)+"' "
				+"AND insplan.GroupNum='"+POut.String(groupNum)+"' "
				+"AND insplan.GroupName='"+POut.String(groupName)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return 0;
			}
			command="UPDATE insplan SET ";
			if(feeSchedType==FeeScheduleType.Normal){
				command+="insplan.FeeSched ="+POut.Long(feeSchedNum)
					+" WHERE insplan.FeeSched !="+POut.Long(feeSchedNum);
			}
			else if(feeSchedType==FeeScheduleType.Allowed){
				command+="insplan.AllowedFeeSched ="+POut.Long(feeSchedNum)
					+" WHERE insplan.AllowedFeeSched !="+POut.Long(feeSchedNum);
			}
			else if(feeSchedType==FeeScheduleType.CoPay){
				command+="insplan.CopayFeeSched ="+POut.Long(feeSchedNum)
					+" WHERE insplan.CopayFeeSched !="+POut.Long(feeSchedNum);
			}
			command+=" AND (";
			for(int i=0;i<table.Rows.Count;i++){
				command+="PlanNum="+table.Rows[i][0].ToString();
				if(i<table.Rows.Count-1){
					command+=" OR ";
				}
			}
			command+=")";
			return Db.NonQ(command);
		}

		///<summary>Returns number of rows affected.</summary>
		public static long ConvertToNewClaimform(long oldClaimFormNum,long newClaimFormNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),oldClaimFormNum,newClaimFormNum);
			}
			string command="UPDATE insplan SET ClaimFormNum="+POut.Long(newClaimFormNum)
				+" WHERE ClaimFormNum="+POut.Long(oldClaimFormNum);
			return Db.NonQ(command);
		}

		///<summary>Returns the number of fee schedules added.  It doesn't inform the user of how many plans were affected, but there will obviously be a certain number of plans for every new fee schedule.</summary>
		public static long GenerateAllowedFeeSchedules() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod());
			}
			//get carrier names for all plans without an allowed fee schedule.
			string command="SELECT carrier.CarrierName "
				+"FROM insplan,carrier "
				+"WHERE carrier.CarrierNum=insplan.CarrierNum "
				+"AND insplan.AllowedFeeSched=0 "
				+"AND insplan.PlanType='' "
				+"GROUP BY carrier.CarrierName";
			DataTable table=Db.GetTable(command);
			//loop through all the carrier names
			string carrierName;
			FeeSched sched;
			int itemOrder=FeeSchedC.ListLong.Count;
			DataTable tableCarrierNums;
			long retVal=0;
			for(int i=0;i<table.Rows.Count;i++){
				carrierName=PIn.String(table.Rows[i]["CarrierName"].ToString());
				if(carrierName=="" || carrierName==" "){
					continue;
				}
				//add a fee schedule if needed
				sched=FeeScheds.GetByExactName(carrierName,FeeScheduleType.Allowed);
				if(sched==null){
					sched=new FeeSched();
					sched.Description=carrierName;
					sched.FeeSchedType=FeeScheduleType.Allowed;
					//sched.IsNew=true;
					sched.ItemOrder=itemOrder;
					FeeScheds.Insert(sched);
					itemOrder++;
				}
				//assign the fee sched to many plans
				//for compatibility with Oracle, get a list of all carrierNums that use the carriername
				command="SELECT CarrierNum FROM carrier WHERE CarrierName='"+POut.String(carrierName)+"'";
				tableCarrierNums=Db.GetTable(command);
				if(tableCarrierNums.Rows.Count==0){
					continue;//I don't see how this could happen
				}
				command="UPDATE insplan "
					+"SET AllowedFeeSched="+POut.Long(sched.FeeSchedNum)+" "
					+"WHERE AllowedFeeSched=0 "
					+"AND PlanType='' "
					+"AND (";
				for(int c=0;c<tableCarrierNums.Rows.Count;c++){
					if(c>0){
						command+=" OR ";
					}
					command+="CarrierNum="+tableCarrierNums.Rows[c]["CarrierNum"].ToString();
				}
				command+=")";
				retVal+=Db.NonQ(command);
			}
			return retVal;
		}

		public static int UnusedGetCount() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command="SELECT COUNT(*) FROM insplan WHERE IsHidden=0 "
				+"AND NOT EXISTS (SELECT * FROM patplan WHERE patplan.PlanNum=insplan.PlanNum)";
			int count=PIn.Int(Db.GetCount(command));
			return count;
		}

		public static void UnusedHideAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="UPDATE insplan SET IsHidden=1 "
				+"WHERE IsHidden=0 "
				+"AND NOT EXISTS (SELECT * FROM patplan WHERE patplan.PlanNum=insplan.PlanNum)";
			Db.NonQ(command);
		}

		//public static int GenerateOneAllowedFeeSchedule(){

		//}

		///<summary>Returns -1 if no copay feeschedule.  Can return -1 if copay amount is blank.</summary>
		public static double GetCopay(string myCode,InsPlan plan) {
			//No need to check RemotingRole; no call to db.
			if(plan==null) {
				return -1;
			}
			if(plan.CopayFeeSched==0) {
				return -1;
			}
			double retVal=Fees.GetAmount(ProcedureCodes.GetCodeNum(myCode),plan.CopayFeeSched);
			if(retVal==-1) {//blank co-pay
				if(PrefC.GetBool(PrefName.CoPay_FeeSchedule_BlankLikeZero)) {
					return -1;//will act like zero.  No patient co-pay.
				} 
				else {
					//The amount from the regular fee schedule
					//In other words, the patient is responsible for procs that are not specified in a managed care fee schedule.
					return Fees.GetAmount(ProcedureCodes.GetCodeNum(myCode),plan.FeeSched);
				}
			}
			return retVal;
		}

		///<summary>Returns -1 if no copay feeschedule.  Can return -1 if copay amount is blank.</summary>
		public static double GetCopay(long codeNum,long feeSched,long copayFeeSched) {
			//No need to check RemotingRole; no call to db.
			if(copayFeeSched==0) {
				return -1;
			}
			double retVal=Fees.GetAmount(codeNum,copayFeeSched);
			if(retVal==-1) {//blank co-pay
				if(PrefC.GetBool(PrefName.CoPay_FeeSchedule_BlankLikeZero)) {
					return -1;//will act like zero.  No patient co-pay.
				}
				else {
					//The amount from the regular fee schedule
					//In other words, the patient is responsible for procs that are not specified in a managed care fee schedule.
					return Fees.GetAmount(codeNum,feeSched);
				}
			}
			return retVal;
		}

		///<summary>Returns -1 if no allowed feeschedule or fee unknown for this procCode. Otherwise, returns the allowed fee including 0. Can handle a planNum of 0.  Tooth num is used for posterior composites.  It can be left blank in some situations.  Provider must be supplied in case plan has no assigned fee schedule.  Then it will use the fee schedule for the provider.</summary>
		public static double GetAllowed(string procCodeStr,long feeSched,long allowedFeeSched,bool codeSubstNone,string planType,string toothNum,long provNum) {
			//No need to check RemotingRole; no call to db.
			//if(planNum==0) {
			//	return -1;
			//}
			//InsPlan plan=InsPlans.GetPlan(planNum,PlanList);
			//if(plan==null) {
			//	return -1;
			//}
			long codeNum=ProcedureCodes.GetCodeNum(procCodeStr);
			long substCodeNum=codeNum;
			if(!codeSubstNone) {
				substCodeNum=ProcedureCodes.GetSubstituteCodeNum(procCodeStr,toothNum);//for posterior composites
			}
			//PPO always returns the PPO fee for the code or substituted code.
			if(planType=="p") {
				return Fees.GetAmount(substCodeNum,feeSched);
			}
			//or, if not PPO, and an allowed fee schedule exists, then we use that.
			if(allowedFeeSched!=0) {
				return Fees.GetAmount(substCodeNum,allowedFeeSched);//whether post composite or not
			}
			//must be an ordinary fee schedule, so if no substitution code, then no allowed override
			if(codeNum==substCodeNum) {
				return -1;
			}
			//must be posterior composite with an ordinary fee schedule
			//Although it won't happen very often, it's possible that there is no fee schedule assigned to the plan.
			if(feeSched==0) {
				return Fees.GetAmount(substCodeNum,Providers.GetProv(provNum).FeeSched);
			}
			return Fees.GetAmount(substCodeNum,feeSched);
		}

		///<summary>Used when closing the edit plan window to find all patients using this plan and to update all claimProcs for each patient.  This keeps estimates correct.</summary>
		public static void ComputeEstimatesForTrojanPlan(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),planNum);
				return;
			}
			string command="SELECT PatNum FROM patplan WHERE PlanNum="+POut.Long(planNum);
			DataTable table=Db.GetTable(command);
			List<long> patnums=new List<long>();
			for(int i=0;i<table.Rows.Count;i++) {
				patnums.Add(PIn.Long(table.Rows[i][0].ToString()));
			}
			ComputeEstimatesForPatNums(patnums);
		}

		///<summary>Used when closing the edit plan window to find all patients using this plan and to update all claimProcs for each patient.  This keeps estimates correct.</summary>
		public static void ComputeEstimatesForSubscriber(long subscriber) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),subscriber);
				return;
			}
			string command="SELECT PatNum FROM patplan,inssub WHERE Subscriber="+POut.Long(subscriber)+" AND patplan.InsSubNum=inssub.InsSubNum";
			DataTable table=Db.GetTable(command);
			List<long> patnums=new List<long>();
			for(int i=0;i<table.Rows.Count;i++) {
				patnums.Add(PIn.Long(table.Rows[i][0].ToString()));
			}
			ComputeEstimatesForPatNums(patnums);
		}

		private static void ComputeEstimatesForPatNums(List<long> patnums){
			long patNum=0;
			for(int i=0;i<patnums.Count;i++) {
				patNum=patnums[i];
				Family fam=Patients.GetFamily(patNum);
				Patient pat=fam.GetPatient(patNum);
				List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
				List<Procedure> procs=Procedures.Refresh(patNum);
				List<InsSub> subs=InsSubs.RefreshForFam(fam);
				List<InsPlan> plans=InsPlans.RefreshForSubList(subs);
				List<PatPlan> patPlans=PatPlans.Refresh(patNum);
				List<Benefit> benefitList=Benefits.Refresh(patPlans);
				Procedures.ComputeEstimatesForAll(patNum,claimProcs,procs,plans,patPlans,benefitList,pat.Age,subs);
				Patients.SetHasIns(patNum);
			}
		}

		/// <summary>Only used from FormInsPlan. Throws ApplicationException if any dependencies. This is quite complex, because it also must update all claimprocs for all patients affected by the deletion.  Also deletes patplans, benefits, and claimprocs.</summary>
		public static void Delete(InsPlan plan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),plan);
				return;
			}
			//first, check claims
			string command="SELECT PatNum FROM claim "
				+"WHERE plannum = '"+plan.PlanNum.ToString()+"' "+DbHelper.LimitAnd(1);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count!=0) {
				throw new ApplicationException(Lans.g("FormInsPlan","Not allowed to delete a plan with existing claims."));
			}
			//then, check claimprocs
			command="SELECT PatNum FROM claimproc "
				+"WHERE PlanNum = "+POut.Long(plan.PlanNum)
				+" AND Status != 6 "//ignore estimates
				+DbHelper.LimitAnd(1);
			table=Db.GetTable(command);
			if(table.Rows.Count!=0) {
				throw new ApplicationException(Lans.g("FormInsPlan","Not allowed to delete a plan attached to procedures."));
			}
			//get a list of all patplans with this planNum
			command="SELECT PatPlanNum FROM patplan "
				+"WHERE PlanNum = "+plan.PlanNum.ToString();
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				//benefits with this PatPlanNum are also deleted here
				PatPlans.Delete(PIn.Long(table.Rows[i][0].ToString()));
			}
			command="DELETE FROM benefit WHERE PlanNum="+POut.Long(plan.PlanNum);
			Db.NonQ(command);
			command="DELETE FROM claimproc WHERE PlanNum="+POut.Long(plan.PlanNum);//just estimates
			Db.NonQ(command);
			command="DELETE FROM insplan "
				+"WHERE PlanNum = '"+plan.PlanNum.ToString()+"'";
			Db.NonQ(command);
		}

		/// <summary>Used from FormInsPlan and InsPlans.Merge. Does not check any dependencies.  Used when a new plan is created and then is no longer needed.  Also used if all dependencies have already been fixed.  Does not affect any other objects.</summary>
		public static void Delete(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),planNum);
				return;
			}
			Crud.InsPlanCrud.Delete(planNum);
		}

		/// <summary>This changes PlanNum in every place in database where it's used.  It also deletes benefits for the old planNum.</summary>
		public static void ChangeReferences(long planNum,long planNumTo) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),planNum,planNumTo);
				return;
			}
			string command;
			//change all references to the old plan to point to the new plan.
			//appointment.InsPlan1/2
			command="UPDATE appointment SET InsPlan1="+POut.Long(planNumTo)+" WHERE InsPlan1="+POut.Long(planNum);
			Db.NonQ(command);
			command="UPDATE appointment SET InsPlan2="+POut.Long(planNumTo)+" WHERE InsPlan2="+POut.Long(planNum);
			Db.NonQ(command);
			//benefit.PlanNum -- DELETE unused
			command="DELETE FROM benefit WHERE PlanNum="+POut.Long(planNum);
			Db.NonQ(command);
			//claim.PlanNum/PlanNum2
			command="UPDATE claim SET PlanNum="+POut.Long(planNumTo)+" WHERE PlanNum="+POut.Long(planNum);
			Db.NonQ(command);
			command="UPDATE claim SET PlanNum2="+POut.Long(planNumTo)+" WHERE PlanNum2="+POut.Long(planNum);
			Db.NonQ(command);
			//claimproc.PlanNum
			command="UPDATE claimproc SET PlanNum="+POut.Long(planNumTo)+" WHERE PlanNum="+POut.Long(planNum);
			Db.NonQ(command);
			//etrans.PlanNum
			command="UPDATE etrans SET PlanNum="+POut.Long(planNumTo)+" WHERE PlanNum="+POut.Long(planNum);
			Db.NonQ(command);
			//inssub.PlanNum
			command="UPDATE inssub SET PlanNum="+POut.Long(planNumTo)+" WHERE PlanNum="+POut.Long(planNum);
			Db.NonQ(command);
			//patplan.PlanNum
			command="UPDATE patplan SET PlanNum="+POut.Long(planNumTo)+" WHERE PlanNum="+POut.Long(planNum);
			Db.NonQ(command);
			//payplan.PlanNum
			command="UPDATE payplan SET PlanNum="+POut.Long(planNumTo)+" WHERE PlanNum="+POut.Long(planNum);
			Db.NonQ(command);
			//the old plan should then be deleted.
		}

		///<summary>Returns the number of plans affected.</summary>
		public static long SetAllPlansToShowUCR() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod());
			}
			string command="UPDATE insplan SET ClaimsUseUCR=1 WHERE PlanType=''";
			return Db.NonQ(command);
		}

		public static InsPlan GetByCarrierName(string carrierName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<InsPlan>(MethodBase.GetCurrentMethod(),carrierName);
			}
			string command="SELECT * FROM insplan WHERE CarrierNum=(SELECT CarrierNum FROM carrier WHERE CarrierName='"+POut.String(carrierName)+"')";
			InsPlan plan=Crud.InsPlanCrud.SelectOne(command);
			if(plan!=null) {
				return plan;
			}
			Carrier carrier=Carriers.GetByNameAndPhone(carrierName,"");
			plan=new InsPlan();
			plan.CarrierNum=carrier.CarrierNum;
			InsPlans.Insert(plan);
			return plan;
		}

	}
}