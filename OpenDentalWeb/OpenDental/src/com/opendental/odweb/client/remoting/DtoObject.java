package com.opendental.odweb.client.remoting;

public class DtoObject {
	/** Qualified name for C#.  Ex: long, Patient, Patient[], List&lt;OpenDentBusiness.Patient&gt;. */
	public String TypeName;
	/** The actual object. */
	public Object Obj;
	/** The actual object fully serialized. */
	public String ObjSerialized;
	
	/** Sets the variables passed in and automatically sets ObjSerialized. */
	public DtoObject(Object obj,String typeName) {
		Obj=obj;
		TypeName=typeName;
		ObjSerialized=GetSerializedString();
	}
	
	/** We must pass in a matching array of types for situations where nulls are used in parameters.  Otherwise, we won't know the parameter type. */
	public static DtoObject[] ConstructArray(String[] paramType,Object[] objArray) {
		DtoObject[] retVal=new DtoObject[objArray.length];
		for(int i=0;i<objArray.length;i++) {
			retVal[i]=new DtoObject(objArray[i],paramType[i]);
		}
		return retVal;
	}
	
	public String GetSerializedString() {
		//Figure out what type of object we're dealing with and return the serialized form.
		String qualifiedName=Obj.getClass().getName();//Ex: ArrayList = "java.util.ArrayList"
		//Primitives--------------------------------------------------------------------------------------------------------
		if(qualifiedName=="Z") {//boolean "Z"
			return "<Obj><boolean></boolean></Obj>";
		}
		if(qualifiedName=="B") {//byte    "B"
			
		}
		if(qualifiedName=="C") {//char    "C"
			
		}
		if(qualifiedName=="S") {//short   "S"
			
		}
		if(qualifiedName=="I") {//int     "I"
			
		}
		if(qualifiedName=="J") {//long    "J"
			
		}
		if(qualifiedName=="F") {//float   "F"
			
		}
		if(qualifiedName=="D") {//double  "D"
			
		}
		if(qualifiedName=="java.lang.String") {//String  "java.lang.String"
			
		}
		if(qualifiedName=="") {
			
		}
		//Arrays------------------------------------------------------------------------------------------------------------
		//Multidimensional arrays have equal number of brackets. Ex: Account[][] = [[L...
		//Object[]  "[Lcom.opendental.odweb.client.tabletypes.Account;" from Account[]
		//int[]     "[I"
		//String[]  "[Ljava.lang.String;"
		return "";
	}
	
//	/** Takes any Java object and returns the C# fully qualified name. Ex: System.Int32 */
//	public static String GetQualifiedName(Object obj) {
//		String name=GetObjectType(obj);
//		//Generic types------------------------------------------------------------------------------------
//		if(name=="ArrayList") {//List<>
//			try {
//				//name="List<"+GetObjectType(((ArrayList<Object>)obj).get(0))+">";//This will fail on an empty list.
//			}
//			finally {
//				//Something went wrong or an empty list was passed in... I have no idea how to know the type of an empty ArrayList...
//			}
//		}
//		return ConvertUnqualifiedToQualified(name);
//	}
//	
//	/** Converts the unqualified Java class into the fully qualified class for C#. Ex: System.Int32, OpenDentBusiness.Account, OpenDentBusiness.Patient[], List&lt;OpenDentBusiness.Patient&gt; */
//	private static String ConvertUnqualifiedToQualified(String name) {
//		if(name.startsWith("List<")) {//List<Object> already handled.
//			return name;
//		}
//		//Must be one of our objects so just precede the object's name with OpenDentBusiness.
//		return name;
//	}
	
	
}
