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
		private static Dictionary<string,string> EB03;
		private static Dictionary<string,string> EB04;
		private static Dictionary<string,string> EB06;
		private static Dictionary<string,string> EB09;

		public EB271(X12Segment segment)  {
			Segment=segment;
		}

		public string GetDescription() {
			if(eb01==null) {
				FillDictionaries();
			}
			string retVal="";
			retVal+=eb01.Find(EB01HasCode).Descript;//Eligibility or benefit information. Required
			if(Segment.Get(2) !="") {
				retVal+=", "+eb02.Find(EB02HasCode).Descript;//Coverage level code. Situational
			}
			if(Segment.Get(3) !="") {
				retVal+=", "+EB03[Segment.Get(3)];//Service type code. Situational
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
				retVal+=", "+(PIn.PDouble(Segment.Get(8))*100).ToString()+"%";//Monetary amount. Situational
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
			EB03=new Dictionary<string,string>();
			EB03.Add("1","Medical Care");
			EB03.Add("2","Surgical");
			EB03.Add("3","Consultation");
			EB03.Add("4","Diagnostic X-Ray");
			EB03.Add("5","Diagnostic Lab");
			EB03.Add("6","Radiation Therapy");
			EB03.Add("7","Anesthesia");
			EB03.Add("8","Surgical Assistance");
			EB03.Add("9","Other Medical");
			EB03.Add("10","Blood Charges");
			EB03.Add("11","Used Durable Medical Equipment");
			EB03.Add("12","Durable Medical Equipment Purchase");
			EB03.Add("13","Ambulatory Service Center Facility");
			EB03.Add("14","Renal Supplies in the Home");
			EB03.Add("15","Alternate Method Dialysis");
			EB03.Add("16","Chronic Renal Disease (CRD) Equipment");
			EB03.Add("17","Pre-Admission Testing");
			EB03.Add("18","Durable Medical Equipment Rental");
			EB03.Add("19","Pneumonia Vaccine");
			EB03.Add("20","Second Surgical Opinion");
			EB03.Add("21","Third Surgical Opinion");
			EB03.Add("22","Social Work");
			EB03.Add("23","Diagnostic Dental");
			EB03.Add("24","Periodontics");
			EB03.Add("25","Restorative");
			EB03.Add("26","Endodontics");
			EB03.Add("27","Maxillofacial Prosthetics");
			EB03.Add("28","Adjunctive Dental Services");
			EB03.Add("30","Health Benefit Plan Coverage");
			EB03.Add("32","Plan Waiting Period");
			EB03.Add("33","Chiropractic");
			EB03.Add("34","Chiropractic Office Visits");
			EB03.Add("35","Dental Care");
			EB03.Add("36","Dental Crowns");
			EB03.Add("37","Dental Accident");
			EB03.Add("38","Orthodontics");
			EB03.Add("39","Prosthodontics");
			EB03.Add("40","Oral Surgery");
			EB03.Add("41","Routine (Preventive) Dental");
			EB03.Add("42","Home Health Care");
			EB03.Add("43","Home Health Prescriptions");
			EB03.Add("44","Home Health Visits");
			EB03.Add("45","Hospice");
			EB03.Add("46","Respite Care");
			EB03.Add("47","Hospital");
			EB03.Add("48","Hospital - Inpatient");
			EB03.Add("49","Hospital - Room and Board");
			EB03.Add("50","Hospital - Outpatient");
			EB03.Add("51","Hospital - Emergency Accident");
			EB03.Add("52","Hospital - Emergency Medical");
			EB03.Add("53","Hospital - Ambulatory Surgical");
			EB03.Add("54","Long Term Care");
			EB03.Add("55","Major Medical");
			EB03.Add("56","Medically Related Transportation");
			EB03.Add("57","Air Transportation");
			EB03.Add("58","Cabulance");
			EB03.Add("59","Licensed Ambulance");
			EB03.Add("60","General Benefits");
			EB03.Add("61","In-vitro Fertilization");
			EB03.Add("62","MRI/CAT Scan");
			EB03.Add("63","Donor Procedures");
			EB03.Add("64","Acupuncture");
			EB03.Add("65","Newborn Care");
			EB03.Add("66","Pathology");
			EB03.Add("67","Smoking Cessation");
			EB03.Add("68","Well Baby Care");
			EB03.Add("69","Maternity");
			EB03.Add("70","Transplants");
			EB03.Add("71","Audiology Exam");
			EB03.Add("72","Inhalation Therapy");
			EB03.Add("73","Diagnostic Medical");
			EB03.Add("74","Private Duty Nursing");
			EB03.Add("75","Prosthetic Device");
			EB03.Add("76","Dialysis");
			EB03.Add("77","Otological Exam");
			EB03.Add("78","Chemotherapy");
			EB03.Add("79","Allergy Testing");
			EB03.Add("80","Immunizations");
			EB03.Add("81","Routine Physical");
			EB03.Add("82","Family Planning");
			EB03.Add("83","Infertility");
			EB03.Add("84","Abortion");
			EB03.Add("85","AIDS");
			EB03.Add("86","Emergency Services");
			EB03.Add("87","Cancer");
			EB03.Add("88","Pharmacy");
			EB03.Add("89","Free Standing Prescription Drug");
			EB03.Add("90","Mail Order Prescription Drug");
			EB03.Add("91","Brand Name Prescription Drug");
			EB03.Add("92","Generic Prescription Drug");
			EB03.Add("93","Podiatry");
			EB03.Add("94","Podiatry - Office Visits");
			EB03.Add("95","Podiatry - Nursing Home Visits");
			EB03.Add("96","Professional (Physician)");
			EB03.Add("97","Anesthesiologist");
			EB03.Add("98","Professional (Physician) Visit - Office");
			EB03.Add("99","Professional (Physician) Visit - Inpatient");
			EB03.Add("A0","Professional (Physician) Visit - Outpatient");
			EB03.Add("A1","Professional (Physician) Visit - Nursing Home");
			EB03.Add("A2","Professional (Physician) Visit - Skilled Nursing Facility");
			EB03.Add("A3","Professional (Physician) Visit - Home");
			EB03.Add("A4","Psychiatric");
			EB03.Add("A5","Psychiatric - Room and Board");
			EB03.Add("A6","Psychotherapy");
			EB03.Add("A7","Psychiatric - Inpatient");
			EB03.Add("A8","Psychiatric - Outpatient");
			EB03.Add("A9","Rehabilitation");
			EB03.Add("AA","Rehabilitation - Room and Board");
			EB03.Add("AB","Rehabilitation - Inpatient");
			EB03.Add("AC","Rehabilitation - Outpatient");
			EB03.Add("AD","Occupational Therapy");
			EB03.Add("AE","Physical Medicine");
			EB03.Add("AF","Speech Therapy");
			EB03.Add("AG","Skilled Nursing Care");
			EB03.Add("AH","Skilled Nursing Care - Room and Board");
			EB03.Add("AI","Substance Abuse");
			EB03.Add("AJ","Alcoholism");
			EB03.Add("AK","Drug Addiction");
			EB03.Add("AL","Vision (Optometry)");
			EB03.Add("AM","Frames");
			EB03.Add("AN","Routine Exam");
			EB03.Add("AO","Lenses");
			EB03.Add("AQ","Nonmedically Necessary Physical");
			EB03.Add("AR","Experimental Drug Therapy");
			EB03.Add("BA","Independent Medical Evaluation");
			EB03.Add("BB","Partial Hospitalization (Psychiatric)");
			EB03.Add("BC","Day Care (Psychiatric)");
			EB03.Add("BD","Cognitive Therapy");
			EB03.Add("BE","Massage Therapy");
			EB03.Add("BF","Pulmonary Rehabilitation");
			EB03.Add("BG","Cardiac Rehabilitation");
			EB03.Add("BH","Pediatric");
			EB03.Add("BI","Nursery");
			EB03.Add("BJ","Skin");
			EB03.Add("BK","Orthopedic");
			EB03.Add("BL","Cardiac");
			EB03.Add("BM","Lymphatic");
			EB03.Add("BN","Gastrointestinal");
			EB03.Add("BP","Endocrine");
			EB03.Add("BQ","Neurology");
			EB03.Add("BR","Eye");
			EB03.Add("BS","Invasive Procedures");
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



}
