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

		///<summary>This is the first step of automation, this checks to see if the new object matches one of the trigger conditions. </summary>
		/// <param name="triggerObject">Can be DiseaseDef, Disease, RxPat, Medication, Allergy, AllergyDef, Snomed, Icd9, Icd10, RxNorm, Cvx, Patient, or null. If patient, will check demographics. if null, will check all existing triggers.</param>
		/// <param name="PatCur">Triggers and intervention are currently always dependant on current patient. </param>
		/// <returns>Returns a dictionary keyed on triggers and a list of all the objects that the trigger matched on. Should be used to generate CDS intervention message and later be passed to FormInfobutton for knowledge request.</returns>
		public static Dictionary<EhrTrigger,List<object>> TriggerMatch(object triggerObject,Patient PatCur) {
			Dictionary<EhrTrigger,List<object>> retVal=new Dictionary<EhrTrigger,List<object>>();
			string command="";
			switch(triggerObject.GetType().Name) {
				case "DiseaseDef":
					DiseaseDef diseaseDef=(DiseaseDef)triggerObject;
					command="SELECT * FROM autotrigger"
					+" WHERE Icd9List LIKE '%"+POut.String(diseaseDef.ICD9Code)+"%'"
					+" OR Icd10List LIKE ' "+POut.String(diseaseDef.Icd10Code)+" '"
					+" OR SnomedList LIKE '%"+POut.String(diseaseDef.SnomedCode)+"%'";
					break;
				case "Disease":
					break;
				case "RxPat":
				default:
					#if DEBUG
						throw new Exception(triggerObject.GetType().ToString()+" object not implemented as intervention trigger yet. Add to the list above to handle.");
					#endif
					break;
			}
			List<EhrTrigger> listAutoTriggers=Crud.AutoTriggerCrud.SelectMany(command);
			if(listAutoTriggers.Count==0){
				return null;//no triggers matched.
			}
			//Fill object lists to be checked-------------------------------------------------------------------------------------------------
			command="SELECT * FROM Allergy WHERE PatNum="+PatCur.PatNum;
			List<Allergy> ListAllergy=Crud.AllergyCrud.SelectMany(command);
			List<AllergyDef> ListAllergyDef=new List<AllergyDef>();
			for(int i=0;i<ListAllergy.Count;i++){
				//ListAllergyDef
			}
			for(int i=0;i<listAutoTriggers.Count;i++) {
				List<object> ListObjectMatches=new List<object>();
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
				//RxPat.RxCui
				//VaccinePat--------------------------------------------------------------------------------------------------------------------
				//VaccinePat.VaccineDefNum>>VaccineDef.CVXCode
				//VitalSign---------------------------------------------------------------------------------------------------------------------
				//VitalSign.Height (Loinc)
				//VitalSign.Weight (Loinc)
				//VitalSign.BpSystolic (Loinc)
				//VitalSign.BpDiastolic (Loinc)
				//VitalSign.WeightCode (Snomed)
				//VitalSign.PregDiseaseNum (Loinc)
				retVal.Add(listAutoTriggers[i],ListObjectMatches);
			}
			return retVal;
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