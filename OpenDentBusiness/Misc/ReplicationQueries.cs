using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class ReplicationQueries {

		public static string GetCurrentDatabaseName(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string command="SELECT DATABASE()";
			return PIn.PString(Db.GetTable(command).Rows[0][0].ToString());
		}

	}
}
