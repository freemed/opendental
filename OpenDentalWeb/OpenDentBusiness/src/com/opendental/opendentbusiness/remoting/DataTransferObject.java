package com.opendental.opendentbusiness.remoting;

public class DataTransferObject {
	/** Credentials are always passed and never null. */
	public Credentials Credentials;
	/** This is the name of the method that we need to call.  "Class.Method" format.  Not used with GetTableLow. */
	public String MethodName;
	/** This is a list of parameters that we are passing.  They can be various kinds of objects. */
	public DtoObject[] Params;
	/** This is a list of parameter types that we are passing.  This array must directly match the count of Params. */
	public String[] ParamTypes;
	/** Used to let the server know what type of dto object it is.  This gets set in the constructor so it will always have a value.  Ex: DtoGetInt */
	public String Type;
	
	/** Constructor figures out the type of dto object that got instantiated. */
	public DataTransferObject() {
		String dtoType=this.getClass().getName();//com.opendental.odweb.client.remoting.DtoGetInt
		//In theory this will always be a fully qualified name so this check might be unnecessary.  It might even be better to have it fail...
		if(dtoType.lastIndexOf('.')>0) {
			Type=dtoType.substring(dtoType.lastIndexOf('.')+1);
		}
	}

	/**  */
	public String serialize() {
		StringBuilder xml=new StringBuilder();
		//Header-------------------------------------------------------------------------------
		xml.append("<?xml version=\"1.0\" encoding=\"utf-16\"?><"
				+Serializing.escapeForXml(Type)+" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
		//Credentials -------------------------------------------------------------------------
		xml.append("<Credentials><Username>"
				+Serializing.escapeForXml(Credentials.Username)+"</Username><Password>"
				+Serializing.escapeForXml(Credentials.Password)+"</Password></Credentials>");
		//MethodName---------------------------------------------------------------------------
		xml.append("<MethodName>"
				+Serializing.escapeForXml(MethodName)+"</MethodName>");
		//Parameters---------------------------------------------------------------------------
		xml.append("<Params>");
		for(int i=0;i<Params.length;i++) {//Loop through all the dto objects.
			xml.append("<DtoObject>");
			xml.append("<TypeName>"+Serializing.escapeForURL(Params[i].TypeName)+"</TypeName>");
			xml.append("<Obj>");
			xml.append(Params[i].ObjSerialized);//Already valid XML that does not need escaping.
			xml.append("</Obj>");
			xml.append("</DtoObject>");
		}
		xml.append("</Params>");
		if(this.Type.equals("DtoGetObject")) {
			DtoGetObject dto=(DtoGetObject)this;
			String objType=dto.ObjectType;
			xml.append("<ObjectType>"+objType+"</ObjectType>");
		}
		xml.append("</"+Serializing.escapeForXml(Type)+">");//End of dto object.
		return xml.toString();
	}
	
	/**  */
	public static DataTransferObject Deserialize(String data) {
		// TODO Write deserializing code specifically for DataTransferObjects here if we end up needing it.
		return null;
	}
	
	
}
