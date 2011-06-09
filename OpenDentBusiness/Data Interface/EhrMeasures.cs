using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrMeasures{
		///<summary>Select All EHRMeasures from combination of db, static data, and complex calculations.</summary>
		public static List<EhrMeasure> SelectAll(DateTime dateStart, DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrMeasure>>(MethodBase.GetCurrentMethod(),dateStart,dateEnd);
			}
			string command="SELECT * FROM ehrmeasure ORDER BY MeasureType";
			List<EhrMeasure> retVal=Crud.EhrMeasureCrud.SelectMany(command);
			for(int i=0;i<retVal.Count;i++) {
				retVal[i].Objective=GetObjective(retVal[i].MeasureType);
				retVal[i].Measure=GetMeasure(retVal[i].MeasureType);
				retVal[i].PercentThreshold=GetThreshold(retVal[i].MeasureType);
				DataTable table=GetTable(retVal[i].MeasureType,dateStart,dateEnd);
				if(table==null) {
					retVal[i].Numerator=-1;
					retVal[i].Denominator=-1;
				}
				else {
					retVal[i].Numerator=CalcNumerator(table);
					retVal[i].Denominator=table.Rows.Count;
				}
				retVal[i].NumeratorExplain=GetNumeratorExplain(retVal[i].MeasureType);
				retVal[i].DenominatorExplain=GetDenominatorExplain(retVal[i].MeasureType);
			}
			return retVal; 
		}

		///<summary></summary>
		public static void Update(EhrMeasure ehrMeasure){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrMeasure);
				return;
			}
			Crud.EhrMeasureCrud.Update(ehrMeasure);
		}

		///<summary>Returns the Objective text based on the EHR certification documents.</summary>
		private static string GetObjective(EhrMeasureType mtype) {
			//No need to check RemotingRole; no call to db.
			switch(mtype) {
				case EhrMeasureType.ProblemList:
					return "Maintain an up-to-date problem list of current and active diagnoses.";
				case EhrMeasureType.MedicationList:
					return "Maintain active medication list.";
				case EhrMeasureType.AllergyList:
					return "Maintain active medication allergy list";
				case EhrMeasureType.Demographics:
					return "Record demographics: Preferred language, Gender, Race, Ethnicity, Date of Birth";
				case EhrMeasureType.Education:
					return "Use certified EHR technology to identify patient-specific education resources and provide those resources to the patient if appropriate.";
				case EhrMeasureType.TimelyAccess:
					return "Provide patients with timely electronic access to their health information (including lab results, problem list, medication lists, medication allergies) within four business days of the information being available to the EP";
				case EhrMeasureType.ProvOrderEntry:
					return "Use CPOE for medication orders directly entered by any licensed healthcare professional who can enter orders into the medical record per state, local and professional guidelines.";
				case EhrMeasureType.Rx:
					return "Generate and transmit permissible prescriptions electronically (eRx).";
				case EhrMeasureType.VitalSigns:
					return "Record and chart changes in vital signs: Height, Weight, Blood pressure, Calculate and display BMI, Plot and display growth charts for children 2-20 years, including BMI";
				case EhrMeasureType.Smoking:
					return "Record smoking status for patients 13 years old or older.";
				case EhrMeasureType.Lab:
					return "Incorporate clinical lab-test results into certified EHR technology as structured data.";
				case EhrMeasureType.ElectronicCopy:
					return "Provide patients with an electronic copy of their health information (including diagnostic test results, problem list, medication lists, medication allergies), upon request.";
				case EhrMeasureType.ClinicalSummaries:
					return "Provide clinical summaries for patients for each office visit.";
				case EhrMeasureType.Reminders:
					return "Send reminders to patients per patient preference for preventive/ follow up care.";
				case EhrMeasureType.MedReconcile:
					return "The EP, eligible hospital or CAH who receives a patient from another setting of care or provider of care or believes an encounter is relevant should perform medication reconciliation.";
				case EhrMeasureType.SummaryOfCare:
					return "The EP, eligible hospital or CAH who transitions their patient to another setting of care or provider of care or refers their patient to another provider of care should provide summary of care record for each transition of care or referral.";
			}
			throw new ApplicationException("Type not found: "+mtype.ToString());
		}

		///<summary>Returns the Measures text based on the EHR certification documents.</summary>
		private static string GetMeasure(EhrMeasureType mtype) {
			//No need to check RemotingRole; no call to db.
			switch(mtype) {
				case EhrMeasureType.ProblemList:
					return "More than 80% of all unique patients seen by the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23) have at least one entry or an indication that no problems are known for the patient recorded as structured data.";
				case EhrMeasureType.MedicationList:
					return "More than 80% of all unique patients seen by the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23) have at least one entry (or an indication that the patient is not currently prescribed any medication) recorded as structured data.";
				case EhrMeasureType.AllergyList:
					return "More than 80% of all unique patients seen by the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23) have at least one entry (or an indication that the patient has no known medication allergies) recorded as structured data.";
				case EhrMeasureType.Demographics:
					return "More than 50% of all unique patients seen by the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23) have demographics recorded as structured data.";
				case EhrMeasureType.Education:
					return "More than 10% of all unique patients seen by the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23) during the EHR reporting period are provided patient-specific education resources.";
				case EhrMeasureType.TimelyAccess:
					return "More than 10% of all unique patients seen by the EP are provided timely (available to the patient within four business days of being updated in the certified EHR technology) electronic access to their health information subject to the EP’s discretion to withhold certain information.";
				case EhrMeasureType.ProvOrderEntry:
					return "More than 30% of unique patients with at least one medication in their medication list seen by the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23) have at least one medication order entered using CPOE.";
				case EhrMeasureType.Rx:
					return "More than 40% of all permissible prescriptions written by the EP are transmitted electronically using certified EHR technology.";
				case EhrMeasureType.VitalSigns:
					return "More than 50% of all unique patients age 2 and over seen by the EP or admitted to eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23), height, weight and blood pressure are recorded as structured data.";
				case EhrMeasureType.Smoking:
					return "More than 50% of all unique patients 13 years old or older seen by the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23) have smoking status recorded as structured data.";
				case EhrMeasureType.Lab:
					return "More than 40% of all clinical lab tests results ordered by the EP or by an authorized provider of the eligible hospital or CAH for patients admitted to its inpatient or emergency department (POS 21 or 23) during the EHR reporting period whose results are either in a positive/negative or numerical format are incorporated in certified EHR technology as structured data.";
				case EhrMeasureType.ElectronicCopy:
					return "More than 40% of all clinical lab tests results ordered by the EP or by an authorized provider of the eligible hospital or CAH for patients admitted to its inpatient or emergency department (POS 21 or 23) during the EHR reporting period whose results are either in a positive/negative or numerical format are incorporated in certified EHR technology as structured data.";
				case EhrMeasureType.ClinicalSummaries:
					return "Clinical summaries provided to patients for more than 50% of all office visits within 3 business days.";
				case EhrMeasureType.Reminders:
					return "More than 20% of all unique patients 65 years or older or 5 years old or younger were sent an appropriate reminder during the EHR reporting period.";
				case EhrMeasureType.MedReconcile:
					return "The EP, eligible hospital or CAH performs medication reconciliation for more than 50% of transitions of care in which the patient is transitioned into the care of the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23).";
				case EhrMeasureType.SummaryOfCare:
					return "The EP, eligible hospital or CAH who transitions or refers their patient to another setting of care or provider of care provides a summary of care record for more than 50% of transitions of care and referrals.";
			}
			throw new ApplicationException("Type not found: "+mtype.ToString());
		}

		///<summary>Returns the Measures text based on the EHR certification documents.</summary>
		private static int GetThreshold(EhrMeasureType mtype) {
			//No need to check RemotingRole; no call to db.
			switch(mtype) {
				case EhrMeasureType.ProblemList:
					return 80;
				case EhrMeasureType.MedicationList:
					return 80;
				case EhrMeasureType.AllergyList:
					return 80;
				case EhrMeasureType.Demographics:
					return 50;
				case EhrMeasureType.Education:
					return 10;
				case EhrMeasureType.TimelyAccess:
					return 10;
				case EhrMeasureType.ProvOrderEntry:
					return 30;
				case EhrMeasureType.Rx:
					return 40;
				case EhrMeasureType.VitalSigns:
					return 50;
				case EhrMeasureType.Smoking:
					return 50;
				case EhrMeasureType.Lab:
					return 40;
				case EhrMeasureType.ElectronicCopy:
					return 40;
				case EhrMeasureType.ClinicalSummaries:
					return 50;
				case EhrMeasureType.Reminders:
					return 20;
				case EhrMeasureType.MedReconcile:
					return 50;
				case EhrMeasureType.SummaryOfCare:
					return 50;
			}
			throw new ApplicationException("Type not found: "+mtype.ToString());
		}

		public static DataTable GetTable(EhrMeasureType mtype,DateTime dateStart,DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),mtype,dateStart,dateEnd);
			}
			string command="";
			DataTable tableRaw=new DataTable();
			switch(mtype) {
				case EhrMeasureType.ProblemList:
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM disease WHERE PatNum=patient.PatNum AND DiseaseDefNum="
							+POut.Long(PrefC.GetLong(PrefName.ProblemsIndicateNone))+") AS problemsNone, "
						+"(SELECT COUNT(*) FROM disease WHERE PatNum=patient.PatNum) AS problemsAll "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.MedicationList:
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM medicationpat WHERE PatNum=patient.PatNum AND MedicationNum="
							+POut.Long(PrefC.GetLong(PrefName.MedicationsIndicateNone))+") AS medsNone, "
						+"(SELECT COUNT(*) FROM medicationpat WHERE PatNum=patient.PatNum) AS medsAll "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.AllergyList:
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM allergy WHERE PatNum=patient.PatNum AND AllergyDefNum="
							+POut.Long(PrefC.GetLong(PrefName.AllergiesIndicateNone))+") AS allergiesNone, "
						+"(SELECT COUNT(*) FROM allergy WHERE PatNum=patient.PatNum) AS allergiesAll "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.Demographics:
					//language, gender, race, ethnicity, and birthdate
					command="SELECT PatNum,LName,FName,Birthdate,Gender,Race,Language "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.Education:
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM ehrmeasureevent WHERE PatNum=patient.PatNum AND EventType="+POut.Int((int)EhrMeasureEventType.EducationProvided)+") AS edCount "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.TimelyAccess:
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM ehrmeasureevent WHERE PatNum=patient.PatNum AND EventType="+POut.Int((int)EhrMeasureEventType.OnlineAccessProvided)+") AS onlineCount "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.ProvOrderEntry:
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM medicationpat mp2 WHERE mp2.PatNum=patient.PatNum "
						+"AND mp2.PatNote != '' AND mp2.DateStart > "+POut.Date(new DateTime(1880,1,1))+") AS countOrders "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "//at least one procedure in the period
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+") "
						+"AND EXISTS(SELECT * FROM medicationpat WHERE medicationpat.PatNum=patient.PatNum)";//at least one medication
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.Rx:
					command="SELECT patient.PatNum,LName,FName,SendStatus,RxDate "
						+"FROM rxpat,patient "
						+"WHERE rxpat.PatNum=patient.PatNum "
						+"AND IsControlled = 0 "
						+"AND RxDate >= "+POut.Date(dateStart)+" "
						+"AND RxDate <= "+POut.Date(dateEnd);
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.VitalSigns:
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM vitalsign WHERE vitalsign.PatNum=patient.PatNum AND Height>0 AND Weight>0) AS hwCount, "
						+"(SELECT COUNT(*) FROM vitalsign WHERE vitalsign.PatNum=patient.PatNum AND BpSystolic>0 AND BpDiastolic>0) AS bpCount "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+") "
						+"AND patient.Birthdate <= "+POut.Date(DateTime.Today.AddYears(-2));//2 and older
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.Smoking:
					command="SELECT PatNum,LName,FName,SmokeStatus "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+") "
						+"AND patient.Birthdate <= "+POut.Date(DateTime.Today.AddYears(-13));//13 and older
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.Lab:
					command="SELECT patient.PatNum,LName,FName,DateTimeOrder, "
						+"(SELECT COUNT(*) FROM labpanel WHERE labpanel.MedicalOrderNum=medicalorder.MedicalOrderNum) AS panelCount "
						+"FROM medicalorder,patient "
						+"WHERE medicalorder.PatNum=patient.PatNum "
						+"AND MedOrderType="+POut.Int((int)MedicalOrderType.Laboratory)+" "
						+"AND DATE(DateTimeOrder) >= "+POut.Date(dateStart)+" "
						+"AND DATE(DateTimeOrder) <= "+POut.Date(dateEnd);
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.ElectronicCopy:
					command="DROP TABLE IF EXISTS tempehrmeasure";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrmeasure (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						dateRequested date NOT NULL,
						dateDeadline date NOT NULL,
						copyProvided tinyint NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrmeasure (PatNum,LName,FName,dateRequested) SELECT patient.PatNum,LName,FName,DATE(DateTEvent) "
						+"FROM ehrmeasureevent "
						+"LEFT JOIN patient ON patient.PatNum=ehrmeasureevent.PatNum "
						+"WHERE EventType="+POut.Int((int)EhrMeasureEventType.ElectronicCopyRequested)+" "
						+"AND DATE(DateTEvent) >= "+POut.Date(dateStart)+" "
						+"AND DATE(DateTEvent) <= "+POut.Date(dateEnd);
					Db.NonQ(command);
					command="UPDATE tempehrmeasure "
						+"SET dateDeadline = ADDDATE(dateRequested, INTERVAL 3 DAY)";
					Db.NonQ(command);
					command="UPDATE tempehrmeasure "
						+"SET dateDeadline = ADDDate(dateDeadline, INTERVAL 2 DAY) "//add 2 more days for weekend
						+"WHERE DAYOFWEEK(dateRequested) IN(4,5,6)";//wed, thur, fri
					Db.NonQ(command);
					command="UPDATE tempehrmeasure,ehrmeasureevent SET copyProvided = 1 "
						+"WHERE ehrmeasureevent.PatNum=tempehrmeasure.PatNum AND EventType="+POut.Int((int)EhrMeasureEventType.ElectronicCopyProvidedToPt)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent) >= dateRequested "
						+"AND DATE(ehrmeasureevent.DateTEvent) <= dateDeadline";
					Db.NonQ(command);
					command="SELECT * FROM tempehrmeasure";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrmeasure";
					Db.NonQ(command);
					break;
				case EhrMeasureType.ClinicalSummaries:
					command="DROP TABLE IF EXISTS tempehrmeasure";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrmeasure (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						visitDate date NOT NULL,
						deadlineDate date NOT NULL,
						summaryProvided tinyint NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrmeasure (PatNum,LName,FName,visitDate) SELECT patient.PatNum,LName,FName,ProcDate "
						+"FROM procedurelog "
						+"LEFT JOIN patient ON patient.PatNum=procedurelog.PatNum "
						+"WHERE ProcDate >= "+POut.Date(dateStart)+" "
						+"AND ProcDate <= "+POut.Date(dateEnd)+" "
						+"GROUP BY procedurelog.PatNum,ProcDate";
					Db.NonQ(command);
					command="UPDATE tempehrmeasure "
						+"SET deadlineDate = ADDDATE(visitDate, INTERVAL 3 DAY)";
					Db.NonQ(command);
					command="UPDATE tempehrmeasure "
						+"SET DeadlineDate = ADDDate(deadlineDate, INTERVAL 2 DAY) "//add 2 more days for weekend
						+"WHERE DAYOFWEEK(visitDate) IN(4,5,6)";//wed, thur, fri
					Db.NonQ(command);
					command="UPDATE tempehrmeasure,ehrmeasureevent SET summaryProvided = 1 "
						+"WHERE ehrmeasureevent.PatNum=tempehrmeasure.PatNum AND EventType="+POut.Int((int)EhrMeasureEventType.ClinicalSummaryProvidedToPt)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent) >= visitDate "
						+"AND DATE(ehrmeasureevent.DateTEvent) <= deadlineDate";
					Db.NonQ(command);
					command="SELECT * FROM tempehrmeasure";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrmeasure";
					Db.NonQ(command);
					break;
				case EhrMeasureType.Reminders:
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM ehrmeasureevent WHERE PatNum=patient.PatNum AND EventType="+POut.Int((int)EhrMeasureEventType.ReminderSent)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent) >= "+POut.Date(dateStart)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent) <= "+POut.Date(dateEnd)+" "
						+") AS reminderCount "
						+"FROM patient "
						+"WHERE patient.Birthdate > '1880-01-01' "//a birthdate is entered
						+"AND (patient.Birthdate > "+POut.Date(DateTime.Today.AddYears(-6))+" "//5 years or younger
						+"OR patient.Birthdate <= "+POut.Date(DateTime.Today.AddYears(-65))+") "//65+
						+"AND patient.PatStatus="+POut.Int((int)PatientStatus.Patient);
					tableRaw=Db.GetTable(command);
					break;
				case EhrMeasureType.MedReconcile:
					command="DROP TABLE IF EXISTS tempehrmeasure";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrmeasure (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						RefCount int NOT NULL,
						ReconcileCount int NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrmeasure (PatNum,LName,FName,RefCount) SELECT patient.PatNum,LName,FName,COUNT(*) "
						+"FROM refattach "
						+"LEFT JOIN patient ON patient.PatNum=refattach.PatNum "
						+"WHERE RefDate >= "+POut.Date(dateStart)+" "
						+"AND RefDate <= "+POut.Date(dateEnd)+" "
						+"AND IsFrom=1 AND IsTransitionOfCare=1 "
						+"GROUP BY refattach.PatNum";
					Db.NonQ(command);
					command="UPDATE tempehrmeasure "
						+"SET ReconcileCount = (SELECT COUNT(*) FROM ehrmeasureevent "
						+"WHERE ehrmeasureevent.PatNum=tempehrmeasure.PatNum AND EventType="+POut.Int((int)EhrMeasureEventType.MedicationReconcile)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent) >= "+POut.Date(dateStart)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent) <= "+POut.Date(dateEnd)+")";
					Db.NonQ(command);
					command="SELECT * FROM tempehrmeasure";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrmeasure";
					Db.NonQ(command);
					break;
				case EhrMeasureType.SummaryOfCare:
					command="DROP TABLE IF EXISTS tempehrmeasure";
					Db.NonQ(command);
					command=@"CREATE TABLE tempehrmeasure (
						PatNum bigint NOT NULL PRIMARY KEY,
						LName varchar(255) NOT NULL,
						FName varchar(255) NOT NULL,
						RefCount int NOT NULL,
						CcdCount int NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
					command="INSERT INTO tempehrmeasure (PatNum,LName,FName,RefCount) SELECT patient.PatNum,LName,FName,COUNT(*) "
						+"FROM refattach "
						+"LEFT JOIN patient ON patient.PatNum=refattach.PatNum "
						+"WHERE RefDate >= "+POut.Date(dateStart)+" "
						+"AND RefDate <= "+POut.Date(dateEnd)+" "
						+"AND IsFrom=0 AND IsTransitionOfCare=1 "
						+"GROUP BY refattach.PatNum";
					Db.NonQ(command);
					command="UPDATE tempehrmeasure "
						+"SET CcdCount = (SELECT COUNT(*) FROM ehrmeasureevent "
						+"WHERE ehrmeasureevent.PatNum=tempehrmeasure.PatNum AND EventType="+POut.Int((int)EhrMeasureEventType.SummaryOfCareProvidedToDr)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent) >= "+POut.Date(dateStart)+" "
						+"AND DATE(ehrmeasureevent.DateTEvent) <= "+POut.Date(dateEnd)+")";
					Db.NonQ(command);
					command="SELECT * FROM tempehrmeasure";
					tableRaw=Db.GetTable(command);
					command="DROP TABLE IF EXISTS tempehrmeasure";
					Db.NonQ(command);
					break;
				default:
					throw new ApplicationException("Type not found: "+mtype.ToString());
			}
			//PatNum, PatientName, Explanation, and Met (X).
			DataTable table=new DataTable("audit");
			DataRow row;
			table.Columns.Add("PatNum");
			table.Columns.Add("patientName");
			table.Columns.Add("explanation");
			table.Columns.Add("met");//X or empty
			List<DataRow> rows=new List<DataRow>();
			Patient pat;
			string explanation;
			for(int i=0;i<tableRaw.Rows.Count;i++) {
				row=table.NewRow();
				row["PatNum"]=tableRaw.Rows[i]["PatNum"].ToString();
				pat=new Patient();
				pat.LName=tableRaw.Rows[i]["LName"].ToString();
				pat.FName=tableRaw.Rows[i]["FName"].ToString();
				pat.Preferred="";
				row["patientName"]=pat.GetNameLF();
				row["met"]="";
				explanation="";
				switch(mtype) {
					case EhrMeasureType.ProblemList:
						if(tableRaw.Rows[i]["problemsNone"].ToString()!="0") {
							explanation="Problems indicated 'None'";
							row["met"]="X";
						}
						else if(tableRaw.Rows[i]["problemsAll"].ToString()!="0") {
							explanation="Problems entered: "+tableRaw.Rows[i]["problemsAll"].ToString();
							row["met"]="X";
						}
						else{
							explanation="No Problems entered";
						}
						break;
					case EhrMeasureType.MedicationList:
						if(tableRaw.Rows[i]["medsNone"].ToString()!="0") {
							explanation="Medications indicated 'None'";
							row["met"]="X";
						}
						else if(tableRaw.Rows[i]["medsAll"].ToString()!="0") {
							explanation="Medications entered: "+tableRaw.Rows[i]["medsAll"].ToString();
							row["met"]="X";
						}
						else {
							explanation="No Medications entered";
						}
						break;
					case EhrMeasureType.AllergyList:
						if(tableRaw.Rows[i]["allergiesNone"].ToString()!="0") {
							explanation="Allergies indicated 'None'";
							row["met"]="X";
						}
						else if(tableRaw.Rows[i]["allergiesAll"].ToString()!="0") {
							explanation="Allergies entered: "+tableRaw.Rows[i]["allergiesAll"].ToString();
							row["met"]="X";
						}
						else {
							explanation="No Allergies entered";
						}
						break;
					case EhrMeasureType.Demographics:
						if(PIn.Date(tableRaw.Rows[i]["Birthdate"].ToString()).Year<1880) {
							explanation+="birthdate";//missing
						}
						if(tableRaw.Rows[i]["Language"].ToString()=="") {
							if(explanation!="") {
								explanation+=", ";
							}
							explanation+="language";
						}
						if(PIn.Int(tableRaw.Rows[i]["Gender"].ToString())==(int)PatientGender.Unknown) {
							if(explanation!="") {
								explanation+=", ";
							}
							explanation+="gender";
						}
						if(tableRaw.Rows[i]["Race"].ToString()=="0") {
							if(explanation!="") {
								explanation+=", ";
							}
							explanation+="race, ethnicity";
						}
						if(explanation=="") {
							explanation="All demographic elements recorded";
							row["met"]="X";
						}
						else {
							explanation="Missing: "+explanation;
						}
						break;
					case EhrMeasureType.Education:
						if(tableRaw.Rows[i]["edCount"].ToString()=="0") {
							explanation="No education resources";
						}
						else {
							explanation="Education resources provided";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.TimelyAccess:
						if(tableRaw.Rows[i]["onlineCount"].ToString()=="0") {
							explanation="No online access provided";
						}
						else {
							explanation="Online access provided";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.ProvOrderEntry:
						if(tableRaw.Rows[i]["countOrders"].ToString()=="0") {
							explanation="No medication order through CPOE";
						}
						else {
							explanation="Medication order in CPOE";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.Rx:
						RxSendStatus sendStatus=(RxSendStatus)PIn.Int(tableRaw.Rows[i]["SendStatus"].ToString());
						DateTime rxDate=PIn.Date(tableRaw.Rows[i]["rxDate"].ToString());
						if(sendStatus==RxSendStatus.SentElect) {
							explanation=rxDate.ToShortDateString()+" Rx sent electronically.";
							row["met"]="X";
						}
						else {
							explanation=rxDate.ToShortDateString()+" Rx not sent electronically.";
						}
						break;
					case EhrMeasureType.VitalSigns:
						if(tableRaw.Rows[i]["hwCount"].ToString()=="0") {
							explanation+="height, weight";
						}
						if(tableRaw.Rows[i]["bpCount"].ToString()=="0") {
							if(explanation!="") {
								explanation+=", ";
							}
							explanation+="blood pressure";
						}
						if(explanation=="") {
							explanation="Vital signs entered";
							row["met"]="X";
						}
						else {
							explanation="Missing: "+explanation;
						}
						break;
					case EhrMeasureType.Smoking:
						SmokingStatus smokeStatus=(SmokingStatus)PIn.Int(tableRaw.Rows[i]["SmokeStatus"].ToString());
						if(smokeStatus==SmokingStatus.UnknownIfEver) {
							explanation+="Smoking status not entered.";
						}
						else{
							explanation="Smoking status entered.";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.Lab:
						int panelCount=PIn.Int(tableRaw.Rows[i]["panelCount"].ToString());
						DateTime dateOrder=PIn.Date(tableRaw.Rows[i]["DateTimeOrder"].ToString());
						if(panelCount==0) {
							explanation+=dateOrder.ToShortDateString()+" results not attached.";
						}
						else {
							explanation=dateOrder.ToShortDateString()+" results attached.";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.ElectronicCopy:
						DateTime dateRequested=PIn.Date(tableRaw.Rows[i]["dateRequested"].ToString());
						if(tableRaw.Rows[i]["copyProvided"].ToString()=="0") {
							explanation=dateRequested.ToShortDateString()+" no copy provided to patient";
						}
						else {
							explanation=dateRequested.ToShortDateString()+" copy provided to patient";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.ClinicalSummaries:
						DateTime visitDate=PIn.Date(tableRaw.Rows[i]["visitDate"].ToString());
						if(tableRaw.Rows[i]["summaryProvided"].ToString()=="0") {
							explanation=visitDate.ToShortDateString()+" no summary provided to patient";
						}
						else {
							explanation=visitDate.ToShortDateString()+" summary provided to patient";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.Reminders:
						if(tableRaw.Rows[i]["reminderCount"].ToString()=="0") {
							explanation="No reminders sent";
						}
						else {
							explanation="Reminders sent";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.MedReconcile:
						int refCount=PIn.Int(tableRaw.Rows[i]["RefCount"].ToString());//this will always be greater than zero
						int reconcileCount=PIn.Int(tableRaw.Rows[i]["ReconcileCount"].ToString());
						if(reconcileCount<refCount) {
							explanation="Transitions of Care:"+refCount.ToString()+", Reconciles:"+reconcileCount.ToString();
						}
						else {
							explanation="Reconciles performed for each transition of care.";
							row["met"]="X";
						}
						break;
					case EhrMeasureType.SummaryOfCare:
						int refCount2=PIn.Int(tableRaw.Rows[i]["RefCount"].ToString());//this will always be greater than zero
						int ccdCount=PIn.Int(tableRaw.Rows[i]["CcdCount"].ToString());
						if(ccdCount<refCount2) {
							explanation="Transitions of Care:"+refCount2.ToString()+", Summaries provided:"+ccdCount.ToString();
						}
						else {
							explanation="Summaries provided for each transition of care.";
							row["met"]="X";
						}
						break;
					default:
						throw new ApplicationException("Type not found: "+mtype.ToString());
				}
				row["explanation"]=explanation;
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>Just counts up the number of rows with an X in the met column.  Very simple.</summary>
		public static int CalcNumerator(DataTable table) {
			//No need to check RemotingRole; no call to db.
			int retVal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["met"].ToString()=="X") {
					retVal++;
				}
			}
			return retVal;
		}

		///<summary>Returns the explanation of the numerator based on the EHR certification documents.</summary>
		private static string GetNumeratorExplain(EhrMeasureType mtype) {
			//No need to check RemotingRole; no call to db.
			switch(mtype) {
				case EhrMeasureType.ProblemList:
					return "Patients with at least one problem list entry or an indication of 'None' on problem list.";
				case EhrMeasureType.MedicationList:
					return "Patients with at least one medication list entry or an indication of 'None' on medication list.";
				case EhrMeasureType.AllergyList:
					return "Patients with at least one allergy list entry or an indication of 'None' on allergy list.";
				case EhrMeasureType.Demographics:
					return "Patients with all required demographic elements recorded as structured data: language, gender, race, ethnicity, and birthdate.";
				case EhrMeasureType.Education:
					return "Patients provided patient-specific education resources, not dependent on requests.";
				case EhrMeasureType.TimelyAccess:
					return "Electronic access of health information provided to seen patients within 4 business days of being entered into their EHR, not dependent on requests.";
				case EhrMeasureType.ProvOrderEntry:
					return "Patients with a medication order entered using CPOE.";
				case EhrMeasureType.Rx:
					return "Permissible prescriptions transmitted electronically.";
				case EhrMeasureType.VitalSigns:
					return "Patients with height, weight, and blood pressure recorded.";
				case EhrMeasureType.Smoking:
					return "Patients with smoking status recorded.";
				case EhrMeasureType.Lab:
					return "Lab results entered.";
				case EhrMeasureType.ElectronicCopy:
					return "Electronic copy received within 3 business days.";
				case EhrMeasureType.ClinicalSummaries:
					return "Clinical summaries of office visits provided to patients within 3 business days, not dependent on requests.";
				case EhrMeasureType.Reminders:
					return "Appropriate reminders sent during the reporting period.";
				case EhrMeasureType.MedReconcile:
					return "Medication reconciliation was performed for each transition of care.";
				case EhrMeasureType.SummaryOfCare:
					return "Summary of care record was provided for each transition or referral.";
			}
			throw new ApplicationException("Type not found: "+mtype.ToString());
		}

		///<summary>Returns the explanation of the denominator based on the EHR certification documents.</summary>
		private static string GetDenominatorExplain(EhrMeasureType mtype) {
			//No need to check RemotingRole; no call to db.
			switch(mtype) {
				case EhrMeasureType.ProblemList:
					return "All unique patients with at least one completed procedure during the reporting period.";
				case EhrMeasureType.MedicationList:
					return "All unique patients with at least one completed procedure during the reporting period.";
				case EhrMeasureType.AllergyList:
					return "All unique patients with at least one completed procedure during the reporting period.";
				case EhrMeasureType.Demographics:
					return "All unique patients with at least one completed procedure during the reporting period.";
				case EhrMeasureType.Education:
					return "All unique patients with at least one completed procedure during the reporting period.";
				case EhrMeasureType.TimelyAccess:
					return "All unique patients with at least one completed procedure during the reporting period.";
				case EhrMeasureType.ProvOrderEntry:
					return "All unique patients with at least one completed procedure during the reporting period and with at least one medication in their medication list.";
				case EhrMeasureType.Rx:
					return "All permissible prescriptions during the reporting period.";
				case EhrMeasureType.VitalSigns:
					return "All unique patients age 2 and over with at least one completed procedure during the reporting period.";
				case EhrMeasureType.Smoking:
					return "All unique patients 13 years or older with at least one completed procedure during the reporting period.";
				case EhrMeasureType.Lab:
					return "All lab orders during the reporting period.";
				case EhrMeasureType.ElectronicCopy:
					return "All patients who request an electronic copy of their health information during the reporting period.";
				case EhrMeasureType.ClinicalSummaries:
					return "All office visits during the reporting period.  An office visit is calculated as any number of completed procedures for a given date.";
				case EhrMeasureType.Reminders:
					return "All unique patients 65+ or 5-.  Not restricted to those seen during the reporting period.  Must have status of Patient rather than Inactive, Nonpatient, Deceased, etc.";
				case EhrMeasureType.MedReconcile:
					return "Number of incoming transitions of care from another provider during the reporting period.";
				case EhrMeasureType.SummaryOfCare:
					return "Number of outgoing transitions of care and referrals during the reporting period.";
			}
			throw new ApplicationException("Type not found: "+mtype.ToString());
		}

		///<summary></summary>
		public static List<EhrMu> GetMu(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrMu>>(MethodBase.GetCurrentMethod(),pat);
			}
			List<EhrMu> list=new List<EhrMu>();
			//add one of each type
			EhrMu mu;
			string explanation;
			List<MedicationPat> medList=MedicationPats.Refresh(pat.PatNum,true);
			List<EhrMeasureEvent> listMeasureEvents=EhrMeasureEvents.Refresh(pat.PatNum);
			List<RefAttach> listRefAttach=RefAttaches.Refresh(pat.PatNum);
			for(int i=0;i<Enum.GetValues(typeof(EhrMeasureType)).Length;i++) {
				mu=new EhrMu();
				mu.Met=MuMet.False;
				mu.MeasureType=(EhrMeasureType)i;
				switch(mu.MeasureType) {
					case EhrMeasureType.ProblemList:
						List<Disease> listDisease=Diseases.Refresh(pat.PatNum);
						if(listDisease.Count==0){
							mu.Details="No problems entered.";
						}
						else{
							mu.Met=MuMet.True;
							bool diseasesNone=false;
							if(listDisease.Count==1 && listDisease[0].DiseaseDefNum==PrefC.GetLong(PrefName.ProblemsIndicateNone)){
								diseasesNone=true;
							}
							if(diseasesNone){
								mu.Details="Problems marked 'none'.";
							}
							else{
								mu.Details="Problems entered: "+listDisease.Count.ToString();
							}
						}
						mu.Action="Enter problems";
						break;
					case EhrMeasureType.MedicationList:
						if(medList.Count==0) {
							mu.Details="No medications entered.";
						}
						else{
							mu.Met=MuMet.True;
							bool medsNone=false;
							if(medList.Count==1 && medList[0].MedicationNum==PrefC.GetLong(PrefName.MedicationsIndicateNone)) {
								medsNone=true;
							}
							if(medsNone) {
								mu.Details="Medications marked 'none'.";
							}
							else{
								mu.Details="Medications entered: "+medList.Count.ToString();
							}
						}
						mu.Action="Enter medications";
						break;
					case EhrMeasureType.AllergyList:
						List<Allergy> listAllergies=Allergies.Refresh(pat.PatNum);
						if(listAllergies.Count==0) {
							mu.Details="No allergies entered.";
						}
						else{
							mu.Met=MuMet.True;
							bool allergiesNone=false;
							if(listAllergies.Count==1 && listAllergies[0].AllergyDefNum==PrefC.GetLong(PrefName.AllergiesIndicateNone)) {
								allergiesNone=true;
							}
							if(allergiesNone) {
								mu.Details="Allergies marked 'none'.";
							}
							else{
								mu.Details="Allergies entered: "+listAllergies.Count.ToString();
							}
						}
						mu.Action="Enter allergies";
						break;
					case EhrMeasureType.Demographics:
						explanation="";
						if(pat.Birthdate.Year<1880) {
							explanation+="birthdate";//missing
						}
						if(pat.Language=="") {
							if(explanation!="") {
								explanation+=", ";
							}
							explanation+="language";
						}
						if(pat.Gender==PatientGender.Unknown) {
							if(explanation!="") {
								explanation+=", ";
							}
							explanation+="gender";
						}
						if(pat.Race==PatientRace.Unknown) {
							if(explanation!="") {
								explanation+=", ";
							}
							explanation+="race, ethnicity";
						}
						if(explanation=="") {
							mu.Details="All demographic elements recorded";
							mu.Met=MuMet.True;
						}
						else {
							mu.Details="Missing: "+explanation;
						}
						mu.Action="Enter demographics";
						break;
					case EhrMeasureType.Education:
						List<EhrMeasureEvent> listEd=EhrMeasureEvents.RefreshByType(EhrMeasureEventType.EducationProvided,pat.PatNum);
						if(listEd.Count==0) {
							mu.Details="No education resources provided.";
						}
						else {
							mu.Details="Education resources provided: "+listEd.Count.ToString();
							mu.Met=MuMet.True;
						}
						mu.Action="Provide education resources";
						break;
					case EhrMeasureType.TimelyAccess:
						List<EhrMeasureEvent> listOnline=EhrMeasureEvents.RefreshByType(EhrMeasureEventType.OnlineAccessProvided,pat.PatNum);
						if(listOnline.Count==0) {
							mu.Details="No online access provided.";
						}
						else {
							mu.Details="Online access provided: "+listOnline[listOnline.Count-1].DateTEvent.ToShortDateString();//most recent
							mu.Met=MuMet.True;
						}
						mu.Action="Provide online Access";
						break;
					case EhrMeasureType.ProvOrderEntry:
						int medOrderCount=0;
						for(int mo=0;mo<medList.Count;mo++){
							if(medList[mo].DateStart.Year>1880 && medList[mo].PatNote!=""){
								medOrderCount++;
							}
						}
						if(medList.Count==0) {
							mu.Met=MuMet.NA;
							mu.Details="No meds.";
						}
						else if(medOrderCount==0) {
							mu.Details="No medication order in CPOE.";
						}
						else {
							mu.Details="Medications entered in CPOE: "+medOrderCount.ToString();
							mu.Met=MuMet.True;
						}
						mu.Action="Provider Order Entry";
						break;
					case EhrMeasureType.Rx:
						List<RxPat> listRx=RxPats.GetPermissableForDateRange(pat.PatNum,DateTime.Today.AddYears(-1),DateTime.Today);
						if(listRx.Count==0){
							mu.Met=MuMet.NA;
							mu.Details="No Rxs entered.";
						}
						else{
							explanation="";
							for(int rx=0;rx<listRx.Count;rx++) {
								if(listRx[rx].SendStatus==RxSendStatus.SentElect){
									continue;
								}
								if(explanation!="") {
									explanation+=", ";
								}
								explanation+=listRx[rx].RxDate.ToShortDateString();
							}
							if(explanation=="") {
								mu.Met=MuMet.True;
								mu.Details="All Rxs sent electronically.";
							}
							else {
								mu.Met=MuMet.False;
								mu.Details="Rxs not sent electronically: "+explanation;
							}
						}
						mu.Action="(edit Rxs from Chart)";//no action
						break;
					case EhrMeasureType.VitalSigns:
						List<Vitalsign> vitalsignList=Vitalsigns.Refresh(pat.PatNum);
						if(vitalsignList.Count==0) {
							mu.Details="No vital signs entered.";
						}
						else {
							bool hFound=false;
							bool wFound=false;
							bool bpFound=false;
							for(int v=0;v<vitalsignList.Count;v++) {
								if(vitalsignList[v].Height>0) {
									hFound=true;
								}
								if(vitalsignList[v].Weight>0) {
									wFound=true;
								}
								if(vitalsignList[v].BpDiastolic>0 && vitalsignList[v].BpSystolic>0) {
									bpFound=true;
								}
							}
							explanation="";
							if(!hFound) {
								explanation+="height";//missing
							}
							if(!wFound) {
								if(explanation!="") {
									explanation+=", ";
								}
								explanation+="weight";
							}
							if(!bpFound) {
								if(explanation!="") {
									explanation+=", ";
								}
								explanation+="blood pressure";
							}
							if(explanation=="") {
								mu.Details="Vital signs entered";
								mu.Met=MuMet.True;
							}
							else {
								mu.Details="Missing: "+explanation;
							}
						}
						mu.Action="Enter vital signs";
						break;
					case EhrMeasureType.Smoking:
						if(pat.SmokeStatus==SmokingStatus.UnknownIfEver) {
							mu.Details="Smoking status not entered";
						}
						else {
							mu.Details="Smoking status entered";
							mu.Met=MuMet.True;
						}
						mu.Action="Edit smoking status";
						break;
					case EhrMeasureType.Lab:
						List<MedicalOrder> listLabOrders=MedicalOrders.GetLabsByDate(pat.PatNum,DateTime.Today.AddYears(-1),DateTime.Today);
						if(listLabOrders.Count==0) {
							mu.Details="No lab orders";
							mu.Met=MuMet.NA;
						}
						else {
							int labPanelCount=0;
							for(int lo=0;lo<listLabOrders.Count;lo++) {
								List<LabPanel> listLabPanels=LabPanels.GetPanelsForOrder(listLabOrders[lo].MedicalOrderNum);
								if(listLabPanels.Count>0) {
									labPanelCount++;
								}
							}
							if(labPanelCount<listLabOrders.Count) {
								mu.Details="Lab orders missing results: "+(listLabOrders.Count-labPanelCount).ToString();
							}
							else {
								mu.Details="Lab results entered for each lab order.";
								mu.Met=MuMet.True;
							}
						}
						mu.Action="Edit lab panels";
						mu.Action2="Import lab results";
						break;
					case EhrMeasureType.ElectronicCopy:
						List<EhrMeasureEvent> listRequests=EhrMeasureEvents.GetByType(listMeasureEvents,EhrMeasureEventType.ElectronicCopyRequested);
						List<EhrMeasureEvent> listRequestsPeriod=new List<EhrMeasureEvent>();
						for(int r=0;r<listRequests.Count;r++) {
							if(listRequests[r].DateTEvent < DateTime.Now.AddYears(-1)) {//not within the last year
								continue;
							}
							listRequestsPeriod.Add(listRequests[r]);
						}
						if(listRequestsPeriod.Count==0) {
							mu.Met=MuMet.NA;
							mu.Details="No requests within the last year.";
						}
						else {
							int countMissingCopies=0;
							bool copyProvidedinTime;
							List<EhrMeasureEvent> listCopiesProvided=EhrMeasureEvents.GetByType(listMeasureEvents,EhrMeasureEventType.ElectronicCopyProvidedToPt);
							for(int rp=0;rp<listRequestsPeriod.Count;rp++) {
								copyProvidedinTime=false;
								DateTime deadlineDateCopy=listRequestsPeriod[rp].DateTEvent.Date.AddDays(3);
								if(listRequestsPeriod[rp].DateTEvent.DayOfWeek==DayOfWeek.Wednesday 
									|| listRequestsPeriod[rp].DateTEvent.DayOfWeek==DayOfWeek.Thursday 
									|| listRequestsPeriod[rp].DateTEvent.DayOfWeek==DayOfWeek.Friday) 
								{
									deadlineDateCopy.AddDays(2);//add two days for the weekend
								}
								for(int cp=0;cp<listCopiesProvided.Count;cp++) {
									if(listCopiesProvided[cp].DateTEvent.Date > deadlineDateCopy) {
										continue;
									}
									if(listCopiesProvided[cp].DateTEvent.Date < listRequestsPeriod[rp].DateTEvent.Date) {
										continue;
									}
									copyProvidedinTime=true;
								}
								if(!copyProvidedinTime) {
									countMissingCopies++;
								}
							}
							if(countMissingCopies==0) {
								mu.Met=MuMet.True;
								mu.Details="Electronic copy provided to Pt within 3 business days of each request.";
							}
							else {
								mu.Met=MuMet.False;
								mu.Details="Electronic copies not provided to Pt within 3 business days of a request:"+countMissingCopies.ToString();
							}
						}
						mu.Action="Provide elect copy to Pt";
						break;
					case EhrMeasureType.ClinicalSummaries:
						List<DateTime> listVisits=new List<DateTime>();//for this year
						List<Procedure> listProcs=Procedures.Refresh(pat.PatNum);
						for(int p=0;p<listProcs.Count;p++) {
							if(listProcs[p].ProcDate < DateTime.Now.AddYears(-1)) {//not within the last year
								continue;
							}
							if(!listVisits.Contains(listProcs[p].ProcDate)) {
								listVisits.Add(listProcs[p].ProcDate);
							}
						}
						if(listVisits.Count==0){
							mu.Met=MuMet.NA;
							mu.Details="No visits within the last year.";
						}
						else{
							int countMissing=0;
							bool summaryProvidedinTime;
							List<EhrMeasureEvent> listClinSum=EhrMeasureEvents.GetByType(listMeasureEvents,EhrMeasureEventType.ClinicalSummaryProvidedToPt);
							for(int p=0;p<listVisits.Count;p++) {
								summaryProvidedinTime=false;
								DateTime deadlineDate=listVisits[p].AddDays(3);
								if(listVisits[p].DayOfWeek==DayOfWeek.Wednesday || listVisits[p].DayOfWeek==DayOfWeek.Thursday || listVisits[p].DayOfWeek==DayOfWeek.Friday){
									deadlineDate.AddDays(2);//add two days for the weekend
								}
								for(int r=0;r<listClinSum.Count;r++) {
									if(listClinSum[r].DateTEvent.Date > deadlineDate) {
										continue;
									}
									if(listClinSum[r].DateTEvent.Date < listVisits[p]) {
										continue;
									}
									summaryProvidedinTime=true;
								}
								if(!summaryProvidedinTime) {
									countMissing++;
								}
							}
							if(countMissing==0) {
								mu.Met=MuMet.True;
								mu.Details="Clinical summary provided to Pt within 3 business days of each visit.";
							}
							else {
								mu.Met=MuMet.False;
								mu.Details="Clinical summaries not provided to Pt within 3 business days of a visit:"+countMissing.ToString();
							}
						}
						mu.Action="Send clinical summary to Pt";
						break;
					case EhrMeasureType.Reminders:
						if(pat.PatStatus!=PatientStatus.Patient) {
							mu.Met=MuMet.NA;
							mu.Details="Status not patient.";
						}
						else if(pat.Age==0) {
							mu.Met=MuMet.NA;
							mu.Details="Age not entered.";
						}
						else if(pat.Age>5 && pat.Age<65) {
							mu.Met=MuMet.NA;
							mu.Details="Patient age not 65+ or 5-.";
						}
						else {
							List<EhrMeasureEvent> listReminders=EhrMeasureEvents.GetByType(listMeasureEvents,EhrMeasureEventType.ReminderSent);
							//during reporting period.
							bool withinLastYear=false;
							for(int r=0;r<listReminders.Count;r++) {
								if(listReminders[r].DateTEvent > DateTime.Now.AddYears(-1)) {
									withinLastYear=true;
								}
							}
							if(withinLastYear) {
								mu.Details="Reminder sent within the last year.";
								mu.Met=MuMet.True;
							}
							else {
								mu.Details="No reminders sent within the last year for patient age 65+ or 5-.";
							}
						}
						mu.Action="Send reminders";
						break;
					case EhrMeasureType.MedReconcile:
						int countFromRef=0;
						int countFromRefPeriod=0;
						for(int c=0;c<listRefAttach.Count;c++) {
							if(listRefAttach[c].IsFrom && listRefAttach[c].IsTransitionOfCare) {
								countFromRef++;
								if(listRefAttach[c].RefDate > DateTime.Now.AddYears(-1)) {//within the last year
									countFromRefPeriod++;
								}
							}
						}
						if(countFromRef==0) {
							mu.Met=MuMet.NA;
							mu.Details="Referral 'from' not entered.";
						}
						else if(countFromRefPeriod==0) {
							mu.Met=MuMet.NA;
							mu.Details="Referral 'from' not entered within the last year.";
						}
						else if(countFromRefPeriod > 0) {
							List<EhrMeasureEvent> listReconciles=EhrMeasureEvents.GetByType(listMeasureEvents,EhrMeasureEventType.MedicationReconcile);
							int countReconciles=0;//during reporting period.
							for(int r=0;r<listReconciles.Count;r++) {
								if(listReconciles[r].DateTEvent > DateTime.Now.AddYears(-1)) {//within the same period as the count for referrals.
									countReconciles++;
								}
							}
							mu.Details="Referrals:"+countFromRefPeriod.ToString()+", Reconciles:"+countReconciles.ToString();
							if(countReconciles>=countFromRefPeriod) {
								mu.Met=MuMet.True;
							}
						}
						mu.Action="Reconcile medications";
						mu.Action2="Enter Referrals";
						break;
					case EhrMeasureType.SummaryOfCare:
						int countToRefPeriod=0;
						for(int c=0;c<listRefAttach.Count;c++) {
							if(!listRefAttach[c].IsFrom && listRefAttach[c].IsTransitionOfCare) {
								if(listRefAttach[c].RefDate > DateTime.Now.AddYears(-1)) {//within the last year
									countToRefPeriod++;
								}
							}
						}
						if(countToRefPeriod==0) {
							mu.Met=MuMet.NA;
							mu.Details="No outgoing transitions of care within the last year.";
						}
						else{// > 0
							List<EhrMeasureEvent> listCcds=EhrMeasureEvents.GetByType(listMeasureEvents,EhrMeasureEventType.SummaryOfCareProvidedToDr);
							int countCcds=0;//during reporting period.
							for(int r=0;r<listCcds.Count;r++) {
								if(listCcds[r].DateTEvent > DateTime.Now.AddYears(-1)) {//within the same period as the count for referrals.
									countCcds++;
								}
							}
							mu.Details="Referrals:"+countToRefPeriod.ToString()+", Summaries:"+countCcds.ToString();
							if(countCcds>=countToRefPeriod) {
								mu.Met=MuMet.True;
							}
						}
						mu.Action="Send summary of care to Dr";
						mu.Action2="Enter Referrals";
						break;
				}
				list.Add(mu);
			}
			return list;
		}

	}

	///<summary>When FormEHR closes, the result will be one of these.  Different results will lead to different behaviors.</summary>
	public enum EhrFormResult {
		None,
		RxSelect,
		RxEdit,
		Medical,
		PatientEdit,
		Online,
		MedReconcile,
		Referrals,
		MedicationPatNew,
		MedicationPatEdit
	}
}