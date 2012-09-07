using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ToothGridDefs{
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ToothGridDefs.</summary>
		private static List<ToothGridDef> listt;

		///<summary>A list of all ToothGridDefs.</summary>
		public static List<ToothGridDef> Listt{
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
			string command="SELECT * FROM toothgriddef ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ToothGridDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ToothGridDefCrud.TableToList(table);
		}
		#endregion*/

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ToothGridDef> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ToothGridDef>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM toothgriddef WHERE PatNum = "+POut.Long(patNum);
			return Crud.ToothGridDefCrud.SelectMany(command);
		}

		///<summary>Gets one ToothGridDef from the db.</summary>
		public static ToothGridDef GetOne(long toothGridDefNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ToothGridDef>(MethodBase.GetCurrentMethod(),toothGridDefNum);
			}
			return Crud.ToothGridDefCrud.SelectOne(toothGridDefNum);
		}

		///<summary></summary>
		public static long Insert(ToothGridDef toothGridDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				toothGridDef.ToothGridDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),toothGridDef);
				return toothGridDef.ToothGridDefNum;
			}
			return Crud.ToothGridDefCrud.Insert(toothGridDef);
		}

		///<summary></summary>
		public static void Update(ToothGridDef toothGridDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),toothGridDef);
				return;
			}
			Crud.ToothGridDefCrud.Update(toothGridDef);
		}

		///<summary></summary>
		public static void Delete(long toothGridDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),toothGridDefNum);
				return;
			}
			string command= "DELETE FROM toothgriddef WHERE ToothGridDefNum = "+POut.Long(toothGridDefNum);
			Db.NonQ(command);
		}
		*/



	}
}