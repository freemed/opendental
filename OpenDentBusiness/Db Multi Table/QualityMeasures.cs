using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used in Ehr quality measures.</summary>
	public class QualityMeasures {
		///<summary>Generates a list of all the quality measures.  Performs all calculations and manipulations.  Returns list for viewing/output.</summary>
		public static List<QualityMeasure> GetAll(DateTime dateStart,DateTime dateEnd,long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<QualityMeasure>>(MethodBase.GetCurrentMethod(),dateStart,dateEnd);
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
					return "Pneumonia immunization, 64+";
				case QualityType.DiabetesBloodPressure:
					return "Diabetes: BP Management";
				case QualityType.BloodPressureManage:
					return "Controlling high BP";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		public static DataTable GetTable(QualityType qtype,DateTime dateStart,DateTime dateEnd,long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),qtype,dateStart,dateEnd);
			}
			//these queries only work for mysql
			string command="";
			DataTable tableRaw=new DataTable();
			switch(qtype) {
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
						+"COUNT(DISTINCT ProcDate),icd9.ICD9Code "
						+"FROM patient "
						+"INNER JOIN procedurelog "
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProvNum="+POut.Long(provNum)+" "
						+"LEFT JOIN disease ON disease.PatNum=patient.PatNum "
						+"LEFT JOIN icd9 ON icd9.ICD9Num=disease.ICD9Num "
						+"AND icd9.ICD9Code REGEXP '^40[1-4]' "//starts with 401 through 404
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
						+"AND patient.SmokeStatus IN("+POut.Int((int)SmokingStatus.CurrentEveryDay)+","+POut.Int((int)SmokingStatus.CurrentSomeDay)+") "
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
						+"AND vaccinepat.DateTimeStart=tempehrquality.DateVaccine "
						+"AND vaccinedef.CVXCode IN('135','15')";
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
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
					//Count5, Hep B
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
					//Count8, hep A
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
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrquality";
					Db.NonQ(command);
					break;
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
						+"FROM medicationpat,medication "
						+"WHERE medicationpat.MedicationNum=medication.MedicationNum "
						+"AND medicationpat.PatNum=tempehrquality.PatNum "
						+"AND medication.RxCui IN(199149, 199150, 200132, 205329, 205330, 205331, 401938,"//alph-glucosidas
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
						+"FROM disease,icd9 "
						+"WHERE disease.ICD9Num=icd9.ICD9Num "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND (icd9.ICD9Code LIKE '250%' "
						+"OR icd9.ICD9Code LIKE '357.2' "
						+"OR icd9.ICD9Code LIKE '362.0%' "
						+"OR icd9.ICD9Code LIKE '366.41' "
						+"OR icd9.ICD9Code LIKE '648.0%') "
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
						+"FROM disease,icd9 "
						+"WHERE disease.ICD9Num=icd9.ICD9Num "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND icd9.ICD9Code = '256.4' "
						+"AND (disease.DateStart <= "+POut.Date(dateEnd)+" "//if there's a datestart, it can't be after period end
						+"OR disease.DateStart < '1880-01-01') "
						//no restrictions on datestop.  It could still be active or could have stopped before or after the period end.
						+") > 0";
					Db.NonQ(command);
					//HasDiagnosisAcuteDiabetes
					command="UPDATE tempehrquality "
						+"SET HasDiagnosisAcuteDiabetes = 1 "
						+"WHERE (SELECT COUNT(*) "
						+"FROM disease,icd9 "
						+"WHERE disease.ICD9Num=icd9.ICD9Num "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND (icd9.ICD9Code LIKE '249%' OR icd9.ICD9Code='251.8' OR icd9.ICD9Code='962.0' "//steroid induced
						+"OR icd9.ICD9Code LIKE '648.8%') "//gestational
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
						+"FROM disease,icd9 "
						+"WHERE disease.ICD9Num=icd9.ICD9Num "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND icd9.ICD9Code LIKE '401%' "
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
						+"FROM disease,icd9 "
						+"WHERE disease.ICD9Num=icd9.ICD9Num "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND (icd9.ICD9Code LIKE '63%' "
						+"OR icd9.ICD9Code LIKE '64%' "
						+"OR icd9.ICD9Code LIKE '65%' "
						+"OR icd9.ICD9Code LIKE '66%' "
						+"OR icd9.ICD9Code LIKE '67%' "
						+"OR icd9.ICD9Code LIKE 'V22%' "
						+"OR icd9.ICD9Code LIKE 'V23%' "
						+"OR icd9.ICD9Code LIKE 'V28%') "
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
						+"FROM disease,icd9 "
						+"WHERE disease.ICD9Num=icd9.ICD9Num "
						+"AND disease.PatNum=tempehrquality.PatNum "
						+"AND (icd9.ICD9Code LIKE '38.95' "
						+"OR icd9.ICD9Code LIKE '39%' "
						+"OR icd9.ICD9Code LIKE '54.98' "
						+"OR icd9.ICD9Code LIKE '55.6%' "
						+"OR icd9.ICD9Code LIKE '585%' "
						+"OR icd9.ICD9Code LIKE 'V42.0%' "
						+"OR icd9.ICD9Code LIKE 'V45.1%' "
						+"OR icd9.ICD9Code LIKE 'V56%') "
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

	}
}
