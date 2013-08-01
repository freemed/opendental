using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Snomeds{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ICD9s.</summary>
		private static List<Snomed> listt;

		///<summary>A list of all ICD9s.</summary>
		public static List<Snomed> Listt{
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
			string command="SELECT * FROM snomed ORDER BY SnomedCode";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Snomed";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.SnomedCrud.TableToList(table);
		}
		#endregion

		///<summary></summary>
		public static List<Snomed> GetByCodeOrDescription(string searchTxt){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Snomed>>(MethodBase.GetCurrentMethod(),searchTxt);
			}
			string command="SELECT * FROM snomed WHERE SnomedCode LIKE '%"+POut.String(searchTxt)+"%' "
				+"OR Description LIKE '%"+POut.String(searchTxt)+"%' ORDER BY SnomedCode";
			return Crud.SnomedCrud.SelectMany(command);
		}
		
		///<summary>Gets one Snomed from the db.</summary>
		public static Snomed GetOne(long snomedNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Snomed>(MethodBase.GetCurrentMethod(),snomedNum);
			}
			return Crud.SnomedCrud.SelectOne(snomedNum);
		}

		///<summary>Directly from db.</summary>
		public static bool CodeExists(string snomedCode) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),snomedCode);
			}
			string command="SELECT COUNT(*) FROM snomed WHERE SnomedCode = '"+POut.String(snomedCode)+"'";
			string count=Db.GetCount(command);
			if(count=="0") {
				return false;
			}
			return true;
		}

		///<summary></summary>
		public static long Insert(Snomed snomed){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				snomed.SnomedNum=Meth.GetLong(MethodBase.GetCurrentMethod(),snomed);
				return snomed.SnomedNum;
			}
			return Crud.SnomedCrud.Insert(snomed);
		}

		///<summary></summary>
		public static void Update(Snomed snomed) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),snomed);
				return;
			}
			Crud.SnomedCrud.Update(snomed);
		}

		///<summary></summary>
		public static void Delete(long snomedNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),snomedNum);
				return;
			}
			//No need to check FKs from other tables since there are none. Snomeds are copied into the DiseaseDef table when in use.
			string command= "DELETE FROM snomed WHERE SnomedNum = "+POut.Long(snomedNum);
			Db.NonQ(command);
		}

		///<summary>Delete all for import. Before importing Snomed Codes, delete the existing list.</summary>
		public static void DeleteAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command= "DELETE FROM snomed";
			Db.NonQ(command);
		}

		///<summary>Returns the code and description of the snomed.</summary>
		public static string GetDescription(long snomedNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].SnomedNum==snomedNum) {
					return Listt[i].SnomedCode+"-"+Listt[i].Description;
				}
			}
			return "";
		}

		///<summary>Returns the Snomed of the code passed in by looking in cache.  If code does not exist, returns null.</summary>
		public static Snomed GetByCode(string snomedCode) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].SnomedCode==snomedCode) {
					return Listt[i];
				}
			}
			return null;
		}

	}
}