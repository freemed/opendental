using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ObjectSerialize {
		///<summary>Goes through all the possible types of objects and returns the object serialized for Java.  This class negates the use of reflection which increases speed.</summary>
		public string Serialize(string typeName,Object obj) {
			StringBuilder strBuild=new StringBuilder();
			XmlWriter writer=XmlWriter.Create(strBuild);
			if(obj==null) {
				//This should be handled before this method... Or throw an exception here?
				return "";//Or don't do anything?
			}
			//Primitives--------------------------------------------------------------------
			if(typeName=="int") {
				return "<int>"+((int)obj).ToString()+"</int>";
			}
			if(typeName=="long") {//Web app does not use longs.
				//Convert to an integer or handle longs differently here.  
				return "<int>"+Convert.ToInt32(((long)obj)).ToString()+"</int>";//If converting fails, throw an exception?
			}
			if(typeName=="bool") {
				return "<boolean>"+(((bool)obj)?"1":"0")+"</boolean>";
			}
			if(typeName=="string") {
				return "<String>"+((string)obj).ToString()+"</String>";
			}
			if(typeName=="char") {
				return "<char>"+((char)obj).ToString()+"</char>";
			}
			if(typeName=="float") {
				return "<float>"+((float)obj).ToString()+"</float>";
			}
			if(typeName=="byte") {
				return "<byte>"+((byte)obj).ToString()+"</byte>";
			}
			if(typeName=="double") {
				return "<double>"+((double)obj).ToString()+"</double>";
			}
			//DataTable---------------------------------------------------------------------
			if(typeName=="DataTable") {
				return "";//Not used just yet.
			}
			//List<?>-----------------------------------------------------------------------
			if(typeName.StartsWith("List<")) {
				return"";//Not sure how to handle lists of objects without reflection just yet...
			}
			//Arrays------------------------------------------------------------------------
			if(typeName.Contains("")){
				return "";//This will need to be enhanced to handle simple and multidimensional arrays.
			}
			//The crud will dynamically generate everything below this line.
			//OpenDentBusiness--------------------------------------------------------------
			if(typeName=="Account") {
				return Accounts.SerializeForJava(obj);
			}
			//Then do this for every single possible Open Dental object type. (Table types and even enums?)
			return "";
		}

	}
}