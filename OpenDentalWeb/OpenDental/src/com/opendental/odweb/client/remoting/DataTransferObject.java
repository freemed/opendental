package com.opendental.odweb.client.remoting;

public class DataTransferObject {
		/** Credentials are always passed and never null. */
		public Credentials Credentials;
		/** This is the name of the method that we need to call.  "Class.Method" format.  Not used with GetTableLow. */
		public String MethodName;
		/** This is a list of parameters that we are passing.  They can be various kinds of objects. */
		public DtoObject[] Params;

	public String Serialize() {
		// TODO Write serializing code here.
		return null;
	}
	
	public static DataTransferObject Deserialize(String data) {
		// TODO Write deserializing code here.
		return null;
	}
	
	/** The username and password are internal to OD.  They are not the MySQL username and password. 
	 * We might use a different method of verifying credentials and permissions... this will probably change.*/
	public class Credentials {
		// TODO We might use a different method of verifying credentials and permissions... this will probably change.
		public String Username;
		/**  */
		public String Password;
	}
	
	public class DtoGetDS extends DataTransferObject {
	
	}

	public class DtoGetTable extends DataTransferObject {
		
	}

	public class DtoGetTableLow extends DataTransferObject {

	}

	/** Gets a long. */
	public class DtoGetLong extends DataTransferObject {
		
	}

	/** Gets an int. */
	public class DtoGetInt extends DataTransferObject {

	}

	/** Used when the return type is void.  It will still return 0 to ack. */
	public class DtoGetVoid extends DataTransferObject {
		
	}

	/** Gets an object which must be serializable.  Calling code will convert object to specific type. */
	public class DtoGetObject extends DataTransferObject {
		/** This is the "FullName" string representation of the type of object that we expect back as a result.  
		 * Examples: System.Int32, OpenDentBusiness.Patient, OpenDentBusiness.Patient[], List&lt;OpenDentBusiness.Patient&gt;.  
		 * DataTable and DataSet not allowed. */
		public String ObjectType;
	}

	/** Gets a simple string. */
	public class DtoGetString extends DataTransferObject {
		
	}

	/** Gets a bool. */
	public class DtoGetBool extends DataTransferObject {
		
	}

	/** OpenDentBusiness and all the DA classes are designed to throw an exception if something goes wrong.  
	 * If using OpenDentBusiness through the remote server, then the server catches the exception and passes it back to the main program using this DTO.  
	 * The client then turns it back into an exception so that it behaves just as if OpenDentBusiness was getting called locally. */
	public class DtoException extends DataTransferObject {
		public String Message;
	}
	
}
