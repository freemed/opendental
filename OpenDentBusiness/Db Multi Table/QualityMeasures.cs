using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used in Ehr quality measures.</summary>
	public class QualityMeasures {
		private static string _elapsedtimetext;
		private static QualityMeasure _measureWeightAssessAll;
		private static QualityMeasure _measureWeightAssess3To11;
		private static QualityMeasure _measureWeightAssess12To16;

		///<summary>Generates a list of all the quality measures for 2011.  Performs all calculations and manipulations.  Returns list for viewing/output.</summary>
		public static List<QualityMeasure> GetAll(DateTime dateStart,DateTime dateEnd,long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<QualityMeasure>>(MethodBase.GetCurrentMethod(),dateStart,dateEnd,provNum);
			}
			List<QualityMeasure> list=new List<QualityMeasure>();
			//add one of each type
			QualityMeasure measure;
			for(int i=0;i<Enum.GetValues(typeof(QualityType)).Length;i++) {
				measure=new QualityMeasure();
				measure.Type=(QualityType)i;
				measure.Id=GetId(measure.Type);
				measure.Descript=GetDescript(measure.Type);
				DataTable table=GetTable(measure.Type,dateStart,dateEnd,provNum);
				if(table!=null) {
					measure.Denominator=table.Rows.Count;
					measure.Numerator=CalcNumerator(table);
					measure.Exclusions=CalcExclusions(table);
					measure.NotMet=measure.Denominator-measure.Exclusions-measure.Numerator;
					measure.ReportingRate=100;
					measure.PerformanceRate=0;
					if(measure.Numerator > 0) {
						measure.PerformanceRate=(int)((float)(measure.Numerator*100)/(float)(measure.Numerator+measure.NotMet));
					}
					measure.DenominatorExplain=GetDenominatorExplain(measure.Type);
					measure.NumeratorExplain=GetNumeratorExplain(measure.Type);
					measure.ExclusionsExplain=GetExclusionsExplain(measure.Type);
				}
				list.Add(measure);
			}
			return list;
		}

		///<summary>Generates a list of all the quality measures for 2014.  Performs all calculations and manipulations.  Returns list for viewing/output.</summary>
		public static List<QualityMeasure> GetAll2014(DateTime dateStart,DateTime dateEnd,long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<QualityMeasure>>(MethodBase.GetCurrentMethod(),dateStart,dateEnd,provNum);
			}
			List<QualityMeasure> list=new List<QualityMeasure>();
			//add one of each type
			QualityMeasure measureCur;
			_measureWeightAssessAll=new QualityMeasure();
			_measureWeightAssess3To11=new QualityMeasure();
			_measureWeightAssess12To16=new QualityMeasure();
			_elapsedtimetext="Elapsed time for each measurement.\r\n";
			for(int i=0;i<Enum.GetValues(typeof(QualityType2014)).Length;i++) {
				measureCur=GetEhrCqmData((QualityType2014)i,dateStart,dateEnd,provNum);
				measureCur.Type2014=(QualityType2014)i;
				measureCur.Id=GetId2014(measureCur.Type2014);
				measureCur.Descript=GetDescript2014(measureCur.Type2014);
				if(measureCur.ListEhrPats!=null) {
					if((QualityType2014)i==QualityType2014.Influenza) {
						//only have to count IsDenominator pats for influenza measure, all other measures will have every patient marked IsDenominator (denom=initial pat population)
						measureCur.Denominator=CalcDenominator2014(measureCur.ListEhrPats);
					}
					else {
						measureCur.Denominator=measureCur.ListEhrPats.Count;
					}
					measureCur.Numerator=CalcNumerator2014(measureCur.ListEhrPats);
					measureCur.Exclusions=CalcExclusions2014(measureCur.ListEhrPats);
					measureCur.Exceptions=CalcExceptions2014(measureCur.ListEhrPats);
					measureCur.NotMet=measureCur.Denominator-measureCur.Exclusions-measureCur.Numerator-measureCur.Exceptions;
					//Reporting rate is (Numerator+Exclusions+Exceptions)/Denominator.  Percentage of qualifying pats classified in one of the three groups Numerator, Exception, Exclusion.
					measureCur.ReportingRate=0;
					if(measureCur.Denominator>0) {
						measureCur.ReportingRate=Math.Round(((decimal)((measureCur.Numerator+measureCur.Exclusions+measureCur.Exceptions)*100)/(decimal)(measureCur.Denominator)),1,MidpointRounding.AwayFromZero);
					}
					//Performance rate is Numerator/(Denominator-Exclusions-Exceptions).  Percentage of qualifying pats (who were not in the Exclusions or Exceptions) who were in the Numerator.
					measureCur.PerformanceRate=0;
					if(measureCur.Numerator>0) {
						measureCur.PerformanceRate=Math.Round(((decimal)(measureCur.Numerator*100)/(decimal)(measureCur.Denominator-measureCur.Exclusions-measureCur.Exceptions)),1,MidpointRounding.AwayFromZero);
					}
					measureCur.DenominatorExplain=GetDenominatorExplain2014(measureCur.Type2014);
					measureCur.NumeratorExplain=GetNumeratorExplain2014(measureCur.Type2014);
					measureCur.ExclusionsExplain=GetExclusionsExplain2014(measureCur.Type2014);
					measureCur.ExceptionsExplain=GetExceptionsExplain2014(measureCur.Type2014);
				}
				list.Add(measureCur);
			}
			System.Windows.Forms.MessageBox.Show(_elapsedtimetext);
			return list;
		}

		private static string GetId(QualityType qtype){
			switch(qtype) {
				case QualityType.WeightOver65:
					return "0421a";
				case QualityType.WeightAdult:
					return "0421b";
				case QualityType.Hypertension:
					return "0013";
				case QualityType.TobaccoUse:
					return "0028a";
				case QualityType.TobaccoCessation:
					return "0028b";
				case QualityType.InfluenzaAdult:
					return "0041";
				case QualityType.WeightChild_1_1:
					return "0024-1.1";
				case QualityType.WeightChild_1_2:
					return "0024-1.2";
				case QualityType.WeightChild_1_3:
					return "0024-1.3";
				case QualityType.WeightChild_2_1:
					return "0024-2.1";
				case QualityType.WeightChild_2_2:
					return "0024-2.2";
				case QualityType.WeightChild_2_3:
					return "0024-2.3";
				case QualityType.WeightChild_3_1:
					return "0024-3.1";
				case QualityType.WeightChild_3_2:
					return "0024-3.2";
				case QualityType.WeightChild_3_3:
					return "0024-3.3";
				case QualityType.ImmunizeChild_1:
					return "0038-1";
				case QualityType.ImmunizeChild_2:
					return "0038-2";
				case QualityType.ImmunizeChild_3:
					return "0038-3";
				case QualityType.ImmunizeChild_4:
					return "0038-4";
				case QualityType.ImmunizeChild_5:
					return "0038-5";
				case QualityType.ImmunizeChild_6:
					return "0038-6";
				case QualityType.ImmunizeChild_7:
					return "0038-7";
				case QualityType.ImmunizeChild_8:
					return "0038-8";
				case QualityType.ImmunizeChild_9:
					return "0038-9";
				case QualityType.ImmunizeChild_10:
					return "0038-10";
				case QualityType.ImmunizeChild_11:
					return "0038-11";
				case QualityType.ImmunizeChild_12:
					return "0038-12";		
				case QualityType.Pneumonia:
					return "0043";
				case QualityType.DiabetesBloodPressure:
					return "0061";
				case QualityType.BloodPressureManage:
					return "0018";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetId2014(QualityType2014 qtype) {
			switch(qtype) {
				case QualityType2014.MedicationsEntered:
					return "68";
				case QualityType2014.WeightOver65:
					return "69 Pop. 1";
				case QualityType2014.WeightAdult:
					return "69 Pop. 2";
				case QualityType2014.CariesPrevent:
					return "74 Pop. All";
				case QualityType2014.CariesPrevent_1:
					return "74 Pop. 1";
				case QualityType2014.CariesPrevent_2:
					return "74 Pop. 2";
				case QualityType2014.CariesPrevent_3:
					return "74 Pop. 3";
				case QualityType2014.ChildCaries:
					return "75";
				case QualityType2014.Pneumonia:
					return "127";
				case QualityType2014.TobaccoCessation:
					return "138";
				case QualityType2014.Influenza:
					return "147";
				case QualityType2014.WeightChild_1_1:
					return "155-1 Pop. All";
				case QualityType2014.WeightChild_1_2:
					return "155-2 Pop. All";
				case QualityType2014.WeightChild_1_3:
					return "155-3 Pop. All";
				case QualityType2014.WeightChild_2_1:
					return "155-1 Pop. 1";
				case QualityType2014.WeightChild_2_2:
					return "155-2 Pop. 1";
				case QualityType2014.WeightChild_2_3:
					return "155-3 Pop. 1";
				case QualityType2014.WeightChild_3_1:
					return "155-1 Pop. 2";
				case QualityType2014.WeightChild_3_2:
					return "155-2 Pop. 2";
				case QualityType2014.WeightChild_3_3:
					return "155-3 Pop. 2";
				case QualityType2014.BloodPressureManage:
					return "165";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		///<summary>Used in reporting, and only for certain types.</summary>
		public static string GetPQRIMeasureNumber(QualityType qtype) {
			switch(qtype) {
				case QualityType.WeightOver65:
					return "128";//"0421";
				case QualityType.Hypertension:
					return "0013";
				case QualityType.TobaccoUse:
					return "114";//"0028";
				case QualityType.InfluenzaAdult:
					return "110";//"0041";
				case QualityType.WeightChild_1_1:
					return "0024";
				case QualityType.ImmunizeChild_1:
					return "0038";
				case QualityType.Pneumonia:
					return "111";//"0043";
				case QualityType.DiabetesBloodPressure:
					return "3";//"0061";
				case QualityType.BloodPressureManage:
					return "0018";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetDescript(QualityType qtype) {
			switch(qtype) {
				case QualityType.WeightOver65:
					return "Weight, Adult, 65+";
				case QualityType.WeightAdult:
					return "Weight, Adult, 18 to 64";
				case QualityType.Hypertension:
					return "Hypertension";
				case QualityType.TobaccoUse:
					return "Tobacco Use Assessment";
				case QualityType.TobaccoCessation:
					return "Tobacco Cessation Intervention";
				case QualityType.InfluenzaAdult:
					return "Influenza Immunization, 50+";
				case QualityType.WeightChild_1_1:
					return "Weight, Child 2-16, BMI";
				case QualityType.WeightChild_1_2:
					return "Weight, Child 2-16, nutrition";
				case QualityType.WeightChild_1_3:
					return "Weight, Child 2-16, physical";
				case QualityType.WeightChild_2_1:
					return "Weight, Child 2-10, BMI";
				case QualityType.WeightChild_2_2:
					return "Weight, Child 2-10, nutrition";
				case QualityType.WeightChild_2_3:
					return "Weight, Child 2-10, physical";
				case QualityType.WeightChild_3_1:
					return "Weight, Child 11-16, BMI";
				case QualityType.WeightChild_3_2:
					return "Weight, Child 11-16, nutrition";
				case QualityType.WeightChild_3_3:
					return "Weight, Child 11-16, physical";
				case QualityType.ImmunizeChild_1:
					return "Immun Status, Child, DTaP";
				case QualityType.ImmunizeChild_2:
					return "Immun Status, Child, IPV";
				case QualityType.ImmunizeChild_3:
					return "Immun Status, Child, MMR";
				case QualityType.ImmunizeChild_4:
					return "Immun Status, Child, HiB";
				case QualityType.ImmunizeChild_5:
					return "Immun Status, Child, hep B";
				case QualityType.ImmunizeChild_6:
					return "Immun Status, Child, VZV";
				case QualityType.ImmunizeChild_7:
					return "Immun Status, Child, pneumococcal";
				case QualityType.ImmunizeChild_8:
					return "Immun Status, Child, hep A";
				case QualityType.ImmunizeChild_9:
					return "Immun Status, Child, rotavirus";
				case QualityType.ImmunizeChild_10:
					return "Immun Status, Child, influenza";
				case QualityType.ImmunizeChild_11:
					return "Immun Status, Child, 1-6";
				case QualityType.ImmunizeChild_12:
					return "Immun Status, Child, 1-7";
				case QualityType.Pneumonia:
					return "Pneumonia Immunization, 64+";
				case QualityType.DiabetesBloodPressure:
					return "Diabetes: BP Management";
				case QualityType.BloodPressureManage:
					return "Controlling High BP";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetDescript2014(QualityType2014 qtype) {
			switch(qtype) {
				case QualityType2014.MedicationsEntered:
					return "Document Current Medications";
				case QualityType2014.WeightOver65:
					return "BMI Screening and Follow-Up, 65+";
				case QualityType2014.WeightAdult:
					return "BMI Screening and Follow-Up, 18-64";
				case QualityType2014.CariesPrevent:
					return "Caries Prevention, 0-20";
				case QualityType2014.CariesPrevent_1:
					return "Caries Prevention, 0-5";
				case QualityType2014.CariesPrevent_2:
					return "Caries Prevention, 6-12";
				case QualityType2014.CariesPrevent_3:
					return "Caries Prevention, 13-20";
				case QualityType2014.ChildCaries:
					return "Child Dental Decay, 0-20";
				case QualityType2014.Pneumonia:
					return "Pneumococcal Vaccination, 65+";
				case QualityType2014.TobaccoCessation:
					return "Tobacco Cessation Intervention";
				case QualityType2014.Influenza:
					return "Influenza Immunization, 6 months+";
				case QualityType2014.WeightChild_1_1:
					return "BMI Assessment, 3-17";
				case QualityType2014.WeightChild_1_2:
					return "Nutrition Counseling, 3-17";
				case QualityType2014.WeightChild_1_3:
					return "Physical Activity Counseling, 3-17";
				case QualityType2014.WeightChild_2_1:
					return "BMI Assessment, 3-11";
				case QualityType2014.WeightChild_2_2:
					return "Nutrition Counseling, 3-11";
				case QualityType2014.WeightChild_2_3:
					return "Physical Activity Counseling, 3-11";
				case QualityType2014.WeightChild_3_1:
					return "BMI Assessment, 12-17";
				case QualityType2014.WeightChild_3_2:
					return "Nutrition Counseling, 12-17";
				case QualityType2014.WeightChild_3_3:
					return "Physical Activity Counseling, 12-17";
				case QualityType2014.BloodPressureManage:
					return "Controlling High BP";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		public static DataTable GetTable(QualityType qtype,DateTime dateStart,DateTime dateEnd,long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),qtype,dateStart,dateEnd,provNum);
			}
			//these queries only work for mysql
			string command="";
			DataTable tableRaw=new DataTable();
			switch(qtype) {
				#region WeightOver65
				case QualityType.WeightOver65:
					//WeightOver65-------------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						DateVisit date NOT NULL DEFAULT '0001-01-01',
						Height float NOT NULL,
						Weight float NOT NULL,
						HasFollowupPlan tinyint NOT NULL,
						IsIneligible tinyint NOT NULL,
						Documentation varchar(255) NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName,DateVisit) SELECT patient.PatNum,LName,FName,"
						+"MAX(ProcDate) "//on the first pass, all we can obtain is the date of the visit
						+"FROM patient "
						+"INNER JOIN procedurelog "//because we want to restrict to only results with procedurelog
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate > '1880-01-01' AND Birthdate <= "+POut.Date(DateTime.Today.AddYears(-65))+" "//65 or older
						+"GROUP BY patient.PatNum";//there will frequently be multiple procedurelog events
					Db.NonQ(command);
					//now, find BMIs within 6 months of each visit date. No logic for picking one of multiple BMIs.
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.Height=vitalsign.Height, "
						+"tempehrquality.Weight=vitalsign.Weight, "//we could also easily get the BMI date if we wanted.
						+"tempehrquality.HasFollowupPlan=vitalsign.HasFollowupPlan, "
						+"tempehrquality.IsIneligible=vitalsign.IsIneligible, "
						+"tempehrquality.Documentation=vitalsign.Documentation "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken <= tempehrquality.DateVisit "
						+"AND vitalsign.DateTaken >= DATE_SUB(tempehrquality.DateVisit,INTERVAL 6 MONTH)";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region WeightAdult
				case QualityType.WeightAdult:
					//WeightAdult---------------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						DateVisit date NOT NULL DEFAULT '0001-01-01',
						Height float NOT NULL,
						Weight float NOT NULL,
						HasFollowupPlan tinyint NOT NULL,
						IsIneligible tinyint NOT NULL,
						Documentation varchar(255) NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName,DateVisit) SELECT patient.PatNum,LName,FName,"
						+"MAX(ProcDate) "//on the first pass, all we can obtain is the date of the visit
						+"FROM patient "
						+"INNER JOIN procedurelog "//because we want to restrict to only results with procedurelog
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate <= "+POut.Date(DateTime.Today.AddYears(-18))+" "//18+
						+"AND Birthdate > "+POut.Date(DateTime.Today.AddYears(-65))+" "//less than 65
						+"GROUP BY patient.PatNum";//there will frequently be multiple procedurelog events
					Db.NonQ(command);
					//now, find BMIs within 6 months of each visit date. No logic for picking one of multiple BMIs.
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.Height=vitalsign.Height, "
						+"tempehrquality.Weight=vitalsign.Weight, "
						+"tempehrquality.HasFollowupPlan=vitalsign.HasFollowupPlan, "
						+"tempehrquality.IsIneligible=vitalsign.IsIneligible, "
						+"tempehrquality.Documentation=vitalsign.Documentation "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken <= tempehrquality.DateVisit "
						+"AND vitalsign.DateTaken >= DATE_SUB(tempehrquality.DateVisit,INTERVAL 6 MONTH)";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region Hypertension
				case QualityType.Hypertension:
					//Hypertension---------------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						DateVisit date NOT NULL DEFAULT '0001-01-01',
						VisitCount int NOT NULL,
						Icd9Code varchar(255) NOT NULL,
						DateBpEntered date NOT NULL DEFAULT '0001-01-01'
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName,DateVisit,VisitCount,Icd9Code) "
						+"SELECT patient.PatNum,LName,FName,"
						+"MAX(ProcDate), "// most recent visit
						+"COUNT(DISTINCT ProcDate),diseasedef.ICD9Code "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"LEFT JOIN disease ON disease.PatNum=patient.PatNum "
						+"AND disease.DiseaseDefNum IN (SELECT DiseaseDefNum FROM diseasedef WHERE ICD9Code REGEXP '^40[1-4]') "//starts with 401 through 404
						//+"LEFT JOIN icd9 ON icd9.ICD9Num=disease.ICD9Num "
						+"LEFT JOIN diseasedef ON diseasedef.DiseaseDefNum=disease.DiseaseDefNum "
						//+"AND icd9.ICD9Code REGEXP '^40[1-4]' "//starts with 401 through 404
						+"WHERE Birthdate <= "+POut.Date(DateTime.Today.AddYears(-18))+" "//18+
						+"GROUP BY patient.PatNum";
					Db.NonQ(command);
					//now, find BMIs in measurement period.
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.DateBpEntered=vitalsign.DateTaken "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.BpSystolic != 0 "
						+"AND vitalsign.BpDiastolic != 0 "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd);
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region TobaccoUse
				case QualityType.TobaccoUse:
					//TobaccoUse---------------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						DateVisit date NOT NULL DEFAULT '0001-01-01',
						VisitCount int NOT NULL,
						DateAssessment date NOT NULL DEFAULT '0001-01-01'
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName,DateVisit,VisitCount) "
						+"SELECT patient.PatNum,LName,FName,"
						+"MAX(ProcDate), "// most recent visit
						+"COUNT(DISTINCT ProcDate) "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"WHERE Birthdate <= "+POut.Date(DateTime.Today.AddYears(-18))+" "//18+
						+"GROUP BY patient.PatNum";
					Db.NonQ(command);
					//now, find most recent tobacco assessment date.  We will check later that it is within 2 years of last exam.
					command="UPDATE tempehrquality "//,ehrmeasureevent "
						+"SET tempehrquality.DateAssessment=(SELECT MAX(DATE(ehrmeasureevent.DateTEvent)) "
						+"FROM ehrmeasureevent "
						+"WHERE tempehrquality.PatNum=ehrmeasureevent.PatNum "
						+"AND ehrmeasureevent.EventType="+POut.Int((int)EhrMeasureEventType.TobaccoUseAssessed)+")";
					Db.NonQ(command);
					command="UPDATE tempehrquality SET DateAssessment='0001-01-01' WHERE DateAssessment='0000-00-00'";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region TobaccoCessation
				case QualityType.TobaccoCessation:
					//TobaccoCessation----------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						DateVisit date NOT NULL DEFAULT '0001-01-01',
						DateAssessment date NOT NULL DEFAULT '0001-01-01',
						DateCessation date NOT NULL DEFAULT '0001-01-01',
						Documentation varchar(255) NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName,DateVisit) "
						+"SELECT patient.PatNum,LName,FName,"
						+"MAX(ProcDate) "// most recent visit
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"WHERE Birthdate <= "+POut.Date(DateTime.Today.AddYears(-18))+" "//18+
						+"AND patient.SmokingSnoMed IN('"+POut.String(SmokingSnoMed._449868002.ToString().Substring(1))+"','"
						+POut.String(SmokingSnoMed._428041000124106.ToString().Substring(1))+"','"
						+POut.String(SmokingSnoMed._428061000124105.ToString().Substring(1))+"','"
						+POut.String(SmokingSnoMed._428071000124103.ToString().Substring(1))+"') "//CurrentEveryDay,CurrentSomeDay,LightSmoker,HeavySmoker
						+"GROUP BY patient.PatNum";
					Db.NonQ(command);
					//find most recent tobacco assessment date.
					command="UPDATE tempehrquality "
						+"SET tempehrquality.DateAssessment=(SELECT MAX(DATE(ehrmeasureevent.DateTEvent)) "
						+"FROM ehrmeasureevent "
						+"WHERE tempehrquality.PatNum=ehrmeasureevent.PatNum "
						+"AND ehrmeasureevent.EventType="+POut.Int((int)EhrMeasureEventType.TobaccoUseAssessed)+")";
					Db.NonQ(command);
					command="UPDATE tempehrquality SET DateAssessment='0001-01-01' WHERE DateAssessment='0000-00-00'";
					Db.NonQ(command);
					//find most recent tobacco cessation date.
					command="UPDATE tempehrquality "
						+"SET tempehrquality.DateCessation=(SELECT MAX(DATE(ehrmeasureevent.DateTEvent)) "
						+"FROM ehrmeasureevent "
						+"WHERE tempehrquality.PatNum=ehrmeasureevent.PatNum "
						+"AND ehrmeasureevent.EventType="+POut.Int((int)EhrMeasureEventType.TobaccoCessation)+")";
					Db.NonQ(command);
					command="UPDATE tempehrquality SET DateCessation='0001-01-01' WHERE DateCessation='0000-00-00'";
					Db.NonQ(command);
					//Pull the documentation based on date
					command="UPDATE tempehrquality "
						+"SET Documentation=(SELECT ehrmeasureevent.MoreInfo "
						+"FROM ehrmeasureevent "
						+"WHERE tempehrquality.PatNum=ehrmeasureevent.PatNum "
						+"AND ehrmeasureevent.EventType="+POut.Int((int)EhrMeasureEventType.TobaccoCessation)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent)=tempehrquality.DateCessation) "
						+"WHERE DateCessation > '1880-01-01'";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region InfluenzaAdult
				case QualityType.InfluenzaAdult:
					//InfluenzaAdult----------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						DateVaccine date NOT NULL,
						NotGiven tinyint NOT NULL,
						Documentation varchar(255) NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName) SELECT patient.PatNum,LName,FName "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate > '1880-01-01' AND Birthdate <= "+POut.Date(DateTime.Today.AddYears(-50))+" "//50 or older
						+"GROUP BY patient.PatNum";
					Db.NonQ(command);
					//find most recent vaccine date
					command="UPDATE tempehrquality "
						+"SET tempehrquality.DateVaccine=(SELECT MAX(DATE(vaccinepat.DateTimeStart)) "
						+"FROM vaccinepat,vaccinedef "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND vaccinedef.CVXCode IN('135','15'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality SET DateVaccine='0001-01-01' WHERE DateVaccine='0000-00-00'";
					Db.NonQ(command);
					//pull documentation on vaccine exclusions based on date.
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET Documentation=Note, "
						+"tempehrquality.NotGiven=vaccinepat.NotGiven "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND DATE(vaccinepat.DateTimeStart)=tempehrquality.DateVaccine "
						+"AND vaccinedef.CVXCode IN('135','15')";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region WeightChild_1
				case QualityType.WeightChild_1_1:
				case QualityType.WeightChild_1_2:
				case QualityType.WeightChild_1_3:
					//WeightChild_1-----------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						IsPregnant tinyint NOT NULL,
						HasBMI tinyint NOT NULL,
						ChildGotNutrition tinyint NOT NULL,
						ChildGotPhysCouns tinyint NOT NULL				
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName) SELECT patient.PatNum,LName,FName "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate <= "+POut.Date(DateTime.Today.AddYears(-2))+" "//2+
						+"AND Birthdate > "+POut.Date(DateTime.Today.AddYears(-17))+" "//less than 17
						+"GROUP BY patient.PatNum";//there will frequently be multiple procedurelog events
					Db.NonQ(command);
					//find any BMIs within the period that indicate pregnancy
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.IsPregnant=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.IsIneligible=1";
					Db.NonQ(command);
					//find any BMIs within the period with a valid BMI
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.HasBMI=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.Height > 0 "
						+"AND vitalsign.Weight > 0";
					Db.NonQ(command);
					//find any BMIs within the period that indicate ChildGotNutrition
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.ChildGotNutrition=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.ChildGotNutrition=1";
					Db.NonQ(command);
					//find any BMIs within the period that indicate ChildGotPhysCouns
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.ChildGotPhysCouns=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.ChildGotPhysCouns=1";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region WeightChild_2
				case QualityType.WeightChild_2_1:
				case QualityType.WeightChild_2_2:
				case QualityType.WeightChild_2_3:
					//WeightChild_2-----------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						IsPregnant tinyint NOT NULL,
						HasBMI tinyint NOT NULL,
						ChildGotNutrition tinyint NOT NULL,
						ChildGotPhysCouns tinyint NOT NULL				
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName) SELECT patient.PatNum,LName,FName "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate <= "+POut.Date(DateTime.Today.AddYears(-2))+" "//2+
						+"AND Birthdate > "+POut.Date(DateTime.Today.AddYears(-11))+" "//less than 11
						+"GROUP BY patient.PatNum";//there will frequently be multiple procedurelog events
					Db.NonQ(command);
					//find any BMIs within the period that indicate pregnancy
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.IsPregnant=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.IsIneligible=1";
					Db.NonQ(command);
					//find any BMIs within the period with a valid BMI
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.HasBMI=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.Height > 0 "
						+"AND vitalsign.Weight > 0";
					Db.NonQ(command);
					//find any BMIs within the period that indicate ChildGotNutrition
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.ChildGotNutrition=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.ChildGotNutrition=1";
					Db.NonQ(command);
					//find any BMIs within the period that indicate ChildGotPhysCouns
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.ChildGotPhysCouns=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.ChildGotPhysCouns=1";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region WeightChild_3
				case QualityType.WeightChild_3_1:
				case QualityType.WeightChild_3_2:
				case QualityType.WeightChild_3_3:
					//WeightChild_3-----------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						IsPregnant tinyint NOT NULL,
						HasBMI tinyint NOT NULL,
						ChildGotNutrition tinyint NOT NULL,
						ChildGotPhysCouns tinyint NOT NULL			
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName) SELECT patient.PatNum,LName,FName "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate <= "+POut.Date(DateTime.Today.AddYears(-11))+" "//11+
						+"AND Birthdate > "+POut.Date(DateTime.Today.AddYears(-17))+" "//less than 17
						+"GROUP BY patient.PatNum";//there will frequently be multiple procedurelog events
					Db.NonQ(command);
					//find any BMIs within the period that indicate pregnancy
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.IsPregnant=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.IsIneligible=1";
					Db.NonQ(command);
					//find any BMIs within the period with a valid BMI
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.HasBMI=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.Height > 0 "
						+"AND vitalsign.Weight > 0";
					Db.NonQ(command);
					//find any BMIs within the period that indicate ChildGotNutrition
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.ChildGotNutrition=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.ChildGotNutrition=1";
					Db.NonQ(command);
					//find any BMIs within the period that indicate ChildGotPhysCouns
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.ChildGotPhysCouns=1 "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd)+" "
						+"AND vitalsign.ChildGotPhysCouns=1";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region ImmunizeChild
				case QualityType.ImmunizeChild_1:
				case QualityType.ImmunizeChild_2:
				case QualityType.ImmunizeChild_3:
				case QualityType.ImmunizeChild_4:
				case QualityType.ImmunizeChild_5:
				case QualityType.ImmunizeChild_6:
				case QualityType.ImmunizeChild_7:
				case QualityType.ImmunizeChild_8:
				case QualityType.ImmunizeChild_9:
				case QualityType.ImmunizeChild_10:
				case QualityType.ImmunizeChild_11:
				case QualityType.ImmunizeChild_12:
					//ImmunizeChild----------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						Birthdate date NOT NULL,
						Count1 tinyint NOT NULL,
						NotGiven1 tinyint NOT NULL,
						Documentation1 varchar(255) NOT NULL,
						Count2 tinyint NOT NULL,
						NotGiven2 tinyint NOT NULL,
						Documentation2 varchar(255) NOT NULL,
						Count3 tinyint NOT NULL,
						NotGiven3 tinyint NOT NULL,
						Documentation3 varchar(255) NOT NULL,
						Count3a tinyint NOT NULL,
						NotGiven3a tinyint NOT NULL,
						Documentation3a varchar(255) NOT NULL,
						Count3b tinyint NOT NULL,
						NotGiven3b tinyint NOT NULL,
						Documentation3b varchar(255) NOT NULL,
						Count3c tinyint NOT NULL,
						NotGiven3c tinyint NOT NULL,
						Documentation3c varchar(255) NOT NULL,
						Count4 tinyint NOT NULL,
						NotGiven4 tinyint NOT NULL,
						Documentation4 varchar(255) NOT NULL,
						Count5 tinyint NOT NULL,
						NotGiven5 tinyint NOT NULL,
						Documentation5 varchar(255) NOT NULL,
						Count6 tinyint NOT NULL,
						NotGiven6 tinyint NOT NULL,
						Documentation6 varchar(255) NOT NULL,
						Count7 tinyint NOT NULL,
						NotGiven7 tinyint NOT NULL,
						Documentation7 varchar(255) NOT NULL,
						Count8 tinyint NOT NULL,
						NotGiven8 tinyint NOT NULL,
						Documentation8 varchar(255) NOT NULL,
						Count9 tinyint NOT NULL,
						NotGiven9 tinyint NOT NULL,
						Documentation9 varchar(255) NOT NULL,
						Count10 tinyint NOT NULL,
						NotGiven10 tinyint NOT NULL,
						Documentation10 varchar(255) NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName,Birthdate) SELECT patient.PatNum,LName,FName,Birthdate "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE DATE_ADD(Birthdate,INTERVAL 2 YEAR) >= "+POut.Date(dateStart)+" "//second birthdate is in meas period
						+"AND DATE_ADD(Birthdate,INTERVAL 2 YEAR) <= "+POut.Date(dateEnd)+" "
						+"GROUP BY patient.PatNum";
					Db.NonQ(command);
					#region DTaP
					//Count1, DTaP
					command="UPDATE tempehrquality "
						+"SET Count1=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) >= DATE_ADD(tempehrquality.Birthdate,INTERVAL 42 DAY) "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('110','120','20','50'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven1=1,Documentation1=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('110','120','20','50')";
					Db.NonQ(command);
					#endregion
					#region IPV
					//Count2, IPV
					command="UPDATE tempehrquality "
						+"SET Count2=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) >= DATE_ADD(tempehrquality.Birthdate,INTERVAL 42 DAY) "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('10','120'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven2=1,Documentation2=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('10','120')";
					Db.NonQ(command);
					#endregion
					#region MMR
					//Count3, MMR
					command="UPDATE tempehrquality "
						+"SET Count3=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('03','94'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven3=1,Documentation3=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('03','94')";
					Db.NonQ(command);
					#endregion
					#region measles
					//Count3a, measles
					command="UPDATE tempehrquality "
						+"SET Count3a=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('05'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven3a=1,Documentation3a=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('05')";
					Db.NonQ(command);
					#endregion
					#region mumps
					//Count3b, mumps
					command="UPDATE tempehrquality "
						+"SET Count3b=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('07'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven3b=1,Documentation3b=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('07')";
					Db.NonQ(command);
					#endregion
					#region rubella
					//Count3c, rubella
					command="UPDATE tempehrquality "
						+"SET Count3c=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('06'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven3c=1,Documentation3c=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('06')";
					Db.NonQ(command);
					#endregion
					#region HiB
					//Count4, HiB
					command="UPDATE tempehrquality "
						+"SET Count4=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) >= DATE_ADD(tempehrquality.Birthdate,INTERVAL 42 DAY) "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('120','46','47','48','49','50','51'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven4=1,Documentation4=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('120','46','47','48','49','50','51')";
					Db.NonQ(command);
					#endregion
					#region HepB
					//Count5, HepB
					command="UPDATE tempehrquality "
						+"SET Count5=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('08','110','44','51'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven5=1,Documentation5=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('08','110','44','51')";
					Db.NonQ(command);
					#endregion
					#region VZV
					//Count6, VZV
					command="UPDATE tempehrquality "
						+"SET Count6=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('21','94'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven6=1,Documentation6=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('21','94')";
					Db.NonQ(command);
					#endregion
					#region pneumococcal
					//Count7, pneumococcal
					command="UPDATE tempehrquality "
						+"SET Count7=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) >= DATE_ADD(tempehrquality.Birthdate,INTERVAL 42 DAY) "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('100','133'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven7=1,Documentation7=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('100','133')";
					Db.NonQ(command);
					#endregion
					#region HepA
					//Count8, HepA
					command="UPDATE tempehrquality "
						+"SET Count8=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('83'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven8=1,Documentation8=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('83')";
					Db.NonQ(command);
					#endregion
					#region rotavirus
					//Count9, rotavirus
					command="UPDATE tempehrquality "
						+"SET Count9=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) >= DATE_ADD(tempehrquality.Birthdate,INTERVAL 42 DAY) "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('116','119'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven9=1,Documentation9=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('116','119')";
					Db.NonQ(command);
					#endregion
					#region influenza
					//Count10, influenza
					command="UPDATE tempehrquality "
						+"SET Count10=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=0 "
						+"AND DATE(DateTimeStart) >= DATE_ADD(tempehrquality.Birthdate,INTERVAL 180 DAY) "
						+"AND DATE(DateTimeStart) < DATE_ADD(tempehrquality.Birthdate,INTERVAL 2 YEAR) "
						+"AND vaccinedef.CVXCode IN('135','15'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality,vaccinepat,vaccinedef "
						+"SET NotGiven10=1,Documentation10=Note "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND NotGiven=1 "
						+"AND vaccinedef.CVXCode IN('135','15')";
					Db.NonQ(command);
					#endregion
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region Pneumonia
				case QualityType.Pneumonia:
					//Pneumonia----------------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						DateVaccine date NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName) SELECT patient.PatNum,LName,FName "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateEnd.AddYears(-1))+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate > '1880-01-01' AND Birthdate <= "+POut.Date(dateStart.AddYears(-65))+" "//65 or older as of dateEnd
						+"GROUP BY patient.PatNum";
					Db.NonQ(command);
					//find most recent vaccine date
					command="UPDATE tempehrquality "
						+"SET tempehrquality.DateVaccine=(SELECT MAX(DATE(vaccinepat.DateTimeStart)) "
						+"FROM vaccinepat,vaccinedef "
						+"WHERE vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"AND tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND vaccinepat.NotGiven=0 "
						+"AND vaccinedef.CVXCode IN('33','100','133'))";
					Db.NonQ(command);
					command="UPDATE tempehrquality SET DateVaccine='0001-01-01' WHERE DateVaccine='0000-00-00'";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region DiabetesBloodPressure
				case QualityType.DiabetesBloodPressure:
					//DiabetesBloodPressure-------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						HasMedication tinyint NOT NULL,
						HasDiagnosisDiabetes tinyint NOT NULL,
						DateBP date NOT NULL,
						Systolic int NOT NULL,
						Diastolic int NOT NULL,
						HasDiagnosisPolycystic tinyint NOT NULL,
						HasDiagnosisAcuteDiabetes tinyint NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName) SELECT patient.PatNum,LName,FName "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate <= "+POut.Date(dateStart.AddYears(-17))+" "
						+"AND Birthdate >= "+POut.Date(dateStart.AddYears(-74))+" "//17-74 before dateStart
						+"GROUP BY patient.PatNum";
					Db.NonQ(command);
					//Medication
					command="UPDATE tempehrquality "
						+"SET HasMedication = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM medicationpat "
						+"WHERE medicationpat.PatNum=tempehrquality.PatNum "
						+"AND medicationpat.RxCui IN(199149, 199150, 200132, 205329, 205330, 205331, 401938,"//alph-glucosidas
						+"200256, 200257, 200258, 311919, 314142, 389139, 861035, 861039, 861042, 861044, 861787, 861790,"//amylin analogs
						+"744863 , 847910 , 847915,"//antidiabetic
						+"602544, 602549, 602550, 647237, 647239, 706895, 706896, 861731, 861736, 861740, 861743, 861748, 861753, 861760, 861763, 861769, 861783, 861787, 861790, 861795, 861806, 861816, 861819, 861822,"//antidiabetic combos
						+"665033, 665038, 665042, 860975, 860978, 860981, 860984, 860996, 860999, 861004, 861007, 861010, 861021, 861025, 861731, 861736, 861740, 861743, 861748, 861753, 861760, 861763, 861769, 861783, 861787, 861790, 861795, 861806, 861816, 861819, 861822,"//Biguanides
						+"205314, 237527, 242120, 242916, 242917, 259111, 260265, 283394, 311040, 311041, 311053, 311054, 311055, 311056, 311057, 311058, 311059, 311060, 311061, 314038, 317800, 351297, 358349, 484322, 485210, 544614, 763002, 763007, 763013, 763014, 833159, 847191, 847207, 847211, 847230, 847239, 847252, 847259, 847263, 847416,"//insulin
						+"200256, 200257, 200258, 311919, 314142, 389139,"//meglitinides
						+"105374, 153842, 197306, 197307, 197495, 197496, 197737, 198291, 198292, 198293, 198294, 199245, 199246, 199247, 199825, 199984, 199985, 200065, 252960, 310488, 310489, 310490, 310534, 310536, 310537, 310539, 312440, 312441, 312859, 312860, 312861, 313418, 313419, 314000, 314006, 315107, 315239, 317573, 379804, 389137, 602544, 602549, 602550, 647237, 647239, 706895, 706896, 757710, 757712, 844809, 844824, 844827, 861731, 861736, 861740, 861743, 861748, 861753, 861760, 861763, 861783, 861795, 861806, 861816, 861822,"//Sulfonylureas
						+"312440, 312441, 312859, 312860, 312861, 317573) "//Thiazolidinediones
						+"AND (DateStop >= "+POut.Date(dateEnd.AddYears(-2))+" "//med active <= 2 years before or simultaneous to end date
						+"OR DateStop < '1880-01-01') "//or still active
						+") > 0";
					Db.NonQ(command);
					//HasDiagnosisDiabetes
					command="UPDATE tempehrquality "
						+"SET HasDiagnosisDiabetes = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM disease,diseasedef "
						+"WHERE disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND (diseasedef.ICD9Code LIKE '250%' "
						+"OR diseasedef.ICD9Code LIKE '357.2' "
						+"OR diseasedef.ICD9Code LIKE '362.0%' "
						+"OR diseasedef.ICD9Code LIKE '366.41' "
						+"OR diseasedef.ICD9Code LIKE '648.0%') "
						+"AND (disease.DateStart <= "+POut.Date(dateEnd)+" "//if there is a start date, it can't be after the period end.
						+"OR disease.DateStart < '1880-01-01') "//no startdate
						+"AND (disease.DateStop >= "+POut.Date(dateEnd.AddYears(-2))+" "//if there's a datestop, it can't have stopped more than 2 years ago.
							//Specs say: diagnosis active <= 2 years before or simultaneous to end date
						+"OR disease.DateStop < '1880-01-01') "//or still active
						+") > 0";
					Db.NonQ(command);
					//DateBP
					command="UPDATE tempehrquality "
						+"SET tempehrquality.DateBP=(SELECT MAX(vitalsign.DateTaken) "
						+"FROM vitalsign "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.BpSystolic != 0 "
						+"AND vitalsign.BpDiastolic != 0 "
						+"GROUP BY vitalsign.PatNum)";
					Db.NonQ(command);
					command="UPDATE tempehrquality SET DateBP='0001-01-01' WHERE DateBP='0000-00-00'";
					Db.NonQ(command);
					//Systolic and diastolic
					command="UPDATE tempehrquality,vitalsign "
						+"SET Systolic=BpSystolic, "
						+"Diastolic=BpDiastolic "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND tempehrquality.DateBP=vitalsign.DateTaken";
					Db.NonQ(command);
					//HasDiagnosisPolycystic
					command="UPDATE tempehrquality "
						+"SET HasDiagnosisPolycystic = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM disease,diseasedef "
						+"WHERE disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND diseasedef.ICD9Code = '256.4' "
						+"AND (disease.DateStart <= "+POut.Date(dateEnd)+" "//if there's a datestart, it can't be after period end
						+"OR disease.DateStart < '1880-01-01') "
						//no restrictions on datestop.  It could still be active or could have stopped before or after the period end.
						+") > 0";
					Db.NonQ(command);
					//HasDiagnosisAcuteDiabetes
					command="UPDATE tempehrquality "
						+"SET HasDiagnosisAcuteDiabetes = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM disease,diseasedef "
						+"WHERE disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND (diseasedef.ICD9Code LIKE '249%' OR diseasedef.ICD9Code='251.8' OR diseasedef.ICD9Code='962.0' "//steroid induced
						+"OR diseasedef.ICD9Code LIKE '648.8%') "//gestational
						+"AND (disease.DateStart <= "+POut.Date(dateEnd)+" "//if there's a datestart, it can't be after period end
						+"OR disease.DateStart < '1880-01-01') "
						//no restrictions on datestop.  It could still be active or could have stopped before or after the period end.
						+") > 0";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				#region BloodPressureManage
				case QualityType.BloodPressureManage:
					//DiabetesBloodPressure-------------------------------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrquality (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						HasDiagnosisHypertension tinyint NOT NULL,
						HasProcedureESRD tinyint NOT NULL,
						HasDiagnosisPregnancy tinyint NOT NULL,
						HasDiagnosisESRD tinyint NOT NULL,
						DateBP date NOT NULL,
						Systolic int NOT NULL,
						Diastolic int NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName) SELECT patient.PatNum,LName,FName "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate <= "+POut.Date(dateStart.AddYears(-17))+" "
						+"AND Birthdate >= "+POut.Date(dateStart.AddYears(-74))+" "//17-74 before dateStart
						+"GROUP BY patient.PatNum";
					Db.NonQ(command);
					//HasDiagnosisHypertension
					command="UPDATE tempehrquality "
						+"SET HasDiagnosisHypertension = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM disease,diseasedef "
						+"WHERE disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND diseasedef.ICD9Code LIKE '401%' "
						+"AND (disease.DateStart <= "+POut.Date(dateStart.AddMonths(6))+" "//if there is a start date, it can't be after this point
						+"OR disease.DateStart < '1880-01-01') "//no startdate
						//no restrictions on datestop.  It could still be active or could have stopped before or after the period end.
						+") > 0";
					Db.NonQ(command);
					command="DELETE FROM tempehrquality WHERE HasDiagnosisHypertension=0";//for speed
					Db.NonQ(command);
					//HasProcedureESRD
					command="UPDATE tempehrquality "
						+"SET HasProcedureESRD = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM procedurelog,procedurecode "
						+"WHERE procedurelog.CodeNum=procedurecode.CodeNum "
						+"AND procedurelog.PatNum=tempehrquality.PatNum "
						+"AND procedurecode.ProcCode IN ('36145','36147','36148','36800', '36810','36815','36818','36819','36820', '36821','36831', '36832', '36833', '50300', '50320','50340','50360','50365','50370', '50380','90920','90921','90924','90925', '90935','90937', '90940','90945', '90947', '90957', '90958','90959','90960','90961','90962','90965','90966','90969','90970','90989','90993','90997','90999','99512') "//ESRD
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+") > 0";
					Db.NonQ(command);
					//HasDiagnosisPregnancy
					command="UPDATE tempehrquality "
						+"SET HasDiagnosisPregnancy = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM disease,diseasedef "
						+"WHERE disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND (diseasedef.ICD9Code LIKE '63%' "
						+"OR diseasedef.ICD9Code LIKE '64%' "
						+"OR diseasedef.ICD9Code LIKE '65%' "
						+"OR diseasedef.ICD9Code LIKE '66%' "
						+"OR diseasedef.ICD9Code LIKE '67%' "
						+"OR diseasedef.ICD9Code LIKE 'V22%' "
						+"OR diseasedef.ICD9Code LIKE 'V23%' "
						+"OR diseasedef.ICD9Code LIKE 'V28%') "
						//active during the period
						+"AND (disease.DateStart <= "+POut.Date(dateEnd)+" "//if there is a start date, it can't be after the period end.
						+"OR disease.DateStart < '1880-01-01') "//no startdate
						+"AND (disease.DateStop >= "+POut.Date(dateStart)+" "//if there's a datestop, it can't have stopped before the period.
						+"OR disease.DateStop < '1880-01-01') "//or still active
						+") > 0";
					Db.NonQ(command);
					//HasDiagnosisESRD
					command="UPDATE tempehrquality "
						+"SET HasDiagnosisESRD = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM disease,diseasedef "
						+"WHERE disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND (diseasedef.ICD9Code LIKE '38.95' "
						+"OR diseasedef.ICD9Code LIKE '39%' "
						+"OR diseasedef.ICD9Code LIKE '54.98' "
						+"OR diseasedef.ICD9Code LIKE '55.6%' "
						+"OR diseasedef.ICD9Code LIKE '585%' "
						+"OR diseasedef.ICD9Code LIKE 'V42.0%' "
						+"OR diseasedef.ICD9Code LIKE 'V45.1%' "
						+"OR diseasedef.ICD9Code LIKE 'V56%') "
						//active during the period
						+"AND (disease.DateStart <= "+POut.Date(dateEnd)+" "
						+"OR disease.DateStart < '1880-01-01') "
						+"AND (disease.DateStop >= "+POut.Date(dateStart)+" "
						+"OR disease.DateStop < '1880-01-01') "
						+") > 0";
					Db.NonQ(command);
					//DateBP
					command="UPDATE tempehrquality "
						+"SET tempehrquality.DateBP=(SELECT MAX(vitalsign.DateTaken) "
						+"FROM vitalsign "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.BpSystolic != 0 "
						+"AND vitalsign.BpDiastolic != 0 "
						+"GROUP BY vitalsign.PatNum)";
					Db.NonQ(command);
					command="UPDATE tempehrquality SET DateBP='0001-01-01' WHERE DateBP='0000-00-00'";
					Db.NonQ(command);
					//Systolic and diastolic
					command="UPDATE tempehrquality,vitalsign "
						+"SET Systolic=BpSystolic, "
						+"Diastolic=BpDiastolic "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND tempehrquality.DateBP=vitalsign.DateTaken";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
				#endregion
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
			//PatNum, PatientName, Numerator(X), and Exclusion(X).
			DataTable table=new DataTable("audit");
			DataRow row;
			table.Columns.Add("PatNum");
			table.Columns.Add("patientName");
			table.Columns.Add("numerator");//X
			table.Columns.Add("exclusion");//X
			table.Columns.Add("explanation");
			List<DataRow> rows=new List<DataRow>();
			Patient pat;
			//string explanation;
			for(int i=0;i<tableRaw.Rows.Count;i++) {
				row=table.NewRow();
				row["PatNum"]=tableRaw.Rows[i]["PatNum"].ToString();
				pat=new Patient();
				pat.LName=tableRaw.Rows[i]["LName"].ToString();
				pat.FName=tableRaw.Rows[i]["FName"].ToString();
				pat.Preferred="";
				row["patientName"]=pat.GetNameLF();
				row["numerator"]="";
				row["exclusion"]="";
				row["explanation"]="";
				float weight=0;
				float height=0;
				float bmi=0;
				DateTime dateVisit;
				int visitCount;
				switch(qtype) {
					#region WeightOver65
					case QualityType.WeightOver65:
						//WeightOver65-----------------------------------------------------------------------------------------------------------------
						weight=PIn.Float(tableRaw.Rows[i]["Weight"].ToString());
						height=PIn.Float(tableRaw.Rows[i]["Height"].ToString());
						bmi=Vitalsigns.CalcBMI(weight,height);
						bool hasFollowupPlan=PIn.Bool(tableRaw.Rows[i]["HasFollowupPlan"].ToString());
						bool isIneligible=PIn.Bool(tableRaw.Rows[i]["IsIneligible"].ToString());
						string documentation=tableRaw.Rows[i]["Documentation"].ToString();
						if(bmi==0){
							row["explanation"]="No BMI";
						}
						else if(bmi < 22) {
							row["explanation"]="Underweight";
							if(hasFollowupPlan) {
								row["explanation"]+=", has followup plan: "+documentation;
								row["numerator"]="X";
							}
						}
						else if(bmi < 30) {
							row["numerator"]="X";
							row["explanation"]="Normal weight";
						}
						else {
							row["explanation"]="Overweight";
							if(hasFollowupPlan) {
								row["explanation"]+=", has followup plan: "+documentation;
								row["numerator"]="X";
							}
						}
						if(isIneligible) {
							row["exclusion"]="X";
							row["explanation"]+=", "+documentation;
						}
						break;
					#endregion
					#region WeightAdult
					case QualityType.WeightAdult:
						//WeightAdult-----------------------------------------------------------------------------------------------------------------
						weight=PIn.Float(tableRaw.Rows[i]["Weight"].ToString());
						height=PIn.Float(tableRaw.Rows[i]["Height"].ToString());
						bmi=Vitalsigns.CalcBMI(weight,height);
						hasFollowupPlan=PIn.Bool(tableRaw.Rows[i]["HasFollowupPlan"].ToString());
						isIneligible=PIn.Bool(tableRaw.Rows[i]["IsIneligible"].ToString());
						documentation=tableRaw.Rows[i]["Documentation"].ToString();
						if(bmi==0){
							row["explanation"]="No BMI";
						}
						else if(bmi < 18.5f) {
							row["explanation"]="Underweight";
							if(hasFollowupPlan) {
								row["explanation"]+=", has followup plan: "+documentation;
								row["numerator"]="X";
							}
						}
						else if(bmi < 25) {
							row["numerator"]="X";
							row["explanation"]="Normal weight";
						}
						else {
							row["explanation"]="Overweight";
							if(hasFollowupPlan) {
								row["explanation"]+=", has followup plan: "+documentation;
								row["numerator"]="X";
							}
						}
						if(isIneligible) {
							row["exclusion"]="X";
							row["explanation"]+=", "+documentation;
						}
						break;
					#endregion
					#region Hypertension
					case QualityType.Hypertension:
						//Hypertension---------------------------------------------------------------------------------------------------------------------
						dateVisit=PIn.Date(tableRaw.Rows[i]["DateVisit"].ToString());
						visitCount=PIn.Int(tableRaw.Rows[i]["VisitCount"].ToString());
						string icd9code=tableRaw.Rows[i]["Icd9Code"].ToString();
						DateTime datePbEntered=PIn.Date(tableRaw.Rows[i]["DateBpEntered"].ToString());
						if(dateVisit<dateStart || dateVisit>dateEnd) {//no visits in the measurement period
							continue;//don't add this row.  Not part of denominator.
						}
						if(visitCount<2) {
							continue;
						}
						if(icd9code=="") {
							continue;
						}
						if(datePbEntered.Year<1880) {//no bp entered
							row["explanation"]="No BP entered";
						}
						else {
							row["numerator"]="X";
							row["explanation"]="BP entered";
						}
						break;
					#endregion
					#region TobaccoUse
					case QualityType.TobaccoUse:
						//TobaccoUse---------------------------------------------------------------------------------------------------------------------
						dateVisit=PIn.Date(tableRaw.Rows[i]["DateVisit"].ToString());
						//visitCount=PIn.Int(tableRaw.Rows[i]["VisitCount"].ToString());
						DateTime dateAssessment=PIn.Date(tableRaw.Rows[i]["DateAssessment"].ToString());
						if(dateVisit<dateStart || dateVisit>dateEnd) {//no visits in the measurement period
							continue;//don't add this row.  Not part of denominator.
						}
						//if(visitCount<2) {//no, as explained in comments in GetDenominatorExplain().
						//	continue;
						//}
						if(dateAssessment.Year<1880) {
							row["explanation"]="No tobacco use entered.";
						}
						else if(dateAssessment < dateVisit.AddYears(-2)) {
							row["explanation"]="No tobacco use entered within timeframe.";
						}
						else{
							row["numerator"]="X";
							row["explanation"]="Tobacco use entered.";
						}
						break;
					#endregion
					#region TobaccoCessation
					case QualityType.TobaccoCessation:
						//TobaccoCessation----------------------------------------------------------------------------------------------------------------
						dateVisit=PIn.Date(tableRaw.Rows[i]["DateVisit"].ToString());
						dateAssessment=PIn.Date(tableRaw.Rows[i]["DateAssessment"].ToString());
						DateTime DateCessation=PIn.Date(tableRaw.Rows[i]["DateCessation"].ToString());
						documentation=tableRaw.Rows[i]["Documentation"].ToString();
						if(dateVisit<dateStart || dateVisit>dateEnd) {//no visits in the measurement period
							continue;//don't add this row.  Not part of denominator.
						}
						else if(dateAssessment < dateVisit.AddYears(-2)) {
							continue;//no assessment within 24 months, so not part of denominator.
						}
						else if(DateCessation.Year<1880) {
							row["explanation"]="No tobacco cessation entered.";
						}
						else if(DateCessation < dateVisit.AddYears(-2)) {
							row["explanation"]="No tobacco cessation entered within timeframe.";
						}
						else {
							row["numerator"]="X";
							row["explanation"]="Tobacco cessation: "+documentation;
						}
						break;
					#endregion
					#region InfluenzaAdult
					case QualityType.InfluenzaAdult:
						//InfluenzaAdult----------------------------------------------------------------------------------------------------------------
						DateTime DateVaccine=PIn.Date(tableRaw.Rows[i]["DateVaccine"].ToString());
						bool notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven"].ToString());
						documentation=tableRaw.Rows[i]["Documentation"].ToString();
						if(DateVaccine.Year<1880) {
							row["explanation"]="No influenza vaccine given";
						}
						else if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No influenza vaccine given, "+documentation;
						}
						else {
							row["numerator"]="X";
							row["explanation"]="Influenza vaccine given";
						}
						break;
					#endregion
					#region WeightChild_1_1
					case QualityType.WeightChild_1_1:
						//WeightChild_1_1----------------------------------------------------------------------------------------------------------------
						bool isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						bool hasBMI=PIn.Bool(tableRaw.Rows[i]["HasBMI"].ToString());
						if(isPregnant) {
							continue;
						}
						if(hasBMI) {
							row["numerator"]="X";
							row["explanation"]="BMI entered";
						}
						else {
							row["explanation"]="No BMI entered";
						}
						break;
					#endregion
					#region WeightChild_1_2
					case QualityType.WeightChild_1_2:
						//WeightChild_1_2----------------------------------------------------------------------------------------------------------------
						isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						bool ChildGotNutrition=PIn.Bool(tableRaw.Rows[i]["ChildGotNutrition"].ToString());
						if(isPregnant) {
							continue;
						}
						if(ChildGotNutrition) {
							row["numerator"]="X";
							row["explanation"]="Counseled for nutrition";
						}
						else {
							row["explanation"]="Not counseled for nutrition";
						}
						break;
					#endregion
					#region WeightChild_1_3
					case QualityType.WeightChild_1_3:
						//WeightChild_1_3----------------------------------------------------------------------------------------------------------------
						isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						bool ChildGotPhysCouns=PIn.Bool(tableRaw.Rows[i]["ChildGotPhysCouns"].ToString());
						if(isPregnant) {
							continue;
						}
						if(ChildGotPhysCouns) {
							row["numerator"]="X";
							row["explanation"]="Counseled for physical activity";
						}
						else {
							row["explanation"]="Not counseled for physical activity";
						}
						break;
					#endregion
					#region WeightChild_2_1
					case QualityType.WeightChild_2_1:
						//WeightChild_2_1----------------------------------------------------------------------------------------------------------------
						isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						hasBMI=PIn.Bool(tableRaw.Rows[i]["HasBMI"].ToString());
						if(isPregnant) {
							continue;
						}
						if(hasBMI) {
							row["numerator"]="X";
							row["explanation"]="BMI entered";
						}
						else {
							row["explanation"]="No BMI entered";
						}
						break;
					#endregion
					#region WeightChild_2_2
					case QualityType.WeightChild_2_2:
						//WeightChild_2_2----------------------------------------------------------------------------------------------------------------
						isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						ChildGotNutrition=PIn.Bool(tableRaw.Rows[i]["ChildGotNutrition"].ToString());
						if(isPregnant) {
							continue;
						}
						if(ChildGotNutrition) {
							row["numerator"]="X";
							row["explanation"]="Counseled for nutrition";
						}
						else {
							row["explanation"]="Not counseled for nutrition";
						}
						break;
					#endregion
					#region WeightChild_2_3
					case QualityType.WeightChild_2_3:
						//WeightChild_2_3----------------------------------------------------------------------------------------------------------------
						isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						ChildGotPhysCouns=PIn.Bool(tableRaw.Rows[i]["ChildGotPhysCouns"].ToString());
						if(isPregnant) {
							continue;
						}
						if(ChildGotPhysCouns) {
							row["numerator"]="X";
							row["explanation"]="Counseled for physical activity";
						}
						else {
							row["explanation"]="Not counseled for physical activity";
						}
						break;
					#endregion
					#region WeightChild_3_1
					case QualityType.WeightChild_3_1:
						//WeightChild_3_1----------------------------------------------------------------------------------------------------------------
						isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						hasBMI=PIn.Bool(tableRaw.Rows[i]["HasBMI"].ToString());
						if(isPregnant) {
							continue;
						}
						if(hasBMI) {
							row["numerator"]="X";
							row["explanation"]="BMI entered";
						}
						else {
							row["explanation"]="No BMI entered";
						}
						break;
					#endregion
					#region WeightChild_3_2
					case QualityType.WeightChild_3_2:
						//WeightChild_3_2----------------------------------------------------------------------------------------------------------------
						isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						ChildGotNutrition=PIn.Bool(tableRaw.Rows[i]["ChildGotNutrition"].ToString());
						if(isPregnant) {
							continue;
						}
						if(ChildGotNutrition) {
							row["numerator"]="X";
							row["explanation"]="Counseled for nutrition";
						}
						else {
							row["explanation"]="Not counseled for nutrition";
						}
						break;
					#endregion
					#region WeightChild_3_3
					case QualityType.WeightChild_3_3:
						//WeightChild_3_3----------------------------------------------------------------------------------------------------------------
						isPregnant=PIn.Bool(tableRaw.Rows[i]["IsPregnant"].ToString());
						ChildGotPhysCouns=PIn.Bool(tableRaw.Rows[i]["ChildGotPhysCouns"].ToString());
						if(isPregnant) {
							continue;
						}
						if(ChildGotPhysCouns) {
							row["numerator"]="X";
							row["explanation"]="Counseled for physical activity";
						}
						else {
							row["explanation"]="Not counseled for physical activity";
						}
						break;
					#endregion
					#region ImmunizeChild_1
					case QualityType.ImmunizeChild_1:
						//ImmunizeChild_1--------------------------------------------------------------------------------------------------------------
						int count=PIn.Int(tableRaw.Rows[i]["Count1"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven1"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation1"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No DTaP vaccine given, "+documentation;
						}
						else if(count>=4) {
							row["numerator"]="X";
							row["explanation"]="DTaP vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="DTaP vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_2
					case QualityType.ImmunizeChild_2:
						//ImmunizeChild_2--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count2"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven2"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation2"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No IPV vaccine given, "+documentation;
						}
						else if(count>=3) {
							row["numerator"]="X";
							row["explanation"]="IPV vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="IPV vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_3
					case QualityType.ImmunizeChild_3:
						//ImmunizeChild_3--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count3"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven3"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation3"].ToString());
						int count3a=PIn.Int(tableRaw.Rows[i]["Count3a"].ToString());
						bool notGiven3a=PIn.Bool(tableRaw.Rows[i]["NotGiven3a"].ToString());
						string documentation3a=PIn.String(tableRaw.Rows[i]["Documentation3a"].ToString());
						int count3b=PIn.Int(tableRaw.Rows[i]["Count3b"].ToString());
						bool notGiven3b=PIn.Bool(tableRaw.Rows[i]["NotGiven3b"].ToString());
						string documentation3b=PIn.String(tableRaw.Rows[i]["Documentation3b"].ToString());
						int count3c=PIn.Int(tableRaw.Rows[i]["Count3c"].ToString());
						bool notGiven3c=PIn.Bool(tableRaw.Rows[i]["NotGiven3c"].ToString());
						string documentation3c=PIn.String(tableRaw.Rows[i]["Documentation3c"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No MMR vaccine given, "+documentation;
						}
						else if(notGiven3a) {
							row["exclusion"]="X";
							row["explanation"]+="No measles vaccine given, "+documentation3a;
						}
						else if(notGiven3b) {
							row["exclusion"]="X";
							row["explanation"]+="No mumps vaccine given, "+documentation3b;
						}
						else if(notGiven3c) {
							row["exclusion"]="X";
							row["explanation"]+="No rubella vaccine given, "+documentation3c;
						}
						else if(count>=1) {
							row["numerator"]="X";
							row["explanation"]="MMR vaccinations: "+count.ToString();
						}
						else if(count3a>=1 && count3b>=1 && count3c>=1) {
							row["numerator"]="X";
							row["explanation"]="MMR individual vaccinations given.";
						}
						else {
							row["explanation"]="MMR vaccination not given";
						}
						break;
					#endregion
					#region ImmunizeChild_4
					case QualityType.ImmunizeChild_4:
						//ImmunizeChild_4--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count4"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven4"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation4"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No HiB vaccine given, "+documentation;
						}
						else if(count>=2) {
							row["numerator"]="X";
							row["explanation"]="HiB vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="HiB vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_5
					case QualityType.ImmunizeChild_5:
						//ImmunizeChild_5--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count5"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven5"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation5"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No hepatitis B vaccine given, "+documentation;
						}
						else if(count>=3) {
							row["numerator"]="X";
							row["explanation"]="hepatitis B vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="hepatitis B vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_6
					case QualityType.ImmunizeChild_6:
						//ImmunizeChild_6--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count6"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven6"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation6"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No VZV vaccine given, "+documentation;
						}
						else if(count>=1) {
							row["numerator"]="X";
							row["explanation"]="VZV vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="VZV vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_7
					case QualityType.ImmunizeChild_7:
						//ImmunizeChild_7--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count7"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven7"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation7"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No pneumococcal vaccine given, "+documentation;
						}
						else if(count>=4) {
							row["numerator"]="X";
							row["explanation"]="pneumococcal vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="pneumococcal vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_8
					case QualityType.ImmunizeChild_8:
						//ImmunizeChild_8--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count8"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven8"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation8"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No hepatitis A vaccine given, "+documentation;
						}
						else if(count>=2) {
							row["numerator"]="X";
							row["explanation"]="hepatitis A vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="hepatitis A vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_9
					case QualityType.ImmunizeChild_9:
						//ImmunizeChild_9--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count9"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven9"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation9"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No rotavirus vaccine given, "+documentation;
						}
						else if(count>=2) {
							row["numerator"]="X";
							row["explanation"]="rotavirus vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="rotavirus vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_10
					case QualityType.ImmunizeChild_10:
						//ImmunizeChild_10--------------------------------------------------------------------------------------------------------------
						count=PIn.Int(tableRaw.Rows[i]["Count10"].ToString());
						notGiven=PIn.Bool(tableRaw.Rows[i]["NotGiven10"].ToString());
						documentation=PIn.String(tableRaw.Rows[i]["Documentation10"].ToString());
						if(notGiven) {
							row["exclusion"]="X";
							row["explanation"]+="No influenza vaccine given, "+documentation;
						}
						else if(count>=2) {
							row["numerator"]="X";
							row["explanation"]="influenza vaccinations: "+count.ToString();
						}
						else {
							row["explanation"]="influenza vaccinations: "+count.ToString();
						}
						break;
					#endregion
					#region ImmunizeChild_11
					case QualityType.ImmunizeChild_11:
						int count1=PIn.Int(tableRaw.Rows[i]["Count1"].ToString());
						int count2=PIn.Int(tableRaw.Rows[i]["Count2"].ToString());
						int count3=PIn.Int(tableRaw.Rows[i]["Count3"].ToString());
						count3a=PIn.Int(tableRaw.Rows[i]["Count3a"].ToString());
						count3b=PIn.Int(tableRaw.Rows[i]["Count3b"].ToString());
						count3c=PIn.Int(tableRaw.Rows[i]["Count3c"].ToString());
						int count4=PIn.Int(tableRaw.Rows[i]["Count4"].ToString());
						int count5=PIn.Int(tableRaw.Rows[i]["Count5"].ToString());
						int count6=PIn.Int(tableRaw.Rows[i]["Count6"].ToString());
						bool notGiven1=PIn.Bool(tableRaw.Rows[i]["NotGiven1"].ToString());
						bool notGiven2=PIn.Bool(tableRaw.Rows[i]["NotGiven2"].ToString());
						bool notGiven3=PIn.Bool(tableRaw.Rows[i]["NotGiven3"].ToString());
						notGiven3a=PIn.Bool(tableRaw.Rows[i]["NotGiven3a"].ToString());
						notGiven3b=PIn.Bool(tableRaw.Rows[i]["NotGiven3b"].ToString());
						notGiven3c=PIn.Bool(tableRaw.Rows[i]["NotGiven3c"].ToString());
						bool notGiven4=PIn.Bool(tableRaw.Rows[i]["NotGiven4"].ToString());
						bool notGiven5=PIn.Bool(tableRaw.Rows[i]["NotGiven5"].ToString());
						bool notGiven6=PIn.Bool(tableRaw.Rows[i]["NotGiven6"].ToString());
						if(notGiven1 || notGiven2 || notGiven3 || notGiven3a || notGiven3b || notGiven3c || notGiven4 || notGiven5 || notGiven6) {
							row["exclusion"]="X";
							row["explanation"]+="Not given.";//too complicated to document.
						}
						else if(count1>=4 && count2>=3 
							&& (count3>=1 || (count3a>=1 && count3b>=1 && count3c>=1)) && count4>=2 && count5>=3 && count6>=1) {
							row["numerator"]="X";
							row["explanation"]="All vaccinations given.";
						}
						else {
							row["explanation"]="Missing vaccinations.";
						}
						break;
					#endregion
					#region ImmunizeChild_12
					case QualityType.ImmunizeChild_12:
						//ImmunizeChild_12--------------------------------------------------------------------------------------------------------------
						count1=PIn.Int(tableRaw.Rows[i]["Count1"].ToString());
						count2=PIn.Int(tableRaw.Rows[i]["Count2"].ToString());
						count3=PIn.Int(tableRaw.Rows[i]["Count3"].ToString());
						count3a=PIn.Int(tableRaw.Rows[i]["Count3a"].ToString());
						count3b=PIn.Int(tableRaw.Rows[i]["Count3b"].ToString());
						count3c=PIn.Int(tableRaw.Rows[i]["Count3c"].ToString());
						count4=PIn.Int(tableRaw.Rows[i]["Count4"].ToString());
						count5=PIn.Int(tableRaw.Rows[i]["Count5"].ToString());
						count6=PIn.Int(tableRaw.Rows[i]["Count6"].ToString());
						int count7=PIn.Int(tableRaw.Rows[i]["Count7"].ToString());
						notGiven1=PIn.Bool(tableRaw.Rows[i]["NotGiven1"].ToString());
						notGiven2=PIn.Bool(tableRaw.Rows[i]["NotGiven2"].ToString());
						notGiven3=PIn.Bool(tableRaw.Rows[i]["NotGiven3"].ToString());
						notGiven3a=PIn.Bool(tableRaw.Rows[i]["NotGiven3a"].ToString());
						notGiven3b=PIn.Bool(tableRaw.Rows[i]["NotGiven3b"].ToString());
						notGiven3c=PIn.Bool(tableRaw.Rows[i]["NotGiven3c"].ToString());
						notGiven4=PIn.Bool(tableRaw.Rows[i]["NotGiven4"].ToString());
						notGiven5=PIn.Bool(tableRaw.Rows[i]["NotGiven5"].ToString());
						notGiven6=PIn.Bool(tableRaw.Rows[i]["NotGiven6"].ToString());
						bool notGiven7=PIn.Bool(tableRaw.Rows[i]["NotGiven7"].ToString());
						if(notGiven1 || notGiven2 || notGiven3 || notGiven3a || notGiven3b || notGiven3c || notGiven4 || notGiven5 || notGiven6 || notGiven7) {
							row["exclusion"]="X";
							row["explanation"]+="Not given.";//too complicated to document.
						}
						else if(count1>=4 && count2>=3 
							&& (count3>=1 || (count3a>=1 && count3b>=1 && count3c>=1)) && count4>=2 && count5>=3 && count6>=1 && count7>=4) {
							row["numerator"]="X";
							row["explanation"]="All vaccinations given.";
						}
						else {
							row["explanation"]="Missing vaccinations.";
						}
						break;
					#endregion
					#region Pneumonia
					case QualityType.Pneumonia:
						//Pneumonia----------------------------------------------------------------------------------------------------------------
						DateVaccine=PIn.Date(tableRaw.Rows[i]["DateVaccine"].ToString());
						if(DateVaccine.Year<1880) {
							row["explanation"]="No pneumococcal vaccine given";
						}
						else {
							row["numerator"]="X";
							row["explanation"]="Pneumococcal vaccine given";
						}
						break;
					#endregion
					#region DiabetesBloodPressure
					case QualityType.DiabetesBloodPressure:
						//DiabetesBloodPressure---------------------------------------------------------------------------------------------------
						bool hasMedication=PIn.Bool(tableRaw.Rows[i]["HasMedication"].ToString());
						bool HasDiagnosisDiabetes=PIn.Bool(tableRaw.Rows[i]["HasDiagnosisDiabetes"].ToString());
						DateTime DateBP=PIn.Date(tableRaw.Rows[i]["DateBP"].ToString());
						int systolic=PIn.Int(tableRaw.Rows[i]["Systolic"].ToString());
						int diastolic=PIn.Int(tableRaw.Rows[i]["Diastolic"].ToString());
						bool HasDiagnosisPolycystic=PIn.Bool(tableRaw.Rows[i]["HasDiagnosisPolycystic"].ToString());
						bool HasDiagnosisAcuteDiabetes=PIn.Bool(tableRaw.Rows[i]["HasDiagnosisAcuteDiabetes"].ToString());
						if(!hasMedication && !HasDiagnosisDiabetes) {
							continue;//not part of denominator
						}
						if(HasDiagnosisPolycystic && !HasDiagnosisDiabetes) {
							row["exclusion"]="X";
							row["explanation"]+="polycystic ovaries";
						}
						else if(HasDiagnosisAcuteDiabetes && hasMedication && !HasDiagnosisDiabetes) {
							row["exclusion"]="X";
							row["explanation"]+="gestational or steroid induced diabetes";
						}
						else if(DateBP.Year<1880) {
							row["explanation"]="No BP entered";
						}
						else if(systolic < 90 && diastolic < 140) {
							row["numerator"]="X";
							row["explanation"]="Controlled blood pressure: "+systolic.ToString()+"/"+diastolic.ToString();
						}
						else {
							row["explanation"]="High blood pressure: "+systolic.ToString()+"/"+diastolic.ToString();
						}
						break;
					#endregion
					#region BloodPressureManage
					case QualityType.BloodPressureManage:
						//BloodPressureManage-------------------------------------------------------------------------------------------------------
						bool HasDiagnosisHypertension=PIn.Bool(tableRaw.Rows[i]["HasDiagnosisHypertension"].ToString());
						bool HasProcedureESRD=PIn.Bool(tableRaw.Rows[i]["HasProcedureESRD"].ToString());
						bool HasDiagnosisPregnancy=PIn.Bool(tableRaw.Rows[i]["HasDiagnosisPregnancy"].ToString());
						bool HasDiagnosisESRD=PIn.Bool(tableRaw.Rows[i]["HasDiagnosisESRD"].ToString());
						DateBP=PIn.Date(tableRaw.Rows[i]["DateBP"].ToString());
						systolic=PIn.Int(tableRaw.Rows[i]["Systolic"].ToString());
						diastolic=PIn.Int(tableRaw.Rows[i]["Diastolic"].ToString());
						if(!HasDiagnosisHypertension) {
							continue;//not part of denominator
						}
						if(HasProcedureESRD || HasDiagnosisPregnancy || HasDiagnosisESRD) {
							continue;//not part of denominator
						}
						if(DateBP.Year<1880) {
							row["explanation"]="No BP entered";
						}
						else if(systolic < 90 && diastolic < 140) {
							row["numerator"]="X";
							row["explanation"]="Controlled blood pressure: "+systolic.ToString()+"/"+diastolic.ToString();
						}
						else {
							row["explanation"]="High blood pressure: "+systolic.ToString()+"/"+diastolic.ToString();
						}
						break;
					#endregion
					default:
						throw new ApplicationException("Type not found: "+qtype.ToString());
				}
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}
		
		///<summary>Only called from GetAll2014.  Once the EhrCqmData object is created, all of the data relevant to the measure and required by the QRDA category 1 and category 3 reporting is part of the object.</summary>
		public static QualityMeasure GetEhrCqmData(QualityType2014 qtype,DateTime dateStart,DateTime dateEnd,long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<QualityMeasure>(MethodBase.GetCurrentMethod(),qtype,dateStart,dateEnd,provNum);
			}
			//these queries only work for mysql
			string command="SELECT GROUP_CONCAT(provider.ProvNum) FROM provider WHERE provider.EhrKey="
				+"(SELECT pv.EhrKey FROM provider pv WHERE pv.ProvNum="+POut.Long(provNum)+")";
			string provs=Db.GetScalar(command);
			QualityMeasure measureCur=new QualityMeasure();
			System.Diagnostics.Stopwatch s=new System.Diagnostics.Stopwatch();
			List<string> listOneOfEncOIDs=new List<string>();
			List<string> listTwoOfEncOIDs=new List<string>();
			List<string> listValueSetOIDs;
			List<string> listReasonOIDs;
			List<long> listEhrPatNums;
			//This adultEncQuery is used by several CQMs
			//All encounters in the date range by the provider (based on ehrkey, so may be list of providers) for patients over 18 at the start of the measurement period
			string encounterSelectWhere="SELECT encounter.* FROM encounter "
				+"INNER JOIN patient ON patient.PatNum=encounter.PatNum "
				+"WHERE YEAR(patient.Birthdate)>1880 "//valid birthdate
				+"AND encounter.ProvNum IN("+POut.String(provs)+") "
				+"AND DATE(encounter.DateEncounter) BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" ";
			string encounterWhereAdults="AND patient.Birthdate<="+POut.Date(dateStart)+"-INTERVAL 18 YEAR ";//18 or over at start of measurement period
			string encounterOrder="ORDER BY encounter.PatNum,encounter.DateEncounter DESC";
			string adultEncCommand=encounterSelectWhere+encounterWhereAdults+encounterOrder;
			switch(qtype) {
				#region MedicationsEntered
				case QualityType2014.MedicationsEntered:
					s.Restart();
					#region Get Initial Patient Population
					#region Get Valid Encounters
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.600.1.1834");//Medications Encounter Code Set Grouping Value Set
					//measureCur.ListEncounters will include all encounters that belong to the OneOf and TwoOf lists, so a patient will appear more than once
					//if they had more than one encounter from those sets in date range
					measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(adultEncCommand,listOneOfEncOIDs,listTwoOfEncOIDs);
					#endregion
					#region Get Patient Data
					//Denominator is equal to inital patient population for this measure, no exclusions
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters);
					#endregion
					#endregion
					#region Get Current Medications Documented Procedures
					//Get procedures from the value set that occurred during measurement period
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.600.1.462" };//Current Medications Documented SNMD SNOMED-CT Value Set
					//Only one procedure code in the value set for this measure, SNOMEDCT - 428191000124101 - Documentation of current medications (procedure)
					measureCur.DictPatNumListMeasureEvents=GetMedDocumentedProcs(listValueSetOIDs,dateStart,dateEnd);
					#endregion
					#region Get Medication Procs Not Performed
					//Get a list of all not performed items from the value set that occurred during the measurement period with valid readson
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.600.1.462" };//Current Medications Documented SNMD SNOMED-CT Value Set
					listReasonOIDs=new List<string>() { "2.16.840.1.113883.3.600.1.1502" };//Medical or Other reason not done SNOMED-CT Value Set
					measureCur.DictPatNumListNotPerfs=GetNotPerformeds(listValueSetOIDs,listReasonOIDs,dateStart,dateEnd);
					#endregion
					break;
				#endregion
				#region WeightAdult And WeightOver65
				//The two populations are >= 18 and < 64 at the start of the measurement period and >= 65 at the start of the measurement period.
				//These two populations exclude patients who are 64 at the start of the measurement period apparently on purpose.
				case QualityType2014.WeightAdult:
				case QualityType2014.WeightOver65:
					s.Restart();
					//Strategy: Get all eligible encounters for patients 65 and over for Over65, >= 18 and < 64 for Adult, at the start of the measurement period
					//Get Not Performed items for BMI exams with valid reason ("Medical or Other" and "Patient" reasons)
					//Remove from the encounter list any encounters that have a Not Performed item for a BMI exam on the same day
					//Get from disease table all palliative care 'procedures' (not likely ever going to be any, but these 'procedure orders' will be stored in the disease table)
					//Remove from the encounter list any encounters that occurred for patients who have a palliative care order that starts before or during the encounter
					//Get patient data from the remaining encounters (for reporting), these patients are the initial patient population.
					//The problem list will contain pregnancies as well as palliative care problems
					//If the pregnancy starts before or during measurement period and does not end before start of measurement period, the patient is excluded
					//Numerator - MOST RECENT physical exam that is within 6 months of the specific encounter with:
						//1.) BMI >= 23 kg/m2 and < 30 kg/m2 for Over65, >= 18.5 and < 25 for Adult, OR
						//2.) BMI >= 30 kg/m2 for Over65, >= 25 for Adult, with an intervention or medication order for 'Overweight' within 6 months of the specific encounter
						//3.) BMI < 23 kg/m2 for Over65, < 18.5 for Adult, with an intervention or medication order for 'Underweight' within 6 months of the specific encounter
					#region Get Initial Patient Population
					#region Get Raw Encounters
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.600.1.1751");//BMI Encounter Code Set Grouping Value Set
					string encsWhere65="AND patient.Birthdate<"+POut.Date(dateStart)+"-INTERVAL 65 YEAR ";//65 or over at the start of the measurement period
					string encsWhereLessThan64="AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 64 YEAR ";//< 64 years old at the start of the measurement period
					string encCommand="";
					if(qtype==QualityType2014.WeightOver65) {
						encCommand=encounterSelectWhere+encsWhere65+encounterOrder;
					}
					if(qtype==QualityType2014.WeightAdult) {
						encCommand=encounterSelectWhere+encounterWhereAdults+encsWhereLessThan64+encounterOrder;
					}
					measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(encCommand,listOneOfEncOIDs,listTwoOfEncOIDs);
					#endregion
					#region Get Pregnancy And Palliative Care Problems
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.600.1.1579" };//Palliative Care Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1623");//Pregnancy Dx Grouping Value Set
					measureCur.DictPatNumListProblems=GetProblems(null,listValueSetOIDs,dateStart,dateEnd);
					#endregion
					#region Get Not Performed
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.600.1.681" };//BMI LOINC Value LOINC Value Set
					listReasonOIDs=new List<string>() { "2.16.840.1.113883.3.600.1.1502" };//Medical or Other reason not done SNOMED-CT Value Set
					listReasonOIDs.Add("2.16.840.1.113883.3.600.1.1503");//Patient Reason Refused SNOMED-CT Value Set
					measureCur.DictPatNumListNotPerfs=GetNotPerformeds(listValueSetOIDs,listReasonOIDs,dateStart,dateEnd);
					#endregion
					#region Remove If Palliative Care Order Exists Prior To Encounter Date
					//if the patient with eligible encounter list has a palliative care order that starts before or during the encounter, remove the encounter
					//if all encounters end up removed, remove the PatNum key from the dictionary
					foreach(KeyValuePair<long,List<EhrCqmEncounter>> patNumListEncs in measureCur.DictPatNumListEncounters) {//loop through every patient with an encounter list
						if(!measureCur.DictPatNumListProblems.ContainsKey(patNumListEncs.Key)) {//if no palliative care problem, move to next patient
							continue;
						}
						List<EhrCqmProblem> listProbsCur=measureCur.DictPatNumListProblems[patNumListEncs.Key];
						for(int i=patNumListEncs.Value.Count-1;i>-1;i--) {//loop through encounter list for this patient
							for(int j=0;j<listProbsCur.Count;j++) {//loop through palliative care problem list for this patient\
								if(listProbsCur[j].ValueSetOID!="2.16.840.1.113883.3.600.1.1579") {//if not Palliative Care, move to next problem (problem list for this measure can contain pregnancy dx or palliative care 'procedures')
									continue;
								}
								if(listProbsCur[j].DateStart.Date<=patNumListEncs.Value[i].DateEncounter.Date) {//if palliative care dx starts before or on encounter date
									patNumListEncs.Value.RemoveAt(i);//remove the encounter
									break;//break out of problem list loop, move to next encounter
								}
							}
						}
					}
					#endregion
					#region Remove If Not Performed Exists On Encounter Date
					//if the patient with eligible encounter list also has a not performed on the same day, remove the encounter
					//this patient will not be in the initial patient population for this encounter but may still be for a different encounter date
					foreach(KeyValuePair<long,List<EhrCqmEncounter>> patNumListEncs in measureCur.DictPatNumListEncounters) {//loop through every patient with an encounter list
						if(!measureCur.DictPatNumListNotPerfs.ContainsKey(patNumListEncs.Key)) {//if no not performed items, move to next patient
							continue;
						}
						List<EhrCqmNotPerf> listNotPerfsCur=measureCur.DictPatNumListNotPerfs[patNumListEncs.Key];//the not performed items are guaranteed to have valid reasons
						for(int i=patNumListEncs.Value.Count-1;i>-1;i--) {//loop through encounters for this patient
							for(int j=0;j<listNotPerfsCur.Count;j++) {//loop through not performed items for this patient
								if(listNotPerfsCur[j].DateEntry.Date==patNumListEncs.Value[i].DateEncounter.Date) {//compare encounter date to not perfomed date
									patNumListEncs.Value.RemoveAt(i);//remove encounter if not performed item on same date
									break;//break out of not performed loop to move to next encounter
								}
							}
						}
					}
					//if all encounters for this patient have been removed, remove the PatNum key so the patient is not in the denominator
					List<long> allKeys=new List<long>(measureCur.DictPatNumListEncounters.Keys);
					for(int i=0;i<allKeys.Count;i++) {
						if(measureCur.DictPatNumListEncounters[allKeys[i]].Count==0) {
							measureCur.DictPatNumListEncounters.Remove(allKeys[i]);
						}
					}
					#endregion
					#region Get Patient Data
					//encounters are now eligible and only if no palliative care order before or on encounter date and no eligible not perforemed item on same date
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters);
					listEhrPatNums=GetListPatNums(measureCur.ListEhrPats);
					#endregion
					#endregion
					#region Get Vital Sign Exams
					//get all vitalsign exams with valid height and weight in the date range, we have to subtract 6 months from dateStart, since encounter must be in measurement period, but exam can be before measurement period as long as it is within 6 months of the encounter
					//each exam will have a calculated BMI, which is (weight*703)/(height*height)
					measureCur.DictPatNumListVitalsigns=GetVitalsignsForBMI(listEhrPatNums,dateStart.AddMonths(-6),dateEnd);
					#endregion
					#region Get Interventions
					//Get all interventions for eligible value sets that occurred within 6 months of the start of the measurement period up to the end of the measurement period
					command="SELECT * FROM intervention "
						+"WHERE DATE(DateEntry) BETWEEN "+POut.Date(dateStart)+"-INTERVAL 6 MONTH AND "+POut.Date(dateEnd)+" ";
					if(listEhrPatNums!=null && listEhrPatNums.Count>0) {
						command+="AND intervention.PatNum IN("+string.Join(",",listEhrPatNums)+") ";
					}
					command+="ORDER BY PatNum,DateEntry DESC";
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.600.1.1525" };//Above Normal Follow-up Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1528");//Below Normal Follow up Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1527");//Referrals where weight assessment may occur Grouping Value Set
					measureCur.DictPatNumListInterventions=GetInterventions(command,listValueSetOIDs);
					#endregion
					#region Get MedicationPats
					//Get all medicationpats (check for start date and instructions when calculating to make sure they are 'Orders') that started within 6 months of start date
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.600.1.1498" };//Above Normal Medications RxNorm Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1499");//Below Normal Medications RxNorm Value Set
					measureCur.DictPatNumListMedPats=GetMedPats(listEhrPatNums,listValueSetOIDs,dateStart.AddMonths(-6),dateEnd);
					#endregion
					break;
				#endregion
				#region CariesPrevent age >= 0 and < 20
				//These four will be the same encounter/procedure codes, just 4 different age groups: 0-19, 0-5, 6-12, and 13-19
				case QualityType2014.CariesPrevent:
				case QualityType2014.CariesPrevent_1:
				case QualityType2014.CariesPrevent_2:
				case QualityType2014.CariesPrevent_3:
					//Strategy: Get all encounters in date range from eligible value sets for patients in age range
					//No exclusions, initial patient population is denominator
					//Get Flouride varnish application procedures that occurred during the measurement period
					//If encounter and procedure, 'Numerator'
					//No exceptions
					s.Restart();
					#region Get Initial Patient Population
					#region Get All Eligible Encounters
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1001");//Office Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.125.12.1003");//Clinical Oral Evaluation Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1022");//Preventive Care- Initial Office Visit, 0 to 17 Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1023");//Preventive Care Services-Initial Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1024");//Preventive Care - Established Office Visit, 0 to 17 Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1025");//Preventive Care Services - Established Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1048");//Face-to-Face Interaction Grouping Value Set
					string child0To19Command=encounterSelectWhere
						+"AND patient.Birthdate<="+POut.Date(dateStart)+" "//age >= 0 at start of measurement period
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 20 YEAR "//age < 20 at start of measurement period
						+encounterOrder;
					string child0To5Command=encounterSelectWhere
						+"AND patient.Birthdate<="+POut.Date(dateStart)+" "//age >= 0 at start of measurement period
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 6 YEAR "//age <= 5 at start of measurement period
						+encounterOrder;
					string child6To12Command=encounterSelectWhere
						+"AND patient.Birthdate<="+POut.Date(dateStart)+"-INTERVAL 6 YEAR "//age >= 6 at start of measurement period
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 13 YEAR "//age <= 12 at start of measurement period
						+encounterOrder;
					string child13To19Command=encounterSelectWhere
						+"AND patient.Birthdate<="+POut.Date(dateStart)+"-INTERVAL 13 YEAR "//age >= 13 at start of measurement period
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 20 YEAR "//age < 20 at start of measurement period
						+encounterOrder;
					if(qtype==QualityType2014.CariesPrevent) {
						measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(child0To19Command,listOneOfEncOIDs,listTwoOfEncOIDs);
					}
					else if(qtype==QualityType2014.CariesPrevent_1) {
						measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(child0To5Command,listOneOfEncOIDs,listTwoOfEncOIDs);
					}
					else if(qtype==QualityType2014.CariesPrevent_2) {
						measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(child6To12Command,listOneOfEncOIDs,listTwoOfEncOIDs);
					}
					else if(qtype==QualityType2014.CariesPrevent_3) {
						measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(child13To19Command,listOneOfEncOIDs,listTwoOfEncOIDs);
					}
					#endregion
					#region Get Patient Data From Encounters
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters);
					listEhrPatNums=GetListPatNums(measureCur.ListEhrPats);
					#endregion
					#endregion
					#region Get Flouride Varnish Application Procedures
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.125.12.1002" };//Fluoride Varnish Application for Children Grouping Value Set
					measureCur.DictPatNumListProcs=GetProcs(listEhrPatNums,listValueSetOIDs,dateStart,dateEnd);
					#endregion
					break;
				#endregion
				#region ChildCaries
				case QualityType2014.ChildCaries:
					//Strategy: Get all encounters in date range from eligible value sets for patients in age range
					//No exclusions, initial patient population is denominator
					//Get Dental Caries diagnoses that were active during any of the measurement period (started before or during period and did NOT end before the start of period)
					//No exceptions
					s.Restart();
					#region Get Initial Patient Population
					#region Get All Eligible Encounters
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1001");//Office Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.125.12.1003");//Clinical Oral Evaluation Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1022");//Preventive Care- Initial Office Visit, 0 to 17 Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1023");//Preventive Care Services-Initial Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1024");//Preventive Care - Established Office Visit, 0 to 17 Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1025");//Preventive Care Services - Established Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1048");//Face-to-Face Interaction Grouping Value Set
					child0To19Command=encounterSelectWhere
						+"AND patient.Birthdate<="+POut.Date(dateStart)+" "//age >= 0 at start of measurement period (born before or on start date)
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 20 YEAR "//age < 20 at start of measurement period
						+encounterOrder;
					measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(child0To19Command,listOneOfEncOIDs,listTwoOfEncOIDs);
					#endregion
					#region Get Patient Data From Encounters
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters);
					listEhrPatNums=GetListPatNums(measureCur.ListEhrPats);
					#endregion
					#endregion
					#region Get Dental Caries Diagnoses
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.125.12.1004" };//Dental Caries Grouping Value Set
					measureCur.DictPatNumListProblems=GetProblems(listEhrPatNums,listValueSetOIDs,dateStart,dateEnd);
					#endregion
					break;
				#endregion
				#region Pneumonia
				case QualityType2014.Pneumonia:
					//Strategy: Get encounters from eligible value sets for patients >= 65 years old before the start of the measurement period
					//Those patients will be initial patient population and denominator
					//No exclusions
					//Get vaccinepats with eligible code, only one code, CVX - 33 - pneumococcal polysaccharide vaccine, 23 valent
					//Get procs with eligible code, SNOMEDCT - 12866006 - Pneumococcal vaccination (procedure) and SNOMEDCT - 394678003 - Booster pneumococcal vaccination (procedure)
					//Get problems with eligible code, only one code, SNOMEDCT - 310578008 - Pneumococcal vaccination given (finding)
					#region Get Initial Patient Population
					#region Get Raw Encounters
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1240");//Annual Wellness Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1001");//Office Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1016");//Home Healthcare Services Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1023");//Preventive Care Services-Initial Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1025");//Preventive Care Services - Established Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1048");//Face-to-Face Interaction Grouping Value Set
					encCommand=encounterSelectWhere
						+"AND patient.Birthdate<"+POut.Date(dateStart)+"-INTERVAL 65 YEAR "//65 or over at start of measurement period
						+encounterOrder;
					measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(encCommand,listOneOfEncOIDs,listTwoOfEncOIDs);
					#endregion
					#region Get Patient Data
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters);
					listEhrPatNums=GetListPatNums(measureCur.ListEhrPats);//for restricting the following list of procs, problems, and vaccinepats
					#endregion
					#endregion
					#region Get Vaccinepats
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.110.12.1027" };//Pneumococcal Vaccine Grouping Value Set
					measureCur.DictPatNumListMedPats=GetVaccines(listEhrPatNums,listValueSetOIDs,DateTime.MinValue,dateEnd);
					#endregion
					#region Get Procs
					//Get procs that are Pneumococcal vaccine administered SNOMEDCT - 12866006
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.110.12.1034" };//Pneumococcal Vaccine Administered Grouping Value Set
					measureCur.DictPatNumListProcs=GetProcs(listEhrPatNums,listValueSetOIDs,DateTime.MinValue,dateEnd);
					#endregion
					#region Get Problems
					//Get problems that are history of pheumococcal vaccine recoreded, one code allowed, SNOMEDCT - 310578008
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.110.12.1028" };//History of Pneumococcal Vaccine Grouping Value Set
					measureCur.DictPatNumListProblems=GetProblems(listEhrPatNums,listValueSetOIDs,DateTime.MinValue,dateEnd);
					#endregion
					s.Restart();
					break;
				#endregion
				#region TobaccoCessation
				case QualityType2014.TobaccoCessation:
					s.Restart();
					#region Get Valid Encounters
					//add one of encounter OIDs to list
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1240");//Annual Wellness Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1023");//Preventive Care Services-Initial Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1025");//Preventive Care Services - Established Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1026");//Preventive Care Services-Individual Counseling Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1027");//Preventive Care Services - Group Counseling Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1030");//Preventive Care Services - Other Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1048");//Face-to-Face Interaction Grouping Value Set
					//add two of encounter OIDs to list
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1001");//Office Visit Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1011");//Occupational Therapy Evaluation Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1020");//Health & Behavioral Assessment - Individual Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1141");//Psychoanalysis Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1245");//Health and Behavioral Assessment - Initial Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1285");//Ophthalmological Services Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1492");//Psych Visit - Diagnostic Evaluation Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1496");//Psych Visit - Psychotherapy Grouping Value Set
					//measureCur.ListEncounters will include all encounters that belong to the OneOf and TwoOf lists, so a patient will appear more than once
					//if they had more than one encounter from those sets in date range
					measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(adultEncCommand,listOneOfEncOIDs,listTwoOfEncOIDs);
					#endregion
					#region Get Initial Patient Population
					//Denominator is equal to initial patient population for this measure
					//the Inital Patient Population will be unique patients in ListEncounters, loop through and count unique patients
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters);
					listEhrPatNums=GetListPatNums(measureCur.ListEhrPats);
					#endregion
					#region Get Tobacco Assessments
					//Get a list of all tobacco assessment events that happened within 24 of end of measurement period
					measureCur.DictPatNumListMeasureEvents=GetTobaccoAssessmentEvents(dateEnd);
					#endregion
					#region Get Tobacco Cessation Interventions
					//Get all interventions within 24 months of end of measurement period
					command="SELECT * FROM intervention "
						+"WHERE DATE(DateEntry) BETWEEN "+POut.Date(dateEnd)+"-INTERVAL 24 MONTH AND "+POut.Date(dateEnd)+" ";
					if(listEhrPatNums!=null && listEhrPatNums.Count>0) {
						command+="AND intervention.PatNum IN("+string.Join(",",listEhrPatNums)+") ";
					}
					command+="ORDER BY PatNum,DateEntry DESC";
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.509" };//Tobacco Use Cessation Counseling Grouping Value Set
					measureCur.DictPatNumListInterventions=GetInterventions(command,listValueSetOIDs);
					#endregion
					#region Get Tobacco Cessation Meds
					//Get a list of all tobacco cessation meds active/ordered within 24 months of end of measurement period
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1190" };////Tobacco Use Cessation Pharmacotherapy Grouping Value Set
					measureCur.DictPatNumListMedPats=GetMedPats(listEhrPatNums,listValueSetOIDs,dateEnd.AddMonths(-24),dateEnd);
					#endregion
					#region Get Tobacco Screenings Not Performed
					//Get a list of all tobacco assessment not performed items that happened in the measurement period that belong to the value set
					//that also have a valid medical reason attached from the above value set
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1278" };//Tobacco Use Screening Grouping Value Set
					listReasonOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1007" };//Medical Reason Grouping Value Set
					measureCur.DictPatNumListNotPerfs=GetNotPerformeds(listValueSetOIDs,listReasonOIDs,dateStart,dateEnd);
					#endregion
					#region Get Limited Life Expectancy Probs
					listValueSetOIDs=new List<string>() {"2.16.840.1.113883.3.526.3.1259"};//Limited Life Expectancy Grouping Value Set
					//Get a list of all limited life expectancy diagnoses in the measurement period that belong to the above value set
					measureCur.DictPatNumListProblems=GetProblems(listEhrPatNums,listValueSetOIDs,dateStart,dateEnd);
					#endregion
					break;
				#endregion
