using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrTriggers{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all AutoTriggers.</summary>
		private static List<AutoTrigger> listt;

		///<summary>A list of all AutoTriggers.</summary>
		public static List<AutoTrigger> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM autotrigger ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutoTrigger";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AutoTriggerCrud.TableToList(table);
		}
		#endregion
		*/

		public static List<EhrTrigger> GetAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrTrigger>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM ehrtrigger";
			return Crud.EhrTriggerCrud.SelectMany(command);
		}

		///<summary>This is the first step of automation, this checks to see if the new object matches one of the trigger conditions. </summary>
		/// <param name="triggerObject">Can be DiseaseDef, Disease, RxPat, Medication, Allergy, AllergyDef, Snomed, Icd9, Icd10, RxNorm, Cvx, Patient, or null. If patient, will check demographics. if null, will check all existing triggers.</param>
		/// <param name="PatCur">Triggers and intervention are currently always dependant on current patient. </param>
		/// <returns>Returns a dictionary keyed on triggers and a list of all the objects that the trigger matched on. Should be used to generate CDS intervention message and later be passed to FormInfobutton for knowledge request.</returns>
		public static Dictionary<string,List<object>> TriggerMatch(object triggerObject,Patient PatCur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Dictionary<string,List<object>>>(MethodBase.GetCurrentMethod(),triggerObject,PatCur);
			}
			Dictionary<string,List<object>> retVal=new Dictionary<string,List<object>>();
			//Define objects to be used in matching triggers.
			DiseaseDef diseaseDef;
			Disease disease;
			ICD9 icd9;
			Icd10 icd10;
			Snomed snomed;
			RxDef rxdef;
			string triggerObjectMessage="";
			string command="";
			switch(triggerObject.GetType().Name) {
				case "DiseaseDef":
					diseaseDef=(DiseaseDef)triggerObject;
					if(diseaseDef.ICD9Code!=""){
						triggerObjectMessage+="  -"+diseaseDef.ICD9Code+"(Icd9)  "+ICD9s.GetByCode(diseaseDef.ICD9Code).Description+"\r\n";
					}
					if(diseaseDef.Icd10Code!=""){
						triggerObjectMessage+="  -"+diseaseDef.Icd10Code+"(Icd10)  "+Icd10s.GetByCode(diseaseDef.Icd10Code).Description+"\r\n";
					}
					if(diseaseDef.SnomedCode!=""){
						triggerObjectMessage+="  -"+diseaseDef.SnomedCode+"(Snomed)  "+Snomeds.GetByCode(diseaseDef.SnomedCode).Description+"\r\n";
					}
					command="SELECT * FROM ehrtrigger"
					+" WHERE Icd9List LIKE '% "+POut.String(diseaseDef.ICD9Code)+" %'"// '% <code> %' so that we can get exact matches.
					+" OR Icd10List LIKE '% "+POut.String(diseaseDef.Icd10Code)+" %'"
					+" OR SnomedList LIKE '% "+POut.String(diseaseDef.SnomedCode)+" %'";
					break;
				case "Disease":
					disease=(Disease)triggerObject;
					//TODO: TriggerObjectMessage
					diseaseDef=DiseaseDefs.GetItem(disease.DiseaseDefNum);
					command="SELECT * FROM ehrtrigger"
					+" WHERE Icd9List LIKE '% "+POut.String(diseaseDef.ICD9Code)+" %'"// '% <code> %' so that we can get exact matches.
					+" OR Icd10List LIKE '% "+POut.String(diseaseDef.Icd10Code)+" %'"
					+" OR SnomedList LIKE '% "+POut.String(diseaseDef.SnomedCode)+" %'";
					break;
				case "ICD9":
					icd9=(ICD9)triggerObject;
					//TODO: TriggerObjectMessage
					command="SELECT * FROM ehrtrigger"
					+" WHERE Icd9List LIKE '% "+POut.String(icd9.ICD9Code)+" %'";// '% <code> %' so that we can get exact matches.
					break;
				case "Icd10":
					icd10=(Icd10)triggerObject;
					//TODO: TriggerObjectMessage
					command="SELECT * FROM ehrtrigger"
					+" WHERE Icd10List LIKE '% "+POut.String(icd10.Icd10Code)+" %'";// '% <code> %' so that we can get exact matches.
					break;
				case "Snomed":
					snomed=(Snomed)triggerObject;
					//TODO: TriggerObjectMessage
					command="SELECT * FROM ehrtrigger"
					+" WHERE SnomedList LIKE '% "+POut.String(snomed.SnomedCode)+" %'";// '% <code> %' so that we can get exact matches.
					break;
				case "RxDef":
					rxdef=(RxDef)triggerObject;
					//TODO: TriggerObjectMessage
					command="SELECT * FROM ehrtrigger"
					+" WHERE RxCuiList LIKE '% "+POut.String(rxdef.RxCui.ToString())+" %'";// '% <code> %' so that we can get exact matches.
					break;
				case "RxPat":
					throw new Exception("RxPat should not be used here. Did you mean to send in RxDef?"); 
					break;
				default:
					command="SELECT * FROM ehrtrigger WHERE false";//should not return any results.
					#if DEBUG
						throw new Exception(triggerObject.GetType().ToString()+" object not implemented as intervention trigger yet. Add to the list above to handle.");
					#endif
					break;
			}
			List<EhrTrigger> listEhrTriggers=Crud.EhrTriggerCrud.SelectMany(command);
			if(listEhrTriggers.Count==0){
				return null;//no triggers matched.
			}
			//Check for MatchCardinality.One type triggers.----------------------------------------------------------------------------
			for(int i=0;i<listEhrTriggers.Count;i++) {
				if(listEhrTriggers[i].Cardinality!=MatchCardinality.One) {
					continue;
				}
				string triggerMessage=listEhrTriggers[i].Description+":\r\n";//Example:"Patient over 55:\r\n"
				triggerMessage+=triggerObjectMessage;//Example:"  -Patient Age 67\r\n"
				List<object> ListObjectMatches=new List<object>();
				ListObjectMatches.Add(triggerObject);
				retVal.Add(triggerMessage,ListObjectMatches);
			}
			//Fill object lists to be checked-------------------------------------------------------------------------------------------------
			List<Allergy> ListAllergy=Allergies.GetAll(PatCur.PatNum,false);
			List<Disease> ListDisease=Diseases.Refresh(PatCur.PatNum,true);
			List<LabPanel> ListLabPanel=LabPanels.Refresh(PatCur.PatNum);
			List<MedicationPat> ListMedicationPat=MedicationPats.Refresh(PatCur.PatNum,false);
			List<AllergyDef> ListAllergyDef=new List<AllergyDef>();
			for(int i=0;i<ListAllergy.Count;i++){
				ListAllergyDef.Add(AllergyDefs.GetOne(ListAllergy[i].AllergyDefNum));
			}
			for(int i=0;i<listEhrTriggers.Count;i++) {
				if(listEhrTriggers[i].Cardinality==MatchCardinality.One) {
					continue;//we handled these above.
				}
				string triggerMessage=listEhrTriggers[i].Description+":\r\n";
				triggerMessage+=triggerObjectMessage;
				List<object> ListObjectMatches=new List<object>();//Allergy, Disease, LabPanels, MedicationPat, Patient, VaccinePat
				List<string> MatchedCodes=new List<string>();
				ListObjectMatches.Add(triggerObject);
				//Allergy-----------------------------------------------------------------------------------------------------------------------
				//allergy.snomedreaction
				//allergy.AllergyDefNum>>AllergyDef.SnomedType
				//allergy.AllergyDefNum>>AllergyDef.SnomedAllergyTo
				//allergy.AllergyDefNum>>AllergyDef.MedicationNum>>Medication.RxCui
				//Disease-----------------------------------------------------------------------------------------------------------------------
				//Disease.DiseaseDefNum>>DiseaseDef.ICD9Code
				//Disease.DiseaseDefNum>>DiseaseDef.SnomedCode
				//Disease.DiseaseDefNum>>DiseaseDef.Icd10Code
				//LabPanels---------------------------------------------------------------------------------------------------------------------
				//LabPanel.LabPanelNum<<LabResult.TestId (Loinc)
				//LabPanel.LabPanelNum<<LabResult.ObsValue (Loinc)
				//LabPanel.LabPanelNum<<LabResult.ObsRange (Loinc)
				//MedicationPat-----------------------------------------------------------------------------------------------------------------
				//MedicationPat.RxCui
				//MedicationPat.MedicationNum>>Medication.RxCui
				//Patient>>Demographics---------------------------------------------------------------------------------------------------------
				//Patient.Gender
				//Patient.Birthdate (Loinc age?)
				//Patient.SmokingSnoMed
				//RxPat-------------------------------------------------------------------------------------------------------------------------
				//Do not check RxPat. It is useless.
				//VaccinePat--------------------------------------------------------------------------------------------------------------------
				//VaccinePat.VaccineDefNum>>VaccineDef.CVXCode
				//VitalSign---------------------------------------------------------------------------------------------------------------------
				//VitalSign.Height (Loinc)
				//VitalSign.Weight (Loinc)
				//VitalSign.BpSystolic (Loinc)
				//VitalSign.BpDiastolic (Loinc)
				//VitalSign.WeightCode (Snomed)
				//VitalSign.PregDiseaseNum (Snomed)
				//Use object matches to check if required conditions are met-------------------------------------------------------------------------------
				switch(listEhrTriggers[i].Cardinality) {
					case MatchCardinality.One:
						//should never get here, handled above.
						continue;
					case MatchCardinality.OneOfEachCategory:
						//Medication
						//Allergy
						//Problem
						//Vital
						//Age
						//Gender
						//Lab Result
						break;
					case MatchCardinality.TwoOrMore:
						if(ListObjectMatches.Count<2) {
							continue;//Must match at least two objects for this category.
						}
						break;
					case MatchCardinality.All:
						//Match all Icd9Codes-------------------------------------------------------------------------------------------------------------------------------------------------
						string[] arrayIcd9Codes=listEhrTriggers[i].ProblemIcd9List.Split(new string[] {" "},StringSplitOptions.RemoveEmptyEntries);
						bool allConditionsMet=true;
						for(int c=0;c<arrayIcd9Codes.Length;c++) {
							if(MatchedCodes.Contains(arrayIcd9Codes[i])){
								continue;//found required code
							}
							//required code not found, set allConditionsMet to false and continue to next trigger
							allConditionsMet=false;
							break;
						}
						if(!allConditionsMet) {
							continue;//next trigger
						}
						//Match all Icd10Codes------------------------------------------------------------------------------------------------------------------------------------------------
						string[] arrayIcd10Codes=listEhrTriggers[i].ProblemIcd10List.Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries);
						for(int c=0;c<arrayIcd10Codes.Length;c++) {
							if(MatchedCodes.Contains(arrayIcd10Codes[i])) {
								continue;//found required code
							}
							//required code not found, set allConditionsMet to false and continue to next trigger
							allConditionsMet=false;
							break;
						}
						if(!allConditionsMet) {
							continue;//next trigger
						}
						//Match all SnomedCodes-----------------------------------------------------------------------------------------------------------------------------------------------
						string[] arraySnomedCodes=listEhrTriggers[i].ProblemSnomedList.Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries);
						for(int c=0;c<arraySnomedCodes.Length;c++) {
							if(MatchedCodes.Contains(arraySnomedCodes[i])) {
								continue;//found required code
							}
							//required code not found, set allConditionsMet to false and continue to next trigger
							allConditionsMet=false;
							break;
						}
						if(!allConditionsMet) {
							continue;//next trigger
						}
						//Match all CvxCodes--------------------------------------------------------------------------------------------------------------------------------------------------
						string[] arrayCvxCodes=listEhrTriggers[i].CvxList.Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries);
						for(int c=0;c<arrayCvxCodes.Length;c++) {
							if(MatchedCodes.Contains(arrayCvxCodes[i])) {
								continue;//found required code
							}
							//required code not found, set allConditionsMet to false and continue to next trigger
							allConditionsMet=false;
							break;
						}
						if(!allConditionsMet) {
							continue;//next trigger
						}
						//Match all LoincCodes------------------------------------------------------------------------------------------------------------------------------------------------
						//TODO:with values
						//Match all Vitals----------------------------------------------------------------------------------------------------------------------------------------------------
						//TODO:with values
						//Match all Demographics---------------------------------------------------------------------------------------------------------------------------------------------
						//if(listEhrTriggers[i].DemographicAgeGreaterThan!=0){
						//	if(PatCur.Birthdate.Year>1880 && PatCur.Birthdate.AddYears(listEhrTriggers[i].DemographicAgeGreaterThan)>DateTime.Now){//patient too young
						//		continue;//next trigger
						//	}
						//}
						//if(listEhrTriggers[i].DemographicAgeLessThan!=0){
						//	if(PatCur.Birthdate.Year>1880 && PatCur.Birthdate.AddYears(listEhrTriggers[i].DemographicAgeGreaterThan)<DateTime.Now){//patient too old
						//		continue;//next trigger
						//	}
						//}
						//if(listEhrTriggers[i].DemographicGender!=""){
						//	if(!listEhrTriggers[i].DemographicGender.Contains(PatCur.Gender.ToString())){//Patient Gender not in gender list of trigger
						//		continue;//next trigger
						//	}
						//}
						//TODO: construct trigger message using all the codes in the trigger.
						break;
				}//end switch trigger cardinality
				retVal.Add(triggerMessage,ListObjectMatches);
			}
			return retVal;
		}

		///<summary></summary>
		public static long Insert(EhrTrigger ehrTrigger) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				ehrTrigger.EhrTriggerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrTrigger);
				return ehrTrigger.EhrTriggerNum;
			}
			return Crud.EhrTriggerCrud.Insert(ehrTrigger);
		}

		///<summary></summary>
		public static void Update(EhrTrigger ehrTrigger) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrTrigger);
				return;
			}
			Crud.EhrTriggerCrud.Update(ehrTrigger);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<AutoTrigger> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<AutoTrigger>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM autotrigger WHERE PatNum = "+POut.Long(patNum);
			return Crud.AutoTriggerCrud.SelectMany(command);
		}

		///<summary>Gets one AutoTrigger from the db.</summary>
		public static AutoTrigger GetOne(long automationTriggerNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<AutoTrigger>(MethodBase.GetCurrentMethod(),automationTriggerNum);
			}
			return Crud.AutoTriggerCrud.SelectOne(automationTriggerNum);
		}

		///<summary></summary>
		public static long Insert(AutoTrigger autoTrigger){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				autoTrigger.AutomationTriggerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),autoTrigger);
				return autoTrigger.AutomationTriggerNum;
			}
			return Crud.AutoTriggerCrud.Insert(autoTrigger);
		}

		///<summary></summary>
		public static void Update(AutoTrigger autoTrigger){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoTrigger);
				return;
			}
			Crud.AutoTriggerCrud.Update(autoTrigger);
		}

		///<summary></summary>
		public static void Delete(long automationTriggerNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),automationTriggerNum);
				return;
			}
			string command= "DELETE FROM autotrigger WHERE AutomationTriggerNum = "+POut.Long(automationTriggerNum);
			Db.NonQ(command);
		}
		*/



	}
}