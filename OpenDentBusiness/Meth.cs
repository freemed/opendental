using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Short for Method.  Will probably be used extensively since every data interface class will need this.  An assortment of methods to allow easy retrieval of info from database without risking sending queries directly.</summary>
	public class Meth {
		///<summary>The query will NOT be used if ClientWeb.  The calling class MUST return a DataTable and must not take any arguments.</summary>
		public static DataTable GetTable(MethodBase methodBase,string command) {
			#if DEBUG
				//We will take the time to verify that it returns a DataTable
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(DataTable)) {
					throw new ApplicationException("Gen.GetTable calling class must return DataTable.");
				}
				if(methodInfo.GetParameters().Length>0) {
					throw new ApplicationException("Gen.GetTable calling class cannot require any parameters.");
				}
			#endif
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DtoGetTable dto=new DtoGetTable();
				dto.MethodNameTable=methodBase.DeclaringType.Name+"."+methodBase.Name;
				//dto.Parameters=;
				dto.Credentials=new Credentials();
				dto.Credentials.Username=Security.CurUser.UserName;
				dto.Credentials.PassHash=Security.CurUser.Password;
				return RemotingClient.ProcessGetTable(dto);
			}
			else {
				DataConnection dcon=new DataConnection();
				return dcon.GetTable(command);
			}
		}


	}
}
