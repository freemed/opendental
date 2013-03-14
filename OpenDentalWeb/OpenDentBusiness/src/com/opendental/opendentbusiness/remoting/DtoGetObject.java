package com.opendental.opendentbusiness.remoting;

/** Gets an object which must be serializable.  Calling code will convert object to specific type. */
public class DtoGetObject extends DataTransferObject {
	/** This is the "FullName" string representation of the type of object that we expect back as a result.  
	 * Examples: System.Int32, OpenDentBusiness.Patient, OpenDentBusiness.Patient[], List&lt;OpenDentBusiness.Patient&gt;.  
	 * DataTable and DataSet not allowed. */
	public String ObjectType;
}
