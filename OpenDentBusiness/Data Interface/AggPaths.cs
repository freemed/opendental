using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AggPaths {

		///<summary>Selects all aggPaths</summary>
		public static List<AggPath> Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<AggPath>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM aggpath";
			return Crud.AggPathCrud.SelectMany(command);
		}

		///<summary>Gets one AggPath from the db.</summary>
		public static AggPath GetOne(long aggPathNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<AggPath>(MethodBase.GetCurrentMethod(),aggPathNum);
			}
			return Crud.AggPathCrud.SelectOne(aggPathNum);
		}

		///<summary></summary>
		public static long Insert(AggPath aggPath) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				aggPath.AggPathNum=Meth.GetLong(MethodBase.GetCurrentMethod(),aggPath);
				return aggPath.AggPathNum;
			}
			return Crud.AggPathCrud.Insert(aggPath);
		}

		///<summary></summary>
		public static void Update(AggPath aggPath) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aggPath);
				return;
			}
			Crud.AggPathCrud.Update(aggPath);
		}

		///<summary></summary>
		public static void Delete(long aggPathNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),aggPathNum);
				return;
			}
			string command= "DELETE FROM aggpath WHERE AggPathNum = "+POut.Long(aggPathNum);
			Db.NonQ(command);
		}


	}
}