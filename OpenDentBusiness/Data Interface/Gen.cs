using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary>A more secure version of "General", which passes specific method names to the server instead of just queries.  This also packages username and pass hash if remote.  User permissions will gradually be incorporated here.</summary>
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

		///<summary>stub</summary>
		public static DataTable GetTable(MethodNameTable methodName,params object[] parameters) {
			return null;
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

		///<summary>The query will NOT be used if ClientWeb.  The calling class MUST return a DataTable.</summary>
		public static DataTable GetTable(MethodBase methodBase,string command) {
			#if DEBUG
				//We will take the time to verify that it returns a DataTable
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(DataTable)) {
					throw new ApplicationException("Gen.GetTable calling class must return DataTable.");
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
