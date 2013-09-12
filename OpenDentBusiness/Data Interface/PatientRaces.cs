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

		///<summary>Gets all patintrace entries for the patient and returns all of their races as a list of ints.  The list of ints corresponds to the PatRace enum.</summary>
		public static List<int> GetPatRaceList(long patNum) {
			//No need to check RemotingRole; no call to db.
			List<PatientRace> patEntries=GetForPatient(patNum);
			List<int> listPatRace=new List<int>();
			for(int i=0;i<patEntries.Count;i++) {
				listPatRace.Add((int)patEntries[i].Race);
			}
			return listPatRace;
		}

		///<summary>Returns the PatientRaceOld enum based on the PatientRace entries for the patient passed in.  Calls GetPatRaceList to get the list of races.</summary>
		public static PatientRaceOld GetPatientRaceOldFromPatientRaces(long patNum) {
			//No need to check RemotingRole; no call to db.
			List<int> races=GetPatRaceList(patNum);
			if(races.Count==0) {
				return(PatientRaceOld.Unknown);//Unknown is default for PatientRaceOld
			}
			if(races.Contains((int)PatRace.White)) {
				if(races.Contains((int)PatRace.Hispanic)) {
					return PatientRaceOld.HispanicLatino;
				}
				return PatientRaceOld.White;
			}
			if(races.Contains((int)PatRace.AfricanAmerican)) {
				if(races.Contains((int)PatRace.Hispanic)) {
					return PatientRaceOld.BlackHispanic;
				}
				return PatientRaceOld.AfricanAmerican;
			}
			if(races.Contains((int)PatRace.Aboriginal)) {
				return PatientRaceOld.Aboriginal;
			}
			if(races.Contains((int)PatRace.AmericanIndian)) {
				return PatientRaceOld.AmericanIndian;
			}
			if(races.Contains((int)PatRace.Asian)) {
				return PatientRaceOld.Asian;
			}
			if(races.Contains((int)PatRace.HawaiiOrPacIsland)) {
				return PatientRaceOld.HawaiiOrPacIsland;
			}
			if(races.Contains((int)PatRace.Multiracial)) {
				return PatientRaceOld.Multiracial;
			}
			if(races.Contains((int)PatRace.Other)) {
				return PatientRaceOld.Other;
			}
			//Hispanic
			//DeclinedToSpecify
			return PatientRaceOld.Unknown;
		}

		///<summary>Gets a list of PatRaces that correspond to a PatientRaceOld enum.</summary>
		public static List<PatRace> GetPatRacesFromPatientRaceOld(PatientRaceOld raceOld) {
			List<PatRace> retVal=new List<PatRace>();
			switch(raceOld) {
				case PatientRaceOld.Unknown:
					//Do nothing.  No entry means "Unknown", the old default.
					break;
				case PatientRaceOld.Multiracial:
					retVal.Add(PatRace.Multiracial);
					break;
				case PatientRaceOld.HispanicLatino:
					retVal.Add(PatRace.White);
					retVal.Add(PatRace.Hispanic);
					break;
				case PatientRaceOld.AfricanAmerican:
					retVal.Add(PatRace.AfricanAmerican);
					break;
				case PatientRaceOld.White:
					retVal.Add(PatRace.White);
					break;
				case PatientRaceOld.HawaiiOrPacIsland:
					retVal.Add(PatRace.HawaiiOrPacIsland);
					break;
				case PatientRaceOld.AmericanIndian:
					retVal.Add(PatRace.AmericanIndian);
					break;
				case PatientRaceOld.Asian:
					retVal.Add(PatRace.Asian);
					break;
				case PatientRaceOld.Other:
					retVal.Add(PatRace.Other);
					break;
				case PatientRaceOld.Aboriginal:
					retVal.Add(PatRace.Aboriginal);
					break;
				case PatientRaceOld.BlackHispanic:
					retVal.Add(PatRace.AfricanAmerican);
					retVal.Add(PatRace.Hispanic);
					break;
			}
			return retVal;
		}

		///<summary>Inserts or Deletes neccesary PatientRace entries for the specified patient given the list of PatRaces provided.</summary>
		public static void Reconcile(long patNum,List<PatRace> listPatRaces) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum,listPatRaces);
				return;
			}
			string command;
			if(listPatRaces.Count==0) { //DELETE all for the patient if listPatRaces is empty.
				command="DELETE FROM patientrace WHERE PatNum = "+POut.Long(patNum);//Can't use CRUD layer here because there might be multiple races for one patient.
				Db.NonQ(command);
				return;
			}
			List<PatientRace> listPatientRaces;//Rename this variable and the listPatRaces variable so it is easier to indicate which is the "selected" list and which is the db list.
			command = "SELECT * FROM patientrace WHERE PatNum = "+POut.Long(patNum);
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