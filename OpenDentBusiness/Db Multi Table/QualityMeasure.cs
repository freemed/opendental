using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	///<summary>Used by ehr.</summary>
	[Serializable]
	public class QualityMeasure {
		public QualityType Type;
		public QualityType2014 Type2014;
		public string Id;
		public string Descript;
		public int Denominator;
		public int Numerator;
		public int Exclusions;
		public int Exceptions;
		///<summary>Denominator-Exceptions-Exclusions-Numerator.  Those that do not fall into a sub-population.</summary>
		public int NotMet;
		///<summary>This represents the percentage of patients in the denominator who fall into one of the other sub-populations.  The Reporting Rate is calculated as: Rate=(Numerator+Exclusions+Exceptions)/Denominator. See \\SERVERFILES\storage\EHR\Quality Measures\QRDA\CDAR2_QRDA_CATIII_DSTU_R1_2012NOV\CDAR2_QRDAIII_DSTU_R1_2012NOV.pdf page 86.</summary>
		public decimal ReportingRate;
		///<summary>The performance rate is a ratio of patients that meet the numerator criteria divided by patients in the denominator (after accounting for exclusions and exceptions).  Rate = Numerator/(Denominator-Exclusions-Exceptions).</summary>
		public decimal PerformanceRate;
		public string DenominatorExplain;
		public string NumeratorExplain;
		public string ExclusionsExplain;
		public string ExceptionsExplain;
		public List<EhrCqmPatient> ListEhrPats;
		public Dictionary<long,List<EhrCqmEncounter>> DictPatNumListEncounters;
		public Dictionary<long,List<EhrCqmMeasEvent>> DictPatNumListMeasureEvents;
		public Dictionary<long,List<EhrCqmIntervention>> DictPatNumListInterventions;
		public Dictionary<long,List<EhrCqmProblem>> DictPatNumListProblems;
		public Dictionary<long,List<EhrCqmMedicationPat>> DictPatNumListMedPats;
		public Dictionary<long,List<EhrCqmNotPerf>> DictPatNumListNotPerfs;
		public Dictionary<long,List<EhrCqmProc>> DictPatNumListProcs;
		public Dictionary<long,List<EhrCqmVitalsign>> DictPatNumListVitalsigns;

		///<summary></summary>
		public QualityMeasure Copy() {
			QualityMeasure qualityMeasure=(QualityMeasure)this.MemberwiseClone();
			qualityMeasure.ListEhrPats=new List<EhrCqmPatient>();
			for(int i=0;i<ListEhrPats.Count;i++) {
				qualityMeasure.ListEhrPats.Add(ListEhrPats[i].Copy());
			}
			return qualityMeasure;
		}
	}

	public enum QualityType {
		WeightOver65,
		WeightAdult,
		Hypertension,
		TobaccoUse,
		TobaccoCessation,
		InfluenzaAdult,
		WeightChild_1_1,
		WeightChild_1_2,
		WeightChild_1_3,
		WeightChild_2_1,
		WeightChild_2_2,
		WeightChild_2_3,
		WeightChild_3_1,
		WeightChild_3_2,
		WeightChild_3_3,
		ImmunizeChild_1,
		ImmunizeChild_2,
		ImmunizeChild_3,
		ImmunizeChild_4,
		ImmunizeChild_5,
		ImmunizeChild_6,
		ImmunizeChild_7,
		ImmunizeChild_8,
		ImmunizeChild_9,
		ImmunizeChild_10,
		ImmunizeChild_11,
		ImmunizeChild_12,
		Pneumonia,
		DiabetesBloodPressure,
		BloodPressureManage
	}

	public enum QualityType2014 {
		MedicationsEntered,
		WeightOver65,
		WeightAdult,
		CariesPrevent,
		CariesPrevent_1,
		CariesPrevent_2,
		CariesPrevent_3,
		ChildCaries,
		Pneumonia,
		TobaccoCessation,
		Influenza,
		///<summary>patients 3-16 with height, weight, and BMI recorded</summary>
		WeightChild_1_1,
		///<summary>patients 3-16 counseled for nutrition</summary>
		WeightChild_1_2,
		///<summary>patients 3-16 counseled for physical activity</summary>
		WeightChild_1_3,
		///<summary>patients 3-11 with height, weight, and BMI recorded</summary>
		WeightChild_2_1,
		///<summary>patients 3-11 counseled for nutrition</summary>
		WeightChild_2_2,
		///<summary>patients 3-11 counseled for physical activity</summary>
		WeightChild_2_3,
		///<summary>patients 12-16 with height, weight, and BMI recorded</summary>
		WeightChild_3_1,
		///<summary>patients 12-16 counseled for nutrition</summary>
		WeightChild_3_2,
		///<summary>patients 12-16 counseled for physical activity</summary>
		WeightChild_3_3,
		BloodPressureManage
	}

	///<summary>This is all of the patient data required for QRDA category 1 reporting (in the patient recordTarget section).  If the patient is in ListEhrPats for this EhrCqmData object, the patient is part of the initial patient population.  The patient will also be placed into the 'Numerator', 'Exclusion', or 'Exception' category, for counting up results for the QRDA category 3 report for this measure.  A short explanation will be provided if the patient is not in the 'Numerator' to help the user improve their percentage.</summary>
	public class EhrCqmPatient {
		public Patient EhrCqmPat;
		public List<PatientRace> ListPatientRaces;
		public PatientRace Ethnicity;
		public string PayorSopCode;
		public string PayorDescription;
		public string PayorValueSetOID;
		public bool IsNumerator;
		public bool IsExclusion;
		public bool IsException;
		public string Explanation;

		public EhrCqmPatient Copy() {
			return (EhrCqmPatient)this.MemberwiseClone();
		}
	}

	public class EhrCqmEncounter {
		public long EhrCqmEncounterNum;
		public long PatNum;
		public long ProvNum;
		public string CodeValue;
		public string CodeSystemName;
		public string CodeSystemOID;
		public string Description;
		public string ValueSetName;
		public string ValueSetOID;
		public DateTime DateEncounter;
	}

	public class EhrCqmIntervention {
		public long EhrCqmInterventionNum;
		public long PatNum;
		public long ProvNum;
		public string CodeValue;
		public string CodeSystemName;
		public string CodeSystemOID;
		public string Description;
		public string ValueSetName;
		public string ValueSetOID;
		public DateTime DateEntry;
	}

	public class EhrCqmProblem {
		public long EhrCqmProblemNum;
		public long PatNum;
		public string CodeValue;
		public string CodeSystemName;
		public string CodeSystemOID;
		public string Description;
		public string ValueSetName;
		public string ValueSetOID;
		public DateTime DateStart;
		public DateTime DateStop;
	}

	public class EhrCqmMedicationPat {
		public long EhrCqmMedicationPatNum;
		public long PatNum;
		public long RxCui;
		public string CodeSystemName;
		public string CodeSystemOID;
		public string Description;
		public string ValueSetName;
		public string ValueSetOID;
		public string PatNote;
		public DateTime DateStart;
		public DateTime DateStop;
	}

	public class EhrCqmNotPerf {
		public long EhrCqmNotPerfNum;
		public long PatNum;
		public string CodeValue;
		public string CodeSystemName;
		public string CodeSystemOID;
		public string Description;
		public string ValueSetName;
		public string ValueSetOID;
		public string CodeValueReason;
		public string CodeSystemNameReason;
		public string CodeSystemOIDReason;
		public string DescriptionReason;
		public string ValueSetNameReason;
		public string ValueSetOIDReason;
		public DateTime DateEntry;
	}

	public class EhrCqmMeasEvent {
		public long EhrCqmMeasEventNum;
		public long PatNum;
		public string CodeValue;
		public string CodeSystemName;
		public string CodeSystemOID;
		public string Description;
		public string ValueSetName;
		public string ValueSetOID;
		public DateTime DateTEvent;
	}

	public class EhrCqmProc {
		public long EhrCqmProcNum;
		public long PatNum;
		public long ProvNum;
		public string ProcCode;
		public string CodeSystemName;
		public string CodeSystemOID;
		public string Description;
		public string ValueSetName;
		public string ValueSetOID;
		public DateTime ProcDate;
	}

	public class EhrCqmVitalsign {
		public long EhrCqmVitalsignNum;
		public long PatNum;
		public decimal BMI;//in kg/m2, if valid BMI value: CodeValue=39156-5, CodeSystemName=LOINC, CodeSystemOID=2.16.840.1.113883.6.1, Description=Body mass index (BMI) [Ratio], ValueSetName=BMI LOINC Value, ValueSetOID=2.16.840.1.113883.3.600.1.681
		public int BpSystolic;//for BP Systolic exam: CodeValue=8480-6, CodeSystemName=LOINC, CodeSystemOID=2.16.840.1.113883.6.1, Description=Systolic blood pressure, ValueSetName=Systolic Blood Pressure, ValueSetOID=2.16.840.1.113883.3.526.3.1032
		public int BpDiastolic;//for BP Diastolic exam: CodeValue=8462-4, CodeSystemName=LOINC, CodeSystemOID=2.16.840.1.113883.6.1, Description=Diastolic blood pressure, ValueSetName=Diastolic Blood Pressure, ValueSetOID=2.16.840.1.113883.3.526.3.1033
		public string HeightExamCode;//LOINC codes only
		public string HeightExamDescript;//from LOINC table
		public string WeightExamCode;//LOINC codes only
		public string WeightExamDescript;//from LOINC table
		public string BMIExamCode;//Percentile code, LOINC codes only
		public string BMIPercentileDescript;//from LOINC table
		public int BMIPercentile;//0-99, if -1 then not a valid BMI Percentile
		public DateTime DateTaken;
	}
}
