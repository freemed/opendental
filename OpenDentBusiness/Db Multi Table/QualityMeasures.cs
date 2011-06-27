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
					return "0024";




				case QualityType.ImmunizeChild://break this into 12
					return "0038";
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
					return "Weight, Child";
				case QualityType.ImmunizeChild:
					return "Immunization Status, Child";
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
					//WeightChild------------------------------------------------------------------------------------------------------------------

					break;
				case QualityType.ImmunizeChild:
					//ImmunizeChild----------------------------------------------------------------------------------------------------------------

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
						//InfluenzaAdult----------------------------------------------------------------------------------------------------------------

						break;
					case QualityType.ImmunizeChild:
						//InfluenzaAdult----------------------------------------------------------------------------------------------------------------

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
					return "";
				case QualityType.ImmunizeChild:
					return "";
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
					return "";
				case QualityType.ImmunizeChild:
					return "";
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
					return "";
				case QualityType.ImmunizeChild:
					return "";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

	}
}
