using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EduResources{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EduResources.</summary>
		private static List<EduResource> listt;

		///<summary>A list of all EduResources.</summary>
		public static List<EduResource> Listt{
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
			string command="SELECT * FROM eduresource ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EduResource";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EduResourceCrud.TableToList(table);
		}
		#endregion

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
						if(eduResourceListAll[i].LabResultID==labResultList[j].TestName) {//TODO: How do we match vs labresults
							retVal.Add(eduResourceListAll[i]);
						}
					}
				}
			}
			return retVal;
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

		///<summary></summary>
		public static long Insert(EduResource eduResource){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				eduResource.EduResourceNum=Meth.GetLong(MethodBase.GetCurrentMethod(),eduResource);
				return eduResource.EduResourceNum;
			}
			return Crud.EduResourceCrud.Insert(eduResource);
		}

		///<summary></summary>
		public static void Update(EduResource eduResource){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),eduResource);
				return;
			}
			Crud.EduResourceCrud.Update(eduResource);
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
		*/



	}
}