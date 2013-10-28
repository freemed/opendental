using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrCodes{
		#region CachePattern
		//Atypical cache pattern. Cache is only filled when we have access to the EHR.dll file, otherwise listt will be an empty list of EHR codes (not null, just empty as if the table were there but with no codes in it.)

		///<summary>A list of all EhrCodes.</summary>
		private static List<EhrCode> listt;

		///<summary>A list of all EhrCodes.</summary>
		public static List<EhrCode> Listt{
			get {
				if(listt==null) {//instead of refreshing the cache using the normal pattern we must retrieve the cache from the EHR.dll. No call to DB.
					object ObjEhrCodeList;
					Assembly AssemblyEHR;
					string dllPathEHR=CodeBase.ODFileUtils.CombinePaths(System.Windows.Forms.Application.StartupPath,"EHR.dll");
					#if EHRTEST
						ObjFormEhrMeasures=new FormEhrMeasures();
						listt=Crud.EhrCodeCrud.TableToList(EHR.EhrCodeList.GetListt());
					#else
						ObjEhrCodeList=null;
						AssemblyEHR=null;
						if(System.IO.File.Exists(dllPathEHR)) {//EHR.dll is available, so load it up
							AssemblyEHR=Assembly.LoadFile(dllPathEHR);
							Type type=AssemblyEHR.GetType("EHR.EhrCodeList");//namespace.class
							ObjEhrCodeList=Activator.CreateInstance(type);
							object[] args=null;
							listt=Crud.EhrCodeCrud.TableToList((DataTable)type.InvokeMember("GetListt",System.Reflection.BindingFlags.InvokeMethod,null,ObjEhrCodeList,args));
						}
						else {//no EHR.dll. "Return" empty list.
							listt=new List<EhrCode>();
						}
					#endif
					updateCodeExistsHelper();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		/// <summary>If the CodeValue of the EhrCode exists in its respective code table (I.e. Snomed, Loinc, Cpt, etc.) this will set ExistsInDbTable=true otherwise false.</summary>
		private static void updateCodeExistsHelper() {
			if(listt.Count==0){
				return;//
			}
			//Cache lists of codes.
			//HashSet<string> cdcrecHS	= new HashSet<string>(Cdcrecs.GetAllCodes());//all CDCREC codes should always be inserted into DB via ConvertDB3
			HashSet<string> cdtHS			= new HashSet<string>(ProcedureCodes.GetAllCodes());
			HashSet<string> cptHS			= new HashSet<string>(Cpts					.GetAllCodes());
			HashSet<string> cvxHS			= new HashSet<string>(Cvxs					.GetAllCodes());
			HashSet<string> hcpcsHS		= new HashSet<string>(Hcpcses				.GetAllCodes());
			HashSet<string> icd10HS		= new HashSet<string>(Icd10s				.GetAllCodes());
			HashSet<string> icd9HS		= new HashSet<string>(ICD9s					.GetAllCodes());
			HashSet<string> loincHS		= new HashSet<string>(Loincs				.GetAllCodes());
			HashSet<string> rxnormHS	= new HashSet<string>(RxNorms				.GetAllCodes());
			HashSet<string> snomedHS	= new HashSet<string>(Snomeds				.GetAllCodes());
			HashSet<string> sopHS			= new HashSet<string>(Sops					.GetAllCodes());
			for(int i=0;i<listt.Count;i++) {
				switch(listt[i].CodeSystem) {
					case "AdministrativeSex"://always "in DB", even though there is no DB table 
						listt[i].ExistsInDbTable=true;
						break;
					case "CDCREC"://always in DB
						listt[i].ExistsInDbTable=true;
						break;
					case "CDT":
						listt[i].ExistsInDbTable=cdtHS.Contains(listt[i].CodeValue);
						break;
					case "CPT":
						listt[i].ExistsInDbTable=cptHS.Contains(listt[i].CodeValue);
						break;
					case "CVX":
						listt[i].ExistsInDbTable=cvxHS.Contains(listt[i].CodeValue);
						break;
					case "HCPCS":
						listt[i].ExistsInDbTable=hcpcsHS.Contains(listt[i].CodeValue);
						break;
					case "ICD9CM":
						listt[i].ExistsInDbTable=icd9HS.Contains(listt[i].CodeValue);
						break;
					case "ICD10CM":
						listt[i].ExistsInDbTable=icd10HS.Contains(listt[i].CodeValue);
						break;
					case "LOINC":
						listt[i].ExistsInDbTable=loincHS.Contains(listt[i].CodeValue);
						break;
					case "RXNORM":
						listt[i].ExistsInDbTable=rxnormHS.Contains(listt[i].CodeValue);
						break;
					case "SNOMEDCT":
						listt[i].ExistsInDbTable=snomedHS.Contains(listt[i].CodeValue);
						break;
					case "SOP":
						listt[i].ExistsInDbTable=sopHS.Contains(listt[i].CodeValue);
						break;
				}
			}

			//This updates the last column "ExistsInDatabse" based on weather or not the code is found in another table in the database.

		}

		///<summary></summary>
		public static DataTable RefreshCache() {
			//==Ryan--In the future, this will not be an actual DB table and will fill this list based on an obfuscated method in the ehr.dll library.
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM ehrcode ORDER BY EhrCodeNum";//Order by is important, since combo boxes will have codes in them in the same order as this table
			DataTable table=new DataTable("EhrCode");
			Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrCode";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrCodeCrud.TableToList(table);
		}
		#endregion

		///<summary></summary>
		public static string GetMeasureIdsForCode(string codeValue,string codeSystem) {
			//No need to check RemotingRole; no call to db.
			string retval="";
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].CodeValue==codeValue && Listt[i].CodeSystem==codeSystem) {
					if(retval.Contains(Listt[i].MeasureIds)) {
						continue;
					}
					if(retval!="") {
						retval+=",";
					}
					retval+=Listt[i].MeasureIds;
				}
			}
			return retval;
		}

		///<summary>Returns a list of EhrCode objects that belong to one of the value sets identified by the ValueSetOIDs supplied.</summary>
		public static List<EhrCode> GetForValueSetOIDs(List<string> listValueSetOIDs) {
			return GetForValueSetOIDs(listValueSetOIDs,false);
		}

		///<summary>Returns a list of EhrCode objects that belong to one of the value sets identified by the ValueSetOIDs supplied AND only those codes that exist in the corresponding table in the database.</summary>
		public static List<EhrCode> GetForValueSetOIDs(List<string> listValueSetOIDs,bool ifExistsInDb) {
			List<EhrCode> retval=new List<EhrCode>();
			for(int i=0;i<Listt.Count;i++) {
				if(ifExistsInDb && !Listt[i].ExistsInDbTable) {
					continue;
				}
				if(listValueSetOIDs.Contains(Listt[i].ValueSetOID)) {					
					retval.Add(Listt[i]);
				}
			}
			return retval;
		}

		///<summary></summary>
		public static List<string> GetValueSetFromCodeAndCategory(string codeValue,string codeSystem,string category) {
			List<string> retval=new List<string>();
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].CodeValue==codeValue && Listt[i].CodeSystem==codeSystem && Listt[i].QDMCategory==category) {
					retval.Add(Listt[i].ValueSetName);
				}
			}
			return retval;
		}

		///<summary></summary>
		public static long Insert(EhrCode ehrCode) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				ehrCode.EhrCodeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrCode);
				return ehrCode.EhrCodeNum;
			}
			return Crud.EhrCodeCrud.Insert(ehrCode);
		}

		///<summary>Used for adding codes, returns a hashset of codevalue+valuesetoid.</summary>
		public static HashSet<string> GetAllCodesHashSet() {
			HashSet<string> retVal=new HashSet<string>();
			for(int i=0;i<Listt.Count;i++) {
				retVal.Add(Listt[i].CodeValue+Listt[i].ValueSetOID);
			}
			return retVal;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrCode> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrCode>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrcode WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrCodeCrud.SelectMany(command);
		}

		///<summary>Gets one EhrCode from the db.</summary>
		public static EhrCode GetOne(long ehrCodeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrCode>(MethodBase.GetCurrentMethod(),ehrCodeNum);
			}
			return Crud.EhrCodeCrud.SelectOne(ehrCodeNum);
		}

		///<summary></summary>
		public static void Update(EhrCode ehrCode){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrCode);
				return;
			}
			Crud.EhrCodeCrud.Update(ehrCode);
		}

		///<summary></summary>
		public static void Delete(long ehrCodeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrCodeNum);
				return;
			}
			string command= "DELETE FROM ehrcode WHERE EhrCodeNum = "+POut.Long(ehrCodeNum);
			Db.NonQ(command);
		}
		*/





	}
}