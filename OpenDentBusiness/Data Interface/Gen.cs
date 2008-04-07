using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness{
	///<summary>A more secure version of "General", which passes specific method names to the server instead of just queries.  This also packages username and pass hash if remote.  User permissions will gradually be incorporated here.</summary>
	public class Gen {
		///<summary></summary>
		public static DataSet GetDS(MethodName methodName, params object[] parameters) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientTcp) {
				DtoGetDS dto=new DtoGetDS();
				dto.MethodName=methodName;
				dto.Parameters=parameters;
				return RemotingClient.ProcessGetDS(dto);
			}
			else {
				return DataCore.GetDsByMethod(methodName,parameters);
			}
		}

		///<summary></summary>
		public static DataTable GetTable(MethodName methodName, params object[] parameters) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientTcp) {
				DtoGetTable dto=new DtoGetTable();
				dto.MethodName=methodName;
				dto.Parameters=parameters;
				return RemotingClient.ProcessGetTable(dto);
			}
			else {
				return DataCore.GetTableByMethod(methodName,parameters);
			}
		}



	}
}
