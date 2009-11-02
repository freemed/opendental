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
				s+=" PlanNum="+POut.PLong(listForPat[i].PlanNum);
				s+=" OR";
				s+=" PatPlanNum="+POut.PLong(listForPat[i].PatPlanNum);
			}
			string command="SELECT * FROM benefit"
				+" WHERE"+s;
			//Debug.WriteLine(command);
			DataTable table=Db.GetTable(command);
			List<Benefit> list=new List<Benefit>();
			Benefit ben;
			for(int i=0;i<table.Rows.Count;i++) {
				ben=new Benefit();
				ben.BenefitNum       = PIn.PLong(table.Rows[i][0].ToString());
				ben.PlanNum          = PIn.PLong(table.Rows[i][1].ToString());
				ben.PatPlanNum       = PIn.PLong(table.Rows[i][2].ToString());
				ben.CovCatNum        = PIn.PLong(table.Rows[i][3].ToString());
				ben.BenefitType      = (InsBenefitType)PIn.PLong(table.Rows[i][4].ToString());
				ben.Percent          = PIn.PInt(table.Rows[i][5].ToString());
				ben.MonetaryAmt      = PIn.PDouble(table.Rows[i][6].ToString());
				ben.TimePeriod       = (BenefitTimePeriod)PIn.PLong(table.Rows[i][7].ToString());
				ben.QuantityQualifier= (BenefitQuantity)PIn.PLong(table.Rows[i][8].ToString());
				ben.Quantity         = PIn.PInt(table.Rows[i][9].ToString());
				ben.CodeNum          = PIn.PLong(table.Rows[i][10].ToString());
				ben.CoverageLevel    = (BenefitCoverageLevel)PIn.PLong(table.Rows[i][11].ToString());
				list.Add(ben);
			}
			list.Sort();
			return list;
		}

		///<summary>Used in the PlanEdit and FormClaimProc to get a list of benefits for specified plan and patPlan.  patPlanNum can be 0.</summary>
		public static List<Benefit> RefreshForPlan(long planNum,long patPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Benefit>>(MethodBase.GetCurrentMethod(),planNum,patPlanNum);
			}
			string command="SELECT *"//,IFNULL(covcat.CovCatNum,0) AS covorder "
				+" FROM benefit"
				//+" LEFT JOIN covcat ON covcat.CovCatNum=benefit.CovCatNum"
				+" WHERE PlanNum = "+POut.PLong(planNum);
			if(patPlanNum!=0) {
				command+=" OR PatPlanNum = "+POut.PLong(patPlanNum);
			}
			DataTable table=Db.GetTable(command);
			List<Benefit> retVal=new List<Benefit>();
			Benefit ben;
			for(int i=0;i<table.Rows.Count;i++) {
				ben=new Benefit();
				ben.BenefitNum       = PIn.PLong(table.Rows[i][0].ToString());
				ben.PlanNum          = PIn.PLong(table.Rows[i][1].ToString());
				ben.PatPlanNum       = PIn.PLong(table.Rows[i][2].ToString());
				ben.CovCatNum        = PIn.PLong(table.Rows[i][3].ToString());
				ben.BenefitType      = (InsBenefitType)PIn.PLong(table.Rows[i][4].ToString());
				ben.Percent          = PIn.PInt(table.Rows[i][5].ToString());
				ben.MonetaryAmt      = PIn.PDouble(table.Rows[i][6].ToString());
				ben.TimePeriod       = (BenefitTimePeriod)PIn.PLong(table.Rows[i][7].ToString());
				ben.QuantityQualifier= (BenefitQuantity)PIn.PLong(table.Rows[i][8].ToString());
				ben.Quantity         = PIn.PInt(table.Rows[i][9].ToString());
				ben.CodeNum          = PIn.PLong(table.Rows[i][10].ToString());
				ben.CoverageLevel    = (BenefitCoverageLevel)PIn.PLong(table.Rows[i][11].ToString());
				retVal.Add(ben);
			}
			return retVal;
		}


		///<summary>Used in the Plan edit window to get a typical list of benefits for all identical plans.  It used to exclude the supplied plan from the benefits, but no longer does that.  This behavior needs to be watched closely for possible bugs.</summary>
		public static List<Benefit> RefreshForAll(InsPlan like) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Benefit>>(MethodBase.GetCurrentMethod(),like);
			}
			if(like.CarrierNum==0){
				return new List<Benefit>();
			}

			//We might try creating a temporary table out of the matched insurance plans, then join with
			//the benefits table so that the query could be sped up.
			//Get benefits for all identical plans
			string command="SELECT b.BenefitNum,b.PlanNum,b.PatPlanNum,b.CovCatNum,b.BenefitType,"
				+"b.Percent,b.MonetaryAmt,b.TimePeriod,b.QuantityQualifier,b.Quantity,b.CodeNum,b.CoverageLevel "
				+"FROM insplan i,benefit b "
				//+"WHERE PlanNum != "   +POut.PInt(like.PlanNum)+" "
				+"WHERE i.PlanNum=b.PlanNum "
				+"AND i.EmployerNum = '"+POut.PLong(like.EmployerNum)+"' "
				+"AND i.GroupName = '"+POut.PString(like.GroupName)+"' "
				+"AND i.GroupNum = '"+POut.PString(like.GroupNum)+"' "
				+"AND i.DivisionNo = '"+POut.PString(like.DivisionNo)+"'"
				+"AND i.CarrierNum = '"+POut.PLong(like.CarrierNum)+"' "
				+"AND i.IsMedical = '"+POut.PBool(like.IsMedical)+"' ";
			DataTable table=Db.GetTable(command);
			Benefit[] benList=new Benefit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				benList[i]=new Benefit();
				benList[i].BenefitNum=PIn.PLong(table.Rows[i][0].ToString());
				benList[i].PlanNum=PIn.PLong(table.Rows[i][1].ToString());
				benList[i].PatPlanNum=PIn.PLong(table.Rows[i][2].ToString());
				benList[i].CovCatNum=PIn.PLong(table.Rows[i][3].ToString());
				benList[i].BenefitType=(InsBenefitType)PIn.PLong(table.Rows[i][4].ToString());
				benList[i].Percent=PIn.PInt(table.Rows[i][5].ToString());
				benList[i].MonetaryAmt=PIn.PDouble(table.Rows[i][6].ToString());
				benList[i].TimePeriod=(BenefitTimePeriod)PIn.PLong(table.Rows[i][7].ToString());
				benList[i].QuantityQualifier=(BenefitQuantity)PIn.PLong(table.Rows[i][8].ToString());
				benList[i].Quantity=PIn.PInt(table.Rows[i][9].ToString());
				benList[i].CodeNum=PIn.PLong(table.Rows[i][10].ToString());
				benList[i].CoverageLevel=(BenefitCoverageLevel)PIn.PLong(table.Rows[i][11].ToString());
			}
			////Get planNums for all identical plans
			//string command="SELECT PlanNum FROM insplan "
			//  //+"WHERE PlanNum != "   +POut.PInt(like.PlanNum)+" "
			//  +"WHERE EmployerNum = '" +POut.PLong(like.EmployerNum)+"' "
			//  +"AND GroupName = '"   +POut.PString(like.GroupName)+"' "
			//  +"AND GroupNum = '"    +POut.PString(like.GroupNum)+"' "
			//  +"AND DivisionNo = '"  +POut.PString(like.DivisionNo)+"'"
			//  +"AND CarrierNum = '"  +POut.PLong(like.CarrierNum)+"' "
			//  +"AND IsMedical = '"   +POut.PBool(like.IsMedical)+"' ";
			//DataTable table=Db.GetTable(command);
			//string planNums="";
			//for(int i=0;i<table.Rows.Count;i++) {
			//  if(i>0) {
			//    planNums+=" OR";
			//  }
			//  planNums+=" PlanNum="+table.Rows[i][0].ToString();
			//}
			//Benefit[] benList=new Benefit[0];
			//if(table.Rows.Count>0){
			//  //Get all benefits for all those plans
			//  command="SELECT * FROM benefit WHERE"+planNums;
			//  table=Db.GetTable(command);
			//  benList=new Benefit[table.Rows.Count];
			//  for(int i=0;i<table.Rows.Count;i++) {
			//    benList[i]=new Benefit();
			//    benList[i].BenefitNum       = PIn.PLong(table.Rows[i][0].ToString());
			//    benList[i].PlanNum          = PIn.PLong(table.Rows[i][1].ToString());
			//    benList[i].PatPlanNum       = PIn.PLong(table.Rows[i][2].ToString());
			//    benList[i].CovCatNum        = PIn.PLong(table.Rows[i][3].ToString());
			//    benList[i].BenefitType      = (InsBenefitType)PIn.PLong(table.Rows[i][4].ToString());
			//    benList[i].Percent          = PIn.PInt(table.Rows[i][5].ToString());
			//    benList[i].MonetaryAmt      = PIn.PDouble(table.Rows[i][6].ToString());
			//    benList[i].TimePeriod       = (BenefitTimePeriod)PIn.PLong(table.Rows[i][7].ToString());
			//    benList[i].QuantityQualifier= (BenefitQuantity)PIn.PLong(table.Rows[i][8].ToString());
			//    benList[i].Quantity         = PIn.PInt(table.Rows[i][9].ToString());
			//    benList[i].CodeNum          = PIn.PLong(table.Rows[i][10].ToString());
			//    benList[i].CoverageLevel    = (BenefitCoverageLevel)PIn.PLong(table.Rows[i][11].ToString());
			//  }
			//}
			//We could probably turn this last part into a group by within the query above in order to make this portion faster.
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
				+"PlanNum = '"          +POut.PLong   (ben.PlanNum)+"'"
				+",PatPlanNum = '"      +POut.PLong   (ben.PatPlanNum)+"'"
				+",CovCatNum = '"       +POut.PLong   (ben.CovCatNum)+"'"
				+",BenefitType = '"     +POut.PLong   ((int)ben.BenefitType)+"'"
				+",Percent = '"         +POut.PLong   (ben.Percent)+"'"
				+",MonetaryAmt = '"     +POut.PDouble(ben.MonetaryAmt)+"'"
				+",TimePeriod = '"      +POut.PLong   ((int)ben.TimePeriod)+"'"
				+",QuantityQualifier ='"+POut.PLong   ((int)ben.QuantityQualifier)+"'"
				+",Quantity = '"        +POut.PLong   (ben.Quantity)+"'"
				+",CodeNum = '"         +POut.PLong   (ben.CodeNum)+"'"
				+",CoverageLevel = '"   +POut.PLong   ((int)ben.CoverageLevel)+"'"
				+" WHERE BenefitNum  ='"+POut.PLong   (ben.BenefitNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Benefit ben) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				ben.BenefitNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ben);
				return ben.BenefitNum;
			}
			if(PrefC.RandomKeys) {
				ben.BenefitNum=ReplicationServers.GetKey("benefit","BenefitNum");
			}
			string command="INSERT INTO benefit (";
			if(PrefC.RandomKeys) {
				command+="BenefitNum,";
			}
			command+="PlanNum,PatPlanNum,CovCatNum,BenefitType,Percent,MonetaryAmt,TimePeriod,"
				+"QuantityQualifier,Quantity,CodeNum,CoverageLevel) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PLong(ben.BenefitNum)+"', ";
			}
			command+=
				 "'"+POut.PLong(ben.PlanNum)+"', "
				+"'"+POut.PLong(ben.PatPlanNum)+"', "
				+"'"+POut.PLong(ben.CovCatNum)+"', "
				+"'"+POut.PLong((int)ben.BenefitType)+"', "
				+"'"+POut.PLong(ben.Percent)+"', "
				+"'"+POut.PDouble(ben.MonetaryAmt)+"', "
				+"'"+POut.PLong((int)ben.TimePeriod)+"', "
				+"'"+POut.PLong((int)ben.QuantityQualifier)+"', "
				+"'"+POut.PLong(ben.Quantity)+"', "
				+"'"+POut.PLong(ben.CodeNum)+"', "
				+"'"+POut.PLong((int)ben.CoverageLevel)+"')";
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
			string command="DELETE FROM benefit WHERE BenefitNum ="+POut.PLong(ben.BenefitNum);
			Db.NonQ(command);
		}

		///<summary>Only for display purposes rather than any calculations.  Gets an annual max from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the top of the benefit list, it will get the most general limitation first.  Returns -1 if none found.  It does not discriminate between family and individual because it doesn't need to.</summary>
		public static double GetAnnualMaxDisplay(List<Benefit> list,long planNum,long patPlanNum) {
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
				//coverage level?
				if(list[i].CodeNum != 0) {
					continue;
				}
				if(list[i].CovCatNum != 0) {
					EbenefitCategory eben=CovCats.GetEbenCat(list[i].CovCatNum);
					if(eben != EbenefitCategory.General && eben != EbenefitCategory.None) {
						continue;
					}
				}
				return list[i].MonetaryAmt;
			}
			return -1;
		}
		/*
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
		}*/

		///<summary>Only for display purposes rather than any calculations.  Gets a general deductible from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.</summary>
		public static double GetDeductGeneralDisplay(List<Benefit> list,long planNum,long patPlanNum,BenefitCoverageLevel level) {
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
				if(list[i].CoverageLevel != level) {
					continue;
				}
				if(list[i].CodeNum != 0) {
					continue;
				}
				if(list[i].CovCatNum != 0) {
					EbenefitCategory eben=CovCats.GetEbenCat(list[i].CovCatNum);
					if(eben != EbenefitCategory.General && eben != EbenefitCategory.None) {
						continue;
					}
				}
				return list[i].MonetaryAmt;
			}
			return -1;
		}

		/*
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
		}*/

		///<summary>Used only in ClaimProcs.ComputeBaseEst.  Gets a deductible amount from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.  It figures out how much was already used and reduces the returned value by that amount.  Both individual and family deductibles will reduce the returned value independently.  Works for individual procs, categories, and general.</summary>
		public static double GetDeductibleByCode(List<Benefit> benList,long planNum,long patPlanNum,DateTime procDate,string procCode,List<ClaimProcHist> histList,List<ClaimProcHist> loopList,InsPlan plan,long patNum) {
			//No need to check RemotingRole; no call to db.
			//first, create a much shorter list with only relevant benefits in it.
			List<Benefit> listShort=new List<Benefit>();
			for(int i=0;i<benList.Count;i++) {
				if(benList[i].PlanNum==0 && benList[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(benList[i].PatPlanNum==0 && benList[i].PlanNum!=planNum) {
					continue;
				}
				if(benList[i].BenefitType!=InsBenefitType.Deductible) {
					continue;
				}
				//if(benList[i].QuantityQualifier!=BenefitQuantity.None) {
				//	continue;
				//}
				if(benList[i].TimePeriod!=BenefitTimePeriod.CalendarYear 
					&& benList[i].TimePeriod!=BenefitTimePeriod.ServiceYear
					&& benList[i].TimePeriod!=BenefitTimePeriod.Lifetime)//this is probably only going to be used in annual max, though
				{
					continue;
				}
				listShort.Add(benList[i]);
			}
			//look for the best matching individual deduct----------------------------------------------------------------
			Benefit benInd=null;
			//start with no category
			for(int i=0;i<listShort.Count;i++){
				if(listShort[i].CoverageLevel == BenefitCoverageLevel.Family){
					continue;
				}
				if(listShort[i].CodeNum>0){
					continue;
				}
				if(listShort[i].CovCatNum==0){
					benInd=listShort[i];
				}
			}
			//then, specific category.
			CovSpan[] spansForCat;
			for(int i=0;i<listShort.Count;i++){
				if(listShort[i].CoverageLevel == BenefitCoverageLevel.Family){
					continue;
				}
				if(listShort[i].CodeNum>0){
					continue;
				}
				if(listShort[i].CovCatNum!=0){
					//see if the span matches
					spansForCat=CovSpans.GetForCat(listShort[i].CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++){
						if(String.Compare(procCode,spansForCat[j].FromCode)>=0 && String.Compare(procCode,spansForCat[j].ToCode)<=0){
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;//no match
					}
					if(benInd != null && benInd.CovCatNum!=0){//must compare
						//only use the new one if the item order is larger
						if(CovCats.GetOrderShort(listShort[i].CovCatNum) > CovCats.GetOrderShort(benInd.CovCatNum)){
							benInd=listShort[i];
						}
					}
					else{//first one encountered for a category
						benInd=listShort[i];
					}
				}
			}
			//then, specific code
			for(int i=0;i<listShort.Count;i++){
				if(listShort[i].CoverageLevel == BenefitCoverageLevel.Family){
					continue;
				}
				if(listShort[i].CodeNum==0){
					continue;
				}
				if(procCode==ProcedureCodes.GetStringProcCode(listShort[i].CodeNum)){
					benInd=listShort[i];
				}
			}
			//look for the best matching family deduct----------------------------------------------------------------
			Benefit benFam=null;
			//start with no category
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel != BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum>0) {
					continue;
				}
				if(listShort[i].CovCatNum==0) {
					benFam=listShort[i];
				}
			}
			//then, specific category.
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel != BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum>0) {
					continue;
				}
				if(listShort[i].CovCatNum!=0) {
					//see if the span matches
					spansForCat=CovSpans.GetForCat(listShort[i].CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(procCode,spansForCat[j].FromCode)>=0 && String.Compare(procCode,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;//no match
					}
					if(benFam != null && benFam.CovCatNum!=0) {//must compare
						//only use the new one if the item order is larger
						if(CovCats.GetOrderShort(listShort[i].CovCatNum) > CovCats.GetOrderShort(benFam.CovCatNum)) {
							benFam=listShort[i];
						}
					}
					else {//first one encountered for a category
						benFam=listShort[i];
					}
				}
			}
			//then, specific code
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel != BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum==0) {
					continue;
				}
				if(procCode==ProcedureCodes.GetStringProcCode(listShort[i].CodeNum)) {
					benFam=listShort[i];
				}
			}
			//example. $50 individual deduct, $150 family deduct.
			//Only individual deductibles make sense as the starting point.
			//Family deductible just limits the sum of individual deductibles.
			//If there is no individual deductible that matches, then return 0.
			if(benInd==null || benInd.MonetaryAmt==-1 || benInd.MonetaryAmt==0) {
				return 0;
			}
			double retVal=benInd.MonetaryAmt;
			//reduce by amount individual already paid this year--------------------------------------------------------------------
			//establish date range for procedures to consider
			DateTime dateStart=BenefitLogic.ComputeRenewDate(procDate,plan.MonthRenew);
			DateTime dateEnd=procDate;//I guess we don't want to consider anything after the date of this procedure.
			if(benInd.TimePeriod==BenefitTimePeriod.Lifetime) {
				dateStart=DateTime.MinValue;
			}
			for(int i=0;i<histList.Count;i++) {
				if(histList[i].PlanNum != planNum) {
					continue;//different plan
				}
				if(histList[i].ProcDate<dateStart || histList[i].ProcDate>dateEnd) {
					continue;
				}
				if(histList[i].PatNum != patNum) {
					continue;//this is for someone else in the family
				}
				if(benInd.CodeNum!=0) {//specific code
					if(ProcedureCodes.GetStringProcCode(benInd.CodeNum)!=histList[i].StrProcCode) {
						continue;
					}
				}
				else if(benInd.CovCatNum!=0) {//specific category
					spansForCat=CovSpans.GetForCat(benInd.CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(histList[i].StrProcCode,spansForCat[j].FromCode)>=0 
							&& String.Compare(histList[i].StrProcCode,spansForCat[j].ToCode)<=0) 
						{
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;
					}
				}
				//if no category, then benefits are not restricted by proc code.
				retVal-=histList[i].Deduct;
			}
			//now, do a similar thing with loopList, individ-----------------------------------------------------------------------
			for(int i=0;i<loopList.Count;i++) {
				//no date restriction, since all TP or part of current claim
				//if(histList[i].ProcDate<dateStart || histList[i].ProcDate>dateEnd) {
				//	continue;
				//}
				if(loopList[i].PlanNum != planNum) {
					continue;//different plan.  Even the loop list can contain info for multiple plans.
				}
				if(loopList[i].PatNum != patNum) {
					continue;//this is for someone else in the family
				}
				if(benInd.CodeNum!=0) {//specific code
					if(ProcedureCodes.GetStringProcCode(benInd.CodeNum)!=loopList[i].StrProcCode) {
						continue;
					}
				}
				else if(benInd.CovCatNum!=0) {//specific category
					spansForCat=CovSpans.GetForCat(benInd.CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(loopList[i].StrProcCode,spansForCat[j].FromCode)>=0 
							&& String.Compare(loopList[i].StrProcCode,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;
					}
				}
				//if no category, then benefits are not restricted by proc code.
				retVal-=loopList[i].Deduct;
			}
			if(retVal<=0) {
				return 0;
			}
			//if there is still a deductible, we might still reduce it based on family ded used.
			if(benFam==null || benFam.MonetaryAmt==-1) {
				return retVal;
			}
			double famded=benFam.MonetaryAmt;
			//reduce the family deductible by amounts already used----------------------------------------------------------
			for(int i=0;i<histList.Count;i++) {
				if(histList[i].ProcDate<dateStart || histList[i].ProcDate>dateEnd) {
					continue;
				}
				if(histList[i].PlanNum != planNum) {
					continue;//different plan
				}
				//now, we do want to see all family members.
				//if(histList[i].PatNum != patNum) {
				//	continue;//this is for someone else in the family
				//}
				if(benFam.CodeNum!=0) {//specific code
					if(ProcedureCodes.GetStringProcCode(benFam.CodeNum)!=histList[i].StrProcCode) {
						continue;
					}
				}
				else if(benFam.CovCatNum!=0) {//specific category
					spansForCat=CovSpans.GetForCat(benFam.CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(histList[i].StrProcCode,spansForCat[j].FromCode)>=0 
							&& String.Compare(histList[i].StrProcCode,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;
					}
				}
				//if no category, then benefits are not restricted by proc code.
				famded-=histList[i].Deduct;
			}
			//reduce family ded by amounts already used in loop---------------------------------------------------------------
			for(int i=0;i<loopList.Count;i++) {
				if(loopList[i].PlanNum != planNum) {
					continue;//different plan
				}
				if(benFam.CodeNum!=0) {//specific code
					if(ProcedureCodes.GetStringProcCode(benFam.CodeNum)!=loopList[i].StrProcCode) {
						continue;
					}
				}
				else if(benFam.CovCatNum!=0) {//specific category
					spansForCat=CovSpans.GetForCat(benFam.CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(loopList[i].StrProcCode,spansForCat[j].FromCode)>=0 
							&& String.Compare(loopList[i].StrProcCode,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;
					}
				}
				//if no category, then benefits are not restricted by proc code.
				famded-=loopList[i].Deduct;
			}
			//if the family deductible has all been used up on other procs
			if(famded<=0) {
				return 0;//then no deductible, regardless of what we computed for individual
			}
			if(retVal > famded) {//example; retInd=$50, but 120 of 150 family ded has been used.  famded=30.  We need to return 30.
				return famded;
			}
			return retVal;
		}

		///<summary>Used only in ClaimProcs.ComputeBaseEst.  Calculates the most specific limitation for the specified code.  This is usually an annual max, ortho max, or fluoride limitation (only if age match).  Ignores benefits that do not match either the planNum or the patPlanNum.  It figures out how much was already used and reduces the returned value by that amount.  Both individual and family limitations will reduce the returned value independently.  Works for individual procs, categories, and general.  Also outputs a string description of the limitation.  There don't seem to be any situations where multiple limitations would each partially reduce coverage for a single code, other than ind/fam.  The returned value will be the original insEstTotal passed in unless there was some limitation that reduced it.</summary>
		public static double GetLimitationByCode(List<Benefit> benList,long planNum,long patPlanNum,DateTime procDate,string procCodeStr,List<ClaimProcHist> histList,List<ClaimProcHist> loopList,InsPlan plan,long patNum,out string note,double insEstTotal,int patientAge) {
			//No need to check RemotingRole;no call to db.
			note ="";
			//first, create a much shorter list with only relevant benefits in it.
			List<Benefit> listShort=new List<Benefit>();
			for(int i=0;i<benList.Count;i++) {
				if(benList[i].PlanNum==0 && benList[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(benList[i].PatPlanNum==0 && benList[i].PlanNum!=planNum) {
					continue;
				}
				if(benList[i].BenefitType!=InsBenefitType.Limitations) {
					continue;
				}
				//if(benList[i].TimePeriod!=BenefitTimePeriod.CalendarYear 
				//	&& benList[i].TimePeriod!=BenefitTimePeriod.ServiceYear
				//	&& benList[i].TimePeriod!=BenefitTimePeriod.Lifetime)
				//{
				//	continue;
				//}
				listShort.Add(benList[i]);
			}
			//look for the best matching individual limitation----------------------------------------------------------------
			Benefit benInd=null;
			//start with no category
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel == BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum>0) {
					continue;
				}
				if(listShort[i].CovCatNum==0) {
					benInd=listShort[i];
				}
			}
			//then, specific category.
			CovSpan[] spansForCat;
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel == BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum>0) {
					continue;
				}
				if(listShort[i].CovCatNum!=0) {
					//see if the span matches
					spansForCat=CovSpans.GetForCat(listShort[i].CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(procCodeStr,spansForCat[j].FromCode)>=0 && String.Compare(procCodeStr,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;//no match
					}
					if(benInd != null && benInd.CovCatNum!=0) {//must compare
						//only use the new one if the item order is larger
						if(CovCats.GetOrderShort(listShort[i].CovCatNum) > CovCats.GetOrderShort(benInd.CovCatNum)) {
							benInd=listShort[i];
						}
					}
					else {//first one encountered for a category
						benInd=listShort[i];
					}
				}
			}
			//then, specific code
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel == BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum==0) {
					continue;
				}
				if(procCodeStr!=ProcedureCodes.GetStringProcCode(listShort[i].CodeNum)) {
					continue;
				}
				//if it's an age based limitation, then make sure the patient age matches.
				//If we have an age match, then we exit the method right here.
				if(listShort[i].QuantityQualifier==BenefitQuantity.AgeLimit && listShort[i].Quantity > 0){
					if(patientAge > listShort[i].Quantity){
						note=Lans.g("Benefits","Age limitation:")+" "+listShort[i].Quantity.ToString();
						return 0;//not covered if too old.
					}
				}
				else{//anything but an age limit
					benInd=listShort[i];
				}
			}
			//look for the best matching family limitation----------------------------------------------------------------
			Benefit benFam=null;
			//start with no category
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel != BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum>0) {
					continue;
				}
				if(listShort[i].CovCatNum==0) {
					benFam=listShort[i];
				}
			}
			//then, specific category.
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel != BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum>0) {
					continue;
				}
				if(listShort[i].CovCatNum!=0) {
					//see if the span matches
					spansForCat=CovSpans.GetForCat(listShort[i].CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(procCodeStr,spansForCat[j].FromCode)>=0 && String.Compare(procCodeStr,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;//no match
					}
					if(benFam != null && benFam.CovCatNum!=0) {//must compare
						//only use the new one if the item order is larger
						if(CovCats.GetOrderShort(listShort[i].CovCatNum) > CovCats.GetOrderShort(benFam.CovCatNum)) {
							benFam=listShort[i];
						}
					}
					else {//first one encountered for a category
						benFam=listShort[i];
					}
				}
			}
			//then, specific code
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CoverageLevel != BenefitCoverageLevel.Family) {
					continue;
				}
				if(listShort[i].CodeNum==0) {
					continue;
				}
				if(procCodeStr==ProcedureCodes.GetStringProcCode(listShort[i].CodeNum)) {
					benFam=listShort[i];
				}
			}
			//example. $1000 individual max, $3000 family max.
			//Only individual max makes sense as the starting point.
			//Family max just limits the sum of individual maxes.
			//If there is no individual limitation that matches, then return 0.
			//fluoride age limit already handled, so all that's left is maximums. 
			if(benInd==null || benInd.MonetaryAmt==-1 || benInd.MonetaryAmt==0) {
				return insEstTotal;//no max found for this code.
			}
			double maxInd=benInd.MonetaryAmt;
			//reduce individual max by amount already paid this year/lifetime---------------------------------------------------
			//establish date range for procedures to consider
			DateTime dateStart=BenefitLogic.ComputeRenewDate(procDate,plan.MonthRenew);
			DateTime dateEnd=procDate;//don't consider anything after the date of this procedure.
			if(benInd.TimePeriod==BenefitTimePeriod.Lifetime) {
				dateStart=DateTime.MinValue;
			}
			for(int i=0;i<histList.Count;i++) {
				if(histList[i].PlanNum != planNum) {
					continue;//different plan
				}
				if(histList[i].ProcDate<dateStart || histList[i].ProcDate>dateEnd) {
					continue;
				}
				if(histList[i].PatNum != patNum) {
					continue;//this is for someone else in the family
				}
				if(benInd.CodeNum!=0) {//specific code
					if(ProcedureCodes.GetStringProcCode(benInd.CodeNum)!=histList[i].StrProcCode) {
						continue;
					}
				}
				else if(benInd.CovCatNum!=0) {//specific category
					spansForCat=CovSpans.GetForCat(benInd.CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(histList[i].StrProcCode,spansForCat[j].FromCode)>=0 
							&& String.Compare(histList[i].StrProcCode,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;
					}
				}
				//if no category, then benefits are not restricted by proc code.
				//In other words, the benefit applies to all codes.
				maxInd-=histList[i].Amount;
			}
			//reduce individual max by amount in loop ------------------------------------------------------------------
			for(int i=0;i<loopList.Count;i++) {
				//no date restriction, since all TP or part of current claim
				//if(histList[i].ProcDate<dateStart || histList[i].ProcDate>dateEnd) {
				//	continue;
				//}
				if(loopList[i].PlanNum != planNum) {
					continue;//different plan.  Even the loop list can contain info for multiple plans.
				}
				if(loopList[i].PatNum != patNum) {
					continue;//this is for someone else in the family
				}
				if(benInd.CodeNum!=0) {//specific code
					if(ProcedureCodes.GetStringProcCode(benInd.CodeNum)!=loopList[i].StrProcCode) {
						continue;
					}
				}
				else if(benInd.CovCatNum!=0) {//specific category
					spansForCat=CovSpans.GetForCat(benInd.CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(loopList[i].StrProcCode,spansForCat[j].FromCode)>=0 
							&& String.Compare(loopList[i].StrProcCode,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;
					}
				}
				//if no category, then benefits are not restricted by proc code.
				maxInd-=loopList[i].Amount;
			}
			if(maxInd <= 0) {//then patient has used up all of their annual max, so no coverage.
				if(benInd.TimePeriod==BenefitTimePeriod.Lifetime) {
					note+=Lans.g("Benefits","Over lifetime max");
				}
				else if(benInd.TimePeriod==BenefitTimePeriod.CalendarYear
					|| benInd.TimePeriod==BenefitTimePeriod.ServiceYear) 
				{
					note+=Lans.g("Benefits","Over annual max");
				}
				return 0;
			}
			double retVal=insEstTotal;
			if(maxInd < insEstTotal) {//if there's not enough left in the annual max to cover this proc.
				retVal=maxInd;//insurance will only cover up to the remaining annual max
			}
			//So at this point, there seems to be enough to cover at least part of this procedure, and the only reason to continue
			//would be if there was also a family max that had been met.
			//I don't think this is common, but the following code will handle it.
			if(benFam==null || benFam.MonetaryAmt==-1) {
				if(retVal != insEstTotal){
					if(benInd.TimePeriod==BenefitTimePeriod.Lifetime) {
						note+=Lans.g("Benefits","Over lifetime max");
					}
					else if(benInd.TimePeriod==BenefitTimePeriod.CalendarYear
						|| benInd.TimePeriod==BenefitTimePeriod.ServiceYear) {
						note+=Lans.g("Benefits","Over annual max");
					}
				}
				return retVal;//no family max anyway, so no need to go further.
			}
			double maxFam=benFam.MonetaryAmt;
			//reduce the family max by amounts already used----------------------------------------------------------
			for(int i=0;i<histList.Count;i++) {
				if(histList[i].ProcDate<dateStart || histList[i].ProcDate>dateEnd) {
					continue;
				}
				if(histList[i].PlanNum != planNum) {
					continue;//different plan
				}
				//now, we do want to see all family members.
				//if(histList[i].PatNum != patNum) {
				//	continue;//this is for someone else in the family
				//}
				if(benFam.CodeNum!=0) {//specific code
					if(ProcedureCodes.GetStringProcCode(benFam.CodeNum)!=histList[i].StrProcCode) {
						continue;
					}
				}
				else if(benFam.CovCatNum!=0) {//specific category
					spansForCat=CovSpans.GetForCat(benFam.CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(histList[i].StrProcCode,spansForCat[j].FromCode)>=0 
							&& String.Compare(histList[i].StrProcCode,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;
					}
				}
				//if no category, then benefits are not restricted by proc code.
				maxFam-=histList[i].Amount;
			}
			//reduce family max by amounts already used in loop---------------------------------------------------------------
			for(int i=0;i<loopList.Count;i++) {
				if(loopList[i].PlanNum != planNum) {
					continue;//different plan
				}
				if(benFam.CodeNum!=0) {//specific code
					if(ProcedureCodes.GetStringProcCode(benFam.CodeNum)!=loopList[i].StrProcCode) {
						continue;
					}
				}
				else if(benFam.CovCatNum!=0) {//specific category
					spansForCat=CovSpans.GetForCat(benFam.CovCatNum);
					bool isMatch=false;
					for(int j=0;j<spansForCat.Length;j++) {
						if(String.Compare(loopList[i].StrProcCode,spansForCat[j].FromCode)>=0 
							&& String.Compare(loopList[i].StrProcCode,spansForCat[j].ToCode)<=0) {
							isMatch=true;
							break;
						}
					}
					if(!isMatch) {
						continue;
					}
				}
				//if no category, then benefits are not restricted by proc code.
				maxFam-=loopList[i].Amount;
			}
			//if the family max has all been used up on other procs
			if(maxFam<=0) {
				if(benInd.TimePeriod==BenefitTimePeriod.Lifetime) {
					note+=Lans.g("Benefits","Over family lifetime max");
				}
				else if(benInd.TimePeriod==BenefitTimePeriod.CalendarYear
					|| benInd.TimePeriod==BenefitTimePeriod.ServiceYear) {
					note+=Lans.g("Benefits","Over family annual max");
				}
				return 0;//then no coverage, regardless of what we computed for individual
			}
			if(maxFam > maxInd) {//restrict by maxInd
				//which we already calculated
				if(benInd.TimePeriod==BenefitTimePeriod.Lifetime) {
					note+=Lans.g("Benefits","Over lifetime max");
				}
				else if(benInd.TimePeriod==BenefitTimePeriod.CalendarYear
					|| benInd.TimePeriod==BenefitTimePeriod.ServiceYear) {
					note+=Lans.g("Benefits","Over annual max");
				}
				return retVal;
			}
			else {//restrict by maxFam
				if(maxFam < retVal) {//if there's not enough left in the annual max to cover this proc.
					//example. retVal=$70.  But 2970 of 3000 family max has been used.  maxFam=30.  We need to return 30.
					if(benInd.TimePeriod==BenefitTimePeriod.Lifetime) {
						note+=Lans.g("Benefits","Over family lifetime max");
					}
					else if(benInd.TimePeriod==BenefitTimePeriod.CalendarYear
					|| benInd.TimePeriod==BenefitTimePeriod.ServiceYear) {
						note+=Lans.g("Benefits","Over family annual max");
					}
					return maxFam;//insurance will only cover up to the remaining annual max
				}
			}
			if(retVal<insEstTotal) {//must have been an individual restriction
				if(benInd.TimePeriod==BenefitTimePeriod.Lifetime) {
					note+=Lans.g("Benefits","Over lifetime max");
				}
				else if(benInd.TimePeriod==BenefitTimePeriod.CalendarYear
					|| benInd.TimePeriod==BenefitTimePeriod.ServiceYear) {
					note+=Lans.g("Benefits","Over annual max");
				}
			}
			return retVal;
		}

		///<summary>Only used from ClaimProc.ComputeBaseEst. This is a low level function to get the percent to store in a claimproc.  It does not consider any percentOverride.  Always returns a number between 0 and 100.  Handles general, category, or procedure level.  Does not handle pat vs family coveragelevel.  Does handle patient override by using patplan.  Does not need to be aware of procedure history or loop history.</summary>
		public static int GetPercent(string procCodeStr,string planType,long planNum,long patPlanNum,List<Benefit> benList) {
			//No need to check RemotingRole; no call to db.
			if(planType=="f" || planType=="c"){
				return 100;//flat and cap are always covered 100%
			}
			//first, create a much shorter list with only relevant benefits in it.
			List<Benefit> listShort=new List<Benefit>();
			for(int i=0;i<benList.Count;i++) {
				if(benList[i].PlanNum==0 && benList[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(benList[i].PatPlanNum==0 && benList[i].PlanNum!=planNum) {
					continue;
				}
				if(benList[i].BenefitType!=InsBenefitType.CoInsurance) {
					continue;
				}
				if(benList[i].Percent == -1) {
					continue;
				}
				listShort.Add(benList[i]);
			}
			//Find the best benefit matches.
			//Plan and Pat here indicate patplan override and have nothing to do with pat vs family coverage level.
			Benefit benPlan=null;
			Benefit benPat=null;
			//start with no category
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CodeNum > 0) {
					continue;
				}
				if(listShort[i].CovCatNum != 0) {
					continue;
				}
				if(benList[i].PatPlanNum !=0) {
					benPat=listShort[i];
				}
				else {
					benPlan=listShort[i];
				}
			}
			//then, specific category.
			CovSpan[] spansForCat;
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CodeNum>0) {
					continue;
				}
				if(listShort[i].CovCatNum==0) {
					continue;
				}
				//see if the span matches
				spansForCat=CovSpans.GetForCat(listShort[i].CovCatNum);
				bool isMatch=false;
				for(int j=0;j<spansForCat.Length;j++) {
					if(String.Compare(procCodeStr,spansForCat[j].FromCode)>=0 && String.Compare(procCodeStr,spansForCat[j].ToCode)<=0) {
						isMatch=true;
						break;
					}
				}
				if(!isMatch) {
					continue;//no match
				}
				if(benList[i].PatPlanNum !=0) {
					if(benPat != null && benPat.CovCatNum!=0) {//must compare
						//only use the new one if the item order is larger
						if(CovCats.GetOrderShort(listShort[i].CovCatNum) > CovCats.GetOrderShort(benPat.CovCatNum)) {
							benPat=listShort[i];
						}
					}
					else {//first one encountered for a category
						benPat=listShort[i];
					}
				}
				else {
					if(benPlan != null && benPlan.CovCatNum!=0) {//must compare
						//only use the new one if the item order is larger
						if(CovCats.GetOrderShort(listShort[i].CovCatNum) > CovCats.GetOrderShort(benPlan.CovCatNum)) {
							benPlan=listShort[i];
						}
					}
					else {//first one encountered for a category
						benPlan=listShort[i];
					}
				}
			}
			//then, specific code
			for(int i=0;i<listShort.Count;i++) {
				if(listShort[i].CodeNum==0) {
					continue;
				}
				if(procCodeStr != ProcedureCodes.GetStringProcCode(listShort[i].CodeNum)) {
					continue;
				}
				if(benList[i].PatPlanNum !=0) {
					benPat=listShort[i];
				}
				else {
					benPlan=listShort[i];
				}				
			}
			if(benPat != null) {
				return benPat.Percent;
			}
			if(benPlan != null) {
				return benPlan.Percent;
			}
			return 0;
		}

		///<summary>Only used from ClaimProc.ComputeBaseEst. This is a low level function to determine if a given procedure is completely excluded from coverage.  It does not consider any dates of service or history.</summary>
		public static bool IsExcluded(string strProcCode,List<Benefit> benList,long planNum,long patPlanNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<benList.Count;i++) {
				if(benList[i].PlanNum==0 && benList[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(benList[i].PatPlanNum==0 && benList[i].PlanNum!=planNum) {
					continue;
				}
				if(benList[i].BenefitType!=InsBenefitType.Exclusions) {
					continue;
				}
				if(benList[i].CodeNum > 0) {
					if(strProcCode==ProcedureCodes.GetStringProcCode(benList[i].CodeNum)) {
						return true;//specific procedure code excluded
					}
					continue;
				}
				if(benList[i].CovCatNum==0) {
					continue;
					//General exclusion with no category.  This is considered an unsupported type of benefit.
					//Nobody should be able to exclude everything with one benefit entry.
				}
				//see if the span matches
				CovSpan[] spansForCat=CovSpans.GetForCat(benList[i].CovCatNum);
				//bool isMatch=false;
				for(int j=0;j<spansForCat.Length;j++) {
					if(String.Compare(strProcCode,spansForCat[j].FromCode)>=0 && String.Compare(strProcCode,spansForCat[j].ToCode)<=0) {
						return true;//span matches
					}
				}
			}
			return false;//no exclusions found for this code
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
		public static bool UpdateListForIdentical(List<Benefit> oldBenefitList,List<Benefit> newBenefitList,List<long> planNums) {
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
				command="DELETE FROM benefit WHERE PlanNum="+POut.PLong(planNums[i]);
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
		public static void DeleteForPlan(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),planNum);
				return;
			}
			string command="DELETE FROM benefit WHERE PlanNum="+POut.PLong(planNum);
			Db.NonQ(command);
		}

	}


		



		
	

	

	


}










