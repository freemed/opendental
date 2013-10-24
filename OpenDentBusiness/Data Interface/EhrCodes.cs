using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrCodes{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrCodes.</summary>
		private static List<EhrCode> listt;

		///<summary>A list of all EhrCodes.</summary>
		public static List<EhrCode> Listt{
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