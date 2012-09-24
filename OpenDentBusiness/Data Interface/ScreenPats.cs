using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ScreenPats{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ScreenPats.</summary>
		private static List<ScreenPat> listt;

		///<summary>A list of all ScreenPats.</summary>
		public static List<ScreenPat> Listt{
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
			string command="SELECT * FROM screenpat ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ScreenPat";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ScreenPatCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ScreenPat> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ScreenPat>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM screenpat WHERE PatNum = "+POut.Long(patNum);
			return Crud.ScreenPatCrud.SelectMany(command);
		}

		///<summary>Gets one ScreenPat from the db.</summary>
		public static ScreenPat GetOne(long screenPatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ScreenPat>(MethodBase.GetCurrentMethod(),screenPatNum);
			}
			return Crud.ScreenPatCrud.SelectOne(screenPatNum);
		}

		///<summary></summary>
		public static long Insert(ScreenPat screenPat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				screenPat.ScreenPatNum=Meth.GetLong(MethodBase.GetCurrentMethod(),screenPat);
				return screenPat.ScreenPatNum;
			}
			return Crud.ScreenPatCrud.Insert(screenPat);
		}

		///<summary></summary>
		public static void Update(ScreenPat screenPat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),screenPat);
				return;
			}
			Crud.ScreenPatCrud.Update(screenPat);
		}

		///<summary></summary>
		public static void Delete(long screenPatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),screenPatNum);
				return;
			}
			string command= "DELETE FROM screenpat WHERE ScreenPatNum = "+POut.Long(screenPatNum);
			Db.NonQ(command);
		}
		*/



	}
}