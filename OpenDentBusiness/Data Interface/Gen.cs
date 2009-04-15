using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary>Gen is short for General.  Not sure how it will be used yet.  It will probably just replace General.  </summary>
	public class Gen {
		///<summary>stub</summary>
		public static DataSet GetDS(MethodNameDS methodName,params object[] parameters) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return null;
			}
			else {
				return DataCore.GetDsByMethod(methodName,parameters);
			}
		}

		///<summary></summary>
		public static DataSet GetDS(MethodBase methodInfo, params object[] parameters) {
			string classMethod=methodInfo.DeclaringType.Name+"."+methodInfo.Name;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DtoGetDS dto=new DtoGetDS();
				dto.MethodNameDS=classMethod;
				dto.Parameters=parameters;
				dto.Credentials=new Credentials();
				dto.Credentials.Username=Security.CurUser.UserName;
				dto.Credentials.PassHash=Security.CurUser.Password;
				return RemotingClient.ProcessGetDS(dto);
			}
			else {
				return null;
				//return DataCore.GetDsByMethod(classMethod,parameters);
			}
		}

		

		/*
		///<summary>Usually, there is a just a single parameter for the query.</summary>
		public static DataTable GetTable(MethodBase methodInfo,params object[] parameters) {
			string classMethod=methodInfo.DeclaringType.Name+"."+methodInfo.Name;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DtoGetTable dto=new DtoGetTable();
				dto.MethodNameTable=classMethod;
				dto.Parameters=parameters;
				dto.Credentials=new Credentials();
				dto.Credentials.Username=Security.CurUser.UserName;
				dto.Credentials.PassHash=Security.CurUser.Password;
				return RemotingClient.ProcessGetTable(dto);
			}
			else {

				string className=classMethod.Split('.')[0];
				string methodName=classMethod.Split('.')[1];
				Type classType=Type.GetType("OpenDentBusiness."+className);
				MethodInfo methodInfo=classType.GetMethod(methodName);
				DataTable result=(DataTable)methodInfo.Invoke(null,parameters);
				return result;
				//return DataCore.GetTableByMethod(classMethod,parameters);
			}
		}*/



	}
}