//Todo:
				#region Influenza
				case QualityType2014.Influenza:
					//Strategy: Get encounters from one of and two of lists for patients >= 6 months before start of measurement period
					//Denominator: Those in the list of encounters who also had an encounter <= 92 days BEFORE the start of the measurement period from three code sets
					//OR an encounter from the same three code sets <= 91 days AFTER the start of the measurement period
					//No exclusions
					//Numerator: Get all flu vaccine procs and flu vaccine medications (vaccinepats, these will include the communication from patient to provider of previous receipt of vaccine)
					//that happened during the encounter <= 92 days before start of period or <= 91 days after start of period
					//Exceptions: 1. communication from patient to prov declining vaccine (vaccinepats with CompletionStatus=1 (refused))
					//2. procedure or medication NotPerformed for medical, patient, or system reason
					//Both 1 and 2 have to be during the encounter in the <= 92 before start or <= 91 days after start of period date range
					//OR active diagnosis of allergy to eggs, allergy to flu vaccine, or intolerance to flu vaccine; medication allergy or intolerance to flu vaccine; procedure intolerance to vaccine
					//Those have to start before or during the encounter in the date range.
					#region Get Initial Patient Population
					#region Get Raw Encounters
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1240");//Annual Wellness Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1012");//Nursing Facility Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1013");//Discharge Services - Nursing Facility Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1022");//Preventive Care- Initial Office Visit, 0 to 17 Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1023");//Preventive Care Services-Initial Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1024");//Preventive Care - Established Office Visit, 0 to 17 Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1025");//Preventive Care Services - Established Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1026");//Preventive Care Services-Individual Counseling Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1027");//Preventive Care Services - Group Counseling Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1030");//Preventive Care Services - Other Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1012");//Patient Provider Interaction Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1001");//Office Visit Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1008");//Outpatient Consultation Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1014");//Care Services in Long-Term Residential Facility Grouping Value Set
					listTwoOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1016");//Home Healthcare Services Grouping Value Set
					encCommand=encounterSelectWhere
						+"AND patient.Birthdate<"+POut.Date(dateStart)+"-INTERVAL 6 MONTH "//>= 6 months at start of measurement period
						+encounterOrder;
					measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(encCommand,listOneOfEncOIDs,listTwoOfEncOIDs);
					#endregion
					#region Get Patients Who Had Initial Population Proc
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1083" };//Hemodialysis Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.1084");//Peritoneal Dialysis Grouping Value Set
					measureCur.DictPatNumListProcs=GetProcs(null,listValueSetOIDs,dateStart,dateEnd);
					#endregion
					#region Get Patient Data
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters,measureCur.DictPatNumListProcs);
					listEhrPatNums=GetListPatNums(measureCur.ListEhrPats);//for restricting the following list of procs, problems, and vaccinepats
					#endregion
					#endregion
					#region Apply Additional Denominator Requirements
					//These are additional requirements for the patient to be in the denominator.
					//The patient must have one of the eligible encounters/procedures <= 92 days before the start of the period or <= 91 days after the start of the period to be in denominator.
					listOneOfEncOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1252" };//Encounter-Influenza Grouping Value Set
					listTwoOfEncOIDs=new List<string>();
					encCommand="SELECT encounter.* FROM encounter "
						+"INNER JOIN patient ON patient.PatNum=encounter.PatNum "
						+"WHERE YEAR(patient.Birthdate)>1880 "//valid birthdate
						+"AND encounter.ProvNum IN("+POut.String(provs)+") "
						+"AND DATE(encounter.DateEncounter) BETWEEN "+POut.Date(dateStart.AddDays(-92))+" AND "+POut.Date(dateStart.AddDays(91))+" ";
					if(listEhrPatNums!=null && listEhrPatNums.Count>0) {
						command+="AND encounter.PatNum IN("+string.Join(",",listEhrPatNums)+") ";
					}
					command+="ORDER BY encounter.PatNum,encounter.DateEncounter DESC";
					Dictionary<long,List<EhrCqmEncounter>> dictPatNumListDenomEncs=GetEncountersWithOneOfAndTwoOfOIDs(encCommand,listOneOfEncOIDs,listTwoOfEncOIDs);
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1083" };//Hemodialysis Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.1084");//Peritoneal Dialysis Grouping Value Set
					Dictionary<long,List<EhrCqmProc>> dictPatNumListDenomProcs=GetProcs(listEhrPatNums,listValueSetOIDs,dateStart.AddDays(-92),dateStart.AddDays(91));
					//for each patient in initial pat population ListEhrPats, make sure they have an encounter in dictPatNumListDenomEncs or a procedure in dictPatNumListDenomProcs
					//those lists will have the eligible encounters or procedures in the <= 92 days starts before start of period and <= 91 days starts after start of period
					//i.e. the encounter or procedure took place between October 1st of year prior to measurement period and March 31st of year of measurement period
					//this date range is their definition of the "influenza season"
					for(int i=0;i<measureCur.ListEhrPats.Count;i++) {
						long patNumCur=measureCur.ListEhrPats[i].EhrCqmPat.PatNum;
						bool isDenom=false;
						if(dictPatNumListDenomEncs.ContainsKey(patNumCur)) {
							isDenom=true;
							if(measureCur.DictPatNumListEncounters.ContainsKey(patNumCur)) {
								measureCur.DictPatNumListEncounters[patNumCur].AddRange(dictPatNumListDenomEncs[patNumCur]);
							}
							else {//no encounters from initial patient population, must be in ipp from procedure, add new list to include these encounters
								measureCur.DictPatNumListEncounters.Add(patNumCur,dictPatNumListDenomEncs[patNumCur]);
							}
						}
						if(dictPatNumListDenomProcs.ContainsKey(patNumCur)) {
							isDenom=true;
							if(measureCur.DictPatNumListProcs.ContainsKey(patNumCur)) {
								measureCur.DictPatNumListProcs[patNumCur].AddRange(dictPatNumListDenomProcs[patNumCur]);
							}
							else {
								measureCur.DictPatNumListProcs.Add(patNumCur,dictPatNumListDenomProcs[patNumCur]);
							}
						}
						if(!isDenom) {
							//this is the only place the bool IsDenominator is ever set to false
							measureCur.ListEhrPats[i].IsDenominator=false;
						}
					}
					#endregion
					#region Get Numerator Data
					#region Get Influenza Vaccination Procedures
					//These will actually be in the procedurelog table, the user will have to manually add the correct code to the proccode table and then chart the procedure
					//Codes are CPT or SNOMEDCT
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.402" };//Influenza Vaccination Grouping Value Set
					Dictionary<long,List<EhrCqmProc>> dictPatNumListNumeProcs=GetProcs(listEhrPatNums,listValueSetOIDs,dateStart.AddDays(-92),dateStart.AddDays(91));
					for(int i=0;i<measureCur.ListEhrPats.Count;i++) {
						long patNumCur=measureCur.ListEhrPats[i].EhrCqmPat.PatNum;
						if(dictPatNumListNumeProcs.ContainsKey(patNumCur)) {
							if(measureCur.DictPatNumListProcs.ContainsKey(patNumCur)) {
								measureCur.DictPatNumListProcs[patNumCur].AddRange(dictPatNumListNumeProcs[patNumCur]);
							}
							else {
								measureCur.DictPatNumListProcs.Add(patNumCur,dictPatNumListNumeProcs[patNumCur]);
							}
						}
					}
					#endregion
					#region Get Influenza Vaccination Medications
					//These will be the CVX codes that will be in the vaccinepat table
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1254" };//Influenza Vaccine Grouping Value Set
					measureCur.DictPatNumListMedPats=GetVaccines(listEhrPatNums,listValueSetOIDs,dateStart.AddDays(-92),dateStart.AddDays(91));
					#endregion
					#region Get Influenza Vaccination Communication of Previous Receipt
					//These will be in the patient's 'Problem' list, like the Pneumonia vaccine communication of previous receipt, 4 possible SNOMEDCT codes
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1185" };//Previous Receipt of Influenza Vaccine Grouping Value Set
					//this code is only one eligible SNOMEDCT code used for exceptions, but we will add it to the list here
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.1255");//Influenza Vaccination Declined Grouping Value Set
					measureCur.DictPatNumListProblems=GetProblems(listEhrPatNums,listValueSetOIDs,dateStart.AddDays(-92),dateStart.AddDays(91));
					#endregion
					#endregion
					#region Get Exceptions
					#region Get Communication of Patient Declined
					//These are added with the previous receipt communications above
					#endregion
					#region Get Not Performed Items
					//These will also have to have taken place during the "Occurrence A of" encounter/procedure so limited to Oct 1 of previous year to March 31 of period year
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1254" };//Medication, Administered: Influenza Vaccine Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.402");//Procedure, Performed: Influenza Vaccination Grouping Value Set
					listReasonOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1007" };//Medical Reason Grouping Value Set
					listReasonOIDs.Add("2.16.840.1.113883.3.526.3.1008");//Patient Reason Grouping Value Set
					listReasonOIDs.Add("2.16.840.1.113883.3.526.3.1009");//System Reason Grouping Value Set
					measureCur.DictPatNumListNotPerfs=GetNotPerformeds(listValueSetOIDs,listReasonOIDs,dateStart.AddDays(-92),dateStart.AddDays(91));
					#endregion
					#region Get Eligible Allergies
					//We might have to enhance the allergydef window to allow for adding allergies/intolerances to CVX codes and CPT/SNOMEDCT procedure codes
					//We can add them to the pat's problem list if they are SNOMEDCT codes, but not if they are CPT or CVX codes
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.1253" };//Diagnosis, Active: Allergy to eggs Grouping Value Set, all SNOMEDCT codes, Problem list?
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.402");//Procedure, Intolerance: Influenza Vaccination Grouping Value Set, CPT and SNOMEDCT codes, Procedurelog? how to mark them as intolerance instead of complete??
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.1254");//Medication, Intolerance OR Medication, Allergy: Influenza Vaccine Grouping Value Set, CVX codes, use the vaccinepat table as NotGiven=1 and CompletionStatus=2 (Not Administered)?? How to identify them as not given due to allergy or intolerance? Add to VaccineRefusalReason enum??
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.1256");//Diagnosis, Active: Allergy to Influenza Vaccine Grouping Value Set, SNOMEDCT codes, Problem list?
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.1257");//Diagnosis, Active: Intolerance to Influenza Vaccine Grouping Value Set, SNOMEDCT codes, Problem list?
					measureCur.DictPatNumListProblems=GetProblems(listEhrPatNums,listValueSetOIDs,DateTime.MinValue,dateStart.AddDays(91));
					#endregion
					#endregion
					s.Restart();
					break;
				#endregion
				#region WeightChild_X_1 Height, Weight, and BMI Exams
				case QualityType2014.WeightChild_1_1:
				case QualityType2014.WeightChild_2_1:
				case QualityType2014.WeightChild_3_1:
					//For each group of three measures, the first of the group will get all encounters, patient data, pregnancy diagnoses, vitalsign exams and interventions for that age group.
					//All three will use the same patient data.  Example 1_1 will get all data used by 1_1, 1_2, and 1_3.
					//Strategy for the weight child measures: Get eligible encounters for patients in age range that occurred during the measurement period
					//These patients are the initial patient population
					//Exclusion: Pregnant during any of the measurement period
					//Numerator 1: There is a vital sign exam with height, weight, and BMI percentile recorded during the measurement period
					//Numerator 2: There is an intervention for nutrition counseling during the measurement period
					//Numerator 3: There is an intervention for physical activity during the measurement period
					s.Restart();
					#region Get Initial Patient Population
					#region Get All Eligible Encounters
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1048");//Face-to-Face Interaction Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1016");//Home Healthcare Services Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1001");//Office Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1024");//Preventive Care - Established Office Visit, 0 to 17 Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1027");//Preventive Care Services - Group Counseling Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1026");//Preventive Care Services-Individual Counseling Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1022");//Preventive Care- Initial Office Visit, 0 to 17 Grouping Value Set
					string child3To16Command=encounterSelectWhere
						+"AND patient.Birthdate<="+POut.Date(dateStart)+"-INTERVAL 3 YEAR "//age >= 3 at start of measurement period
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 17 YEAR "//age < 17 at start of measurement period
						+encounterOrder;
					string child3To11Command=encounterSelectWhere
						+"AND patient.Birthdate<="+POut.Date(dateStart)+"-INTERVAL 3 YEAR "//age >= 3 at start of measurement period
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 12 YEAR "//age <= 11 at start of measurement period
						+encounterOrder;
					string child12To16Command=encounterSelectWhere
						+"AND patient.Birthdate<="+POut.Date(dateStart)+"-INTERVAL 12 YEAR "//age >= 12 at start of measurement period
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 17 YEAR "//age < 17 at start of measurement period
						+encounterOrder;
					if(qtype==QualityType2014.WeightChild_1_1) {
						measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(child3To16Command,listOneOfEncOIDs,listTwoOfEncOIDs);
					}
					else if(qtype==QualityType2014.WeightChild_2_1) {
						measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(child3To11Command,listOneOfEncOIDs,listTwoOfEncOIDs);
					}
					else if(qtype==QualityType2014.WeightChild_3_1) {
						measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(child12To16Command,listOneOfEncOIDs,listTwoOfEncOIDs);
					}
					#endregion
					#region Get Patient Data From Encounters
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters);
					listEhrPatNums=GetListPatNums(measureCur.ListEhrPats);
					#endregion
					#endregion
					#region Get Pregnancy Diagnoses
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.526.3.378" };//Pregnancy Grouping Value Set
					measureCur.DictPatNumListProblems=GetProblems(listEhrPatNums,listValueSetOIDs,dateStart,dateEnd);
					#endregion
					#region Get Vitalsign Exams
					measureCur.DictPatNumListVitalsigns=GetVitalsignsForBMI(listEhrPatNums,dateStart,dateEnd);
					#endregion
					#region Get Interventions
					//DictPatNumListInterventions will hold the phys activity and Nutrition counseling interventions
					command="SELECT * FROM intervention "
						+"WHERE DATE(DateEntry) BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" ";
					if(listEhrPatNums!=null && listEhrPatNums.Count>0) {
						command+="AND intervention.PatNum IN("+string.Join(",",listEhrPatNums)+") ";
					}
					command+="ORDER BY PatNum,DateEntry DESC";
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.195.12.1003" };//Counseling for Nutrition Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.464.1003.118.12.1035");//Counseling for Physical Activity Grouping Value Set
					measureCur.DictPatNumListInterventions=GetInterventions(command,listValueSetOIDs);
					#endregion
					#region Make Copies Of MeasureCur Data For WeightChild _2 and _3
					if(qtype==QualityType2014.WeightChild_1_1) {
						_measureWeightAssessAll=measureCur.Copy();
					}
					else if(qtype==QualityType2014.WeightChild_2_1) {
						_measureWeightAssess3To11=measureCur.Copy();
					}
					else if(qtype==QualityType2014.WeightChild_3_1) {
						_measureWeightAssess12To16=measureCur.Copy();
					}
					#endregion
					break;
				#endregion
				#region WeightChild_X_2 and WeightChild_X_3 Nutrition and Physical Activity Counseling
				case QualityType2014.WeightChild_1_2:
				case QualityType2014.WeightChild_2_2:
				case QualityType2014.WeightChild_3_2:
				case QualityType2014.WeightChild_1_3:
				case QualityType2014.WeightChild_2_3:
				case QualityType2014.WeightChild_3_3:
					s.Restart();
					#region Get All Data From Copy
					if(qtype==QualityType2014.WeightChild_1_2 || qtype==QualityType2014.WeightChild_1_3) {
						measureCur=_measureWeightAssessAll.Copy();
					}
					else if(qtype==QualityType2014.WeightChild_2_2 || qtype==QualityType2014.WeightChild_2_3) {
						measureCur=_measureWeightAssess3To11.Copy();
					}
					else if(qtype==QualityType2014.WeightChild_3_2 || qtype==QualityType2014.WeightChild_3_3) {
						measureCur=_measureWeightAssess12To16.Copy();
					}
					#endregion
					break;
				#endregion
				#region BloodPressureManage
				case QualityType2014.BloodPressureManage:
					s.Restart();
					#region Get Initial Patient Population
					#region Get Raw Encounters
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.526.3.1240");//Annual Wellness Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1001");//Office Visit Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1008");//Outpatient Consultation Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1016");//Home Healthcare Services Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1023");//Preventive Care Services-Initial Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1025");//Preventive Care Services - Established Office Visit, 18 and Up Grouping Value Set
					listOneOfEncOIDs.Add("2.16.840.1.113883.3.464.1003.101.12.1048");//Face-to-Face Interaction Grouping Value Set
					string encsWhere18To85=encounterSelectWhere
						+"AND patient.Birthdate>"+POut.Date(dateStart)+"-INTERVAL 85 YEAR "//< 85 years old at the start of the measurement period
						+encounterOrder;
					measureCur.DictPatNumListEncounters=GetEncountersWithOneOfAndTwoOfOIDs(encsWhere18To85,listOneOfEncOIDs,listTwoOfEncOIDs);
					#endregion
					#region Get Essential Hypertension Problems
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.104.12.1011" };//Essential Hypertension Grouping Value Set
					//hypertension problem must start within 6 months of start of measurement period
					//if measurement period end date is less than 6 months after the start date,
					//problem start date is limited by end date not 6 months after start date as that would start after the end of the measurement period
					DateTime probDateEnd=dateEnd;
					if(dateStart.AddMonths(6)<dateEnd) {
						probDateEnd=dateStart.AddMonths(6);
					}
					Dictionary<long,List<EhrCqmProblem>> dictPatNumListHypertension=GetProblems(null,listValueSetOIDs,dateStart,probDateEnd);
					#endregion
					#region Remove If No Hypertension Diagnosis
					//if the patient with eligible encounter list has not been diagnosed with hypertension within 6 months of the start of the measurement period, remove from IPP
					//(diagnosis <= 6 months starts after dateStart) OR (diagnosis starts before dateStart AND NOT diagnosis ends before dateStart)
					List<long> allEncKeys=new List<long>(measureCur.DictPatNumListEncounters.Keys);
					for(int i=0;i<allEncKeys.Count;i++) {
						if(!dictPatNumListHypertension.ContainsKey(allEncKeys[i])) {
							measureCur.DictPatNumListEncounters.Remove(allEncKeys[i]);
						}
					}
					#endregion
					#region Get Patient Data
					//encounters are now eligible and only if hypertension diagnosis within 6 months of start of measurement period (or before endDate if sooner than 6 months after start date)
					measureCur.ListEhrPats=GetEhrPatsFromEncsOrProcs(measureCur.DictPatNumListEncounters);
					listEhrPatNums=GetListPatNums(measureCur.ListEhrPats);
					#endregion
					#endregion
					#region Get Exclusion Items
					#region Get Exclusion Diagnoses And Hypertension Diagnoses
					//ListEhrPats is now initial patient population, get hyptertension, pregnancy, end stage renal disease, and chronic kidney disease diagnoses
					//This time the hypertension diagnoses will be for the entire measurement period, not just within the 6 months after the start date.
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.104.12.1011" };//Essential Hypertension Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.378");//Pregnancy Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.353");//End Stage Renal Disease Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.1002");//Chronic Kidney Disease, Stage 5 Grouping Value Set
					measureCur.DictPatNumListProblems=GetProblems(listEhrPatNums,listValueSetOIDs,dateStart,dateEnd);
					#endregion
					#region Get Interventions For Exclusion
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.109.12.1015" };//Other Services Related to Dialysis Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.464.1003.109.12.1016");//Dialysis Education Grouping Value Set
					//Get all interventions for eligible value sets that occurred before or during the measurement period
					command="SELECT * FROM intervention "
						+"WHERE DATE(DateEntry)<="+POut.Date(dateEnd)+" ";
					if(listEhrPatNums!=null && listEhrPatNums.Count>0) {
						command+="AND intervention.PatNum IN("+string.Join(",",listEhrPatNums)+") ";
					}
					command+="ORDER BY PatNum,DateEntry DESC";
					measureCur.DictPatNumListInterventions=GetInterventions(command,listValueSetOIDs);
					#endregion
					#region Get Procedures For Exclusion
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.109.12.1011" };//Vascular Access for Dialysis Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.464.1003.109.12.1012");//Kidney Transplant Grouping Value Set
					listValueSetOIDs.Add("2.16.840.1.113883.3.464.1003.109.12.1013");//Dialysis Services Grouping Value Set
					measureCur.DictPatNumListProcs=GetProcs(listEhrPatNums,listValueSetOIDs,DateTime.MinValue,dateEnd);
					#endregion
					#region Get Encounters For Exclusion
					//Get encounters that occurred before or during the measurement period that belong to the value set and add them to DictPatNumListEncounters
					//these encounters will be only for those patients already in the IPP, and will simply be added to the list of encounters for that patient
					//they will be used for exclusion in the Classify function, if one exists the patient is in the IPP and denominator but excluded from the measure
					listValueSetOIDs=new List<string>() { "2.16.840.1.113883.3.464.1003.109.12.1014" };//ESRD Monthly Outpatient Services Grouping Value Set
					command="SELECT encounter.* FROM encounter "
						+"INNER JOIN patient ON patient.PatNum=encounter.PatNum "
						+"WHERE YEAR(patient.Birthdate)>1880 "//valid birthdate
						+"AND encounter.ProvNum IN("+POut.String(provs)+") "
						+"AND DATE(encounter.DateEncounter)<="+POut.Date(dateEnd)+" ";
					if(listEhrPatNums!=null && listEhrPatNums.Count>0) {
						command+="AND encounter.PatNum IN("+string.Join(",",listEhrPatNums)+") ";
					}
					Dictionary<long,List<EhrCqmEncounter>> dictPatNumListEncs=GetEncountersWithOneOfAndTwoOfOIDs(command,listValueSetOIDs,listTwoOfEncOIDs);
					//add to the list of encounters for all patients in the IPP any end stage renal disease encounters
					allKeys=new List<long>(dictPatNumListEncs.Keys);
					for(int i=0;i<allKeys.Count;i++) {
						if(measureCur.DictPatNumListEncounters.ContainsKey(allKeys[i])) {
							measureCur.DictPatNumListEncounters[allKeys[i]].AddRange(dictPatNumListEncs[allKeys[i]]);
						}
					}
					#endregion
					#endregion
					#region Get Vitalsign Exams For BP
					measureCur.DictPatNumListVitalsigns=GetVitalsignsForBP(listEhrPatNums,dateStart,dateEnd);
					#endregion
					break;
				#endregion
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
			//this will mark the patients in ListEhrPats as Numerator, Exclusion, or Exception, with explanation
			ClassifyPatients(measureCur,qtype);
			s.Stop();
			_elapsedtimetext+=qtype.ToString()+": "+s.Elapsed.ToString()+"\r\n";
			return measureCur;
		}

		///<summary>Simple helper function to get a list of PatNums from a list of EhrCqmPatient objects.  Used to limit the number of records returned in other Get functions below, like GetProcs.</summary>
		private static List<long> GetListPatNums(List<EhrCqmPatient> listEhrCqmPats) {
			List<long> retval=new List<long>();
			for(int i=0;i<listEhrCqmPats.Count;i++) {
				retval.Add(listEhrCqmPats[i].EhrCqmPat.PatNum);
			}
			return retval;
		}

		///<summary>The string command will retrieve all unique encounters in the date range, for the provider (based on provider.EhrKey, so may be more than one ProvNum), with age limitation or other restrictions applied.  The encounters will then be required to belong to the value sets identified by the oneOf and twoOf lists of OID's (Object Identifiers), and the patient will have to have had one or more of the oneOf encounters or two or more of the two of encounters in the list returned by the string command.  We will return a dictionary with PatNum as the key that links to a list of all EhrCqmEncounter objects for that patient with all of the required elements for creating the QRDA Category I and III documents.</summary>
		private static Dictionary<long,List<EhrCqmEncounter>> GetEncountersWithOneOfAndTwoOfOIDs(string command,List<string> listOneOfEncOIDs,List<string> listTwoOfEncOIDs) {
			List<Encounter> listEncs=Crud.EncounterCrud.SelectMany(command);
			List<EhrCode> listOneOfEncs=EhrCodes.GetForValueSetOIDs(listOneOfEncOIDs,false);
			List<EhrCode> listTwoOfEncs=EhrCodes.GetForValueSetOIDs(listTwoOfEncOIDs,false);
			Dictionary<long,int> dictPatNumAndTwoOfCount=new Dictionary<long,int>();
			List<long> listPatNums=new List<long>();
			Dictionary<long,EhrCode> dictEncNumEhrCode=new Dictionary<long,EhrCode>();
			//Remove any encounters that are not one of the allowed types and create a list of patients who had 1 or more of the OneOf encounters and a dictionary with PatNum,Count
			//for counting the number of TwoOf encounters for each patient
			for(int i=listEncs.Count-1;i>-1;i--) {
				bool isOneOf=false;
				for(int j=0;j<listOneOfEncs.Count;j++) {
					if(listEncs[i].CodeValue==listOneOfEncs[j].CodeValue && listEncs[i].CodeSystem==listOneOfEncs[j].CodeSystem) {
						if(!listPatNums.Contains(listEncs[i].PatNum)) {
							listPatNums.Add(listEncs[i].PatNum);
						}
						dictEncNumEhrCode.Add(listEncs[i].EncounterNum,listOneOfEncs[j]);
						isOneOf=true;
						break;
					}
				}
				if(isOneOf) {
					continue;
				}
				bool isTwoOf=false;
				for(int j=0;j<listTwoOfEncs.Count;j++) {
					if(listEncs[i].CodeValue==listTwoOfEncs[j].CodeValue && listEncs[i].CodeSystem==listTwoOfEncs[j].CodeSystem) {
						if(dictPatNumAndTwoOfCount.ContainsKey(listEncs[i].PatNum)) {
							dictPatNumAndTwoOfCount[listEncs[i].PatNum]++;
						}
						else {
							dictPatNumAndTwoOfCount.Add(listEncs[i].PatNum,1);
						}
						dictEncNumEhrCode.Add(listEncs[i].EncounterNum,listTwoOfEncs[j]);
						isTwoOf=true;
						break;
					}
				}
				if(!isTwoOf) {//not oneOf or twoOf encounter, remove from list
					listEncs.RemoveAt(i);//not an eligible encounter
				}
			}
			//add the patients who had 2 or more of the TwoOf encounters to the list of patients
			foreach(KeyValuePair<long,int> kpairCur in dictPatNumAndTwoOfCount) {
				if(listPatNums.Contains(kpairCur.Key)) {
					continue;
				}
				if(kpairCur.Value>1) {
					listPatNums.Add(kpairCur.Key);
				}
			}
			//remove any encounters from the list for patients who did not have a OneOf or two or more of the TwoOf encounters.
			for(int i=listEncs.Count-1;i>-1;i--) {
				if(!listPatNums.Contains(listEncs[i].PatNum)) {
					listEncs.RemoveAt(i);
				}
			}
			//listEncs is now all encounters returned by the command (should be date restricted, age restricted, provider restricted, etc.) that belong to the OneOf or TwoOf list
			//for patients who had one or more of the OneOf encounters and/or two or more of the TwoOf encounters
			//dictEncNumEhrCode links an EncounterNum to an EhrCode object.  This will be an easy way to get the ValueSetOID, ValueSetName, and CodeSystemOID for the encounter.
			Dictionary<long,List<EhrCqmEncounter>> retval=new Dictionary<long,List<EhrCqmEncounter>>();
			for(int i=0;i<listEncs.Count;i++) {
				EhrCqmEncounter ehrEncCur=new EhrCqmEncounter();
				ehrEncCur.EhrCqmEncounterNum=listEncs[i].EncounterNum;
				ehrEncCur.PatNum=listEncs[i].PatNum;
				ehrEncCur.ProvNum=listEncs[i].ProvNum;
				ehrEncCur.CodeValue=listEncs[i].CodeValue;
				ehrEncCur.CodeSystemName=listEncs[i].CodeSystem;
				ehrEncCur.DateEncounter=listEncs[i].DateEncounter;
				EhrCode ehrCodeCur=dictEncNumEhrCode[listEncs[i].EncounterNum];
				ehrEncCur.ValueSetName=ehrCodeCur.ValueSetName;
				ehrEncCur.ValueSetOID=ehrCodeCur.ValueSetOID;
				ehrEncCur.CodeSystemOID=ehrCodeCur.CodeSystemOID;
				string descript="";
				descript=ehrEncCur.Description;//in case not in table default to EhrCode object description
				//to get description, first determine which table the code is from.  Encounter is only allowed to be a CDT, CPT, HCPCS, and SNOMEDCT.
				switch(ehrEncCur.CodeSystemName) {
					case "CDT":
						descript=ProcedureCodes.GetProcCode(ehrEncCur.CodeValue).Descript;
						break;
					case "CPT":
						Cpt cptCur=Cpts.GetByCode(ehrEncCur.CodeValue);
						if(cptCur!=null) {
							descript=cptCur.Description;
						}
						break;
					case "HCPCS":
						Hcpcs hCur=Hcpcses.GetByCode(ehrEncCur.CodeValue);
						if(hCur!=null) {
							descript=hCur.DescriptionShort;
						}
						break;
					case "SNOMEDCT":
						Snomed sCur=Snomeds.GetByCode(ehrEncCur.CodeValue);
						if(sCur!=null) {
							descript=sCur.Description;
						}
						break;
				}
				ehrEncCur.Description=descript;
				if(retval.ContainsKey(ehrEncCur.PatNum)) {
					retval[ehrEncCur.PatNum].Add(ehrEncCur);
				}
				else {
					retval.Add(ehrEncCur.PatNum,new List<EhrCqmEncounter>() { ehrEncCur });
				}
			}
			return retval;
		}

		private static List<EhrCqmPatient> GetEhrPatsFromEncsOrProcs(Dictionary<long,List<EhrCqmEncounter>> dictPatNumListEncounters) {
			return GetEhrPatsFromEncsOrProcs(dictPatNumListEncounters,null);
		}

		///<summary>Get relevant demographic and supplemental patient data required for CQM reporting for each unique patient in the list of eligible encounters in the dictionary of PatNums linked to a list of encounters for each PatNum.</summary>
		private static List<EhrCqmPatient> GetEhrPatsFromEncsOrProcs(Dictionary<long,List<EhrCqmEncounter>> dictPatNumListEncounters,Dictionary<long,List<EhrCqmProc>> dictPatNumListProcs) {
			//get list of distinct PatNums from the keys in the incoming dictionary
			List<long> listPatNums=new List<long>();
			if(dictPatNumListEncounters!=null) {
				listPatNums.AddRange(dictPatNumListEncounters.Keys);
			}
			else if(dictPatNumListProcs!=null) {
				listPatNums.AddRange(dictPatNumListProcs.Keys);
			}			
			List<EhrCqmPatient> retval=new List<EhrCqmPatient>();
			if(listPatNums.Count==0) {
				return retval;
			}
			Patient[] uniquePats=Patients.GetMultPats(listPatNums); 
			//All PayerType codes in ehrcode list are SOP codes
			List<EhrCode> listEhrCodesForPayerTypeOID=EhrCodes.GetForValueSetOIDs(new List<string>() { "2.16.840.1.114222.4.11.3591" },false);
			for(int i=0;i<uniquePats.Length;i++) {
				EhrCqmPatient ehrPatCur=new EhrCqmPatient();
				ehrPatCur.EhrCqmPat=uniquePats[i];
				//we will set all patients to denominator.  The only measure where the list of patients (which is the initial patient population) is not the same as the denominator is the influenza vaccine measure, CMS147v2, and we will set those not in the denominator to false after we determine they are in the IPP but not the denominator
				ehrPatCur.IsDenominator=true;
				List<PatientRace> listPatRaces=PatientRaces.GetForPatient(ehrPatCur.EhrCqmPat.PatNum);
				for(int j=0;j<listPatRaces.Count;j++) {
					if(listPatRaces[j].Race==PatRace.Hispanic || listPatRaces[j].Race==PatRace.NotHispanic) {
						ehrPatCur.Ethnicity=listPatRaces[j];//if race is entered, either one or the other ethnicity
						continue;
					}
					if(listPatRaces[j].Race==PatRace.AmericanIndian
						|| listPatRaces[j].Race==PatRace.AfricanAmerican
						|| listPatRaces[j].Race==PatRace.Asian
						|| listPatRaces[j].Race==PatRace.HawaiiOrPacIsland
						|| listPatRaces[j].Race==PatRace.White
						|| listPatRaces[j].Race==PatRace.Other)
					{
						ehrPatCur.ListPatientRaces.Add(listPatRaces[j]);
					}
					//if not one of these races, not sure what to do with it, cannot report it??
				}
				PayorType payerTypeCur=PayorTypes.GetCurrentType(ehrPatCur.EhrCqmPat.PatNum);
				ehrPatCur.PayorSopCode="";
				ehrPatCur.PayorDescription="";
				ehrPatCur.PayorValueSetOID="";
				if(payerTypeCur!=null) {
					for(int j=0;j<listEhrCodesForPayerTypeOID.Count;j++) {
						if(listEhrCodesForPayerTypeOID[j].CodeValue==payerTypeCur.SopCode) {//add payer information if it is in the value set
							ehrPatCur.PayorSopCode=payerTypeCur.SopCode;
							ehrPatCur.PayorDescription=Sops.GetDescriptionFromCode(payerTypeCur.SopCode);
							ehrPatCur.PayorValueSetOID=listEhrCodesForPayerTypeOID[j].ValueSetOID;
							break;
						}
					}
				}
				retval.Add(ehrPatCur);
			}
			return retval;
		}

		///<summary>Get ehrmeasureevents of type TobaccoAssessment where the event code is in the Tobacco Use Screening value set and the assessment resulted in categorizing the patient as a user or non-user and the screening was within 24 months of the measurement period end date.  Ordered by PatNum, DateTEvent.</summary>
		private static Dictionary<long,List<EhrCqmMeasEvent>> GetTobaccoAssessmentEvents(DateTime dateEnd) {
			string command="SELECT ehrmeasureevent.*,COALESCE(snomed.Description,'') AS Description "
				+"FROM ehrmeasureevent "
				+"LEFT JOIN snomed ON snomed.SnomedCode=ehrmeasureevent.CodeValueResult AND ehrmeasureevent.CodeSystemResult='SNOMEDCT' "
				+"WHERE EventType="+POut.Int((int)EhrMeasureEventType.TobaccoUseAssessed)+" " 
				+"AND "+DbHelper.DateColumn("DateTEvent")+">="+POut.Date(dateEnd)+"-INTERVAL 24 MONTH "
				+"ORDER BY ehrmeasureevent.PatNum,ehrmeasureevent.DateTEvent DESC";
			DataTable tableEvents=Db.GetTable(command);
			Dictionary<string,string> dictEventCodesAndSystems=EhrCodes.GetCodeAndCodeSystem(new List<string>() { "2.16.840.1.113883.3.526.3.1278" },true);//Tobacco Use Screening Value Set
			List<string> listTobaccoStatusOIDs=new List<string>();
			listTobaccoStatusOIDs.Add("2.16.840.1.113883.3.526.3.1170");//Tobacco User Grouping Value Set
			listTobaccoStatusOIDs.Add("2.16.840.1.113883.3.526.3.1189");//Tobacco Non-User Grouping Value Set
			List<EhrCode> listTobaccoStatusCodes=EhrCodes.GetForValueSetOIDs(listTobaccoStatusOIDs,false);
			Dictionary<long,EhrCode> dictEventNumEhrCode=new Dictionary<long,EhrCode>();
			for(int i=tableEvents.Rows.Count-1;i>-1;i--) {
				bool isValidEvent=false;
				if(dictEventCodesAndSystems.ContainsKey(tableEvents.Rows[i]["CodeValueEvent"].ToString())
					&& dictEventCodesAndSystems[tableEvents.Rows[i]["CodeValueEvent"].ToString()]==tableEvents.Rows[i]["CodeSystemEvent"].ToString())
				{
					isValidEvent=true;
				}
				int indexStatus=-1;
				for(int j=0;j<listTobaccoStatusCodes.Count;j++) {
					if(listTobaccoStatusCodes[j].CodeValue==tableEvents.Rows[i]["CodeValueResult"].ToString()
						&& listTobaccoStatusCodes[j].CodeSystem==tableEvents.Rows[i]["CodeSystemResult"].ToString())
					{
						indexStatus=j;
						break;
					}
				}
				if(isValidEvent && indexStatus>-1) {
					dictEventNumEhrCode.Add(PIn.Long(tableEvents.Rows[i]["EhrMeasureEventNum"].ToString()),listTobaccoStatusCodes[indexStatus]);
					continue;
				}
				tableEvents.Rows.RemoveAt(i);
			}
			Dictionary<long,List<EhrCqmMeasEvent>> retval=new Dictionary<long,List<EhrCqmMeasEvent>>();
			for(int i=0;i<tableEvents.Rows.Count;i++) {
				EhrCqmMeasEvent tobaccoAssessCur=new EhrCqmMeasEvent();
				tobaccoAssessCur.EhrCqmMeasEventNum=PIn.Long(tableEvents.Rows[i]["EhrMeasureEventNum"].ToString());
				tobaccoAssessCur.PatNum=PIn.Long(tableEvents.Rows[i]["PatNum"].ToString());
				tobaccoAssessCur.CodeValue=tableEvents.Rows[i]["CodeValueResult"].ToString();
				tobaccoAssessCur.CodeSystemName=tableEvents.Rows[i]["CodeSystemResult"].ToString();
				tobaccoAssessCur.DateTEvent=PIn.DateT(tableEvents.Rows[i]["DateTEvent"].ToString());
				string descript=tableEvents.Rows[i]["Description"].ToString();//if code is not in snomed table we will use description of EhrCode object
				EhrCode ehrCodeCur=dictEventNumEhrCode[tobaccoAssessCur.EhrCqmMeasEventNum];
				tobaccoAssessCur.CodeSystemOID=ehrCodeCur.CodeSystemOID;
				tobaccoAssessCur.ValueSetName=ehrCodeCur.ValueSetName;
				tobaccoAssessCur.ValueSetOID=ehrCodeCur.ValueSetOID;
				//all statuses for tobacco use are SNOMEDCT codes
				if(descript=="") {
					descript=ehrCodeCur.Description;//default to description of EhrCode object
				}
				tobaccoAssessCur.Description=descript;
				if(retval.ContainsKey(tobaccoAssessCur.PatNum)) {
					retval[tobaccoAssessCur.PatNum].Add(tobaccoAssessCur);
				}
				else {
					retval.Add(tobaccoAssessCur.PatNum,new List<EhrCqmMeasEvent>() { tobaccoAssessCur });
				}
			}
			return retval;
		}

		///<summary>Get all data needed for reporting QRDA's for interventions from the supplied command where the code belongs to the value set(s) sent in.  Command orders interventions by patnum, then date entered so the first one found for patient is most recent intervention when looping through table.</summary>
		private static Dictionary<long,List<EhrCqmIntervention>> GetInterventions(string command,List<string> listValueSetOIDs) {
			List<Intervention> listInterventions=Crud.InterventionCrud.SelectMany(command);
			//remove any interventions that are not in the Tobacco Use Cessation Counseling Grouping Value Set
			List<EhrCode> listAllInterventionCodes=EhrCodes.GetForValueSetOIDs(listValueSetOIDs,false);//Tobacco Use Cessation Counseling Grouping Value Set
			Dictionary<long,EhrCode> dictInterventionNumEhrCode=new Dictionary<long,EhrCode>();
			for(int i=listInterventions.Count-1;i>-1;i--) {
				bool isValidIntervention=false;
				for(int j=0;j<listAllInterventionCodes.Count;j++) {
					if(listInterventions[i].CodeValue==listAllInterventionCodes[j].CodeValue
								&& listInterventions[i].CodeSystem==listAllInterventionCodes[j].CodeSystem) {
						isValidIntervention=true;
						dictInterventionNumEhrCode.Add(listInterventions[i].InterventionNum,listAllInterventionCodes[j]);
						break;
					}
				}
				if(!isValidIntervention) {
					listInterventions.RemoveAt(i);
				}
			}
			Dictionary<long,List<EhrCqmIntervention>> retval=new Dictionary<long,List<EhrCqmIntervention>>();
			for(int i=0;i<listInterventions.Count;i++) {
				EhrCqmIntervention interventionCur=new EhrCqmIntervention();
				interventionCur.EhrCqmInterventionNum=listInterventions[i].InterventionNum;
				interventionCur.PatNum=listInterventions[i].PatNum;
				interventionCur.ProvNum=listInterventions[i].ProvNum;
				interventionCur.CodeValue=listInterventions[i].CodeValue;
				interventionCur.CodeSystemName=listInterventions[i].CodeSystem;
				interventionCur.DateEntry=listInterventions[i].DateEntry;
				EhrCode ehrCodeCur=dictInterventionNumEhrCode[listInterventions[i].InterventionNum];
				interventionCur.CodeSystemOID=ehrCodeCur.CodeSystemOID;
				interventionCur.ValueSetName=ehrCodeCur.ValueSetName;
				interventionCur.ValueSetOID=ehrCodeCur.ValueSetOID;
				string descript=ehrCodeCur.Description;//if not in table or not a CPT, ICD9CM, ICD10CM, HCPCS, or SNOMEDCT code, default to EhrCode object description
				switch(listInterventions[i].CodeSystem) {
					case "CPT":
						Cpt cCur=Cpts.GetByCode(listInterventions[i].CodeValue);
						if(cCur!=null) {
							descript=cCur.Description;
						}
						break;
					case "HCPCS":
						Hcpcs hCur=Hcpcses.GetByCode(listInterventions[i].CodeValue);
						if(hCur!=null) {
							descript=hCur.DescriptionShort;
						}
						break;
					case "ICD9CM":
						ICD9 i9Cur=ICD9s.GetByCode(listInterventions[i].CodeValue);
						if(i9Cur!=null) {
							descript=i9Cur.Description;
						}
						break;
					case "ICD10CM":
						Icd10 i10Cur=Icd10s.GetByCode(listInterventions[i].CodeValue);
						if(i10Cur!=null) {
							descript=i10Cur.Description;
						}
						break;
					case "SNOMEDCT":
						Snomed sCur=Snomeds.GetByCode(listInterventions[i].CodeValue);
						if(sCur!=null) {
							descript=sCur.Description;
						}
						break;
				}
				interventionCur.Description=descript;
				if(retval.ContainsKey(interventionCur.PatNum)) {
					retval[interventionCur.PatNum].Add(interventionCur);
				}
				else {
					retval.Add(interventionCur.PatNum,new List<EhrCqmIntervention>() { interventionCur });
				}
			}
			return retval;
		}

		///<summary>Get the medication information for medications where the code belongs to one of the value sets in the supplied list and the medication start date is in the supplied date range.  Ordered by PatNum,DateStart so first found for patient is most recent to make calculation easier.  If there is a PatNote, this is a Medication Order.  If there is no note and there is either no stop date or the stop date is after the measurement period end date, this is an active Medication.</summary>
		private static Dictionary<long,List<EhrCqmMedicationPat>> GetMedPats(List<long> listPatNums,List<string> listValueSetOIDs,DateTime dateStart,DateTime dateEnd) {
			string command="SELECT medicationpat.MedicationPatNum,medicationpat.PatNum,medicationpat.DateStart,medicationpat.DateStop,medicationpat.PatNote,"
				+"(CASE WHEN medication.RxCui IS NULL THEN medicationpat.RxCui ELSE medication.RxCui END) AS RxCui "
				+"FROM medicationpat "
				+"LEFT JOIN medication ON medication.MedicationNum=medicationpat.MedicationNum "
				+"WHERE (medication.RxCui IS NOT NULL OR medicationpat.RxCui>0) "
				+"AND DATE(medicationpat.DateStart) BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" ";
				//not going to check stop date, the measures only specify 'starts before or during' without any reference to whether or not the medication has stopped
				//+"AND (YEAR(medicationpat.DateStop)<1880 OR medicationpat.DateStop>"+POut.Date(dateEnd)+") "//no valid stop date or stop date after measurement period end date
			if(listPatNums!=null && listPatNums.Count>0) {
				command+="AND medicationpat.PatNum IN("+string.Join(",",listPatNums)+") ";
			}
			command+="ORDER BY medicationpat.PatNum,medicationpat.DateStart DESC";
			DataTable tableAllMedPats=Db.GetTable(command);
			List<EhrCode> listEhrCodes=EhrCodes.GetForValueSetOIDs(listValueSetOIDs,false);
			Dictionary<long,EhrCode> dictMedicationPatNumEhrCode=new Dictionary<long,EhrCode>();
			for(int i=tableAllMedPats.Rows.Count-1;i>-1;i--) {
				bool isValidMedication=false;
				for(int j=0;j<listEhrCodes.Count;j++) {
					if(tableAllMedPats.Rows[i]["RxCui"].ToString()==listEhrCodes[j].CodeValue) {
						dictMedicationPatNumEhrCode.Add(PIn.Long(tableAllMedPats.Rows[i]["MedicationPatNum"].ToString()),listEhrCodes[j]);
						isValidMedication=true;
						break;
					}
				}
				if(!isValidMedication) {
					tableAllMedPats.Rows.RemoveAt(i);
				}
			}
			Dictionary<long,List<EhrCqmMedicationPat>> retval=new Dictionary<long,List<EhrCqmMedicationPat>>();
			for(int i=0;i<tableAllMedPats.Rows.Count;i++) {
				EhrCqmMedicationPat ehrMedPatCur=new EhrCqmMedicationPat();
				ehrMedPatCur.EhrCqmMedicationPatNum=PIn.Long(tableAllMedPats.Rows[i]["MedicationPatNum"].ToString());
				ehrMedPatCur.PatNum=PIn.Long(tableAllMedPats.Rows[i]["PatNum"].ToString());
				ehrMedPatCur.PatNote=tableAllMedPats.Rows[i]["PatNote"].ToString();
				ehrMedPatCur.RxCui=PIn.Long(tableAllMedPats.Rows[i]["RxCui"].ToString());
				ehrMedPatCur.DateStart=PIn.Date(tableAllMedPats.Rows[i]["DateStart"].ToString());
				ehrMedPatCur.DateStop=PIn.Date(tableAllMedPats.Rows[i]["DateStop"].ToString());
				EhrCode ehrCodeCur=dictMedicationPatNumEhrCode[ehrMedPatCur.EhrCqmMedicationPatNum];
				ehrMedPatCur.CodeSystemName=ehrCodeCur.CodeSystem;
				ehrMedPatCur.CodeSystemOID=ehrCodeCur.CodeSystemOID;
				ehrMedPatCur.ValueSetName=ehrCodeCur.ValueSetName;
				ehrMedPatCur.ValueSetOID=ehrCodeCur.ValueSetOID;
				string descript=ehrCodeCur.Description;
				RxNorm rCur=RxNorms.GetByRxCUI(ehrMedPatCur.RxCui.ToString());
				if(rCur!=null) {
					descript=rCur.Description;
				}
				ehrMedPatCur.Description=descript;//description either from rxnorm table or, if not in table, default to EhrCode object description
				if(retval.ContainsKey(ehrMedPatCur.PatNum)) {
					retval[ehrMedPatCur.PatNum].Add(ehrMedPatCur);
				}
				else {
					retval.Add(ehrMedPatCur.PatNum,new List<EhrCqmMedicationPat>() { ehrMedPatCur });
				}
			}
			return retval;
		}

		///<summary>Get all NotPerformed items that belong to one of the ValueSetOIDs in listItemOIDs, with valid reasons that belong to one of the ValueSetOIDs in listReasonOIDs, that were entered between dateStart and dateStop.  For QRDA reporting, the resulting list must include the item not performed, a code for 'reason', and the code for the specific reason.  Example: If not administering a flu vaccine, you would have the code not being done (like CVX 141 "Influenza, seasonal, injectable"), the code for 'reason' (like SNOMEDCT 281000124100 "Patient reason for exclusion from performance measure (observable entity)"), and the code for the specific reason (like SNOMEDCT 105480006 "Refusal of treatment by patient (situation)").  Not fun.</summary>
		private static Dictionary<long,List<EhrCqmNotPerf>> GetNotPerformeds(List<string> listValueSetOIDs,List<string> listReasonOIDs,DateTime dateStart,DateTime dateEnd) {			
			//Reasons not done come from these value sets for our 9 CQMs:
			//Medical Reason Grouping Value Set 2.16.840.1.113883.3.526.3.1007
			//Patient Reason Grouping Value Set 2.16.840.1.113883.3.526.3.1008
			//System Reason Grouping Value Set 2.16.840.1.113883.3.526.3.1009
			//Patient Reason Refused SNOMED-CT Value Set 2.16.840.1.113883.3.600.1.1503 (this is a sub-set of Patient Reason ...1008 above)
			//Medical or Other reason not done SNOMED-CT Value Set 2.16.840.1.113883.3.600.1.1502 (this is a sub-set of Medical Reason ...1007 above)
			string command="SELECT ehrnotperformed.*, "
				+"COALESCE(snomed.Description,loinc.NameLongCommon,cpt.Description,cvx.Description,'') AS Description, "
				+"COALESCE(sReason.Description,'') AS DescriptionReason "
				+"FROM ehrnotperformed "
				+"LEFT JOIN cpt ON cpt.CptCode=ehrnotperformed.CodeValue AND ehrnotperformed.CodeSystem='CPT' "
				+"LEFT JOIN cvx ON cvx.CvxCode=ehrnotperformed.CodeValue AND ehrnotperformed.CodeSystem='CVX' "
				+"LEFT JOIN loinc ON loinc.LoincCode=ehrnotperformed.CodeValue AND ehrnotperformed.CodeSystem='LOINC' "
				+"LEFT JOIN snomed ON snomed.SnomedCode=ehrnotperformed.CodeValue AND ehrnotperformed.CodeSystem='SNOMEDCT' "
				+"LEFT JOIN snomed sReason ON sReason.SnomedCode=ehrnotperformed.CodeValueReason AND ehrnotperformed.CodeSystemReason='SNOMEDCT' "
				+"WHERE ehrnotperformed.DateEntry BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" "
				+"GROUP BY ehrnotperformed.EhrNotPerformedNum "//just in case a code was in one of the code system tables more than once, should never happen
				+"ORDER BY ehrnotperformed.PatNum,ehrnotperformed.DateEntry DESC";
			DataTable tableNotPerfs=Db.GetTable(command);
			List<EhrCode> listItems=EhrCodes.GetForValueSetOIDs(listValueSetOIDs,false);
			List<EhrCode> listReasons=EhrCodes.GetForValueSetOIDs(listReasonOIDs,false);
			Dictionary<long,EhrCode> dictItemNumEhrCode=new Dictionary<long,EhrCode>();
			Dictionary<long,EhrCode> dictReasonNumEhrCode=new Dictionary<long,EhrCode>();
			//loop through items and remove if not in valid value set or if reason is not in valid reason value set
			//link the item to the EhrCode object for both the item code and the reason code using dictionaries for retrieving required data for QRDA reports
			for(int i=tableNotPerfs.Rows.Count-1;i>-1;i--) {
				bool isValidItem=false;
				for(int j=0;j<listItems.Count;j++) {
					if(tableNotPerfs.Rows[i]["CodeValue"].ToString()==listItems[j].CodeValue
						&& tableNotPerfs.Rows[i]["CodeSystem"].ToString()==listItems[j].CodeSystem)
					{
						dictItemNumEhrCode.Add(PIn.Long(tableNotPerfs.Rows[i]["EhrNotPerformedNum"].ToString()),listItems[j]);
						isValidItem=true;
						break;
					}
				}
				if(!isValidItem) {
					tableNotPerfs.Rows.RemoveAt(i);
					continue;
				}
				isValidItem=false;
				for(int j=0;j<listReasons.Count;j++) {
					if(tableNotPerfs.Rows[i]["CodeValueReason"].ToString()==listReasons[j].CodeValue
						&& tableNotPerfs.Rows[i]["CodeSystemReason"].ToString()==listReasons[j].CodeSystem)
					{
						dictReasonNumEhrCode.Add(PIn.Long(tableNotPerfs.Rows[i]["EhrNotPerformedNum"].ToString()),listReasons[j]);
						isValidItem=true;
						break;
					}
				}
				if(!isValidItem) {
					tableNotPerfs.Rows.RemoveAt(i);
				}
			}
			Dictionary<long,List<EhrCqmNotPerf>> retval=new Dictionary<long,List<EhrCqmNotPerf>>();
			for(int i=0;i<tableNotPerfs.Rows.Count;i++) {
				EhrCqmNotPerf ehrNotPerfCur=new EhrCqmNotPerf();
				ehrNotPerfCur.EhrCqmNotPerfNum=PIn.Long(tableNotPerfs.Rows[i]["EhrNotPerformedNum"].ToString());
				ehrNotPerfCur.PatNum=PIn.Long(tableNotPerfs.Rows[i]["PatNum"].ToString());
				ehrNotPerfCur.CodeValue=tableNotPerfs.Rows[i]["CodeValue"].ToString();
				ehrNotPerfCur.CodeSystemName=tableNotPerfs.Rows[i]["CodeSystem"].ToString();
				ehrNotPerfCur.CodeValueReason=tableNotPerfs.Rows[i]["CodeValueReason"].ToString();
				ehrNotPerfCur.CodeSystemNameReason=tableNotPerfs.Rows[i]["CodeSystemReason"].ToString();
				ehrNotPerfCur.DateEntry=PIn.Date(tableNotPerfs.Rows[i]["DateEntry"].ToString());
				EhrCode itemEhrCode=dictItemNumEhrCode[ehrNotPerfCur.EhrCqmNotPerfNum];
				ehrNotPerfCur.CodeSystemOID=itemEhrCode.CodeSystemOID;
				ehrNotPerfCur.ValueSetName=itemEhrCode.ValueSetName;
				ehrNotPerfCur.ValueSetOID=itemEhrCode.ValueSetOID;
				EhrCode reasonEhrCode=dictReasonNumEhrCode[ehrNotPerfCur.EhrCqmNotPerfNum];
				ehrNotPerfCur.CodeSystemOIDReason=reasonEhrCode.CodeSystemOID;
				ehrNotPerfCur.ValueSetNameReason=reasonEhrCode.ValueSetName;
				ehrNotPerfCur.ValueSetOIDReason=reasonEhrCode.ValueSetOID;
				string descript=tableNotPerfs.Rows[i]["Description"].ToString();
				if(descript=="") {//just in case not found in table, will default to description of EhrCode object
					descript=itemEhrCode.Description;
				}
				ehrNotPerfCur.Description=descript;
				string reasonDescript=tableNotPerfs.Rows[i]["DescriptionReason"].ToString();
				if(reasonDescript=="") {//just in case not found in table, will default to description of EhrCode object
					reasonDescript=reasonEhrCode.Description;
				}
				ehrNotPerfCur.DescriptionReason=reasonDescript;
				if(retval.ContainsKey(ehrNotPerfCur.PatNum)) {
					retval[ehrNotPerfCur.PatNum].Add(ehrNotPerfCur);
				}
				else {
					retval.Add(ehrNotPerfCur.PatNum,new List<EhrCqmNotPerf>() { ehrNotPerfCur });
				}
			}
			return retval;
		}

		///<summary>Get all problems that started before or during the date range, that have a code that belong to the value sets in listProbOIDs, and that either have no stop date or the stop date is after dateStart.</summary>
		private static Dictionary<long,List<EhrCqmProblem>> GetProblems(List<long> listPatNums,List<string> listValueSetOIDs,DateTime dateStart,DateTime dateEnd) {
			string command="SELECT disease.DiseaseNum,disease.PatNum,disease.DateStart,disease.DateStop,"
				+"diseasedef.SnomedCode,diseasedef.ICD9Code,diseasedef.Icd10Code,"
				+"COALESCE(snomed.Description,icd9.Description,icd10.Description,diseasedef.DiseaseName) AS Description "
				+"FROM disease INNER JOIN diseasedef ON disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
				+"LEFT JOIN snomed ON snomed.SnomedCode=diseasedef.SnomedCode "
				+"LEFT JOIN icd9 ON icd9.ICD9Code=diseasedef.ICD9Code "
				+"LEFT JOIN icd10 ON icd10.Icd10Code=diseasedef.Icd10Code "
				+"WHERE disease.DateStart<="+POut.Date(dateEnd)+" "
				+"AND (YEAR(disease.DateStop)<1880 OR disease.DateStop>"+POut.Date(dateStart)+") ";
			if(listPatNums!=null && listPatNums.Count>0) {
				command+="AND disease.PatNum IN("+string.Join(",",listPatNums)+") ";
			}
			command+="ORDER BY disease.PatNum,disease.DateStart DESC";
			DataTable tableAllProbs=Db.GetTable(command);
			List<EhrCode> listValidProbs=EhrCodes.GetForValueSetOIDs(listValueSetOIDs,false);
			Dictionary<long,EhrCode> dictDiseaseNumEhrCode=new Dictionary<long,EhrCode>();
			for(int i=tableAllProbs.Rows.Count-1;i>-1;i--) {
				bool isValid=false;
				for(int j=0;j<listValidProbs.Count;j++) {//all problems are either SNOMED, ICD9, or ICD10 codes
					if((listValidProbs[j].CodeSystem=="SNOMEDCT" && tableAllProbs.Rows[i]["SnomedCode"].ToString()==listValidProbs[j].CodeValue)
						|| (listValidProbs[j].CodeSystem=="ICD9CM" && tableAllProbs.Rows[i]["ICD9Code"].ToString()==listValidProbs[j].CodeValue)
						|| (listValidProbs[j].CodeSystem=="ICD10CM" && tableAllProbs.Rows[i]["Icd10Code"].ToString()==listValidProbs[j].CodeValue)) {
						dictDiseaseNumEhrCode.Add(PIn.Long(tableAllProbs.Rows[i]["DiseaseNum"].ToString()),listValidProbs[j]);//link the problem to the EhrCode object for retrieving information
						isValid=true;
						break;
					}
				}
				if(!isValid) {
					tableAllProbs.Rows.RemoveAt(i);//remove problems that are not in the value set
				}
			}
			Dictionary<long,List<EhrCqmProblem>> retval=new Dictionary<long,List<EhrCqmProblem>>();
			for(int i=0;i<tableAllProbs.Rows.Count;i++) {
				EhrCqmProblem ehrProblemCur=new EhrCqmProblem();
				ehrProblemCur.EhrCqmProblemNum=PIn.Long(tableAllProbs.Rows[i]["DiseaseNum"].ToString());
				ehrProblemCur.PatNum=PIn.Long(tableAllProbs.Rows[i]["PatNum"].ToString());
				ehrProblemCur.DateStart=PIn.Date(tableAllProbs.Rows[i]["DateStart"].ToString());
				ehrProblemCur.DateStop=PIn.Date(tableAllProbs.Rows[i]["DateStop"].ToString());
				ehrProblemCur.Description=tableAllProbs.Rows[i]["Description"].ToString();
				EhrCode ehrCodeCur=dictDiseaseNumEhrCode[ehrProblemCur.EhrCqmProblemNum];
				ehrProblemCur.CodeValue=ehrCodeCur.CodeValue;//use the code value from the ehrcode object because diseasedef can have an ICD9CM, ICD10CM, and SNOMEDCT code, and the codes do not have to be for the same thing, so use the code that belongs to the ValueSetOID that makes it valid for this measure
				ehrProblemCur.CodeSystemName=ehrCodeCur.CodeSystem;
				ehrProblemCur.CodeSystemOID=ehrCodeCur.CodeSystemOID;
				ehrProblemCur.ValueSetName=ehrCodeCur.ValueSetName;
				ehrProblemCur.ValueSetOID=ehrCodeCur.ValueSetOID;
				if(retval.ContainsKey(ehrProblemCur.PatNum)) {
					retval[ehrProblemCur.PatNum].Add(ehrProblemCur);
				}
				else {
					retval.Add(ehrProblemCur.PatNum,new List<EhrCqmProblem>() { ehrProblemCur });
				}
			}
			return retval;
		}

		///<summary>Get all medication documented procedures that happened in the date range that belong to the value set OIDs.  These 'procedures' are actually in the ehrmeasureevent table and can only possibly be one code (restricted by value set OID), SNOMEDCT - 428191000124101 - Documentation of current medications (procedure).  Ordered by PatNum, DateTEvent DESC for making CQM calc easier, most recent 'procedure' will be the first one found for the patient in list.</summary>
		private static Dictionary<long,List<EhrCqmMeasEvent>> GetMedDocumentedProcs(List<string> listValueSetOIDs,DateTime dateStart,DateTime dateEnd) {
			string command="SELECT ehrmeasureevent.*,COALESCE(snomed.Description,'') AS Description "
				+"FROM ehrmeasureevent "
				+"LEFT JOIN snomed ON snomed.SnomedCode=ehrmeasureevent.CodeValueEvent AND ehrmeasureevent.CodeSystemEvent='SNOMEDCT' "
				+"WHERE EventType="+POut.Int((int)EhrMeasureEventType.CurrentMedsDocumented)+" "
				+"AND DATE(DateTEvent) BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" "
				+"ORDER BY PatNum,DateTEvent DESC";
			DataTable tableEvents=Db.GetTable(command);
			List<EhrCode> listEhrCodes=EhrCodes.GetForValueSetOIDs(listValueSetOIDs,false);
			Dictionary<long,EhrCode> dictEhrMeasureEventNumEhrCode=new Dictionary<long,EhrCode>();
			//remove 'procs' from table of ehrmeasureevents if not in the value set OIDs in listEhrCodes
			for(int i=tableEvents.Rows.Count-1;i>-1;i--) {
				bool isValid=false;
				for(int j=0;j<listEhrCodes.Count;j++) {//currently this can only be one code, SNOMEDCT - 428191000124101, but we will treat it like a list in case that changes
					if(tableEvents.Rows[i]["CodeValueEvent"].ToString()==listEhrCodes[j].CodeValue && tableEvents.Rows[i]["CodeSystemEvent"].ToString()==listEhrCodes[j].CodeSystem) {
						dictEhrMeasureEventNumEhrCode.Add(PIn.Long(tableEvents.Rows[i]["EhrMeasureEventNum"].ToString()),listEhrCodes[j]);
						isValid=true;
						break;
					}
				}
				if(!isValid) {
					tableEvents.Rows.RemoveAt(i);
				}
			}
			Dictionary<long,List<EhrCqmMeasEvent>> retval=new Dictionary<long,List<EhrCqmMeasEvent>>();
			for(int i=0;i<tableEvents.Rows.Count;i++) {
				EhrCqmMeasEvent ehrProcCur=new EhrCqmMeasEvent();
				ehrProcCur.EhrCqmMeasEventNum=PIn.Long(tableEvents.Rows[i]["EhrMeasureEventNum"].ToString());
				ehrProcCur.PatNum=PIn.Long(tableEvents.Rows[i]["PatNum"].ToString());
				ehrProcCur.CodeValue=tableEvents.Rows[i]["CodeValueEvent"].ToString();
				ehrProcCur.CodeSystemName=tableEvents.Rows[i]["CodeSystemEvent"].ToString();
				ehrProcCur.DateTEvent=PIn.DateT(tableEvents.Rows[i]["DateTEvent"].ToString());
				EhrCode ehrCodeCur=dictEhrMeasureEventNumEhrCode[ehrProcCur.EhrCqmMeasEventNum];
				ehrProcCur.CodeSystemOID=ehrCodeCur.CodeSystemOID;
				ehrProcCur.ValueSetName=ehrCodeCur.ValueSetName;
				ehrProcCur.ValueSetOID=ehrCodeCur.ValueSetOID;
				ehrProcCur.Description=ehrCodeCur.Description;
				Snomed sCur=Snomeds.GetByCode(ehrCodeCur.CodeValue);
				if(sCur!=null) {
					ehrProcCur.Description=sCur.Description;
				}
				if(retval.ContainsKey(ehrProcCur.PatNum)) {
					retval[ehrProcCur.PatNum].Add(ehrProcCur);
				}
				else {
					retval.Add(ehrProcCur.PatNum,new List<EhrCqmMeasEvent>() { ehrProcCur });
				}
			}
			return retval;
		}

		///<summary>Used in measure 69, BMI Screening and Follow-up and measure 155, Weight Assessment and Counseling for Nutrition and Physical Activity for Children and Adolescents.  Get all vitalsigns with DateTaken in the date range with valid height and weight.  Only one code available for a BMI exam - LOINC 39156-5 Body mass index (BMI) [Ratio].  Any vitalsign object with valid height and weight is assumed to be a LOINC 39156-5, not stored explicitly.  Results ordered by PatNum then DateTaken DESC, so MOST RECENT for each patient will be the first one in the list for that pat (i.e. dict[PatNum][0]).</summary>
		private static Dictionary<long,List<EhrCqmVitalsign>> GetVitalsignsForBMI(List<long> listPatNums,DateTime dateStart,DateTime dateEnd) {
			string command="SELECT * FROM vitalsign "
				+"WHERE DATE(DateTaken) BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" "
				+"AND vitalsign.Height>0 AND vitalsign.Weight>0 ";
			if(listPatNums!=null && listPatNums.Count>0) {
				command+="AND vitalsign.PatNum IN("+string.Join(",",listPatNums)+") ";
			}
			command+="ORDER BY vitalsign.PatNum,vitalsign.DateTaken DESC";
			List<Vitalsign> listVitalsigns=Crud.VitalsignCrud.SelectMany(command);
			//every row in the table has valid height and weight, so they are all in the value set for BMI LOINC Value LOINC Value Set - 2.16.840.1.113883.3.600.1.681 which is one code: LOINC 39156-5 Body mass index (BMI) [Ratio].
			Dictionary<long,List<EhrCqmVitalsign>> retval=new Dictionary<long,List<EhrCqmVitalsign>>();
			for(int i=0;i<listVitalsigns.Count;i++) {
				EhrCqmVitalsign ehrVitalsignCur=new EhrCqmVitalsign();
				ehrVitalsignCur.EhrCqmVitalsignNum=listVitalsigns[i].VitalsignNum;
				ehrVitalsignCur.PatNum=listVitalsigns[i].PatNum;
				float h=listVitalsigns[i].Height;
				float w=listVitalsigns[i].Weight;
				ehrVitalsignCur.BMI=Math.Round((decimal)((w*703)/(h*h)),2,MidpointRounding.AwayFromZero);
				ehrVitalsignCur.HeightExamCode=listVitalsigns[i].HeightExamCode;
				Loinc lCur=Loincs.GetByCode(ehrVitalsignCur.HeightExamCode);
				if(lCur!=null) {
					ehrVitalsignCur.HeightExamDescript=lCur.NameLongCommon;
				}
				ehrVitalsignCur.WeightExamCode=listVitalsigns[i].WeightExamCode;
				lCur=Loincs.GetByCode(ehrVitalsignCur.WeightExamCode);
				if(lCur!=null) {
					ehrVitalsignCur.WeightExamDescript=lCur.NameLongCommon;
				}
				ehrVitalsignCur.BMIPercentile=listVitalsigns[i].BMIPercentile;
				ehrVitalsignCur.BMIExamCode=listVitalsigns[i].BMIExamCode;//percentile code
				lCur=Loincs.GetByCode(ehrVitalsignCur.BMIExamCode);
				if(lCur!=null) {
					ehrVitalsignCur.BMIPercentileDescript=lCur.NameLongCommon;
				}
				ehrVitalsignCur.DateTaken=listVitalsigns[i].DateTaken;
				if(retval.ContainsKey(ehrVitalsignCur.PatNum)) {
					retval[ehrVitalsignCur.PatNum].Add(ehrVitalsignCur);
				}
				else {
					retval.Add(ehrVitalsignCur.PatNum,new List<EhrCqmVitalsign>() { ehrVitalsignCur });
				}
			}
			return retval;
		}

		///<summary>Used in measure 165, Controlling High Blood Pressure.  Get all vitalsigns with DateTaken in the date range with valid BP.  Only one code available for Systolic BP, LOINC 8480-6, and one code for Diastolic, LOINC 8462-4.  Results ordered by PatNum then DateTaken DESC, so MOST RECENT for each patient will be the first one in the list for that pat (i.e. dict[PatNum][0]).</summary>
		private static Dictionary<long,List<EhrCqmVitalsign>> GetVitalsignsForBP(List<long> listPatNums,DateTime dateStart,DateTime dateEnd) {
			string command="SELECT * FROM vitalsign "
				+"WHERE DATE(DateTaken) BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" "
				+"AND vitalsign.BpSystolic>0 AND vitalsign.BpDiastolic>0 ";
			if(listPatNums!=null && listPatNums.Count>0) {
				command+="AND vitalsign.PatNum IN("+string.Join(",",listPatNums)+") ";
			}
			command+="ORDER BY vitalsign.PatNum,vitalsign.DateTaken DESC";
			List<Vitalsign> listVitalsigns=Crud.VitalsignCrud.SelectMany(command);
			//No need to get EhrCode objects from dll, every vitalsign exam with valid Systolic BP is assumed LOINC 8480-6 and valid Diastolic BP is assumed LOINC 8462-4
			Dictionary<long,List<EhrCqmVitalsign>> retval=new Dictionary<long,List<EhrCqmVitalsign>>();
			for(int i=0;i<listVitalsigns.Count;i++) {
				EhrCqmVitalsign ehrVitalsignCur=new EhrCqmVitalsign();
				ehrVitalsignCur.EhrCqmVitalsignNum=listVitalsigns[i].VitalsignNum;
				ehrVitalsignCur.PatNum=listVitalsigns[i].PatNum;
				ehrVitalsignCur.BpSystolic=listVitalsigns[i].BpSystolic;//LOINC 8480-6
				ehrVitalsignCur.BpDiastolic=listVitalsigns[i].BpDiastolic;//LOINC 8462-4
				ehrVitalsignCur.DateTaken=listVitalsigns[i].DateTaken;
				if(retval.ContainsKey(ehrVitalsignCur.PatNum)) {
					retval[ehrVitalsignCur.PatNum].Add(ehrVitalsignCur);
				}
				else {
					retval.Add(ehrVitalsignCur.PatNum,new List<EhrCqmVitalsign>() { ehrVitalsignCur });
				}
			}
			return retval;
		}

		///<summary>Get all procedures with ProcDate in the date range and ProcCode in the list of codes that belong to one of the value sets in listValueSetOIDs.</summary>
		private static Dictionary<long,List<EhrCqmProc>> GetProcs(List<long> listPatNums,List<string> listValueSetOIDs,DateTime dateStart,DateTime dateEnd) {
			string command="SELECT procedurelog.ProcNum,procedurelog.PatNum,procedurelog.ProvNum,procedurelog.ProcDate,"
				+"procedurecode.ProcCode,procedurecode.Descript FROM procedurelog "
				+"INNER JOIN procedurecode ON procedurelog.CodeNum=procedurecode.CodeNum "
				+"WHERE procedurelog.ProcStatus=2 "
				+"AND procedurelog.ProcDate BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" ";
			if(listPatNums!=null && listPatNums.Count>0) {
				command+="AND procedurelog.PatNum IN ("+string.Join(",",listPatNums)+")";
			}
			command+="ORDER BY procedurelog.PatNum,procedurelog.ProcDate DESC";
			DataTable tableAllProcs=Db.GetTable(command);
			List<EhrCode> listValidProcs=EhrCodes.GetForValueSetOIDs(listValueSetOIDs,false);
			Dictionary<long,EhrCode> dictProcNumEhrCode=new Dictionary<long,EhrCode>();
			for(int i=tableAllProcs.Rows.Count-1;i>-1;i--) {
				bool isValid=false;
				for(int j=0;j<listValidProcs.Count;j++) {
					if(tableAllProcs.Rows[i]["ProcCode"].ToString()==listValidProcs[j].CodeValue) {
						isValid=true;
						dictProcNumEhrCode.Add(PIn.Long(tableAllProcs.Rows[i]["ProcNum"].ToString()),listValidProcs[j]);
						break;
					}
				}
				if(!isValid) {
					tableAllProcs.Rows.RemoveAt(i);//remove procs that are not in the list of valid codes
				}
			}
			Dictionary<long,List<EhrCqmProc>> retval=new Dictionary<long,List<EhrCqmProc>>();
			for(int i=0;i<tableAllProcs.Rows.Count;i++) {
				EhrCqmProc ehrProcCur=new EhrCqmProc();
				ehrProcCur.EhrCqmProcNum=PIn.Long(tableAllProcs.Rows[i]["ProcNum"].ToString());
				ehrProcCur.PatNum=PIn.Long(tableAllProcs.Rows[i]["PatNum"].ToString());
				ehrProcCur.ProvNum=PIn.Long(tableAllProcs.Rows[i]["ProvNum"].ToString());
				ehrProcCur.ProcDate=PIn.Date(tableAllProcs.Rows[i]["ProcDate"].ToString());
				ehrProcCur.ProcCode=tableAllProcs.Rows[i]["ProcCode"].ToString();
				ehrProcCur.Description=tableAllProcs.Rows[i]["Descript"].ToString();
				EhrCode ehrCodeCur=dictProcNumEhrCode[ehrProcCur.EhrCqmProcNum];
				ehrProcCur.CodeSystemName=ehrCodeCur.CodeSystem;
				ehrProcCur.CodeSystemOID=ehrCodeCur.CodeSystemOID;
				ehrProcCur.ValueSetName=ehrCodeCur.ValueSetName;
				ehrProcCur.ValueSetOID=ehrCodeCur.ValueSetOID;
				if(retval.ContainsKey(ehrProcCur.PatNum)) {
					retval[ehrProcCur.PatNum].Add(ehrProcCur);
				}
				else {
					retval.Add(ehrProcCur.PatNum,new List<EhrCqmProc>() { ehrProcCur });
				}
			}
			return retval;
		}

		///<summary>Returns all vaccinepat objects for pneumonia and influenza CQMs.  These are basically just medicationpat objects, so we will use the same object with two optional fields to identify them as vaccinepats instead of medicationpats.</summary>
		private static Dictionary<long,List<EhrCqmMedicationPat>> GetVaccines(List<long> listPatNums,List<string> listValueSetOIDs,DateTime dateStart,DateTime dateEnd) {
			string command="SELECT vaccinepat.VaccinePatNum,vaccinepat.PatNum,vaccinepat.DateTimeStart,vaccinepat.DateTimeEnd,vaccinedef.CVXCode "
				+"FROM vaccinepat "
				+"INNER JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
				+"WHERE DATE(vaccinepat.DateTimeStart) BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" "
				+"AND vaccinepat.NotGiven=0 AND vaccinepat.CompletionStatus=0 ";//NotGiven=false and CompletionStatus=0 (complete)
			if(listPatNums!=null && listPatNums.Count>0) {
				command+="AND vaccinepat.PatNum IN("+string.Join(",",listPatNums)+") ";
			}
			command+="ORDER BY vaccinepat.PatNum,vaccinepat.DateTimeStart DESC";
			DataTable tableAllVaccinePats=Db.GetTable(command);
			List<EhrCode> listEhrCodes=EhrCodes.GetForValueSetOIDs(listValueSetOIDs,false);//this list will only contain one code, CVX 33 - pneumococcal polysaccharide vaccine, 23 valent
			Dictionary<long,EhrCode> dictVaccinePatNumEhrCode=new Dictionary<long,EhrCode>();
			for(int i=tableAllVaccinePats.Rows.Count-1;i>-1;i--) {
				bool isValidVaccine=false;
				for(int j=0;j<listEhrCodes.Count;j++) {
					if(tableAllVaccinePats.Rows[i]["CVXCode"].ToString()==listEhrCodes[j].CodeValue) {
						dictVaccinePatNumEhrCode.Add(PIn.Long(tableAllVaccinePats.Rows[i]["VaccinePatNum"].ToString()),listEhrCodes[j]);
						isValidVaccine=true;
						break;
					}
				}
				if(!isValidVaccine) {
					tableAllVaccinePats.Rows.RemoveAt(i);
				}
			}
			Dictionary<long,List<EhrCqmMedicationPat>> retval=new Dictionary<long,List<EhrCqmMedicationPat>>();
			for(int i=0;i<tableAllVaccinePats.Rows.Count;i++) {
				EhrCqmMedicationPat ehrVacPatCur=new EhrCqmMedicationPat();
				ehrVacPatCur.EhrCqmVaccinePatNum=PIn.Long(tableAllVaccinePats.Rows[i]["VaccinePatNum"].ToString());
				ehrVacPatCur.PatNum=PIn.Long(tableAllVaccinePats.Rows[i]["PatNum"].ToString());
				ehrVacPatCur.CVXCode=tableAllVaccinePats.Rows[i]["CVXCode"].ToString();
				ehrVacPatCur.DateStart=PIn.DateT(tableAllVaccinePats.Rows[i]["DateTimeStart"].ToString());
				ehrVacPatCur.DateStop=PIn.DateT(tableAllVaccinePats.Rows[i]["DateTimeEnd"].ToString());
				EhrCode ehrCodeCur=dictVaccinePatNumEhrCode[ehrVacPatCur.EhrCqmVaccinePatNum];
				ehrVacPatCur.CodeSystemName=ehrCodeCur.CodeSystem;
				ehrVacPatCur.CodeSystemOID=ehrCodeCur.CodeSystemOID;
				ehrVacPatCur.ValueSetName=ehrCodeCur.ValueSetName;
				ehrVacPatCur.ValueSetOID=ehrCodeCur.ValueSetOID;
				string descript=ehrCodeCur.Description;
				Cvx cvxCur=Cvxs.GetByCode(ehrVacPatCur.CVXCode);
				if(cvxCur!=null) {
					descript=cvxCur.Description;
				}
				ehrVacPatCur.Description=descript;//description either from cvx table or, if not in table, default to EhrCode object description
				if(retval.ContainsKey(ehrVacPatCur.PatNum)) {
					retval[ehrVacPatCur.PatNum].Add(ehrVacPatCur);
				}
				else {
					retval.Add(ehrVacPatCur.PatNum,new List<EhrCqmMedicationPat>() { ehrVacPatCur });
				}
			}
			return retval;
		}

		///<summary>Using the data in alldata, determine if the patients in alldata.ListEhrPats are in the 'Numerator', 'Exclusion', or 'Exception' category for this measure and enter an explanation if applicable.  All of the patients in ListEhrPats are the initial patient population (almost always equal to the Denominator).</summary>
		private static void ClassifyPatients(QualityMeasure alldata,QualityType2014 qtype) {
			switch(qtype) {
				#region MedicationsEntered
				case QualityType2014.MedicationsEntered:
					//alldata.ListEhrPats: All unique patients with necessary reporting data.  This is the initial patient population (Denominator).
					//alldata.DictPatNumListEncounters:  PatNums linked to a list of all encounters from the eligble value sets for patients 18 or over at the start of the measurement period.
					//alldata.DictPatNumListMeasureEvents: All Current Meds Documented 'procedures' that occurred during the measurement period
					//alldata.DictPatNumListNotPerfs: All Current Meds Documented events not performed with valid reasons that occurred during the measurement period
					//No exclusions for this measure
					//Strategy: For each patient in ListEhrPats, loop through the encounters in dictionary DictPatNumListEncounters; key=PatNum, value=List<EhrCqmEncounter>
					//For each encounter, loop through the measure events in DictPatNumListMeasureEvents and try to locate a meds documented 'proc' on the same date.
					//If one exists for any encounter, Numerator.
					//If no procedure exists, look for a not performed item for Exception
					//Otherwise unclassified
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						bool isCategorized=false;
						//for each encounter for the patient, look for ehrmeasureevent for procedure
						List<EhrCqmEncounter> listEncsCur=new List<EhrCqmEncounter>();
						if(alldata.DictPatNumListEncounters.ContainsKey(patNumCur)) {
							listEncsCur=alldata.DictPatNumListEncounters[patNumCur];
						}
						//loop through EhrCqmMeasEvents for the patient to find one for the same date as the encounter
						List<EhrCqmMeasEvent> listMeasEventsCur=new List<EhrCqmMeasEvent>();
						if(alldata.DictPatNumListMeasureEvents.ContainsKey(patNumCur)) {
							listMeasEventsCur=alldata.DictPatNumListMeasureEvents[patNumCur];
						}
						for(int j=0;j<listEncsCur.Count;j++) {
							for(int k=0;k<listMeasEventsCur.Count;k++) {
								if(listEncsCur[j].DateEncounter.Date==listMeasEventsCur[k].DateTEvent.Date) {
									//measure event with same date as encounter, numerator
									alldata.ListEhrPats[i].IsNumerator=true;
									alldata.ListEhrPats[i].Explanation="Encounter on "+listEncsCur[j].DateEncounter.ToShortDateString()+" with a current medications documented procedure on the same date.";
									isCategorized=true;
									break;
								}
							}
							if(isCategorized) {
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						List<EhrCqmNotPerf> listNotPerfsCur=new List<EhrCqmNotPerf>();
						if(alldata.DictPatNumListNotPerfs.ContainsKey(patNumCur)) {
							listNotPerfsCur=alldata.DictPatNumListNotPerfs[patNumCur];
						}
						//No procedure for current meds documented on any encounter, check for not performed item on one of the encounter dates
						for(int j=0;j<listEncsCur.Count;j++) {
							for(int k=0;k<listNotPerfsCur.Count;k++) {
								if(listEncsCur[j].DateEncounter.Date==listNotPerfsCur[k].DateEntry.Date) {
									alldata.ListEhrPats[i].IsException=true;
									alldata.ListEhrPats[i].Explanation="Encounter on "+listEncsCur[j].DateEncounter.ToShortDateString()+" with a current medications documented procedure not done for a valid reason on the same date.";
									isCategorized=true;
									break;
								}
							}
							if(isCategorized) {
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						alldata.ListEhrPats[i].Explanation="Eligible encounter(s) for this measure occurred with no current medications documented procedure recorded on the same date.";
					}
					break;
				#endregion
				#region WeightAdultAndOver65
				case QualityType2014.WeightAdult:
				case QualityType2014.WeightOver65:
					//Strategy: All patients in alldata.ListEhrPats are the initial patient population for this measure
					//Denominator - Inital Patient Population
					//Exclusions - Pregnant during any of the measurement period
					//Find the most recent vitalsign exam date such that there is a valid encounter within the 6 months after the exam
					//If there is more than one encounter within that 6 months, one of them must meet the numerator criteria for the patient to be in the numerator
					//If that most recent exam found the patient with BMI >= 23 kg/m2 and < 30 kg/m2 for Over65, >= 18.5 and < 25 for Adult, 'Numerator'
					//If the most recent exam found the patient with BMI < 23 kg/m2 or >= 30 kg/m2 for Over65, < 18.5 or >= 25 for Adult, check for Intervention or Medication Order
					//The intervention/medication order must be within 6 months of one of the encounters, which are within 6 months of the exam
					//If order exists for any encounter, 'Numerator'
					//If no intervention/medication order for any of the encounters for the exam, not classified
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						bool isCategorized=false;
						//first apply pregnancy exclusion
						List<EhrCqmProblem> listProbsCur=new List<EhrCqmProblem>();
						if(alldata.DictPatNumListProblems.ContainsKey(patNumCur)) {
							listProbsCur=alldata.DictPatNumListProblems[patNumCur];
						}
						for(int j=0;j<listProbsCur.Count;j++) {
							if(listProbsCur[j].ValueSetOID=="2.16.840.1.113883.3.600.1.1623") {//Pregnancy Dx Grouping Value Set
								alldata.ListEhrPats[i].IsExclusion=true;
								alldata.ListEhrPats[i].Explanation="The patient had a pregnancy diagnosis that started on "+listProbsCur[j].DateStart.ToShortDateString()+" that is either still active or ended after the start of the measurement period.";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						//get vital sign exams that took place in the measurement period for the patient.  Ordered by DateTaken DESC, so index 0 will hold the most recent exam
						List<EhrCqmVitalsign> listVitalsignsCur=new List<EhrCqmVitalsign>();
						if(alldata.DictPatNumListVitalsigns.ContainsKey(patNumCur)) {
							listVitalsignsCur=alldata.DictPatNumListVitalsigns[patNumCur];
						}
						if(listVitalsignsCur.Count==0) {//no vitalsign exams within 6 months of start of measurement period, but encounters exist or they would not be in the IPP
							alldata.ListEhrPats[i].Explanation="Valid encounters exist, but there are no BMI vital sign exams within 6 months of the measurement period.";
							continue;
						}
						//get eligible enounters that took place in the measurement period for the patient.  Ordered by DateEncounter DESC, so index 0 will hold the most recent.
						List<EhrCqmEncounter> listEncsCur=new List<EhrCqmEncounter>();
						if(alldata.DictPatNumListEncounters.ContainsKey(patNumCur)) {
							listEncsCur=alldata.DictPatNumListEncounters[patNumCur];
						}
						//Find the most recent exam date such that there is an eligible encounter on that date or within the 6 months after the exam date
						DateTime dateMostRecentExam=DateTime.MinValue;
						int indexMostRecentExam=-1;
						for(int j=0;j<listVitalsignsCur.Count;j++) {
							if(dateMostRecentExam.Date>listVitalsignsCur[j].DateTaken.Date) {//most recent exam date already set and set to a date more recent than current, continue
								continue;
							}
							for(int k=0;k<listEncsCur.Count;k++) {
								if(listVitalsignsCur[j].DateTaken.Date<=listEncsCur[k].DateEncounter.Date
									&& listVitalsignsCur[j].DateTaken.Date>=listEncsCur[k].DateEncounter.AddMonths(-6).Date
									&& listVitalsignsCur[j].DateTaken.Date>=dateMostRecentExam.Date)
								{
									dateMostRecentExam=listVitalsignsCur[j].DateTaken;
									indexMostRecentExam=j;
									break;
								}
							}
						}
						//If there are no exams that occurred within 6 months of an eligible encounter, not classified
						if(indexMostRecentExam==-1) {
							alldata.ListEhrPats[i].Explanation="Valid encounters exist and BMI vital sign exams exist, but no BMI exam date is within 6 months of a valid encounter in the measurement period.";
							continue;
						}
						//If WeightOver65 measure AND the most recent BMI was in the allowed range of >=23 and <30 kg/m2, then no intervention required, patient in 'Numerator'
						if(qtype==QualityType2014.WeightOver65 && listVitalsignsCur[indexMostRecentExam].BMI>=23m && listVitalsignsCur[indexMostRecentExam].BMI<30m) {//'m' converts to decimal
							alldata.ListEhrPats[i].IsNumerator=true;
							alldata.ListEhrPats[i].Explanation="BMI in normal range.  Most recent BMI exam date: "+listVitalsignsCur[indexMostRecentExam].DateTaken.ToShortDateString()+".  BMI result: "+listVitalsignsCur[indexMostRecentExam].BMI.ToString();
							continue;
						}
						//If WeightAdult measure AND the most recent BMI was in the allowed range of >=23 and <30 kg/m2, then no intervention required, patient in 'Numerator'
						if(qtype==QualityType2014.WeightAdult && listVitalsignsCur[indexMostRecentExam].BMI>=18.5m && listVitalsignsCur[indexMostRecentExam].BMI<25m) {//'m' converts to decimal
							alldata.ListEhrPats[i].IsNumerator=true;
							alldata.ListEhrPats[i].Explanation="BMI in normal range.  Most recent BMI exam date: "+listVitalsignsCur[indexMostRecentExam].DateTaken.ToShortDateString()+".  BMI result: "+listVitalsignsCur[indexMostRecentExam].BMI.ToString();
							continue;
						}
						//BMI must be out of range, for each encounter of which this exam is within the previous 6 months, look for an intervention/medication order that took place within the 6 months prior to the encounter
						//If an encounter and intervention/medication order exist for this encounter, 'Numerator'
						List<EhrCqmIntervention> listInterventionsCur=new List<EhrCqmIntervention>();
						if(alldata.DictPatNumListInterventions.ContainsKey(patNumCur)) {
							listInterventionsCur=alldata.DictPatNumListInterventions[patNumCur];
						}
						List<EhrCqmMedicationPat> listMedPatsCur=new List<EhrCqmMedicationPat>();
						if(alldata.DictPatNumListMedPats.ContainsKey(patNumCur)) {
							listMedPatsCur=alldata.DictPatNumListMedPats[patNumCur];
						}
						for(int j=0;j<listEncsCur.Count;j++) {
							//if encounter is before exam or more than 6 months after the exam, move to next encounter
							if(listEncsCur[j].DateEncounter.Date<listVitalsignsCur[indexMostRecentExam].DateTaken.Date
								|| listEncsCur[j].DateEncounter.Date>listVitalsignsCur[indexMostRecentExam].DateTaken.AddMonths(6).Date)
							{
								continue;
							}
							for(int k=0;k<listInterventionsCur.Count;k++) {
								//if intervention order is within 6 months of the encounter, classify as 'Numerator'
								if(listInterventionsCur[k].DateEntry.Date<=listEncsCur[j].DateEncounter.Date
									&& listInterventionsCur[k].DateEntry.Date>=listEncsCur[j].DateEncounter.AddMonths(-6).Date)
								{
									//encounter within 6 months of the most recent exam and intervention within 6 months of encounter, 'Numerator'
									alldata.ListEhrPats[i].IsNumerator=true;
									alldata.ListEhrPats[i].Explanation="Most recent exam on "+listVitalsignsCur[indexMostRecentExam].DateTaken.ToShortDateString()+" with encounter on "
										+listEncsCur[j].DateEncounter.ToShortDateString()+" resulted in a BMI of "+listVitalsignsCur[indexMostRecentExam].BMI.ToString()+" "
										+"and intervention on "+listInterventionsCur[k].DateEntry.ToShortDateString()+".";
									isCategorized=true;
									break;
								}
							}
							if(isCategorized) {
								break;
							}
							for(int k=0;k<listMedPatsCur.Count;k++) {
								//if medication order is within 6 months of the encounter, classify as 'Numerator'
								if(listMedPatsCur[k].DateStart.Date<=listEncsCur[j].DateEncounter.Date
									&& listMedPatsCur[k].DateStart.Date>=listEncsCur[j].DateEncounter.AddMonths(-6).Date)
								{
									alldata.ListEhrPats[i].IsNumerator=true;
									alldata.ListEhrPats[i].Explanation="Most recent exam on "+listVitalsignsCur[indexMostRecentExam].DateTaken.ToShortDateString()+" with encounter on "
										+listEncsCur[j].DateEncounter.ToShortDateString()+" resulted in a BMI of "+listVitalsignsCur[indexMostRecentExam].BMI.ToString()+" "
										+"and medication order on "+listMedPatsCur[k].DateStart.ToShortDateString()+".";
									isCategorized=true;
									break;
								}
							}
							if(isCategorized) {
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						//If we get here, the most recent BMI exam that had eligible encounters on the exam date or within the 6 months after that date resulted in a BMI outside of normal range
						//but there were no intervention/medication orders entered
						alldata.ListEhrPats[i].Explanation="Most recent exam on "+listVitalsignsCur[indexMostRecentExam].DateTaken.ToShortDateString()+" with BMI of "
							+listVitalsignsCur[indexMostRecentExam].BMI.ToString()+" had valid encounters within 6 months of the exam but no valid intervention or medication order.";
					}
					break;
				#endregion
				#region CariesPrevent
				case QualityType2014.CariesPrevent:
				case QualityType2014.CariesPrevent_1:
				case QualityType2014.CariesPrevent_2:
				case QualityType2014.CariesPrevent_3:
					//Strategy: alldata.ListEhrPats will be the initial patient population, already restricted by appropriate age for all, _1, _2 and _3 stratification
					//No Exclusions, denominator is initial patient population
					//alldata.DictPatNumListProcs will hold the eligible procs in the measurement period for the patients in the initial patient population
					//if Dict contains PatNum, 'Numerator'
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						if(alldata.DictPatNumListProcs.ContainsKey(patNumCur)) {
							alldata.ListEhrPats[i].IsNumerator=true;
							alldata.ListEhrPats[i].Explanation="This patient had an eligible encounter in the measurement period and a flouride varnish application procedure with code "
								+alldata.DictPatNumListProcs[patNumCur][0].ProcCode+" on "+alldata.DictPatNumListProcs[patNumCur][0].ProcDate.ToShortDateString()+".";
							continue;
						}
						alldata.ListEhrPats[i].Explanation="This patient had an eligible encounter in the measurement period, but did not have a flouride varnish application procedure with a valid code in the measurement period.";
					}
					break;
				#endregion
				#region ChildCaries
				case QualityType2014.ChildCaries:
					//This measure is misleading, A lower score indicates better quality, so a high percentage means they have a lot of kids with cavities or dental decay
					//Strategy: alldata.ListEhrPats will be the initial patient population, already restricted by age >=0 and < 20 at start of measurement period
					//No Exclusions, denominator is initial patient population
					//alldata.DictPatNumListProblems will hold all eligible problems for dental caries with a start date before the end of the measurement period and either no end date or an end date after the start of the measurement period
					//if patient is in ListEhrPats and Dict contains PatNum, 'Numerator'
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						if(alldata.DictPatNumListProblems.ContainsKey(patNumCur)) {
							alldata.ListEhrPats[i].IsNumerator=true;
							alldata.ListEhrPats[i].Explanation="This patient had an eligible encounter in the measurement period and had an active diagnosis of caries with code "
								+alldata.DictPatNumListProblems[patNumCur][0].CodeValue+" that started on "+alldata.DictPatNumListProblems[patNumCur][0].DateStart.ToShortDateString()+".";
							continue;
						}
						alldata.ListEhrPats[i].Explanation="This patient had an eligible encounter in the measurement period, but did not have an active diagnosis of caries with a valid code in the measurement period.";
					}
					break;
				#endregion
				#region Pneumonia
				case QualityType2014.Pneumonia:
					//Strategy: alldata.ListEhrPats is IPP and denominator, non exclusions
					//Numerator: if medicationpat, procedure, or problem exists, numerator
					//Otherwise: not categorized
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						bool isCategorized=false;
						//first see if medicationpat exists
						List<EhrCqmMedicationPat> listMedPatsCur=new List<EhrCqmMedicationPat>();
						if(alldata.DictPatNumListMedPats.ContainsKey(patNumCur)) {
							listMedPatsCur=alldata.DictPatNumListMedPats[patNumCur];
						}
						for(int j=0;j<listMedPatsCur.Count;j++) {
							if(listMedPatsCur[j].ValueSetOID=="2.16.840.1.113883.3.464.1003.110.12.1027") {//Pneumococcal Vaccine Grouping Value Set
								alldata.ListEhrPats[i].IsNumerator=true;
								alldata.ListEhrPats[i].Explanation="The patient had a Pneumococcal medication administered on "+listMedPatsCur[j].DateStart.ToShortDateString()+".";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						List<EhrCqmProc> listProcsCur=new List<EhrCqmProc>();
						if(alldata.DictPatNumListProcs.ContainsKey(patNumCur)) {
							listProcsCur=alldata.DictPatNumListProcs[patNumCur];
						}
						for(int j=0;j<listProcsCur.Count;j++) {
							if(listProcsCur[j].ValueSetOID=="2.16.840.1.113883.3.464.1003.110.12.1034") {//Pneumococcal Vaccine Administered Grouping Value Set
								alldata.ListEhrPats[i].IsNumerator=true;
								alldata.ListEhrPats[i].Explanation="The patient had a Pneumococcal administered procedure on "+listProcsCur[j].ProcDate.ToShortDateString()+".";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						List<EhrCqmProblem> listProbsCur=new List<EhrCqmProblem>();
						if(alldata.DictPatNumListProblems.ContainsKey(patNumCur)) {
							listProbsCur=alldata.DictPatNumListProblems[patNumCur];
						}
						for(int j=0;j<listProbsCur.Count;j++) {
							if(listProbsCur[j].ValueSetOID=="2.16.840.1.113883.3.464.1003.110.12.1028") {//History of Pneumococcal Vaccine Grouping Value Set
								alldata.ListEhrPats[i].IsNumerator=true;
								alldata.ListEhrPats[i].Explanation="The patient has a history of Pneumococcal vaccination on "+listProbsCur[j].DateStart.ToShortDateString()+".";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						alldata.ListEhrPats[i].Explanation="The patient had eligible encounters in the date range but no Pneumococcal vaccine was administered and no history of having had the vaccine was recorded.";
					}
					break;
				#endregion
				#region TobaccoCessation
				case QualityType2014.TobaccoCessation:
					//alldata.ListEhrPats: All unique patients with necessary reporting data.  This is the initial patient population (Denominator).
					//alldata.ListEncounters:  All encounters from the eligble value sets for patients 18 or over at the start of the measurement period.
					//alldata.ListMeasureEvents: All tobacco use assessment ehrmeasureevents with recorded status that occurred within 24 months of the end of the measurement period
					//alldata.ListInterventions: All eligible interventions performed within 24 months of the end of the measurement period
					//alldata.ListMedPats: All eligible medications, active or ordered, that started within 24 months of the end of the measurement period
					//alldata.ListNotPerfs: All tobacco assessment events not performed with valid reasons that occurred during the measurement period
					//alldata.ListProblems: All eligible problems that were active during any of the measurement period.
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						//No exclusions for this measure
						//Strategy: Find the most recent tobacco assessment for the patient.
						//If Non-User, this patient is in the Numerator.
						//If User, check for intervention, medication active or order, if one exists, then Numerator.
						//If User and no intervention/med, or no assessment at all, then check notperformed and problems for possible Exception.
						//Finally, if none of the above, then only fill the Explanation column with the appropriate text.
						#region Get most recent assessment date and value set OID for patient
						DateTime mostRecentAssessDate=DateTime.MinValue;
						string mostRecentAssessValueSetOID="";
						List<EhrCqmMeasEvent> listMeasEventsCur=new List<EhrCqmMeasEvent>();
						if(alldata.DictPatNumListMeasureEvents.ContainsKey(patNumCur)) {
							listMeasEventsCur=alldata.DictPatNumListMeasureEvents[patNumCur];
						}
						for(int j=0;j<listMeasEventsCur.Count;j++) {
							if(listMeasEventsCur[j].DateTEvent>mostRecentAssessDate) {
								mostRecentAssessDate=listMeasEventsCur[j].DateTEvent;
								mostRecentAssessValueSetOID=listMeasEventsCur[j].ValueSetOID;
							}
						}
						#endregion
						//if most recently (all assessments in our list are in the last 24 months prior to the measurement period end date) assessed Non-User, then Numerator
						#region Most recently assessed Non-User
						if(mostRecentAssessDate>DateTime.MinValue && mostRecentAssessValueSetOID=="2.16.840.1.113883.3.526.3.1189") {//Non-User
							alldata.ListEhrPats[i].IsNumerator=true;
							alldata.ListEhrPats[i].Explanation="Patient categorized as Non-User on "+mostRecentAssessDate.Date.ToShortDateString();
							continue;
						}
						#endregion
						//if most recently assessed User, check for intervention or medication active/order
						#region Most recently assessed User
						if(mostRecentAssessDate>DateTime.MinValue && mostRecentAssessValueSetOID=="2.16.840.1.113883.3.526.3.1170") {//User
							//check for intervention.  If in the list, it is already guaranteed to be valid and in the date range and order by PatNum and DateEntry, so first one found will be the most recent for the patient
							List<EhrCqmIntervention> listIntervensCur=new List<EhrCqmIntervention>();
							if(alldata.DictPatNumListInterventions.ContainsKey(patNumCur)) {
								listIntervensCur=alldata.DictPatNumListInterventions[patNumCur];
							}
							if(listIntervensCur.Count>0) {
								alldata.ListEhrPats[i].IsNumerator=true;
								alldata.ListEhrPats[i].Explanation="Patient categorized as User with an intervention on "+listIntervensCur[0].DateEntry.ToShortDateString();
								continue;
							}
							//check for medication.  If there is one in the list, it is guaranteed to be valid for tobacco cessation and in the date range and ordered by PatNum and DateStart, so first found is most recent
							List<EhrCqmMedicationPat> listMedPatsCur=new List<EhrCqmMedicationPat>();
							if(alldata.DictPatNumListMedPats.ContainsKey(patNumCur)) {
								listMedPatsCur=alldata.DictPatNumListMedPats[patNumCur];
							}
							if(listMedPatsCur.Count>0) {
								alldata.ListEhrPats[i].IsNumerator=true;
								string explain="Patient categorized as User with medication ";
								string activeOrOrder="active";
								if(listMedPatsCur[0].PatNote!="") {//PatNote means Medication Order, otherwise Medication Active
									activeOrOrder="order";
								}
								alldata.ListEhrPats[i].Explanation=explain+activeOrOrder+" with start date "+listMedPatsCur[0].DateStart.ToShortDateString();
								continue;
							}
						}
						#endregion
						//if we get here, there is either no valid assessment date in the date range or the patient was most recently categorized as User with no intervention or medication
						//check for valid NotPerformed item, for exception
						#region Check for not performed
						//alldata.ListNotPerf is ordered by PatNum, DateEntry DESC so first one found is the most recent for the patient
						List<EhrCqmNotPerf> listNotPerfsCur=new List<EhrCqmNotPerf>();
						if(alldata.DictPatNumListNotPerfs.ContainsKey(patNumCur)) {
							listNotPerfsCur=alldata.DictPatNumListNotPerfs[patNumCur];
						}
						if(listNotPerfsCur.Count>0) {
							alldata.ListEhrPats[i].IsException=true;
							alldata.ListEhrPats[i].Explanation="Assessment not done for valid medical reason on "+listNotPerfsCur[0].DateEntry.ToShortDateString();
							continue;
						}
						#endregion
						//last, check for limited life expectancy, for exception
						#region Check for active diagnosis of limited life expectancy
						List<EhrCqmProblem> listProbsCur=new List<EhrCqmProblem>();
						if(alldata.DictPatNumListProblems.ContainsKey(patNumCur)) {
							listProbsCur=alldata.DictPatNumListProblems[patNumCur];
						}
						if(listProbsCur.Count>0) {
							alldata.ListEhrPats[i].IsException=true;
							string explain="Assessment not done due to an active limited life expectancy diagnosis";
							if(listProbsCur[0].DateStart.Year>1880) {
								explain+=" with start date "+listProbsCur[0].DateStart.ToShortDateString();
							}
							alldata.ListEhrPats[i].Explanation=explain;
							continue;
						}
						#endregion
						//still not categorized, put note in explanation, could be due to no assessment in date range or categorized User with no intervention/medication
						#region Not met explanation
						if(mostRecentAssessDate==DateTime.MinValue) {
							alldata.ListEhrPats[i].Explanation="No tobacco use assessment entered";
						}
						else if(mostRecentAssessValueSetOID=="2.16.840.1.113883.3.526.3.1170") {//User
							alldata.ListEhrPats[i].Explanation="Patient categorized as User on "+mostRecentAssessDate.ToShortDateString()+" without an intervention";
						}
						#endregion
					}
					break;
				#endregion
//TODO:
				#region Influenza
				case QualityType2014.Influenza:
					//Only classify as numerator, exclusion, or exception if IsDenominator
					break;
				#endregion
				#region WeightChild_X_1
				//All the _1 measures will calculate BMI, height, and weight exams the same, the ListEhrPats will be limited by the age groups already
				case QualityType2014.WeightChild_1_1:
				case QualityType2014.WeightChild_2_1:
				case QualityType2014.WeightChild_3_1:
					//Strategy: alldata.ListEhrPats will be initial patient population
					//First check for pregnancy diagnosis, alldata.DictPatNumListProblems will hold all of the pregnancy problems that were active during any of the measurement period.
					//Exclusion if active pregnancy dx exists
					//alldata.DictPatNumListVitalsigns holds all vitalsign exams in the date range with a valid height and weight value.
					//Make sure the vitalsign.BMIExamCode, vitalsign.HeightExamCode, and vitalsign.WeightExamCode are in the allowed value sets
					//If the codes in BMI, height and weight exam code fields are in the value sets, 'Numerator'
					//No exceptions
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						bool isCategorized=false;
						//first apply pregnancy exclusion
						List<EhrCqmProblem> listProbsCur=new List<EhrCqmProblem>();
						if(alldata.DictPatNumListProblems.ContainsKey(patNumCur)) {
							listProbsCur=alldata.DictPatNumListProblems[patNumCur];
						}
						for(int j=0;j<listProbsCur.Count;j++) {
							if(listProbsCur[j].ValueSetOID=="2.16.840.1.113883.3.526.3.378") {//Pregnancy Grouping Value Set
								alldata.ListEhrPats[i].IsExclusion=true;
								alldata.ListEhrPats[i].Explanation="The patient had a pregnancy diagnosis that started on "+listProbsCur[j].DateStart.ToShortDateString()+" that is either still active or ended after the start of the measurement period.";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						//get vital sign exams that took place in the measurement period for the patient.  Ordered by DateTaken DESC, so index 0 will hold the most recent exam
						List<EhrCqmVitalsign> listVitalsignsCur=new List<EhrCqmVitalsign>();
						if(alldata.DictPatNumListVitalsigns.ContainsKey(patNumCur)) {
							listVitalsignsCur=alldata.DictPatNumListVitalsigns[patNumCur];
						}
						if(listVitalsignsCur.Count==0) {//no vitalsign exams within 6 months of start of measurement period, but encounters exist or they would not be in the IPP
							alldata.ListEhrPats[i].Explanation="There is a valid encounter for this patient, but there are no BMI vital sign exams within 6 months of the measurement period.";
							continue;
						}
						//all three sets of exam codes come from the LOINC table only
						List<EhrCode> listBMIExamCodes=EhrCodes.GetForValueSetOIDs(new List<string>() { "2.16.840.1.113883.3.464.1003.121.12.1012" },false);//BMI percentile Grouping Value Set
						List<EhrCode> listHeightExamCodes=EhrCodes.GetForValueSetOIDs(new List<string>() { "2.16.840.1.113883.3.464.1003.121.12.1014" },false);//Height Grouping Value Set
						List<EhrCode> listWeightExamCodes=EhrCodes.GetForValueSetOIDs(new List<string>() { "2.16.840.1.113883.3.464.1003.121.12.1015" },false);//Weight Grouping Value Set
						//loop through vitalsign exams looking for valid height, weight, and BMI exam codes (percentile) based on value set OIDs
						for(int j=0;j<listVitalsignsCur.Count;j++) {
							bool isBMIExamCodeValid=false;
							bool isHeightExamCodeValid=false;
							bool isWeightExamCodeValid=false;
							for(int k=0;k<listBMIExamCodes.Count;k++) {
								if(listVitalsignsCur[j].BMIExamCode!=listBMIExamCodes[k].CodeValue) {
									continue;
								}
								if(listVitalsignsCur[j].BMIPercentile==-1) {//-1 if not in age range or if BMI percentile was not calculated correctly
									continue;
								}
								isBMIExamCodeValid=true;
								break;
							}
							for(int k=0;k<listHeightExamCodes.Count;k++) {
								if(listVitalsignsCur[j].HeightExamCode!=listHeightExamCodes[k].CodeValue) {
									continue;
								}
								isHeightExamCodeValid=true;
								break;
							}
							for(int k=0;k<listWeightExamCodes.Count;k++) {
								if(listVitalsignsCur[j].WeightExamCode!=listWeightExamCodes[k].CodeValue) {
									continue;
								}
								isWeightExamCodeValid=true;
								break;
							}
							if(isBMIExamCodeValid && isHeightExamCodeValid && isWeightExamCodeValid) {
								alldata.ListEhrPats[i].IsNumerator=true;
								alldata.ListEhrPats[i].Explanation="The vitalsign exam on "+listVitalsignsCur[j].DateTaken.ToShortDateString()+" has valid height, weight, and BMI Percentile code.";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						alldata.ListEhrPats[i].Explanation="There is a valid encounter for this patient, but no valid vitalsign exam recording height, weight, and BMI in the measurement period.";
					}
					break;
				#endregion
				#region WeightChild_X_2 and WeightChild_X_3
				//All _2 and _3 measures will calculate the Nutrition/Physical Activity counseling interventions the same, already limited by the appropriate age groups
				case QualityType2014.WeightChild_1_2:
				case QualityType2014.WeightChild_2_2:
				case QualityType2014.WeightChild_3_2:
				case QualityType2014.WeightChild_1_3:
				case QualityType2014.WeightChild_2_3:
				case QualityType2014.WeightChild_3_3:
					//Strategy: alldata.ListEhrPats will be initial patient population
					//First check for pregnancy diagnosis, alldata.DictPatNumListProblems will hold all of the pregnancy problems that were active during any of the measurement period.
					//Exclusion if active pregnancy dx exists
					//alldata.DictPatNumListInterventions will hold all of the nutrition and physical activity counseling interventions that apply
					//if intervention exists, 'Numerator'
					string interventionType="physical activity";//used for generating explanation string, 1_3, 2_3, and 3_3 are phys activity
					string valueSetCur="2.16.840.1.113883.3.464.1003.118.12.1035";//Counseling for Physical Activity Grouping Value Set
					if(qtype==QualityType2014.WeightChild_1_2 || qtype==QualityType2014.WeightChild_2_2 || qtype==QualityType2014.WeightChild_3_2) {
						interventionType="nutrition";
						valueSetCur="2.16.840.1.113883.3.464.1003.195.12.1003";//Counseling for Nutrition Grouping Value Set
					}
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						bool isCategorized=false;
						//first apply pregnancy exclusion
						List<EhrCqmProblem> listProbsCur=new List<EhrCqmProblem>();
						if(alldata.DictPatNumListProblems.ContainsKey(patNumCur)) {
							listProbsCur=alldata.DictPatNumListProblems[patNumCur];
						}
						for(int j=0;j<listProbsCur.Count;j++) {
							if(listProbsCur[j].ValueSetOID=="2.16.840.1.113883.3.526.3.378") {//Pregnancy Grouping Value Set
								alldata.ListEhrPats[i].IsExclusion=true;
								alldata.ListEhrPats[i].Explanation="The patient had a pregnancy diagnosis that started on "+listProbsCur[j].DateStart.ToShortDateString()+" that is either still active or ended after the start of the measurement period.";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						List<EhrCqmIntervention> listIntervenCur=new List<EhrCqmIntervention>();
						if(alldata.DictPatNumListInterventions.ContainsKey(patNumCur)) {
							listIntervenCur=alldata.DictPatNumListInterventions[patNumCur];
						}
						//loop through interventions, if one for the current value set (could be nutrition or physical activity) exists, 'Numerator'
						for(int j=0;j<listIntervenCur.Count;j++) {
							if(listIntervenCur[j].ValueSetOID==valueSetCur) {
								alldata.ListEhrPats[i].IsNumerator=true;
								alldata.ListEhrPats[i].Explanation="This patient was counseled for "+interventionType+" on "+listIntervenCur[0].DateEntry.ToShortDateString()+".";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						//eligible encounters exist, but no intervention for nutrition/physical activity counseling, not met
						alldata.ListEhrPats[i].Explanation="There is a valid encounter for this patient, but no "+interventionType+" counseling intervention in the measurement period.";
					}
					break;
				#endregion
				#region BloodPressureManage
				case QualityType2014.BloodPressureManage:
					//Strategy: alldata.ListEhrPats will be the initial patient population, DictPatNumListEncounters holds all encounters for each patient in ListEhrPats
					//Denominator: initial patient population
					//Exclusions: alldata.DictPatNumListProblems holds all diagnoses for the patient that started before the measurement period end date
					//and either have no stop date or the stop date is after the period start date
						//DictPatNumListProblems will also hold the hypertension dx's, so only exclude if problem is pregnancy, end stage renal disease, or chronic kidney disease stage 5
						//DictPatNumListInterventions holds all interventions that exclude the patient from this measure in the date range, if intervention exists, exclude
						//DictPatNumListProcs holds all procedures that exclude the patient form this measure in the date range, if proc exists, exclude
						//DictPatNumListEncounters will have any ESRD Monthly Outpatient Services encounters in the list, if one of these encounters exist, exclude
					//Numerator: During most recent encounter (that is not an ESRD Monthly... encounter from exclusion ValueSetOID), BPDiastolic<90 mmHg, BPSystolic<140 mmHg
					for(int i=0;i<alldata.ListEhrPats.Count;i++) {
						long patNumCur=alldata.ListEhrPats[i].EhrCqmPat.PatNum;
						bool isCategorized=false;
						//first apply exclusion diagnoses, problem list will hold hypertension dx's, so have to check for valid exlusion dx
						List<EhrCqmProblem> listProbsCur=new List<EhrCqmProblem>();
						if(alldata.DictPatNumListProblems.ContainsKey(patNumCur)) {
							listProbsCur=alldata.DictPatNumListProblems[patNumCur];
						}
						for(int j=0;j<listProbsCur.Count;j++) {
							if(listProbsCur[j].ValueSetOID=="2.16.840.1.113883.3.526.3.378") {//Pregnancy Grouping Value Set
								alldata.ListEhrPats[i].IsExclusion=true;
								alldata.ListEhrPats[i].Explanation="The patient had a pregnancy diagnosis that started on "+listProbsCur[j].DateStart.ToShortDateString()+" that is either still active or ended after the start of the measurement period.";
								isCategorized=true;
								break;
							}
							if(listProbsCur[j].ValueSetOID=="2.16.840.1.113883.3.526.3.353") {//End Stage Renal Disease Grouping Value Set
								alldata.ListEhrPats[i].IsExclusion=true;
								alldata.ListEhrPats[i].Explanation="The patient had an end stage renal disease diagnosis that started on "+listProbsCur[j].DateStart.ToShortDateString()+" that is either still active or ended after the start of the measurement period.";
								isCategorized=true;
								break;
							}
							if(listProbsCur[j].ValueSetOID=="2.16.840.1.113883.3.526.3.1002") {//Chronic Kidney Disease, Stage 5 Grouping Value Set
								alldata.ListEhrPats[i].IsExclusion=true;
								alldata.ListEhrPats[i].Explanation="The patient had a chronic kidney disease, stage 5 diagnosis that started on "+listProbsCur[j].DateStart.ToShortDateString()+" that is either still active or ended after the start of the measurement period.";
								isCategorized=true;
								break;
							}
						}
						if(isCategorized) {
							continue;
						}
						//next apply exclusion interventions, only valid interventions will be in the list
						if(alldata.DictPatNumListInterventions.ContainsKey(patNumCur)) {
							alldata.ListEhrPats[i].IsExclusion=true;
							alldata.ListEhrPats[i].Explanation="The patient had an intervention for dialysis education or other services related to dialysis on "+alldata.DictPatNumListInterventions[patNumCur][0].DateEntry.ToShortDateString()+".";
							continue;
						}
						//next apply exclusion procedures, only valid procs will be in the list
						if(alldata.DictPatNumListProcs.ContainsKey(patNumCur)) {
							alldata.ListEhrPats[i].IsExclusion=true;
							alldata.ListEhrPats[i].Explanation="The patient had a procedure performed for vascular access for dialysis, kidney transplant, or dialysis services on "+alldata.DictPatNumListProcs[patNumCur][0].ProcDate.ToShortDateString()+".";
							continue;
						}
						//finally apply exclusion encounters, have to loop through encounters and look for any ESRD Monthly Outpatient Services encounters
						//while looping through encounters, get the most recent encounter that is not an ESRD encounter
						DateTime dateMostRecentEnc=DateTime.MinValue;
						List<EhrCqmEncounter> listEncountersCur=alldata.DictPatNumListEncounters[patNumCur];
						for(int j=0;j<listEncountersCur.Count;j++) {
							if(listEncountersCur[j].ValueSetOID=="2.16.840.1.113883.3.464.1003.109.12.1014") {//ESRD Monthly Outpatient Services Grouping Value Set
								alldata.ListEhrPats[i].IsExclusion=true;
								alldata.ListEhrPats[i].Explanation="The patient had an end stage renal disease monthly outpatient services encounter on "+alldata.DictPatNumListEncounters[patNumCur][j].DateEncounter.ToShortDateString()+".";
								isCategorized=true;
								continue;
							}
							if(listEncountersCur[j].DateEncounter.Date>dateMostRecentEnc.Date) {
								dateMostRecentEnc=listEncountersCur[j].DateEncounter;
							}
						}
						if(isCategorized) {
							continue;
						}
						//patient not excluded, try to match numerator criteria
						//using dateMostRecentEnc set above, try to find a vitalsign exam on the same date with recorded Diastolic BP < 90 mmHg and Systolic BP < 140 mmHg
						List<EhrCqmVitalsign> listVitalsignsCur=new List<EhrCqmVitalsign>();
						if(alldata.DictPatNumListVitalsigns.ContainsKey(patNumCur)) {
							listVitalsignsCur=alldata.DictPatNumListVitalsigns[patNumCur];
						}
						int recentSystolic=0;
						int recentDiastolic=0;
						for(int j=0;j<listVitalsignsCur.Count;j++) {
							if(listVitalsignsCur[j].DateTaken.Date!=dateMostRecentEnc.Date) {
								continue;
							}
							if(j==0) {
								recentSystolic=listVitalsignsCur[j].BpSystolic;
								recentDiastolic=listVitalsignsCur[j].BpDiastolic;
							}
							if(listVitalsignsCur[j].BpSystolic<recentSystolic) {
								recentSystolic=listVitalsignsCur[j].BpSystolic;
							}
							if(listVitalsignsCur[j].BpDiastolic<recentDiastolic) {
								recentDiastolic=listVitalsignsCur[j].BpDiastolic;
							}
						}
						if(recentSystolic>0 && recentSystolic<90 && recentDiastolic>0 && recentDiastolic<140) {
							alldata.ListEhrPats[i].IsNumerator=true;
							alldata.ListEhrPats[i].Explanation="The patient had a vitalsign exam on "+dateMostRecentEnc.ToShortDateString()
								+" with systolic blood pressure "+recentSystolic.ToString()+" mmHg and diastolic blood pressure "+recentDiastolic.ToString()+" mmHg.";
							continue;
						}
						//no exceptions, if not in the numerator, patient had a hypertension dx within 6 months of the measurement period start date or any time before the measurement period that did not end before the measurement period start date, was not excluded, and either did not have a vitalsign exam recording BP during their most recent encounter or their most recent encounter with vitalsign exam recorded BP above the allowed range (diastolic<140, systolic<90)
						alldata.ListEhrPats[i].Explanation="The patient's most recent qualifying encounter on "+dateMostRecentEnc.ToShortDateString();
						if(recentSystolic==0 || recentDiastolic==0) {//BP was not recorded during most recent encounter
							alldata.ListEhrPats[i].Explanation+=" did not have a corresponding vitalsign exam recording systolic and diastolic blood pressure with the same date.";
						}
						else {
							alldata.ListEhrPats[i].Explanation+=" had a corresponding vitalsign exam with systolic blood pressure "
								+recentSystolic.ToString()+" mmHg and diastolic blood pressure "+recentDiastolic.ToString()+" mmHg.";
						}
					}
					break;
				#endregion
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		///<summary>Just counts up the number of rows with an X in the numerator column.  Very simple.</summary>
		public static int CalcNumerator(DataTable table) {
			//No need to check RemotingRole; no call to db.
			int retVal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["numerator"].ToString()=="X") {
					retVal++;
				}
			}
			return retVal;
		}

		///<summary>Just counts up the number of EhrPatients with IsNumerator=true.</summary>
		public static int CalcNumerator2014(List<EhrCqmPatient> listPats) {
			int retval=0;
			for(int i=0;i<listPats.Count;i++) {
				if(listPats[i].IsNumerator) {
					retval++;
				}
			}
			return retval;
		}

		///<summary>Just counts up the number of EhrPatients with IsDenominator=true.  The only measure that may have some patients in the initial patient population that are not in the denominator is the influenza vaccine measure, CMS147v2.</summary>
		public static int CalcDenominator2014(List<EhrCqmPatient> listPats) {
			int retval=0;
			for(int i=0;i<listPats.Count;i++) {
				if(listPats[i].IsDenominator) {
					retval++;
				}
			}
			return retval;
		}

		///<summary>Just counts up the number of rows with an X in the exclusion column.  Very simple.</summary>
		public static int CalcExclusions(DataTable table) {
			//No need to check RemotingRole; no call to db.
			int retVal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["exclusion"].ToString()=="X") {
					retVal++;
				}
			}
			return retVal;
		}

		///<summary>Just counts up the number of EhrPatients with IsException=true.</summary>
		public static int CalcExceptions2014(List<EhrCqmPatient> listPats) {
			int retval=0;
			for(int i=0;i<listPats.Count;i++) {
				if(listPats[i].IsException) {
					retval++;
				}
			}
			return retval;
		}

		///<summary>Just counts up the number of EhrPatients with IsExclusion=true.</summary>
		public static int CalcExclusions2014(List<EhrCqmPatient> listPats) {
			int retval=0;
			for(int i=0;i<listPats.Count;i++) {
				if(listPats[i].IsExclusion) {
					retval++;
				}
			}
			return retval;
		}

		private static string GetDenominatorExplain(QualityType qtype) {
			//No need to check RemotingRole; no call to db.
			switch(qtype) {
				case QualityType.WeightOver65:
					return "All patients 65+ with at least one visit during the measurement period.";
				case QualityType.WeightAdult:
					return "All patients 18 to 64 with at least one visit during the measurement period.";
				case QualityType.Hypertension:
					return "All patients 18+ with ICD9 hypertension(401-404) and at least two visits, one during the measurement period.";
				case QualityType.TobaccoUse:
					//The original manual that these specs came from stated ALL patients seen during the period, and did not say anything about needing two visits.
					return "All patients 18+ with at least one visit during the measurement period.";
				case QualityType.TobaccoCessation:
					//It's inconsistent.  Sometimes it says 24 months from now (which doesn't make sense).  
					//Other times it says 24 months from last visit.  We're going with that.
					//Again, we will ignore the part about needing two visits.
					return "All patients 18+ with at least one visit during the measurement period; and identified as tobacco users within the 24 months prior to the last visit.";
				case QualityType.InfluenzaAdult:
					//the documentation is very sloppy.  It's including a flu season daterange in the denominator that is completely illogical.
					return "All patients 50+ with a visit during the measurement period.";
				case QualityType.WeightChild_1_1:
				case QualityType.WeightChild_1_2:
				case QualityType.WeightChild_1_3:
					return "All patients 2-16 with a visit during the measurement period, unless pregnant.";
				case QualityType.WeightChild_2_1:
				case QualityType.WeightChild_2_2:
				case QualityType.WeightChild_2_3:
					return "All patients 2-10 with a visit during the measurement period, unless pregnant.";
				case QualityType.WeightChild_3_1:
				case QualityType.WeightChild_3_2:
				case QualityType.WeightChild_3_3:
					return "All patients 11-16 with a visit during the measurement period, unless pregnant.";
				case QualityType.ImmunizeChild_1:
				case QualityType.ImmunizeChild_2:
				case QualityType.ImmunizeChild_3:
				case QualityType.ImmunizeChild_4:
				case QualityType.ImmunizeChild_5:
				case QualityType.ImmunizeChild_6:
				case QualityType.ImmunizeChild_7:
				case QualityType.ImmunizeChild_8:
				case QualityType.ImmunizeChild_9:
				case QualityType.ImmunizeChild_10:
				case QualityType.ImmunizeChild_11:
				case QualityType.ImmunizeChild_12:
					return "All patients with a visit during the measurement period who turned 2 during the measurement period.";
				case QualityType.Pneumonia:
					return "All patients 65+ during the measurement period with a visit within 1 year before the measurement end date.";
				case QualityType.DiabetesBloodPressure:
					return "All patients 17-74 before the measurement period, who either have a diabetes-related medication dispensed, or who have an active diagnosis of diabetes.";
				case QualityType.BloodPressureManage:
					return "All patients 17-74 before the measurement period who have an active diagnosis of hypertension and who are not pregnant or have ESRD.";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetNumeratorExplain(QualityType qtype) {
			//No need to check RemotingRole; no call to db.
			switch(qtype) {
				case QualityType.WeightOver65:
					return @"BMI < 22 or >= 30 with Followup documented.
BMI 22-30.";
				case QualityType.WeightAdult:
					return @"BMI < 18.5 or >= 25 with Followup documented.
BMI 18.5-25.";
				case QualityType.Hypertension:
					return "Blood pressure entered during measurement period.";
				case QualityType.TobaccoUse:
					return "Tobacco use recorded within the 24 months prior to the last visit.";
				case QualityType.TobaccoCessation:
					return "Tobacco cessation entry within the 24 months prior to the last visit.";
				case QualityType.InfluenzaAdult:
					return "Influenza vaccine administered.";
				case QualityType.WeightChild_1_1:
					return "BMI recorded during measurement period.";
				case QualityType.WeightChild_1_2:
					return "Counseling for nutrition during measurement period.";
				case QualityType.WeightChild_1_3:
					return "Counseling for physical activity during measurement period.";
				case QualityType.WeightChild_2_1:
					return "BMI recorded during measurement period.";
				case QualityType.WeightChild_2_2:
					return "Counseling for nutrition during measurement period.";
				case QualityType.WeightChild_2_3:
					return "Counseling for physical activity during measurement period.";
				case QualityType.WeightChild_3_1:
					return "BMI recorded during measurement period.";
				case QualityType.WeightChild_3_2:
					return "Counseling for nutrition during measurement period.";
				case QualityType.WeightChild_3_3:
					return "Counseling for physical activity during measurement period.";
				case QualityType.ImmunizeChild_1:
					return "4 DTaP vaccinations between 42 days and 2 years of age. CVX=110,120,20,50";
				case QualityType.ImmunizeChild_2:
					return "3 IPV vaccinations between 42 days and 2 years of age. CVX=10,120";
				case QualityType.ImmunizeChild_3:
					return "1 MMR vaccination before 2 years of age. CVX=03,94\r\n"
						+"OR 1 measles(05), 1 mumps(07), and 1 rubella(06).";
				case QualityType.ImmunizeChild_4:
					//the intro paragraph states 4 HiB.  They have a typo someplace.
					return "2 HiB vaccinations between 42 days and 2 years of age. CVX=120,46,47,48,49,50,51";
				case QualityType.ImmunizeChild_5:
					return "3 hepatitis B vaccinations before 2 years of age. CVX=08,110,44,51";
				case QualityType.ImmunizeChild_6:
					return "1 VZV vaccination before 2 years of age. CVX=21,94";
				case QualityType.ImmunizeChild_7:
					return "4 pneumococcal vaccinations between 42 days and 2 years of age. CVX=100,133";
				case QualityType.ImmunizeChild_8:
					return "2 hepatitis A vaccinations before 2 years of age. CVX=83";
				case QualityType.ImmunizeChild_9:
					return "2 rotavirus vaccinations between 42 days and 2 years of age. CVX=116,119";
				case QualityType.ImmunizeChild_10:
					return "2 influenza vaccinations between 180 days and 2 years of age. CVX=135,15";
				case QualityType.ImmunizeChild_11:
					return "All vaccinations 1-6.";
				case QualityType.ImmunizeChild_12:
					return "All vaccinations 1-7.";
				case QualityType.Pneumonia:
					return "Pneumococcal vaccine before measurement end date. CVX=33,100,133";
				case QualityType.DiabetesBloodPressure:
					return "Diastolic < 90 and systolic < 140 at most recent encounter.";
				case QualityType.BloodPressureManage:
					return "Diastolic < 90 and systolic < 140 at most recent encounter.";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetExclusionsExplain(QualityType qtype) {
			//No need to check RemotingRole; no call to db.
			switch(qtype) {
				case QualityType.WeightOver65:
					return "Marked ineligible within 6 months prior to the last visit.";
				case QualityType.WeightAdult:
					return "Terminal ineligible within 6 months prior to the last visit.";
				case QualityType.Hypertension:
					return "N/A";
				case QualityType.TobaccoUse:
					return "N/A";
				case QualityType.TobaccoCessation:
					return "N/A";
				case QualityType.InfluenzaAdult:
					return "A valid reason was entered for medication not given.";
				case QualityType.WeightChild_1_1:
				case QualityType.WeightChild_1_2:
				case QualityType.WeightChild_1_3:
				case QualityType.WeightChild_2_1:
				case QualityType.WeightChild_2_2:
				case QualityType.WeightChild_2_3:
				case QualityType.WeightChild_3_1:
				case QualityType.WeightChild_3_2:
				case QualityType.WeightChild_3_3:
					return "N/A";
				case QualityType.ImmunizeChild_1:
				case QualityType.ImmunizeChild_2:
				case QualityType.ImmunizeChild_3:
				case QualityType.ImmunizeChild_4:
				case QualityType.ImmunizeChild_5:
				case QualityType.ImmunizeChild_6:
				case QualityType.ImmunizeChild_7:
				case QualityType.ImmunizeChild_8:
				case QualityType.ImmunizeChild_9:
				case QualityType.ImmunizeChild_10:
				case QualityType.ImmunizeChild_11:
				case QualityType.ImmunizeChild_12:
					return "Contraindicated due to specific allergy or disease.";
				case QualityType.Pneumonia:
					return "N/A";
				case QualityType.DiabetesBloodPressure:
					return "1. Diagnosis polycystic ovaries and not active diagnosis of diabetes.\r\n"
						+"or 2. Medication was due to an accute episode, and not active diagnosis of diabetes.";
				case QualityType.BloodPressureManage:
					return "N/A";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetDenominatorExplain2014(QualityType2014 qtype) {
			//No need to check RemotingRole; no call to db.
			switch(qtype) {
				case QualityType2014.MedicationsEntered:
					return "All eligible encounters occurring during the reporting period for patients age 18+ at the start of the measurement period.";
				case QualityType2014.WeightOver65:
					return "All patients age 65+ at the start of the measurement period with an eligible encounter during the measurement period.  Not including encounters where the patient is receiving palliative care, the patient refuses measurement of height and/or weight, the patient is in an urgent or emergent medical situation, or there is any other qualified reason documenting why BMI measurement was not appropriate.";
				case QualityType2014.WeightAdult:
					return "All patients age 18 to 64 at the start of the measurement period with an eligible encounter during the measurement period. Not including encounters where the patient is receiving palliative care, the patient refuses measurement of height and/or weight, the patient in an urgent or emergent medical situation, or there is any other qualified reason documenting why BMI measurement was not appropriate.";
				case QualityType2014.CariesPrevent:
					return "Children age 0-19 at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.CariesPrevent_1:
					return "Children age 0-5 at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.CariesPrevent_2:
					return "Children age 6-12 at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.CariesPrevent_3:
					return "Children age 13-19 at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.ChildCaries:
					return "Childred age 0-19 at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.Pneumonia:
					return "All patients age 65+ at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.TobaccoCessation:
					return "All patients age 18+ at the start of the measurement period with an eligible encounter(s) during the measurement period.";
				case QualityType2014.Influenza:
					return "All patients 6 months+ at the start of the measurement period with eligible influenza encounter between October 1 of the year before the measurement period and March 31 of the measurement period and an eligible encounter during the measurement period.";
				case QualityType2014.WeightChild_1_1:
				case QualityType2014.WeightChild_1_2:
				case QualityType2014.WeightChild_1_3:
					return "All patients age 3-16 at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.WeightChild_2_1:
				case QualityType2014.WeightChild_2_2:
				case QualityType2014.WeightChild_2_3:
					return "All patients age 3-11 at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.WeightChild_3_1:
				case QualityType2014.WeightChild_3_2:
				case QualityType2014.WeightChild_3_3:
					return "All patients age 12-16 at the start of the measurement period with an eligible encounter during the measurement period.";
				case QualityType2014.BloodPressureManage:
					return "All patients age 18-84 at the start of the measurement period with an eligible encounter during the measurement period who have an active diagnosis of hypertension that starts before or within the first 6 months of the start of the measurement period.";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetNumeratorExplain2014(QualityType2014 qtype) {
			//No need to check RemotingRole; no call to db.
			switch(qtype) {
				case QualityType2014.MedicationsEntered:
					return "Encounters during which the provider attests to documenting a list of current medications to the best of his/her knowledge and ability by completing the current medications documented procedure.";
				case QualityType2014.WeightOver65:
					return "Patients who, for every encounter in the measurement period, the most recent physical examination documenting BMI occurred during the encounter or during the previous six months.  If the BMI was outside of normal parameters at any exam, a follow-up plan was documented during the exam or during the previous six months.";
				case QualityType2014.WeightAdult:
					return "Patients who, for every encounter in the measurement period, the most recent physical examination documenting BMI occurred during the encounter or during the previous six months.  If the BMI was outside of normal parameters at any exam, a follow-up plan was documented during the exam or during the previous six months.";
				case QualityType2014.CariesPrevent:
					return "Childred with an eligible flouride varnish procedure performed during the measurement period.";
				case QualityType2014.CariesPrevent_1:
					return "Childred with an eligible flouride varnish procedure performed during the measurement period.";
				case QualityType2014.CariesPrevent_2:
					return "Childred with an eligible flouride varnish procedure performed during the measurement period.";
				case QualityType2014.CariesPrevent_3:
					return "Childred with an eligible flouride varnish procedure performed during the measurement period.";
				case QualityType2014.ChildCaries:
					return "Childred with a diagnosis of caries with an eligible code during the measurement period.";
				case QualityType2014.Pneumonia:
					return "Patients with a Pneumococcal vaccination with qualified code administered or a history of the vaccination documented before or during the measurement period.";
				case QualityType2014.TobaccoCessation:
					return "Patients whose most recent assessment of tobacco use was within 24 month of the measurement end date and if characterized as a user also received tobacco cessation counseling intervention.";
				case QualityType2014.Influenza:
					return "Patients who received the influenza vaccination during the eligible influenza encounter or reported previous receipt of the vaccination during the encounter.";
				case QualityType2014.WeightChild_1_1:
				case QualityType2014.WeightChild_2_1:
				case QualityType2014.WeightChild_3_1:
					return "Patiens who had height, weight, and BMI percentile recorded during measurement period.";
				case QualityType2014.WeightChild_1_2:
				case QualityType2014.WeightChild_2_2:
				case QualityType2014.WeightChild_3_2:
					return "Patients who were counseled for nutrition during measurement period.";
				case QualityType2014.WeightChild_1_3:
				case QualityType2014.WeightChild_2_3:
				case QualityType2014.WeightChild_3_3:
					return "Patients who were counseled for physical activity during measurement period.";
				case QualityType2014.BloodPressureManage:
					return "Patients whose blood pressure was recorded during the most recent eligible encounter, and the results were diastolic < 90 mmHg and systolic < 140 mmHg.";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		///<summary>The exclusions and exceptions are very similar, in fact no measure from our set of 9 CQM's has both an exclusion and an exception.  Possibly combine these into one text box called "Exclusions/Exceptions" to save space on FormQualityMeasureEdit2014.  We will have to report them in the correctly labeled section in our QRDA III report, so we probably have to keep them in separate functions.  The difference between them is when to take each into account in calculating measure.  Find denominator, apply exclusions, then classify using numerator criteria, then only if not in numerator apply exceptions and subtract from denominator.  Exceptions only apply if the patient/encounter doesn't meet the numerator criteria, don't immediately subtract from the denominator population.</summary>
		private static string GetExclusionsExplain2014(QualityType2014 qtype) {
			//No need to check RemotingRole; no call to db.
			switch(qtype) {
				case QualityType2014.MedicationsEntered:
					return "N/A";
				case QualityType2014.WeightOver65:
				case QualityType2014.WeightAdult:
					return "Patients who were pregnant during any of the measurement period.";
				case QualityType2014.CariesPrevent:
				case QualityType2014.CariesPrevent_1:
				case QualityType2014.CariesPrevent_2:
				case QualityType2014.CariesPrevent_3:
				case QualityType2014.ChildCaries:
				case QualityType2014.Pneumonia:
				case QualityType2014.TobaccoCessation:
				case QualityType2014.Influenza:
					return "N/A";
				case QualityType2014.WeightChild_1_1:
				case QualityType2014.WeightChild_1_2:
				case QualityType2014.WeightChild_1_3:
				case QualityType2014.WeightChild_2_1:
				case QualityType2014.WeightChild_2_2:
				case QualityType2014.WeightChild_2_3:
				case QualityType2014.WeightChild_3_1:
				case QualityType2014.WeightChild_3_2:
				case QualityType2014.WeightChild_3_3:
					return "Patients who are pregnant during any of the measurement period.";
				case QualityType2014.BloodPressureManage:
					return "Patients who are pregnant during any of the measurement period.  Patients who had an active diagnosis of end stage renal disease, chronic kidney disease, or were undergoing dialysis during the measurement period.";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetExceptionsExplain2014(QualityType2014 qtype) {
			//No need to check RemotingRole; no call to db.
			switch(qtype) {
				case QualityType2014.MedicationsEntered:
					return "Encounters where the current medications documented procedure was not performed due to an eligible medical or other reason.";
				case QualityType2014.WeightOver65:
				case QualityType2014.WeightAdult:
				case QualityType2014.CariesPrevent:
				case QualityType2014.CariesPrevent_1:
				case QualityType2014.CariesPrevent_2:
				case QualityType2014.CariesPrevent_3:
				case QualityType2014.ChildCaries:
				case QualityType2014.Pneumonia:
					return "N/A";
				case QualityType2014.TobaccoCessation:
					return "Patients with a diagnosis of limited life expectancy during the measurement period or who have another eligible medical reason documented for not performing the screening.";
				case QualityType2014.Influenza:
					return @"Patients who have a documented medical reason (e.g. patient allergy), system reason (e.g. drug not available), or patient reason (e.g. medication refused) for not receiving the immunization.";
				case QualityType2014.WeightChild_1_1:
				case QualityType2014.WeightChild_1_2:
				case QualityType2014.WeightChild_1_3:
				case QualityType2014.WeightChild_2_1:
				case QualityType2014.WeightChild_2_2:
				case QualityType2014.WeightChild_2_3:
				case QualityType2014.WeightChild_3_1:
				case QualityType2014.WeightChild_3_2:
				case QualityType2014.WeightChild_3_3:
				case QualityType2014.BloodPressureManage:
					return "N/A";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

	}
}

