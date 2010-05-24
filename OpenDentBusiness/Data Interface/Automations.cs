using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Automations {
		private static List<Automation> listt;

		public static List<Automation> Listt {
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
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM automation";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Automation";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AutomationCrud.TableToList(table);
		}

		///<summary></summary>
		public static long Insert(Automation auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				auto.AutomationNum=Meth.GetLong(MethodBase.GetCurrentMethod(),auto);
				return auto.AutomationNum;
			}
			return Crud.AutomationCrud.Insert(auto);
		}

		///<summary></summary>
		public static void Update(Automation auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),auto);
				return;
			}
			Crud.AutomationCrud.Update(auto);
		}

		///<summary></summary>
		public static void Delete(Automation auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),auto);
				return;
			}
			string command="DELETE FROM automation" 
				+" WHERE AutomationNum = "+POut.Long(auto.AutomationNum);
 			Db.NonQ(command);
		}


		
		

	}
	


}













