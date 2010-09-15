using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ChartViews{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ChartViews.</summary>
		private static List<ChartView> listt;

		///<summary>A list of all ChartViews.</summary>
		public static List<ChartView> Listt{
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
			string command="SELECT * FROM chartview ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ChartView";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ChartViewCrud.TableToList(table);
		}
		#endregion

		///<summary></summary>
		public static long Insert(ChartView chartView) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				chartView.ChartViewNum=Meth.GetLong(MethodBase.GetCurrentMethod(),chartView);
				return chartView.ChartViewNum;
			}
			return Crud.ChartViewCrud.Insert(chartView);
		}

		///<summary></summary>
		public static void Update(ChartView chartView) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),chartView);
				return;
			}
			Crud.ChartViewCrud.Update(chartView);
		}

		///<summary></summary>
		public static void Delete(long chartViewNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),chartViewNum);
				return;
			}
			string command= "DELETE FROM chartview WHERE ChartViewNum = "+POut.Long(chartViewNum);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ChartView> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ChartView>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM chartview WHERE PatNum = "+POut.Long(patNum);
			return Crud.ChartViewCrud.SelectMany(command);
		}

		///<summary>Gets one ChartView from the db.</summary>
		public static ChartView GetOne(long chartViewNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ChartView>(MethodBase.GetCurrentMethod(),chartViewNum);
			}
			return Crud.ChartViewCrud.SelectOne(chartViewNum);
		}

		*/



	}
}