using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness {
	public class ClaimCondCodeLogs {

		public static ClaimCondCodeLog GetOne(long ClaimNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ClaimCondCodeLog>(MethodBase.GetCurrentMethod(),ClaimNum);
			}
			string command="SELECT * FROM claimcondcodelog WHERE ClaimNum='" + ClaimNum + "'";
			ClaimCondCodeLog claimCondCodeLog = Crud.ClaimCondCodeLogCrud.SelectOne(command);
			if(claimCondCodeLog==null){
				return new ClaimCondCodeLog();
			}
			return claimCondCodeLog;
		}
	}
}
