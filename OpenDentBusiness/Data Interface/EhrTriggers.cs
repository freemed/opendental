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
		/// <param name="triggerObject">Can be DiseaseDef, ICD9, Icd10, Snomed, Medication, RxNorm, Cvx, AllerfyDef, EHRLabResult, Patient, or VitalSign.</param>
		/// <param name="PatCur">Triggers and intervention are currently always dependant on current patient. </param>
		/// <returns>Returns a dictionary keyed on triggers and a list of all the objects that the trigger matched on. Should be used to generate CDS intervention message and later be passed to FormInfobutton for knowledge request.</returns>
		public static List<CDSIntervention> TriggerMatch(object triggerObject,Patient PatCur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CDSIntervention>>(MethodBase.GetCurrentMethod(),triggerObject,PatCur);
			}
			//Dictionary<string,List<object>> retVal=new Dictionary<string,List<object>>();
			List<CDSIntervention> retVal=new List<CDSIntervention>();
			//Define objects to be used in matching triggers.
			DiseaseDef diseaseDef;
			ICD9 icd9;
			Icd10 icd10;
			Snomed snomed;
			Medication medication;
			RxNorm rxNorm;
			Cvx cvx;
			AllergyDef allergyDef;
			EhrLabResult ehrLabResult;
			Patient pat;
			Vitalsign vitalsign;
			string triggerObjectMessage="";
			string command="";
			switch(triggerObject.GetType().Name) {
				case "DiseaseDef":
					diseaseDef=(DiseaseDef)triggerObject;
					command="SELECT * FROM ehrtrigger"
					+" WHERE ProblemDefNumList LIKE '% "+POut.String(diseaseDef.DiseaseDefNum.ToString())+" %'";// '% <code> %' so that we can get exact matches.
					if(diseaseDef.ICD9Code!="") {
						command+=" OR ProblemIcd9List LIKE '% "+POut.String(diseaseDef.ICD9Code)+" %'";
						triggerObjectMessage+="  -"+diseaseDef.ICD9Code+"(Icd9)  "+ICD9s.GetByCode(diseaseDef.ICD9Code).Description+"\r\n";
					}
					if(diseaseDef.Icd10Code!="") {
						command+=" OR ProblemIcd10List LIKE '% "+POut.String(diseaseDef.Icd10Code)+" %'";
						triggerObjectMessage+="  -"+diseaseDef.Icd10Code+"(Icd10)  "+Icd10s.GetByCode(diseaseDef.Icd10Code).Description+"\r\n";
					}
					if(diseaseDef.SnomedCode!="") {
						command+=" OR ProblemSnomedList LIKE '% "+POut.String(diseaseDef.SnomedCode)+" %'";
						triggerObjectMessage+="  -"+diseaseDef.SnomedCode+"(Snomed)  "+Snomeds.GetByCode(diseaseDef.SnomedCode).Description+"\r\n";
					}
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
				case "Medication":
					medication=(Medication)triggerObject;
					triggerObjectMessage="  - "+medication.MedName+(medication.RxCui==0?"":" (RxCui:"+RxNorms.GetByRxCUI(medication.RxCui.ToString()).RxCui+")")+"\r\n";
					command="SELECT * FROM ehrtrigger"
					+" WHERE MedicationNumList LIKE '% "+POut.String(medication.MedicationNum.ToString())+" %'";// '% <code> %' so that we can get exact matches.
					if(medication.RxCui!=0) {
						command+=" OR RxCuiList LIKE '% "+POut.String(medication.RxCui.ToString())+" %'";// '% <code> %' so that we can get exact matches.
					}
					break;
				case "RxNorm":
					rxNorm=(RxNorm)triggerObject;
					triggerObjectMessage="  - "+rxNorm.Description+"(RxCui:"+rxNorm.RxCui+")\r\n";
					command="SELECT * FROM ehrtrigger"
					+" WHERE RxCuiList LIKE '% "+POut.String(rxNorm.RxCui)+" %'";// '% <code> %' so that we can get exact matches.
					break;
				case "Cvx":
					cvx=(Cvx)triggerObject;
					//TODO: TriggerObjectMessage
					command="SELECT * FROM ehrtrigger"
					+" WHERE CvxList LIKE '% "+POut.String(cvx.CvxCode)+" %'";// '% <code> %' so that we can get exact matches.
					break;
				case "AllergyDef":
					allergyDef=(AllergyDef)triggerObject;
					//TODO: TriggerObjectMessage
					command="SELECT * FROM ehrtrigger"
					+" WHERE AllergyDefNumList LIKE '% "+POut.String(allergyDef.AllergyDefNum.ToString())+" %'";// '% <code> %' so that we can get exact matches.
					break;
				case "EhrLabResult"://match loinc only, no longer 
					ehrLabResult=(EhrLabResult)triggerObject;
					//TODO: TriggerObjectMessage
					command="SELECT * FROM ehrtrigger WHERE "
						+"(LabLoincList LIKE '% "+ehrLabResult.ObservationIdentifierID+" %'" //LOINC may be in one of two fields
						+"OR LabLoincList LIKE '% "+ehrLabResult.ObservationIdentifierIDAlt+" %')"; //LOINC may be in one of two fields
					break;
				case "Patient":
					pat=(Patient)triggerObject;
					//TODO: TriggerObjectMessage
					//command="SELECT * FROM ehrtrigger"
					//Age todo
					//+" WHERE SnomedList LIKE '% "+POut.String(snomed.SnomedCode)+" %'";// '% <code> %' so that we can get exact matches.
					//Gender todo
					break;
				case "VitalSign":
					vitalsign=(Vitalsign)triggerObject;
					//TODO: TriggerObjectMessage
					//command="SELECT * FROM ehrtrigger"
						//Height
						//Weight
						//BP
						//BMI
					//+" WHERE SnomedList LIKE '% "+POut.String(snomed.SnomedCode)+" %'";// '% <code> %' so that we can get exact matches.
					break;
				default:
					//command="SELECT * FROM ehrtrigger WHERE false";//should not return any results.
					return null;
					#if DEBUG
						throw new Exception(triggerObject.GetType().ToString()+" object not implemented as intervention trigger yet. Add to the list above to handle.");
					#endif
					//break;
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
				CDSIntervention cdsi=new CDSIntervention();
				cdsi.EhrTrigger=listEhrTriggers[i];
				cdsi.InterventionMessage=triggerMessage;
				cdsi.TriggerObjects=ListObjectMatches;
				retVal.Add(cdsi);
			}
			//Fill object lists to be checked-------------------------------------------------------------------------------------------------
			List<Allergy> ListAllergy=Allergies.GetAll(PatCur.PatNum,false);
			List<Disease> ListDisease=Diseases.Refresh(PatCur.PatNum,true);
			List<DiseaseDef> ListDiseaseDef=new List<DiseaseDef>();
			List<EhrLab> ListEhrLab=EhrLabs.GetAllForPat(PatCur.PatNum);
			//List<EhrLabResult> ListEhrLabResults=null;//Lab results are stored in a list in the EhrLab object.
			List<MedicationPat> ListMedicationPat=MedicationPats.Refresh(PatCur.PatNum,false);
			List<AllergyDef> ListAllergyDef=new List<AllergyDef>();
			for(int i=0;i<ListAllergy.Count;i++){
				ListAllergyDef.Add(AllergyDefs.GetOne(ListAllergy[i].AllergyDefNum));
			}
			for(int i=0;i<ListDisease.Count;i++){
				ListDiseaseDef.Add(DiseaseDefs.GetItem(ListDisease[i].DiseaseDefNum));
			}
			for(int i=0;i<listEhrTriggers.Count;i++) {
				if(listEhrTriggers[i].Cardinality==MatchCardinality.One) {
					continue;//we handled these above.
				}
				string triggerMessage=listEhrTriggers[i].Description+":\r\n";
				triggerMessage+=triggerObjectMessage;
				List<object> ListObjectMatches=new List<object>();//Allergy, Disease, LabPanels, MedicationPat, Patient, VaccinePat
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
					case MatchCardinality.OneOfEachCategory://falls through to two or more, but then branches at the end of the case statement.
					case MatchCardinality.TwoOrMore:
						//if(ListObjectMatches.Count<2) {
						//	continue;//Must match at least two objects for this category.
						//}
						//Medication
						for(int m=0;m<ListMedicationPat.Count;m++) {
							if(listEhrTriggers[i].MedicationNumList.Contains(" "+ListMedicationPat[m].MedicationNum+" ")) {
								ListObjectMatches.Add(ListMedicationPat[m]);
								continue;
							}
							if(ListMedicationPat[m].RxCui!=0 
								&& listEhrTriggers[i].RxCuiList.Contains(" "+ListMedicationPat[m].RxCui+" ")) 
							{
								ListObjectMatches.Add(ListMedicationPat[m]);
								continue;
							}
						}
						//Allergy
						for(int a=0;a<ListAllergy.Count;a++) {
							if(listEhrTriggers[i].AllergyDefNumList.Contains(" "+ListAllergy[a].AllergyDefNum+" ")) {
								ListObjectMatches.Add(AllergyDefs.GetOne(ListAllergy[a].AllergyDefNum));
								triggerMessage+="  -(Allergy) "+AllergyDefs.GetOne(ListAllergy[a].AllergyDefNum).Description+"\r\n";
								continue;
							}
						}
						//Problem
						for(int d=0;d<ListDiseaseDef.Count;d++) {
							if(ListDiseaseDef[d].ICD9Code!=""
								&& listEhrTriggers[i].ProblemIcd9List.Contains(" "+ListDiseaseDef[d].ICD9Code+" ")) 
							{
								ListObjectMatches.Add(ListDiseaseDef[d]);
								triggerMessage+="  -(ICD9) "+ICD9s.GetByCode(ListDiseaseDef[d].ICD9Code).Description+"\r\n";
								continue;
							}
							if(ListDiseaseDef[d].Icd10Code!=""
								&& listEhrTriggers[i].ProblemIcd10List.Contains(" "+ListDiseaseDef[d].Icd10Code+" ")) {
								ListObjectMatches.Add(ListDiseaseDef[d]);
								triggerMessage+="  -(Icd10) "+Icd10s.GetByCode(ListDiseaseDef[d].Icd10Code).Description+"\r\n";
								continue;
							}
							if(ListDiseaseDef[d].SnomedCode!=""
								&& listEhrTriggers[i].ProblemSnomedList.Contains(" "+ListDiseaseDef[d].SnomedCode+" ")) {
								ListObjectMatches.Add(ListDiseaseDef[d]);
								triggerMessage+="  -(Snomed) "+Snomeds.GetByCode(ListDiseaseDef[d].SnomedCode).Description+"\r\n";
								continue;
							}
							if(listEhrTriggers[i].ProblemDefNumList.Contains(" "+ListDiseaseDef[d].DiseaseDefNum+" ")) {
								ListObjectMatches.Add(ListDiseaseDef[d]);
								triggerMessage+="  -(Problem Def) "+ListDiseaseDef[d].DiseaseName+"\r\n";
								continue;
							}
						}
						//Vital
						//TODO
						//Age
						//TODO
						//Gender
						//TODO
						//Lab Result
						for(int l=0;l<ListEhrLab.Count;l++) {
							for(int r=0;r<ListEhrLab[l].ListEhrLabResults.Count;r++) {
								if(listEhrTriggers[i].LabLoincList.Contains(" "+ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierID+" ")
									|| listEhrTriggers[i].LabLoincList.Contains(" "+ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierIDAlt+" ")) 
								{
									ListObjectMatches.Add(ListEhrLab[l].ListEhrLabResults[r]);
									if (ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierID!=""){//should almost always be the case.
										triggerMessage+="  -(LOINC) "+Loincs.GetByCode(ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierID).NameShort+"\r\n";
									}
									else if (ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierID!="") {
										triggerMessage+="  -(LOINC) "+Loincs.GetByCode(ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierIDAlt).NameShort+"\r\n";
									}
									else if(ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierText!="") {
										triggerMessage+="  -(LOINC) "+ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierText+"\r\n";
									}
									else if(ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierTextAlt!="") {
										triggerMessage+="  -(LOINC) "+ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierTextAlt+"\r\n";
									}
									else if(ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierID!="") {
										triggerMessage+="  -(LOINC) "+ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierID+"\r\n";
									}
									else if(ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierIDAlt!="") {
										triggerMessage+="  -(LOINC) "+ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierIDAlt+"\r\n";
									}
									else if(ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierTextOriginal!="") {
										triggerMessage+="  -(LOINC) "+ListEhrLab[l].ListEhrLabResults[r].ObservationIdentifierTextOriginal+"\r\n";
									}
									else {
										triggerMessage+="  -(LOINC) Unknown code.\r\n";//should never happen.
									}
									continue;
								}
							}
						}
						ListObjectMatches=RemoveDuplicateObjectsHelper(ListObjectMatches);
						if(listEhrTriggers[i].Cardinality==MatchCardinality.TwoOrMore && ListObjectMatches.Count<2) {
							continue;//next trigger, do not add to retVal
						}
						if(listEhrTriggers[i].Cardinality==MatchCardinality.OneOfEachCategory && !OneOfEachCategoryHelper(listEhrTriggers[i],ListObjectMatches)) {
							continue;
						}
						break;
					case MatchCardinality.All:
						bool allConditionsMet=true;
						List<string> MatchedCodes=getCodesFromListHelper(ListObjectMatches);//new List<string>();
						//Match all Icd9Codes-------------------------------------------------------------------------------------------------------------------------------------------------
						string[] arrayIcd9Codes=listEhrTriggers[i].ProblemIcd9List.Split(new string[] {" "},StringSplitOptions.RemoveEmptyEntries);
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
						string[] arrayIcd10Codes=listEhrTriggers[i].ProblemIcd10List.Split(new string[] {" "},StringSplitOptions.RemoveEmptyEntries);
						for(int c=0;c<arrayIcd10Codes.Length;c++) {
							if(MatchedCodes.Contains(arrayIcd10Codes[i])){
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
							continue;//next trigger, do not add to retval
						}
						//Match all LoincCodes------------------------------------------------------------------------------------------------------------------------------------------------
						string[] arrayLoincCodes=listEhrTriggers[i].LabLoincList.Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries);
						for(int c=0;c<arrayLoincCodes.Length;c++) {
							if(MatchedCodes.Contains(arrayLoincCodes[i])) {
								continue;//found required code
							}
							//required code not found, set allConditionsMet to false and continue to next trigger
							allConditionsMet=false;
							break;
						}
						if(!allConditionsMet) {
							continue;//next trigger, do not add to retval
						}
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
				//retVal.Add(triggerMessage,ListObjectMatches);
				CDSIntervention cdsi=new CDSIntervention();
				cdsi.EhrTrigger=listEhrTriggers[i];
				cdsi.InterventionMessage=triggerMessage;
				cdsi.TriggerObjects=ListObjectMatches;
				retVal.Add(cdsi);
			}//end triggers
			return retVal;
		}

		private static List<string> getCodesFromListHelper(List<object> ListObjectMatches) {
			List<string> retVal=new List<string>();
			for(int i=0;i<ListObjectMatches.Count;i++) {
				switch(ListObjectMatches[i].GetType().Name) {
					case "DiseaseDef":
						retVal.Add(((DiseaseDef)ListObjectMatches[i]).DiseaseDefNum.ToString());
						retVal.Add(((DiseaseDef)ListObjectMatches[i]).ICD9Code);
						retVal.Add(((DiseaseDef)ListObjectMatches[i]).Icd10Code);
						retVal.Add(((DiseaseDef)ListObjectMatches[i]).SnomedCode);
						break;
					case "ICD9":
						retVal.Add(((ICD9)ListObjectMatches[i]).ICD9Code);
						break;
					case "Icd10":
						retVal.Add(((Icd10)ListObjectMatches[i]).Icd10Code);
						break;
					case "Snomed":
						retVal.Add(((Snomed)ListObjectMatches[i]).SnomedCode);
						break;
					case "Medication":
						retVal.Add(((Medication)ListObjectMatches[i]).MedicationNum.ToString());
						retVal.Add(((Medication)ListObjectMatches[i]).RxCui.ToString());
						break;
					case "RxNorm":
						retVal.Add(((RxNorm)ListObjectMatches[i]).RxCui);
						break;
					case "Cvx":
						retVal.Add(((Cvx)ListObjectMatches[i]).CvxCode);
						break;
					case "AllergyDef":
						retVal.Add(((AllergyDef)ListObjectMatches[i]).AllergyDefNum.ToString());
						break;
					case "EHRLabResult"://match loinc only 
						retVal.Add(((EhrLabResult)ListObjectMatches[i]).ObservationIdentifierID);
						retVal.Add(((EhrLabResult)ListObjectMatches[i]).ObservationIdentifierIDAlt);
						break;
					case "Patient":
						retVal.Add(((Patient)ListObjectMatches[i]).Gender.ToString());
						//Maybe more here?
						break;
					case "VitalSign":
						//retVal.Add(((Vitalsign)ListObjectMatches[i]).???);
						break;
					default:
						#if DEBUG
							throw new Exception(ListObjectMatches[i].GetType().ToString()+" object not implemented as intervention trigger yet. Add to the list above to handle.");
						#endif
						break;
				}
			}
			return retVal;
		}

		///<summary>Returns true if ListObjectMatches satisfies trigger conditions.</summary>
		private static bool OneOfEachCategoryHelper(EhrTrigger ehrTrigger,List<object> ListObjectMatches) {
			//problems
			if(ehrTrigger.ProblemDefNumList.Trim()!=""
				|| ehrTrigger.ProblemIcd9List.Trim()!=""
				|| ehrTrigger.ProblemIcd10List.Trim()!=""
				|| ehrTrigger.ProblemSnomedList.Trim()!="") 
			{
				//problem condition exists
				for(int i=0;i<ListObjectMatches.Count;i++) {
					if(ListObjectMatches[i].GetType().Name=="DiseaseDef") {
						break;//satisfied problem category, move on to next category
					}
					if(i==ListObjectMatches.Count-1) {
						return false;//made it to end of list and did not find a problem.
					}
				}//end list matches
			}//end problem
			//medication
			if(ehrTrigger.MedicationNumList.Trim()!=""
				|| ehrTrigger.RxCuiList.Trim()!=""
				|| ehrTrigger.CvxList.Trim()!="") 
			{
				//Medication condition exists
				for(int i=0;i<ListObjectMatches.Count;i++) {
					if(ListObjectMatches[i].GetType().Name=="Medication"
						|| ListObjectMatches[i].GetType().Name=="VaccineDef") {
						break;//satisfied medication category, move on to next category
					}
					if(i==ListObjectMatches.Count-1) {
						return false;//made it to end of list and did not find a problem.
					}
				}//end list matches
			}//end medication
			//allergy
			if(ehrTrigger.AllergyDefNumList.Trim()!="") {
				//Allergy condition exists
				for(int i=0;i<ListObjectMatches.Count;i++) {
					if(ListObjectMatches[i].GetType().Name=="AllergyDef") {
						break;//satisfied Allergy category, move on to next category
					}
					if(i==ListObjectMatches.Count-1) {
						return false;//made it to end of list and did not find a problem.
					}
				}//end list matches
			}//end allergy
			//lab-todo
			//vitals-todo
			//demographics-todo
			return true;
		}

		private static List<object> RemoveDuplicateObjectsHelper(List<object> ListObjectMatches) {
			List<object> retVal=new List<object>();
			for(int i=0;i<ListObjectMatches.Count;i++) {
				bool IsNew=true;
				for(int j=0;j<retVal.Count;j++) {
					if(retVal[j].GetType()!=ListObjectMatches[i].GetType()) {
						continue;
					}
					switch(retVal[j].GetType().Name) {
						case "AllergyDef":
							if(((AllergyDef)ListObjectMatches[i]).AllergyDefNum==((AllergyDef)retVal[j]).AllergyDefNum) {
								IsNew=false;
							}
							break;
						default://handle other cases as needed.
							break;
					}
				}
				if(IsNew) {
					retVal.Add(ListObjectMatches[i]);
				}
			}//next input object
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

		///<summary></summary>
		public static void Delete(long ehrTriggerNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrTriggerNum);
				return;
			}
			string command= "DELETE FROM ehrtrigger WHERE EhrTriggerNum = "+POut.Long(ehrTriggerNum);
			Db.NonQ(command);
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
		*/



	}
}