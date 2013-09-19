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