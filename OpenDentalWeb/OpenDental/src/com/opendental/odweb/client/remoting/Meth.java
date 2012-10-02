package com.opendental.odweb.client.remoting;

public class Meth {
	
		/** Calls the server to query the database for an integer. 
		 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
		 * @param paramTypes Declare the parameter types of the C# method being called into a String array. 
		 * @param parameters An array of objects that must exactly match the parameters of the calling method. */
		public static int GetInt(String classMethod,String[] paramTypes,Object... parameters) {
			DtoGetInt dto=(DtoGetInt)new DataTransferObject();
			dto.MethodName=classMethod;
			dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
			return 1;
		}
		
		/** Calls the server to query the database for an object. 
		 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
		 * @param paramTypes Declare the parameter types of the C# method being called into a String array. 
		 * @param parameters An array of objects that must exactly match the parameters of the calling method. */
		public static Object GetObject(String classMethod,String[] paramTypes,Object... parameters) {
			DtoGetInt dto=(DtoGetInt)new DataTransferObject();
			dto.MethodName=classMethod;
			dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
			return null;
		}
		
		/** Calls the server to query the database expecting no result. 
		 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
		 * @param paramTypes Declare the parameter types of the C# method being called into a String array. 
		 * @param parameters An array of objects that must exactly match the parameters of the calling method. */
		public static void GetVoid(String classMethod,String[] paramTypes,Object... parameters) {
			DtoGetInt dto=(DtoGetInt)new DataTransferObject();
			dto.MethodName=classMethod;
			dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
		}
		
		
		
}
