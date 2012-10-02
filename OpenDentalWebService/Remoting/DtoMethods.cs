using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DtoMethods {
		///<summary>Processes any type of data transfer object by calling the desired method.</summary>
		public static object ProcessDtoObject(DataTransferObject dto) {
			string classAndMethod=dto.MethodName;
			object[] parameters=new object[dto.Params.Length];
			for(int i=0;i<dto.Params.Length;i++) {
				parameters[i]=dto.Params[i].Obj;
			}
			return CallMethod(classAndMethod,parameters);
		}

		///<summary>Finds the corresponding class, instantiates an instance of that class and invokes the method with the parameters.  Void methods will return null.</summary>
		private static object CallMethod(string classAndMethod,object[] parameters) {
			string className=classAndMethod.Split('.')[0];
			string methodName=classAndMethod.Split('.')[1];
			if(className=="Accounts") {
				MethodAccounts(methodName,parameters);
			}
			if(className=="Patients") {
				MethodPatients(methodName,parameters);
			}
			return null;//An exception should be thrown here saying that the class does not exist.
		}

		///<summary></summary>
		private static object MethodAccounts(string methodName,object[] parameters) {
			if(methodName=="RefreshCache") {
				}
				if(methodName=="Insert") {
					return Accounts.Insert((OpenDentBusiness.Account)parameters[0]);
				}
				if(methodName=="Update") {
					Accounts.Update((OpenDentBusiness.Account)parameters[0]);
					return null;
				}
				if(methodName=="GetDescript") {
				}
				if(methodName=="GetAccount") {
				}
				if(methodName=="Delete") {
				}
				if(methodName=="DebitIsPos") {
				}
				if(methodName=="GetBalance") {
				}
				if(methodName=="DepositsLinked") {
				}
				if(methodName=="PaymentsLinked") {
				}
				if(methodName=="GetDepositAccounts") {
				}
				if(methodName=="GetDepositAccountsQB") {
				}
				if(methodName=="GetIncomeAccountsQB") {
				}
				if(methodName=="GetFullList") {
				}
			return null;//An exception should be thrown here stating that the method does not exist.
		}

		///<summary></summary>
		private static object MethodPatients(string methodName,object[] parameters) {
			//This is an example for handling polymorphism.  The crud generator should throw an exception or let us know if two methods share the same number of parameters. (Derek said we shouldn't have any in the program).  This code assumes that every method has a unique number of parameters that it accepts.  If this is not the case, we need to rethink this logic.
			if(methodName=="GetPaymentStartingBalances") {
				if(parameters.Length==2) {
					return Patients.GetPaymentStartingBalances((long)parameters[0],(long)parameters[1]);
				}
				if(parameters.Length==3) {
					return Patients.GetPaymentStartingBalances((long)parameters[0],(long)parameters[1],(bool)parameters[2]);
				}
				return null;//An exception should be thrown here stating that the method with the number of parameters does not exist.
			}
			return null;//An exception should be thrown here stating that the method does not exist.
		}


	}
}