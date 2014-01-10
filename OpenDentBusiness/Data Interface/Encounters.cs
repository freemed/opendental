using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Encounters{

		///<summary></summary>
		public static List<Encounter> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Encounter>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM encounter WHERE PatNum = "+POut.Long(patNum)+" ORDER BY DateEncounter";
			return Crud.EncounterCrud.SelectMany(command);
		}

		///<summary>Gets one Encounter from the db.</summary>
		public static Encounter GetOne(long encounterNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Encounter>(MethodBase.GetCurrentMethod(),encounterNum);
			}
			return Crud.EncounterCrud.SelectOne(encounterNum);
		}

		///<summary>Automatically generate and insert encounter as long as there is no other encounter with that date and provider for that patient.  Does not insert an encounter if one of the CQM default encounter prefs are invalid.</summary>
		public static void InsertDefaultEncounter(long patNum, long provNum, DateTime date) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum,provNum,date);
				return;
			}
			//Validate prefs. If they are not set, we have nothing to insert so no reason to check.
			if(PrefC.GetString(PrefName.CQMDefaultEncounterCodeSystem)=="" || PrefC.GetString(PrefName.CQMDefaultEncounterCodeValue)=="none"){
				return;
			}
			//If no encounter for date for this patient
			string command="SELECT COUNT(*) NumEncounters FROM encounter WHERE encounter.PatNum="+POut.Long(patNum)+" "
				+"AND encounter.DateEncounter="+POut.Date(date)+" "
				+"AND encounter.ProvNum="+POut.Long(provNum);
			int count=PIn.Int(Db.GetCount(command));
			if(count > 0) { //Encounter already exists for date
				return;
			}
			//Insert encounter with default encounter code system and code value set in Setup>EHR>Settings
			Encounter encounter = new Encounter();
			encounter.PatNum=patNum;
			encounter.ProvNum=provNum;
			encounter.DateEncounter=date;
			encounter.CodeSystem=PrefC.GetString(PrefName.CQMDefaultEncounterCodeSystem);
			encounter.CodeValue=PrefC.GetString(PrefName.CQMDefaultEncounterCodeValue);
			Insert(encounter);
		}

		///<summary></summary>
		public static long Insert(Encounter encounter) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				encounter.EncounterNum=Meth.GetLong(MethodBase.GetCurrentMethod(),encounter);
				return encounter.EncounterNum;
			}
			return Crud.EncounterCrud.Insert(encounter);
		}

		///<summary></summary>
		public static void Update(Encounter encounter) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),encounter);
				return;
			}
			Crud.EncounterCrud.Update(encounter);
		}

		///<summary></summary>
		public static void Delete(long encounterNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),encounterNum);
				return;
			}
			string command= "DELETE FROM encounter WHERE EncounterNum = "+POut.Long(encounterNum);
			Db.NonQ(command);
		}


	}
}