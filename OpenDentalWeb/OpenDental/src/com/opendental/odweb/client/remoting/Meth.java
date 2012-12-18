package com.opendental.odweb.client.remoting;

public class Meth {
	
		/** Calls the server to query the database for a DataTable. 
		 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
		 * @param paramTypes Declare the parameter types of the C# method being called into a String array.  Use type names for C#.  Ex: long, Patient
		 * @param parameters An array of objects that must exactly match the parameters of the calling method. 
		 * @throws Exception The method ConstructArray can throw an exception. */
		public static DtoGetTable getTable(String classMethod,String[] paramTypes,Object... parameters) throws Exception {
			DtoGetTable dto=new DtoGetTable();
			dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
			dto.MethodName=classMethod;
			dto.ParamTypes=paramTypes;
			dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
			return dto;
		}
	
		/** Calls the server to query the database for an integer. 
		 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
		 * @param paramTypes Declare the parameter types of the C# method being called into a String array. Use type names for C#.  Ex: long, Patient
		 * @param parameters An array of objects that must exactly match the parameters of the calling method. 
		 * @throws Exception The method ConstructArray can throw an exception. */
		public static int getInt(String classMethod,String[] paramTypes,Object... parameters) throws Exception {
			DtoGetInt dto=new DtoGetInt();
			dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
			dto.MethodName=classMethod;
			dto.ParamTypes=paramTypes;
			dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
			return 1;
		}
		
		/** Calls the server to query the database for an object. 
		 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
		 * @param paramTypes Declare the parameter types of the C# method being called into a String array.  Use type names for C#.  Ex: long, Patient 
		 * @param parameters An array of objects that must exactly match the parameters of the calling method. 
		 * @throws Exception The method ConstructArray can throw an exception. */
		public static Object getObject(String classMethod,String[] paramTypes,Object... parameters) throws Exception {
			DtoGetObject dto=new DtoGetObject();
			dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
			dto.MethodName=classMethod;
			dto.ParamTypes=paramTypes;
			dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
			return null;
		}
		
		/** Calls the server to query the database expecting no result. 
		 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
		 * @param paramTypes Declare the parameter types of the C# method being called into a String array.  Use type names for C#.  Ex: long, Patient 
		 * @param parameters An array of objects that must exactly match the parameters of the calling method. 
		 * @throws Exception The method ConstructArray can throw an exception. */
		public static void getVoid(String classMethod,String[] paramTypes,Object... parameters) throws Exception {
			DtoGetVoid dto=new DtoGetVoid();
			dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
			dto.MethodName=classMethod;
			dto.ParamTypes=paramTypes;
			dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
		}
		
		
		
}
