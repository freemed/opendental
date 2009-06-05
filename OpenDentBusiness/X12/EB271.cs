using System;
using System.Collections.Generic;

namespace OpenDentBusiness
{
	///<summary>One EB segment from a 271.  Helps to organize a 271 for presentation to the user.</summary>
	public class EB271{
		public X12Segment Segment;
		///<summary>Can be null if the segment can't be translated to an appropriate benefit.  Many fields of the Benefit won't be used.  Just ones needed for display.</summary>
		public Benefit Benefitt;
		private static List<EB01> eb01;
		private static List<EB02> eb02;
		private static List<EB03> eb03;
		private static Dictionary<string,string> EB04;
		private static Dictionary<string,string> EB06;
		private static Dictionary<string,string> EB09;

		public EB271(X12Segment segment)  {
			if(eb01==null) {
				FillDictionaries();
			}
			Segment=segment;
			//start pattern matching to generate closest Benefit
			EB01 eb01val=eb01.Find(EB01HasCode);
			EB02 eb02val=eb02.Find(EB02HasCode);
			EB03 eb03val=eb03.Find(EB03HasCode);
			if(!eb01val.IsSupported
				|| (eb02val!=null && !eb02val.IsSupported)
				|| (eb03val!=null && !eb03val.IsSupported)) 
			{
				Benefitt=null;
				return;
			}
			if(eb01val.BenefitType==InsBenefitType.ActiveCoverage	&& Segment.Get(3)=="30") {
				Benefitt=null;
				return;
			}
			Benefitt=new Benefit();
			Benefitt.BenefitType=eb01val.BenefitType;
			if(eb02val!=null) {
				Benefitt.CoverageLevel=eb02val.CoverageLevel;
			}
			if(eb03val!=null) {
				Benefitt.CovCatNum=CovCats.GetForEbenCat(eb03val.ServiceType).CovCatNum;
			}
		}

		public string GetDescription() {
			string retVal="";
			retVal+=eb01.Find(EB01HasCode).Descript;//Eligibility or benefit information. Required
			if(Segment.Get(2) !="") {
				retVal+=", "+eb02.Find(EB02HasCode).Descript;//Coverage level code. Situational
			}
			if(Segment.Get(3) !="") {
				retVal+=", "+eb03.Find(EB03HasCode).Descript;//Service type code. Situational
			}
			if(Segment.Get(4) !="") {
				retVal+=", "+EB04[Segment.Get(4)];//Insurance type code. Situational
			}
			if(Segment.Get(5) !="") {
				retVal+=", "+Segment.Get(5);//Plan coverage description. Situational
			}
			if(Segment.Get(6) !="") {
				retVal+=", "+EB06[Segment.Get(6)];//Time period qualifier. Situational
			}
			if(Segment.Get(7) !="") {
				retVal+=", "+PIn.PDouble(Segment.Get(7)).ToString("c");//Monetary amount. Situational
			}
			if(Segment.Get(8) !="") {
				retVal+=", "+(PIn.PDouble(Segment.Get(8))*100).ToString()+"%";//Percent. Situational
			}
			if(Segment.Get(9) !="") {
				retVal+=", "+EB09[Segment.Get(9)];//Quantity qualifier. Situational
			}
			if(Segment.Get(10) !="") {
				retVal+=", "+Segment.Get(10);//Quantity. Situational
			}
			if(Segment.Get(11) !="") {
				retVal+=", Authorization Required-"+Segment.Get(11);//Situational.
			}
			if(Segment.Get(12) !="") {
				retVal+=", In Plan Network-"+Segment.Get(12);//Situational.
			}
			//if(Segment.Get(13) !="") {//Procedure identifier. Situational
				//Since we won't yet be making requests about specific procedure codes, this segment should never come back.
			//}
			return retVal;
		}

		/// <summary>Search predicate returns true if code matches.</summary>
		private bool EB01HasCode(EB01 eb01val) {
			if(Segment.Get(1)==eb01val.Code) {
				return true;
			}
			return false;
		}

		/// <summary>Search predicate returns true if code matches.</summary>
		private bool EB02HasCode(EB02 eb02val) {
			if(Segment.Get(2)==eb02val.Code) {
				return true;
			}
			return false;
		}

