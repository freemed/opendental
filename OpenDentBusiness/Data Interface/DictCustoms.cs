using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class DictCustoms{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all DictCustoms.</summary>
		private static List<DictCustom> listt;

		///<summary>A list of all DictCustoms.</summary>
		public static List<DictCustom> Listt{
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
			string command="SELECT * FROM dictcustom ORDER BY WordText";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="DictCustom";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.DictCustomCrud.TableToList(table);
		}

		///<summary></summary>
		public static long Insert(DictCustom dictCustom){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				dictCustom.DictCustomNum=Meth.GetLong(MethodBase.GetCurrentMethod(),dictCustom);
				return dictCustom.DictCustomNum;
			}
			return Crud.DictCustomCrud.Insert(dictCustom);
		}

		///<summary></summary>
		public static void Update(DictCustom dictCustom){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dictCustom);
				return;
			}
			Crud.DictCustomCrud.Update(dictCustom);
		}

		///<summary></summary>
		public static void Delete(long dictCustomNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dictCustomNum);
				return;
			}
			Crud.DictCustomCrud.Delete(dictCustomNum);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<DictCustom> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DictCustom>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM dictcustom WHERE PatNum = "+POut.Long(patNum);
			return Crud.DictCustomCrud.SelectMany(command);
		}
		
		///<summary>Gets one DictCustom from the db.</summary>
		public static DictCustom GetOne(long dictCustomNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<DictCustom>(MethodBase.GetCurrentMethod(),dictCustomNum);
			}
			return Crud.DictCustomCrud.SelectOne(dictCustomNum);
		}
		 

		
		*/



	}
}