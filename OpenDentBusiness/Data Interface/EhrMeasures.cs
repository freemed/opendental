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
					return "Maintain active medication list";
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
				case EhrMeasureType.Summary:
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
					return "More than 50% of all unique patients 65 years old or older admitted to the eligible hospital have an indication of an advance directive status recorded.";
				case EhrMeasureType.ElectronicCopy:
					return "More than 40% of all clinical lab tests results ordered by the EP or by an authorized provider of the eligible hospital or CAH for patients admitted to its inpatient or emergency department (POS 21 or 23) during the EHR reporting period whose results are either in a positive/negative or numerical format are incorporated in certified EHR technology as structured data.";
				case EhrMeasureType.ClinicalSummaries:
					return "Clinical summaries provided to patients for more than 50% of all office visits within 3 business days.";
				case EhrMeasureType.Reminders:
					return "More than 20% of all unique patients 65 years or older or 5 years old or younger were sent an appropriate reminder during the EHR reporting period.";
				case EhrMeasureType.MedReconcile:
					return "The EP, eligible hospital or CAH performs medication reconciliation for more than 50% of transitions of care in which the patient is transitioned into the care of the EP or admitted to the eligible hospital’s or CAH’s inpatient or emergency department (POS 21 or 23).";
				case EhrMeasureType.Summary:
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
					return 50;
				case EhrMeasureType.ElectronicCopy:
					return 40;
				case EhrMeasureType.ClinicalSummaries:
					return 50;
				case EhrMeasureType.Reminders:
					return 20;
				case EhrMeasureType.MedReconcile:
					return 50;
				case EhrMeasureType.Summary:
					return 50;
			}
			throw new ApplicationException("Type not found: "+mtype.ToString());
		}

		public static DataTable GetTable(EhrMeasureType mtype,DateTime dateStart,DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),mtype,dateStart,dateEnd);
			}
			string command="";
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
					break;
				case EhrMeasureType.Demographics:
					//language, gender, race, ethnicity, and birthdate
					command="SELECT PatNum,LName,FName,Birthdate,Gender,Race,Language "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";
					break;
				case EhrMeasureType.Education:
					command="SELECT PatNum,LName,FName "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";
					break;
				case EhrMeasureType.TimelyAccess:
					command="";
					break;
				case EhrMeasureType.ProvOrderEntry:
					command="";
					break;
				case EhrMeasureType.Rx:
					command="";
					break;
				case EhrMeasureType.VitalSigns:
					command="";
					break;
				case EhrMeasureType.Smoking:
					command="";
					break;
				case EhrMeasureType.Lab:
					command="";
					break;
				case EhrMeasureType.ElectronicCopy:
					command="";
					break;
				case EhrMeasureType.ClinicalSummaries:
					command="";
					break;
				case EhrMeasureType.Reminders:
					command="";
					break;
				case EhrMeasureType.MedReconcile:
					command="";
					break;
				case EhrMeasureType.Summary:
					command="";
					break;
				default:
					throw new ApplicationException("Type not found: "+mtype.ToString());
			}
			DataTable tableRaw=null;
			if(command=="") {
				tableRaw=new DataTable();
			}
			else{
				tableRaw=Db.GetTable(command);
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
						
						break;
					case EhrMeasureType.TimelyAccess:
						
						break;
					case EhrMeasureType.ProvOrderEntry:
						
						break;
					case EhrMeasureType.Rx:
						
						break;
					case EhrMeasureType.VitalSigns:
						
						break;
					case EhrMeasureType.Smoking:
						
						break;
					case EhrMeasureType.Lab:
						
						break;
					case EhrMeasureType.ElectronicCopy:
						
						break;
					case EhrMeasureType.ClinicalSummaries:
						
						break;
					case EhrMeasureType.Reminders:
						
						break;
					case EhrMeasureType.MedReconcile:
						
						break;
					case EhrMeasureType.Summary:
						
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
				case EhrMeasureType.Summary:
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
					return "All unique patients 65 years or older or 5 years old or younger.  Not restricted to those seen during the reporting period.  Must have status of Patient rather than Inactive, Nonpatient, Deceased, etc.";
				case EhrMeasureType.MedReconcile:
					return "Number of transitions of care from another provider to here during the reporting period.";
				case EhrMeasureType.Summary:
					return "Number of transitions of care and referrals during the reporting period for which the provider was the transferring or referring provider.";
			}
			throw new ApplicationException("Type not found: "+mtype.ToString());
		}

	}
}