using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EduResources{
		///<summary></summary>
		public static List<EduResource> SelectAllForPatient(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EduResource>>(MethodBase.GetCurrentMethod(),patNum);
			}
			List<Disease> diseaseList = Diseases.Refresh(patNum);
			List<MedicationPat> medicationPatList = MedicationPats.GetList(patNum);
			List<LabResult> labResultList = LabResults.GetAllForPatient(patNum);
			List<EduResource> eduResourceListAll = Crud.EduResourceCrud.SelectMany("SELECT * FROM eduresource");
			List<EduResource> retVal = new List<EduResource>();
			for(int i=0;i<eduResourceListAll.Count;i++) {
				if(eduResourceListAll[i].DiseaseDefNum!=0) {
					for(int j=0;j<diseaseList.Count;j++) {
						if(eduResourceListAll[i].DiseaseDefNum==diseaseList[j].DiseaseDefNum) {
							retVal.Add(eduResourceListAll[i]);
						}
					}
				}
				else if(eduResourceListAll[i].MedicationNum!=0) {
					for(int j=0;j<medicationPatList.Count;j++) {
						if(eduResourceListAll[i].MedicationNum==medicationPatList[j].MedicationNum) {
							retVal.Add(eduResourceListAll[i]);
						}
					}
				}
				else if(eduResourceListAll[i].LabResultID!="") {
					for(int j=0;j<labResultList.Count;j++) {
						if(eduResourceListAll[i].LabResultID!=labResultList[j].TestID) {
							continue;
						}
						//TODO: How do we match vs labresults
						if(eduResourceListAll[i].LabResultCompare.StartsWith("<")){
							try{
								if(int.Parse(labResultList[j].ObsValue)<int.Parse(eduResourceListAll[i].LabResultCompare.Substring(1))){
									retVal.Add(eduResourceListAll[i]);
								}
							}
							catch{
								//Because LabResult.ObsValue can only be stored as > or < followed by an int, this case should never be reached. fail silently
								//In the future there may be non intiger values that we would not want this to fail on.
							}
							//labResultList[j].ObsValue
						}
						else if(eduResourceListAll[i].LabResultCompare.StartsWith(">")){

						}
						else{
							//Might be used in the future to match with non < or > ...TODO:
							//error?
						}
						//retVal.Add(eduResourceListAll[i]);
					}
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static List<EduResource> SelectAll(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EduResource>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM eduresource";
			return Crud.EduResourceCrud.SelectMany(command);
		}

		///<summary></summary>
		public static void Delete(long eduResourceNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),eduResourceNum);
				return;
			}
			string command= "DELETE FROM eduresource WHERE EduResourceNum = "+POut.Long(eduResourceNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(EduResource eduResource) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				eduResource.EduResourceNum=Meth.GetLong(MethodBase.GetCurrentMethod(),eduResource);
				return eduResource.EduResourceNum;
			}
			return Crud.EduResourceCrud.Insert(eduResource);
		}

		///<summary></summary>
		public static void Update(EduResource eduResource) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),eduResource);
				return;
			}
			Crud.EduResourceCrud.Update(eduResource);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EduResource> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EduResource>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM eduresource WHERE PatNum = "+POut.Long(patNum);
			return Crud.EduResourceCrud.SelectMany(command);
		}

		///<summary>Gets one EduResource from the db.</summary>
		public static EduResource GetOne(long eduResourceNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EduResource>(MethodBase.GetCurrentMethod(),eduResourceNum);
			}
			return Crud.EduResourceCrud.SelectOne(eduResourceNum);
		}



		*/



	}
}