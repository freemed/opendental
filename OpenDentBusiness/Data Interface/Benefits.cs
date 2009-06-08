using CodeBase;
using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class Benefits {
		///<summary>Gets a list of all benefits for a given list of patplans for one patient.</summary>
		public static List <Benefit> Refresh(List<PatPlan> listForPat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Benefit>>(MethodBase.GetCurrentMethod(),listForPat);
			} 
			if(listForPat.Count==0) {
				return new List <Benefit> ();
			}
			string s="";
			for(int i=0;i<listForPat.Count;i++) {
				if(i>0) {
					s+=" OR";
				}
				s+=" PlanNum="+POut.PInt(listForPat[i].PlanNum);
				s+=" OR";
				s+=" PatPlanNum="+POut.PInt(listForPat[i].PatPlanNum);
			}
			string command="SELECT * FROM benefit"
				+" WHERE"+s;
			//Debug.WriteLine(command);
			DataTable table=Db.GetTable(command);
			List<Benefit> list=new List<Benefit>();
			Benefit ben;
			for(int i=0;i<table.Rows.Count;i++) {
				ben=new Benefit();
				ben.BenefitNum       = PIn.PInt(table.Rows[i][0].ToString());
				ben.PlanNum          = PIn.PInt(table.Rows[i][1].ToString());
				ben.PatPlanNum       = PIn.PInt(table.Rows[i][2].ToString());
				ben.CovCatNum        = PIn.PInt(table.Rows[i][3].ToString());
				ben.BenefitType      = (InsBenefitType)PIn.PInt(table.Rows[i][4].ToString());
				ben.Percent          = PIn.PInt(table.Rows[i][5].ToString());
				ben.MonetaryAmt      = PIn.PDouble(table.Rows[i][6].ToString());
				ben.TimePeriod       = (BenefitTimePeriod)PIn.PInt(table.Rows[i][7].ToString());
				ben.QuantityQualifier= (BenefitQuantity)PIn.PInt(table.Rows[i][8].ToString());
				ben.Quantity         = PIn.PInt(table.Rows[i][9].ToString());
				ben.CodeNum          = PIn.PInt(table.Rows[i][10].ToString());
				ben.CoverageLevel    = (BenefitCoverageLevel)PIn.PInt(table.Rows[i][11].ToString());
				list.Add(ben);
			}
			list.Sort();
			return list;
		}

		///<summary>Used in the Plan edit window to get a list of benefits for specified plan and patPlan.  patPlanNum can be 0.</summary>
		public static List<Benefit> RefreshForPlan(int planNum,int patPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Benefit>>(MethodBase.GetCurrentMethod(),planNum,patPlanNum);
			}
			string command="SELECT *"//,IFNULL(covcat.CovCatNum,0) AS covorder "
				+" FROM benefit"
				//+" LEFT JOIN covcat ON covcat.CovCatNum=benefit.CovCatNum"
				+" WHERE PlanNum = "+POut.PInt(planNum);
			if(patPlanNum!=0) {
				command+=" OR PatPlanNum = "+POut.PInt(patPlanNum);
			}
			DataTable table=Db.GetTable(command);
			List<Benefit> retVal=new List<Benefit>();
			Benefit ben;
			for(int i=0;i<table.Rows.Count;i++) {
				ben=new Benefit();
				ben.BenefitNum       = PIn.PInt(table.Rows[i][0].ToString());
				ben.PlanNum          = PIn.PInt(table.Rows[i][1].ToString());
				ben.PatPlanNum       = PIn.PInt(table.Rows[i][2].ToString());
				ben.CovCatNum        = PIn.PInt(table.Rows[i][3].ToString());
				ben.BenefitType      = (InsBenefitType)PIn.PInt(table.Rows[i][4].ToString());
				ben.Percent          = PIn.PInt(table.Rows[i][5].ToString());
				ben.MonetaryAmt      = PIn.PDouble(table.Rows[i][6].ToString());
				ben.TimePeriod       = (BenefitTimePeriod)PIn.PInt(table.Rows[i][7].ToString());
				ben.QuantityQualifier= (BenefitQuantity)PIn.PInt(table.Rows[i][8].ToString());
				ben.Quantity         = PIn.PInt(table.Rows[i][9].ToString());
				ben.CodeNum          = PIn.PInt(table.Rows[i][10].ToString());
				ben.CoverageLevel    = (BenefitCoverageLevel)PIn.PInt(table.Rows[i][11].ToString());
				retVal.Add(ben);
			}
			return retVal;
		}


		///<summary>Used in the Plan edit window to get a typical list of benefits for all identical plans.  If the supplied plan has a planNum, then that planNum will be excluded from result list.  patPlanNum can be 0.</summary>
		public static List<Benefit> RefreshForAll(InsPlan like) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Benefit>>(MethodBase.GetCurrentMethod(),like);
			}
			if(like.CarrierNum==0){
				return new List<Benefit>();
			}
			//Get planNums for all identical plans
			string command="SELECT PlanNum FROM insplan "
				+"WHERE PlanNum != "   +POut.PInt(like.PlanNum)+" "
				+"AND EmployerNum = '" +POut.PInt(like.EmployerNum)+"' "
				+"AND GroupName = '"   +POut.PString(like.GroupName)+"' "
				+"AND GroupNum = '"    +POut.PString(like.GroupNum)+"' "
				+"AND DivisionNo = '"  +POut.PString(like.DivisionNo)+"'"
				+"AND CarrierNum = '"  +POut.PInt(like.CarrierNum)+"' "
				+"AND IsMedical = '"   +POut.PBool(like.IsMedical)+"' ";
			DataTable table=Db.GetTable(command);
			string planNums="";
			for(int i=0;i<table.Rows.Count;i++) {
				if(i>0) {
					planNums+=" OR";
				}
				planNums+=" PlanNum="+table.Rows[i][0].ToString();
			}
			Benefit[] benList=new Benefit[0];
			if(table.Rows.Count>0){
				//Get all benefits for all those plans
				command="SELECT * FROM benefit WHERE"+planNums;
				table=Db.GetTable(command);
				benList=new Benefit[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++) {
					benList[i]=new Benefit();
					benList[i].BenefitNum       = PIn.PInt(table.Rows[i][0].ToString());
					benList[i].PlanNum          = PIn.PInt(table.Rows[i][1].ToString());
					benList[i].PatPlanNum       = PIn.PInt(table.Rows[i][2].ToString());
					benList[i].CovCatNum        = PIn.PInt(table.Rows[i][3].ToString());
					benList[i].BenefitType      = (InsBenefitType)PIn.PInt(table.Rows[i][4].ToString());
					benList[i].Percent          = PIn.PInt(table.Rows[i][5].ToString());
					benList[i].MonetaryAmt      = PIn.PDouble(table.Rows[i][6].ToString());
					benList[i].TimePeriod       = (BenefitTimePeriod)PIn.PInt(table.Rows[i][7].ToString());
					benList[i].QuantityQualifier= (BenefitQuantity)PIn.PInt(table.Rows[i][8].ToString());
					benList[i].Quantity         = PIn.PInt(table.Rows[i][9].ToString());
					benList[i].CodeNum          = PIn.PInt(table.Rows[i][10].ToString());
					benList[i].CoverageLevel    = (BenefitCoverageLevel)PIn.PInt(table.Rows[i][11].ToString());
				}
			}
			List<Benefit> retVal=new List<Benefit>();
			//Loop through all benefits
			bool matchFound;
			for(int i=0;i<benList.Length;i++) {
				//For each benefit, loop through retVal and compare.
				matchFound=false;
				for(int j=0;j<retVal.Count;j++) {
					if(benList[i].CompareTo(retVal[j])==0) {//if the type is equal
						matchFound=true;
						break;
					}
				}
				if(matchFound) {
					continue;
				}
				//If no match found, then add it to the return list
				retVal.Add(benList[i]);
			}
			for(int i=0;i<retVal.Count;i++) {
				retVal[i].PlanNum=like.PlanNum;//change all the planNums to match the current plan
				//all set to 0 if the plan IsForIdentical.
			}
			return retVal;
		}
	

		///<summary></summary>
		public static void Update(Benefit ben) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ben);
				return;
			}
			string command="UPDATE benefit SET " 
				+"PlanNum = '"          +POut.PInt   (ben.PlanNum)+"'"
				+",PatPlanNum = '"      +POut.PInt   (ben.PatPlanNum)+"'"
				+",CovCatNum = '"       +POut.PInt   (ben.CovCatNum)+"'"
				+",BenefitType = '"     +POut.PInt   ((int)ben.BenefitType)+"'"
				+",Percent = '"         +POut.PInt   (ben.Percent)+"'"
				+",MonetaryAmt = '"     +POut.PDouble(ben.MonetaryAmt)+"'"
				+",TimePeriod = '"      +POut.PInt   ((int)ben.TimePeriod)+"'"
				+",QuantityQualifier ='"+POut.PInt   ((int)ben.QuantityQualifier)+"'"
				+",Quantity = '"        +POut.PInt   (ben.Quantity)+"'"
				+",CodeNum = '"         +POut.PInt   (ben.CodeNum)+"'"
				+",CoverageLevel = '"   +POut.PInt   ((int)ben.CoverageLevel)+"'"
				+" WHERE BenefitNum  ='"+POut.PInt   (ben.BenefitNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static int Insert(Benefit ben) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				ben.BenefitNum=Meth.GetInt(MethodBase.GetCurrentMethod(),ben);
				return ben.BenefitNum;
			}
			if(PrefC.RandomKeys) {
				ben.BenefitNum=MiscData.GetKey("benefit","BenefitNum");
			}
			string command="INSERT INTO benefit (";
			if(PrefC.RandomKeys) {
				command+="BenefitNum,";
			}
			command+="PlanNum,PatPlanNum,CovCatNum,BenefitType,Percent,MonetaryAmt,TimePeriod,"
				+"QuantityQualifier,Quantity,CodeNum,CoverageLevel) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(ben.BenefitNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(ben.PlanNum)+"', "
				+"'"+POut.PInt(ben.PatPlanNum)+"', "
				+"'"+POut.PInt(ben.CovCatNum)+"', "
				+"'"+POut.PInt((int)ben.BenefitType)+"', "
				+"'"+POut.PInt(ben.Percent)+"', "
				+"'"+POut.PDouble(ben.MonetaryAmt)+"', "
				+"'"+POut.PInt((int)ben.TimePeriod)+"', "
				+"'"+POut.PInt((int)ben.QuantityQualifier)+"', "
				+"'"+POut.PInt(ben.Quantity)+"', "
				+"'"+POut.PInt(ben.CodeNum)+"', "
				+"'"+POut.PInt((int)ben.CoverageLevel)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				ben.BenefitNum=Db.NonQ(command,true);
			}
			return ben.BenefitNum;
		}

		///<summary></summary>
		public static void Delete(Benefit ben) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ben);
				return;
			}
			string command="DELETE FROM benefit WHERE BenefitNum ="+POut.PInt(ben.BenefitNum);
			Db.NonQ(command);
		}
		
		///<summary>Gets an annual max from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the top of the benefit list, it will get the most general limitation first.  Returns -1 if none found.  It does not discriminate between family and individual because it doesn't need to.</summary>
		public static double GetAnnualMax(List<Benefit> list,int planNum,int patPlanNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].PlanNum==0 && list[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(list[i].PatPlanNum==0 && list[i].PlanNum!=planNum) {
					continue;
				}
				if(list[i].BenefitType!=InsBenefitType.Limitations) {
					continue;
				}
				if(list[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(list[i].TimePeriod!=BenefitTimePeriod.CalendarYear && list[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				return list[i].MonetaryAmt;
			}
			return -1;
		}

		///<Summary>Returns true if there is a family max for the given plan.</Summary>
		public static bool GetIsFamMax(List <Benefit> list,int planNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].PlanNum!=planNum) {
					continue;
				}
				if(list[i].BenefitType!=InsBenefitType.Limitations) {
					continue;
				}
				if(list[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(list[i].TimePeriod!=BenefitTimePeriod.CalendarYear && list[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				if(list[i].CoverageLevel!=BenefitCoverageLevel.Family){
					continue;
				}
				return true;
			}
			return false;
		}

		///<summary>Gets a deductible from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the top of the benefit list, it will get the most general deductible first.  Does not need to discriminate between family and individual.</summary>
		public static double GetDeductible(List <Benefit> list,int planNum,int patPlanNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].PlanNum==0 && list[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(list[i].PatPlanNum==0 && list[i].PlanNum!=planNum) {
					continue;
				}
				if(list[i].BenefitType!=InsBenefitType.Deductible) {
					continue;
				}
				if(list[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(list[i].TimePeriod!=BenefitTimePeriod.CalendarYear && list[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				return list[i].MonetaryAmt;
			}
			return -1;
		}

		///<Summary>Returns true if there is a family deductible for the given plan.</Summary>
		public static bool GetIsFamDed(List <Benefit> list,int planNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].PlanNum!=planNum) {
					continue;
				}
				if(list[i].BenefitType!=InsBenefitType.Deductible) {
					continue;
				}
				if(list[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(list[i].TimePeriod!=BenefitTimePeriod.CalendarYear && list[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				if(list[i].CoverageLevel!=BenefitCoverageLevel.Family) {
					continue;
				}
				return true;
			}
			return false;
		}

		///<summary>Gets a deductible from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the bottom of the benefit list, it will get the most specific matching deductible first.</summary>
		public static double GetDeductibleByCode(List <Benefit> benList,int planNum,int patPlanNum,string code) {
			//No need to check RemotingRole; no call to db.
			CovSpan[] spansForCat;
			for(int i=benList.Count-1;i>=0;i--) {
				if(benList[i].PlanNum==0 && benList[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(benList[i].PatPlanNum==0 && benList[i].PlanNum!=planNum) {
					continue;
				}
				if(benList[i].BenefitType!=InsBenefitType.Deductible) {
					continue;
				}
				if(benList[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(benList[i].TimePeriod!=BenefitTimePeriod.CalendarYear && benList[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				if(benList[i].CovCatNum==0) {
					return benList[i].MonetaryAmt;
				}
				spansForCat=CovSpans.GetForCat(benList[i].CovCatNum);
				for(int j=0;j<spansForCat.Length;j++){
					if(String.Compare(code,spansForCat[j].FromCode)>=0
						&& String.Compare(code,spansForCat[j].ToCode)<=0)
					{
						return benList[i].MonetaryAmt;
					}
				}
			}
			return 0;
		}

		///<summary>Gets the renewal date for annual benefits from the supplied list of benefits.  Looks for a general limitation dollar amount.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the top of the benefit list, it will get the most general limitation first.  Because there is one renew date each year, the date returned will be the asofDate or earlier; the most recent renewal date.</summary>
		public static DateTime GetRenewDate(List<Benefit> list,int planNum,int patPlanNum,DateTime insStartDate,DateTime asofDate) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].PlanNum==0 && list[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(list[i].PatPlanNum==0 && list[i].PlanNum!=planNum) {
					continue;
				}
				if(list[i].BenefitType!=InsBenefitType.Limitations) {
					continue;
				}
				if(list[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(list[i].TimePeriod!=BenefitTimePeriod.CalendarYear && list[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				bool isCalendarYear=list[i].TimePeriod==BenefitTimePeriod.CalendarYear;
				return OpenDentBusiness.BenefitLogic.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			}
			return new DateTime(DateTime.Now.Year,1,1);
		}

		///<summary>Only use pri or sec, not tot.  Used from ClaimProc.ComputeBaseEst. This is a low level function to get the percent to store in a claimproc.  It does not consider any percentOverride.  Always returns a number between 0 and 100.  The supplied benefit list should be sorted frirst.</summary>
		public static int GetPercent(string myCode,InsPlan insPlan,PatPlan patPlan,List <Benefit> benList){
			//No need to check RemotingRole; no call to db.
			if(insPlan.PlanType=="f" || insPlan.PlanType=="c"){
				return 100;//flat and cap are always covered 100%
			}
			CovSpan[] spansForCat;
			//loop through benefits starting at bottom (most specific)
			for(int i=benList.Count-1;i>=0;i--){
				//if plan benefit, but no match
				if(benList[i].PlanNum!=0 && insPlan.PlanNum!=benList[i].PlanNum){
					continue;
				}
				//if patplan benefit, but no match
				if(benList[i].PatPlanNum!=0 && patPlan.PatPlanNum!=benList[i].PatPlanNum){
					continue;
				}
				if(benList[i].BenefitType!=InsBenefitType.CoInsurance){
					continue;
				}
				spansForCat=CovSpans.GetForCat(benList[i].CovCatNum);
				for(int j=0;j<spansForCat.Length;j++){
					if(String.Compare(myCode,spansForCat[j].FromCode)>=0 && String.Compare(myCode,spansForCat[j].ToCode)<=0){
						return benList[i].Percent;
					}
				}
			}
			return 0;
		}

		///<summary>Used in FormInsPlan to sych database with changes user made to the benefit list for a plan.  Must supply an old list for comparison.  Only the differences are saved.</summary>
		public static void UpdateList(List<Benefit> oldBenefitList,List<Benefit> newBenefitList){
			//No need to check RemotingRole; no call to db.
			Benefit newBenefit;
			for(int i=0;i<oldBenefitList.Count;i++){//loop through the old list
				newBenefit=null;
				for(int j=0;j<newBenefitList.Count;j++){
					if(newBenefitList[j]==null || newBenefitList[j].BenefitNum==0){
						continue;
					}
					if(oldBenefitList[i].BenefitNum==newBenefitList[j].BenefitNum){
						newBenefit=newBenefitList[j];
						break;
					}
				}
				if(newBenefit==null){
					//benefit with matching benefitNum was not found, so it must have been deleted
					Delete(oldBenefitList[i]);
					continue;
				}
				//benefit was found with matching benefitNum, so check for changes
				if(  newBenefit.PlanNum != oldBenefitList[i].PlanNum
					|| newBenefit.PatPlanNum != oldBenefitList[i].PatPlanNum
					|| newBenefit.CovCatNum != oldBenefitList[i].CovCatNum
					|| newBenefit.BenefitType != oldBenefitList[i].BenefitType
					|| newBenefit.Percent != oldBenefitList[i].Percent
					|| newBenefit.MonetaryAmt != oldBenefitList[i].MonetaryAmt
					|| newBenefit.TimePeriod != oldBenefitList[i].TimePeriod
					|| newBenefit.QuantityQualifier != oldBenefitList[i].QuantityQualifier
					|| newBenefit.Quantity != oldBenefitList[i].Quantity
					|| newBenefit.CodeNum != oldBenefitList[i].CodeNum
					|| newBenefit.CoverageLevel != oldBenefitList[i].CoverageLevel)
				{
					Benefits.Update(newBenefit);
				}
			}
			for(int i=0;i<newBenefitList.Count;i++){//loop through the new list
				if(newBenefitList[i]==null){
					continue;	
				}
				if(((Benefit)newBenefitList[i]).BenefitNum!=0){
					continue;
				}
				//benefit with benefitNum=0, so it's new
				Benefits.Insert((Benefit)newBenefitList[i]);
			}
		}

		///<summary>Used in FormInsPlan when applying changes to all identical plans.  Also used when merging plans. It first compares the old benefit list with the new one.  If there are no changes, it does nothing.  But if there are any changes, then we no longer care what the old benefit list was.  We will just delete it for all similar plans and recreate it.  Returns true if a change was made, false if no change made.</summary>
		public static bool UpdateListForIdentical(List<Benefit> oldBenefitList,List<Benefit> newBenefitList,List<int> planNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),oldBenefitList,newBenefitList,planNums);
			}
			Benefit newBenefit;
			bool changed=false;
			for(int i=0;i<newBenefitList.Count;i++) {//loop through the new list
				//look for new benefits
				if(newBenefitList[i].BenefitNum==0) {
					changed=true;
					break;
				}
			}
			if(!changed){
				for(int i=0;i<oldBenefitList.Count;i++) {//loop through the old list
					newBenefit=null;
					for(int j=0;j<newBenefitList.Count;j++) {
						if(newBenefitList[j]==null || newBenefitList[j].BenefitNum==0) {
							continue;
						}
						if(oldBenefitList[i].BenefitNum==newBenefitList[j].BenefitNum) {
							newBenefit=newBenefitList[j];
							break;
						}
					}
					if(newBenefit==null) {
						//benefit with matching benefitNum was not found, so it must have been deleted
						changed=true;
						break;
					}
					//benefit was found with matching benefitNum, so check for changes
					if(newBenefit.PlanNum             != oldBenefitList[i].PlanNum
						|| newBenefit.PatPlanNum        != oldBenefitList[i].PatPlanNum
						|| newBenefit.CovCatNum         != oldBenefitList[i].CovCatNum
						|| newBenefit.BenefitType       != oldBenefitList[i].BenefitType
						|| newBenefit.Percent           != oldBenefitList[i].Percent
						|| newBenefit.MonetaryAmt       != oldBenefitList[i].MonetaryAmt
						|| newBenefit.TimePeriod        != oldBenefitList[i].TimePeriod
						|| newBenefit.QuantityQualifier != oldBenefitList[i].QuantityQualifier
						|| newBenefit.Quantity          != oldBenefitList[i].Quantity
						|| newBenefit.CodeNum           != oldBenefitList[i].CodeNum 
						|| newBenefit.CoverageLevel     != oldBenefitList[i].CoverageLevel) 
					{
						changed=true;
						break;
					}
				}
			}
			if(!changed){
				return false;
			}
			//List<int> planNums=new List<int>();
			//planNums.AddRange(InsPlans.GetPlanNumsOfSamePlans(Employers.GetName(plan.EmployerNum),plan.GroupName,plan.GroupNum,
			//	plan.DivisionNo,Carriers.GetName(plan.CarrierNum),plan.IsMedical,plan.PlanNum,true));
			string command="";
			for(int i=0;i<planNums.Count;i++){//loop through each plan
				//delete all benefits for all identical plans
				command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(planNums[i]);
				Db.NonQ(command);
				for(int j=0;j<newBenefitList.Count;j++){//loop through the new list
					if(newBenefitList[j]==null) {
						continue;
					}
					if(((Benefit)newBenefitList[j]).PatPlanNum!=0) {
						continue;//skip benefits attached to patients.  We are only concerned with ones attached to plans.
					}
					newBenefit=((Benefit)newBenefitList[j]).Copy();
					newBenefit.PlanNum=planNums[i];
					Insert(newBenefit);
				}
			}
			return true;
			//don't forget to compute estimates for each plan now.
		}

		///<summary>Used in family module display to get a list of benefits.  The main purpose of this function is to group similar benefits for each plan on the same row, making it easier to display in a simple grid.  Supply a list of all benefits for the patient, and the patPlans for the patient.</summary>
		public static Benefit[,] GetDisplayMatrix(List <Benefit> bensForPat,List <PatPlan> patPlanList){
			//No need to check RemotingRole; no call to db.
			ArrayList AL=new ArrayList();//each object is a Benefit[]
			Benefit[] row;
			ArrayList refAL=new ArrayList();//each object is a Benefit from any random column. Used when searching for a type.
			int col;
			for(int i=0;i<bensForPat.Count;i++){
				//determine the column
				col=-1;
				for(int j=0;j<patPlanList.Count;j++){
					if(patPlanList[j].PatPlanNum==bensForPat[i].PatPlanNum
						|| patPlanList[j].PlanNum==bensForPat[i].PlanNum)
					{
						col=j;
						break;
					}
				}
				if(col==-1){
					throw new Exception("col not found");//should never happen
				}
				//search refAL for a matching type that already exists
				row=null;
				for(int j=0;j<refAL.Count;j++){
					if(((Benefit)refAL[j]).CompareTo(bensForPat[i])==0){//if the type is equivalent
						row=(Benefit[])AL[j];
						break;
					}
				}
				//if no matching type found, add a row, and use that row
				if(row==null){
					refAL.Add(bensForPat[i].Copy());
					row=new Benefit[patPlanList.Count];
					row[col]=bensForPat[i].Copy();
					AL.Add(row);
					continue;
				}
				//if the column for the matching row is null, then use that row
				if(row[col]==null){
					row[col]=bensForPat[i].Copy();
					continue;
				}
				//if not null, then add another row.
				refAL.Add(bensForPat[i].Copy());
				row=new Benefit[patPlanList.Count];
				row[col]=bensForPat[i].Copy();
				AL.Add(row);
			}
			IComparer myComparer = new BenefitArraySorter();
			AL.Sort(myComparer);
			Benefit[,] retVal=new Benefit[patPlanList.Count,AL.Count];
			for(int y=0;y<AL.Count;y++){
				for(int x=0;x<patPlanList.Count;x++){
					if(((Benefit[])AL[y])[x]!=null){
						retVal[x,y]=((Benefit[])AL[y])[x].Copy();
					}
				}
			}
			return retVal;
		}

		///<summary>Deletes all benefits for a plan from the database.  Only used in FormInsPlan when picking a plan from the list.  Need to clear out benefits so that they won't be picked up when choosing benefits for all.</summary>
		public static void DeleteForPlan(int planNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),planNum);
				return;
			}
			string command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(planNum);
			Db.NonQ(command);
		}

	}


		



		
	

	

	


}










