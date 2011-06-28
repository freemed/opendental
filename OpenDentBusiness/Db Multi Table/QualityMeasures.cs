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
					return "Weight, Child, pop 1, num 1";
				case QualityType.WeightChild_1_2:
					return "Weight, Child, pop 1, num 2";
				case QualityType.WeightChild_1_3:
					return "Weight, Child, pop 1, num 3";
				case QualityType.WeightChild_2_1:
					return "Weight, Child, pop 2, num 1";
				case QualityType.WeightChild_2_2:
					return "Weight, Child, pop 2, num 2";
				case QualityType.WeightChild_2_3:
					return "Weight, Child, pop 2, num 3";
				case QualityType.WeightChild_3_1:
					return "Weight, Child, pop 3, num 1";
				case QualityType.WeightChild_3_2:
					return "Weight, Child, pop 3, num 2";
				case QualityType.WeightChild_3_3:
					return "Weight, Child, pop 3, num 3";
				case QualityType.ImmunizeChild_1:
					return "Immun Status, Child, num 1";
				case QualityType.ImmunizeChild_2:
					return "Immun Status, Child, num 2";
				case QualityType.ImmunizeChild_3:
					return "Immun Status, Child, num 3";
				case QualityType.ImmunizeChild_4:
					return "Immun Status, Child, num 4";
				case QualityType.ImmunizeChild_5:
					return "Immun Status, Child, num 5";
				case QualityType.ImmunizeChild_6:
					return "Immun Status, Child, num 6";
				case QualityType.ImmunizeChild_7:
					return "Immun Status, Child, num 7";
				case QualityType.ImmunizeChild_8:
					return "Immun Status, Child, num 8";
				case QualityType.ImmunizeChild_9:
					return "Immun Status, Child, num 9";
				case QualityType.ImmunizeChild_10:
					return "Immun Status, Child, num 10";
				case QualityType.ImmunizeChild_11:
					return "Immun Status, Child, num 11";
				case QualityType.ImmunizeChild_12:
					return "Immun Status, Child, num 12";

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
						NotGivenDate1 date NOT NULL,
						Documentation1 varchar(255) NOT NULL

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
					/*
					//Count1, DTaP
					command="UPDATE tempehrquality "
						+"SET Count1=(SELECT COUNT(DISTINCT VaccinePatNum) FROM vaccinepat "
						+"LEFT JOIN vaccinedef ON vaccinepat.VaccineDefNum=vaccinedef.VaccineDefNum "
						+"WHERE tempehrquality.PatNum=vaccinepat.PatNum "
						+"AND vaccinedef.CVXCode IN('135','15'))";
					Db.NonQ(command);




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
					Db.NonQ(command);*/





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
					return "4 DTaP vaccinations between 42 days and 2 years of age.";
				case QualityType.ImmunizeChild_2:
					return "3 IPV vaccinations between 42 days and 2 years of age.";
				case QualityType.ImmunizeChild_3:
					return "1 MMR vaccination before 2 years of age.\r\n"
						+"OR 1 measles, 1 mumps, and 1 rubella.";
				case QualityType.ImmunizeChild_4:
					//the intro paragraph states 4 HiB.  They have a typo someplace.
					return "2 HiB vaccinations between 42 days and 2 years of age.";
				case QualityType.ImmunizeChild_5:
					return "3 hepatitis B vaccinations before 2 years of age.";
				case QualityType.ImmunizeChild_6:
					return "1 VZV vaccination before 2 years of age.";
				case QualityType.ImmunizeChild_7:
					return "4 pneumococcal vaccinations between 42 days and 2 years of age.";
				case QualityType.ImmunizeChild_8:
					return "2 hepatitis A vaccinations before 2 years of age.";
				case QualityType.ImmunizeChild_9:
					return "2 rotavirus vaccinations between 42 days and 2 years of age.";
				case QualityType.ImmunizeChild_10:
					return "2 influenza vaccinations between 180 days and 2 years of age.";
				case QualityType.ImmunizeChild_11:
					return "All vaccinations 1-6.";
				case QualityType.ImmunizeChild_12:
					return "All vaccinations 1-7.";
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
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

	}
}
