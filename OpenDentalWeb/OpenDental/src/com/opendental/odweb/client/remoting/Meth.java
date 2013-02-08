package com.opendental.odweb.client.remoting;

public class Meth {
	
	/** Calls the server to query a method that takes no arguments that returns a DataTable. */
	public static DtoGetTable getTable(String classMethod) throws Exception {
		return getTable(classMethod,new String[0],"");
	}
	
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
	
	/** Calls the server to query the database for a DataTable.  Uses the username and password for MySQL that has less permissions. 
	 * @param command The query that needs to be executed using the MySQL user with less permissions. */
	public static DtoGetTableLow getTableLow(String command) throws Exception {
		DtoGetTableLow dto=new DtoGetTableLow();
		dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
		dto.MethodName="";
		dto.ParamTypes=new String[] { "string" };
		dto.Params=DtoObject.ConstructArray(dto.ParamTypes, new Object[] { command });
		return dto;
	}
	
	/** Calls the server to query a method that takes no arguments that returns an Integer. */
	public static DtoGetInt getInt(String classMethod) throws Exception {
		return getInt(classMethod,new String[0],"");
	}
	
	/** Calls the server to query the database for an integer. 
	 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
	 * @param paramTypes Declare the parameter types of the C# method being called into a String array. Use type names for C#.  Ex: long, Patient
	 * @param parameters An array of objects that must exactly match the parameters of the calling method. 
	 * @throws Exception The method ConstructArray can throw an exception. */
	public static DtoGetInt getInt(String classMethod,String[] paramTypes,Object... parameters) throws Exception {
		DtoGetInt dto=new DtoGetInt();
		dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
		dto.MethodName=classMethod;
		dto.ParamTypes=paramTypes;
		dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
		return dto;
	}
	
	/** Calls the server to query a method that takes no arguments that returns an Object. */
	public static DtoGetObject getObject(String classMethod) throws Exception {
		return getObject(classMethod,new String[0],"");
	}
	
	/** Calls the server to query the database for an object. 
	 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
	 * @param paramTypes Declare the parameter types of the C# method being called into a String array.  Use type names for C#.  Ex: long, Patient 
	 * @param objectType Fully qualified name of the C# object that the calling method will return.  Examples: "List&lt;OpenDentBusiness.Account&gt;" or "System.Boolean"
	 * @param parameters An array of objects that must exactly match the parameters of the calling method. 
	 * @throws Exception The method ConstructArray can throw an exception. */
	public static DtoGetObject getObject(String classMethod,String[] paramTypes,String objectType,Object... parameters) throws Exception {
		DtoGetObject dto=new DtoGetObject();
		dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
		dto.MethodName=classMethod;
		dto.ParamTypes=paramTypes;
		dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
		dto.ObjectType=Serializing.escapeForURL(objectType);
		return dto;
	}
	
	/** Calls the server to query a method that takes no arguments and has a void return type. */
	public static DtoGetVoid getVoid(String classMethod) throws Exception {
		return getVoid(classMethod,new String[0],"");
	}
	
	/** Calls the server to query the database expecting no result. 
	 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
	 * @param paramTypes Declare the parameter types of the C# method being called into a String array.  Use type names for C#.  Ex: long, Patient 
	 * @param parameters An array of objects that must exactly match the parameters of the calling method. 
	 * @throws Exception The method ConstructArray can throw an exception. */
	public static DtoGetVoid getVoid(String classMethod,String[] paramTypes,Object... parameters) throws Exception {
		DtoGetVoid dto=new DtoGetVoid();
		dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
		dto.MethodName=classMethod;
		dto.ParamTypes=paramTypes;
		dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
		return dto;
	}
	
	/** Calls the server to query a method that takes no arguments that returns a string. */
	public static DtoGetString getString(String classMethod) throws Exception {
		return getString(classMethod,new String[0],"");
	}
	
	/** Calls the server to query the database for a string. 
	 * @param classMethod Must be a string in "Class.Method" format.  Ex: Accounts.GetBalance
	 * @param paramTypes Declare the parameter types of the C# method being called into a String array.  Use type names for C#.  Ex: long, Patient 
	 * @param parameters An array of objects that must exactly match the parameters of the calling method. 
	 * @throws Exception The method ConstructArray can throw an exception. */
	public static DtoGetString getString(String classMethod,String[] paramTypes,Object... parameters) throws Exception {
		DtoGetString dto=new DtoGetString();
		dto.Credentials=new Credentials("","");// TODO Pass the user's credentials that is currently logged in.
		dto.MethodName=classMethod;
		dto.ParamTypes=paramTypes;
		dto.Params=DtoObject.ConstructArray(paramTypes,parameters);
		return dto;
	}
		
		
		
}
