using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary>Refreshed with local data.</summary>
	public class ProviderIdents {
		///<summary>This is the list of all id's for all providers. They are extracted as needed.</summary>
		private static ProviderIdent[] list;

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM providerident";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ProviderIdent";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=Crud.ProviderIdentCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static void Update(ProviderIdent pi){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pi);
				return;
			}
			Crud.ProviderIdentCrud.Update(pi);
		}

		///<summary></summary>
		public static long Insert(ProviderIdent pi){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pi.ProviderIdentNum=Meth.GetLong(MethodBase.GetCurrentMethod(),pi);
				return pi.ProviderIdentNum;
			}
			return Crud.ProviderIdentCrud.Insert(pi);
		}

		///<summary></summary>
		public static void Delete(ProviderIdent pi){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pi);
				return;
			}
			string command= "DELETE FROM providerident "
				+"WHERE ProviderIdentNum = "+POut.Long(pi.ProviderIdentNum);
 			Db.NonQ(command);
		}

		///<summary>Gets all supplemental identifiers that have been attached to this provider. Used in the provider edit window.</summary>
		public static ProviderIdent[] GetForProv(long provNum) {
			//No need to check RemotingRole; no call to db.
			if(list==null) {
				RefreshCache();
			}
			ArrayList arrayL=new ArrayList();
			for(int i=0;i<list.Length;i++) {
				if(list[i].ProvNum==provNum) {
					arrayL.Add(list[i]);
				}
			}
			ProviderIdent[] ForProv=new ProviderIdent[arrayL.Count];
			for(int i=0;i<arrayL.Count;i++){
				ForProv[i]=(ProviderIdent)arrayL[i];
			}
			return ForProv;
		}

		///<summary>Gets all supplemental identifiers that have been attached to this provider and for this particular payorID.  Called from X12 when creating a claim file.  Also used now on printed claims.</summary>
		public static ProviderIdent[] GetForPayor(long provNum,string payorID) {
			//No need to check RemotingRole; no call to db.
			if(list==null) {
				RefreshCache();
			}
			ArrayList arrayL=new ArrayList();
			for(int i=0;i<list.Length;i++) {
				if(list[i].ProvNum==provNum
					&& list[i].PayorID==payorID)
				{
					arrayL.Add(list[i]);
				}
			}
			ProviderIdent[] ForPayor=new ProviderIdent[arrayL.Count];
			for(int i=0;i<arrayL.Count;i++){
				ForPayor[i]=(ProviderIdent)arrayL[i];
			}
			return ForPayor;
		}

		///<summary>Called from FormProvEdit if cancel on a new provider.</summary>
		public static void DeleteAllForProv(long provNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),provNum);
				return;
			}
			string command= "DELETE from providerident WHERE provnum = '"+POut.Long(provNum)+"'";
 			Db.NonQ(command);
		}

		/// <summary></summary>
		public static bool IdentExists(ProviderSupplementalID type,long provNum,string payorID) {
			//No need to check RemotingRole; no call to db.
			if(list==null) {
				RefreshCache();
			}
			for(int i=0;i<list.Length;i++) {
				if(list[i].ProvNum==provNum
					&& list[i].SuppIDType==type
					&& list[i].PayorID==payorID)
				{
					return true;
				}
			}
			return false;
		}

	
	}
	
	

}










