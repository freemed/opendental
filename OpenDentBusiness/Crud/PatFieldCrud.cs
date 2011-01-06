//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class PatFieldCrud {
		///<summary>Gets one PatField object from the database using the primary key.  Returns null if not found.</summary>
		internal static PatField SelectOne(long patFieldNum){
			string command="SELECT * FROM patfield "
				+"WHERE PatFieldNum = "+POut.Long(patFieldNum);
			List<PatField> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one PatField object from the database using a query.</summary>
		internal static PatField SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PatField> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of PatField objects from the database using a query.</summary>
		internal static List<PatField> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PatField> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<PatField> TableToList(DataTable table){
			List<PatField> retVal=new List<PatField>();
			PatField patField;
			for(int i=0;i<table.Rows.Count;i++) {
				patField=new PatField();
				patField.PatFieldNum= PIn.Long  (table.Rows[i]["PatFieldNum"].ToString());
				patField.PatNum     = PIn.Long  (table.Rows[i]["PatNum"].ToString());
				patField.FieldName  = PIn.String(table.Rows[i]["FieldName"].ToString());
				patField.FieldValue = PIn.String(table.Rows[i]["FieldValue"].ToString());
				retVal.Add(patField);
			}
			return retVal;
		}

		///<summary>Inserts one PatField into the database.  Returns the new priKey.</summary>
		internal static long Insert(PatField patField){
			return Insert(patField,false);
		}

		///<summary>Inserts one PatField into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(PatField patField,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				patField.PatFieldNum=ReplicationServers.GetKey("patfield","PatFieldNum");
			}
			string command="INSERT INTO patfield (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="PatFieldNum,";
			}
			command+="PatNum,FieldName,FieldValue) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(patField.PatFieldNum)+",";
			}
			command+=
				     POut.Long  (patField.PatNum)+","
				+"'"+POut.String(patField.FieldName)+"',"
				+"'"+POut.String(patField.FieldValue)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				patField.PatFieldNum=Db.NonQ(command,true);
			}
			return patField.PatFieldNum;
		}

		///<summary>Updates one PatField in the database.</summary>
		internal static void Update(PatField patField){
			string command="UPDATE patfield SET "
				+"PatNum     =  "+POut.Long  (patField.PatNum)+", "
				+"FieldName  = '"+POut.String(patField.FieldName)+"', "
				+"FieldValue = '"+POut.String(patField.FieldValue)+"' "
				+"WHERE PatFieldNum = "+POut.Long(patField.PatFieldNum);
			Db.NonQ(command);
		}

		///<summary>Updates one PatField in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(PatField patField,PatField oldPatField){
			string command="";
			if(patField.PatNum != oldPatField.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(patField.PatNum)+"";
			}
			if(patField.FieldName != oldPatField.FieldName) {
				if(command!=""){ command+=",";}
				command+="FieldName = '"+POut.String(patField.FieldName)+"'";
			}
			if(patField.FieldValue != oldPatField.FieldValue) {
				if(command!=""){ command+=",";}
				command+="FieldValue = '"+POut.String(patField.FieldValue)+"'";
			}
			if(command==""){
				return;
			}
			command="UPDATE patfield SET "+command
				+" WHERE PatFieldNum = "+POut.Long(patField.PatFieldNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one PatField from the database.</summary>
		internal static void Delete(long patFieldNum){
			string command="DELETE FROM patfield "
				+"WHERE PatFieldNum = "+POut.Long(patFieldNum);
			Db.NonQ(command);
		}

	}
}