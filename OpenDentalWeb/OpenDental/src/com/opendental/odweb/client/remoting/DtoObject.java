package com.opendental.odweb.client.remoting;

public class DtoObject {
	/** Qualified name for C#.  Ex: long, Patient, Patient[], List&lt;OpenDentBusiness.Patient&gt;. */
	public String TypeName;
	/** The actual object. */
	public Object Obj;
	/** The actual object fully serialized. */
	public String ObjSerialized;
	
	/** Sets the variables passed in and automatically sets ObjSerialized. 
	 * @param obj Null is acceptable.
	 * @param typeName Required to have a type of some kind.
	 * @throws Exception The method call to GetSerializedString can throw an exception. */
	public DtoObject(Object obj,String typeName) throws Exception {
		Obj=obj;
		TypeName=typeName;
		ObjSerialized=getSerializedString();
	}
	
	/** We must pass in a matching array of types for situations where nulls are used in parameters.  Otherwise, we won't know the parameter type. 
	 * @throws Exception DtoObject constructor can throw an exception. */
	public static DtoObject[] ConstructArray(String[] paramType,Object[] objArray) throws Exception {
		DtoObject[] retVal=new DtoObject[objArray.length];
		for(int i=0;i<objArray.length;i++) {
			retVal[i]=new DtoObject(objArray[i],paramType[i]);
		}
		return retVal;
	}
	
	/** Loops through general types first.  If no match was found then it calls a method that loops through all the Open Dental types. 
	 * @throws Exception Throws unsupported type exception. */
	public String getSerializedString() throws Exception {
		if(Obj==null) {
			return "<"+TypeName+"/>";
		}
		return Serializing.getSerializedObject(Obj);
	}
	
	
}