		/// <summary>Search predicate returns true if code matches.</summary>
		private bool EB03HasCode(EB03 eb03val) {
			if(Segment.Get(3)==eb03val.Code) {
				return true;
			}
			return false;
		}

		private static void FillDictionaries(){
			eb01=new List<EB01>();
			eb01.Add(new EB01("1","Active Coverage",InsBenefitType.ActiveCoverage));
			eb01.Add(new EB01("2","Active - Full Risk Capitation",InsBenefitType.ActiveCoverage));
			eb01.Add(new EB01("3","Active - Services Capitated",InsBenefitType.ActiveCoverage));
			eb01.Add(new EB01("4","Active - Services Capitated to Primary Care Physician"));
			eb01.Add(new EB01("5","Active - Pending Investigation"));
			eb01.Add(new EB01("6","Inactive"));
			eb01.Add(new EB01("7","Inactive - Pending Eligibility Update"));
			eb01.Add(new EB01("8","Inactive - Pending Investigation"));
			eb01.Add(new EB01("A","Co-Insurance",InsBenefitType.CoInsurance));
			eb01.Add(new EB01("B","Co-Payment",InsBenefitType.CoPayment));
			eb01.Add(new EB01("C","Deductible",InsBenefitType.Deductible));
			eb01.Add(new EB01("CB","Coverage Basis"));
			eb01.Add(new EB01("D","Benefit Description"));
			eb01.Add(new EB01("E","Exclusions",InsBenefitType.Exclusions));
			eb01.Add(new EB01("F","Limitations",InsBenefitType.Limitations));
			eb01.Add(new EB01("G","Out of Pocket (Stop Loss)"));
			eb01.Add(new EB01("H","Unlimited"));
			eb01.Add(new EB01("I","Non-Covered",InsBenefitType.Exclusions));
			eb01.Add(new EB01("J","Cost Containment"));
			eb01.Add(new EB01("K","Reserve"));
			eb01.Add(new EB01("L","Primary Care Provider"));
			eb01.Add(new EB01("M","Pre-existing Condition"));
			eb01.Add(new EB01("MC","Managed Care Coordinator"));
			eb01.Add(new EB01("N","Services Restricted to Following Provider"));
			eb01.Add(new EB01("O","Not Deemed a Medical Necessity"));
			eb01.Add(new EB01("P","Benefit Disclaimer"));
			eb01.Add(new EB01("Q","Second Surgical Opinion Required"));
			eb01.Add(new EB01("R","Other or Additional Payor"));
			eb01.Add(new EB01("S","Prior Year(s) History"));
			eb01.Add(new EB01("T","Card(s) Reported Lost/Stolen"));
			eb01.Add(new EB01("U","Contact Following Entity for Information"));//Too long: ...for Eligibility or Benefit Information"));
			eb01.Add(new EB01("V","Cannot Process"));
			eb01.Add(new EB01("W","Other Source of Data"));
			eb01.Add(new EB01("X","Health Care Facility"));
			eb01.Add(new EB01("Y","Spend Down"));
			//------------------------------------------------------------------------------------------------------
			eb02=new List<EB02>();
			eb02.Add(new EB02("CHD","Children Only"));
			eb02.Add(new EB02("DEP","Dependents Only"));
			eb02.Add(new EB02("ECH","Employee and Children",BenefitCoverageLevel.Family));
			eb02.Add(new EB02("ESP","Employee and Spouse",BenefitCoverageLevel.Family));
			eb02.Add(new EB02("FAM","Family",BenefitCoverageLevel.Family));
			eb02.Add(new EB02("IND","Individual",BenefitCoverageLevel.Individual));
			eb02.Add(new EB02("SPC","Spouse and Children"));
			eb02.Add(new EB02("SPO","Spouse Only"));
			//------------------------------------------------------------------------------------------------------
			eb03=new List<EB03>();
			eb03.Add(new EB03("1","Medical Care"));
			eb03.Add(new EB03("2","Surgical"));
			eb03.Add(new EB03("3","Consultation"));
			eb03.Add(new EB03("4","Diagnostic X-Ray"));
			eb03.Add(new EB03("5","Diagnostic Lab"));
			eb03.Add(new EB03("6","Radiation Therapy"));
			eb03.Add(new EB03("7","Anesthesia"));
			eb03.Add(new EB03("8","Surgical Assistance"));
			eb03.Add(new EB03("9","Other Medical"));
			eb03.Add(new EB03("10","Blood Charges"));
			eb03.Add(new EB03("11","Used Durable Medical Equipment"));
			eb03.Add(new EB03("12","Durable Medical Equipment Purchase"));
			eb03.Add(new EB03("13","Ambulatory Service Center Facility"));
			eb03.Add(new EB03("14","Renal Supplies in the Home"));
			eb03.Add(new EB03("15","Alternate Method Dialysis"));
			eb03.Add(new EB03("16","Chronic Renal Disease (CRD) Equipment"));
			eb03.Add(new EB03("17","Pre-Admission Testing"));
			eb03.Add(new EB03("18","Durable Medical Equipment Rental"));
			eb03.Add(new EB03("19","Pneumonia Vaccine"));
			eb03.Add(new EB03("20","Second Surgical Opinion"));
			eb03.Add(new EB03("21","Third Surgical Opinion"));
			eb03.Add(new EB03("22","Social Work"));
			eb03.Add(new EB03("23","Diagnostic Dental"));
			eb03.Add(new EB03("24","Periodontics"));
			eb03.Add(new EB03("25","Restorative"));
			eb03.Add(new EB03("26","Endodontics"));
			eb03.Add(new EB03("27","Maxillofacial Prosthetics"));
			eb03.Add(new EB03("28","Adjunctive Dental Services"));
			eb03.Add(new EB03("30","Health Benefit Plan Coverage"));
			eb03.Add(new EB03("32","Plan Waiting Period"));
			eb03.Add(new EB03("33","Chiropractic"));
			eb03.Add(new EB03("34","Chiropractic Office Visits"));
			eb03.Add(new EB03("35","Dental Care"));
			eb03.Add(new EB03("36","Dental Crowns"));
			eb03.Add(new EB03("37","Dental Accident"));
			eb03.Add(new EB03("38","Orthodontics"));
			eb03.Add(new EB03("39","Prosthodontics"));
			eb03.Add(new EB03("40","Oral Surgery"));
			eb03.Add(new EB03("41","Routine (Preventive) Dental"));
			eb03.Add(new EB03("42","Home Health Care"));
			eb03.Add(new EB03("43","Home Health Prescriptions"));
			eb03.Add(new EB03("44","Home Health Visits"));
			eb03.Add(new EB03("45","Hospice"));
			eb03.Add(new EB03("46","Respite Care"));
			eb03.Add(new EB03("47","Hospital"));
			eb03.Add(new EB03("48","Hospital - Inpatient"));
			eb03.Add(new EB03("49","Hospital - Room and Board"));
			eb03.Add(new EB03("50","Hospital - Outpatient"));
			eb03.Add(new EB03("51","Hospital - Emergency Accident"));
			eb03.Add(new EB03("52","Hospital - Emergency Medical"));
			eb03.Add(new EB03("53","Hospital - Ambulatory Surgical"));
			eb03.Add(new EB03("54","Long Term Care"));
			eb03.Add(new EB03("55","Major Medical"));
			eb03.Add(new EB03("56","Medically Related Transportation"));
			eb03.Add(new EB03("57","Air Transportation"));
			eb03.Add(new EB03("58","Cabulance"));
			eb03.Add(new EB03("59","Licensed Ambulance"));
			eb03.Add(new EB03("60","General Benefits"));
			eb03.Add(new EB03("61","In-vitro Fertilization"));
			eb03.Add(new EB03("62","MRI/CAT Scan"));
			eb03.Add(new EB03("63","Donor Procedures"));
			eb03.Add(new EB03("64","Acupuncture"));
			eb03.Add(new EB03("65","Newborn Care"));
			eb03.Add(new EB03("66","Pathology"));
			eb03.Add(new EB03("67","Smoking Cessation"));
			eb03.Add(new EB03("68","Well Baby Care"));
			eb03.Add(new EB03("69","Maternity"));
			eb03.Add(new EB03("70","Transplants"));
			eb03.Add(new EB03("71","Audiology Exam"));
			eb03.Add(new EB03("72","Inhalation Therapy"));
			eb03.Add(new EB03("73","Diagnostic Medical"));
			eb03.Add(new EB03("74","Private Duty Nursing"));
			eb03.Add(new EB03("75","Prosthetic Device"));
			eb03.Add(new EB03("76","Dialysis"));
			eb03.Add(new EB03("77","Otological Exam"));
			eb03.Add(new EB03("78","Chemotherapy"));
			eb03.Add(new EB03("79","Allergy Testing"));
			eb03.Add(new EB03("80","Immunizations"));
			eb03.Add(new EB03("81","Routine Physical"));
			eb03.Add(new EB03("82","Family Planning"));
			eb03.Add(new EB03("83","Infertility"));
			eb03.Add(new EB03("84","Abortion"));
			eb03.Add(new EB03("85","AIDS"));
			eb03.Add(new EB03("86","Emergency Services"));
			eb03.Add(new EB03("87","Cancer"));
			eb03.Add(new EB03("88","Pharmacy"));
			eb03.Add(new EB03("89","Free Standing Prescription Drug"));
			eb03.Add(new EB03("90","Mail Order Prescription Drug"));
			eb03.Add(new EB03("91","Brand Name Prescription Drug"));
			eb03.Add(new EB03("92","Generic Prescription Drug"));
			eb03.Add(new EB03("93","Podiatry"));
			eb03.Add(new EB03("94","Podiatry - Office Visits"));
			eb03.Add(new EB03("95","Podiatry - Nursing Home Visits"));
			eb03.Add(new EB03("96","Professional (Physician)"));
			eb03.Add(new EB03("97","Anesthesiologist"));
			eb03.Add(new EB03("98","Professional (Physician) Visit - Office"));
			eb03.Add(new EB03("99","Professional (Physician) Visit - Inpatient"));
			eb03.Add(new EB03("A0","Professional (Physician) Visit - Outpatient"));
			eb03.Add(new EB03("A1","Professional (Physician) Visit - Nursing Home"));
			eb03.Add(new EB03("A2","Professional (Physician) Visit - Skilled Nursing Facility"));
			eb03.Add(new EB03("A3","Professional (Physician) Visit - Home"));
			eb03.Add(new EB03("A4","Psychiatric"));
			eb03.Add(new EB03("A5","Psychiatric - Room and Board"));
			eb03.Add(new EB03("A6","Psychotherapy"));
			eb03.Add(new EB03("A7","Psychiatric - Inpatient"));
			eb03.Add(new EB03("A8","Psychiatric - Outpatient"));
			eb03.Add(new EB03("A9","Rehabilitation"));
			eb03.Add(new EB03("AA","Rehabilitation - Room and Board"));
			eb03.Add(new EB03("AB","Rehabilitation - Inpatient"));
			eb03.Add(new EB03("AC","Rehabilitation - Outpatient"));
			eb03.Add(new EB03("AD","Occupational Therapy"));
			eb03.Add(new EB03("AE","Physical Medicine"));
			eb03.Add(new EB03("AF","Speech Therapy"));
			eb03.Add(new EB03("AG","Skilled Nursing Care"));
			eb03.Add(new EB03("AH","Skilled Nursing Care - Room and Board"));
			eb03.Add(new EB03("AI","Substance Abuse"));
			eb03.Add(new EB03("AJ","Alcoholism"));
			eb03.Add(new EB03("AK","Drug Addiction"));
			eb03.Add(new EB03("AL","Vision (Optometry)"));
			eb03.Add(new EB03("AM","Frames"));
			eb03.Add(new EB03("AN","Routine Exam"));
			eb03.Add(new EB03("AO","Lenses"));
			eb03.Add(new EB03("AQ","Nonmedically Necessary Physical"));
			eb03.Add(new EB03("AR","Experimental Drug Therapy"));
			eb03.Add(new EB03("BA","Independent Medical Evaluation"));
			eb03.Add(new EB03("BB","Partial Hospitalization (Psychiatric)"));
			eb03.Add(new EB03("BC","Day Care (Psychiatric)"));
			eb03.Add(new EB03("BD","Cognitive Therapy"));
			eb03.Add(new EB03("BE","Massage Therapy"));
			eb03.Add(new EB03("BF","Pulmonary Rehabilitation"));
			eb03.Add(new EB03("BG","Cardiac Rehabilitation"));
			eb03.Add(new EB03("BH","Pediatric"));
			eb03.Add(new EB03("BI","Nursery"));
			eb03.Add(new EB03("BJ","Skin"));
			eb03.Add(new EB03("BK","Orthopedic"));
			eb03.Add(new EB03("BL","Cardiac"));
			eb03.Add(new EB03("BM","Lymphatic"));
			eb03.Add(new EB03("BN","Gastrointestinal"));
			eb03.Add(new EB03("BP","Endocrine"));
			eb03.Add(new EB03("BQ","Neurology"));
			eb03.Add(new EB03("BR","Eye"));
			eb03.Add(new EB03("BS","Invasive Procedures"));
			//------------------------------------------------------------------------------------------------------
			EB04=new Dictionary<string,string>();
			EB04.Add("12","Medicare Secondary Working Aged Beneficiary or Spouse with Employer Group Health Plan");
			EB04.Add("13","Medicare Secondary End-Stage Renal Disease Beneficiary in the 12 month coordination period with an employer’s group health plan");
			EB04.Add("14","Medicare Secondary, No-fault Insurance including Auto is Primary");
			EB04.Add("15","Medicare Secondary Worker’s Compensation");
			EB04.Add("16","Medicare Secondary Public Health Service (PHS)or Other Federal Agency");
			EB04.Add("41","Medicare Secondary Black Lung");
			EB04.Add("42","Medicare Secondary Veteran’s Administration");
			EB04.Add("43","Medicare Secondary Disabled Beneficiary Under Age 65 with Large Group Health Plan (LGHP)");
			EB04.Add("47","Medicare Secondary, Other Liability Insurance is Primary");
			EB04.Add("AP","Auto Insurance Policy");
			EB04.Add("C1","Commercial");
			EB04.Add("CO","Consolidated Omnibus Budget Reconciliation Act (COBRA)");
			EB04.Add("CP","Medicare Conditionally Primary");
			EB04.Add("D","Disability");
			EB04.Add("DB","Disability Benefits");
			EB04.Add("EP","Exclusive Provider Organization");
			EB04.Add("FF","Family or Friends");
			EB04.Add("GP","Group Policy");
			EB04.Add("HM","Health Maintenance Organization (HMO)");
			EB04.Add("HN","Health Maintenance Organization (HMO) - Medicare Risk");
			EB04.Add("HS","Special Low Income Medicare Beneficiary");
			EB04.Add("IN","Indemnity");
			EB04.Add("IP","Individual Policy");
			EB04.Add("LC","Long Term Care");
			EB04.Add("LD","Long Term Policy");
			EB04.Add("LI","Life Insurance");
			EB04.Add("LT","Litigation");
			EB04.Add("MA","Medicare Part A");
			EB04.Add("MB","Medicare Part B");
			EB04.Add("MC","Medicaid");
			EB04.Add("MH","Medigap Part A");
			EB04.Add("MI","Medigap Part B");
			EB04.Add("MP","Medicare Primary");
			EB04.Add("OT","Other");
			EB04.Add("PE","Property Insurance - Personal");
			EB04.Add("PL","Personal");
			EB04.Add("PP","Personal Payment (Cash - No Insurance)");
			EB04.Add("PR","Preferred Provider Organization (PPO)");
			EB04.Add("PS","Point of Service (POS)");
			EB04.Add("QM","Qualified Medicare Beneficiary");
			EB04.Add("RP","Property Insurance - Real");
			EB04.Add("SP","Supplemental Policy");
			EB04.Add("TF","Tax Equity Fiscal Responsibility Act (TEFRA)");
			EB04.Add("WC","Workers Compensation");
			EB04.Add("WU","Wrap Up Policy");
			//------------------------------------------------------------------------------------------------------
			EB06=new Dictionary<string,string>();
			EB06.Add("6","Hour");
			EB06.Add("7","Day");
			EB06.Add("13","24 Hours");
			EB06.Add("21","Years");
			EB06.Add("22","Service Year");
			EB06.Add("23","Calendar Year");
			EB06.Add("24","Year to Date");
			EB06.Add("25","Contract");
			EB06.Add("26","Episode");
			EB06.Add("27","Visit");
			EB06.Add("28","Outlier");
			EB06.Add("29","Remaining");
			EB06.Add("30","Exceeded");
			EB06.Add("31","Not Exceeded");
			EB06.Add("32","Lifetime");
			EB06.Add("33","Lifetime Remaining");
			EB06.Add("34","Month");
			EB06.Add("35","Week");
			EB06.Add("36","Admisson");
			//------------------------------------------------------------------------------------------------------
			EB09=new Dictionary<string,string>();
			EB09.Add("99","Quantity Used");
			EB09.Add("CA","Covered - Actual");
			EB09.Add("CE","Covered - Estimated");
			EB09.Add("DB","Deductible Blood Units");
			EB09.Add("DY","Days");
			EB09.Add("HS","Hours");
			EB09.Add("LA","Life-time Reserve - Actual");
			EB09.Add("LE","Life-time Reserve - Estimated");
			EB09.Add("MN","Month");
			EB09.Add("P6","Number of Services or Procedures");
			EB09.Add("QA","Quantity Approved");
			EB09.Add("S7","Age, High Value");
			EB09.Add("S8","Age, Low Value");
			EB09.Add("VS","Visits");
			EB09.Add("YY","Years");

		}
		


	}

