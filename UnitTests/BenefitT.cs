using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class BenefitT {
		public static void CreateAnnualMax(long planNum,double amt){
			Benefit ben=new Benefit();
			ben.PlanNum=planNum;
			ben.BenefitType=InsBenefitType.Limitations;
			ben.CovCatNum=0;
			ben.CoverageLevel=BenefitCoverageLevel.Individual;
			ben.MonetaryAmt=amt;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
		}

		public static void CreateDeductibleGeneral(long planNum,double amt){
			Benefit ben=new Benefit();
			ben.PlanNum=planNum;
			ben.BenefitType=InsBenefitType.Deductible;
			ben.CovCatNum=0;
			ben.CoverageLevel=BenefitCoverageLevel.Individual;
			ben.MonetaryAmt=amt;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
		}

		public static void CreateDeductible(long planNum,EbenefitCategory category,double amt){
			Benefit ben=new Benefit();
			ben.PlanNum=planNum;
			ben.BenefitType=InsBenefitType.Deductible;
			ben.CovCatNum=CovCats.GetForEbenCat(category).CovCatNum;
			ben.CoverageLevel=BenefitCoverageLevel.Individual;
			ben.MonetaryAmt=amt;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
		}

		public static void CreateLimitation(long planNum,EbenefitCategory category,double amt){
			Benefit ben=new Benefit();
			ben.PlanNum=planNum;
			ben.BenefitType=InsBenefitType.Limitations;
			ben.CovCatNum=CovCats.GetForEbenCat(category).CovCatNum;
			ben.CoverageLevel=BenefitCoverageLevel.Individual;
			ben.MonetaryAmt=amt;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
		}

		public static void CreateLimitationProc(long planNum,string procCodeStr,double amt) {
			Benefit ben=new Benefit();
			ben.PlanNum=planNum;
			ben.BenefitType=InsBenefitType.Limitations;
			ben.CodeNum=ProcedureCodes.GetCodeNum(procCodeStr);
			ben.CoverageLevel=BenefitCoverageLevel.Individual;
			ben.MonetaryAmt=amt;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
		}

		public static void CreateAnnualMaxFamily(long planNum,double amt){
			Benefit ben=new Benefit();
			ben.PlanNum=planNum;
			ben.BenefitType=InsBenefitType.Limitations;
			ben.CovCatNum=0;
			ben.CoverageLevel=BenefitCoverageLevel.Family;
			ben.MonetaryAmt=amt;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
		}

		public static void CreateCategoryPercent(long planNum,EbenefitCategory category,int percent){
			Benefit ben=new Benefit();
			ben.PlanNum=planNum;
			ben.BenefitType=InsBenefitType.CoInsurance;
			ben.CovCatNum=CovCats.GetForEbenCat(category).CovCatNum;
			ben.CoverageLevel=BenefitCoverageLevel.None;
			ben.Percent=percent;
			ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			Benefits.Insert(ben);
		}

		public static void CreateFrequency(long planNum,string procCodeStr,BenefitQuantity quantityQualifier,Byte quantity){
			Benefit ben=new Benefit();
			ben.PlanNum=planNum;
			ben.BenefitType=InsBenefitType.Limitations;
			ben.CovCatNum=0;
			ben.CodeNum=ProcedureCodes.GetCodeNum(procCodeStr);
			ben.CoverageLevel=BenefitCoverageLevel.None;
			ben.TimePeriod=BenefitTimePeriod.None;
			ben.Quantity=quantity;
			ben.QuantityQualifier=quantityQualifier;
			Benefits.Insert(ben);
		}


		/*
		private void BenefitComputeRenewDate(){
			DateTime asofDate=new DateTime(2006,3,19);
			//bool isCalendarYear=true;
			//DateTime insStartDate=new DateTime(2003,3,1);
			DateTime result=BenefitLogic.ComputeRenewDate(asofDate,0);
			if(result!=new DateTime(2006,1,1)){
				textResults.Text+="BenefitComputeRenewDate 1 failed.\r\n";
			}
			//isCalendarYear=false;//for the remaining tests
			//earlier in same month
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 2 failed.\r\n";
			}
			//earlier month in year
			asofDate=new DateTime(2006,5,1);
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 3 failed.\r\n";
			}
			asofDate=new DateTime(2006,12,1);
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 4 failed.\r\n";
			}
			//later month in year
			asofDate=new DateTime(2006,2,1);
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2005,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 5 failed.\r\n";
			}
			asofDate=new DateTime(2006,2,12);
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2005,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 6 failed.\r\n";
			}
			//Insurance start date not on the 1st.
			//asofDate=new DateTime(2008,5,10);
			//insStartDate=new DateTime(2007,1,12);
			//result=BenefitLogic.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			//if(result!=new DateTime(2008,1,1)) {
			//	textResults.Text+="BenefitComputeRenewDate 7 failed.\r\n";
			//}
			textResults.Text+="BenefitRenewDates complete.\r\n";
		}*/
	}
}
