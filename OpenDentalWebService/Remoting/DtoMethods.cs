using System;
using System.Collections.Generic;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DtoMethods {
		///<summary>Processes any type of data transfer object by calling the desired method.</summary>
		public static object ProcessDtoObject(DataTransferObject dto) {
			string classAndMethod=dto.MethodName;
			List<object> parameters=new List<object>();
			for(int i=0;i<dto.Params.Count;i++) {
				parameters.Add(dto.Params[i].Obj);
			}
			return CallMethod(classAndMethod,parameters);
		}

		///<summary>Calls the classes deserializer based on the typeName passed in.  Mainly used for deserializing parameters on DtoObjects.</summary>
		public static object CallClassDeserializer(string typeName,string xml) {
			#region Primitive and General Types
			if(typeName=="long") {//TODO: Figure out if the desired object is a primitive/general type.  
				//This part of the method will be static for the crud gen, any new primitive/general class should be manually added to this section of the crud.  Then the programmer will need to manually add the new primitive or general type to the Serializing and Deserializing in Serializing.aaGeneralTypes.
				//Please make the crud have a unique "helper" method when it generates this section.  Put the name of that method in a comment here so that we can quickly go and make changes for more primitives when needed.
				aaGeneralTypes.Deserialize(typeName,xml);
			}
			#endregion
			#region Open Dental Classes
			if(typeName=="OpenDentBusiness.Account") {
				return Account.Deserialize(xml);
			}
			#endregion
			return null;//TODO: Throw an exception for unknown type?
		}

		///<summary>Finds the corresponding class, instantiates an instance of that class and invokes the method with the parameters.  Void methods will return null.</summary>
		private static object CallMethod(string classAndMethod,List<object> parameters) {
			string className=classAndMethod.Split('.')[0];
			string methodName=classAndMethod.Split('.')[1];
			if(className=="Accounts") {
				MethodAccounts(methodName,parameters);
			}
			if(className=="Patients") {
				MethodPatients(methodName,parameters);
			}
			return null;//TODO: Throw exception for unknown class.
		}

		#region Method Calls

		///<summary></summary>
		private static object MethodAccounts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="Insert") {
				return Accounts.Insert((OpenDentBusiness.Account)parameters[0]);
			}
			if(methodName=="Update") {
				Accounts.Update((OpenDentBusiness.Account)parameters[0]);
				return null;
			}
			return null;//TODO: Throw exception for unknown method.
		}

		///<summary></summary>
		private static object MethodPatients(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			return null;//An exception should be thrown here stating that the method does not exist.
		}

		#endregion

	}
}