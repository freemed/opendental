using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CentralConnections{
		///<summary></summary>
		public static List<CentralConnection> Refresh(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CentralConnection>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM centralconnection ORDER BY ItemOrder";
			return Crud.CentralConnectionCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(CentralConnection centralConnection){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				centralConnection.CentralConnectionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),centralConnection);
				return centralConnection.CentralConnectionNum;
			}
			return Crud.CentralConnectionCrud.Insert(centralConnection);
		}

		///<summary></summary>
		public static void Update(CentralConnection centralConnection){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),centralConnection);
				return;
			}
			Crud.CentralConnectionCrud.Update(centralConnection);
		}

		///<summary></summary>
		public static void Delete(long centralConnectionNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),centralConnectionNum);
				return;
			}
			string command= "DELETE FROM centralconnection WHERE CentralConnectionNum = "+POut.Long(centralConnectionNum);
			Db.NonQ(command);
		}
		/*
		
		///<summary>Gets one CentralConnection from the db.</summary>
		public static CentralConnection GetOne(long centralConnectionNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CentralConnection>(MethodBase.GetCurrentMethod(),centralConnectionNum);
			}
			return Crud.CentralConnectionCrud.SelectOne(centralConnectionNum);
		}

		
		*/



	}
}