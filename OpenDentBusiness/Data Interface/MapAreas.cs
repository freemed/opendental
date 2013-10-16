using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class MapAreas{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all MapAreas.</summary>
		private static List<MapArea> listt;

		///<summary>A list of all MapAreas.</summary>
		public static List<MapArea> Listt{
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
			string command="SELECT * FROM maparea ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="MapArea";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.MapAreaCrud.TableToList(table);
		}
		#endregion
		*/
		
		///<summary></summary>
		public static List<MapArea> Refresh(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MapArea>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM maparea";
			return Crud.MapAreaCrud.SelectMany(command);
		}
		/*		
		///<summary>Gets one MapArea from the db.</summary>
		public static MapArea GetOne(long mapAreaNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<MapArea>(MethodBase.GetCurrentMethod(),mapAreaNum);
			}
			return Crud.MapAreaCrud.SelectOne(mapAreaNum);
		}
		*/
		///<summary></summary>
		public static long Insert(MapArea mapArea){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				mapArea.MapAreaNum=Meth.GetLong(MethodBase.GetCurrentMethod(),mapArea);
				return mapArea.MapAreaNum;
			}
			return Crud.MapAreaCrud.Insert(mapArea);
		}

		///<summary></summary>
		public static void Update(MapArea mapArea){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mapArea);
				return;
			}
			Crud.MapAreaCrud.Update(mapArea);
		}

		///<summary></summary>
		public static void Delete(long mapAreaNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),mapAreaNum);
				return;
			}
			string command= "DELETE FROM maparea WHERE MapAreaNum = "+POut.Long(mapAreaNum);
			Db.NonQ(command);
		}



	}
}