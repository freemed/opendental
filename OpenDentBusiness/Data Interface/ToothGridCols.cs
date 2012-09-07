using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ToothGridCols{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ToothGridCols.</summary>
		private static List<ToothGridCol> listt;

		///<summary>A list of all ToothGridCols.</summary>
		public static List<ToothGridCol> Listt{
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
			string command="SELECT * FROM toothgridcol ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ToothGridCol";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ToothGridColCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ToothGridCol> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ToothGridCol>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM toothgridcol WHERE PatNum = "+POut.Long(patNum);
			return Crud.ToothGridColCrud.SelectMany(command);
		}

		///<summary>Gets one ToothGridCol from the db.</summary>
		public static ToothGridCol GetOne(long toothGridColNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ToothGridCol>(MethodBase.GetCurrentMethod(),toothGridColNum);
			}
			return Crud.ToothGridColCrud.SelectOne(toothGridColNum);
		}

		///<summary></summary>
		public static long Insert(ToothGridCol toothGridCol){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				toothGridCol.ToothGridColNum=Meth.GetLong(MethodBase.GetCurrentMethod(),toothGridCol);
				return toothGridCol.ToothGridColNum;
			}
			return Crud.ToothGridColCrud.Insert(toothGridCol);
		}

		///<summary></summary>
		public static void Update(ToothGridCol toothGridCol){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),toothGridCol);
				return;
			}
			Crud.ToothGridColCrud.Update(toothGridCol);
		}

		///<summary></summary>
		public static void Delete(long toothGridColNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),toothGridColNum);
				return;
			}
			string command= "DELETE FROM toothgridcol WHERE ToothGridColNum = "+POut.Long(toothGridColNum);
			Db.NonQ(command);
		}
		*/



	}
}