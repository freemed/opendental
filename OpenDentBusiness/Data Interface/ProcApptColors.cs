using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ProcApptColors{
		#region CachePattern

		///<summary>A list of all ProcApptColors.</summary>
		private static List<ProcApptColor> listt;

		///<summary>A list of all ProcApptColors.</summary>
		public static List<ProcApptColor> Listt{
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
			string command="SELECT * FROM procapptcolor ORDER BY CodeRange";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ProcApptColor";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ProcApptColorCrud.TableToList(table);
		}
		#endregion


		///<summary></summary>
		public static List<ProcApptColor> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ProcApptColor>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM procapptcolor WHERE PatNum = "+POut.Long(patNum);
			return Crud.ProcApptColorCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(ProcApptColor procApptColor){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				procApptColor.ProcApptColorNum=Meth.GetLong(MethodBase.GetCurrentMethod(),procApptColor);
				return procApptColor.ProcApptColorNum;
			}
			return Crud.ProcApptColorCrud.Insert(procApptColor);
		}

		///<summary></summary>
		public static void Update(ProcApptColor procApptColor){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procApptColor);
				return;
			}
			Crud.ProcApptColorCrud.Update(procApptColor);
		}

		///<summary></summary>
		public static void Delete(long procApptColorNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procApptColorNum);
				return;
			}
			string command= "DELETE FROM procapptcolor WHERE ProcApptColorNum = "+POut.Long(procApptColorNum);
			Db.NonQ(command);
		}

		/*
		///<summary>Gets one ProcApptColor from the db.</summary>
		public static ProcApptColor GetOne(long procApptColorNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ProcApptColor>(MethodBase.GetCurrentMethod(),procApptColorNum);
			}
			return Crud.ProcApptColorCrud.SelectOne(procApptColorNum);
		}*/



	}
}