using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Operatories {
		#region CachePattern
		///<summary>Refresh all operatories</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM operatory "
				+"ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Operatory";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			OperatoryC.Listt=Crud.OperatoryCrud.TableToList(table);
			OperatoryC.ListShort=new List<Operatory>();
			for(int i=0;i<OperatoryC.Listt.Count;i++) {
				if(!OperatoryC.Listt[i].IsHidden) {
					OperatoryC.ListShort.Add(OperatoryC.Listt[i]);
				}
			}
		}
		#endregion

		///<summary></summary>
		public static long Insert(Operatory operatory) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				operatory.OperatoryNum=Meth.GetLong(MethodBase.GetCurrentMethod(),operatory);
				return operatory.OperatoryNum;
			}
			return Crud.OperatoryCrud.Insert(operatory);
		}

		///<summary></summary>
		public static void Update(Operatory operatory) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),operatory);
				return;
			}
			Crud.OperatoryCrud.Update(operatory);
		}

		//<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		//public void Delete(){//no such thing as delete.  Hide instead
		//}

		public static List<Operatory> GetChangedSince(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Operatory>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM operatory WHERE DateTStamp > "+POut.DateT(changedSince);
			return Crud.OperatoryCrud.SelectMany(command);
		}

		public static string GetAbbrev(long operatoryNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<OperatoryC.Listt.Count;i++){
				if(OperatoryC.Listt[i].OperatoryNum==operatoryNum){
					return OperatoryC.Listt[i].Abbrev;
				}
			}
			return "";
		}

		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(long opNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<OperatoryC.ListShort.Count;i++) {
				if(OperatoryC.ListShort[i].OperatoryNum==opNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static Operatory GetOperatory(long operatoryNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<OperatoryC.Listt.Count;i++) {
				if(OperatoryC.Listt[i].OperatoryNum==operatoryNum) {
					return OperatoryC.Listt[i].Copy();
				}
			}
			return null;
		}
		
	
	}
	


}













