using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used in Ehr quality measures.</summary>
	public class QualityMeasures {
		///<summary>Generates a list of all the quality measures.  Performs all calculations and manipulations.  Returns list for viewing/output.</summary>
		public static List<QualityMeasure> GetAll(DateTime dateStart,DateTime dateEnd) {
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
				DataTable table=GetTable(measure.Type,dateStart,dateEnd);
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
			//measure.Id="421a";
			//measure.Descript=";
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
				case QualityType.WeightChild:
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
				case QualityType.WeightChild:
					return "Weight, Child";
				case QualityType.ImmunizeChild:
					return "Immunization Status, Child";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		public static DataTable GetTable(QualityType qtype,DateTime dateStart,DateTime dateEnd) {
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
						Weight float NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName,DateVisit) SELECT patient.PatNum,LName,FName,"
						+"MAX(ProcDate) "//on the first pass, all we can obtain is the date of the visit
						+"FROM patient "
						+"INNER JOIN procedurelog "//because we want to restrict to only results with procedurelog
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate > '1880-01-01' AND Birthdate <= "+POut.Date(DateTime.Today.AddYears(-65))+" "//65 or older
						+"GROUP BY patient.PatNum";//there will frequently be multiple procedurelog events
					Db.NonQ(command);
					//now, find BMIs within 6 months of each visit date. No logic for picking one of multiple BMIs.
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.Height=vitalsign.Height, "
						+"tempehrquality.Weight=vitalsign.Weight "//we could also easily get the BMI date if we wanted.
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken <= tempehrquality.DateVisit "
						+"AND vitalsign.DateTaken >= DATE_SUB(tempehrquality.DateVisit,INTERVAL 6 MONTH)";
					Db.NonQ(command);
					//todo
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
						Weight float NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrquality (PatNum,LName,FName,DateVisit) SELECT patient.PatNum,LName,FName,"
						+"MAX(ProcDate) "//on the first pass, all we can obtain is the date of the visit
						+"FROM patient "
						+"INNER JOIN procedurelog "//because we want to restrict to only results with procedurelog
						+"ON Patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
						+"WHERE Birthdate <= "+POut.Date(DateTime.Today.AddYears(-18))+" "//18+
						+"AND Birthdate > "+POut.Date(DateTime.Today.AddYears(-65))+" "//less than 65
						+"GROUP BY patient.PatNum";//there will frequently be multiple procedurelog events
					Db.NonQ(command);
					//now, find BMIs within 6 months of each visit date. No logic for picking one of multiple BMIs.
					command="UPDATE tempehrquality,vitalsign "
						+"SET tempehrquality.Height=vitalsign.Height, "
						+"tempehrquality.Weight=vitalsign.Weight "
						+"WHERE tempehrquality.PatNum=vitalsign.PatNum "
						+"AND vitalsign.DateTaken <= tempehrquality.DateVisit "
						+"AND vitalsign.DateTaken >= DATE_SUB(tempehrquality.DateVisit,INTERVAL 6 MONTH)";
					Db.NonQ(command);
					//todo
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
						//+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						//+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+" "
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
						+"AND vitalsign.DateTaken >= "+POut.Date(dateStart)+" "
						+"AND vitalsign.DateTaken <= "+POut.Date(dateEnd);
					Db.NonQ(command);
					command="SELECT * FROM tempehrquality";
					tableRaw=Db.GetTable(command);
					//command="DROP TABLE IF EXISTS tempehrquality";
					//Db.NonQ(command);
					break;
				case QualityType.TobaccoUse:

					break;
				case QualityType.TobaccoCessation:

					break;
				case QualityType.InfluenzaAdult:

					break;
				case QualityType.WeightChild:

					break;
				case QualityType.ImmunizeChild:

					break;
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
			//PatNum, PatientName, Numerator(X), and Exclusion(X).
			DataTable table=new DataTable("audit");
			DataRow row;
			table.Columns.Add("PatNum");
			table.Columns.Add("patientName");
			table.Columns.Add("numerator");
			table.Columns.Add("exclusion");
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
				switch(qtype) {
					case QualityType.WeightOver65:
						weight=PIn.Float(tableRaw.Rows[i]["Weight"].ToString());
						height=PIn.Float(tableRaw.Rows[i]["Height"].ToString());
						bmi=Vitalsigns.CalcBMI(weight,height);
						if(bmi==0){
							row["explanation"]="No BMI";
						}
						else if(bmi < 22) {
							row["explanation"]="Underweight";
						}
						else if(bmi < 30) {
							row["numerator"]="X";
							row["explanation"]="Normal weight";
						}
						else {
							row["explanation"]="Overweight";
						}
						break;
					case QualityType.WeightAdult:
						weight=PIn.Float(tableRaw.Rows[i]["Weight"].ToString());
						height=PIn.Float(tableRaw.Rows[i]["Height"].ToString());
						bmi=Vitalsigns.CalcBMI(weight,height);
						if(bmi==0){
							row["explanation"]="No BMI";
						}
						else if(bmi < 18.5f) {
							row["explanation"]="Underweight";
						}
						else if(bmi < 25) {
							row["numerator"]="X";
							row["explanation"]="Normal weight";
						}
						else {
							row["explanation"]="Overweight";
						}
						break;
					case QualityType.Hypertension:
						//Hypertension---------------------------------------------------------------------------------------------------------------------
						DateTime dateVisit=PIn.Date(tableRaw.Rows[i]["DateVisit"].ToString());
						int visitCount=PIn.Int(tableRaw.Rows[i]["VisitCount"].ToString());
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

						break;
					case QualityType.TobaccoCessation:

						break;
					case QualityType.InfluenzaAdult:

						break;
					case QualityType.WeightChild:

						break;
					case QualityType.ImmunizeChild:

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
					return "All patients 65 and older with at least one completed procedure during the measurement period.";
				case QualityType.WeightAdult:
					return "All patients 18 to 64 with at least one completed procedure during the measurement period.";
				case QualityType.Hypertension:
					return "All patients 18+ with ICD9 hypertension(401-404) and at least two visits, one during the measurement period.";
				case QualityType.TobaccoUse:
					return "";
				case QualityType.TobaccoCessation:
					return "";
				case QualityType.InfluenzaAdult:
					return "";
				case QualityType.WeightChild:
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
					return @"BMI < 22 or >= 30 with care goal of follow-up BMI, or with dietary consultation order.
BMI 22-30.";
				case QualityType.WeightAdult:
					return @"BMI < 18.5 or >= 25 with care goal of follow-up BMI, or with dietary consultation order.
BMI 18.5-25.";
				case QualityType.Hypertension:
					return "Blood pressure entered during measurement period.";
				case QualityType.TobaccoUse:
					return "";
				case QualityType.TobaccoCessation:
					return "";
				case QualityType.InfluenzaAdult:
					return "";
				case QualityType.WeightChild:
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
					return "Terminal illness; pregnancy; physical exam not done for patient, medical, or system reason.";
				case QualityType.WeightAdult:
					return "";
				case QualityType.Hypertension:
					return "";
				case QualityType.TobaccoUse:
					return "";
				case QualityType.TobaccoCessation:
					return "";
				case QualityType.InfluenzaAdult:
					return "";
				case QualityType.WeightChild:
					return "";
				case QualityType.ImmunizeChild:
					return "";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

	}
}