	public class EB01 {
		private string code;
		private string descript;
		private InsBenefitType benefitType;
		private bool isSupported;

		public EB01(string code,string descript,InsBenefitType benefitType) {
			this.code=code;
			this.descript=descript;
			this.benefitType=benefitType;
			this.isSupported=true;
		}

		public EB01(string code,string descript) {
			this.code=code;
			this.descript=descript;
			this.benefitType=InsBenefitType.ActiveCoverage;//ignored
			this.isSupported=false;
		}

		public InsBenefitType BenefitType {
			get { return benefitType; }
			set { benefitType = value; }
		}

		public string Code {
			get { return code; }
			set { code = value; }
		}
		
		public string Descript {
			get { return descript; }
			set { descript = value; }
		}

		public bool IsSupported {
			get { return isSupported; }
			set { isSupported = value; }
		}
	}

	public class EB02 {
		private string code;
		private string descript;
		private BenefitCoverageLevel coverageLevel;
		private bool isSupported;

		public EB02(string code,string descript,BenefitCoverageLevel coverageLevel) {
			this.code=code;
			this.descript=descript;
			this.coverageLevel=coverageLevel;
			this.isSupported=true;
		}

		public EB02(string code,string descript) {
			this.code=code;
			this.descript=descript;
			this.coverageLevel=BenefitCoverageLevel.Individual;//ignored
			this.isSupported=false;
		}

		public BenefitCoverageLevel CoverageLevel {
			get { return coverageLevel; }
			set { coverageLevel = value; }
		}

		public string Code {
			get { return code; }
			set { code = value; }
		}

		public string Descript {
			get { return descript; }
			set { descript = value; }
		}

		public bool IsSupported {
			get { return isSupported; }
			set { isSupported = value; }
		}
	}

	public class EB03 {
		private string code;
		private string descript;
		private EbenefitCategory serviceType;
		private bool isSupported;

		public EB03(string code,string descript,EbenefitCategory serviceType) {
			this.code=code;
			this.descript=descript;
			this.serviceType=serviceType;
			this.isSupported=true;
		}

		public EB03(string code,string descript) {
			this.code=code;
			this.descript=descript;
			this.serviceType=EbenefitCategory.None;//ignored
			this.isSupported=false;
		}

		public EbenefitCategory ServiceType {
			get { return serviceType; }
			set { serviceType = value; }
		}

		public string Code {
			get { return code; }
			set { code = value; }
		}

		public string Descript {
			get { return descript; }
			set { descript = value; }
		}

		public bool IsSupported {
			get { return isSupported; }
			set { isSupported = value; }
		}
	}



}
