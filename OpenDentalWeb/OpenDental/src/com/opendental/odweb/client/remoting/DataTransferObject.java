package com.opendental.odweb.client.remoting;

public class DataTransferObject {
	/** Credentials are always passed and never null. */
	public Credentials Credentials;
	/** This is the name of the method that we need to call.  "Class.Method" format.  Not used with GetTableLow. */
	public String MethodName;
	/** This is a list of parameters that we are passing.  They can be various kinds of objects. */
	public DtoObject[] Params;
	/** This is a list of parameter types that we are passing.  They must directly match the list of parameters. */
	public String[] ParamTypes;
	/** Used to let the server know what type of dto object it is.  This gets set in the constructor so it will always have a value.  Ex: DtoGetInt */
	public String DtoType;
	
	/** Constructor figures out the type of dto object that got instantiated. */
	public DataTransferObject() {
		String dtoType=this.getClass().getName();//com.opendental.odweb.client.remoting.DtoGetInt
		//In theory this will always be a fully qualified name so this check might be unnecessary.  It might even be better to have it fail...
		if(dtoType.lastIndexOf('.')>0) {
			DtoType=dtoType.substring(dtoType.lastIndexOf('.')+1);
		}
	}

	/**  */
	public String Serialize() {
		StringBuilder xml=new StringBuilder();
		//Header-------------------------------------------------------------------------------
		xml.append("<?xml version=\"1.0\" encoding=\"utf-16\"?><"
				+SerializeStringEscapes.EscapeForXml(DtoType)+" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
		//Credentials will be differently than this--------------------------------------------
		xml.append("<Credentials><UserName>"
				+SerializeStringEscapes.EscapeForXml(Credentials.Username)+"</Username><Password>"
				+SerializeStringEscapes.EscapeForXml(Credentials.Password)+"</Password></Credentials>");
		//MethodName---------------------------------------------------------------------------
		xml.append("<MethodName>"
				+SerializeStringEscapes.EscapeForXml(MethodName)+"</MethodName>");
		//Parameters---------------------------------------------------------------------------
		xml.append("<Params>");
		for(int i=0;i<Params.length;i++) {//Loop through all the dto objects.
			xml.append("<DtoObject>");
			xml.append("<TypeName>"+SerializeStringEscapes.EscapeForXml(Params[i].TypeName)+"</TypeName>");
			xml.append("<Obj>");
			//Without reflection we cannot know what type of object this is so it has to be serialized before coming in here.
			xml.append(Params[i].ObjSerialized);//Valid XML that does not need escaping.
			xml.append("</Obj>");
			xml.append("</DtoObject>");
		}
		xml.append("</Params>");
		xml.append("</"+SerializeStringEscapes.EscapeForXml(DtoType)+">");//End of dto object.
		return xml.toString();
	}
	
	public static DataTransferObject Deserialize(String data) {
		// TODO Write deserializing code here.
		return null;
	}
	
}
