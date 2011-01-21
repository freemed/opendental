using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Short for Method.  Calls a method remotely.  ONLY used if ClientWeb.  This must be tested at top of the method in question.  These will be used extensively since ALL data interface classes will need this.  This completely avoids sending queries directly to the server.  Must pass all the parameters from the original method.</summary>
	public class Meth {
		///<summary></summary>
		public static DataTable GetTable(MethodBase methodBase,params object[] parameters) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetTable may only be used when RemotingRole is ClientWeb.");
			}
			#if DEBUG
				//Verify that it returns a DataTable
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(DataTable)) {
					throw new ApplicationException("Meth.GetTable calling class must return DataTable.");
				}
			#endif
			DtoGetTable dto=new DtoGetTable();
			dto.MethodName=methodBase.DeclaringType.Name+"."+methodBase.Name;
			dto.Params=DtoObject.ConstructArray(parameters,GetParamTypes(methodBase));
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			return RemotingClient.ProcessGetTable(dto);
		}

		///<summary>Uses lower sql permissions, making it safe to pass a query.</summary>
		public static DataTable GetTableLow(string command) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetTableLow may only be used when RemotingRole is ClientWeb.");
			}
			DtoGetTableLow dto=new DtoGetTableLow();
			dto.MethodName="";
			DtoObject dtoObj=new DtoObject(command,typeof(string));
			dto.Params=new DtoObject[] { dtoObj };
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			return RemotingClient.ProcessGetTableLow(dto);
		}

		///<summary></summary>
		public static DataSet GetDS(MethodBase methodBase,params object[] parameters) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetDS may only be used when RemotingRole is ClientWeb.");
			}
			#if DEBUG
				//Verify that it returns a DataSet
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(DataSet)) {
					throw new ApplicationException("Meth.GetDS calling class must return DataSet.");
				}
			#endif
			DtoGetDS dto=new DtoGetDS();
			dto.MethodName=methodBase.DeclaringType.Name+"."+methodBase.Name;
			dto.Params=DtoObject.ConstructArray(parameters,GetParamTypes(methodBase));
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			return RemotingClient.ProcessGetDS(dto);
		}

		///<summary></summary>
		public static long GetLong(MethodBase methodBase,params object[] parameters) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetLong may only be used when RemotingRole is ClientWeb.");
			}
			#if DEBUG
				//Verify that it returns an int
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(long)) {
					throw new ApplicationException("Meth.GetLong calling class must return long.");
				}
			#endif
			DtoGetLong dto=new DtoGetLong();
			dto.MethodName=methodBase.DeclaringType.Name+"."+methodBase.Name;
			dto.Params=DtoObject.ConstructArray(parameters,GetParamTypes(methodBase));
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			return RemotingClient.ProcessGetLong(dto);
		}

		///<summary></summary>
		public static int GetInt(MethodBase methodBase,params object[] parameters) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetInt may only be used when RemotingRole is ClientWeb.");
			}
			#if DEBUG
			//Verify that it returns an int
			MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
			if(methodInfo.ReturnType != typeof(int)) {
				throw new ApplicationException("Meth.GetInt calling class must return int.");
			}
			#endif
			DtoGetInt dto=new DtoGetInt();
			dto.MethodName=methodBase.DeclaringType.Name+"."+methodBase.Name;
			dto.Params=DtoObject.ConstructArray(parameters,GetParamTypes(methodBase));
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			return RemotingClient.ProcessGetInt(dto);
		}

		///<summary></summary>
		public static void GetVoid(MethodBase methodBase,params object[] parameters) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetVoid may only be used when RemotingRole is ClientWeb.");
			}
			#if DEBUG
				//Verify that it returns void
				MethodInfo methodInfo=null;
				try {
					methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				}
				catch {
					//this is just a debugging tool for obvious mismatches.  Sometimes it will fail if there are two methods with the same name.
				}
				if(methodInfo!=null && methodInfo.ReturnType != typeof(void)) {
					throw new ApplicationException("Meth.GetVoid calling class must return void.");
				}
			#endif
			DtoGetVoid dto=new DtoGetVoid();
			dto.MethodName=methodBase.DeclaringType.Name+"."+methodBase.Name;
			dto.Params=DtoObject.ConstructArray(parameters,GetParamTypes(methodBase));
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			RemotingClient.ProcessGetVoid(dto);
		}

		///<summary></summary>
		public static T GetObject<T>(MethodBase methodBase,params object[] parameters) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetObject may only be used when RemotingRole is ClientWeb.");
			}
			//can't verify return type
			DtoGetObject dto=new DtoGetObject();
			if(typeof(T).IsGenericType) {
				Type listType=typeof(T).GetGenericArguments()[0];
				dto.ObjectType="List<"+listType.FullName+">";
			}
			else {
				dto.ObjectType=typeof(T).FullName;
			}
			dto.MethodName=methodBase.DeclaringType.Name+"."+methodBase.Name;
			dto.Params=DtoObject.ConstructArray(parameters,GetParamTypes(methodBase));
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			return RemotingClient.ProcessGetObject<T>(dto);
		}

		///<summary></summary>
		public static string GetString(MethodBase methodBase,params object[] parameters) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetString may only be used when RemotingRole is ClientWeb.");
			}
			#if DEBUG
				//Verify that it returns string
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(string)) {
					throw new ApplicationException("Meth.GetString calling class must return string.");
				}
			#endif
			DtoGetString dto=new DtoGetString();
			dto.MethodName=methodBase.DeclaringType.Name+"."+methodBase.Name;
			dto.Params=DtoObject.ConstructArray(parameters,GetParamTypes(methodBase));
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			return RemotingClient.ProcessGetString(dto);
		}

		///<summary></summary>
		public static bool GetBool(MethodBase methodBase,params object[] parameters) {
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb) {
				throw new ApplicationException("Meth.GetBool may only be used when RemotingRole is ClientWeb.");
			}
			#if DEBUG
			/*
				//Verify that it returns string
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(bool)) {
					throw new ApplicationException("Meth.GetBool calling class must return bool.");
				}*/
			#endif
			DtoGetBool dto=new DtoGetBool();
			dto.MethodName=methodBase.DeclaringType.Name+"."+methodBase.Name;
			dto.Params=DtoObject.ConstructArray(parameters,GetParamTypes(methodBase));
			dto.Credentials=new Credentials();
			dto.Credentials.Username=Security.CurUser.UserName;
			dto.Credentials.Password=Security.PasswordTyped;//.CurUser.Password;
			return RemotingClient.ProcessGetBool(dto);
		}

		private static Type[] GetParamTypes(MethodBase methodBase) {
			ParameterInfo[] paramInfo=methodBase.GetParameters();
			Type[] retVal=new Type[paramInfo.Length];
			for(int i=0;i<paramInfo.Length;i++) {
				retVal[i]=paramInfo[i].ParameterType;
			}
			return retVal;
		}


	}
}
