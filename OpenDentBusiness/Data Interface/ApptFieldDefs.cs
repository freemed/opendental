using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ApptFieldDefs{
		#region CachePattern
		///<summary>A list of all ApptFieldDefs.</summary>
		private static List<ApptFieldDef> listt;

		///<summary>A list of all ApptFieldDefs.</summary>
		public static List<ApptFieldDef> Listt{
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
			string command="SELECT * FROM apptfielddef ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ApptFieldDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ApptFieldDefCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ApptFieldDef> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ApptFieldDef>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM apptfielddef WHERE PatNum = "+POut.Long(patNum);
			return Crud.ApptFieldDefCrud.SelectMany(command);
		}

		///<summary>Gets one ApptFieldDef from the db.</summary>
		public static ApptFieldDef GetOne(long apptFieldDefNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ApptFieldDef>(MethodBase.GetCurrentMethod(),apptFieldDefNum);
			}
			return Crud.ApptFieldDefCrud.SelectOne(apptFieldDefNum);
		}

		///<summary></summary>
		public static long Insert(ApptFieldDef apptFieldDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				apptFieldDef.ApptFieldDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),apptFieldDef);
				return apptFieldDef.ApptFieldDefNum;
			}
			return Crud.ApptFieldDefCrud.Insert(apptFieldDef);
		}

		///<summary></summary>
		public static void Update(ApptFieldDef apptFieldDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptFieldDef);
				return;
			}
			Crud.ApptFieldDefCrud.Update(apptFieldDef);
		}

		///<summary></summary>
		public static void Delete(long apptFieldDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptFieldDefNum);
				return;
			}
			string command= "DELETE FROM apptfielddef WHERE ApptFieldDefNum = "+POut.Long(apptFieldDefNum);
			Db.NonQ(command);
		}
		*/



	}
}