//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class SecurityLogCrud {
		///<summary>Gets one SecurityLog object from the database using the primary key.  Returns null if not found.</summary>
		internal static SecurityLog SelectOne(long securityLogNum){
			string command="SELECT * FROM securitylog "
				+"WHERE SecurityLogNum = "+POut.Long(securityLogNum);
			List<SecurityLog> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one SecurityLog object from the database using a query.</summary>
		internal static SecurityLog SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SecurityLog> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of SecurityLog objects from the database using a query.</summary>
		internal static List<SecurityLog> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SecurityLog> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<SecurityLog> TableToList(DataTable table){
			List<SecurityLog> retVal=new List<SecurityLog>();
			SecurityLog securityLog;
			for(int i=0;i<table.Rows.Count;i++) {
				securityLog=new SecurityLog();
				securityLog.SecurityLogNum= PIn.Long  (table.Rows[i]["SecurityLogNum"].ToString());
				securityLog.PermType      = (Permissions)PIn.Int(table.Rows[i]["PermType"].ToString());
				securityLog.UserNum       = PIn.Long  (table.Rows[i]["UserNum"].ToString());
				securityLog.LogDateTime   = PIn.DateT (table.Rows[i]["LogDateTime"].ToString());
				securityLog.LogText       = PIn.String(table.Rows[i]["LogText"].ToString());
				securityLog.PatNum        = PIn.Long  (table.Rows[i]["PatNum"].ToString());
				retVal.Add(securityLog);
			}
			return retVal;
		}

		///<summary>Inserts one SecurityLog into the database.  Returns the new priKey.</summary>
		internal static long Insert(SecurityLog securityLog){
			return Insert(securityLog,false);
		}

		///<summary>Inserts one SecurityLog into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(SecurityLog securityLog,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				securityLog.SecurityLogNum=ReplicationServers.GetKey("securitylog","SecurityLogNum");
			}
			string command="INSERT INTO securitylog (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="SecurityLogNum,";
			}
			command+="PermType,UserNum,LogDateTime,LogText,PatNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(securityLog.SecurityLogNum)+",";
			}
			command+=
				     POut.Int   ((int)securityLog.PermType)+","
				+    POut.Long  (securityLog.UserNum)+","
				+"NOW(),"
				+"'"+POut.String(securityLog.LogText)+"',"
				+    POut.Long  (securityLog.PatNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				securityLog.SecurityLogNum=Db.NonQ(command,true);
			}
			return securityLog.SecurityLogNum;
		}

		///<summary>Updates one SecurityLog in the database.</summary>
		internal static void Update(SecurityLog securityLog){
			string command="UPDATE securitylog SET "
				+"PermType      =  "+POut.Int   ((int)securityLog.PermType)+", "
				+"UserNum       =  "+POut.Long  (securityLog.UserNum)+", "
				//LogDateTime not allowed to change
				+"LogText       = '"+POut.String(securityLog.LogText)+"', "
				+"PatNum        =  "+POut.Long  (securityLog.PatNum)+" "
				+"WHERE SecurityLogNum = "+POut.Long(securityLog.SecurityLogNum);
			Db.NonQ(command);
		}

		///<summary>Updates one SecurityLog in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(SecurityLog securityLog,SecurityLog oldSecurityLog){
			string command="";
			if(securityLog.PermType != oldSecurityLog.PermType) {
				if(command!=""){ command+=",";}
				command+="PermType = "+POut.Int   ((int)securityLog.PermType)+"";
			}
			if(securityLog.UserNum != oldSecurityLog.UserNum) {
				if(command!=""){ command+=",";}
				command+="UserNum = "+POut.Long(securityLog.UserNum)+"";
			}
			//LogDateTime not allowed to change
			if(securityLog.LogText != oldSecurityLog.LogText) {
				if(command!=""){ command+=",";}
				command+="LogText = '"+POut.String(securityLog.LogText)+"'";
			}
			if(securityLog.PatNum != oldSecurityLog.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(securityLog.PatNum)+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE securitylog SET "+command
				+" WHERE SecurityLogNum = "+POut.Long(securityLog.SecurityLogNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one SecurityLog from the database.</summary>
		internal static void Delete(long securityLogNum){
			string command="DELETE FROM securitylog "
				+"WHERE SecurityLogNum = "+POut.Long(securityLogNum);
			Db.NonQ(command);
		}

	}
}