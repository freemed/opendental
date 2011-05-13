using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CanadianNetworks{
		private static List<CanadianNetwork> listt;

		public static List<CanadianNetwork> Listt{
			//No need to check RemotingRole; no call to db.
			get{
				if(listt==null){
					Refresh();
				}
				return listt;
			}
		}
	
		///<summary></summary>
		public static void Refresh(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="SELECT * FROM canadiannetwork ORDER BY Descript";
			DataTable table=Db.GetTable(command);
			listt=Crud.CanadianNetworkCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(CanadianNetwork canadianNetwork) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				canadianNetwork.CanadianNetworkNum=Meth.GetLong(MethodBase.GetCurrentMethod(),canadianNetwork);
				return canadianNetwork.CanadianNetworkNum;
			}
			return Crud.CanadianNetworkCrud.Insert(canadianNetwork);
		}

		///<summary></summary>
		public static void Update(CanadianNetwork canadianNetwork){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),canadianNetwork);
				return;
			}
			Crud.CanadianNetworkCrud.Update(canadianNetwork);
		}

		///<summary></summary>
		public static void Delete(int networkNum){

		}

		///<summary></summary>
		public static CanadianNetwork GetNetwork(long networkNum) {
			//No need to check RemotingRole; no call to db.
			if(listt==null) {
				Refresh();
			}
			for(int i=0;i<listt.Count;i++){
				if(listt[i].CanadianNetworkNum==networkNum){
					return listt[i];
				}
			}
			return null;
		}


	


		 
		 
	}
}













