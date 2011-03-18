using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Vitalsigns{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Vitalsigns.</summary>
		private static List<Vitalsign> listt;

		///<summary>A list of all Vitalsigns.</summary>
		public static List<Vitalsign> Listt{
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
			string command="SELECT * FROM vitalsign ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Vitalsign";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.VitalsignCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Vitalsign> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Vitalsign>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM vitalsign WHERE PatNum = "+POut.Long(patNum);
			return Crud.VitalsignCrud.SelectMany(command);
		}

		///<summary>Gets one Vitalsign from the db.</summary>
		public static Vitalsign GetOne(long vitalSignNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Vitalsign>(MethodBase.GetCurrentMethod(),vitalSignNum);
			}
			return Crud.VitalsignCrud.SelectOne(vitalSignNum);
		}

		///<summary></summary>
		public static long Insert(Vitalsign vitalsign){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				vitalsign.VitalSignNum=Meth.GetLong(MethodBase.GetCurrentMethod(),vitalsign);
				return vitalsign.VitalSignNum;
			}
			return Crud.VitalsignCrud.Insert(vitalsign);
		}

		///<summary></summary>
		public static void Update(Vitalsign vitalsign){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),vitalsign);
				return;
			}
			Crud.VitalsignCrud.Update(vitalsign);
		}

		///<summary></summary>
		public static void Delete(long vitalSignNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),vitalSignNum);
				return;
			}
			string command= "DELETE FROM vitalsign WHERE VitalSignNum = "+POut.Long(vitalSignNum);
			Db.NonQ(command);
		}
		*/



	}
}