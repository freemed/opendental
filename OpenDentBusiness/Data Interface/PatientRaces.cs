using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PatientRaces{

		///<summary>Gets all PatientRace enteries from the db.</summary>
		public static List<PatientRace> GetForPatient(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PatientRace>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command = "SELECT * FROM patientrace WHERE PatNum = "+POut.Long(patNum);
			return Crud.PatientRaceCrud.SelectMany(command);
		}

		///<summary>Inserts or Deletes neccesary PatientRace entries for the specified patient given the list of PatRaces provided.</summary>
		public static void Reconcile(long patNum,List<PatRace> listPatRaces) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum,listPatRaces);
				return;
			}
			List<PatientRace> listPatientRaces;
			string command = "SELECT * FROM patientrace WHERE PatNum = "+POut.Long(patNum);
			listPatientRaces = Crud.PatientRaceCrud.SelectMany(command);
			//delete excess rows
			for(int i=0;i<listPatientRaces.Count;i++) {
				if(!listPatRaces.Contains((PatRace)listPatientRaces[i].Race)) {//if there is a PatientRace row that does not match the new list of PatRaces, delete it
					Crud.PatientRaceCrud.Delete(listPatientRaces[i].PatientRaceNum);
				}
			}
			//insert new rows
			for(int i=0;i<listPatRaces.Count;i++) {
				bool insertNeeded=true;
				for(int j=0;j<listPatientRaces.Count;j++) {
					if(listPatRaces[i]==listPatientRaces[j].Race) {
						insertNeeded=false;
					}
				}
				if(insertNeeded) {
					PatientRace pr=new PatientRace();
					pr.PatNum=patNum;
					pr.Race=listPatRaces[i];
					Crud.PatientRaceCrud.Insert(pr);
				}
				//next PatRace
			}
			//return;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<PatientRace> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PatientRace>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM patientrace WHERE PatNum = "+POut.Long(patNum);
			return Crud.PatientRaceCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(PatientRace patientRace){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				patientRace.PatientRaceNum=Meth.GetLong(MethodBase.GetCurrentMethod(),patientRace);
				return patientRace.PatientRaceNum;
			}
			return Crud.PatientRaceCrud.Insert(patientRace);
		}

		///<summary></summary>
		public static void Delete(long patientRaceNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patientRaceNum);
				return;
			}
			string command= "DELETE FROM patientrace WHERE PatientRaceNum = "+POut.Long(patientRaceNum);
			Db.NonQ(command);
		}
		*/



	}
}