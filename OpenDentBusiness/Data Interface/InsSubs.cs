using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class InsSubs{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all InsSubs.</summary>
		private static List<InsSub> listt;

		///<summary>A list of all InsSubs.</summary>
		public static List<InsSub> Listt{
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
			string command="SELECT * FROM inssub ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="InsSub";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.InsSubCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<InsSub> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InsSub>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM inssub WHERE PatNum = "+POut.Long(patNum);
			return Crud.InsSubCrud.SelectMany(command);
		}

		///<summary>Gets one InsSub from the db.</summary>
		public static InsSub GetOne(long insSubNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<InsSub>(MethodBase.GetCurrentMethod(),insSubNum);
			}
			return Crud.InsSubCrud.SelectOne(insSubNum);
		}

		///<summary></summary>
		public static long Insert(InsSub insSub){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				insSub.InsSubNum=Meth.GetLong(MethodBase.GetCurrentMethod(),insSub);
				return insSub.InsSubNum;
			}
			return Crud.InsSubCrud.Insert(insSub);
		}

		///<summary></summary>
		public static void Update(InsSub insSub){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insSub);
				return;
			}
			Crud.InsSubCrud.Update(insSub);
		}

		///<summary></summary>
		public static void Delete(long insSubNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insSubNum);
				return;
			}
			string command= "DELETE FROM inssub WHERE InsSubNum = "+POut.Long(insSubNum);
			Db.NonQ(command);
		}
		*/



	}
}