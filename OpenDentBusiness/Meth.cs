using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Short for Method.  Will probably be used extensively since many data interface classes will need this.  An assortment of methods to allow easy retrieval of info from database without risking sending queries directly.  Only useful where one query is used in a method.  If multiple queries are to be used, then manually test RemotingRole and then use Gen.</summary>
	public class Meth {
		///<summary>The query will NOT be used if ClientWeb.  The calling class MUST return a DataTable and must not take any arguments.</summary>
		public static DataTable GetTable(MethodBase methodBase,string command) {
			#if DEBUG
				//Verify that it returns a DataTable
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(DataTable)) {
					throw new ApplicationException("Meth.GetTable calling class must return DataTable.");
				}
				if(methodInfo.GetParameters().Length>0) {
					throw new ApplicationException("Meth.GetTable calling class cannot require any parameters.");
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

		///<summary>The calling class MUST return a DataSet and must take the same parameters as passed in here.  Only used if RemotingRole=ClientWeb.  Only used in one class so far: Cache.  Need to reevaluate after using in a few more classes.</summary>
		public static DataSet GetDS(MethodBase methodBase,params object[] parameters) {
			#if DEBUG
				//Verify that it returns a DataSet
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(DataSet)) {
					throw new ApplicationException("Meth.GetDS calling class must return DataSet.");
				}
			#endif
			//string className=methodBase.DeclaringType.Name;
			//string methodName=methodBase.Name;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DtoGetDS dto=new DtoGetDS();
				dto.MethodNameDS=methodBase.DeclaringType.Name+"."+methodBase.Name;
				dto.Parameters=parameters;
				dto.Credentials=new Credentials();
				dto.Credentials.Username=Security.CurUser.UserName;
				dto.Credentials.PassHash=Security.CurUser.Password;
				return RemotingClient.ProcessGetDS(dto);
			}
			else {
				throw new NotSupportedException();
					//string assemb=Assembly.GetAssembly(typeof(General)).FullName;//any OpenDentBusiness class will do.
				//Type classType=Type.GetType("OpenDentBusiness."+methodBase.DeclaringType.Name);//+","+assemb);
				//MethodInfo methodI=classType.GetMethod(methodBase.Name);
				//return (DataSet)methodI.Invoke(null,parameters);
			}
		}

		///<summary>The query will NOT be used if ClientWeb.  The calling class MUST return void and must take the same parameters as passed in here.</summary>
		public static void SendCmd(MethodBase methodBase, string command, params object[] parameters) {
			#if DEBUG
				//Verify that it returns void
				MethodInfo methodInfo=methodBase.ReflectedType.GetMethod(methodBase.Name);
				if(methodInfo.ReturnType != typeof(void)) {
					throw new ApplicationException("Meth.SendCmd calling class must return void.");
				}
			#endif
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DtoSendCmd dto=new DtoSendCmd();
				dto.MethodNameCmd=methodBase.DeclaringType.Name+"."+methodBase.Name;
				//dto.Parameters=;
				dto.Parameters=parameters;
				dto.Credentials=new Credentials();
				dto.Credentials.Username=Security.CurUser.UserName;
				dto.Credentials.PassHash=Security.CurUser.Password;
				RemotingClient.ProcessCommand(dto);
				//this method will not return the result.
			}
			else {
				DataConnection dcon=new DataConnection();
				dcon.NonQ(command);
			}
		}


	}
}
