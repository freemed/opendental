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
				plan.PlanNum=Meth.GetInt(MethodBase.GetCurrentMethod(),plan);
				return plan.PlanNum;
			}
			if(PrefC.RandomKeys) {
				plan.PlanNum=ReplicationServers.GetKey("insplan","PlanNum");
			}
			string command= "INSERT INTO insplan (";
			if(PrefC.RandomKeys) {
				command+="PlanNum,";
			}
			command+="Subscriber,"
				+"DateEffective,DateTerm,GroupName,GroupNum,PlanNote,"
				+"FeeSched,ReleaseInfo,AssignBen,PlanType,ClaimFormNum,UseAltCode,"
				+"ClaimsUseUCR,CopayFeeSched,SubscriberID,"
				+"EmployerNum,CarrierNum,AllowedFeeSched,TrojanID,DivisionNo,BenefitNotes,IsMedical,SubscNote,FilingCode,"
				+"DentaideCardSequence,ShowBaseUnits,DedBeforePerc,CodeSubstNone,IsHidden,MonthRenew,FilingCodeSubtype"
				+") VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PLong(plan.PlanNum)+"', ";
			}
			command+=
				 "'"+POut.PLong(plan.Subscriber)+"', "
				+POut.PDate(plan.DateEffective)+", "
				+POut.PDate(plan.DateTerm)+", "
				+"'"+POut.PString(plan.GroupName)+"', "
				+"'"+POut.PString(plan.GroupNum)+"', "
				+"'"+POut.PString(plan.PlanNote)+"', "
				+"'"+POut.PLong(plan.FeeSched)+"', "
				+"'"+POut.PBool(plan.ReleaseInfo)+"', "
				+"'"+POut.PBool(plan.AssignBen)+"', "
				+"'"+POut.PString(plan.PlanType)+"', "
				+"'"+POut.PLong(plan.ClaimFormNum)+"', "
				+"'"+POut.PBool(plan.UseAltCode)+"', "
				+"'"+POut.PBool(plan.ClaimsUseUCR)+"', "
				+"'"+POut.PLong(plan.CopayFeeSched)+"', "
				+"'"+POut.PString(plan.SubscriberID)+"', "
				+"'"+POut.PLong(plan.EmployerNum)+"', "
				+"'"+POut.PLong(plan.CarrierNum)+"', "
				+"'"+POut.PLong(plan.AllowedFeeSched)+"', "
				+"'"+POut.PString(plan.TrojanID)+"', "
				+"'"+POut.PString(plan.DivisionNo)+"', "
				+"'"+POut.PString(plan.BenefitNotes)+"', "
				+"'"+POut.PBool(plan.IsMedical)+"', "
				+"'"+POut.PString(plan.SubscNote)+"', "
				+"'"+POut.PLong(plan.FilingCode)+"', "
				+"'"+POut.PLong((int)plan.DentaideCardSequence)+"', "
				+"'"+POut.PBool(plan.ShowBaseUnits)+"', "
				+"'"+POut.PBool(plan.DedBeforePerc)+"', "
				+"'"+POut.PBool(plan.CodeSubstNone)+"', "
				+"'"+POut.PBool(plan.IsHidden)+"', "
				+"'"+POut.PLong(plan.MonthRenew)+"',"
				+"'"+POut.PLong(plan.FilingCodeSubtype)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				plan.PlanNum=Db.NonQ(command,true);
			}
			return plan.PlanNum;
		}

		///<summary></summary>
		public static void Update(InsPlan plan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),plan);
				return;
			}
			string command="UPDATE insplan SET "
				+"Subscriber = '"    +POut.PLong   (plan.Subscriber)+"'"
				+",DateEffective = "+POut.PDate  (plan.DateEffective)
				+",DateTerm = "     +POut.PDate  (plan.DateTerm)
				+",GroupName = '"    +POut.PString(plan.GroupName)+"'"
				+",GroupNum = '"     +POut.PString(plan.GroupNum)+"'"
				+",PlanNote = '"     +POut.PString(plan.PlanNote)+"'"
				+",FeeSched = '"     +POut.PLong   (plan.FeeSched)+"'"
				+",ReleaseInfo = '"  +POut.PBool  (plan.ReleaseInfo)+"'"
				+",AssignBen = '"    +POut.PBool  (plan.AssignBen)+"'"
				+",PlanType = '"     +POut.PString(plan.PlanType)+"'"
				+",ClaimFormNum = '" +POut.PLong   (plan.ClaimFormNum)+"'"
				+",UseAltcode = '"   +POut.PBool  (plan.UseAltCode)+"'"
				+",ClaimsUseUCR = '" +POut.PBool  (plan.ClaimsUseUCR)+"'"
				+",CopayFeeSched = '"+POut.PLong   (plan.CopayFeeSched)+"'"
				+",SubscriberID = '" +POut.PString(plan.SubscriberID)+"'"
				+",EmployerNum = '"  +POut.PLong   (plan.EmployerNum)+"'"
				+",CarrierNum = '"   +POut.PLong   (plan.CarrierNum)+"'"
				+",AllowedFeeSched='"+POut.PLong   (plan.AllowedFeeSched)+"'"
				+",TrojanID='"       +POut.PString(plan.TrojanID)+"'"
				+",DivisionNo='"     +POut.PString(plan.DivisionNo)+"'"
				+",BenefitNotes='"   +POut.PString(plan.BenefitNotes)+"'"
				+",IsMedical='"      +POut.PBool  (plan.IsMedical)+"'"
				+",SubscNote='"      +POut.PString(plan.SubscNote)+"'"
				+",FilingCode='"     +POut.PLong(plan.FilingCode)+"'"
				+",DentaideCardSequence='" +POut.PLong(plan.DentaideCardSequence)+"'"
				+",ShowBaseUnits='"  +POut.PBool(plan.ShowBaseUnits)+"'"
				+",DedBeforePerc='"  +POut.PBool(plan.DedBeforePerc)+"'"
				+",CodeSubstNone='"  +POut.PBool(plan.CodeSubstNone)+"'"
				+",IsHidden='"       +POut.PBool(plan.IsHidden)+"'"
				+",MonthRenew='"     +POut.PLong(plan.MonthRenew)+"'"
				+",FilingCodeSubtype='"		+POut.PLong(plan.FilingCodeSubtype)+"'"
				+" WHERE PlanNum = '"+POut.PLong   (plan.PlanNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Called from FormInsPlan when applying changes to all identical insurance plans. This updates the synchronized fields for all plans like the specified insPlan.  Current InsPlan must be set to the new values that we want.  BenefitNotes and SubscNote are specific to subscriber and are not changed.  PlanNotes are handled separately in a different function after this one is complete.</summary>
		public static void UpdateForLike(InsPlan like, InsPlan plan) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),like,plan);
				return;
			}
			string command= "UPDATE insplan SET "
				+"EmployerNum = '"     +POut.PLong   (plan.EmployerNum)+"'"
				+",GroupName = '"      +POut.PString(plan.GroupName)+"'"
				+",GroupNum = '"       +POut.PString(plan.GroupNum)+"'"
				+",DivisionNo = '"     +POut.PString(plan.DivisionNo)+"'"
				+",CarrierNum = '"     +POut.PLong   (plan.CarrierNum)+"'"
				+",PlanType = '"       +POut.PString(plan.PlanType)+"'"
				+",UseAltCode = '"     +POut.PBool  (plan.UseAltCode)+"'"
				+",IsMedical = '"      +POut.PBool  (plan.IsMedical)+"'"
				+",ClaimsUseUCR = '"   +POut.PBool  (plan.ClaimsUseUCR)+"'"
				+",FeeSched = '"       +POut.PLong   (plan.FeeSched)+"'"
				+",CopayFeeSched = '"  +POut.PLong   (plan.CopayFeeSched)+"'"
				+",ClaimFormNum = '"   +POut.PLong   (plan.ClaimFormNum)+"'"
				+",AllowedFeeSched= '" +POut.PLong   (plan.AllowedFeeSched)+"'"
				+",TrojanID = '"       +POut.PString(plan.TrojanID)+"'"
				+",FilingCode = '"     +POut.PLong   (plan.FilingCode)+"'"
				+",ShowBaseUnits = '"  +POut.PBool  (plan.ShowBaseUnits)+"'"
				+",ShowBaseUnits = '"  +POut.PBool  (plan.DedBeforePerc)+"'"
				+",CodeSubstNone='"    +POut.PBool  (plan.CodeSubstNone)+"'"
				+",IsHidden='"         +POut.PBool  (plan.IsHidden)+"'"
				//MonthRenew would be different between similar plans
				+" WHERE "
				+"EmployerNum = '"        +POut.PLong   (like.EmployerNum)+"' "
				+"AND GroupName = '"      +POut.PString(like.GroupName)+"' "
				+"AND GroupNum = '"       +POut.PString(like.GroupNum)+"' "
				+"AND DivisionNo = '"     +POut.PString(like.DivisionNo)+"'"
				+"AND CarrierNum = '"     +POut.PLong   (like.CarrierNum)+"' "
				+"AND IsMedical = '"      +POut.PBool  (like.IsMedical)+"'";
			Db.NonQ(command);
		}

		///<summary>It's fastest if you supply a plan list that contains the plan, but it also works just fine if it can't initally locate the plan in the list.  You can supply an array of length 0.  If still not found, returns null.</summary>
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
				retPlan=Refresh(planNum);//retPlan will now be null if not found
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
			string command="SELECT * FROM insplan WHERE TrojanID = '"+POut.PString(trojanID)+"'";
			DataTable table=Db.GetTable(command);
			return RefreshFill(table).ToArray();
		}

		///<summary>Used in FormInsSelectSubscr to get a list of insplans for one subscriber directly from the database.</summary>
		public static List <InsPlan> GetListForSubscriber(long subscriber) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return MiscUtils.ArrayToList(Meth.GetObject<InsPlan[]>(MethodBase.GetCurrentMethod(),subscriber));
			} 
			string command="SELECT * FROM insplan WHERE Subscriber="+POut.PLong(subscriber);
			DataTable table=Db.GetTable(command);
			return RefreshFill(table);
		}

		///<summary>Only loads one plan from db. Can return null.</summary>
		private static InsPlan Refresh(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<InsPlan>(MethodBase.GetCurrentMethod(),planNum);
			} 
			if(planNum==0)
				return null;
			string command="SELECT * FROM insplan WHERE plannum = '"+planNum+"'";
			DataTable table=Db.GetTable(command);
			List<InsPlan> planList=RefreshFill(table);
			if(planList.Count>0) {
				return planList[0].Copy();
			}
			else {
				return null;
			}
		}

		///<summary>Gets new List for the specified family.  The only plans it misses are for claims with no current coverage.  These are handled as needed.</summary>
		public static List<InsPlan> Refresh(Family Fam) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InsPlan>>(MethodBase.GetCurrentMethod(),Fam);
			} 
			string command=
				"(SELECT * from insplan "
				+"WHERE";
			//subscribers in family
			for(int i=0;i<Fam.ListPats.Length;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" Subscriber="+POut.PLong(Fam.ListPats[i].PatNum);
			}
			//in union, distinct is implied
			command+=") UNION (SELECT insplan.* FROM insplan,patplan WHERE insplan.PlanNum=patplan.PlanNum AND (";
			for(int i=0;i<Fam.ListPats.Length;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" patplan.PatNum="+POut.PLong(Fam.ListPats[i].PatNum);
			}
			//command+=")) ORDER BY DateEffective";//FIXME:UNION-ORDER-BY
			command+=")) ORDER BY 3";//***ORACLE ORDINAL
			//Debug.WriteLine(command);
			DataTable table=Db.GetTable(command);
			return RefreshFill(table);
		}

		private static List<InsPlan> RefreshFill(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<InsPlan> planList=new List<InsPlan>();
			InsPlan plan;
			for(int i=0;i<table.Rows.Count;i++) {
				plan=new InsPlan();
				plan.PlanNum        = PIn.PLong   (table.Rows[i][0].ToString());
				plan.Subscriber     = PIn.PLong   (table.Rows[i][1].ToString());
				plan.DateEffective  = PIn.PDate  (table.Rows[i][2].ToString());
				plan.DateTerm       = PIn.PDate  (table.Rows[i][3].ToString());
				plan.GroupName      = PIn.PString(table.Rows[i][4].ToString());
				plan.GroupNum       = PIn.PString(table.Rows[i][5].ToString());
				plan.PlanNote       = PIn.PString(table.Rows[i][6].ToString());
				plan.FeeSched       = PIn.PLong   (table.Rows[i][7].ToString());
				plan.ReleaseInfo    = PIn.PBool  (table.Rows[i][8].ToString());
				plan.AssignBen      = PIn.PBool  (table.Rows[i][9].ToString());
				plan.PlanType       = PIn.PString(table.Rows[i][10].ToString());
				plan.ClaimFormNum   = PIn.PLong   (table.Rows[i][11].ToString());
				plan.UseAltCode     = PIn.PBool  (table.Rows[i][12].ToString());
				plan.ClaimsUseUCR   = PIn.PBool  (table.Rows[i][13].ToString());
				plan.CopayFeeSched  = PIn.PLong   (table.Rows[i][14].ToString());
				plan.SubscriberID   = PIn.PString(table.Rows[i][15].ToString());
				plan.EmployerNum    = PIn.PLong   (table.Rows[i][16].ToString());
				plan.CarrierNum     = PIn.PLong   (table.Rows[i][17].ToString());
				plan.AllowedFeeSched= PIn.PLong   (table.Rows[i][18].ToString());
				plan.TrojanID       = PIn.PString(table.Rows[i][19].ToString());
				plan.DivisionNo     = PIn.PString(table.Rows[i][20].ToString());
				plan.BenefitNotes   = PIn.PString(table.Rows[i][21].ToString());
				plan.IsMedical      = PIn.PBool  (table.Rows[i][22].ToString());
				plan.SubscNote      = PIn.PString(table.Rows[i][23].ToString());
				plan.FilingCode     = PIn.PLong   (table.Rows[i][24].ToString());
				plan.DentaideCardSequence= PIn.PInt(table.Rows[i][25].ToString());
				plan.ShowBaseUnits  = PIn.PBool  (table.Rows[i][26].ToString());
				plan.DedBeforePerc  = PIn.PBool  (table.Rows[i][27].ToString());
				plan.CodeSubstNone  = PIn.PBool  (table.Rows[i][28].ToString());
				plan.IsHidden       = PIn.PBool(table.Rows[i][29].ToString());
				plan.MonthRenew     = PIn.PInt(table.Rows[i][30].ToString());
				plan.FilingCodeSubtype = PIn.PLong(table.Rows[i][31].ToString());
				planList.Add(plan);
			}
			return planList;
		}

		///<summary>Gets a description of the specified plan, including carrier name and subscriber. It's fastest if you supply a plan list that contains the plan, but it also works just fine if it can't initally locate the plan in the list.  You can supply an array of length 0 for both family and planlist.</summary>
		public static string GetDescript(long planNum,Family family,List<InsPlan> planList) {
			//No need to check RemotingRole; no call to db.
			if(planNum==0)
				return "";
			InsPlan plan=GetPlan(planNum,planList);
			if(plan==null || plan.PlanNum==0) {
				return "";
			}
			string subscriber=family.GetNameInFamFL(plan.Subscriber);
			if(subscriber=="") {//subscriber from another family
				subscriber=Patients.GetLim(plan.Subscriber).GetNameLF();
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
			if(otherFam)//retStr=="")
				retStr="(other fam):";
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
			double insUsed=GetInsUsedDisplay(histList,asofDate,planNum,patPlanNum,excludeClaim,planList);
			InsPlan plan=InsPlans.GetPlan(planNum,planList);
			double insPending=GetPendingDisplay(histList,asofDate,plan,patPlanNum,excludeClaim,patNum);
			double annualMax=Benefits.GetAnnualMaxDisplay(benList,planNum,patPlanNum);
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
			for(int i=0;i<histList.Count;i++) {
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

		/// <summary>Only for display purposes rather than for calculations.  Get insurance benefits used for one benefit year.  Must supply all relevant hist for the patient.  asofDate is used to determine which benefit year to calc.  Usually date of service for a claim.  The insplan.PlanNum is the plan to get value for.  ExcludeClaim is the ClaimNum to exclude, or enter -1 to include all.</summary>
		public static double GetInsUsedDisplay(List<ClaimProcHist> histList,DateTime asofDate,long planNum,long patPlanNum,long excludeClaim,List<InsPlan> planList) {
			//No need to check RemotingRole; no call to db.
			InsPlan curPlan=GetPlan(planNum,planList);
			if(curPlan==null) {
				return 0;
			}
			//get the most recent renew date, possibly including today:
			DateTime renewDate=BenefitLogic.ComputeRenewDate(asofDate,curPlan.MonthRenew);
			DateTime stopDate=renewDate.AddYears(1);
			double retVal=0;
			for(int i=0;i<histList.Count;i++) {
				if(histList[i].PlanNum==planNum
					&& histList[i].ClaimNum != excludeClaim
					&& histList[i].ProcDate < stopDate
					&& histList[i].ProcDate >= renewDate
					//enum ClaimProcStatus{NotReceived,Received,Preauth,Adjustment,Supplemental}
					) {
					if(histList[i].Status==ClaimProcStatus.Received 
						|| histList[i].Status==ClaimProcStatus.Adjustment
						|| histList[i].Status==ClaimProcStatus.Supplemental) 
					{
						retVal+=histList[i].Amount;
					}
					else {//NotReceived
						//retVal-=ClaimProcList[i].InsPayEst;
					}
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
				plannum=PIn.PLong(table.Rows[i][0].ToString());
				carrierName=PIn.PString(table.Rows[i][1].ToString());
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

		///<summary>Gets all distinct notes for the planNums supplied.  Supply a planNum to exclude it.  Only called when closing FormInsPlan.  Includes blank notes.</summary>
		public static string[] GetNotesForPlans(List<long> planNums,long excludePlanNum) {
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
				s+=" PlanNum="+POut.PLong(planNums[i]);
			}
			string command="SELECT DISTINCT PlanNote FROM insplan WHERE"+s;
			DataTable table=Db.GetTable(command);
			string[] retVal=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				retVal[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		///<summary>Called when closing FormInsPlan to set the PlanNote for multiple plans at once.</summary>
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
				s+=" PlanNum="+POut.PLong(planNums[i]);
			}
			string command="UPDATE insplan SET PlanNote='"+POut.PString(newNote)+"' "
				+"WHERE"+s;
			Db.NonQ(command);
		}

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
				s+=" PlanNum="+POut.PLong(planNums[i]);
			}
			string command="SELECT BenefitNotes FROM insplan WHERE BenefitNotes != '' AND ("+s+") ";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command+="AND ROWNUM<=1";
			}else{//Assume MySQL
				command+="LIMIT 1";
			}
			DataTable table=Db.GetTable(command);
			//string[] retVal=new string[];
			if(table.Rows.Count==0){
				return "";
			}
			return PIn.PString(table.Rows[0][0].ToString());
		}

		///<summary>Only used once.  Gets a list of subscriber names from the database that have identical plan info as this one. Used to display in the insplan window.  The returned list never includes the plan that we're viewing.  Use excludePlan for this purpose; it's more consistent, because we have no way of knowing if the current plan will be picked up or not.</summary>
		public static string[] GetSubscribersForSamePlans(string employerName, string groupName, string groupNum,
				string divisionNo,string carrierName,bool isMedical,long excludePlan)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<string[]>(MethodBase.GetCurrentMethod(),employerName,groupName,groupNum,divisionNo,carrierName,isMedical,excludePlan);
			}
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) "
				+"FROM patient "
				+"LEFT JOIN insplan ON patient.PatNum=insplan.Subscriber "
				+"LEFT JOIN carrier ON carrier.CarrierNum = insplan.CarrierNum "
				+"LEFT JOIN employer ON employer.EmployerNum = insplan.EmployerNum ";
			if(employerName==""){
				command+="WHERE employer.EmpName IS NULL ";
			}
			else{
				command+="WHERE employer.EmpName = '"+POut.PString(employerName)+"' ";
			}
			command+="AND insplan.GroupName = '"  +POut.PString(groupName)+"' "
				+"AND insplan.GroupNum = '"   +POut.PString(groupNum)+"' "
				+"AND insplan.DivisionNo = '" +POut.PString(divisionNo)+"' "
				+"AND carrier.CarrierName = '"+POut.PString(carrierName)+"' "
				+"AND insplan.IsMedical = '"  +POut.PBool(isMedical)+"' "
				+"AND insplan.PlanNum != "    +POut.PLong(excludePlan)
				+" ORDER BY LName,FName";
			DataTable table=Db.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

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
				command+="WHERE employer.EmpName = '"+POut.PString(employerName)+"' ";
			}
			command+="AND insplan.GroupName = '"  +POut.PString(groupName)+"' "
				+"AND insplan.GroupNum = '"   +POut.PString(groupNum)+"' "
				+"AND insplan.DivisionNo = '" +POut.PString(divisionNo)+"' "
				+"AND carrier.CarrierName = '"+POut.PString(carrierName)+"' "
				+"AND insplan.IsMedical = '"  +POut.PBool  (isMedical)+"'"
				+"AND insplan.PlanNum != "+POut.PLong(planNum);
			DataTable table=Db.GetTable(command);
			List<long> retVal=new List<long>();
			//if(includePlanNum){
			//	retVal=new int[table.Rows.Count+1];
			//}
			//else{
			//	retVal=new int[table.Rows.Count];
			//}
			for(int i=0;i<table.Rows.Count;i++) {
				retVal.Add(PIn.PLong(table.Rows[i][0].ToString()));
			}
			if(includePlanNum){
				retVal.Add(planNum);
			}
			return retVal;
		}

		///<summary>Used from FormInsPlans to get a big list of many plans, organized by carrier name or by employer.  Identical plans are grouped as one row.</summary>
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
			table.Columns.Add("plans");
			table.Columns.Add("State");
			table.Columns.Add("trojanID");
			table.Columns.Add("Zip");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT carrier.Address,carrier.City,CarrierName,ElectID,EmpName,GroupName,GroupNum,NoSendElect,"
				+"carrier.Phone,MAX(PlanNum) onePlanNum,"//for Oracle
				+"COUNT(*) plans,carrier.State,TrojanID,carrier.Zip, "
				+"CASE WHEN (EmpName IS NULL) THEN 1 ELSE 0 END as haveName "//for Oracle
				+"FROM insplan "
				+"LEFT JOIN employer ON employer.EmployerNum = insplan.EmployerNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum = insplan.CarrierNum "
				+"WHERE CarrierName LIKE '%"+POut.PString(carrierName)+"%' ";
			if(empName!="") {
				command+="AND EmpName LIKE '%"+POut.PString(empName)+"%' ";
			}
			if(groupName!="") {
				command+="AND GroupName LIKE '%"+POut.PString(groupName)+"%' ";
			}
			if(groupNum!="") {
				command+="AND GroupNum LIKE '%"+POut.PString(groupNum)+"%' ";
			}
			if(!showHidden){
				command+="AND insplan.IsHidden=0 ";
			}
			command+="GROUP BY insplan.EmployerNum,GroupName,GroupNum,DivisionNo,"
				+"insplan.CarrierNum,insplan.IsMedical,TrojanID ";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command+=",carrier.Address,carrier.City,CarrierName,ElectID,EmpName,NoSendElect,carrier.Phone,carrier.State,carrier.Zip ";
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
				}	else{
					row["noSendElect"]="X";
				}
				row["Phone"]=rawT.Rows[i]["Phone"].ToString();
				row["PlanNum"]=rawT.Rows[i]["onePlanNum"].ToString();
				row["plans"]=rawT.Rows[i]["plans"].ToString();
				row["State"]=rawT.Rows[i]["State"].ToString();
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
				+"WHERE carrier.CarrierName LIKE '%"+POut.PString(carrierName)+"%' ";
			if(carrierNameNot!=""){
				command+="AND carrier.CarrierName NOT LIKE '%"+POut.PString(carrierNameNot)+"%' ";
			}
			if(feeSchedWithout!=0){
				command+="AND insplan."+pFeeSched+" !="+POut.PLong(feeSchedWithout)+" ";
			}
			if(feeSchedWith!=0) {
				command+="AND insplan."+pFeeSched+" ="+POut.PLong(feeSchedWith)+" ";
			}
			command+="GROUP BY insplan.EmployerNum,insplan.GroupName,insplan.GroupNum,carrier.CarrierName,insplan.PlanType,"
				+"insplan."+pFeeSched+" "
				+"ORDER BY carrier.CarrierName,employer.EmpName,insplan.GroupNum";
			return Db.GetTable(command);
		}

		///<summary>Based on the four supplied parameters, it updates all similar plans.  Used in a specific tool: FormFeesForIns.</summary>
		public static long SetFeeSched(long employerNum,string carrierName,string groupNum,string groupName,long feeSchedNum,
			FeeScheduleType feeSchedType)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),employerNum,carrierName,groupNum,groupName,feeSchedNum,feeSchedType);
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
				+"AND insplan.EmployerNum='"+POut.PLong(employerNum)+"' "
				+"AND carrier.CarrierName='"+POut.PString(carrierName)+"' "
				+"AND insplan.GroupNum='"+POut.PString(groupNum)+"' "
				+"AND insplan.GroupName='"+POut.PString(groupName)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return 0;
			}
			command="UPDATE insplan SET ";
			if(feeSchedType==FeeScheduleType.Normal){
				command+="insplan.FeeSched ="+POut.PLong(feeSchedNum)
					+" WHERE insplan.FeeSched !="+POut.PLong(feeSchedNum);
			}
			else if(feeSchedType==FeeScheduleType.Allowed){
				command+="insplan.AllowedFeeSched ="+POut.PLong(feeSchedNum)
					+" WHERE insplan.AllowedFeeSched !="+POut.PLong(feeSchedNum);
			}
			else if(feeSchedType==FeeScheduleType.CoPay){
				command+="insplan.CopayFeeSched ="+POut.PLong(feeSchedNum)
					+" WHERE insplan.CopayFeeSched !="+POut.PLong(feeSchedNum);
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
				return Meth.GetInt(MethodBase.GetCurrentMethod(),oldClaimFormNum,newClaimFormNum);
			}
			string command="UPDATE insplan SET ClaimFormNum="+POut.PLong(newClaimFormNum)
				+" WHERE ClaimFormNum="+POut.PLong(oldClaimFormNum);
			return Db.NonQ(command);
		}

		///<summary>Returns the number of fee schedules added.  It doesn't inform the user of how many plans were affected, but there will obviously be a certain number of plans for every new fee schedule.</summary>
		public static long GenerateAllowedFeeSchedules() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
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
				carrierName=PIn.PString(table.Rows[i]["CarrierName"].ToString());
				if(carrierName=="" || carrierName==" "){
					continue;
				}
				//add a fee schedule if needed
				sched=FeeScheds.GetByExactName(carrierName,FeeScheduleType.Allowed);
				if(sched==null){
					sched=new FeeSched();
					sched.Description=carrierName;
					sched.FeeSchedType=FeeScheduleType.Allowed;
					sched.IsNew=true;
					sched.ItemOrder=itemOrder;
					FeeScheds.WriteObject(sched);
					itemOrder++;
				}
				//assign the fee sched to many plans
				//for compatibility with Oracle, get a list of all carrierNums that use the carriername
				command="SELECT CarrierNum FROM carrier WHERE CarrierName='"+POut.PString(carrierName)+"'";
				tableCarrierNums=Db.GetTable(command);
				if(tableCarrierNums.Rows.Count==0){
					continue;//I don't see how this could happen
				}
				command="UPDATE insplan "
					+"SET AllowedFeeSched="+POut.PLong(sched.FeeSchedNum)+" "
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
				return Meth.GetInt32(MethodBase.GetCurrentMethod());
			}
			string command="SELECT COUNT(*) FROM insplan WHERE IsHidden=0 "
				+"AND NOT EXISTS (SELECT * FROM patplan WHERE patplan.PlanNum=insplan.PlanNum)";
			int count=PIn.PInt(Db.GetCount(command));
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
				if(PrefC.GetBool("CoPay_FeeSchedule_BlankLikeZero")) {
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
				if(PrefC.GetBool("CoPay_FeeSchedule_BlankLikeZero")) {
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
		public static void ComputeEstimatesForPlan(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),planNum);
				return;
			}
			string command="SELECT PatNum FROM patplan WHERE PlanNum="+POut.PLong(planNum);
			DataTable table=Db.GetTable(command);
			long patNum=0;
			for(int i=0;i<table.Rows.Count;i++) {
				patNum=PIn.PLong(table.Rows[i][0].ToString());
				Family fam=Patients.GetFamily(patNum);
				Patient pat=fam.GetPatient(patNum);
				List<ClaimProc> claimProcs=ClaimProcs.Refresh(patNum);
				List<Procedure> procs=Procedures.Refresh(patNum);
				List<InsPlan> plans=InsPlans.Refresh(fam);
				List<PatPlan> patPlans=PatPlans.Refresh(patNum);
				List<Benefit> benefitList=Benefits.Refresh(patPlans);
				Procedures.ComputeEstimatesForAll(patNum,claimProcs,procs,plans,patPlans,benefitList,pat.Age);
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
				+"WHERE plannum = '"+plan.PlanNum.ToString()+"' ";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+="AND ROWNUM<=1";
			}
			else {//Assume MySQL
				command+="LIMIT 1";
			}
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count!=0) {
				throw new ApplicationException(Lans.g("FormInsPlan","Not allowed to delete a plan with existing claims."));
			}
			//then, check claimprocs
			command="SELECT PatNum FROM claimproc "
				+"WHERE PlanNum = "+POut.PLong(plan.PlanNum)
				+" AND Status != 6 ";//ignore estimates
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+="AND ROWNUM<=1";
			}
			else {//Assume MySQL
				command+="LIMIT 1";
			}
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
				PatPlans.Delete(PIn.PLong(table.Rows[i][0].ToString()));
			}
			command="DELETE FROM benefit WHERE PlanNum="+POut.PLong(plan.PlanNum);
			Db.NonQ(command);
			command="DELETE FROM claimproc WHERE PlanNum="+POut.PLong(plan.PlanNum);//just estimates
			Db.NonQ(command);
			command="DELETE FROM insplan "
				+"WHERE PlanNum = '"+plan.PlanNum.ToString()+"'";
			Db.NonQ(command);
		}

		public static long SetDeductBeforePercentAll(bool checkDeductibleBeforePercentChecked) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),checkDeductibleBeforePercentChecked);
			}
			string command="UPDATE insplan SET DedBeforePerc=";
			if(checkDeductibleBeforePercentChecked) {
				command+="1";
			}
			else {
				command+="0";
			}
			long result=Db.NonQ(command);
			return result;
		}

	}
}